using System;
using System.Collections;
using System.Drawing;
using muphic.LinkMake.ScoreParts;
using muphic.Common;

namespace muphic.LinkMake
{
	/// <summary>
	/// Score �̊T�v�̐����ł��B
	/// </summary>
	public class Score : Screen
	{
		//public ArrayList AnimalList;							//�ʂ�BaseList���g���Ă��������A�����͂����ĕʂɐ錾����
		public Animals Animals;
		public LinkMakeScreen parent;
		public const int MaxAnimals_perPage = 12;		//1�y�[�W�������1�̉��K�ɂ�����4�������̍ő吔(�����l)
		public const int MaxAnimals = 24;				//1�y�[�W�������1�̉��K�ɂ�����ő吔(�����l�A�����������Kugiri=2�ɂ����ʗp���Ȃ����Ƃɒ���)
		public int nowPlace;							//���ݕ\�����̈ʒu
		int defaultPlace;								//��Đ����̕\���ʒu(�Đ��I���������̈ʒu�ɖ߂�)
		public bool isPlay;								//�Đ������ǂ�����\���t���O
		//�����Ĕz�u�p�ϐ�
		public int beginAnimalNum;						//�h���b�O�J�n���ɑI������Ă��������̗v�f�ԍ�(�I������Ă��Ȃ��Ȃ�-1)
		
		public RightScrollButton right;
		public LeftScrollButton left;
		public ScrollBar bar;
		public ClearButton clear;
		public AllClearButton allclear;
		public SharpButton sharp;

		public SignBoard signboard;

