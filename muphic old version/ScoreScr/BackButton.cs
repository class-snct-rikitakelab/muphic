using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// 楽譜画面の戻るボタン
	/// 楽譜画面から呼び出し元の画面（ひとりで音楽・つなげて音楽・ものがたり音楽）に戻る際に使用
	/// </summary>
	public class BackButton : Base
	{
        public ScoreScreen parent;

		public BackButton(ScoreScreen score)
		{
			this.parent = score;
		}

		/// <summary>
		/// クリック時の処理
		/// 楽譜画面を呼び出した画面に戻る
		/// </summary>
		/// <param name="p"></param>
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			// テクスチャの開放
			//this.parent.delete.DeleteAllScoreTexture();

			// どの画面から呼び出されたかによって処理が変わる
			switch(parent.ParentScreenMode)
			{
				case ScreenMode.OneScreen:
					// ひとりで音楽モードから楽譜画面が呼び出されていた場合、戻るボタンを押すとひとりで音楽モードに戻る
					((OneScreen)this.parent.parent).OneScreenMode = OneScreenMode.OneScreen;
					break;
				case ScreenMode.LinkScreen:
					// つなげて音楽モードから楽譜画面が呼び出されていた場合、戻るボタンを押すとつなげて音楽モードに戻る
					((LinkScreen)this.parent.parent).LinkScreenMode = LinkScreenMode.LinkScreen;
					break;
				case ScreenMode.LinkMakeScreen:
					// つなげて音楽モードから楽譜画面が呼び出されていた場合、戻るボタンを押すとつなげて音楽モードに戻る
					((LinkMakeScreen)this.parent.parent).LinkMakeScreenMode = LinkMakeScreenMode.LinkMakeScreen;
					break;
				case ScreenMode.StoryScreen:
					// ものがたり音楽モードから楽譜画面が呼び出されていた場合、戻るボタンを押すとものがたり音楽モードに戻る
					((StoryScreen)parent.parent).StoryScreenMode = StoryScreenMode.StoryScreen;
					break;
				default:
					// このコードが実行されることはありえません。実行されるようなことがあれば世界が終わります。あしからず。
					System.Console.WriteLine("Sekai no owari. Fatal error.");
					Environment.Exit(52);
					break;
			}

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
