using System;
using System.IO;
using System.Text;
using muphic.Common;

namespace muphic.Tutorial.TutorialSPParts
{
	/// <summary>
	/// HintButton �̊T�v�̐����ł��B
	/// </summary>
	public class HintButton : Base
	{
		public TutorialMain parent;
		
		public string directoryname;
		public string[] filenames;
		public muphic.ADV.PatternData pattern;
		public string voicefile;

		public int hintstate;
		
		public HintButton(TutorialMain tm, string directoryname)
		{
			this.parent = tm;
			this.directoryname = directoryname;
			hintstate = -1;
			
			// �q���g�t�@�C���ꗗ�𓾂�
			filenames = TutorialTools.getFileNames(directoryname, ".hnt");
		}
		
		
		/// <summary>
		/// �X�e�[�g�i�s���\�b�h �q���g�G�f�B�V����
		/// </summary>
		public void NextState()
		{
			// �X�e�[�g�i�s
			this.hintstate++;
			
			// �������A�X�e�[�g���q���g�t�@�C���̃t�@�C�����𒴂��Ă�����-1�ɖ߂�
			if(hintstate >= this.filenames.Length) this.hintstate = 0;
			
			// �q���g�t�@�C���ǂݍ���
			this.pattern = this.ReadHintFile(this.filenames[hintstate]);
			
			// ���b�Z�[�W�E�B���h�E�Ƀe�L�X�g��n��
			this.parent.msgwindow.getText(this.pattern.text);
			
			// ���b�Z�[�W�E�B���h�E����
			this.parent.msgwindow.ChangeWindowCoordinate(1);
			
			// Voice�̏���
			// �܂��Đ����̂��~���A�V�����t�@�C�������Z�b�g�����肷��
			this.parent.SetVoice(this.pattern.Voice);
		}
		
		/// <summary>
		/// Voice�̍Đ����J�n���郁�\�b�h
		/// Voice�̃t�@�C�����̓N���X�t�B�[���h����擾
		/// </summary>
		/// <returns>�Đ��J�n�ł������ǂ��� true�Ȃ琬��</returns>
		public bool PlayVoice()
		{
			if(!this.parent.isPlayVoice) return false;
			
			// �p�X���܂߂��t�@�C�����̐���
			string filename = this.directoryname +  "\\" + this.voicefile;
			
			if( File.Exists(filename) )
			{
				// �t�@�C���̑��݃`�F�b�N���s���A���݂��Ă�����Đ��J�n
				VoiceManager.Play(filename);
				System.Console.WriteLine("Voice " + filename + " �Đ�");
				return true;
			}
			return false;
		}
		
		
		/// <summary>
		/// Voice�̍Đ����~���郁�\�b�h
		/// Voice�̃t�@�C�����̓N���X�t�B�[���h����擾
		/// </summary>
		/// <returns>�Đ���~�ł������ǂ��� true�Ȃ琬��</returns>
		public bool StopVoice()
		{
			if(!this.parent.isPlayVoice) return false;
			
			// �p�X���܂߂��t�@�C�����̐���
			string filename = this.directoryname + "\\" + this.voicefile;
			
			if( File.Exists(filename) )
			{
				// �t�@�C���̑��݃`�F�b�N���s���A���݂��Ă�����Đ���~
				VoiceManager.Stop();
				System.Console.WriteLine("Voice " + filename + " ��~");
				return true;
			}
			return false;
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �N���b�N���ꂽ��Ƃ肠�����X�e�[�g�i�s
			this.NextState();
		}
		
		
		/// <summary>
		/// �`���[�g���A���p�^�[���t�@�C���ǂݍ��݂̃��\�b�h���g���ăq���g�t�@�C����ǂ�
		/// </summary>
		/// <returns></returns>
		public muphic.ADV.PatternData ReadHintFile(string filename)
		{
			return TutorialTools.ReadPatternFile(filename);
		}
		
		#region �������͔p�~
		/*
		public string[] ReadHintFile()
		{
			System.Console.WriteLine("TutorialHintFile " + filenames[hintstate] + " �ǂݍ���");
			StreamReader reader = new StreamReader(filenames[hintstate++], Encoding.GetEncoding("Shift_JIS"));
			string[] hint = new string[3];
			string str;
			int num=0;
			
			// �Ƃ肠����������
			for(int i=0; i<hint.Length; i++) hint[i] = "";
			
			// �t�@�C�������܂œǂݍ���
			while( (str = reader.ReadLine()) != null ) hint[num++] = str;
			
			reader.Close();

			if(hintstate >= this.filenames.Length) this.hintstate = 0;
			
			return hint;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			this.parent.msgwindow.getText(this.ReadHintFile());
			this.parent.msgwindow.ChangeWindowCoordinate(1);
		}
		*/
		#endregion
		
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
