using System;

namespace muphic.Story
{
	/// <summary>
	/// Tempo �̊T�v�̐����ł��B
	/// </summary>
	public class Tempo : Screen
	{
		public StoryScreen parent;
		/// <summary>
		/// ���݂ǂ̃e���|�{�^����������Ă��邩�D�͈͂�1�`5
		/// </summary>
		private int tempomode = 0;
		public TempoButton[] tempobutton;

		public int TempoMode
		{
			get
			{
				return tempomode;
			}
			set
			{
				parent.parent.PictureStory.Slide[parent.NowPage].tempo = value;
				tempomode = value;
				for(int i=0;i<5;i++)
				{
					tempobutton[i].State = 0;
				}
				tempobutton[value-1].State = 1;
			}
		}
		public Tempo(StoryScreen story)
		{
			parent = story;
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X���A�e�N�X�`���E���W�̓o�^�A��ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			tempobutton = new TempoButton[5];
			for(int i=0;i<5;i++)
				//muphic.DrawManager.Regist(tempobutton[i].ToString(), 399-(i*66), 126, 
			{
				tempobutton[i] = new TempoButton(this, i+1);
				muphic.DrawManager.Regist(tempobutton[i].ToString(), 399-(i*66),126,/*456-(i*75), 123,*/ 
					"image\\one\\button\\tempo\\off\\tempo_" + (i+1) + ".png", "image\\one\\button\\tempo\\on\\tempo_" + (i+1) + ".png");
				BaseList.Add(tempobutton[i]);
			}

			//tempobutton[2].Click(System.Drawing.Point.Empty);	//�f�t�H���g�Ƃ��Đ^�񒆂̃{�^�����N���b�N������Ԃɂ��Ă���
		}

		public override void Click(System.Drawing.Point p)
		{												//���ׂĂ̗v�f���N���b�N���Ă��Ȃ���Ԃɖ߂�
			base.Click (p);
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			for(int i=0;i<5;i++)
			{
				tempobutton[i].MouseLeave();
			}
		}
	}
}
