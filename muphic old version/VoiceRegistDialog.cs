using System;
using muphic.One.VoiceDialog;

namespace muphic.One
{
	/// <summary>
	/// VoiceDialog の概要の説明です。
	/// </summary>
	public class VoiceRegistDialog : Screen
	{
		public OneScreen parent;
		public StartButton start;
		public muphic.One.VoiceDialog.BackButton back;

		public VoiceRegistDialog(OneScreen one)
		{
			parent = one;
			
//			DrawManager.BeginRegist();
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			start = new StartButton(this);
			back = new muphic.One.VoiceDialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 300, 300, "image\\one\\Voice\\parts\\background.png");
			muphic.DrawManager.Regist("wait", 350, 350, "image\\one\\Voice\\parts\\waiting.png");
			muphic.DrawManager.Regist("load", 350, 350, "image\\one\\Voice\\parts\\loading.png");
			DrawManager.Regist(start.ToString(), 400, 500, "image\\one\\Voice\\button\\start_off.png", "image\\one\\Voice\\button\\start_on.png");
			DrawManager.Regist(back.ToString(), 600, 500, "image\\one\\Voice\\button\\back_off.png", "image\\one\\Voice\\button\\back_on.png");

//			DrawManager.EndRegist();
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(start);
			BaseList.Add(back);
		}
	}
}
