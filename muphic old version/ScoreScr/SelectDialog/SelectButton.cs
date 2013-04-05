using System;

namespace muphic.ScoreScr.SelectDialog
{
	/// <summary>
	/// SelectButton �̊T�v�̐����ł��B
	/// </summary>
	public class SelectButton : Base
	{
		public SelectButtons parent;
		public int num;
		public SelectButton(SelectButtons sbs, int num)
		{
			parent = sbs;
			this.num = num;
		}

		public override void Click(System.Drawing.Point p)
		{
			// �t�@�C���������I�H �Ȃ�N���b�N�Ȃ񂴂����˂����I
			if(this.parent.GetSelectFileName(this.num) == null) return;
			
			base.Click (p);
			
			// �t�@�C���ǂݍ��݃N���X���C���X�^���X�����Ă݂�
			ScoreFileReader sfr = new ScoreFileReader(this.parent.parent.parent.AnimalList);
			
			// 1�ԏ�̃t�@�C�����̗v�f�ԍ��{���������Ԗڂ�(0�`3)
			string filename = this.parent.FileNames[this.parent.NowPage + num];
			
			// ��Ȃ牽�����Ȃ�
			if(filename == "") return;
			
			// ���ۂɓǂݍ��� �������A�ǂݍ��݂Ɏ��s�����炻�̂܂�
			if( !sfr.ReadMSDFile(filename) ) return;
			
			// �ǂݍ��񂾃f�[�^���特�����X�g���쐬���A�`�悳����
			System.Console.WriteLine("�������X�g�ǂݍ��� �ĕ`��");
			parent.parent.parent.scoremain.CreateScoreListAll();
			parent.parent.parent.scoremain.ReDraw();
			
			// �ۑ��_�C�A���O�̑薼�Ƀt�@�C�������R�s�[���Ă���
			parent.parent.parent.sadialog.SetTitleName(this.parent.GetSelectFileName(num));
			
			this.State = 0;
			
			// ���ǂ�{�^�������������Ƃɂ��āA�_�C�A���O�����
			parent.parent.back.Click(System.Drawing.Point.Empty);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			
			if(this.parent.GetSelectFileName(this.num) == null) return;
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
		
		public override string ToString()
		{
			return "SelectButton" + num;
		}
		
	}
}
