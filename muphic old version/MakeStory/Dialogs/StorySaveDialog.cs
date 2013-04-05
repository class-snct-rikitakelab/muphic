using System;
using muphic.MakeStory.Dialog;

namespace muphic.MakeStory
{
	/// <summary>
	/// StorySaveDialog の概要の説明です。
	/// </summary>
	public class StorySaveDialog : Screen
	{
		public MakeStoryScreen parent;
		public NoButton no;
		public YesButton yes;
		public Dialog.BackButton back;
		//public BackButton back;

		public string DialogMsg;

		public StorySaveDialog(MakeStoryScreen story)
		{
			parent = story;

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			no = new NoButton(this);
			yes = new YesButton(this);
			back = new Dialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 239, 287, @"image\MakeStory\dialog\save\haikei.png");
			muphic.DrawManager.Regist(no.ToString(), 575, 460, @"image\MakeStory\dialog\no_off.png", @"image\MakeStory\dialog\no_on.png");
			muphic.DrawManager.Regist(yes.ToString(), 375, 460, @"image\MakeStory\dialog\yes_off.png", @"image\MakeStory\dialog\yes_on.png");
			muphic.DrawManager.Regist(back.ToString(), 475, 460, @"image\MakeStory\dialog\back_off.png", @"image\MakeStory\dialog\back_on.png");
			muphic.DrawManager.Regist("SaveWarning",  310, 392, @"image\MakeStory\dialog\save\warning.png" );
			muphic.DrawManager.Regist("SaveQuestion", 409-30, 389, @"image\MakeStory\dialog\save\question.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(no);
			BaseList.Add(yes);
			BaseList.Add(back);

			this.CheckName();
		}
		
		public void CheckName()
		{
			if(this.parent.PictureStory.Title == null || this.parent.PictureStory.Title == "")
			{
				// 曲名が空だったら、曲名を決めるよう促す
				this.DialogMsg = "SaveWarning";
				this.yes.Visible = this.no.Visible = false;
				this.back.Visible = true;
			}
			else
			{
				// 曲名があれば、それを保存するか問う
				this.DialogMsg = "SaveQuestion";
				this.yes.Visible = this.no.Visible = true;
				this.back.Visible = false;
			}
		}

		public override void Draw()
		{
			base.Draw ();
			DrawManager.Draw(this.DialogMsg);
		}

	}
}
