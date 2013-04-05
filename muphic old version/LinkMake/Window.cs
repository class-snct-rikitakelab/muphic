using System;
using muphic.LinkMake.WindowParts;

namespace muphic.LinkMake
{
	public enum StoryWindowMode{Title};							//Titleだけだけど…
	/// <summary>
	/// Window の概要の説明です。
	/// </summary>
	public class Window : Screen
	{
		LinkMakeScreen parent;
		public Title title;
		public Window(LinkMakeScreen one)
		{
			parent = one;
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			title = new Title(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(title.ToString(), 763, 50, "image\\one\\parts\\title.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			//BaseList.Add(title);
		}
	}
}
