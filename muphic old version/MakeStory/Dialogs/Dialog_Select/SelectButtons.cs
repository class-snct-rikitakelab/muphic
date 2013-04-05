using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// �����͍��܂łƂ͏�������āA4��SelectButton�͏�ɌŒ肵�Ă����B
	/// �����āANowPage�̒l�ɂ���ĕ`�悷��t�@�C������ς��Ă������ƂɂȂ�B������AUpper��Lower
	/// �ł͂���NowPage�̒l��ς��āA�`�悷��̂��ύX�����͕̂����񂾂��ɂȂ�B
	/// ��ŁASelectButton���N���b�N���ꂽ�Ƃ��ɁA���݊֘A�t������Ă��镶�����p���ăt�@�C�����J���B
	/// </summary>
	public class SelectButtons : Screen
	{
		public StorySelectDialog parent;
		public String[] FileNames;
		public SelectButton[] sb;
		public int NowPage;															//�v��1�ԏ�ɕ\������Ă��镶����̗v�f�ԍ��̂���
		public SelectButtons(StorySelectDialog dialog)
		{
			parent = dialog;
			NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			FileNames = this.SearchStoryDataFiles();
			if(FileNames.Length < 4)
			{
				sb = new SelectButton[FileNames.Length];
			}
			else
			{
				sb = new SelectButton[4];
			}

			for(int i=0;i<sb.Length;i++)
			{
				sb[i] = new SelectButton(this, i);
			}

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				muphic.DrawManager.Regist(sb[i].ToString(), 307, 383 + i*28, @"image\MakeStory\dialog\select\sbutton_off.png", @"image\MakeStory\dialog\select\sbutton_on.png");
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
			for(int i=0;i<sb.Length;i++)
			{
				muphic.DrawManager.Draw(sb[i].ToString(), sb[i].State);
				if(NowPage + i < FileNames.Length      && !muphic.Common.TutorialStatus.getisTutorialDialog())
				{
					muphic.DrawManager.DrawString(FileNames[i+this.NowPage], 342, 386 + i*28);
				}
			}
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

		/// <summary>
		/// �t�H���_StoryData�Ɋi�[����Ă��镨��t�@�C���̖��O(�g���q������)�̔z���
		/// �擾���郁�\�b�h
		/// </summary>
		/// <returns></returns>
		private String[] SearchStoryDataFiles()
		{
			char[] separator = {'.'};
			char[] yen = {'\\'};
			String[] strs = System.IO.Directory.GetFiles("StoryData");					//StoryData���̃t�@�C�������擾
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
	}
}