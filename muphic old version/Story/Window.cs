using System;
using muphic.Story.WindowParts;

namespace muphic.Story
{
	public enum StoryWindowMode{Title, Thumbnail};					//�����炭�\������̂̓T���l�C�����������ǁc
	/// <summary>
	/// Window �̊T�v�̐����ł��B
	/// </summary>
	public class Window : Screen
	{
		public StoryScreen parent;
		public StoryWindowMode windowmode;
		public Title title;
		public Thumbnail thumbnail;
		public Window(StoryScreen story, int num)
		{
			parent = story;
			this.windowmode = StoryWindowMode.Thumbnail;
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			title = new Title(this);
			thumbnail = new Thumbnail(this, num);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(title.ToString(), 551, 45, "image\\one\\parts\\title.png");
			muphic.DrawManager.Regist(thumbnail.ToString(), 760, 10, "image\\one\\parts\\story_window.png");//537,12

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			//BaseList.Add(title);					//�{����title��Screen�^�ł����Ăق���(���̓s����)���A�߂ǂ��̂ŁA���̂܂�
		}

		public override void Click(System.Drawing.Point p)
		{
			switch(this.windowmode)
			{
				case muphic.Story.StoryWindowMode.Title:
					title.Click(p);
					break;
				case muphic.Story.StoryWindowMode.Thumbnail:
					thumbnail.Click(p);
					break;
				default:
					base.Click (p);
					break;
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			switch(this.windowmode)
			{
				case muphic.Story.StoryWindowMode.Title:
					//title.MouseMove(p);
					base.MouseMove(p);
					break;
				case muphic.Story.StoryWindowMode.Thumbnail:
					thumbnail.MouseMove(p);
					break;
				default:
					base.MouseMove (p);
					break;
			}
		}

		public override void Draw()
		{
			switch(this.windowmode)
			{
				case muphic.Story.StoryWindowMode.Title:
					//title.Draw();
					base.Draw();
					break;
				case muphic.Story.StoryWindowMode.Thumbnail:
					thumbnail.Draw();
					break;
				default:
					base.Draw ();
					break;
			}
		}
	}
}
