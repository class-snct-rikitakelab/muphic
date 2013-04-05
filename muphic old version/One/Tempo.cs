using System;

namespace muphic.One
{
	/// <summary>
	/// Tempo �̊T�v�̐����ł��B
	/// </summary>
	public class Tempo : Screen
	{
		public OneScreen parent;
		public int tempoMode = 3;
		public TempoButton[] tempobutton;

		public int TempoMode
		{
			get
			{
				return tempoMode;
			}
			set
			{
				tempoMode = value;
				AllClear();
				tempobutton[tempoMode-1].State = 1;
			}
		}

		public Tempo(OneScreen one)
		{
			parent = one;
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X���A�e�N�X�`���E���W�̓o�^�A��ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			tempobutton = new TempoButton[5];
			for(int i=0;i<5;i++)
			{
				tempobutton[i] = new TempoButton(this, i+1);
				muphic.DrawManager.Regist(tempobutton[i].ToString(), 399-(i*66), 126, 
					"image\\one\\button\\tempo\\off\\tempo_" + (i+1) + ".png", "image\\one\\button\\tempo\\on\\tempo_" + (i+1) + ".png");
				BaseList.Add(tempobutton[i]);
			}

			tempobutton[2].Click(System.Drawing.Point.Empty);	//�f�t�H���g�Ƃ��Đ^�񒆂̃{�^�����N���b�N������Ԃɂ��Ă���
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		private void AllClear()
		{
			for(int i=0;i<5;i++)
			{
				tempobutton[i].State = 0;						//�{���̃N���b�N�������s���O��
			}													//���ׂĂ̗v�f���N���b�N���Ă��Ȃ���Ԃɖ߂�
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
