
namespace Muphic.CompositionScreenParts.Buttons.Animals
{
	/// <summary>
	/// 動物選択ボタン群 音声選択ボタンクラス
	/// </summary>
	public class VoiceButton : Common.Button
	{
		/// <summary>
		/// 親にあたる動物選択ボタン管理クラス
		/// </summary>
		public AnimalButtons Parent { get; private set; }


		/// <summary>
		/// 音声ボタンの有効性を表わすbool値。
		/// プロパティ"Enabled"を使用すること。
		/// </summary>
		private bool __enabled;

		/// <summary>
		/// 音声ボタンの有効性を表わすbool値。
		/// trueに設定してもシステム設定で音声ボタンがOFFの場合はfalseにしかならないので注意。
		/// </summary>
		public new bool Enabled
		{
			get
			{
				return this.__enabled;
			}
			set
			{
				if (value && Manager.ConfigurationManager.Current.EnabledVoice)
				{
					this.__enabled = true;
					base.Enabled = true;
				}
				else
				{
					this.__enabled = false;
					base.Enabled = false;
				}
			}
		}

		/// <summary>
		/// 音声選択ボタンのインスタンス化を行う。
		/// </summary>
		/// <param name="animalButtons">親にあたる動物選択ボタン管理クラス。</param>
		public VoiceButton(AnimalButtons animalButtons)
		{
			this.Parent = animalButtons;

			System.Drawing.Point location = Locations.AnimalButton7;
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON", "IMAGE_BUTTON_BOX1_ORANGE");
			this.SetLabelTexture(this.ToString(), location, "IMAGE_COMPOSITIONSCR_ANIMALBTN_VOICE");

			this.Enabled = Manager.ConfigurationManager.Current.EnabledVoice;
		}


		/// <summary>
		/// 声選択ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.NowAnimalButtonMode == AnimalButtonMode.Voice)
			{
				// 既に声ボタンが選択されている状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.None;	// 何も選択していない状態にする
				this.Pressed = false;										// ボタンを通常状態にする
			}
			else
			{
				// 声ボタンが選択されていない状態であれば

				this.Parent.NowAnimalButtonMode = AnimalButtonMode.Voice;	// 声を選択した状態にする
				this.Pressed = true;										// ボタンをON状態にする
			}
		}
	}
}
