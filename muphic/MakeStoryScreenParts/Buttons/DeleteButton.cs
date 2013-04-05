
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"もどす"ボタンクラス。
	/// <para>クリックされると、スタンプを削除する追尾状態へ移行する。</para>
	/// </summary>
	public class DeleteButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 物語作成画面の"もどす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public DeleteButton(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			//this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_DeleteBtn, "IMAGE_BUTTON_BOX2_YELLOW", "IMAGE_BUTTON_BOX2_ON");
			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_DeleteBtn, "IMAGE_BUTTON_BOX2_YELLOW", "IMAGE_BUTTON_BOX2_ON", "IMAGE_BUTTON_BOX2_ORANGE");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_DeleteBtn, "IMAGE_MAKESTORYSCR_DELETEBTN");
		}


		/// <summary>
		/// 物語作成画面の"もどす"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.StampHoming.DeleteTarget.HasValue)
			{
				// 既に追尾状態が削除モードであれば、削除モードを解除する
				this.Parent.StampHoming.HomingTarget = null;
			}
			else
			{
				// 未指定のアイテムを作成し、削除追尾用のテクスチャ名を設定
				this.Parent.StampHoming.HomingTarget = new MakeStoryMainParts.Item(MakeStoryMainParts.ItemKind.None);
				this.Parent.StampHoming.HomingTarget.StampImageName = "IMAGE_MAKESTORYSCR_STMP_DELETE";

				// 追尾状態を削除モードにする
				this.Parent.StampHoming.DeleteTarget = -1;

				// ボタン領域での一時的な追尾を有効にする
				this.Parent.StampSelectArea.EnabledHoming = true;

				// スタンプ選択ボタンの押下状態を解除する。
				this.Parent.StampSelectArea.AllUnPressWithoutBackground();
			}

		}

	}
}
