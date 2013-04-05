using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// Tempo �̊T�v�̐����ł��B
	/// </summary>
	public class Tempo : Screen
	{
		public LinkMakeScreen parent;
		public int TempoMode = 0;
		public TempoButton[] tempobutton_l;
		public Tempo(LinkMakeScreen one)
		{
			parent = one;
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X���A�e�N�X�`���E���W�̓o�^�A��ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			tempobutton_l = new TempoButton[5];
			for(int i=0;i<5;i++)
			{
				tempobutton_l[i] = new TempoButton(this, i+1);
				muphic.DrawManager.Regist(tempobutton_l[i].ToString(), 399-(i*66), 126,/*590-(i*70), 125,*/ 
					"image\\link\\button\\tempo\\off\\tempo_" + (i+1) + ".png", "image\\link\\button\\tempo\\on\\tempo_" + (i+1) + ".png");
				BaseList.Add(tempobutton_l[i]);
			}

			tempobutton_l[2].Click(System.Drawing.Point.Empty);	//�f�t�H���g�Ƃ��Đ^�񒆂̃{�^�����N���b�N������Ԃɂ��Ă���
		}

		public override void Click(System.Drawing.Point p)
		{
			for(int i=0;i<5;i++)
			{
				tempobutton_l[i].State = 0;						//�{���̃N���b�N�������s���O��
			}													//���ׂĂ̗v�f���N���b�N���Ă��Ȃ���Ԃɖ߂�
			base.Click (p);
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			for(int i=0;i<5;i++)
			{
				((Base)BaseList[i]).MouseLeave();
			}
		}


	}
}
