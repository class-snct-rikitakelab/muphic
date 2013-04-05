
namespace Muphic.CompositionScreenParts.Buttons.Animals
{
	/// <summary>
	/// 動物選択ボタン群 ヒツジ選択ボタンクラス
	/// </summary>
	public class SheepButton : Common.Button
	{
		/// <summary>
		/// 親にあたる動物選択ボタン管理クラス
		/// </summary>
		public AnimalButtons Parent { get; private set; }

		/// <summary>
		/// ヒツジ選択ボタンのインスタンス化を行う。
		/// </summary>
		/// <param name="animalButtons">親にあたる動物選択ボタン管理クラス。</param>
		public SheepButton(AnimalButtons animalButtons)
		{
			this.Parent = animalButtons;

			System.Drawing.Point location = Locations.AnimalButton1;
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON", "IMAGE_BUTTON_BOX1_ORANGE");
			this.SetLabelTexture(this.ToString(), location, "IMAGE_COMPOSITIONSCR_ANIMALBTN_SHEEP");
		}

		/// <summary>
		/// ヒツジ選択ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.NowAnimalButtonMode == AnimalButtonMode.Sheep)
			{
				// 既にヒツジボタンが選択されている状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.None;	// 何も選択していない状態にする
				this.Pressed = false;										// ボタンを通常状態にする
			}
			else
			{
				// ヒツジボタンが選択されていない状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.Sheep;	// ヒツジを選択した状態にする
				this.Pressed = true;										// ボタンをON状態にする
			}
		}

		
		/// <summary>
		/// ヒツジボタン上にマウスポインタが入った時に呼ばれる。
		/// </summary>
		public override void MouseEnter()
		{
			base.MouseEnter();
		}

		/// <summary>
		/// ヒツジボタン上からマウスポインタが出た時に呼ばれる。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();
		}
	}
}
