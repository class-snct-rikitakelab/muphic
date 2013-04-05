//using System;
//using muphic.Link.RightButtons;
//
//namespace muphic.Link.Dialog.Select
//{
//	public enum SelectButtonsClickMode {None, Ques01, Ques02, Ques03, Ques04, Ques05, Ques06};
//	/// <summary>
//	/// OneButtons の概要の説明です。
//	/// </summary>
//	public class SelectButtons : Screen
//	{
//		public SelectDialog parent;
//		private SelectButtonsClickMode nowClick;
//		private Question01Button ques01;
//		private Question02Button ques02;
//		private Question03Button ques03;
//		private Question04Button ques04;
//		private Question05Button ques05;
//		private Question06Button ques06;
//
//		public SelectButtonsClickMode NowClick
//		{
//			get
//			{
//				return nowClick;
//			}
//			set
//			{
//				nowClick = value;
//				if(value == SelectButtonsClickMode.None)
//				{
//					parent.parent.QuestionNum = 0;
//				}
//				else
//				{
//					switch(value)
//					{
//						case SelectButtonsClickMode.Ques01:
//							parent.parent.QuestionNum = 1;
//							break;
//						case SelectButtonsClickMode.Ques02:
//							parent.parent.QuestionNum = 2;
//							break;
//						case SelectButtonsClickMode.Ques03:
//							parent.parent.QuestionNum = 3;
//							break;
//						case SelectButtonsClickMode.Ques04:
//							parent.parent.QuestionNum = 4;
//							break;
//						case SelectButtonsClickMode.Ques05:
//							parent.parent.QuestionNum = 5;
//							break;
//						case SelectButtonsClickMode.Ques06:
//							parent.parent.QuestionNum = 6;
//							break;
//						default:
//							parent.parent.QuestionNum = 0;
//							break;
//					}
//				}
//			}
//		}
//
//		public SelectButtons(SelectDialog dia)
//		{
//			parent = dia;
//	
//			///////////////////////////////////////////////////////////////////
//			//部品のインスタンス化
//			///////////////////////////////////////////////////////////////////
//			ques01 = new Question01Button(this);
//			ques02 = new Question02Button(this);
//			ques03 = new Question03Button(this);
//			ques04 = new Question04Button(this);
//			ques05 = new Question05Button(this);
//			ques06 = new Question06Button(this);
//
//
//			///////////////////////////////////////////////////////////////////
//			//部品のテクスチャ・座標の登録
//			///////////////////////////////////////////////////////////////////
//
//			muphic.DrawManager.Regist(ques01.ToString(), 243+112, 302+125, "image\\link\\dialog\\select\\button\\mondai1_off.png", "image\\link\\dialog\\select\\button\\mondai1_on.png");
//			muphic.DrawManager.Regist(ques02.ToString(), 350+112, 302+125, "image\\link\\dialog\\select\\button\\mondai2_off.png", "image\\link\\dialog\\select\\button\\mondai2_on.png");
//			muphic.DrawManager.Regist(ques03.ToString(), 457+112, 302+125, "image\\link\\dialog\\select\\button\\mondai3_off.png", "image\\link\\dialog\\select\\button\\mondai3_on.png");
//			muphic.DrawManager.Regist(ques04.ToString(), 243+112, 357+125, "image\\link\\dialog\\select\\button\\mondai4_off.png", "image\\link\\dialog\\select\\button\\mondai4_on.png");
//			muphic.DrawManager.Regist(ques05.ToString(), 350+112, 357+125, "image\\link\\dialog\\select\\button\\mondai5_off.png", "image\\link\\dialog\\select\\button\\mondai5_on.png");
//			muphic.DrawManager.Regist(ques06.ToString(), 457+112, 357+125, "image\\link\\dialog\\select\\button\\mondai6_off.png", "image\\link\\dialog\\select\\button\\mondai6_on.png");
//
//			///////////////////////////////////////////////////////////////////
//			//部品の画面への登録
//			///////////////////////////////////////////////////////////////////
//			BaseList.Add(ques01);
//			BaseList.Add(ques02);
//			BaseList.Add(ques03);
//			BaseList.Add(ques04);
//			BaseList.Add(ques05);
//			BaseList.Add(ques06);
//
//			switch (parent.parent.QuestionNum)
//			{
//				case 1:
//					ques01.State = 1;
//					break;
//				case 2:
//					ques02.State = 1;
//					break;
//				case 3:
//					ques03.State = 1;
//					break;
//				case 4:
//					ques04.State = 1;
//					break;
//				case 5:
//					ques05.State = 1;
//					break;
//				case 6:
//					ques06.State = 1;
//					break;
//				default:
//					break;
//			}
//		}
//
//		public override void Click(System.Drawing.Point p)
//		{
//			for(int i=0;i<BaseList.Count;i++)
//			{
//				((Base)BaseList[i]).State = 0;							//本来のクリック処理を行う前に
//			}															//すべての要素をクリックしていない状態に戻す
//			base.Click (p);
//		}
//
//	}
//}