		public Score(LinkMakeScreen one)
		{
			parent = one;
			Animals = new Animals();
			defaultPlace = nowPlace = 0;
			this.isPlay = false;

			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			right = new RightScrollButton(this);
			left = new LeftScrollButton(this);
			bar = new ScrollBar(this);
			clear = new ClearButton(this);
			allclear = new AllClearButton(this);
			sharp = new SharpButton(this);

			signboard = new SignBoard(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			DrawManager.Regist("Sheep", 0, 0, "image\\one\\button\\animal\\sheep\\sheep.png");
			DrawManager.Regist("AnimalCheck_Sheep", 0, 0, "image\\one\\focus\\focus_sheep.png");
			muphic.DrawManager.Regist(right.ToString(), 844, 695, "image\\one\\parts\\scroll\\next.png");
			muphic.DrawManager.Regist(left.ToString(), 116, 695, "image\\one\\parts\\scroll\\prev.png");
			DrawManager.Regist(bar.ToString(), 145, 693, "image\\one\\parts\\scroll\\bars.png");
			muphic.DrawManager.Regist(clear.ToString(), 287, 194, "image\\one\\button\\score\\clear_off.png", "image\\one\\button\\score\\clear_on.png");
			muphic.DrawManager.Regist(allclear.ToString(), 159, 194, "image\\one\\button\\score\\allclear_off.png", "image\\one\\button\\score\\allclear_on.png");
			muphic.DrawManager.Regist(sharp.ToString(), 578, 195, "image\\one\\button\\score\\sharp_off.png", "image\\one\\button\\score\\sharp_on.png");
			muphic.DrawManager.Regist("Sign", 1050, 0, "image\\one\\parts\\signboard.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(right);
			BaseList.Add(left);
			BaseList.Add(bar);
			BaseList.Add(clear);
			BaseList.Add(allclear);
			//BaseList.Add(sharp);
			BaseList.Add(signboard);
		}

		public override void Draw()
		{
			base.Draw ();
			if(!Visible)
			{
				return;
			}

			bool isPlayFinished = Animals.Draw(this.nowPlace, parent.tempo_l.TempoMode, this.isPlay);//�������̕`��
			if(isPlayFinished)
			{
				this.PlayStop();																//�����Đ��I���t���O���������Ȃ�Đ����I������
			}
		}


		bool isRightClick = true;							//�E�N���b�N�폜����������Ƃ���true
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(this.isPlay)								//�Đ����A�y�����̕ҏW��Ƃ͋����Ȃ�
			{
				return;
			}

			Point place = muphic.Common.ScoreTools.DecidePlace(p);			//�y�����ł̈ʒu�Ɖ��K�����肷��
			if(place.X == 0 && place.Y == 0)								//DebicePlace���y���O(�������͂����
			{																//�����Ƃ��������)���Ɣ��f����
				return;														//��A�A��
			}
			place.X += this.nowPlace;										//���݂̃I�t�Z�b�g��ǉ�
			System.Diagnostics.Debug.WriteLine(place.Y, "���K");
			System.Diagnostics.Debug.WriteLine(place.X, "�ʒu");
			if(isRightClick && Animals.Search(place.X, place.Y) != -1 && parent.parent.parent.e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				Animals.Delete(place.X, place.Y);
			}
			else
				if(parent.linkmakes.NowClick != muphic.LinkMake.LinkMakeButtonsClickMode.None)		//�E�̃{�^���Q�ŉ�����I�����Ă���Έȉ������s����
			{
				if(parent.linkmakes.NowClick == muphic.LinkMake.LinkMakeButtonsClickMode.Cancel)//�L�����Z���{�^�����N���b�N����Ă�����
				{
					bool b = Animals.Delete(place.X, place.Y);					//�폜���������s
					System.Diagnostics.Debug.WriteLine(b, "Delete");
				}
				else														//�ق��̓������N���b�N����Ă�����
				{
					bool b = Animals.InsertL(place.X, place.Y, parent.linkmakes.NowClick.ToString());//�}�����������s
					System.Diagnostics.Debug.WriteLine(b, "Insert");
					
					// �`���[�g���A�����s���ŁA����̑ҋ@��Ԃ������ꍇ
					if(TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
					{
						// �X�e�[�g�i�s
						parent.parent.tutorialparent.NextState();
					}
				}
			}
			//else																//���̃{�^�����I������Ă��Ȃ��Ƃ��́A������I����Ԃɂ���
			//{
			/*int CheckedAnimalNum;
			CheckedAnimalNum = Animals.ClickAnimal(place.X, place.Y);		//������I����Ԃɂ���
			if(CheckedAnimalNum == -1)										//�����A���̓������I�����Ă��Ȃ����
			{
				this.clear.Visible = false;									//�߂�{�^����\�����Ȃ�
			}
			else															//������I�����Ă����
			{
				this.clear.Visible = true;									//�߂�{�^����\������

			}*/
			//}
		}
		
		public override void DragBegin(Point begin)
		{
			base.DragBegin (begin);
			if(true)//if(parent.ones.NowClick == muphic.One.OneButtonsClickMode.None)		//�E�̃{�^���Q�ŉ�����I�����Ă��Ȃ��Ƃ��Ɍ���
			{
				Point place = muphic.Common.ScoreTools.DecidePlace(begin);		//�y�����ł̈ʒu�Ɖ��K�����肷��
				place.X += this.nowPlace;										//���݂̃I�t�Z�b�g��ǉ�
				this.beginAnimalNum = Animals.Search(place.X, place.Y);			//���ݑI������Ă��铮���̗v�f�ԍ����i�[���Ă���
				parent.tsuibi.Visible = false;
			}
			else
			{
				this.beginAnimalNum = -1;
			}
		}

		public override void DragEnd(Point begin, Point p)
		{
			base.DragEnd (begin, p);
			this.beginAnimalNum = -1;
			parent.tsuibi.Visible = true;
		}


		public override void Drag(Point begin, Point p)
		{
			base.Drag (begin, p);
			if(this.beginAnimalNum != -1)										//�h���b�O�J�n���ɉ�������̓�����I�����Ă�����
			{
				int OldPlace = Animals[this.beginAnimalNum].place;
				int OldCode = Animals[this.beginAnimalNum].code;
				Point now = muphic.Common.ScoreTools.DecidePlace(p);
				if(now.X == 0 && now.Y == 0)
				{
					return;														//�y���O�Ƀh���b�O���悤�Ƃ��Ă͂����܂���
				}
				now.X += this.nowPlace;											//���݂̃I�t�Z�b�g��ǉ�
				int NewNum = Animals.ReplaceL(OldPlace, OldCode, now.X, now.Y, true);
				if(NewNum != -1)
				{
					this.beginAnimalNum = NewNum;								//�Ĕz�u�������ł����beginNum�ɓ���Ƃ�
				}
			}
		}


		public void PlayFirst()
		{
			this.nowPlace = 0;													//�y�[�W�������I�ɍŏ��ɖ߂�
			Animals.PlayOffset = 0;												//�Đ��r���������ꍇ�A�ŏ��ɖ߂�
			for(int i=0;i<Animals.AnimalList.Count;i++)
			{
				((Animal)Animals[i]).Visible = true;							//�Đ��r���������ꍇ���l����
			}																	//visible�𕜋A������
			this.PlayStart();
		}

		public void PlayStart()
		{
			if(this.isPlay)
			{
				return;
			}
			if(Animals.AnimalList.Count == 0)
			{
				PlayStop();
				return;
			}
			Animals.PlayOffset = 0;												//�I�t�Z�b�g�����ɖ߂�
			parent.score.right.Visible = false;									//�X�N���[���{�^���͉����Ȃ��悤�ɂ��Ă���
			parent.score.left.Visible = false;
			this.isPlay = true;
		}

		public void PlayStop()
		{
			for(int i=0;i<Animals.AnimalList.Count;i++)
			{
				((Animal)Animals[i]).Visible = true;							//visible�𕜋A������
			}
			parent.score.right.Visible = true;									//�X�N���[���{�^�������A������
			parent.score.left.Visible = true;
			parent.startstop.State = 0;
			this.nowPlace = this.defaultPlace;									//�͂��߂����������ꍇ�̃y�[�W���A
			this.isPlay = false;
		}

		public void RightScroll()
		{
			ChangeScroll(this.nowPlace + 4 * muphic.Common.ScoreTools.Kugiri);
		}

		public void LeftScroll()
		{
			ChangeScroll(this.nowPlace - 4 * muphic.Common.ScoreTools.Kugiri);
		}

		/// <summary>
		/// ��ʂ��X�N���[������Ƃ��ɌĂԃ��\�b�h
		/// �����ŁA��ʂ̐؂�ւ��ƁA�X�N���[���̒l�̐ݒ������Ă���
		/// </summary>
		/// <param name="NewPlace">�V�����ݒ肷��y���̈ʒu</param>
		public void ChangeScroll(int NewPlace)
		{
			if(NewPlace > 60)
			{
				NewPlace = 56;													//���߂��Ƃɋ�؂邩��100�ɂ͂Ȃ�Ȃ�
			}
			else if(NewPlace < 0)
			{
				NewPlace = 0;
			}
			this.defaultPlace = this.nowPlace = NewPlace;
			bar.Percent = (float)NewPlace / 56 * 100;							//�X�N���[���̏ꏊ��V���ɐݒ�
		}

		/// <summary>
		/// ��ʂ��X�N���[������Ƃ��ɌĂԃ��\�b�h
		/// ������́A�X�N���[���o�[�ɂ���ĕύX���ꂽ�Ƃ��ɌĂԗp�̃��\�b�h
		/// </summary>
		/// <param name="Percent">�X�N���[���o�[�̃p�[�Z���e�[�W</param>
		public void ChangeScroll(float Percent)
		{
			float New = Percent / 100 * 8;										//�͈͂�0�`96�Ȃ̂ŁA�܂�Percent��0�`12�͈̔͂ɕϊ�����
			int NewPlace = (int)(New + 0.5) * 8;								//�l�̌ܓ��̂��߁A+0.5���Ă���int�^�ɒ���
			ChangeScroll(NewPlace);												//���߂�ChangeScroll
		}
/*
		private bool inScore(Point p)
		{
			if(109 <= p.X && p.X <= 109+555)
			{
				if(181 <= p.Y && p.Y <= 181+410)
				{
					return true;
				}
			}
			return false;
		}*/
	}
}
