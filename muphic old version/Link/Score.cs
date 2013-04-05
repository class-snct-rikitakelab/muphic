using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic.Link
{
	/// <summary>
	/// Score �̊T�v�̐����ł��B
	/// </summary>
	public class Score : Screen
	{
		public ArrayList AnimalList;							//�ʂ�BaseList���g���Ă��������A�����͂����ĕʂɐ錾����
		public LinkScreen parent;
		public const int AnimalWidth = 76;				//�����̍ő剡��
		public const int Kugiri = 2;					//�����̋�؂�B1����4���̂݁A2����8���݂̂ɂȂ�B
		//public const int MaxAnimals_perPage = 7;		//1�y�[�W�������1�̉��K�ɂ�����4�������̍ő吔(�����l)
		public int nowPlace;									//���ݕ\�����̈ʒu
		int defaultPlace;								//��Đ����̕\���ʒu(�Đ��I���������̈ʒu�ɖ߂�)
		int PlayOffset;									//�Đ����̃I�t�Z�b�g�B�s�N�Z���P��
		public bool isPlay;								//�Đ������ǂ�����\���t���O

		private muphic.HouseLight houselight;


		//public ArrayList AnimalList;							//�ʂ�BaseList���g���Ă��������A�����͂����ĕʂɐ錾����
		public Animals Animals;
		public const int MaxAnimals_perPage = 12;		//1�y�[�W�������1�̉��K�ɂ�����4�������̍ő吔(�����l)
		public const int MaxAnimals = 24;				//1�y�[�W�������1�̉��K�ɂ�����ő吔(�����l�A�����������Kugiri=2�ɂ����ʗp���Ȃ����Ƃɒ���)

		public bool answerCheckFlag = false;

		public int scoreLength = 0;
		public int nowScore = 0;
		public int tempo = 3;

		public bool[,] ribbon;

		public bool[] putFlag;

		public ScrollBar bar;

		public int barNum;

		public Score(LinkScreen link)
		{
			parent = link;
			AnimalList = new ArrayList();
			bar = new ScrollBar(this);
			defaultPlace = nowPlace = 0;
			this.isPlay = false;


			DrawManager.Regist("Sheep01", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_01.png");
			DrawManager.Regist("Sheep02", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_02.png");
			DrawManager.Regist("Sheep03", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_03.png");
			DrawManager.Regist("Sheep04", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_04.png");
			DrawManager.Regist("Sheep05", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_05.png");
			DrawManager.Regist("Sheep06", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_06.png");
			DrawManager.Regist("Sheep07", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_07.png");
			DrawManager.Regist("Sheep08", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_08.png");
			DrawManager.Regist("Sheep09", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_09.png");
			DrawManager.Regist("Sheep10", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_10.png");

            DrawManager.Regist("ribbon1", 0, 0, "image\\link\\parts\\ribbon\\blue.png");
			DrawManager.Regist("ribbon2", 0, 0, "image\\link\\parts\\ribbon\\pink.png");
			DrawManager.Regist("ribbon3", 0, 0, "image\\link\\parts\\ribbon\\green.png");
			DrawManager.Regist("ribbon4", 0, 0, "image\\link\\parts\\ribbon\\purple.png");

			DrawManager.Regist(bar.ToString(), 145, 693, "image\\one\\parts\\scroll\\bars.png");

			BaseList.Add(bar);

			ribbon = new bool[10, 4];
			putFlag = new bool[100];
			for (int i = 0; i < 100; i++) putFlag[i] = false;
			scoreLength = 0;
			nowScore = 0;
			barNum = 0;

			houselight = new muphic.HouseLight();

		}

		/// <summary>
		/// ���݂̊y�����ł̑��ΓI�Ȉʒu������o��
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Point PointtoScoreRelative(Point p)
		{
			Point pp = muphic.Common.ScoreTools.PointtoScore(p);				//�܂���ΓI�Ȉʒu������o���B
			pp.X += this.nowPlace;									//���ݕ\�����Ă�����W�̈�ԍ����̒l�𑫂�
			return pp;
		}

		/// <summary>
		/// ���݂̊y�����ł̑��ΓI�ȍ��W������o��
		/// </summary>
		/// <param name="place"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public Point ScoretoPointRelative(int place, int code)
		{
			return muphic.Common.ScoreTools.ScoretoPoint(place-this.nowPlace, code);			//���Ǒ��ΓI�Ȉʒu������o���̂�
			//���ݕ\�����Ă�����W�̈�ԍ��[�̒l������
		}

		public override void Draw()
		{
			base.Draw ();
			houselight.Draw();
			if(!Visible)
			{
				return;
			}

			if (!this.answerCheckFlag)
			{
				if(this.isPlay)
				{
					DrawPlaying();
				}
				else
				{
					DrawNotPlaying();
				}
			}
			else
			{
				AnswerCheck();
			}
		}

		/// <summary>
		/// �Đ����̕`�揈��
		/// </summary>
		private void DrawPlaying()
		{
			int count;
			// �`���[�g���A�����s����FrameCount�̎Q�ƕ��@���ς��܂�
			if(muphic.Common.TutorialStatus.getIsTutorial()) count = this.parent.parent.tutorialparent.parent.parent.parent.FrameCount;
			else count = parent.parent.parent.FrameCount;
			
			//int count = parent.parent.parent.FrameCount;
			//int tempo = parent.tempo.TempoMode;
			this.PlayOffset += tempo;									//�e���|�̕������I�t�Z�b�g�𑫂��Ƃ�
			for(int i = 0; i < AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//�����̉��K�ƈʒu������W������o��
				p.X -= this.PlayOffset;									//�Đ����Ȃ̂ŁA�I�t�Z�b�g�������Ƃ�
				//parent.light.Draw();
				if(p.X < 55 && a.Visible == false)						//�����Ƃ�ʂ�߂��Ă���Ȃ�
				{
					continue;											//�����for�͔�΂�
				}
				else if(p.X <= 55)										//�����ƂɂԂ����Ă�����
				{
					muphic.SoundManager.Play("Sheep" + a.code + ".wav");			//�������炵�āc
					//parent.light.Add(a.code);
					a.Visible = false;
					houselight.Add(a.code);	
					continue;											//����for��
				}
				else if(p.X <= ScoreTools.score.Right)					//�����A���̒��ɓ����Ă���Ȃ�
				{
					a.Visible = true;									//�\��������
				}
				else if(ScoreTools.score.Right < p.X)					//�����A�܂��y���܂œ��B���Ă��Ȃ��Ȃ�
				{														//AnimalList�͏��Ԃǂ���ɕ���ł���̂ŁA���ꂩ����
					break;												//�y���܂œ��B���Ă��Ȃ����ƂɂȂ邩��for�����I������
				}

				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//�����̏����𖞂����Ă��Ȃ���΁A���ʂɕ`�悷��
				switch (a.group)
				{
					case 0:
						DrawManager.DrawCenter("ribbon1", p.X-12, spY-7);
						break;
					case 1:
						DrawManager.DrawCenter("ribbon2", p.X-12, spY-7);
						break;
					case 2:
						DrawManager.DrawCenter("ribbon3", p.X-12, spY-7);
						break;
					case 3:
						DrawManager.DrawCenter("ribbon4", p.X-12, spY-7);
						break;
					default:
						break;
				}
			}
			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalList�̍Ō�̗v�f�����o��
			Point bp = this.ScoretoPointRelative(b.place, b.code);
			bp.X -= this.PlayOffset;
			if(!b.Visible && bp.X <= 55)
			{
				this.PlayStop();										//�����̍Ō�̗v�f���ƂɂԂ���I������
				parent.startstop.State = 0;								//�Đ��I��
			}
		}

		/// <summary>
		/// ��Đ����̕`�揈��
		/// </summary>
		private void DrawNotPlaying()
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);
				//System.Diagnostics.Debug.WriteLine(a.place);
				if(ScoreTools.inScore(p))								//���̓����ɂ����
				{
					a.Visible = true;									//�\��������
				}
				else													//�O���ɂ����
				{
					a.Visible = false;									//�\�������Ȃ�
				}
				if(a.Visible)											//���̓����ɂ���Ƃ�����
				{														//�`�悳����
					DrawManager.DrawCenter(AnimalList[i].ToString(), p.X, p.Y);
				
					switch (a.group)
					{
						case 0:
							DrawManager.DrawCenter("ribbon1", p.X-12, p.Y-7);
							break;
						case 1:
							DrawManager.DrawCenter("ribbon2", p.X-12, p.Y-7);
							break;
						case 2:
							DrawManager.DrawCenter("ribbon3", p.X-12, p.Y-7);
							break;
						case 3:
							DrawManager.DrawCenter("ribbon4", p.X-12, p.Y-7);
							break;
						default:
							break;
					}
				}
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			//base.Click (p);								//���̃N���b�N�����͍s��Ȃ����Ƃɒ���
			if(this.isPlay || parent.LinkScreenMode == muphic.LinkScreenMode.AnswerDialog)		//�Đ����A�y�����̕ҏW��Ƃ͋����Ȃ�
			{
				return;
			}

			Point temp3 = new Point();
			temp3.X = nowPlace;
			temp3.Y = 0;
			if (p.X > 360) temp3.X += 8;
			if (p.X > 600) temp3.X += 8;
			if (temp3.X % 8 != 0)
			{
				temp3.X = temp3.X + (8 - (temp3.X % 8));
			}

			if(parent.links.NowClick != muphic.Link.LinkButtonsClickMode.None)	//�E�̃{�^���Q�ŉ�����I�����Ă���Έȉ������s����
			{
				Point place = muphic.Common.ScoreTools.DecidePlace(p);			//�y�����ł̈ʒu�Ɖ��K�����肷��
				place.X += this.nowPlace;
				//if (p.X > 398) place.X += 8;
				System.Diagnostics.Debug.WriteLine(place.Y, "���K");
				System.Diagnostics.Debug.WriteLine(place.X, "�ʒu");

				if(place.X == 0 && place.Y == 0)								//DebicePlace���y���O(�������͂����
				{																//�����Ƃ��������)���Ɣ��f����
					return;
				}
				

				if(parent.links.NowClick == muphic.Link.LinkButtonsClickMode.Cancel)//�L�����Z���{�^�����N���b�N����Ă�����
				{
					int temp = temp3.X;//(place.X / 8)*8;
					for (int i = 0; i < 8; i++)
					{
						for (int j = 0; j < 3; j++) //�a���΍��3����s
						{
							bool b = this.Delete(temp+i);						//�폜���������s
							System.Diagnostics.Debug.WriteLine(b, "Delete");
						}
					}
					putFlag[temp3.X/8] = false;
				}
				else															//�ق��̓������N���b�N����Ă�����
				{
					int rib = 0;
					bool insert = false;

					for (int i = 0; i < 4; i++)
					{
						if (!ribbon[parent.tsuibi.State, i])
						{
							rib = i;
							break;
						}
					}

					Point temp2 = new Point();
					temp2.X = this.nowPlace;
					temp2.Y = 0;
					if (p.X > 360) temp2.X += 8;
					if (p.X > 600) temp2.X += 8;
					if (temp2.X % 8 != 0)
					{
						temp2.X = temp2.X + (8 - (temp2.X % 8));
					}
					//temp2 = this.ScoretoPointRelative(temp2.X, temp2.Y);
					
					//���łɃO���[�v���z�u����Ă��������(�㏑���@�\)
					if (putFlag[temp2.X/8])
					{
						int temp = temp3.X;//(place.X / 8)*8;
						for (int i = 0; i < 8; i++)
						{
							for (int j = 0; j < 3; j++) //�a���΍��3����s
							{
								bool b = this.Delete(temp+i);						//�폜���������s
								System.Diagnostics.Debug.WriteLine(b, "Delete");
							}
						}
						putFlag[temp3.X/8] = false;
					}
					//if (!putFlag[temp2.X/8])
					{
						for (int i = 0; i < 8; i++)
						{
							bool b;
							Point temp;
							temp = parent.group.getPattern(parent.tsuibi.State, i);
							if (temp.Y == -1) break;
							b = this.Insert(temp2.X + 3+temp.X, temp.Y+1, rib);

							//						if (parent.tsuibi.point.X < 261)
							//						{
							//							b = this.Insert(3+temp.X, temp.Y+1, rib);
							//						}
							//						else if (parent.tsuibi.point.X > 489)
							//						{
							//							b = this.Insert(9+temp.X, temp.Y+1, rib);
							//						}
							//						else
							//						{
							//							b = this.Insert(place.X+temp.X, temp.Y+1, rib);
							//						}
							System.Diagnostics.Debug.WriteLine(b, "Insert");
							if (b) insert = true;
						}
						if (insert) putFlag[temp2.X/8] = true;
						scoreLength += 8;
					}

					if (insert) ribbon[parent.tsuibi.State, rib] = true;
				}
			}
		}

		/// <summary>
		/// ������V���ɒǉ�����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <param name="code">���K</param>
		/// <returns>�����������ǂ���</returns>
		private bool Insert(int place, int code, int group)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					return false;
				}
				if(a.place > place)												//�ݒ肷��ʒu��艓�����̂����݂���
				{																//AnimalList�͏����Ƀ\�[�g���Ă��邩��A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�place��
				}																//�z���Ă���̂ŁA���Ԃ��Ă��Ȃ����Ƃ��m�肷��
			}
			Animal newAnimal = new Animal(place, code, group, parent.links.NowClick);	//Animal�I�u�W�F�N�g���C���X�^���X��
			AnimalList.Insert(i, newAnimal);									//�����悤�ɂȂ����Ƃ���Ɋ��荞��
																				//�������邱�Ƃɂ���ď������ۂ����
			return true;
		}

		/// <summary>
		/// �������폜����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <param name="code">���K</param>
		/// <returns>�����������ǂ���</returns>
		private bool Delete(int place)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					AnimalList.RemoveAt(i);
					return true;
				}
				if(a.place > place)												//�폜����ʒu��艓�����̂����݂���
				{																//�܂�A����ȏ�T���Ă��Ӗ����Ȃ�����
					break;
				}
			}
			return false;
		}

		public void PlayAnswerCheck()
		{
			this.nowPlace = 0;
			this.PlayOffset = 0;
			for(int i=0;i<AnimalList.Count;i++)
			{
				((Animal)AnimalList[i]).Visible = true;							//�Đ��r���������ꍇ���l����
			}																	//visible�𕜋A������
			if(this.isPlay)
			{
				return;
			}
			parent.right.Visible = false;
			parent.left.Visible = false;
			//this.isPlay = true;
			answerCheckFlag = true;
		}

		public void PlayFirst()
		{
			this.nowPlace = 0;													//�y�[�W�������I�ɍŏ��ɖ߂�
			this.PlayOffset = 0;												//�Đ��r���������ꍇ�A�ŏ��ɖ߂�
			for(int i=0;i<AnimalList.Count;i++)
			{
				((Animal)AnimalList[i]).Visible = true;							//�Đ��r���������ꍇ���l����
			}																	//visible�𕜋A������
			this.PlayStart();
		}

		public void PlayStart()
		{

			if(this.isPlay)
			{
				return;
			}
			if(AnimalList.Count == 0)
			{
				PlayStop();
				parent.startstop.State = 0;
				return;
			}
			parent.startstop.State = 1;
			this.PlayOffset = 0;												//�I�t�Z�b�g�����ɖ߂�
			parent.right.Visible = false;										//�X�N���[���{�^���͉����Ȃ��悤�ɂ��Ă���
			parent.left.Visible = false;
			this.isPlay = true;
		}



		public void PlayStop()
		{
			if(!this.isPlay)
			{
				return;
			}
			for(int i=0;i<AnimalList.Count;i++)
			{
				((Animal)AnimalList[i]).Visible = true;							//visible�𕜋A������
			}
			parent.startstop.State = 0;
			parent.right.Visible = true;										//�X�N���[���{�^�������A������
			parent.left.Visible = true;
			this.isPlay = false;

			// �`���[�g���A�����s���ŁA����̑ҋ@��Ԃ������ꍇ
			if(TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
			{
				// �X�e�[�g�i�s
				parent.parent.tutorialparent.NextState();
			}
		}

//		public void RightScroll()
//		{
//			if(this.nowPlace < 100)
//			{																	//1���ߕ��y�[�W��i�߂�
//				this.defaultPlace = this.nowPlace = this.nowPlace + 4 * muphic.Common.ScoreTools.Kugiri;
//			}
//		}
//
//		public void LeftScroll()
//		{
//			if(this.nowPlace >= 0 + 4 * muphic.Common.ScoreTools.Kugiri)
//			{																	//1���ߕ��y�[�W��߂�
//				this.defaultPlace = this.nowPlace = this.nowPlace - 4 * muphic.Common.ScoreTools.Kugiri;
//			}
//		}

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
			int num = barNum -1 -2;
			if (num >= 1)
			{
				if (NewPlace > (num * 8) + 4)
				{
					NewPlace = num * 8;													//���߂��Ƃɋ�؂邩��100�ɂ͂Ȃ�Ȃ�
				}
				else if(NewPlace < 0)
				{
					NewPlace = 0;
				}
				this.defaultPlace = this.nowPlace = NewPlace;
				bar.Percent = ((float)NewPlace / ((num) * 8)) * 100;							//�X�N���[���̏ꏊ��V���ɐݒ�
			}
			else
			{
				this.defaultPlace = 0;
				bar.Percent = 0;							//�X�N���[���̏ꏊ��V���ɐݒ�
			}
		}

		/// <summary>
		/// ��ʂ��X�N���[������Ƃ��ɌĂԃ��\�b�h
		/// ������́A�X�N���[���o�[�ɂ���ĕύX���ꂽ�Ƃ��ɌĂԗp�̃��\�b�h
		/// </summary>
		/// <param name="Percent">�X�N���[���o�[�̃p�[�Z���e�[�W</param>
		public void ChangeScroll(float Percent)
		{
			int num = barNum -1 -2;
			float New = (Percent / 100) * (num);										//�͈͂�0�`96�Ȃ̂ŁA�܂�Percent��0�`12�͈̔͂ɕϊ�����
			int NewPlace = (int)(New + 0.5) * 8;								//�l�̌ܓ��̂��߁A+0.5���Ă���int�^�ɒ���
			ChangeScroll(NewPlace);												//���߂�ChangeScroll
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



		/// <summary>
		/// �Đ����̕`�揈��
		/// </summary>
		private void AnswerCheck()
		{
			int count;
			// �`���[�g���A�����s����FrameCount�̎Q�ƕ��@���ς��܂�
			if(muphic.Common.TutorialStatus.getIsTutorial()) count = this.parent.parent.tutorialparent.parent.parent.parent.FrameCount;
			else count = parent.parent.parent.FrameCount;
			
			//int count = parent.parent.parent.FrameCount;
			//int tempo = parent.tempo.TempoMode;
			this.PlayOffset += tempo;									//�e���|�̕������I�t�Z�b�g�𑫂��Ƃ�
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
//				bool flag = false;
//				if (parent.quest.Question[parent.QuestionNum].Length >= i)
//				{
//					Animal q = (Animal)parent.quest.Question[parent.QuestionNum][i];
//					if (q.code == a.code && q.place == a.place)
//					{
//						flag = true;
//					}
//				}

				Point p = this.ScoretoPointRelative(a.place, a.code);	//�����̉��K�ƈʒu������W������o��
				p.X -= this.PlayOffset;									//�Đ����Ȃ̂ŁA�I�t�Z�b�g�������Ƃ�
				//parent.light.Draw();
				if(p.X < 0 || a.Visible == false)						//������ʊO�ɏo�Ă��邩�A�`��֎~�Ȃ�
				{
					continue;											//�����for�͔�΂�
				}
				else if(p.X <= 55)										//�����ƂɂԂ����Ă�����
				{
					//if (parent.check.answerFlag.Length <= i)
					//{
						//if (parent.check.answerFlag[i])
						//{
							muphic.SoundManager.Play("Sheep" + a.code + ".wav");			//�������炵�āc
							//parent.light.Add(a.code);
							houselight.Add(a.code);
							a.Visible = false;
						//}
					//}
					continue;											//����for��
				}
				else if(800 < p.X)										//�����A�܂��y���܂œ��B���Ă��Ȃ��Ȃ�
				{														//AnimalList�͏��Ԃǂ���ɕ���ł���̂ŁA���ꂩ����
					break;												//�y���܂œ��B���Ă��Ȃ����ƂɂȂ邩��for�����I������
				}

				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//�����̏����𖞂����Ă��Ȃ���΁A���ʂɕ`�悷��
				switch (a.group)
				{
					case 0:
						DrawManager.DrawCenter("ribbon1", p.X-12, spY-7);
						break;
					case 1:
						DrawManager.DrawCenter("ribbon2", p.X-12, spY-7);
						break;
					case 2:
						DrawManager.DrawCenter("ribbon3", p.X-12, spY-7);
						break;
					case 3:
						DrawManager.DrawCenter("ribbon4", p.X-12, spY-7);
						break;
					default:
						break;
				}
			}

			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalList�̍Ō�̗v�f�����o��

			if(!b.Visible)
			{
				// �`���[�g���A�����s���ŁA����̑ҋ@��Ԃ������ꍇ
				if(TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
				{
					// �X�e�[�g�i�s
					parent.parent.tutorialparent.NextState();
				}
				
				
				parent.Screen = parent.check.flag ? new Dialog.Answer.AnswerDialog(parent, true) : new Dialog.Answer.AnswerDialog(parent, false);
				parent.signboard.drawFlag = false;
				for(int i=0;i<AnimalList.Count;i++)
				{
					((Animal)AnimalList[i]).Visible = true;							//visible�𕜋A������
				}

				parent.right.Visible = true;										//�X�N���[���{�^�������A������
				parent.left.Visible = true;
				this.answerCheckFlag = false;

				parent.startstop.State = 0;								//�Đ��I��
			}
		}
	}
}
