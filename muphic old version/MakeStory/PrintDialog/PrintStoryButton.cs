using System;

namespace muphic.MakeStory.PrintDialog
{
	/// <summary>
	/// PrintStoryButton �̊T�v�̐����ł��B
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
			
			// �b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b
			// �b�����A�����ɏ������܂��I������\�b�h�̌Ăяo�����I�b
			// ������������������������������������������������������
			parent.parent.wind.PrintStory();
			// ������������������������������������������������������
			// �b�����A�����ɏ������܂��I������\�b�h�̌Ăяo�����I�b
			// �b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b�b
			
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