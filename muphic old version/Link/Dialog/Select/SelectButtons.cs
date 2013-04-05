using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.Link.Dialog.Select
{
	public class SelectButtons : Screen
	{
		public SelectDialog parent;
		public String[] FileNames;
		public SelectButton[] sb;

		//public LinkFileData[] lfd;
		
		// �v��1�ԏ�ɕ\������Ă��镶����̗v�f�ԍ��̂���
		public int NowPage;
		
		public SelectButtons(SelectDialog dialog)
		{
			parent = dialog;
			NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			//this.doSearchScoreDataFiles();
			//this.FileNames = this.SearchScoreDataFiles();

			sb = new SelectButton[6];
			for(int i=0;i<sb.Length;i++)
			{
				sb[i] = new SelectButton(this, i);
			}

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				if(i+1+this.NowPage > parent.parent.dfList.Index.Count) continue;
					//this.FileNames.Length) continue;
				muphic.DrawManager.Regist(sb[i].ToString(), 324, 378 + i*28, "image\\link\\dialog\\select_new\\sbutton_off.png", "image\\link\\dialog\\select_new\\sbutton_on.png");
			}
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				BaseList.Add(sb[i]);
			}
		}

		public SelectButtons(int num, SelectDialog dialog)
		{
			parent = dialog;
			//this.doSearchScoreDataFiles();
			ReadFile(num);
			
		}
		
		public override void Draw()
		{
			//base.Draw ();
			for(int i=0; i<sb.Length; i++)
			{
				muphic.DrawManager.Draw(sb[i].ToString(), sb[i].State);

				if(NowPage + i < parent.parent.dfList.Index.Count && !muphic.Common.TutorialStatus.getisTutorialDialog())
					//FileNames.Length   && !muphic.Common.TutorialStatus.getisTutorialDialog())
				{
					//muphic.DrawManager.DrawString(this.GetSelectFileName(i).Title, 358, 381 + i*28);
					DataIndex di = (DataIndex)parent.parent.dfList.Index[NowPage+i];
					muphic.DrawManager.DrawString(di.Title, 358, 381 + i*28);
				}
			}
		}
		
		
//		/// <summary>
//		/// �f�[�^�t�H���_���̃t�@�C���ꗗ���擾���郁�\�b�h
//		/// </summary>
//		/// <returns>�擾�����t�@�C���ꗗ</returns>
//		private string[] SearchScoreDataFiles()
//		{
//			// ScoreData�t�H���_���̃t�@�C���ꗗ�̎擾
//			//string[] scorefiles = System.IO.Directory.GetFiles("LinkData");
//
//			ArrayList linkfiles = new ArrayList();
//			for (int i = 0; i < 100; i++)
//			{
//				// 0���珇�Ԃ�"������_�ԍ�.mdl"�����݂��邩�`�F�b�N���Ă���
//				string filename = "LinkData\\QuesPatt_" + i.ToString() + ".mdl";
//
//				//�@�t�@�C�������݂���ꍇ�ǉ�
//				if (File.Exists(filename)) linkfiles.Add(filename);
//			}
//			
//			// �p�X�Ɗg���q�����̂܂܂ɂ����t�@�C�����̈ꗗ��Ԃ�
//
//			string[] str = (string[])linkfiles.ToArray(typeof(string));
//			return str;
//		}
//		
//		
//		/// <summary>
//		/// �t�@�C���ꗗ���擾���AFileNames�t�B�[���h�Ɋi�[���郁�\�b�h
//		/// �O���N���X��������s�ł���悤�ɂ��邽�߃��\�b�h�ɂ���
//		/// </summary>
//		public void doSearchScoreDataFiles()
//		{
//			this.FileNames = this.SearchScoreDataFiles();
//			
//			lfd = new LinkFileData[FileNames.Length];
//			for (int i = 0; i < FileNames.Length; i++)
//			{
//				lfd[i] = GetSelectFileName(i);
//			}
//		}
//		
//		
//		/// <summary>
//		/// null
//		/// </summary>
//		/// <param name="num"></param>
//		/// <returns></returns>
//		public LinkFileData GetSelectFileName(int num)
//		{
//			if(FileNames.Length <= NowPage + num) return null;
//			LinkFileData lfd = new LinkFileData();
//			
//			try
//			{
//				StreamReader sr = new StreamReader(FileNames[NowPage + num], Encoding.GetEncoding("Shift_JIS"));
//
//				lfd.Title = sr.ReadLine();
//				lfd.Level = int.Parse(sr.ReadLine());
//				sr.Close();
//				return lfd;
//			}
//			catch(FileNotFoundException e)
//			{
//				return null;
//			}
//		}
		
		
		/// <summary>
		/// �t�@�C��������ɃX�N���[��������Ƃ��ɌĂԃ��\�b�h
		/// </summary>
		public void Upper()
		{
			if(0 < NowPage)
			{
				NowPage--;
			}
		}

		/// <summary>
		/// �t�@�C���������ɃX�N���[��������Ƃ��ɌĂԃ��\�b�h
		/// </summary>
		public void Lower()
		{
			if(NowPage+6 < parent.parent.dfList.Index.Count)//FileNames.Length)
			{
				NowPage++;
			}
		}

		/// <summary>
		/// �t�@�C���ǂݍ���
		/// </summary>
		public void ReadFile(int num)
		{
			// �t�@�C���ǂݍ��݃N���X���C���X�^���X��
			LinkFileReader sfr = new LinkFileReader(this.parent.parent.quest.AnimalList, parent.level_select);
			
			// 1�ԏ�̃t�@�C�����̗v�f�ԍ��{���������Ԗڂ�(0�`3)
			if (this.NowPage+num >= parent.parent.dfList.Index.Count) return;

			DataIndex di = (DataIndex)parent.parent.dfList.Index[this.NowPage + num];//this.FileNames[this.NowPage + num];
			
			// ��Ȃ牽�����Ȃ�
			if(di == null) return;

			string filename = "LinkData\\QuesPatt_" + di.Num + ".mdl";
					

			// ���ۂɓǂݍ��� �������A�ǂݍ��݂Ɏ��s�����炻�̂܂�
			int count;
			if((count = sfr.Read(filename)) < 0) return;
			
			parent.parent.score.barNum = count;
			
			parent.parent.links.ButtonVisibleOn();
			parent.parent.links.ButtonVisibleOff(count);
			parent.parent.titlebar.Title = sfr.Name;

			parent.parent.links.BaseState0(); //���炩���ߑI���{�^���̑I�����������Ă���
			parent.parent.tsuibi.State = 11;

			parent.parent.QuestionNum = 1;
			parent.parent.Tempo = sfr.Tempo;
			parent.parent.group.pattern = sfr.pattern;
			parent.parent.score.AnimalList.Clear();
			for (int i = 0; i < parent.parent.score.putFlag.Length; i++)
			{
				parent.parent.score.putFlag[i] = false;
			}
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					parent.parent.score.ribbon[i, j] = false;
				}
			}

			parent.parent.LinkScreenMode = muphic.LinkScreenMode.ListenDialog;
		}
	}
}