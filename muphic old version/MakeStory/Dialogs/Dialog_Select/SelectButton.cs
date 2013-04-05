using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// SelectButton �̊T�v�̐����ł��B
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
			String fname = parent.GetSelectFileName(num);								//1�ԏ�̃t�@�C�����̗v�f�ԍ��{���������Ԗڂ�(0�`3)
			if(fname == "")																//����Ȃ牽�����Ȃ�
			{
				return;
			}
			sfr.Read(fname);
			parent.parent.parent.ChangeSlide0();
			parent.parent.parent.thumb.init();
			parent.parent.back.Click(System.Drawing.Point.Empty);						//�o�b�N�{�^�������������Ƃɂ��āA�_�C�A���O�����
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
