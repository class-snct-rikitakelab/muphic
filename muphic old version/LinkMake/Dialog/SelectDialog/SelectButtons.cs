using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.LinkMake.Dialog.Select
{
	public class SelectButtons : Screen
	{
		public LinkSelectDialog parent;
		public String[] FileNames;
		public SelectButton[] sb;
		
		// �v��1�ԏ�ɕ\������Ă��镶����̗v�f�ԍ��̂���
		public int NowPage;
		
		public SelectButtons(LinkSelectDialog dialog)
		{
			parent = dialog;
			NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			//this.doSearchScoreDataFiles();
			//this.FileNames = this.SearchScoreDataFiles();

			sb = new SelectButton[4];
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
				muphic.DrawManager.Regist(sb[i].ToString(), 307, 383 + i*28, "image\\link\\dialog\\select\\sbutton_off.png", "image\\link\\dialog\\select\\sbutton_on.png");
			}
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				BaseList.Add(sb[i]);
			}
		}
		
		public override void Draw()
		{
			//base.Draw ();
			for(int i=0; i<sb.Length; i++)
			{
				muphic.DrawManager.Draw(sb[i].ToString(), sb[i].State);
				if(NowPage + i < parent.parent.dfList.Index.Count)
				{
					// �t�@�C��������A���_�[�X�R�A�ȉ����������������\��
					//muphic.DrawManager.DrawString(this.GetSelectFileName(i), 342, 386 + i*28);
					DataIndex di = (DataIndex)parent.parent.dfList.Index[NowPage+i];
					muphic.DrawManager.DrawString(di.Title, 342, 386 + i*28);
				}
			}
		}
		
		
//		/// <summary>
//		/// �y���f�[�^�t�H���_���̃t�@�C���ꗗ���擾���郁�\�b�h
//		/// </summary>
//		/// <returns>�擾�����t�@�C���ꗗ</returns>
//		private string[] SearchScoreDataFiles()
//		{
//			// ScoreData�t�H���_���̃t�@�C���ꗗ�̎擾
//			//string[] scorefiles = System.IO.Directory.GetFiles("LinkData");
//
//			ArrayList linkfiles = new ArrayList();
//			for (int i = 10; i < 100; i++)
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
//		}
//		
//		
//		/// <summary>
//		/// ��т����ꗗ�ɕ\�����镶����𓾂郁�\�b�h
//		/// �������܂܍������ł݂��Ȋ���
//		/// </summary>
//		/// <param name="num"></param>
//		/// <returns></returns>
//		public string GetSelectFileName(int num)
//		{
//			// ���X�g�̃T�C�Y�𒴂����ʒu�̃t�@�C�����𓾂悤�Ƃ��Ă���null��Ԃ�
//			if(FileNames.Length <= NowPage + num) return null;
//			
//			////			// �t�@�C���� "ScoreData\\*_?.msd" ���� "*_?.msd" �̕����𒊏o���A����ɃA���_�[�X�R�A�ȉ���؂����ĕԂ�
//			////			return FileNames[NowPage + num].Split(new char[] {'\\'})[1].Split(new char[] {'_'})[0];
//			try
//			{
//				StreamReader sr = new StreamReader(FileNames[NowPage + num], Encoding.GetEncoding("Shift_JIS"));
//				String str = sr.ReadLine();
//				sr.Close();
//				return str;
//			}
//			catch(FileNotFoundException e)
//			{
//				// ����Ȃ��Ƃ��肦��Ǝv�����ǂ�
//				//MessageBox.Show("��p�I�t�@�C����������Ȃ�");
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
			if(NowPage+4 < parent.parent.dfList.Index.Count)
			{
				NowPage++;
			}
		}
	}
}