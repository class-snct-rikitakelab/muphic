using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// �`�悷��f�[�^���i�[ ���X�g�ɂ��Ďg��
	/// </summary>
	public class DrawData : Base
	{
		public String Image;
		public int x;
		public int y;

		public DrawData(String image, int x, int y)
		{
			this.Image = image;
			this.x = x;
			this.y = y;
		}
	}
}
