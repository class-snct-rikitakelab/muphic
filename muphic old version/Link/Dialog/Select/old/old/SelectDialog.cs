//using System;
//using System.Drawing;
//
//
//namespace muphic.Link.Dialog.Select
//{
//	/// <summary>
//	/// SelectDialog �̊T�v�̐����ł��B
//	/// </summary>
//	public class SelectDialog : Screen
//	{
//		public LinkScreen parent;
//		
//		public SelectBackButton back;
//		public Select.SelectButtons button;
//		//public SelectPattern pattern;
//
//		public SelectDialog(LinkScreen link)
//		{
//			this.parent = link;
//			
//			//this.Point = new Point(0,0);
//			///////////////////////////////////////////////////////////////////
//			//���i�̃C���X�^���X��
//			///////////////////////////////////////////////////////////////////
//			back = new SelectBackButton(this);
//			button = new SelectButtons(this);
//
//
//			//question = new QuestionPattern("data\\QuesPatt.mdm");
//
//			///////////////////////////////////////////////////////////////////
//			//���i�̃e�N�X�`���E���W�̓o�^
//			///////////////////////////////////////////////////////////////////
//			muphic.DrawManager.Regist(this.ToString(), 124+112, 167+125, "image\\link\\dialog\\select\\dialog.png");
//			muphic.DrawManager.Regist(back.ToString(), 558+112, 350+125, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");
//			muphic.DrawManager.Regist(button.ToString(), 245+112, 304+125, "image\\link\\dialog\\select\\buttons.png");
//			
//			///////////////////////////////////////////////////////////////////
//			//���i�̉�ʂւ̓o�^
//			///////////////////////////////////////////////////////////////////
//			BaseList.Add(back);
//			BaseList.Add(button);
//		}
//
////		public override void MouseMove(System.Drawing.Point p)
////		{
////			base.MouseMove (p);
////		}
//
////		public override void Draw()
////		{
////			base.Draw ();
////		}
//
//	}
//}