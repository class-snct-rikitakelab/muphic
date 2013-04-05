using System;
using muphic.One.VoiceDialog;

namespace muphic.One
{
	/// <summary>
	/// VoiceRegistOneMoreDialog の概要の説明です。
	/// </summary>
	public class VoiceRegistOneMoreDialog : Screen
	{
		public OneScreen parent;
		public OneMoreCloseButton close;

		public VoiceRegistOneMoreDialog(OneScreen one)
		{
			parent = one;
			
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
