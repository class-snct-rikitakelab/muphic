//using System;
//
//namespace muphic.Link.Dialog.Select
//{
//	/// <summary>
//	/// QuestionButton の概要の説明です。
//	/// </summary>
//	public class Question02Button : Base
//	{
//		public SelectButtons parent;
//		public Question02Button(SelectButtons select)
//		{
//			parent = select;
//		}
//
//		public override void Click(System.Drawing.Point p)
//		{
//			base.Click (p);
//			if(parent.NowClick != muphic.Link.Dialog.Select.SelectButtonsClickMode.Ques02)
////			{
////				parent.NowClick = muphic.Link.Dialog.Select.SelectButtonsClickMode.None;	
////				this.State = 0;	
////			}
////			else															
//			{
//				parent.NowClick = muphic.Link.Dialog.Select.SelectButtonsClickMode.Ques02;
//				parent.parent.parent.score.AnimalList.Clear();
//				for (int i = 0; i < parent.parent.parent.score.putFlag.Length; i++) parent.parent.parent.score.putFlag[i] = false;
//				this.State = 1;	
//				parent.parent.parent.Screen = new muphic.Link.Dialog.Listen.ListenDialog(parent.parent.parent);
//			}
//		}
//	}
//}