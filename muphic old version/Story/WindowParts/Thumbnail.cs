using System;
using System.Drawing;

namespace muphic.Story.WindowParts
{
	/// <summary>
	/// Thumbnail �̊T�v�̐����ł��B
	/// </summary>
	public class Thumbnail : Screen
	{
		public Window parent;
		int num;
//		public SlideLayout layout;

		public Thumbnail(Window window, int num)
		{
			parent = window;
			this.num = num;
/*			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			layout = new SlideLayout(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(layout.ToString(), 797, 38, "image\\story\\button\\null.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(layout);
			
			///////////////////////////////////////////////////////////////////
			//�T���l�C���̃Z�b�g
			///////////////////////////////////////////////////////////////////
			this.SetThumbnail(num);*/
		}

		public override void Draw()
		{
			base.Draw ();

			Rectangle src = PointManager.Get(parent.parent.parent.wind.ToString());
//			src.X -= 5;src.Y -= 5;
//			src.Width += 10;src.Height += 10;
			Rectangle dist = PointManager.Get(parent.ToString());
//			dist.X = x;
//			dist.Y = y;
			DrawManager.Change(src,dist);

			MakeStory.Slide slide = parent.parent.parent.PictureStory.Slide[this.num];
			//�w�i�̕`��
			if(slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.DrawDiv(slide.haikei.ToString(),
					slide.haikei.X, slide.haikei.Y);
			}
			//�z�u���̕`��
			for(int i = 0;i < slide.ObjList.Count;i++)
			{
				MakeStory.Obj o = (MakeStory.Obj)(slide.ObjList[i]);
				DrawManager.DrawDivCenter((slide.ObjList[i].ToString()), o.X, o.Y);
			}
		}



/*
		/// <summary>
		/// �T���l�C����ݒ肷��Ƃ��Ɏg�p���郁�\�b�h
		/// </summary>
		/// <param name="num">�ݒ肷��̃T���l�C���̃y�[�W</param>
		public void SetThumbnail(int num)
		{
			String snum = this.easyFormat(num);								//������2���Ńt�H�[�}�b�g����
			muphic.DrawManager.Regist(layout.ToString(), 797, 38, snum + ".png");//�T���l�C����o�^����
		}

		/// <summary>
		/// �{���ɊȒP�ȃt�H�[�}�b�g�����B������1���Ȃ�擪��0�����ċ����I��2���ɂ���B3���ȏ�̏ꍇ��00�ɂ���B
		/// </summary>
		/// <param name="num"></param>
		private String easyFormat(int num)
		{
			String s;
			if(num < 0)
			{
				s = "00";
			}
			else if(num < 10)
			{
				s = "0" + num;
			}
			else if(num < 100)
			{
				s = num.ToString();
			}
			else
			{
				s = "00";
			}
			return s;
		}*/
	}
}
