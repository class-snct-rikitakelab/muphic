//using System;
//using muphic.Link.Dialog.Select;
//
//namespace muphic.Link.Dialog.Select
//{
//	/// <summary>
//	/// SelectDialog の概要の説明です。
//	/// </summary>
//	public class SelectDialog : Screen
//	{
//		public LinkScreen parent;
//		public SelectButtons sbs;
//		public UpperScrollButton upper;
//		public LowerScrollButton lower;
//		public muphic.Link.Dialog.Select.BackButton back;
//
//		public SelectDialog(LinkScreen link)
//		{
//			parent = link;
//			
//			///////////////////////////////////////////////////////////////////
//			//部品のインスタンス化
//			///////////////////////////////////////////////////////////////////
//			sbs = new SelectButtons(this);
//			upper = new UpperScrollButton(this);
//			lower = new LowerScrollButton(this);
//			back = new muphic.Link.Dialog.Select.BackButton(this);
//
//			///////////////////////////////////////////////////////////////////
//			//部品のテクスチャ・座標の登録
//			///////////////////////////////////////////////////////////////////
//			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\link\\dialog\\select\\dialog_select_bak.png");
//			muphic.DrawManager.Regist(sbs.ToString(), 230, 290, "image\\link\\dialog\\select\\dialog_nokosu.png");
//			muphic.DrawManager.Regist(upper.ToString(), 633, 388, "image\\link\\dialog\\select\\scroll_up.png");
//			muphic.DrawManager.Regist(lower.ToString(), 635, 470, "image\\link\\dialog\\select\\scroll_down.png");
//			muphic.DrawManager.Regist(back.ToString(), 680, 435, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");
//
//			///////////////////////////////////////////////////////////////////
//			//部品の画面への登録
//			///////////////////////////////////////////////////////////////////
//			BaseList.Add(sbs);
//			BaseList.Add(upper);
//			BaseList.Add(lower);
//			BaseList.Add(back);
//		}
//		
//		public override void Draw()
//		{
//			base.Draw();
//		}
//	}
//}
