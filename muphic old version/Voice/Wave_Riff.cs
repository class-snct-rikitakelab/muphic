using System;

namespace muphic.tag
{
	/// <summary>
	/// Wave_Riff �̊T�v�̐����ł��B
	/// </summary>
	public class Wave_Riff
	{
		public String aIdentity;				//�����"RIFF"�������ĂȂ��Ƃ�������
		public int bFileSize;					//����ȍ~�̃t�@�C���T�C�Y(���ۂ̃T�C�Y-8)
		public String cRiffKind;				//RIFF�̎�ނ�����킷���ʎq�B"WAVE"����Ȃ��Ƃ�������
		public Chunk[] chunk;

		public Wave_Riff()
		{
		}

	}
}
