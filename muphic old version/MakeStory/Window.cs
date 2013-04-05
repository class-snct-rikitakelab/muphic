using System;
using System.Collections;
using System.Drawing;
using muphic;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;
using muphic.Story;
using muphic.Common;

namespace muphic.MakeStory
{
	/// <summary>
	/// Window �̊T�v�̐����ł��B
	/// </summary>
	public class Window : Screen
	{
		//public int now;
		ArrayList ObjList;
		public MakeStoryScreen parent;
		public int backscreen;

		public Window(MakeStoryScreen mss)
		{
			//now = 0;
			ObjList =new ArrayList();
			parent = mss;
			backscreen = 0;
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}

		public override void Draw()
		{
			base.Draw ();
			//�w�i�̕`��
			if(parent.PictureStory.Slide[parent.NowPage].haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.Draw(parent.PictureStory.Slide[parent.NowPage].haikei.ToString(),
					parent.PictureStory.Slide[parent.NowPage].haikei.X, parent.PictureStory.Slide[parent.NowPage].haikei.Y);
			}
			//�z�u���̕`��
			for(int i = 0;i < parent.PictureStory.Slide[parent.NowPage].ObjList.Count;i++)
			{
				Obj o = (Obj)(parent.PictureStory.Slide[parent.NowPage].ObjList[i]);
				DrawManager.DrawCenter((parent.PictureStory.Slide[parent.NowPage].ObjList[i].ToString()), o.X, o.Y);
			}
		}

		/// <summary>
		/// ���݃X���C�h�����y�[�W�܂ł��邩�𒲂ׂ郁�\�b�h
		/// </summary>
		/// <returns>�����Ă��閇���̐�(��Ȃ�0���Ԃ�)</returns>
		private int DecidePage()
		{
			for(int i=9;i>=0;i--)
			{
				Slide s = parent.PictureStory.Slide[i];
				if(s.ObjList.Count == 0 && s.AnimalList.Count == 0 &&
					s.haikei.ToString() == "BGNone")
				{
				}
				else
				{
					return i+1;
				}
			}
			return 0;
		}

