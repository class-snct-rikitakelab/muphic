using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// BackButton �̊T�v�̐����ł��B
	/// </summary>
	public class BackButton : Base
	{
		MakeStoryScreen parent;
		public BackButton(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public override void Click(System.Drawing.Point p)
		{
			// Client����ŃV�t�g�L�[�t���O�������ĂȂ�������߂�܂���
			if (muphic.Common.CommonSettings.getClientMode() && !this.parent.parent.parent.getIsShiftKey())
			{
				System.Console.WriteLine("Client����̂��߁A�߂�{�^���̓u���b�N����܂��i�둀��ɂ��f�[�^�r����h�����߁j.");
				System.Console.WriteLine("Shift�L�[����������̂ݖ߂�{�^���͗L���ɂȂ�܂�.");
				return;
			}
			
			base.Click (p);
			//parent.parent.IsMakeStory = false;
			//parent.parent.StoryScreenMode = muphic.StoryScreenMode.StoryScreen;
            parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;

			System.IO.File.Delete("AutoSaveData\\STORY_AUTOSAVE.txt");
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
