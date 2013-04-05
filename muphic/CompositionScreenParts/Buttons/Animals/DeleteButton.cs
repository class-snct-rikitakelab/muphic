
namespace Muphic.CompositionScreenParts.Buttons.Animals
{
	/// <summary>
	/// 動物選択ボタン群 削除選択ボタンクラス
	/// </summary>
	public class DeleteButton : Common.Button
	{
		/// <summary>
		/// 親にあたる動物選択ボタン管理クラス
		/// </summary>
		public AnimalButtons Parent { get; private set; }

		/// <summary>
		/// 削除選択ボタンのインスタンス化を行う。
		/// </summary>
		/// <param name="animalButtons">親にあたる動物選択ボタン管理クラス。</param>
		public DeleteButton(AnimalButtons animalButtons)
		{
			this.Parent = animalButtons;

			System.Drawing.Point location = Locations.AnimalButton8;
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON", "IMAGE_BUTTON_BOX1_ORANGE");
			this.SetLabelTexture(this.ToString(), location, "IMAGE_COMPOSITIONSCR_ANIMALBTN_DELETE");
		}


		/// <summary>
		/// 削除選択ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.NowAnimalButtonMode == AnimalButtonMode.Delete)
			{
				// 既に削除ボタンが選択されている状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.None;	// 何も選択していない状態にする
				this.Pressed = false;										// ボタンを通常状態にする
			}
			else
			{
				// 削除ボタンが選択されていない状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.Delete;	// 削除を選択した状態にする
				this.Pressed = true;										// ボタンをON状態にする
			}
		}


		/// <summary>
		/// 削除ボタン上にマウスポインタが入った時に呼ばれる。
		/// </summary>
		public override void MouseEnter()
		{
			base.MouseEnter();
		}

		/// <summary>
		/// 削除ボタン上からマウスポインタが出た時に呼ばれる。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();
		}
	}
}
