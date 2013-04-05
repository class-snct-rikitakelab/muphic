using System.Drawing;

using Muphic.Manager;

namespace Muphic.CompositionScreenParts.CompositionMainParts
{
	/// <summary>
	/// 作曲画面のスクロールバークラス。
	/// </summary>
	public class ScrollBar : Common.Screen
	{
		/// <summary>
		/// 親にあたる作曲部
		/// </summary>
		public CompositionMain Parent { get; set; }


		#region スクロールバーとボタンの有効/無効に関するプロパティ

		/// <summary>
		/// スクロールバーの有効性を表わすbool値。
		/// プロパティ"Enabled"を使用すること。
		/// </summary>
		private bool __enabled;

		/// <summary>
		/// スクロールバーの有効性を表わすbool値。
		/// スクロールボタンも連動する。
		/// </summary>
		public bool Enabled
		{
			get
			{
				return this.__enabled;
			}
			set
			{
				this.__enabled = value;
				this.Parent.SettingEnabledScrollButtons(value);					// スクロールボタンの有効/無効の切り替え

				if (value)
				{																// スクロールバーが有効化された場合
					this.Alpha = Settings.System.Default.AlphaBlending_PartsEnabled;		// 透過度を通常状態にする
				}
				else
				{																// スクロールバーが無効化された場合
					this.Alpha = Settings.System.Default.AlphaBlending_PartsDisabled;	// 透過度を落とす
				}
			}
		}

		/// <summary>
		/// スクロールバー描画時の透過度を表わすbyte値。
		/// </summary>
		private byte Alpha { get; set; }

		#endregion


		#region スクロールバーの位置やサイズに関するプロパティ

		/// <summary>
		/// スクロールバーが最も左に位置する場合の左端x座標を表わす。
		/// </summary>
		public int LeftLocation { get; private set; }

		/// <summary>
		/// スクロールバーが最も右に位置する場合の左端x座標表す。
		/// </summary>
		public int RightLocation { get; private set; }

		/// <summary>
		/// スクロールバーの左上y座標を表わす。
		/// </summary>
		public int TopLocation { get; private set; }

		/// <summary>
		/// スクロールバーのサイズを表わす。
		/// </summary>
		public Size ScrollBarSize { get; private set; }

		/// <summary>
		/// 現在のスクロールバーの矩形を表わす。
		/// </summary>
		private Rectangle NowScrollBar { get; set; }


		/// <summary>
		/// スクロールバーが何％の位置にあるかを表す。
		/// プロパティ"PercentLocation"を使用すること。
		/// </summary>
		private float __percentLocation;

		/// <summary>
		/// スクロールバーが何％の位置にあるかを表す。
		/// </summary>
		public float PercentLocation
		{
			get
			{
				return this.__percentLocation;
			}
			set
			{
				this.__percentLocation = value;

				// スクロールバー矩形を更新
				this.NowScrollBar = new Rectangle(new Point(this.PercentToPoint(value), this.TopLocation), this.ScrollBarSize);
			}
		}


		#endregion


		/// <summary>
		/// スクロールバーがドラッグ中であるかを示すフラグ。
		/// trueならドラッグ処理、falseならクリック処理が実行される。
		/// </summary>
		private bool IsDrag { get; set; }

		/// <summary>
		/// ドラッグ中のマウス位置とスクロールバーの左端との差を表わす。
		/// </summary>
		private int Difference { get; set; }


		/// <summary>
		/// スクロールバーのインスタンス化を行う。
		/// </summary>
		/// <param name="compositionMain">親にあたる作曲部。</param>
		public ScrollBar(CompositionMain compositionMain)
		{
			this.Parent = compositionMain;

			// スクロール領域の登録
			DrawManager.Regist(this.ToString(), Locations.ScrollArea.Location, "IMAGE_COMPOSITIONSCR_SCROLLAREA");

			// スクロールバーテクスチャの登録
			DrawManager.Regist("ScrollBar", 0, TopLocation, "IMAGE_COMPOSITIONSCR_SCROLLBAR");

			// ドラッグフラグを降ろしておく
			this.IsDrag = false;

			this.ScrollBarSize = TextureFileManager.GetRectangle("IMAGE_COMPOSITIONSCR_SCROLLBAR").Size;
			this.TopLocation = Locations.ScrollArea.Top;
			this.LeftLocation = Locations.ScrollArea.Left;
			this.RightLocation = Locations.ScrollArea.Right - this.ScrollBarSize.Width;
			this.PercentLocation = 0;
		}


		/// <summary>
		/// スクロールバーの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			DrawManager.Draw("ScrollBar", this.NowScrollBar.Location, this.Alpha);
		}


		/// <summary>
		/// スクロールバー上でマウスが移動した際に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">。</param>
		public override void MouseMove(MouseStatusArgs mouseStatus)
		{
		    if (!this.Enabled) return;

			base.MouseMove(mouseStatus);

			if (mouseStatus.IsDrag && this.IsDrag)
			{
				// スクロールバーのドラッグ中のみ以下を実行

				this.PercentLocation = this.PointToPercent(mouseStatus.X - this.Difference);		// 現在のスクロールバー座標から％位置を算出
				this.Parent.Scroll(PercentLocation);												// スクロールバーの位置をマウスの中央に移動
			}
		}


		/// <summary>
		/// スクロールバーのドラッグが開始された時に呼び出される。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragBegin(MouseStatusArgs mouseStatus)
		{
			if (!this.Enabled) return;

			base.DragBegin(mouseStatus);

			if (this.NowScrollBar.Contains(mouseStatus.BeginLocation))
			{
				// マウスポインタがスクロールバー内に入っている場合、ドラッグ処理を行う

				this.IsDrag = true;															// ドラッグのフラグを立てる
				this.Difference = mouseStatus.BeginLocation.X - this.NowScrollBar.Left;		// マウス座標とスクロールバー左端の差を求める
			}
		}


		/// <summary>
		/// スクロールバーのドラッグが終了したときに呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragEnd(MouseStatusArgs mouseStatus)
		{
			base.DragEnd(mouseStatus);

			this.IsDrag = false;	// ドラッグのフラグを降ろす
		}


		/// <summary>
		/// スクロールバーがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			if (!this.Enabled) return;

			base.Click(mouseStatus);

			if (!this.IsDrag && !this.NowScrollBar.Contains(mouseStatus.NowLocation))
			{
				float percent = this.PointToPercent(mouseStatus.X - ScrollBarSize.Width / 2);		// 
				if (this.PercentLocation > percent) this.Parent.LeftScroll();						// マウスポインタがスクロールバーより左にあれば、左にスクロール
				else this.Parent.RightScroll();														// マウスポインタがスクロールバーより右にあれば、右にスクロール
			}
		}



		/// <summary>
		/// スクロールバーの実際の座標を％単位での位置に変換する。
		/// </summary>
		/// <param name="point">スクロールバーの左上x座標。</param>
		/// <returns>スクロールバーの位置(％表記)。</returns>
		private float PointToPercent(int point)
		{
			return (float)(point - this.LeftLocation) / (this.RightLocation - this.LeftLocation) * 100.0F;
		}

		/// <summary>
		/// スクロールバーの％単位での位置を実際の座標に変換する。
		/// </summary>
		/// <param name="percent">スクロールバーの位置(％表記)。</param>
		/// <returns>スクロールバーの左上x座標。</returns>
		private int PercentToPoint(float percent)
		{
			return this.LeftLocation + (int)((this.RightLocation - this.LeftLocation) * percent / 100.0F);
		}
	}
}
