using System.Drawing;

using Muphic.Manager;
using Muphic.CompositionScreenParts.CompositionMainParts;
using Muphic.CompositionScreenParts.CompositionMainParts.Buttons;
using Muphic.Tools;

namespace Muphic.CompositionScreenParts
{
	/// <summary>
	/// 作曲部の主クラス(ver.7以前はmuphic.One.Score)
	/// </summary>
	public class CompositionMain : Muphic.Common.Screen
	{

		#region 作曲部を構成する各部品のインスタンスのプロパティ群

		/// <summary>
		/// 親にあたる作曲画面
		/// </summary>
		public CompositionScreen Parent { get; private set; }


		/// <summary>
		/// 小節区切りを表わす看板
		/// </summary>
		public SignBorad SignBoard { get; private set; }

		/// <summary>
		/// 楽譜(道)の左スクロールボタン
		/// </summary>
		public LeftScrollButton LeftScrollButton { get; private set; }

		/// <summary>
		/// 楽譜(道)の右スクロールボタン
		/// </summary>
		public RightScrollButton RightScrollButton { get; private set; }

		/// <summary>
		/// 楽譜(道)のスクロールバー
		/// </summary>
		public ScrollBar ScrollBar { get; private set; }


		/// <summary>
		/// 楽譜(道)の背景
		/// </summary>
		public ScoreBackground ScoreBackground { get; private set; }

		#endregion


		#region 作曲部の動作に関するフィールドとそのプロパティ群

		/// <summary>
		/// 楽譜上の動物を管理する。
		/// </summary>
		public AnimalScore AnimalScore { get; private set; }


		/// <summary>
		/// 現在表示中の位置を取得または設定する。
		/// </summary>
		public int NowPlace { get; private set; }

		/// <summary>
		/// 非再生時の表示位置 (再生終了時にこの位置に戻る) を取得または設定する。
		/// </summary>
		public int DefaultPlace { get; private set; }


		/// <summary>
		/// 再生中かどうかを表わすフラグを取得または設定する。
		/// <para>IsPlaying プロパティを使用すること。</para>
		/// </summary>
		private bool __isPlaying;

