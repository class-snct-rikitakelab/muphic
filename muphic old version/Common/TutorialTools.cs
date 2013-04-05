using System;
using System.IO;
using System.Text;
using System.Drawing;
using muphic.Common;
using muphic.ADV;

namespace muphic.Common
{
	/// <summary>
	/// TutorialTools �̊T�v�̐����ł��B
	/// </summary>
	public class TutorialTools
	{
		public TutorialTools()
		{
		}
		
		/// <summary>
		/// �p�^�[���t�@�C����ǂݍ��ރ��\�b�h
		/// ���ADV�p�[�g���`���[�g���A���Ŏg�p
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static PatternData ReadPatternFile(string filename)
		{
			string str;
			PatternData data = new PatternData();
			
			// �ǂݍ��݃o�b�t�@�ݒ� �t�@�C�����̓p�^�[���t�@�C���ꗗ����擾
			Console.WriteLine("�p�^�[���t�@�C��: " + filename + "�ǂݍ���");
			StreamReader reader = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
			
			// �s���܂�1�s���Ɠǂݍ���
			while( (str = reader.ReadLine()) != null )
			{
				string[] temp;
				
				// �悸�́A�R�����g&�^�u�̍폜
				str = TutorialTools.RemoveStr(str, "/");
				str = TutorialTools.RemoveStr(str, "\t");
				
				// ���ɁA�p�^�[���{����'='�ŕ���
				temp = str.Split( new char[] {'='} );
				
				// �ǂݍ��񂾕����񂪉��̒l���ɂ���ď������ς��
				switch(temp[0])
				{
					case "NowLoading":
						// NowLoading��ʂ��Ăяo��or����
						int registnum = int.Parse(temp[1]);
						// �l��1�ȏ�Ȃ炻�̐��������ɂ���NowLoading���Ăяo��
						// ����ȊO�Ȃ�NowLoading�I���Ƃ���
						if(registnum > 0) DrawManager.BeginRegist(registnum);
						else DrawManager.EndRegist();
						break;
						
					case "Regist":
					case "NextButtonRegist":
						// �o�^����摜�t�@�C���� �o�^���ƃp�X�t���t�@�C�����𓾂�
						// �܂�CVS�`���œo�^���A�p�X�t���t�@�C������؂藣���Ĕz��Ɋi�[
						string[] registdata = temp[1].Split(new char[] {','});
						// �������ACVS�`���Ńt�@�C�����Ɠo�^�����Ȃ���΃_��
						if(registdata.Length < 2) break;
						// �o�^����摜�t�@�C������filenames�z��Ɋi�[
						string[] filenames = new string[registdata.Length-1];
						for(int i=0; i<filenames.Length; i++) filenames[i] = registdata[1+i];
						if(temp[0] == "Regist")
							// �摜��o�^ filenames�z����ہX�o�^�[ ���W�͓K��
							DrawManager.Regist(registdata[0], 0, 0, filenames);
						else
							// ���b�Z�[�W�E�B���h�E�̒x���`���A���q�{�^���\���̒��O�ɉ摜��Ǎ��ނ悤�ݒ�
							data.addNBRegist(registdata[0], 0, 0, filenames);
						break;
						
					case "Image":
						// �摜�t�@�C���̐ݒ� �����ɍ��W�̐ݒ���s��
						// �܂�CVS�`���œo�^�摜�t�@�C���A���W��؂藣���Ĕz��Ɋi�[
						string[] image = temp[1].Split(new char[] {','});
						// �������ACVS�`���ŉ摜���ƍ��W���Ȃ���΃_��
						if(image.Length != 3) break;
						data.addUseImage(image[0], int.Parse(image[1]), int.Parse(image[2]));
						break;
						
					case "MsgWindow":
						// ���b�Z�[�W�E�B���h�E�̕\���ݒ�
						data.Window = int.Parse(temp[1]);
						break;

					case "Text":
						// �V�i���I�{���̐ݒ�
						data.addText(temp[1]);
						break;
						
					case "MsgAssistant":
						// ���b�Z�[�W�E�B���h�E�̃A�V�X�^���g�����̑I��
						data.assistant = temp[1];
						break;
						
					case "NextButton":
						// ���b�Z�[�W�E�B���h�E�̎��փ{�^����`�悷�邩�ۂ�
						if(temp[1] == "false") data.NextButton = false;
						break;

					case "Trigger":
						// �g���K�[���X�g�֒ǉ�
						TutorialStatus.addTriggerPartsList(temp[1]);
						break;

					case "Permission":
						// �N���b�N�����X�g�֒ǉ�
						TutorialStatus.addPermissionPartsList(temp[1]);
						break;
						
					case "StandBy":
						// true�������瓮��ҋ@��Ԃɂ���
						if(temp[1] == "true") TutorialStatus.setEnableNextStateStandBy();
						break;
						
					case "NextState":
						// true�������炻�̂܂܎��̃X�e�[�g�֐i�܂���
						if(temp[1] == "true") data.NextState = true;
						break;
						
					case "ClickRectangle":
						// �N���b�N�����W�Ő�������ꍇ
						string[] rect = temp[1].Split(new char[] {','});
						data.rect = new Rectangle(int.Parse(rect[0]), int.Parse(rect[1]), int.Parse(rect[2]), int.Parse(rect[3]));
						break;
						
					case "MouseClick":
						// ���W���w�肵�N���b�N���������ꍇ
						string[] p = temp[1].Split(new char[] {','});
						data.MouseClick = new Point(int.Parse(p[0]), int.Parse(p[1]));
						break;
						
					case "ChapterTop":
						// �e�`���v�^�[�̃g�b�v��ʂ�`�悷��
						data.ChapterTop = temp[1];
						break;
						
					case "BackTopScreen":
						// true��������g�b�v�X�N���[���ɖ߂�
						if(temp[1] == "true") data.TopScreen = true;
						break;
						
					case "SPMode":
						// ��������R�}���h�g�p����
						data.SPMode = temp[1];
						break;
						
					case "DrawString":
						// �`���[�g���A���Ǘ����ȊO�̕�����̕`��̐ݒ�
						if( temp[1] == "false" ) data.DrawString = false;
						break;
						
					case "Fade":
						// �t�F�[�h���ʂ�t������ꍇ
						string[] temp5 = temp[1].Split(new char[] {','});
						if(temp5.Length != 2) break;
						data.Fade.X = int.Parse(temp5[0]);
						data.Fade.Y = int.Parse(temp5[1]);
						break;
						
					case "BGM":
						// BGM�t�@�C�����̐ݒ� �������́ABGM���~�߂�"STOP"�Ȃ�
						data.BGM = temp[1];
						break;
						
					case "SE":
						// ���ʉ��̐ݒ�
						data.SE = temp[1];
						break;
						
					case "Voice":
						// Voice�̐ݒ�
						data.Voice = temp[1];
						break;
						
					default:
						break;
				}
			}
			
			reader.Close();
			
			return data;
		}
		
		
		/// <summary>
		/// �`���[�g���A���̏�Ԃ��t�@�C���ɃZ�[�u���郁�\�b�h
		/// </summary>
		/// <param name="savefile">�Z�[�u�t�@�C����</param>
		/// <param name="chapter">�`���v�^�[�ԍ�</param>
		/// <param name="chaptername">�`���v�^�[��</param>
		/// <param name="chapternum">�S�`���v�^�[��</param>
		public static void WriteSaveFile(string savefile, int chapter, string chaptername, int chapternum)
		{
			// �������݃o�b�t�@�̐ݒ�
			StreamWriter writer = new StreamWriter(savefile, false, Encoding.GetEncoding("Shift_JIS"));
			
			// �`���v�^�[�ԍ��ƃ`���v�^�[���A�S�`���v�^�[���̊e���ڂ�����������
			writer.WriteLine("Chapter=" + chapter);
			writer.WriteLine("ChapterName=" + chaptername);
			writer.WriteLine("AllChapterNum=" + chapternum);
			System.Console.WriteLine("TutrialSave : " + savefile);
			
			writer.Close();
		}
		
		
		/// <summary>
		/// �`���[�g���A���̏�Ԃ��t�@�C������ǂݍ��ރ��\�b�h
		/// �������Z�[�u�f�[�^�Ȃ̂����낢��`�F�b�N����\�肾���������Ԗ�����Ō����_�ł͍Œ���̋@�\�̂ݎ�������
		///	�ǂݍ��񂾌�͍폜�ł���
		/// </summary>
		/// <param name="savefile">�Z�[�u�t�@�C����</param>
		/// <param name="delete">�ǂݍ��݌�ɍ폜���邩�ǂ���</param>
		/// <returns>�ĊJ����`���v�^�[�ԍ�</returns>
		public static int ReadSaveFile(string savefile, bool delete)
		{
			// �܂��^����ꂽ�t�@�C�����̑��݃`�F�b�N
			// �t�@�C�������݂��Ȃ������ꍇ��0��Ԃ�
			if( !File.Exists(savefile) ) return 0;
			
			// �ǂݍ��݃o�b�t�@�̐ݒ�
			System.Console.WriteLine("TutorialSaveRead : " + savefile);
			StreamReader reader = new StreamReader(savefile, Encoding.GetEncoding("Shift_JIS"));
			string str;
			
			// �t�@�C�������܂ōs���Ƃɓǂݍ���
			while( (str = reader.ReadLine()) != null )
			{
				// �`���v�^�[���̍��ڂ���������A�`���v�^�[��������؂����Ă��̒l��Ԃ�
				// ���Ԗ�����ŗ�O�]�X�͍��͊��� ���Ԃ������珑���܂�
				if( str.IndexOf("Chapter=") != -1 )
				{
					reader.Close();
					if(delete) File.Delete(savefile);	// �폜����ݒ�ɂȂ��Ă�����
					return int.Parse( str.Split(new char[] {'='})[1] );
				}
			}
			
			// �t�@�C�����Ƀ`���v�^�[�����ۂ��̂������ĂȂ������ꍇ��0��Ԃ�
			reader.Close();
			if(delete) File.Delete(savefile);
			return 0;
		}
		
		
		/// <summary>
		/// �^����ꂽ�����񂩂�w�肳�ꂽ��������폜���郁�\�b�h
		/// </summary>
		/// <param name="str">���̕�����</param>
		/// <param name="remove">�폜���镶����</param>
		/// <returns>���̕����񂩂�w�肳�ꂽ��������폜����������</returns>
		public static string RemoveStr(string str, string remove)
		{
			int num = str.IndexOf(remove);
			if(num != -1)
			{
				str = str.Remove(num, str.Length - num);
			}
			return str;
		}
		
		
		/// <summary>
		/// �w�肳�ꂽ�g���q�̃t�@�C���ꗗ�𓾂郁�\�b�h
		/// </summary>
		/// <param name="directoryname">�ꗗ�𓾂�f�B���N�g��</param>
		/// <returns>�p�^�[���t�@�C���ꗗ</returns>
		public static string[] getFileNames(string directoryname, string extension)
		{
			// �܂��t�@�C���ꗗ�𓾂�
			string[] AllFileNames = TutorialTools.getFileNames(directoryname);
			
			// �t�@�C���ꗗ�̒�����p�^�[���t�@�C�������𒊏o
			int num = 0;
			for(int i=0; i<AllFileNames.Length; i++)
			{
				// �t�@�C���ꗗ�̒���".pat"�ȊO�̃t�@�C����S�ċ�ɂ��A".pat"�̃t�@�C�����J�E���g
				if( Path.GetExtension(AllFileNames[i]) != extension ) AllFileNames[i] = "";
				else num++;
			}

			// �p�^�[���t�@�C���ꗗ�p�z���e�� �v�f���̓J�E���g����".pat"�t�@�C���̐�
			string[] FileNames = new string[num];
			num = 0;
			
			for(int i=0; i<AllFileNames.Length; i++)
			{
				if(AllFileNames[i] != "") FileNames[num++] = AllFileNames[i]; 
			}

			return FileNames;
		}
		
		
		/// <summary>
		/// �w�肳�ꂽ�f�B���N�g���̃t�@�C���̈ꗗ�𓾂郁�\�b�h
		/// </summary>
		/// <param name="directoryname">�ꗗ�𓾂�f�B���N�g��</param>
		/// <returns>�t�@�C���ꗗ</returns>
		public static string[] getFileNames(string directoryname)
		{
			return System.IO.Directory.GetFiles(directoryname);
		}
		
		
		
