using System;

namespace muphic.ScoreScr.SelectDialog
{
	public class SelectButtons : Screen
	{
		public ScoreSelectDialog parent;
		public String[] FileNames;
		public SelectButton[] sb;
		
		// �v��1�ԏ�ɕ\������Ă��镶����̗v�f�ԍ��̂���
		public int NowPage;
		
		public SelectButtons(ScoreSelectDialog dialog)
		{
			parent = dialog;
			NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			this.doSearchScoreDataFiles();
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
				if(i+1+this.NowPage > this.FileNames.Length) continue;
				muphic.DrawManager.Regist(sb[i].ToString(), 307, 383 + i*28, "image\\ScoreXGA\\dialog_new\\yobidasu\\sbutton_off.png", "image\\ScoreXGA\\dialog_new\\yobidasu\\sbutton_on.png");
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
				if(NowPage + i < FileNames.Length)
				{
					// �t�@�C��������A���_�[�X�R�A�ȉ����������������\��
					muphic.DrawManager.DrawString(this.GetSelectFileName(i), 342, 386 + i*28);
				}
			}
		}
		
		#region ver.SETO
		
		/*
		/// <summary>
		/// �t�H���_StoryData�Ɋi�[����Ă��镨��t�@�C���̖��O(�g���q������)�̔z���
		/// �擾���郁�\�b�h
		/// </summary>
		/// <returns></returns>
		private String[] SearchScoreDataFiles()
		{
			char[] separator = {'.'};
			char[] yen = {'\\'};
			String[] strs = System.IO.Directory.GetFiles("ScoreData");					//StoryData���̃t�@�C�������擾
			String[] answer = new String[strs.Length];
			for(int i=0;i<strs.Length;i++)
			{
				strs[i] = strs[i].Split(yen, 2)[1];										//"StoryData\\����.txt"�́A"StoryData"�̕�������菜��
			}
			for(int i=0;i<strs.Length;i++)												//"����.txt"�́A".txt"�̕�������菜��											
			{
				answer[i] = "";
				String[] s = strs[i].Split(separator);
				for(int j=0;j<s.Length-1;j++)											//�g���q�ȊO��.���g���Ă��鎞�̂��߂̑΍�
				{																		//Split�����ۂ̊g���q�ȊO�̗v�f�����ׂĂȂ����킹��΂����B
					if(answer[i] != "")
					{
						answer[i] += '.';
					}
					answer[i] += s[j];
				}
			}
			return answer;
		}
		
		
		/// <summary>
		/// SelectButton��Click���\�b�h�ł�����ĂԁB
		/// ������SelectButton�ɑΉ������t�@�C�������擾����B
		/// </summary>
		/// <param name="num"></param>
		public String GetSelectFileName(int num)
		{
			if(FileNames.Length <= NowPage + num)
			{
				return null;
			}
			return FileNames[NowPage + num];
		}
		*/
		
		#endregion
		
		#region ver.Gackt ���������g��
		
		/// <summary>
		/// �y���f�[�^�t�H���_���̃t�@�C���ꗗ���擾���郁�\�b�h
		/// </summary>
		/// <returns>�擾�����t�@�C���ꗗ</returns>
		private string[] SearchScoreDataFiles()
		{
			// ScoreData�t�H���_���̃t�@�C���ꗗ�̎擾
			string[] scorefiles = System.IO.Directory.GetFiles("ScoreData");
			
			// �p�X�Ɗg���q�����̂܂܂ɂ����t�@�C�����̈ꗗ��Ԃ�
			return scorefiles;
		}

		
		/// <summary>
		/// �t�@�C���ꗗ���擾���AFileNames�t�B�[���h�Ɋi�[���郁�\�b�h
		/// �O���N���X��������s�ł���悤�ɂ��邽�߃��\�b�h�ɂ���
		/// </summary>
		public void doSearchScoreDataFiles()
		{
			this.FileNames = this.SearchScoreDataFiles();
		}
		
		
		/// <summary>
		/// ��т����ꗗ�ɕ\�����镶����𓾂郁�\�b�h
		/// �������܂܍������ł݂��Ȋ���
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public string GetSelectFileName(int num)
		{
			// ���X�g�̃T�C�Y�𒴂����ʒu�̃t�@�C�����𓾂悤�Ƃ��Ă���null��Ԃ�
			if(FileNames.Length <= NowPage + num) return null;
			
			// �t�@�C���� "ScoreData\\*_?.msd" ���� "*_?.msd" �̕����𒊏o���A����ɃA���_�[�X�R�A�ȉ���؂����ĕԂ�
			return FileNames[NowPage + num].Split(new char[] {'\\'})[1].Split(new char[] {'_'})[0];
		}
		
		#endregion
		
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
			if(NowPage+4 < FileNames.Length)
			{
				NowPage++;
			}
		}
	}
}