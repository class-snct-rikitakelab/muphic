using System;
using muphic.One.SaveDialog;

namespace muphic.One
{
	/// <summary>
	/// ScoreSaveDialog �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreSaveDialog : Screen
	{
		public OneScreen parent;

		public muphic.One.SaveDialog.SaveButton save;
		public muphic.One.SaveDialog.BackButton back;
		public muphic.One.SaveDialog.YesButton yes;
		
		public string DialogMsg;

		public ScoreSaveDialog(OneScreen one)
		{
			this.parent = one;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			save = new muphic.One.SaveDialog.SaveButton(this);
			back = new muphic.One.SaveDialog.BackButton(this);
			yes = new muphic.One.SaveDialog.YesButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\one\\savedialog\\dialog_save_bak.png");
			muphic.DrawManager.Regist(save.ToString(), 386, 434, "image\\one\\savedialog\\yes_off.png", "image\\one\\savedialog\\yes_on.png");
			muphic.DrawManager.Regist(back.ToString(), 535, 434, "image\\one\\savedialog\\no_off.png", "image\\one\\savedialog\\no_on.png");
			muphic.DrawManager.Regist(yes.ToString(), 460, 434, "image\\one\\savedialog\\yes_off.png", "image\\one\\savedialog\\yes_on.png");
			muphic.DrawManager.Regist("ScoreSaveDialog_warning",  310, 352, "image\\one\\savedialog\\warning.png" );
			muphic.DrawManager.Regist("ScoreSaveDialog_question", 409, 369, "image\\one\\savedialog\\question.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(save);
			BaseList.Add(back);
			BaseList.Add(yes);
			
			this.checkScoreName();
		}
		
		
		public void checkScoreName()
		{
			if(this.parent.ScoreTitle == null || this.parent.ScoreTitle == "")
			{
				// �Ȗ����󂾂�����A�Ȗ������߂�悤����
				this.DialogMsg = "ScoreSaveDialog_warning";
				this.save.Visible = this.back.Visible = false;
				this.yes.Visible = true;
			}
			else
			{
				// �Ȗ�������΁A�����ۑ����邩�₤
				this.DialogMsg = "ScoreSaveDialog_question";
				this.save.Visible = this.back.Visible = true;
				this.yes.Visible = false;
			}
		}
		
		public override void Draw()
		{
			base.Draw ();
			
			DrawManager.Draw(this.DialogMsg);
		}
	}
}
