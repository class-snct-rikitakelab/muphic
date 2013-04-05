using System.Drawing;

using Muphic.MakeStoryScreenParts.MakeStoryMainParts;
using Muphic.Manager;

namespace Muphic.MakeStoryScreenParts
{
	/// <summary>
	/// 物語作成画面で、追尾しているスタンプの状態を表す識別子を指定する。
	/// </summary>
	public enum HomingMode
	{
		/// <summary>
		/// 何も追尾していない状態。
		/// </summary>
		Empty,

		/// <summary>
		/// 絵に配置可能な位置でスタンプが追尾している状態。
		/// </summary>
		StampEnabled,

		/// <summary>
		/// 絵に配置できない位置でスタンプが追尾している状態。
		/// </summary>
		StampDisabled,

		/// <summary>
		/// 絵の領域内で削除スタンプが追尾している状態。
		/// </summary>
		DeleteEnabled,

		/// <summary>
		/// 絵の領域外で削除スタンプが追尾している状態。
		/// </summary>
		DeleteDisabled,
	}



	/// <summary>
	/// ものがたりおんがく画面でのスタンプの動きを管理する追尾クラス（ver.7 以前は Tsuibi）。
	/// </summary>
	/// <remarks>
	/// 追尾時のスタンプのデータ (HomingTarget プロパティ) と、マウスポインタへの追尾の有無 (Visible プロパティ) の管理を行う。
	/// <para>追尾状態の場合 (マウスポインタが絵の中にある場合)、このクラスの親クラス (Screen 型) は、HomingTarget プロパティで指定されたスタンプを指定位置に描画する。</para>
	/// <para>また、スタンプボタンが押された直後は絵の外 (スタンプ選択領域) でも追尾しなければならない。このクラスのインスタンス生成時に、絵以外で追尾を許可する領域 (__homingRectangle フィールド) の算出も行う。</para>
	/// </remarks>
	public class StampHoming
	{
		/// <summary>
		///  親にあたる物語作成画面クラス。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 追尾するスタンプを保持する。
		/// <para>HomingTarget プロパティを使用すること。</para>
		/// </summary>
		private Stamp __homingTarget;

		/// <summary>
		/// 追尾するスタンプを取得または設定する。
		/// <para></para>
		/// </summary>
		public Stamp HomingTarget
		{
			get
			{
				return this.__homingTarget;
			}
			set
			{
				this.__homingTarget = value;

				// 追尾対象がnullでなければスタンプ追尾状態にし、
				if (value != null) this.HomingMode = HomingMode.StampEnabled;

				// 追尾対象がnullであればスタンプ追尾を無効化する。
				else this.HomingMode = HomingMode.Empty;

				// 追尾するスタンプが設定されたら、削除モードを無効化。
				this.DeleteTarget = null;
			}
		}


		/// <summary>
		/// 絵以外で追尾が有効となる領域を保持する。
		/// </summary>
		private readonly Rectangle __homingRectangle;

		/// <summary>
		/// 絵以外で追尾が有効となる領域を取得する。
		/// </summary>
		public Rectangle HomingRectangle
		{
			get
			{
				return this.__homingRectangle;
			}
		}


		/// <summary>
		/// 追尾の状態を取得または設定する。
		/// </summary>
		public HomingMode HomingMode { get; set; }


		/// <summary>
		/// 追尾対象となる、現在編集中のスライドの StampList 内の番号。
		/// <para>追尾状態が削除モードでなければ、このフィールドの値は null となる。</para>
		/// <para>DeleteTarget プロパティを使用すること。</para>
		/// </summary>
		private int? __deleteTarget;

