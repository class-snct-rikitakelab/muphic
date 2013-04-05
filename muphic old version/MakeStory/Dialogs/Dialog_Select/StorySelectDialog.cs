using System;
using muphic.MakeStory.Dialog;

namespace muphic.MakeStory
{
	/// <summary>
	/// StorySelectDialog �̊T�v�̐����ł��B
	/// </summary>
	public class StorySelectDialog : Screen
	{
		public MakeStoryScreen parent;
		public SelectButtons sbs;
		public UpperScrollButton upper;
		public LowerScrollButton lower;
		public muphic.MakeStory.Dialog.BackButton back;

		public StorySelectDialog(MakeStoryScreen story)
		{
			parent = story;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			sbs = new SelectButtons(this);
			upper = new UpperScrollButton(this);
			lower = new LowerScrollButton(this);
			back = new muphic.MakeStory.Dialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, @"image\MakeStory\dialog\select\haikei.png");
			muphic.DrawManager.Regist(sbs.ToString(), 285, 358, @"image\MakeStory\dialog\select\dialog_select.png");
			muphic.DrawManager.Regist(upper.ToString(), 640, 388, @"image\MakeStory\dialog\select\scroll_up.png");
			muphic.DrawManager.Regist(lower.ToString(), 642, 470, @"image\MakeStory\dialog\select\scroll_down.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 435, @"image\MakeStory\dialog\select\back_off.png", @"image\MakeStory\dialog\select\back_on.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(sbs);
			BaseList.Add(upper);
			BaseList.Add(lower);
			BaseList.Add(back);
		}
		
		public override void Draw()
		{
			base.Draw ();
		}

	}
}
