using System;
using muphic.MakeStory.Dialog;

namespace muphic.MakeStory
{
	/// <summary>
	/// StoryMakeDialog �̊T�v�̐����ł��B
	/// </summary>
	public class StoryMakeDialog : Screen
	{
		public MakeStoryScreen parent;
		public NoButton no;
		public YesButton yes;

		public StoryMakeDialog(MakeStoryScreen story)
		{
			parent = story;

			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			no = new NoButton(this);
			yes = new YesButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 239, 287, @"image\MakeStory\dialog\newmake\haikei.png");
			muphic.DrawManager.Regist(no.ToString(), 575, 460, @"image\MakeStory\dialog\no_off.png", @"image\MakeStory\dialog\no_on.png");
			muphic.DrawManager.Regist(yes.ToString(), 375, 460, @"image\MakeStory\dialog\yes_off.png", @"image\MakeStory\\dialog\yes_on.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(no);
			BaseList.Add(yes);
		}

		public override void Draw()
		{
			base.Draw ();
		}

	}
}
