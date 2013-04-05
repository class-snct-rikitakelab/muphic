
namespace Muphic.CompositionScreenParts.Buttons.Animals
{
	/// <summary>
	/// 動物選択ボタン群 鳥選択ボタンクラス
	/// </summary>
	public class BirdButton : Common.Button
	{
		/// <summary>
		/// 親にあたる動物選択ボタン管理クラス
		/// </summary>
		public AnimalButtons Parent { get; private set; }

		/// <summary>
		/// 鳥選択ボタンのインスタンス化を行う。
		/// </summary>
		/// <param name="animalButtons">親にあたる動物選択ボタン管理クラス。</param>
		public BirdButton(AnimalButtons animalButtons)
		{
			this.Parent = animalButtons;

			System.Drawing.Point location = Locations.AnimalButton3;
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON", "IMAGE_BUTTON_BOX1_ORANGE");
			this.SetLabelTexture(this.ToString(), location, "IMAGE_COMPOSITIONSCR_ANIMALBTN_BIRD");
		}


		/// <summary>
		/// 鳥選択ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.NowAnimalButtonMode == AnimalButtonMode.Bird)
			{
				// 既に鳥ボタンが選択されている状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.None;	// 何も選択していない状態にする
				this.Pressed = false;										// ボタンを通常状態にする
			}
			else
			{
				// 鳥ボタンが選択されていない状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.Bird;	// 鳥を選択した状態にする
				this.Pressed = true;										// ボタンをON状態にする
			}
		}


		/// <summary>
		/// 鳥ボタン上にマウスポインタが入った時に呼ばれる。
		/// </summary>
		public override void MouseEnter()
		{
			base.MouseEnter();
		}

		/// <summary>
		/// 鳥ボタン上からマウスポインタが出た時に呼ばれる。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();
		}
	}
}
