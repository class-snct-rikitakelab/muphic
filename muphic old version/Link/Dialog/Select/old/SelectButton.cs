//using System;
//
//namespace muphic.Link.Dialog.Select
//{
//	/// <summary>
//	/// SelectButton �̊T�v�̐����ł��B
//	/// </summary>
//	public class SelectButton : Base
//	{
//		public SelectButtons parent;
//		public int num;
//		public SelectButton(SelectButtons sbs, int num)
//		{
//			parent = sbs;
//			this.num = num;
//		}
//
//		public override void Click(System.Drawing.Point p)
//		{
//			// ���e���V���������ˁ[��
//			// �t�@�C���������I�H �Ȃ�N���b�N�Ȃ񂴂����˂����I
//			if(this.parent.GetSelectFileName(this.num) == null) return;
//			
//			base.Click (p);
//			
//			// �t�@�C���ǂݍ��݃N���X���C���X�^���X�����Ă݂�
//			LinkFileReader sfr = new LinkFileReader(this.parent.parent.parent.quest.AnimalList);
//			
//			// 1�ԏ�̃t�@�C�����̗v�f�ԍ��{���������Ԗڂ�(0�`3)
//			string filename = this.parent.FileNames[this.parent.NowPage + num];
//			
//			// ��Ȃ牽�����Ȃ�
//			if(filename == "") return;
//			
//			// ���ۂɓǂݍ��� �������A�ǂݍ��݂Ɏ��s�����炻�̂܂�
//			if( !sfr.Read(filename) ) return;
//			parent.parent.parent.titlebar.Title = sfr.Name;
//
//			parent.parent.parent.QuestionNum = 1;
//			parent.parent.parent.Tempo = sfr.Tempo;
//			parent.parent.parent.group.pattern = sfr.pattern;
//			parent.parent.parent.score.AnimalList.Clear();
//			for (int i = 0; i < parent.parent.parent.score.putFlag.Length; i++)
//			{
//				parent.parent.parent.score.putFlag[i] = false;
//			}
//			for (int i = 0; i < 10; i++)
//			{
//				for (int j = 0; j < 4; j++)
//				{
//					parent.parent.parent.score.ribbon[i, j] = false;
//				}
//			}
//
//			// �ۑ��_�C�A���O�̑薼�Ƀt�@�C�������R�s�[���Ă���
//			//parent.parent.parent.sadialog.SetTitleName(this.parent.GetSelectFileName(num));
//			
//			this.State = 0;
//			
//			// ���ǂ�{�^�������������Ƃɂ��āA�_�C�A���O�����
//			//parent.parent.back.Click(System.Drawing.Point.Empty);
//			parent.parent.parent.LinkScreenMode = muphic.LinkScreenMode.ListenDialog;
//		}
//		
//		public override void MouseEnter()
//		{
//			base.MouseEnter ();
//			
//			if(this.parent.GetSelectFileName(this.num) == null) return;
//			this.State = 1;
//		}
//		
//		public override void MouseLeave()
//		{
//			base.MouseLeave ();
//			this.State = 0;
//		}
//		
//		public override string ToString()
//		{
//			return "SelectButton" + num;
//		}
//		
//	}
//}