		/// <summary>
		/// 削除対象となる現在編集中のスライドの StampList 内の番号を取得する。
		/// <para>追尾状態が削除モードでなければ、このプロパティの値は null となる。</para>
		/// </summary>
		public int? DeleteTarget
		{
			get
			{
				return this.__deleteTarget;
			}
			set
			{
				this.__deleteTarget = value;

				// 削除モードであれば"もどす"ボタンを押下状態にし、
				// 削除モードが解除されれば"もどす"ボタンの押下状態も解除する
				this.Parent.DeleteButton.Pressed = (value != null);
			}
		}


		/// <summary>
		/// 追尾クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面クラス。</param>
		public StampHoming(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			// 追尾有効領域の算出
			Rectangle picrureRectangle = Settings.PartsLocation.Default.MakeStoryScr_PictureArea;		// 絵の領域
			Rectangle ssareaRectangle = RectangleManager.Get(this.Parent.StampSelectArea.ToString());	// スタンプ選択ボタン群の領域
			this.__homingRectangle = new Rectangle(					// 絵の外で追尾が有効な領域を設定
				ssareaRectangle.Left,								// 左上x座標はスタンプ選択ボタン群の領域の左端
				picrureRectangle.Top,								// 左上y座標は絵の上端
				picrureRectangle.Right - ssareaRectangle.Left,		// 横幅は絵の右端とスタンプ選択ボタン群の領域の左端との差
				ssareaRectangle.Bottom - picrureRectangle.Top		// 縦幅はスタンプ選択ボタン群の領域の下端と絵の上端との差
			);
		}


		/// <summary>
		/// マウスポインタの位置によって、追尾対象をどのように描画するかを決定する。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態。</param>
		public void MouseMove(MouseStatusArgs mouseStatus)
		{
			if (this.DeleteTarget.HasValue && this.HomingTarget != null)
			{
				// 追尾状態が削除モードだった場合

				if (Settings.PartsLocation.Default.MakeStoryScr_PictureArea.Contains(mouseStatus.NowLocation))
				{
					// マウス位置が絵の中だった場合
					this.HomingMode = HomingMode.DeleteEnabled;								// 削除追尾有効化
					this.HomingTarget.Location = mouseStatus.NowLocation;					// 追尾対象の位置をマウス位置にセット
					this.Parent.StampSelectArea.EnabledHoming = false;						// マウスが絵の中に入ったら、ボタン領域の追尾は無効化する
					this.DeleteTarget = this.UpdateDeleteTarget(mouseStatus.NowLocation);	// 現在のマウス位置で削除できるスタンプがあるかをチェック
				}
				else if (this.Parent.StampSelectArea.EnabledHoming && (this.HomingRectangle.Contains(mouseStatus.NowLocation)))
				{
					// "もどす"ボタンが押された直後で(選択されてから一度も絵の中にマウスが入っていない状態で)、追尾有効領域内だった場合
					this.HomingMode = HomingMode.DeleteDisabled;							// 削除追尾半有効化
					this.HomingTarget.Location = mouseStatus.NowLocation;					// 追尾対象の位置をマウス位置にセット
				}
				else
				{
					// 上記以外の場合は追尾無効化
					this.HomingMode = HomingMode.Empty;
				}
			}
			else if (this.HomingTarget != null)
			{
				// 追尾対象が設定されている場合は以下を実行

				// 追尾対象の矩形（現在のマウス位置と追尾対象テクスチャサイズ）
				Rectangle target = new Rectangle(
					Tools.CommonTools.CenterToOnreft(mouseStatus.NowLocation, this.HomingTarget.Size),
					this.HomingTarget.Size
				);

				if (Settings.PartsLocation.Default.MakeStoryScr_PictureArea.Contains(target))
				{
					// 追尾中のテクスチャが絵の中だった場合
					this.HomingMode = HomingMode.StampEnabled;					// 追尾有効化
					this.HomingTarget.Location = mouseStatus.NowLocation;		// 追尾対象の位置をマウス位置にセット
					this.Parent.StampSelectArea.EnabledHoming = false;			// テクスチャが絵の中に入ったら、ボタン領域の追尾は無効化する
				}
				else if (this.Parent.StampSelectArea.EnabledHoming && (this.HomingRectangle.Contains(mouseStatus.NowLocation)))
				{
					// スタンプが選択された直後で(選択されてから一度も絵の中にマウスが入っていない状態で)、追尾有効領域内だった場合
					this.HomingMode = HomingMode.StampDisabled;					// 追尾半有効化
					this.HomingTarget.Location = mouseStatus.NowLocation;		// 追尾対象の位置をマウス位置にセット
				}
				else
				{
					// 上記以外の場合は追尾無効化
					this.HomingMode = HomingMode.Empty;
				}
			}
			else
			{
				// 追尾対象が設定されていない場合は追尾を無効にする
				this.HomingMode = HomingMode.Empty;
			}
		}


