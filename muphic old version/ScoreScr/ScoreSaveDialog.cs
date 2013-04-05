using System;
using muphic.ScoreScr.SaveDialog;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScoreSaveDialog �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreSaveDialog : Screen
	{
		public ScoreScreen parent;

		public SaveButton save;
		public muphic.ScoreScr.SaveDialog.BackButton back;
		public CharForm form;
		public TitleButton title;

		public string titlename;	// �薼

		public ScoreSaveDialog(ScoreScreen score)
		{
			this.parent = score;
			this.SetTitleName(null);
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			save = new SaveButton(this);
			back = new muphic.ScoreScr.SaveDialog.BackButton(this);
			title = new TitleButton(this);
			form = new CharForm(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\ScoreXGA\\dialog_new\\nokosu\\haikei.png");
			muphic.DrawManager.Regist(save.ToString(), 671, 393, "image\\ScoreXGA\\dialog_new\\nokosu\\nokosu_off.png", "image\\ScoreXGA\\dialog_new\\nokosu\\nokosu_on.png");
			muphic.DrawManager.Regist(back.ToString(), 671, 451, "image\\ScoreXGA\\dialog_new\\back_off.png", "image\\ScoreXGA\\dialog_new\\back_on.png");
			muphic.DrawManager.Regist(title.ToString(), 338, 402, "image\\ScoreXGA\\dialog_new\\nokosu\\daimei_off.png", "image\\ScoreXGA\\dialog_new\\nokosu\\daimei_on.png");
			muphic.DrawManager.Regist(form.ToString(), 342, 456, "image\\ScoreXGA\\dialog_new\\nokosu\\charform.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(save);
			BaseList.Add(back);
			BaseList.Add(title);
			BaseList.Add(form);
		}
		
		
		/// <summary>
		/// �薼��^����ꂽ������ɏ��������郁�\�b�h
		/// </summary>
		/// <param name="name"></param>
		public void SetTitleName(string name)
		{
			this.titlename = name;
			this.parent.title.Text = name;
		}
		
		
		public override void Draw()
		{
			base.Draw ();
			
			// �薼�̕`��
			muphic.DrawManager.DrawString(this.titlename, 360, 470);
		}
	}
}
