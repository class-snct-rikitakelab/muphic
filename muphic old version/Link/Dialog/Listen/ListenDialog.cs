using System;
using System.Drawing;


namespace muphic.Link.Dialog.Listen
{
	/// <summary>
	/// SelectDialog �̊T�v�̐����ł��B
	/// </summary>
	public class ListenDialog : Screen
	{
		public LinkScreen parent;
		
		public ListenBackButton back;
		public ListenStartStopButton listen;
		public ListenSelect select;
		public ListenBar bar;

		public ListenDialog(LinkScreen link)
		{
			this.parent = link;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			back = new ListenBackButton(this);
			listen = new ListenStartStopButton(this);
			select = new ListenSelect(this);
			bar = new ListenBar(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 124+112, 167+84, "image\\link\\dialog\\listen\\dialog.png");
			muphic.DrawManager.Regist(back.ToString(), 558+112, 368+84, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");
			muphic.DrawManager.Regist(listen.ToString(), 340+112, 368+84, "image\\link\\dialog\\listen\\start_off.png", "image\\link\\dialog\\listen\\start_on.png");
			muphic.DrawManager.Regist(select.ToString(), 375+112, 234+84, new String[7]
				{
					"image\\link\\dialog\\listen\\none.png", "image\\link\\dialog\\listen\\none.png",
					"image\\link\\dialog\\listen\\mondai2.png", "image\\link\\dialog\\listen\\mondai3.png",
					"image\\link\\dialog\\listen\\mondai4.png", "image\\link\\dialog\\listen\\mondai5.png",
					"image\\link\\dialog\\listen\\mondai6.png"
				});			
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(back);
			BaseList.Add(listen);
			BaseList.Add(select);
			BaseList.Add(bar);
		}

		//		public override void MouseMove(System.Drawing.Point p)
		//		{
		//			base.MouseMove (p);
		//		}

		//		public override void Draw()
		//		{
		//			base.Draw ();
		//		}

	}
}