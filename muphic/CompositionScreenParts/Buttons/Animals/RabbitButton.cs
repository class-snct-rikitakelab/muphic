
namespace Muphic.CompositionScreenParts.Buttons.Animals
{
	/// <summary>
	/// 動物選択ボタン群 ウサギ選択ボタンクラス
	/// </summary>
	public class RabbitButton : Common.Button
	{
		/// <summary>
		/// 親にあたる動物選択ボタン管理クラス
		/// </summary>
		public AnimalButtons Parent { get; private set; }

		/// <summary>
		/// ウサギ選択ボタンのインスタンス化を行う。
		/// </summary>
		/// <param name="animalButtons">親にあたる動物選択ボタン管理クラス。</param>
		public RabbitButton(AnimalButtons animalButtons)
		{
			this.Parent = animalButtons;

			System.Drawing.Point location = Locations.AnimalButton2;
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON", "IMAGE_BUTTON_BOX1_ORANGE");
			this.SetLabelTexture(this.ToString(), location, "IMAGE_COMPOSITIONSCR_ANIMALBTN_RABBIT");
		}


		/// <summary>
		/// ウサギ選択ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.NowAnimalButtonMode == AnimalButtonMode.Rabbit)
			{
				// 既にウサギボタンが選択されている状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.None;	// 何も選択していない状態にする
				this.Pressed = false;										// ボタンを通常状態にする
			}
			else
			{
				// ウサギボタンが選択されていない状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.Rabbit;	// ウサギを選択した状態にする
				this.Pressed = true;										// ボタンをON状態にする
			}
		}


		/// <summary>
		/// ウサギボタン上にマウスポインタが入った時に呼ばれる。
		/// </summary>
		public override void MouseEnter()
		{
			base.MouseEnter();
		}

		/// <summary>
		/// ウサギボタン上からマウスポインタが出た時に呼ばれる。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();
		}
	}
}
