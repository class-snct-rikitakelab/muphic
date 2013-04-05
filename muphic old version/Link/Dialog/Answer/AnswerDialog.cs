using System;
using System.Drawing;


namespace muphic.Link.Dialog.Answer
{
	/// <summary>
	/// SelectDialog �̊T�v�̐����ł��B
	/// </summary>
	public class AnswerDialog : Screen
	{
		public LinkScreen parent;
		
		public AnswerBackButton back;
		public AnswerResult result;

		public AnswerDialog(LinkScreen link, bool state)
		{
			this.parent = link;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			result = new AnswerResult(this);
			back = new AnswerBackButton(this);


			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			if (state)
			{
				result.State = 0;
				muphic.SoundManager.Play("OK.wav");
			}
			else
			{
				result.State = 1;
				muphic.SoundManager.Play("NG.wav");
			}
			muphic.DrawManager.Regist(this.ToString(), 124+112, 167+84, "image\\link\\dialog\\answer\\dummy.png");
            muphic.DrawManager.Regist(result.ToString(), 124+112, 167+84, "image\\link\\dialog\\answer\\dialog_true.png", "image\\link\\dialog\\answer\\dialog_false.png");
			muphic.DrawManager.Regist(back.ToString(), 558+112, 368+70, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////

			BaseList.Add(result);
			BaseList.Add(back);
			

			//result���������Cback��MouseEnter�CMouseLeave���Ă΂�Ȃ��݂����Ȃ̂ŁC
			//����̍�C�摜��؂����Ă݂܂���
			
			
			// �`���[�g���A�����ł���΂��Ă���悤�ł��B
			if(muphic.Common.TutorialStatus.getIsSPMode() == "PT02_Link30") this.getResult(state);
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}

		public override void Draw()
		{
			base.Draw ();
		}
		
		
		
		
		// �`���[�g���A�����ł���΂��Ă���悤�ł��B
		
		/// <summary>
		/// �������킹�̌��ʂ��`���[�g���A���̃��C�����ɑ���
		/// ���R����I���̔���Ɏg�p
		/// </summary>
		/// <param name="result">����</param>
		/// <returns></returns>
		public bool getResult(bool result)
		{
			this.parent.parent.tutorialparent.SPCommand("PT02_Link30_" + result.ToString());
			return result;
		}
		
	}
}