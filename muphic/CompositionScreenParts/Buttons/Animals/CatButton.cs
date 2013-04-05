
namespace Muphic.CompositionScreenParts.Buttons.Animals
{
	/// <summary>
	/// 動物選択ボタン群 ネコ選択ボタンクラス
	/// </summary>
	public class CatButton : Common.Button
	{
		/// <summary>
		/// 親にあたる動物選択ボタン管理クラス
		/// </summary>
		public AnimalButtons Parent { get; private set; }

		/// <summary>
		/// ネコ選択ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="animalButtons">親にあたる動物選択ボタン管理クラス。</param>
		public CatButton(AnimalButtons animalButtons)
		{
			this.Parent = animalButtons;

			System.Drawing.Point location = Locations.AnimalButton6;
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON", "IMAGE_BUTTON_BOX1_ORANGE");
			this.SetLabelTexture(this.ToString(), location, "IMAGE_COMPOSITIONSCR_ANIMALBTN_CAT");
		}


		/// <summary>
		/// ネコ選択ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.NowAnimalButtonMode == AnimalButtonMode.Cat)
			{
				// 既にネコボタンが選択されている状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.None;	// 何も選択していない状態にする
				this.Pressed = false;										// ボタンを通常状態にする
			}
			else
			{
				// ネコボタンが選択されていない状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.Cat;		// ネコを選択した状態にする
				this.Pressed = true;										// ボタンをON状態にする
			}
		}
	}
}
