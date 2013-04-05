using System;
using muphic.Story.VoiceDialog;

namespace muphic.Story
{
	/// <summary>
	/// VoiceRegistOneMoreDialog �̊T�v�̐����ł��B
	/// </summary>
	public class VoiceRegistOneMoreDialog : Screen
	{
		public StoryScreen parent;
		public OneMoreCloseButton close;

		public VoiceRegistOneMoreDialog(StoryScreen story)
		{
			parent = story;
			
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
