using System;
using System.IO;

namespace muphic.Tutorial.TutorialStartParts
{
	/// <summary>
	/// StartButton の概要の説明です。
	/// </summary>
	public class ContinueButton : Base
	{
		public TutorialStart parent;
		
		public ContinueButton(TutorialStart ts)
		{
			this.parent = ts;
			
			// セーブデータが存在しなかったら半透明表示 押せなくする
			string savefile = TutorialScreen.TutorialPass + TutorialMain.SaveFileDirectory + "\\" + TutorialMain.SaveFileName;
			//if( !File.Exists(savefile) )
			if( muphic.Common.TutorialTools.ReadSaveFile(savefile, false) < 1 )
			{
				this.State = 2;
			}
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(this.State == 2) return;
			
			// チュートリアル画面 チュートリアルの開始
			// 続きからスタート
			parent.parnet.StartTutorial(true);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			if(this.State == 0) this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(this.State == 1) this.State = 0;
		}
	}
}