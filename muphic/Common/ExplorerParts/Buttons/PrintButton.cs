
namespace Muphic.Common.ExplorerParts.Buttons
{
	/// <summary>
	/// エクスプローラ上の "印刷" ボタン。
	/// </summary>
	public class PrintButton : Button
	{
		/// <summary>
		/// 親にあたるエクスプローラ。
		/// </summary>
		public Explorer Parent { get; private set; }

		/// <summary>
		/// エクスプローラ上の "印刷" ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるエクスプローラ。</param>
		public PrintButton(Explorer parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Settings.PrintButtonLocation, "IMAGE_BUTTON_BOX2_ORANGE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PrintButtonLocation, "IMAGE_EXPLORER_PRINTBTN");
		}

		/// <summary>
		///	エクスプローラ上の "印刷" ボタンをクリックする。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.TargetType == ExplorerTargetType.StoryData)
			{
				var targetData = (Muphic.PlayerWorks.StoryData)(this.Parent.WorkInfoArea.Target);
				Tools.MakeStoryTools.SetStampImageName(targetData);

				new Tools.Printer.StoryPrinter(targetData).PrintForStudent();

				// 印刷済みとしてリストに追加し、情報を更新
				this.Parent.FileSelectArea.AddPrintedFilePath();
				this.Parent.WorkInfoArea.Update();
			}
		}

		/// <summary>
		/// 現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String を返す。
		/// </summary>
		/// <returns>現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Parent.ParentName;
		}
	}
}
