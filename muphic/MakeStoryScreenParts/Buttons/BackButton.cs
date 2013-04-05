
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"はじめにもどる"ボタンクラス。
	/// <para>クリックされると、トップ画面へ遷移する。</para>
	/// </summary>
	public class BackButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 物語作成画面の"はじめにもどる"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public BackButton(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_BackBtn, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_BackBtn, "IMAGE_MAKESTORYSCR_BACKBTN");
		}


		/// <summary>
		/// 物語作成画面の"はじめにもどる"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (Manager.KeyboardInputManager.IsProtection) return;

			if (this.Parent.IsChanged)
			{
				// 最後に物語が保存されてから変更が加えられていれば、確認ダイアログ表示
				Tools.DebugTools.ConsolOutputMessage("BackButton -Click", "ものがたりおんがく終了確認ダイアログ表示", true);
				this.Parent.ScreenMode = MakeStoryScreenMode.BackDialog;
			}
			else
			{
				// 最後に物語が保存されてから変更が加えられていなければ、ダイアログを表示せずそのまま戻る。
				this.Parent.Parent.ScreenMode = TopScreenMode.TopScreen;
			}
		}

	}
}
