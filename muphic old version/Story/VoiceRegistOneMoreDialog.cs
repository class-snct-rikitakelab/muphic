using System;
using muphic.Story.VoiceDialog;

namespace muphic.Story
{
	/// <summary>
	/// VoiceRegistOneMoreDialog の概要の説明です。
	/// </summary>
	public class VoiceRegistOneMoreDialog : Screen
	{
		public StoryScreen parent;
		public OneMoreCloseButton close;

		public VoiceRegistOneMoreDialog(StoryScreen story)
		{
			parent = story;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			close = new OneMoreCloseButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			DrawManager.Regist(this.ToString(), 236, 251, "image\\one\\Voice\\onemore\\onemoredialog.png");
			DrawManager.Regist(close.ToString(), 670, 450, "image\\one\\Voice\\onemore\\close_off.png", "image\\one\\Voice\\onemore\\close_on.png");
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(close);
		}
	}
}