		/// <summary>
		/// 再生中かどうかを表わすフラグを取得または設定する。
		/// 値の変更と連動し、各コントロールの有効/無効も切り替わる。
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				return this.__isPlaying;
			}
			set
			{
				this.__isPlaying = value;

				// 再生開始/再生終了に関わらず以下を実行
				this.Parent.AnimalButtons.Enabled = !value;				// 動物ボタン
				this.Parent.Tempo.SettingVisibleTempoButtons(!value);	// テンポボタンの可視/不可視の切り替え
				this.ScrollBar.Enabled = !value;						// スクロールバーとボタンの有効/無効の切り替え
				this.Parent.TitleButton.Enabled = !value;				// 題名ボタンの有効/無効の切り替え
				this.Parent.CreateNewButton.Enabled = !value;			// あたらしくつくるボタンの有効/無効の切り替え
				this.Parent.ScoreSaveButton.Enabled = !value;			// ファイル保存ボタンの有効/無効の切り替え
				this.Parent.ScoreLoadButton.Enabled = !value;			// ファイル読込ボタンの有効/無効の切り替え
				this.Parent.ScoreButton.Enabled = !value;				// 楽譜ボタンの有効/無効の切り替え

				if (!value)
				{
					// 再生終了時はさらに以下を実行
					this.Parent.PlayFirstButton.Enabled = this.Parent.PlayContinueButton.Enabled = true;	// 再生ボタン二種を有効化
					this.Parent.PlayFirstButton.Pressed = this.Parent.PlayContinueButton.Pressed = false;	// 再生ボタン二種の状態を通常に戻す
				}
			}
		}


		/// <summary>
		/// ドラッグ開始時に選択されていた動物の要素番号 (選択されていない場合は -1) を取得または設定する。
		/// </summary>
		public int DragBeginAnimalNum { get; private set; }

		#endregion


		#region コンストラクタと初期設定

		/// <summary>
		/// 作曲部主クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる作曲画面。</param>
		public CompositionMain(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.Initialization();

			this.ScrollBar.Enabled = true;

			this.DragBeginAnimalNum = -1;
		}


		/// <summary>
		/// 作曲部を構成する各部品のインスタンス化等を行う。
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();

			// ==============================
			//      部品のインスタンス化
			// ==============================
			this.AnimalScore = new AnimalScore(this);
			this.SignBoard = new SignBorad(this);
			this.LeftScrollButton = new LeftScrollButton(this);
			this.RightScrollButton = new RightScrollButton(this);
			this.ScrollBar = new ScrollBar(this);
			this.ScoreBackground = new ScoreBackground(this);

			// ==============================
			//      部品のリストへの登録
			// ==============================
			this.PartsList.Add(this.ScoreBackground);
			//this.PartsList.Add(this.SignBoard);
			this.PartsList.Add(this.LeftScrollButton);
			this.PartsList.Add(this.RightScrollButton);
			this.PartsList.Add(this.ScrollBar);

			// ==============================
			//     テクスチャと座標の登録
			// ==============================
			DrawManager.Regist(this.ToString(), Locations.CompositionArea.Location, "IMAGE_COMPOSITIONSCR_COMPOSITIONAREA");
		}

		#endregion


		#region 描画

		/// <summary>
		/// 作曲部の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			// 何らかの理由で不可視に設定されていた場合は描画しない
			if (!this.Visible) return;

			// 楽譜上の動物たちの描画
			if (this.AnimalScore.Draw(this.NowPlace, this.Parent.Tempo.TempoMode, this.IsPlaying, drawStatus))
			{
				// 再生終了のシグナルが返ってきた場合
				this.PlayStop();
			}

			//this.SignBoard.Draw(drawStatus);
		}

		#endregion


		#region クリック/ドラッグ

		/// <summary>
		/// 楽譜がクリックされた際の処理を行う。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.IsPlaying) return;				// 再生中であればクリックさせない

			Point clickPlace = CompositionTools.GetNearestScore(mouseStatus.NowLocation);	// クリック位置が楽譜上でどの位置・音階なのかを決定する
			if (clickPlace == Point.Empty) return;									// クリック位置が楽譜外(もしくはそれに相当する座標)と判断された場合、帰る。

			clickPlace.X += this.NowPlace;			// オフセット加算

			DebugTools.ConsolOutputMessage("CompositionMain -Click", "位置:" + clickPlace.X + ", 音階:" + clickPlace.Y);

			if (this.Parent.AnimalButtons.NowAnimalButtonMode != AnimalButtonMode.None)
			{
				// 動物選択ボタンのうち何かが選択されている状態であれば以下を実行

				if (this.Parent.AnimalButtons.NowAnimalButtonMode == AnimalButtonMode.Delete)
				{
					// 選択されているのが削除ボタンであれば、当該位置の動物を削除
					this.AnimalScore.Delete(clickPlace.X, clickPlace.Y);
				}
				else
				{
					// 選択されているのがそれ以外(動物ボタン)でれば、当該位置に動物を追加
					this.AnimalScore.Insert(clickPlace.X, clickPlace.Y, this.Parent.AnimalButtons.NowAnimalButtonMode);
				}
			}
		}


		/// <summary>
		/// 作曲部上でマウスが移動した際の処理を行う。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void MouseMove(MouseStatusArgs mouseStatus)
		{
			base.MouseMove(mouseStatus);

			if (mouseStatus.IsDrag && this.DragBeginAnimalNum != -1)
			{
				// ドラッグ開始時に何らかの動物を選択していた場合、以下の処理を実行

				Point NowAnimalPlace = CompositionTools.GetNearestScore(mouseStatus.NowLocation);
				if (NowAnimalPlace == Point.Empty || this.Parent.AnimalButtons.NowAnimalButtonMode == AnimalButtonMode.Delete) return;

				int NewAnimalNum = this.AnimalScore.Replace(
					this.AnimalScore[this.DragBeginAnimalNum].Place, this.AnimalScore[this.DragBeginAnimalNum].Code,
					NowAnimalPlace.X + this.NowPlace, NowAnimalPlace.Y,
					true, true
				);

				if (NewAnimalNum != -1) this.DragBeginAnimalNum = NewAnimalNum;
			}
		}


		/// <summary>
		/// 作曲部上でドラッグを開始する。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragBegin(MouseStatusArgs mouseStatus)
		{
			base.DragBegin(mouseStatus);

			Point dragBeginPlace = CompositionTools.GetNearestScore(mouseStatus.BeginLocation);						// クリック座標から楽譜内での音階と位置を決定
			this.DragBeginAnimalNum = AnimalScore.Exists(dragBeginPlace.X + this.NowPlace, dragBeginPlace.Y);	// 現在選択されている動物の要素番号を格納
		}

		/// <summary>
		/// 作曲部上でドラッグを終了する。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragEnd(MouseStatusArgs mouseStatus)
		{
			base.DragEnd(mouseStatus);

			this.DragBeginAnimalNum = -1;								// 動物の選択を解除
		}

		#endregion


		#region キーボード

		/// <summary>
		/// 何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus"></param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
		}

		#endregion


		#region 再生/停止

		/// <summary>
		/// 楽譜の最初から再生を開始する。
		/// </summary>
		public void PlayStart()
		{
			this.PlayStart(true);
		}

		/// <summary>
		/// 再生を開始する。
		/// </summary>
		/// <param name="isFirst">楽譜の最初から開始する場合はtrue、現在の位置から再生する場合はfalse。</param>
		public void PlayStart(bool isFirst)
		{
			// 再生中か、楽譜上に動物が1匹もいない場合は無効とする
			if (this.IsPlaying || this.AnimalScore.AnimalList.Count == 0) return;

			if (isFirst) this.NowPlace = 0;		// ページを最初に戻す
			this.AnimalScore.PlayOffset = 0;	// オフセットをクリア

			this.IsPlaying = true;				// 再生状態にする
		}


		/// <summary>
		/// 再生を停止する。
		/// </summary>
		public void PlayStop()
		{
			// 全ての動物を可視状態にする
			foreach (Animal animal in this.AnimalScore.AnimalList)
			{
				animal.Visible = true;
			}

			this.NowPlace = this.DefaultPlace;	// 再生前のページを復帰させる
			this.IsPlaying = false;				// 非再生状態にする
		}

		#endregion


		#region スクロール

		/// <summary>
		/// 楽譜を左へスクロールする。
		/// </summary>
		public void LeftScroll()
		{
			this.Scroll(this.NowPlace - Settings.System.Default.CompositionMeterNum * 4);
		}


		/// <summary>
		/// 楽譜を右へスクロールする。
		/// </summary>
		public void RightScroll()
		{
			this.Scroll(this.NowPlace + Settings.System.Default.CompositionMeterNum * 4);
		}


		/// <summary>
		/// 楽譜を任意の位置へスクロールする。
		/// </summary>
		/// <param name="percent">スクロールバーの位置パーセンテージ。</param>
		public void Scroll(float percent)
		{
			this.Scroll(((int)((percent / 100 * (Manager.ConfigurationManager.Current.CompositionMaxLine - 3)) + 0.5)) * Settings.System.Default.CompositionMeterNum * 4);
		}


		/// <summary>
		/// 楽譜を任意の位置へスクロールする。
		/// </summary>
		/// <param name="newPlace">楽譜の位置。</param>
		public void Scroll(int newPlace)
		{
			int limit = 
				Manager.ConfigurationManager.Current.CompositionMaxLine * 
				(4 * Settings.System.Default.CompositionMeterNum) - 
				(4 * Settings.System.Default.CompositionMeterNum) * 3;

			if (newPlace >= limit)
			{
				newPlace = limit;
			}
			else if (newPlace <= 0)
			{
				newPlace = 0;
			}

			this.DefaultPlace = this.NowPlace = newPlace;
			this.ScrollBar.PercentLocation = newPlace / (float)limit * 100.0F;		// スクロールバーの位置を計算

			this.SettingEnabledScrollButtons(true);
		}


		/// <summary>
		/// 現在のスクロールバーの位置に応じ、スクロールボタンの有効/無効を切り替える。
		/// </summary>
		public void SettingEnabledScrollButtons(bool enabled)
		{
			if (!enabled)
			{
				this.LeftScrollButton.Enabled = false;
				this.RightScrollButton.Enabled = false;
			}
			else if (this.ScrollBar.PercentLocation <= 0)
			{
				this.LeftScrollButton.Enabled = false;
				this.RightScrollButton.Enabled = true;
			}
			else if (this.ScrollBar.PercentLocation >= 100)
			{
				this.LeftScrollButton.Enabled = true;
				this.RightScrollButton.Enabled = false;
			}
			else
			{
				this.LeftScrollButton.Enabled = true;
				this.RightScrollButton.Enabled = true;
			}
		}

		#endregion

	}
}
