using System;
using System.Drawing;
using System.Collections;

namespace muphic.ADV
{
	/// <summary>
	/// PatternData �̊T�v�̐����ł��B
	/// </summary>
	public class PatternData
	{
		public ArrayList UseImage;		// �g�p����摜(MainStoryScreen��Regist����������), �摜�̍��W
		public string[] text;			// �V�i���I�{��
		public string BGM;				// BGM "STOP"�Ȃ猻�݂�BGM���~������			��
		public string SE;				// SE ��{�I�Ɉ�񂾂�����						�������̓t�@�C�������w��
		public string Voice;			// Voice ��񂾂��炵�Ď��̃p�^�[���Œ�~		��
		
		public int Window;				// �E�B���h�E�̕\���ʒu 0:�\�����Ȃ� 1:�ʏ�ʒu�ɕ\�� 2�`:�ʂȈʒu�ɕ\��
		public string assistant;		// �E�B���h�E���ɕ\������A�V�X�^���g�̓����摜(Regist����������)
		public bool NextButton;			// ���b�Z�[�W�E�B���h�E�̎��փ{�^����`�悷�邩
		public bool EndButton;			// ���b�Z�[�W�E�B���h�E�̂����{�^����`�悷�邩
		public bool TopScreen;			// �`���[�g���A�����s���Ɏ����I�Ƀg�b�v��ʂɖ߂�ۂɎg�p
		public bool NextState;			// �p�^�[���ǂݍ��݌�ɑ��X�e�[�g�i�s���s���ۂɎg�p
		public Rectangle rect;			// �N���b�N�ł���̈�𕔕i�łȂ����W�Ő�������ꍇ�Ɏg�p
		public Point MouseClick;		// ����̍��W�ŃN���b�N������ꍇ�Ɏg�p������W
		public string ChapterTop;		// �e�`���v�^�[�J�n���̃g�b�v��ʂ�`�悷��ۂɎg�p�i�g�b�v��ʔw�i�摜�̃t�@�C�������i�[����j
		public string SPMode;			// ����ȏ�Ԃ�\�� ��������R�}���h�g�p�̂��߂�
		public bool DrawString;			// �`���[�g���A�����Ƀg�b�v�X�N���[���ȉ��̕������`�悷�邩
		public Point Fade;				// �t�F�[�h���ʂ��s�����ǂ��� �~�����͉̂񐔂Ǝ��Ԃ���Point�N���X�ő�p
		public ArrayList NBRegist;		// �x���`��I����̎��փ{�^���\�����O�ŉ摜��Ǎ��ލۂɎg�p
�@		
		public PatternData()
		{
			this.UseImage = new ArrayList();		// �Ƃ肠�����C���X�^���X�����Ă݂�
			this.BGM = "";							// �f�t�H���g�ł͋�
			this.SE = "";							// �f�t�H���g�ł͋�
			this.Voice = "";						// �f�t�H���g�ł͋�
			this.Window = 1;						// �f�t�H���g�ł͒ʏ�ʒu�ɕ`�悳����
			this.assistant = "";					// �f�t�H���g�ł͋�
			this.NextButton = true;					// �f�t�H���g�ł͕`�悳����
			this.EndButton = false;					// �f�t�H���g�ł͕`�悳���Ȃ�
			this.TopScreen = false;					// �f�t�H���g�ł�(ry
			this.NextState = false;					// �f�t�H���g�ł�(ry
			this.rect = new Rectangle(-1,0,0,0);	// �f�t�H���g�ł�X��-1�ɂ��Ă���
			this.MouseClick = new Point(-1,0);		// �f�t�H���g�ł�X��-1�ɂ��Ă���
			this.ChapterTop = "";					// �f�t�H���g�ł͋�
			this.SPMode = "";						// �f�t�H���g�ł͋�
			this.DrawString = true;					// �f�t�H���g�ł͕`�悳����ݒ�
			this.Fade = new Point(-1, 0);			// �f�t�H���g�ŉ񐔂�-1�ɂ��Ă���
			this.NBRegist = new ArrayList();
			
			// �V�i���I�̃C���X�^���X���Ə�����
			this.text = new string[3];
			for(int i=0; i<this.text.Length; i++) this.text[i] = "";
		}
		
		
		/// <summary>
		/// �^����ꂽ�e�L�X�g���V�i���I�{���ɒǉ����郁�\�b�h
		/// </summary>
		/// <param name="text">�ǉ�����e�L�X�g</param>
		/// <returns>�ǉ��ł������ǂ��� true�Ȃ琬��</returns>
		public bool addText(string text)
		{
			for(int i=0; i<this.text.Length; i++)
			{
				if( this.text[i] == "" )
				{
					this.text[i] = text;
					return true;
				}
			}
			return false;
		}
		
		
		/// <summary>
		/// �^����ꂽ�摜�����X�g�ɒǉ����郁�\�b�h
		/// </summary>
		/// <param name="image">�ǉ�����摜��</param>
		/// <param name="x">�ǉ�����摜��x���W</param>
		/// <param name="y">�ǉ�����摜��y���W</param>
		/// <returns>�ǉ��ł������ǂ��� true�Ȃ琬��</returns>
		public bool addUseImage(string image, int x, int y)
		{
			// ���ɓ����摜�������X�g�ɂ���Βǉ����Ȃ�
			if( this.UseImage.IndexOf(image) != -1 ) return false;
			
			// �摜��,x���W,y���W�̏��Ƀ��X�g�ɓo�^
			this.UseImage.Add(image);
			this.UseImage.Add(x);
			this.UseImage.Add(y);
			
			return true;
		}
		
		
		public bool addNBRegist(string name, int x, int y, string[] image)
		{
			// ���ɓ����o�^��������΃��X�g�ɓo�^���Ȃ�
			if( this.NBRegist.IndexOf(name) != -1) return false;
			
			// �o�^���A���W�A�摜�p�X�̏��Ƀ��X�g�ɓo�^
			this.NBRegist.Add(name);
			this.NBRegist.Add(x);
			this.NBRegist.Add(y);
			this.NBRegist.Add(image);
			
			return true;
		}
	}
}
