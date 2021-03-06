using System;

namespace muphic.MakeStory.PrintDialog
{
	/// <summary>
	/// PrintStoryButton の概要の説明です。
	/// </summary>
	public class PrintStoryButton : Base
	{
		public StoryPrintDialog parent;
		
		public PrintStoryButton(StoryPrintDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// ｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜
			// ｜さあ、ここに書きたまえ！印刷メソッドの呼び出しを！｜
			// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
			parent.parent.wind.PrintStory();
			// ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
			// ｜さあ、ここに書きたまえ！印刷メソッドの呼び出しを！｜
			// ｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜｜
			
			this.parent.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
