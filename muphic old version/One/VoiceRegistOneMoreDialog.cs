using System;
using muphic.One.VoiceDialog;

namespace muphic.One
{
	/// <summary>
	/// VoiceRegistOneMoreDialog �̊T�v�̐����ł��B
	/// </summary>
	public class VoiceRegistOneMoreDialog : Screen
	{
		public OneScreen parent;
		public OneMoreCloseButton close;

		public VoiceRegistOneMoreDialog(OneScreen one)
		{
			parent = one;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			close = new OneMoreCloseButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			DrawManager.Regist(this.ToString(), 236, 251, "image\\one\\Voice\\onemore\\onemoredialog.png");
			DrawManager.Regist(close.ToString(), 670, 450, "image\\one\\Voice\\onemore\\close_off.png", "image\\one\\Voice\\onemore\\close_on.png");
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(close);
		}
	}
}