		/// <summary>
		/// 追尾対象を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態。</param>
		public void DrawTarget(DrawStatusArgs drawStatus)
		{
			// 現在の追尾状態に合わせて描画を行う
			switch (this.HomingMode)
			{
				case HomingMode.StampEnabled:		// 絵の領域内でスタンプが追尾している場合、追尾中のスタンプを描画し、カーソルをやや透過させる
					DrawManager.DrawCenter(this.HomingTarget.StampImageName, this.HomingTarget.Location);
					drawStatus.CursorAlpha = Settings.System.Default.AlphaBlending_CursorHoming;
					break;

				case HomingMode.StampDisabled:		// 絵の領域外でスタンプが追尾している場合、追尾中のスタンプをやや透過させて描画する
					DrawManager.DrawCenter(this.HomingTarget.StampImageName, this.HomingTarget.Location, (byte)200);
					break;

				case HomingMode.DeleteEnabled:		// 絵の領域内で削除スタンプが追尾している場合
					if (this.DeleteTarget == -1)
					{								// マウスがスタンプを削除できない位置にある場合、追尾中の削除スタンプを描画し、カーソルをやや透過させる
						DrawManager.DrawCenter(this.HomingTarget.StampImageName, this.HomingTarget.Location);
						drawStatus.CursorAlpha = Settings.System.Default.AlphaBlending_CursorHoming;
					}
					else
					{								// マウスがスタンプを削除できる位置にある場合、削除対象のスタンプの矩形と同形の線を描画する
						DrawManager.DrawLine(this.Parent.CurrentSlide.StampList[this.DeleteTarget.Value].RectangleCenter);
					}
					break;

				case HomingMode.DeleteDisabled:		// 絵の領域外で削除スタンプが追尾している場合、追尾中の削除スタンプをやや透過させて描画する
					DrawManager.DrawCenter(this.HomingTarget.StampImageName, this.HomingTarget.Location, (byte)200);
					break;

				default:
					break;
			}
		}


		/// <summary>
		/// 与えられた座標が、絵に配置されたスタンプのいずれかに含まれているかを調べる。
		/// <para>主に、削除モードでの追尾中、マウス座標に削除可能なスタンプがあるかを調べる際に使用する。</para>
		/// </summary>
		/// <param name="mouseLocation">マウス座標。</param>
		/// <returns>座標が配置されたスタンプに含まれる場合はそのスタンプの StampList 内の番号、それ以外は -1。</returns>
		public int UpdateDeleteTarget(Point mouseLocation)
		{
			for (int i = this.Parent.CurrentSlide.StampList.Count - 1; i >= 0; i--)
			{
				if (this.Parent.CurrentSlide.StampList[i].RectangleCenter.Contains(mouseLocation))
				{						// マウスがi番目のスタンプの矩形内に入っていた場合
					return i;			// i番目のスタンプを削除対象とする
				}
			}

			return -1;					// 上記で引っかからなければ、削除対象無し
		}


		/// <summary>
		/// 追尾状態をクリアする。
		/// </summary>
		public void ClearHoming()
		{
			this.HomingTarget = null;
		}

	}
}
