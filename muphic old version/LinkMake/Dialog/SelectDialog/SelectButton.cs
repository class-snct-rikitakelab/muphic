using System;

namespace muphic.LinkMake.Dialog.Select
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
			// ���e���V���������ˁ[��
			// �t�@�C���������I�H �Ȃ�N���b�N�Ȃ񂴂����˂����I
			if (this.parent.parent.parent.dfList.Index.Count <= this.num) return;
			
			base.Click (p);
			
			// �t�@�C���ǂݍ��݃N���X���C���X�^���X��
			//LinkFileReader sfr = new LinkFileReader(this.parent.parent.quest.AnimalList, parent.level_select);
			LinkFileReader sfr = new LinkFileReader(this.parent.parent.parent.score.Animals.AnimalList);
			
			// 1�ԏ�̃t�@�C�����̗v�f�ԍ��{���������Ԗڂ�(0�`3)
			if (parent.NowPage+num >= parent.parent.parent.dfList.Index.Count) return;

			DataIndex di = (DataIndex)parent.parent.parent.dfList.Index[parent.NowPage + num];//this.FileNames[this.NowPage + num];
			
			// ��Ȃ牽�����Ȃ�
			if(di == null) return;

			string filename = "LinkData\\QuesPatt_" + di.Num + ".mdl";

			// ���ۂɓǂݍ��� �������A�ǂݍ��݂Ɏ��s�����炻�̂܂�
			if( !sfr.Read(filename) ) return;

			parent.parent.parent.tempo_l.TempoMode = sfr.Tempo;
			for(int i=0;i<5;i++)
			{
				parent.parent.parent.tempo_l.tempobutton_l[i].State = 0;						//�{���̃N���b�N�������s���O��
			}	
			parent.parent.parent.tempo_l.tempobutton_l[sfr.Tempo-1].State = 1;
			// �ۑ��_�C�A���O�̑薼�Ƀt�@�C�������R�s�[���Ă���
			//parent.parent.parent.sadialog.SetTitleName(this.parent.GetSelectFileName(num));
			parent.parent.parent.title = di.Title;
			parent.parent.parent.filenum = di.Num;
			
			this.State = 0;
			
			// ���ǂ�{�^�������������Ƃɂ��āA�_�C�A���O�����
			parent.parent.back.Click(System.Drawing.Point.Empty);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			
			//if(this.parent.GetSelectFileName(this.num) == null) return;
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