		/// <summary>
		/// ���ꉹ�y�̃X���C�h�̋L�O�`���̈���v���r���[��\������悤�Ȃ���
		/// </summary>
		public void PreviewMemorial()
		{
			int maxpage = DecidePage();								//�y�[�W���ő剽����
			Slide slide = parent.PictureStory.Slide[0];
//			if(slide.ObjList.Count == 0)
//			{
//				return;
//			}
			//�w�i�̕`��
			DrawManager.DrawDiv(parent.wind.ToString());
			if(parent.PictureStory.Slide[parent.NowPage].haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.DrawDiv(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
			}
			//�z�u���̕`��
			for(int j = 0;j < slide.ObjList.Count;j++)
			{
				Obj o = (Obj)(slide.ObjList[j]);
				DrawManager.DrawDivCenter((o.ToString()), o.X, o.Y);
			}

			//wind.rect = {x=177, y=262, width=668, height=643}  right=845, bottom=643
			
		}

		/// <summary>
		/// ���ꉹ�y�̃X���C�h���L�O�Ɉ������悤�Ȃ���
		/// </summary>
		public void PrintMemorial()
		{
			int allpage = this.DecidePage();						//�y�[�W�̑��������肷��
			this.WindRect = PointManager.Get(parent.wind.ToString());
			this.WindPrintRect = new Rectangle(100, 100, 824, 568);
			for(int i=0;i<allpage;i++)
			{
				Slide slide = parent.PictureStory.Slide[i];
				if(slide.ObjList.Count == 0 && slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
					break;
				}
				PrintManager.ChangePage(i+1);

				// �y�[�W�������o�^
//				muphic.PrintManager.RegistString("�y�[�W " + (i+1) + " / " + allpage, 830, 690, 16);
				
				// �^�C�g���̓o�^
//				String title = parent.PictureStory.Title;							// ������Ȗ����Ƃ��Ă���
//				if(title == null || title == "") title = "�u�����炵�����̂�����v";// �ۑ��_�C�A���O�̃^�C�g��������Ȃ�΁A�������ŏ���Ɍ��߂�
//				else title = "�u" + title + "�v";									// �w�x������
//				muphic.PrintManager.RegistString("�����߂�", 40, 40, 14);
//				muphic.PrintManager.RegistString(title, 65, 70, 24);

				//wind.rect = {x=177, y=262, width=668, height=381}  right=845, bottom=643
				// �y�[�W�������o�^
				muphic.PrintManager.RegistString("�y�[�W " + (i+1) + " / " + allpage, 177+668-100, 262+381+45, 16);
				
				// ���S��o�^
				DrawManager.Regist("Logo", 730, 35, "image\\ScoreXGA\\logo.png");						// ����p���S
				PrintManager.Regist("Logo", 177+668-200, 262-90);
				// �^�C�g���̓o�^
				String title = parent.PictureStory.Title;							// ������Ȗ����Ƃ��Ă���
				if(title == null || title == "") title = "�u�����炵�����̂�����v";// �ۑ��_�C�A���O�̃^�C�g��������Ȃ�΁A�������ŏ���Ɍ��߂�
				else title = "�u" + title + "�v";									// �w�x������
				muphic.PrintManager.RegistString("�����߂�", -20+177, -60+262, 14);
				muphic.PrintManager.RegistString(title, 5+177, -40+262, 24);

				//���͂̓o�^
				PrintManager.RegistString(Common.CommonTools.StringCenter(slide.Sentence, 30), 117+67, 262+381+10);

				//�w�i�̕`��
				if(slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
				}
				else
				{
					PrintManager.Regist(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
					//PrintMemorialDiv(slide.haikei.ToString(), new Point(slide.haikei.X, slide.haikei.Y), false);
				}
				//�z�u���̕`��
				for(int j = 0;j < slide.ObjList.Count;j++)
				{
					Obj o = (Obj)(slide.ObjList[j]);
					PrintManager.RegistCenter((o.ToString()), o.X, o.Y);
					//PrintMemorialDiv(o.ToString(), new Point(o.X, o.Y), true);
				}
			}
			Rectangle r = PointManager.Get(parent.wind.ToString());
			PrintManager.Print(new Rectangle(r.X-100, r.Y-100, r.Width+200, r.Height+200), true, true);
		}

		Rectangle WindRect;
		Rectangle WindPrintRect;

		/// <summary>
		/// �L�O�ň���̏ꍇ�AWindow�̂�����̂܂ܕ`�悷��ƁA�������̂Ŋg�債�ĕ`�悷�邽��
		/// �̃��\�b�h
		/// </summary>
/*		private void PrintMemorialDiv(String ClassName, Point p, bool isCenter)
		{
			float divX = (float)WindPrintRect.Width / WindRect.Width;
			float divY = (float)WindPrintRect.Height / WindRect.Height;
			p.X = p.X - WindRect.X;
			p.X = (int)(p.X * divX);
			p.X = p.X + WindPrintRect.X;
			p.Y = p.Y - WindRect.Y;
			p.Y = (int)(p.Y * divY);
			p.Y = p.Y + WindPrintRect.Y;
			if(isCenter)
			{
				PrintManager.RegistCenter(ClassName, p.X, p.Y);
			}
			else
			{
				PrintManager.Regist(ClassName, p.X, p.Y);
			}
		}*/

		/// <summary>
		/// ���ꉹ�y�̃X���C�h�̎��ŋ��`���̈���v���r���[��\������悤�Ȃ���
		/// </summary>
		public void PreviewStory()
		{
			Slide slide = parent.PictureStory.Slide[0];
//			if(slide.ObjList.Count == 0)
//			{
//				return;
//			}
			//�w�i�̕`��
			if(parent.PictureStory.Slide[parent.NowPage].haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.DrawDiv(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
			}
			//�z�u���̕`��
			for(int j = 0;j < slide.ObjList.Count;j++)
			{
				Obj o = (Obj)(slide.ObjList[j]);
				DrawManager.DrawDivCenter((o.ToString()), o.X, o.Y);
			}
		}

		/// <summary>
		/// ���ꉹ�y�̃X���C�h�����ŋ��`���ň������悤�Ȃ���
		/// </summary>
		public void PrintStory()
		{	//wind.rect = {x=177, y=262, width=668, height=381}  right=845, bottom=643
			Rectangle r = PointManager.Get(parent.wind.ToString());
			DrawManager.Regist("SlideTitle", r.X, r.Y, "image\\MakeStory\\print\\title.png");
			PrintManager.ChangePage(1);
			PrintManager.Regist("SlideTitle");
			String beforeString = parent.PictureStory.Title;
			if(beforeString == "")
			{
				beforeString = "�����炵�����̂�����";
			}
			String CenterTitle = muphic.Common.CommonTools.StringCenter(beforeString, 15);
			PrintManager.RegistString(CenterTitle, 4+177, 176+262, 44);

			for(int i=0;i<10;i++)
			{
				Slide slide = parent.PictureStory.Slide[i];
				if(slide.ObjList.Count == 0 && slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
					break;
				}
				PrintManager.ChangePage(i+2);
				//�w�i�̕`��
				if(slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
				}
				else
				{
					PrintManager.Regist(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
				}
				//�z�u���̕`��
				for(int j = 0;j < slide.ObjList.Count;j++)
				{
					Obj o = (Obj)(slide.ObjList[j]);
					PrintManager.RegistCenter((o.ToString()), o.X, o.Y);
				}
			}
			PrintManager.Print(PointManager.Get(parent.wind.ToString()), true, true);
		}

		public override void Click(System.Drawing.Point p)
		{
			if(parent.ButtonsMode != muphic.MakeStory.ButtonsMode.None)	//�E�̃{�^���Q�ŉ�����I�����Ă���Έȉ������s����
			{
				Point place = p;
				System.Diagnostics.Debug.WriteLine(place.Y, "�ʒu");
				System.Diagnostics.Debug.WriteLine(place.X, "�ʒu");
				if(place.X == 0 && place.Y == 0)								//DevicePlace���y���O(�������͂����
				{																//�����Ƃ��������)���Ɣ��f����
					return;
				}
				//if(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Clear)//�L�����Z���{�^�����N���b�N����Ă�����
				if(parent.isClear)
				{
					bool b = this.Delete(place.X, place.Y);						//�폜���������s
					System.Diagnostics.Debug.WriteLine(b, "Delete");
				}
				else if((parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Forest)
					||(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.River)
					||(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Town)
					||(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Room))
				{

				}
				else															//�ق��̓������N���b�N����Ă�����
				{
					System.Drawing.Rectangle rec = PointManager.Get(((ObjMode)parent.tsuibi.State).ToString());

					int xl = 177,yt = 262;
					System.Drawing.Rectangle wind = PointManager.Get(parent.wind.ToString());
					if((parent.tsuibi.State != 0)
						&&(place.X-rec.Width/2 > xl)&&(xl+wind.Width > place.X+rec.Width/2)
						&&(place.Y-rec.Height/2 > yt)&&(yt+wind.Height > place.Y+rec.Height/2))
					{
						bool b = this.Insert(place.X, place.Y);						//�}�����������s
						System.Diagnostics.Debug.WriteLine(b, "Insert");
						
						// �`���[�g���A�����s���ŁA����̑ҋ@��Ԃ������ꍇ
						if(b && TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
						{
							// �X�e�[�g�i�s
							parent.parent.tutorialparent.NextState();
						}
					}
					
				}
				
			}
		}

		/// <summary>
		/// �p�[�c��V���ɒǉ�����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <returns>�����������ǂ���</returns>
		private bool Insert(int X, int Y)
		{
			Obj newObj = new Obj(X, Y, parent.tsuibi.State);
			parent.PictureStory.Slide[parent.NowPage].ObjList.Insert(parent.PictureStory.Slide[parent.NowPage].ObjList.Count, newObj);
			return true;
		}

		/// <summary>
		/// �p�[�c���폜����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <returns>�����������ǂ���</returns>
		private bool Delete(int X, int Y)
		{
			for(int i = parent.PictureStory.Slide[parent.NowPage].ObjList.Count;i > 0;i--)
			{
				Obj o = (Obj)parent.PictureStory.Slide[parent.NowPage].ObjList[i-1];
				Rectangle rec = PointManager.Get(o.ToString());
				if((o.X-rec.Width/2 <= X & X <= o.X+rec.Width/2)&&
					(o.Y-rec.Height/2 <= Y & Y <= o.Y+rec.Height/2))
				{
					parent.PictureStory.Slide[parent.NowPage].ObjList.RemoveAt(i-1);
					return true;
				}
			}
			return false;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			parent.tsuibi.Visible = true;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			parent.tsuibi.Visible = false;
		}
	}
}
