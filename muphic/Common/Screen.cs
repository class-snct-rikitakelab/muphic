using System;
using System.Collections.Generic;
using System.Drawing;

using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.Common
{
	/// <summary>
	/// 汎用画面クラス。
	/// <para>各部の画面はこのクラスを継承して作成する。</para>
	/// </summary>
	public abstract class Screen : Parts, IDisposable
	{

		#region プロパティ

		/// <summary>
		/// 画面を構成する各部品のリスト。
		/// </summary>
		protected List<Parts> PartsList { get; set; }

		/// <summary>
		/// 以前のフレームの時点でマウスが指していた部品の要素番号を表す整数を取得または設定する。
		/// </summary>
		private int PrevPartsNum { get; set; }

		/// <summary>
		/// ドラックを開始した時にクリックした部品の要素番号を表す整数を取得または設定する。
		/// </summary>
		private int BeginPartsNum { get; set; }


		/// <summary>
		/// DrawManager への登録番号を取得または設定する。
		/// <para>クラス解放時に DrawManager に登録したキーとテクスチャを一括削除する際に使用する。</para>
		/// </summary>
		private int RegistNum { get; set; }

		/// <summary>
		/// この画面で使用する統合画像ファイル名のリスト。
		/// Key はファイル名で、Value はこの画面が生成された時点で既に読み込まれていたかどうかを示すブール値。
		/// </summary>
		/// <remarks>
		/// Value が true の場合は既に読み込まれているので、この画面を生成する際に読み込む必要が無く、また、この画面を破棄する際も解放する必要が無い。
		/// </remarks>
		private Dictionary<string, bool> UseImageFileNameList { get; set; }

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// 汎用画面クラスの初期化を行う。
		/// </summary>
		protected Screen()
		{
			this.PrevPartsNum = -1;					// 番号クリア
			this.BeginPartsNum = -1;				// 番号クリア
			this.PartsList = new List<Parts>();		// リスト初期化

			this.RegistNum = -1;
			this.UseImageFileNameList = new Dictionary<string, bool>();
		}


		/// <summary>
		/// 画面クラスを構成する各部品のインスタンス化などを行う。
		/// </summary>
		protected virtual void Initialization()
		{
		}

		/// <summary>
		/// 画面クラスを構成する各部品のインスタンス化などを行う。
		/// 自動的に DrawManager の BeginRegist メソッドが実行されるため、このメソッドを実行した場合は後に必ず EndRegist メソッドも実行しなければならない。
		/// </summary>
		/// <param name="loadFileNames">この画面で使用する統合画像ファイル名 (セミコロン区切りで複数指定可能)。</param>
		protected void Initialization(string loadFileNames)
		{
			// この使用する統合画像ファイル名のリストを取得する
			CommonTools.GetResourceFileNames(this.UseImageFileNameList, loadFileNames);

			// 登録開始を管理クラスへ通知
			// このとき、読み込まれていない画像が 1 つでもあれば NowLoading ダイアログを表示する
			this.RegistNum = DrawManager.BeginRegist(this.UseImageFileNameList.ContainsValue(false));
		}


		/// <summary>
		/// この画面で使用する統合画像のうち、読み込まれていない全ての画像ファイルを読み込む。
		/// </summary>
		protected void LoadImageFiles()
		{
			foreach (KeyValuePair<string, bool> useImageFileName in this.UseImageFileNameList)
			{
				DrawManager.LoadTextureFile(useImageFileName.Key);
			}
		}

		/// <summary>
		/// この画面で使用する統合画像のうち、この画面が生成された時点で新たに読み込まれた画像を全て解放する。
		/// </summary>
		protected void UnloadImageFiles()
		{
			foreach (KeyValuePair<string, bool> useImageFileName in this.UseImageFileNameList)
			{
				if (!useImageFileName.Value) DrawManager.UnLoadTextureFile(useImageFileName.Key);
			}
		}

		#endregion


		#region 描画関連メソッド群

		/// <summary>
		/// 画面と、画面を構成する全ての可視状態の部品を描画する。
		/// <para>
		/// クラステクスチャ (this.ToString() で登録されたテクスチャ) を強制的に描画することになるため、
		/// クラステクスチャの描画が必要ない場合 (エリア指定目的で作成されたクラスなど) はこの基本クラスは使用せず、
		/// DrawParts メソッドを使用するとよい。
		/// </para>
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public virtual void Draw(DrawStatusArgs drawStatus)
		{
			// if (this.Visible == false) return;		// 不可視に設定されていたら描画しない

			DrawManager.Draw(this.ToString());		// まずは自分自身を描画

			this.DrawParts(drawStatus);				// 続けて各パーツを描画

			return;
		}

		/// <summary>
		/// 画面に含まれるパーツを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public virtual void DrawParts(DrawStatusArgs drawStatus)
		{
			// 部品リスト内の全ての部品に対する描画処理
			for (int i = 0; i < PartsList.Count; i++)
			{
				if (PartsList[i] is Screen)
				{
					// i番目の部品がScreen型だった場合
					// 可視状態であれば、そのScreen型の描画を行う
					if (((Screen)PartsList[i]).Visible) ((Screen)PartsList[i]).Draw(drawStatus);
				}
				else if (PartsList[i] is Button)
				{
					// i番目の部品がButton型だった場合
					// 可視状態であれば、そのButton型の描画を行う
					if (((Button)PartsList[i]).Visible) ((Button)PartsList[i]).Draw(drawStatus);
				}
				else
				{
					// i番目の部品がそれ以外だった場合
					// 可視状態であれば、その部品の描画を行う
					if (PartsList[i].Visible) DrawManager.Draw(PartsList[i].ToString(), PartsList[i].State);
				}
			}

			return;
		}

		#endregion


		#region マウス操作関連メソッド群

		/// <summary>
		/// 画面内でマウスが移動した場合の処理を行う。
		/// <para>ここで各部品の MouseEnter メソッドや MouseLeave メソッドが呼び出される。</para>
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public virtual void MouseMove(MouseStatusArgs mouseStatus)
		{
			int nowPartsNum = -1;

			// 現在の座標がどの部品内にあるのかを探す
			for (int i = PartsList.Count - 1; i >= 0; i--)
			{
				// 不可視状態であれば無視し次に進む
				if (PartsList[i].Visible == false) continue;

				// 可視状態でも無効になっているボタンは無視して次に進む
				if ((PartsList[i] is Button) && !((Button)PartsList[i]).Enabled) continue;
				
				// 座標管理クラスからリスト内i番目の部品の座標とサイズを得る
				if(RectangleManager.Get(PartsList[i].ToString()).Contains(mouseStatus.NowLocation))
				{
					// 現在の座標が部品の中に入っている場合の処理

					// もし部品がScreen型なら、そのMouseMoveも呼ぶ
					if (PartsList[i] is Screen) ((Screen)PartsList[i]).MouseMove(mouseStatus);

					// 探索完了
					nowPartsNum = i;
					break;
				}
			}

			if (PrevPartsNum != nowPartsNum)
			{
				// 前のフレームでマウスが指していた部品と現時点での部品が違っていた場合(更に、ドラッグ中でなければ)

				// 前のフレームで部品を指していなかった場合、今回になって新たに何らかの部品を指したことになる
				// 部品のMouseEnterメソッドを呼ぶ
				if (PrevPartsNum == -1) PartsList[nowPartsNum].MouseEnter();

				// 現時点で部品を指していない場合、今回になって何らかの部品からマウスが出たことになる
				// 前のフレームで指していた部品のMouseLeaveメソッドを呼ぶ
				else if (nowPartsNum == -1) PartsList[PrevPartsNum].MouseLeave();

				// 現時点で前のフレームと違う部品を指している場合
				// 前のフレームで指していた部品のMouseLeaveメソッドと現在指している部品のMouseEnterメソッドを呼ぶ
				else
				{
					PartsList[PrevPartsNum].MouseLeave();
					PartsList[nowPartsNum].MouseEnter();
				}
			}

			// 次のフレームのため現時点で指している部品番号をコピー
			this.PrevPartsNum = nowPartsNum;

			return;
		}


		#region クリック関連メソッド群

		/// <summary>
		/// マウスが画面内でクリックされた場合の処理を行う。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			for (int i = PartsList.Count - 1; i >= 0; i--)
			{
				// リスト内の全ての部品に対し以下の処理を実行

				if (RectangleManager.Get(PartsList[i].ToString()).Contains(mouseStatus.NowLocation) &&
					RectangleManager.Get(PartsList[i].ToString()).Contains(mouseStatus.BeginLocation) &&
					PartsList[i].Visible)
				{
					// 部品の四角形領域内かつ可視状態である場合

					// 部品がボタンで、かつ無効状態の場合はクリックせず終了
					if ((PartsList[i] is Button) && !((Button)PartsList[i]).Enabled) return;

					// ☆☆☆ クリックのお知らせ ☆☆☆
					Muphic.Tools.DebugTools.ConsolOutputMessage("Click -" + this.ToString(), PartsList[i].ToString(), true);

					// 部品のClickメソッドを呼ぶ
					PartsList[i].Click(mouseStatus);

					return;
				}
			}

			return;
		}


		/// <summary>
		/// マウスが画面内でクリックされた場合の処理を行う。
		/// <para>許可リストで指定された部品のみクリックが可能となる。</para>
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		/// <param name="permissionList">クリックを許可する部品のリスト(クラスのToStringで指定)。</param>
		public void Click(MouseStatusArgs mouseStatus, String[] permissionList)
		{
			for (int i = PartsList.Count - 1; i >= 0; i--)
			{
				// リスト内の全ての部品に対し以下の処理を実行

				if (RectangleManager.Get(PartsList[i].ToString()).Contains(mouseStatus.NowLocation) && PartsList[i].Visible)
				{
					// 部品の四角形領域内かつ可視状態である場合

					for (int j = 0; j < permissionList.Length; j++)
					{
						if (PartsList[i].ToString() == permissionList[j])
						{
							// 許可リスト内に部品名が存在した場合

							// 部品がボタンで、かつ無効状態の場合はクリックせず終了
							if ((PartsList[i] is Button) && !((Button)PartsList[i]).Enabled) return;

							// ☆☆☆ クリックのお知らせ ☆☆☆
							Muphic.Tools.DebugTools.ConsolOutputMessage("クリック", PartsList[i].ToString(), true);

							// 部品のClickメソッドを呼ぶ
							PartsList[i].Click(mouseStatus);

							return;
						}
					}
				}
			}

			return;
		}

		#endregion


		#region ドラッグ関連メソッド群

		/// <summary>
		/// ドラッグが開始された時に呼ばれるメソッド
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public virtual void DragBegin(MouseStatusArgs mouseStatus)
		{
			for (int i = PartsList.Count - 1; i >= 0; i--)
			{
				// 部品リスト内の全ての部品に対し以下を実行

				// 部品の位置とサイズを得る
				Rectangle r = RectangleManager.Get(PartsList[i].ToString());

				if (r.Contains(mouseStatus.BeginLocation))
				{
					// 部品の四角形領域内かつ可視状態である場合

					// 部品がScreen型なら、そちらのメソッドを呼ぶ
					if (PartsList[i] is Screen) ((Screen)PartsList[i]).DragBegin(mouseStatus);

					// 探索完了
					this.BeginPartsNum = i;

					break;
				}
			}
		}


		/// <summary>
		/// ドラッグが終了したときに呼ばれるメソッド
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public virtual void DragEnd(MouseStatusArgs mouseStatus)
		{
			// ドラッグが開始されていなければ終了
			if (this.BeginPartsNum == -1) return;

			// ドラッグ中の部品がScreen型であれば、そちらのメソッドを呼ぶ
			if (PartsList[this.BeginPartsNum] is Screen) ((Screen)PartsList[this.BeginPartsNum]).DragEnd(mouseStatus);

			// ドラッグ終了
			this.BeginPartsNum = -1;
		}

		#endregion

		#endregion


		#region キーボード関連メソッド群

		/// <summary>
		/// キーボードの何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態を示す Muphic.KeyboardStatusArgs クラス。</param>
		public virtual void KeyDown(KeyboardStatusArgs keyStatus)
		{
		}


		#endregion


		#region 解放

		/// <summary>
		/// この画面で使用したリソースを解放する。この画面が生成された時点で新たに読み込まれた統合画像ファイルは、自動的に解放される。
		/// </summary>
		public virtual void Dispose()
		{
			// この画面が生成される時点で既に読み込まれていた統合画像ファイルは全てそのままにし、
			// 新たに読み込まれた統合画像ファイルのみ解放する
			foreach (KeyValuePair<string, bool> useImageFileName in this.UseImageFileNameList)
			{
				if (!useImageFileName.Value) DrawManager.UnLoadTextureFile(useImageFileName.Key);
			}

			// 登録番号が有効な場合は、テクスチャと座標の削除を行う
			if (this.RegistNum >= 0) DrawManager.Delete(this.RegistNum);

			// それぞれの部品をチェックし、解放メソッドを実装していれば解放させる
			foreach (Parts parts in this.PartsList)
			{
				if (parts is IDisposable) this.SafeDispose((IDisposable)parts);
			}
		}

		/// <summary>
		/// リソースを安全に破棄する。
		/// </summary>
		/// <param name="resource">破棄するリソース。</param>
		protected void SafeDispose(IDisposable resource)
		{
			if (resource != null)
			{
				resource.Dispose();
			}
		}

		#endregion

	}
}