		/// <summary>
		/// �w�肳�ꂽ�f�B���N�g�����̓���̕���������f�B���N�g���ꗗ�𓾂郁�\�b�h
		/// indexname��"ABC"�Ǝw�肷��ƁAdirectoryname����"ABC"�Ƃ�����������܂ރf�B���N�g�����̈ꗗ��Ԃ�
		/// </summary>
		/// <param name="directoryname">�ꗗ�𓾂�f�B���N�g��</param>
		/// <param name="indexname"></param>
		/// <returns></returns>
		public static string[] getDirectoryNames(string directoryname, string indexname)
		{
			// �܂��f�B���N�g���ꗗ�𓾂�
			string[] AllDirectoryNames = TutorialTools.getDirectoryNames(directoryname);

			// �f�B���N�g���ꗗ�̒��������̕�����������̂����𒊏o
			int num = 0;
			for(int i=0; i<AllDirectoryNames.Length; i++)
			{
				// �f�B���N�g���ꗗ�̒��œ���̕�����������Ȃ����̂���ɏ㏑�����A���f�B���N�g�������J�E���g
				if( AllDirectoryNames[i].IndexOf(indexname) == -1 ) AllDirectoryNames[i] = "";
				else num++;
			}
			
			// �f�B���N�g���ꗗ�p�z���p�� �v�f���̓J�E���g����indexname���܂ރf�B���N�g�����̐�
			string[] DirectoryNames = new string[num];
			num = 0;

			for(int i=0; i<AllDirectoryNames.Length; i++)
			{
				if(AllDirectoryNames[i] != "") DirectoryNames[num++] = AllDirectoryNames[i];
			}
			
			return DirectoryNames;
		}
		

		/// <summary>
		/// �w�肳�ꂽ�f�B���N�g�����̃f�B���N�g���ꗗ�𓾂郁�\�b�h
		/// </summary>
		/// <param name="directoryname">�ꗗ�𓾂�f�B���N�g��</param>
		/// <returns></returns>
		public static string[] getDirectoryNames(string directoryname)
		{
			return System.IO.Directory.GetDirectories(directoryname);
		}
	}
}
