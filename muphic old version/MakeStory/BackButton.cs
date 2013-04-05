using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// BackButton の概要の説明です。
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
			// Client動作でシフトキーフラグが立ってなかったら戻れません
			if (muphic.Common.CommonSettings.getClientMode() && !this.parent.parent.parent.getIsShiftKey())
			{
				System.Console.WriteLine("Client動作のため、戻るボタンはブロックされます（誤操作によるデータ喪失を防ぐため）.");
				System.Console.WriteLine("Shiftキーを押した後のみ戻るボタンは有効になります.");
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
