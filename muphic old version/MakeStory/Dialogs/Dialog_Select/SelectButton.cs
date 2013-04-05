using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// SelectButton の概要の説明です。
	/// </summary>
	public class SelectButton : Base
	{
		public SelectButtons parent;
		public int num;
		public SelectButton(SelectButtons sbs, int num)
		{
			parent = sbs;
			this.num = num;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			StoryFileReader sfr = new StoryFileReader(parent.parent.parent.PictureStory);
			String fname = parent.GetSelectFileName(num);								//1番上のファイル名の要素番号＋自分が何番目か(0～3)
			if(fname == "")																//からなら何もしない
			{
				return;
			}
			sfr.Read(fname);
			parent.parent.parent.ChangeSlide0();
			parent.parent.parent.thumb.init();
			parent.parent.back.Click(System.Drawing.Point.Empty);						//バックボタンを押したことにして、ダイアログを閉じる
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}

		public override string ToString()
		{
			return "SelectButton" + num;
		}

	}
}
