using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// Score �̊T�v�̐����ł��B
	/// </summary>
	public class Score : Base
	{
		public string sname;			// ����
		public int[] code = new int[4];	// ���K �z��
		public double length;			// ���̒����i�����Ƃ��l���Ƃ�
		public int  tie;				// �^�C���ǂ��� 1:�^�C(�J�n) 2:�^�C(�I��)
		public int chord;				// �a�����(�����̐�)

		public Score()
		{
			// �Ƃ肠�������K��-1�ŏ�����
			for(int i=0; i<4; i++) this.code[i] = -1;
			// �������̉��̒�����8���ɂ��Ă��� �K�v�Ȃ��Œ���
			this.length = 0.5;
			this.tie = 0;
			this.chord = 1;
		}

		// ���K��ǉ�����
		// �P�Ԗڂ��珇�Ɍ��Ă����A�󂢂Ă����(-1��������)�����ɒǉ�
		public int AddCode(int code)
		{
			if(this.code[0] < 0) this.code[0] = code;
			else if(this.code[1] < 0) this.code[1] = code;
			else if(this.code[2] < 0) this.code[2] = code;
			else return 0;
			return 1;
		}


		public override string ToString()
		{
			return this.sname;
		}
	}
}
