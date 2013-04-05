using System;
using System.Collections;
using System.Drawing;

namespace muphic.ScoreScr
{
	#region SVGA (�`ver.0.8.8)
	/*
	
	/// <summary>
	/// Score �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreMain : Screen
	{
		public ScoreScreen parent;
		public const int maxAnimals = 100;	// �z�u�\�ȓ����̐�
		private int offset;					// �`��J�n�̉�����32����(�P�s������)���炷
		
		// �e�������Ƃ̉������X�g
		public ArrayList SheepScoreList  = new ArrayList();
		public ArrayList RabbitScoreList = new ArrayList();
		public ArrayList BirdScoreList   = new ArrayList();
		public ArrayList DogScoreList    = new ArrayList();
		public ArrayList PigScoreList    = new ArrayList();
		public ArrayList VoiceScoreList  = new ArrayList();
		
		public int MaxNote;					// �S�������X�g�̉�����(8�Ŋ���Ə��ߐ��A32�Ŋ���ƍs���ɂȂ�)
		public AnimalScoreMode nowScore;	// ���ݕ\�����Ă��鉹�����X�g
		
		public ArrayList DrawList = new ArrayList();	// �`�悷��f�[�^�̃��X�g| _-)/
		
	# region �e��T�C�Y��`
		const int NotePerBar = 8;							// �P�ʏ��߂�����̍ő剹����
		const int BarPerLine = 4;							// �P�ʍs������̏��ߐ�
		const int NotePerLine = NotePerBar * BarPerLine;	// �P�ʍs������̍ő剹����
		const int LinePerPage = 6;							// ��ʂɕ\���ł���ܐ����̍ő�s��
		const int NotePerPage = NotePerLine * LinePerPage;	// ��ʂɕ\���ł��鉹���̍ő吔
		const int MaxChord = 3;								// �a���ő吔
		const int NoteXBegin = 148;							// ����x���W�̊(�s1�Ԗڂ̉�����x���W)(px)
		const int NoteYBegin = 141;							// ����y���W�̊(px)
		const int NoteXDifference = 16;						// �������m��x���W�̍�
		const int BarXDifference = 141;						// ���ߓ��m��x���W�̍�(px)
		const int ScoreXBegin = 99;							// �ܐ���x���W�̊(�ܐ�����x���W)(px)
		const int ScoreYBegin = 134;						// �ܐ���y���W�̊(1�s�ڂ̌ܐ�����y���W)(px)
		const int ScoreYDifference = 66;					// �ܐ������m��y���W�̍�(px)
		const int EndXBegin = 696;							// �I�[����x���W(px)
	# endregion
		
		public ScoreMain(ScoreScreen screen)
		{
			this.parent = screen;
			this.offset = 0;
			this.nowScore = AnimalScoreMode.All;
			this.parent.scores.all.State = 1;
			
	#region �摜��ĳ۰� ( ߁��)�
			DrawManager.Regist("QuarterNotes", 0, 0, "image\\Score\\note\\4buonpu.png");		// �l������-������
			DrawManager.Regist("QuarterNotes_", 0, 0, "image\\Score\\note\\4buonpu_.png");		// �l������-������
			DrawManager.Regist("QuarterNotes_do", 0, 0, "image\\Score\\note\\4buonpu_do.png");	// �l������-�h
			DrawManager.Regist("EighthNotes", 0, 0, "image\\Score\\note\\8buonpu.png");			// ��������-������
			DrawManager.Regist("EighthNotes_", 0, 0, "image\\Score\\note\\8buonpu_.png");		// ��������-������
			DrawManager.Regist("EighthNotes_do", 0, 0, "image\\Score\\note\\8buonpu_do.png");	// ��������-�h
			DrawManager.Regist("QuarterRest", 0, 0, "image\\Score\\note\\4bukyuhu.png");		// �l���x��
			DrawManager.Regist("EighthRest", 0, 0, "image\\Score\\note\\8bukyuhu.png");			// �����x��
			DrawManager.Regist("AllRest", 0, 0, "image\\Score\\note\\zenkyuhu.png");			// �S�x��
			DrawManager.Regist("Meter", 0, 0, "image\\Score\\note\\hyousi.png");				// 4/4���q�L��
			DrawManager.Regist("End", 0, 0, "image\\Score\\note\\end.png");						// �I�[
			DrawManager.Regist("End_full", 0, 0, "image\\Score\\note\\end_full.png");			// �I�[-�t���X�R�A�p
			DrawManager.Regist("Staff", 0, 0, "image\\Score\\score\\gosen.png");				// �ܐ���
			DrawManager.Regist("Line", 0, 0, "image\\Score\\score\\syousetu.png");				// ���ߋ�؂��
			DrawManager.Regist("Full", 0, 0, "image\\Score\\score\\full_line.png");				// �t���X�R�A
			
			DrawManager.Regist("SheepBig", 0, 0, "image\\Score\\omake\\Sheep_big.png");			// �w�i�¼��
			DrawManager.Regist("RabbitBig", 0, 0, "image\\Score\\omake\\Rabbit_big.png");		// �w�i�e
			DrawManager.Regist("BirdBig", 0, 0, "image\\Score\\omake\\Bird_big.png");			// �w�i���o�[�h
			DrawManager.Regist("DogBig", 0, 0, "image\\Score\\omake\\Dog_big.png");				// �w�iDog
			DrawManager.Regist("PigBig", 0, 0, "image\\Score\\omake\\Pig_big.png");				// �w�i�x�C�u
	#endregion
			
			// �l�̏������Ɖ������X�g�̾���� ( ߁��)�
			this.CreateScoreListAll();
			
			// �y�����ޮ���� ( ߁��)�
			this.ReDraw();
		}
		
		public override void Draw()
		{
			base.Draw();
			
			// �`�惊�X�g����`�悷��f�[�^��ǂݏo��
			for(int i=0; i<this.DrawList.Count; i++)
			{
				DrawData data = (DrawData)DrawList[i];
				muphic.DrawManager.Draw(data.Image, data.x, data.y);
			}
		}
		
		/// <summary>
		/// �`�惊�X�g���Đ������郁�\�b�h
		/// </summary>
		public void ReDraw()
		{
			DrawData drawdata;
			
			// �����̕`�惊�X�g���N���A����
			this.DrawList.Clear();	
			
			// DrawScore���\�b�h���`�惊�X�g���Đ�������
			switch(this.nowScore)
			{
				case AnimalScoreMode.All:
					this.DrawScore(this.SheepScoreList, 1, 0);
					this.DrawScore(this.RabbitScoreList, 1, ScoreYDifference);
					this.DrawScore(this.BirdScoreList, 1, ScoreYDifference * 2);
					this.DrawScore(this.DogScoreList, 1, ScoreYDifference * 3);
					this.DrawScore(this.PigScoreList, 1, ScoreYDifference * 4);
					this.DrawScore(this.VoiceScoreList, 1, ScoreYDifference * 5);
					this.DrawAll();	// �t���X�R�A��p�摜�̕`��
					break;
				case AnimalScoreMode.Sheep:
					drawdata = new DrawData("SheepBig", 508, 366); this.DrawList.Add(drawdata);
					this.DrawScore(this.SheepScoreList, 6, 0);	// �`�惊�X�g�ɂЂ��̊y�����Z�b�g
					break;
				case AnimalScoreMode.Rabbit:
					drawdata = new DrawData("RabbitBig", 581, 351); this.DrawList.Add(drawdata);
					this.DrawScore(this.RabbitScoreList, 6, 0);	// �`�惊�X�g�ɂ������̊y�����Z�b�g
					break;
				case AnimalScoreMode.Bird:
					drawdata = new DrawData("BirdBig", 549, 388); this.DrawList.Add(drawdata);
					this.DrawScore(this.BirdScoreList, 6, 0);	// �`�惊�X�g�Ƀo�[�h�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Dog:
					drawdata = new DrawData("DogBig", 525, 376); this.DrawList.Add(drawdata);
					this.DrawScore(this.DogScoreList, 6, 0);	// �`�惊�X�g�ɂ��ʂ̊y�����Z�b�g
					break;
				case AnimalScoreMode.Pig:
					drawdata = new DrawData("PigBig", 514, 386); this.DrawList.Add(drawdata);
					this.DrawScore(this.PigScoreList, 6, 0);	// �`�惊�X�g�Ƀx�C�u�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Voice:
					this.DrawScore(this.VoiceScoreList, 6, 0);	// �`�惊�X�g�ɳޫ���̊y�����Z�b�g
					break;
				default:
					break;
			}
		}
		
		/// <summary>
		/// �S�Ẳ������X�g�𐶐����郁�\�b�h
		/// </summary>
		public void CreateScoreListAll()
		{
			int max;
			try
			{
				// �������X�g�̍Ō�̓����̈ʒu����A�S�������X�g�ɂ�����Œ��̈ʒu�𓾂�
				Animal animal = ((Animal)parent.AnimalList[parent.AnimalList.Count-1]);
				max = animal.place;
			}
			catch(Exception) // ����0�C�̏�ԂŌĂяo���Ɣ͈͊O�Q�Ƃ��������Ă��܂����ߖ�����c
			{
				max = 0;
			}
			
			// ���̒l����A�������X�g�̉��������Z�o(32�̔{���ɂȂ�悤����)
			this.MaxNote = max + (NotePerLine - max % NotePerLine);
			
			// �e�������X�g��������
			this.SheepScoreList.Clear();
			this.RabbitScoreList.Clear();
			this.BirdScoreList.Clear();
			this.DogScoreList.Clear();
			this.PigScoreList.Clear();
			this.VoiceScoreList.Clear();
			
			this.CheckScoreList(this.SheepScoreList);
			
			// �������X�g�̾���� ( ߁��)�
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Sheep);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Rabbit);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Bird);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Dog);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Pig);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Voice);
		}
		
		/// <summary>
		/// �`�惊�X�gDrawList�Ɋy���f�[�^��o�^���郁�\�b�h
		/// </summary>
		/// <param name="data">�`�悷�鉹�����X�g</param>
		/// <param name="mode">�`�悷��s��</param>
		/// <param name="yoffset">�t���X�R�A�p y���W�I�t�Z�b�g</param>
		public void DrawScore(ArrayList data, int mode, int yoffset)
		{
			int i=this.offset, j, end;
			DrawData drawdata;
			
			// ���[�v�I�������̐ݒ�
			end = i + mode * NotePerLine;
			if(end > NotePerPage) end = NotePerPage;	// �������U�s���𒴂����ꍇ�͂U�s�܂łƂ���
			if(end > data.Count) end = data.Count;		// ����ɉ������X�g�̉������𒴂����ꍇ�͉������ɂ���
			
			yoffset -= ( this.offset / NotePerLine ) * ScoreYDifference;
			
			// offset����X�^�[�g
			while(i < end)
			{
				Score score = (Score)data[i];
				
				// �s�̍ŏ��̏��߂̕`�掞�Ɍܐ������`�悷��
				if(i%NotePerLine == 0)
				{
					// y���W��1�s��134px�{�s���~66px
					drawdata = new DrawData( "Staff", ScoreXBegin, ScoreYBegin + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
					
					if(i/NotePerLine == 0)
					{
						// ����ɁA1�s�ڂł�4/4���q�̕`��
						drawdata = new DrawData( "Meter", ScoreYBegin, ScoreYBegin-2 + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
						DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
					}
				}
				
				// �����`��
				for(j=0; j<3; j++)
				{
					// �x���̘a���͍݂�܂��� �a���Q�ڈȍ~�ɉ������Ȃ���Ύ��ɐi�ނ悤�ɂ���
					if( (j==1 && score.code[1]==-1) || (j==2 && score.code[2]==-1) ) break;

					Point p = getScoreCoordinate(score, j, i);	// ���W�Q�b�c��
					drawdata = new DrawData(getScoreImage(score, j), p.X, p.Y +yoffset);
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
				}

				// �����̒�������ɐi�߂�
				i += (int)(score.length * 2);
			}

			// �I�[�̕`�� i���ő剹�����������ꍇ�A�y���̏I�[�ł��邽��
			if(i == this.MaxNote)
			{
				// �s��*66�����ĕ`��
				if(this.nowScore != muphic.ScoreScr.AnimalScoreMode.All)
				{
					drawdata = new DrawData( "End", EndXBegin, NoteXBegin-2 + (int)(Math.Floor(i/NotePerLine) - 1) * ScoreYDifference );
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
				}
			}
		}

		/// <summary>
		/// �t���X�R�A���̕`��
		/// </summary>
		public void DrawAll()
		{
			// �t���X�R�A���͂܂Ƃ߂�J�b�R��`�悷��
			DrawData drawdata = new DrawData("Full", 53, 135);
			DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�

			// �I�t�Z�b�g+32���������������ꍇ�A�y���̏I�[�Ɣ��f
			if(this.offset+NotePerLine == this.MaxNote)
			{
				drawdata = new DrawData( "End_full", EndXBegin+1, NoteXBegin-2 );
				DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
			}

		}
		
		/// <summary>
		/// �^����ꂽ�����̕\���ʒu�����肷�郁�\�b�h
		/// </summary>
		/// <param name="score">�ʒu�����肷�鉹��</param>
		/// <param name="num">�a�����Ԗڂ̉�����</param>
		/// <param name="i">���Ԗڂ̉�����</param>
		/// <returns>�\���ʒu���W</returns>
		public Point getScoreCoordinate(Score score, int num, int i)
		{
			Point p = new Point(0,0);
			int n = i%32;	// �s���̉��Ԗڂ̉�������
			
			if(score.code[0] == -1 )
			{
				// �x���̏ꍇ
				if(score.length == 0.5) p.Y = ScoreYBegin + 20 + i/NotePerLine * ScoreYDifference;		// �����x��
				else if(score.length == 1) p.Y = ScoreYBegin + 14 + i/NotePerLine * ScoreYDifference;	// �l���x��
				else if(score.length == 2) p.Y = ScoreYBegin + 24 + i/NotePerLine * ScoreYDifference;	// �񕪋x��
				else if(score.length == 4) p.Y = ScoreYBegin + 19 + i/NotePerLine * ScoreYDifference;	// �S�x��
			}
			else
			{
				// �x������Ȃ��ꍇ ���K���Ƃ�y���W��ς���
				p.Y = ScoreYBegin-3 + score.code[num]*4-4 + i/NotePerLine * ScoreYDifference;
	
				// �V����̉��������畄�������̉����ɂȂ�
				if( -1 < score.code[num] && score.code[num] < 3 )
					// �������A���悵���̉���������a���̏ꍇ�͂��̌���ł͂Ȃ�
					if( !(score.code[0] > 2 || score.code[1] > 2 || score.code[2] > 2) ) 
						p.Y += 22;
			}

			// ������x���W������
			// 1�Ԗڂ̉������W148px + ���ߐ�(0�`3)*141px + ���ߓ��̉�����(0�`7)*16px
			p.X = NoteXBegin + (n/NotePerBar * BarXDifference) + (n%NotePerBar * NoteXDifference);
			
			// �����ȊO�̏ꍇ�͂��ꂼ��x���W�����ăo�����X����
			if(score.length == 1) p.X += NoteXDifference / 2;
			else if(score.length == 2) p.X += (NoteXDifference / 2) * 3;
			else if(score.length == 4) p.X += (NoteXDifference / 2) * 7;

			// ���̃h�������ꍇ�������ɂ��炷
			if(score.code[num] == 8) p.X -= 3;

			return p;
		}
		
		/// <summary>
		/// �^����ꂽ�����̉摜�����肷�郁�\�b�h
		/// </summary>
		/// <param name="score">�摜�����肷�鉹��</param>
		/// <param name="num">�a�����Ԗڂ̉�����</param>
		/// <returns>�\���摜������</returns>
		public String getScoreImage(Score score, int num)
		{
			// �a����ڂ̉���(code[0]) �x�������肦��
			if(num == 0 && score.code[0] == -1)
			{
				// �����x���Ɍ���
				if(score.length == 0.5) return "EighthRest";

				// �l���x���Ɍ���
				if(score.length == 1) return "QuarterRest";

				// ����ȊO�͓񕪋x�����S�x��
				return "AllRest";
			}
			// �a����ڈȍ~�͋x���͖���
			// �����ĂȂ񂩃A�� ���������̉����g��Ȃ��Ă����C�����Ă���

			// ���������Ɍ��� �����������̉摜�͘a����ԏ�ł����g���܂���
			if(score.length == 0.5 && num == 0)
			{
				// ���̃h��������h�̉����ɂ���
				if(score.code[num] == 8) return "EighthNotes_do";

				// �V����̉��������畄�������̉����ɂȂ�
				if( -1 < score.code[num] && score.code[num] < 3 )
					// �������A���悵���̉���������a���̏ꍇ�͂��̌���ł͂Ȃ�
					if( !(score.code[0] > 2 || score.code[1] > 2 || score.code[2] > 2) ) 
						return "EighthNotes_";

				// �����ɂ�����Ȃ���Ε��ʂ̔�������
				return "EighthNotes";
			}
			// ����ȊO�͎l�������ɂȂ�܂���
			else
			{
				// ���̃h��������h�̉����ɂ���
				if(score.code[num] == 8) return "QuarterNotes_do";

				// �V����̉��������畄�������̉����ɂȂ�
				if( -1 < score.code[num] && score.code[num] < 3 )
					// �������A���悵���̉���������a���̏ꍇ�͂��̌���ł͂Ȃ�
					if( !(score.code[0] > 2 || score.code[1] > 2 || score.code[2] > 2) ) 
						return "QuarterNotes_";

				// �����ɂ�����Ȃ���Ε��ʂ̎l������
				return "QuarterNotes";
			}
		}

		/// <summary>
		/// AnimalList���炻�ꂼ��̓����̉������X�g�����
		/// </summary>
		/// <param name="animalmode">���X�g�쐬�̑Ώۂ̓���</param>
		public void CreateScoreList(muphic.ScoreScr.AnimalScoreMode animalmode)
		{
			int i,n;
			Animal animal = new Animal(0,0);
			Score[] ScoreList = new Score[this.MaxNote];
			for(i=0; i<this.MaxNote; i++) ScoreList[i] = new Score();
			i=n=0;

			// �������X�g�Ɋ܂܂�Ă���Ώۂ̓����̐����擾
			int num = this.CheckAnimalNumber(animalmode);

			// 0�Ԗڂ̓����̃f�[�^���擾 �Ώۂ̓����Ƀq�b�g����܂Ńf�[�^�擾�������� 
			if(num > 0)
			{
				animal = ((Animal)parent.AnimalList[n]);
				while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);
			}

			// �������X�g�쐬���C�����[�v
			while(num > 0)
			{
				// �y���ʒui�Ɠ����ʒuplace����v���A���Ώۂ̓����ł��邩�ǂ��� 
				if( i==animal.place && animal.AnimalName.Equals(animalmode.ToString()) )
				{
					// ��L�����𖞂������ꍇ�A���K���y���ɃR�s�[
					ScoreList[i].AddCode(animal.code);

					// ����ɁA���X�g���̑Ώۓ��������P���炷
					// ���X�g���̑Ώۓ�������0�ɂȂ����烋�[�v�I��
					if(--num == 0) break;

					// ���̓����f�[�^���擾 �Ώۂ̓����Ƀq�b�g����܂Ńf�[�^�擾��������
					animal = ((Animal)parent.AnimalList[++n]);
					while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);

					// ���̓��������̊y���ʒu�ɖ����ꍇ�A�l�������ɂ���
					if(i % 32 != 31 && animal.place > i+1) ScoreList[i].length = 1;
				}
				else
				{
					// �����𖞂����Ȃ������ꍇ��i��i�߂�
					i++;
				}
			}

			// �Ō�̉������l���ɂ���
			if( (i % NotePerLine != NotePerLine-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;

			// �������X�g�������[�v
			i = -1;
			while(++i < this.MaxNote)
			{
				// �S�x������
				// ���߂̍ŏ��̉����Ŕ�������{
				if( (i%NotePerBar == NotePerBar-4*2) && CheckRest(ScoreList, i, 8) )
				{
					ScoreList[i].length = 4;	// ���ߓ��̂��ׂẲ������x����������A�S�x���ɂ���
					i += 7; continue;			// �ꏬ�ߕ���ɐi�߂�
				}
				
				// �񕪋x������
				// ���ߓ���5�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
				if( (i%NotePerBar <= NotePerBar-2*2) && CheckRest(ScoreList, i, 4) )
				{
				
					ScoreList[i].length = 2;	// �񕪋x��
					i += 3; continue;			// �񕪐�ɐi�߂�
				}
				
				// �l���x������
				// ���ߓ���7�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
				if( (i%NotePerBar <= NotePerBar-1*2) && CheckRest(ScoreList, i, 2) )
				{
					ScoreList[i].length = 1;	// �l���x��
					i += 1; continue;			// �l����ɐi�߂�
				}
				
				// �^�C�̔���
				// ���̏��߂��y���̍Ō�̏��߂łȂ��ꍇ
				if( (i%NotePerLine == NotePerBar-1) && (this.MaxNote > i) )
				{
					// �܂��x���łȂ��l�������������ꍇ���Y��
					if ( (ScoreList[i].code[0] != -1) && (ScoreList[i].length >= 1) )
					{
						ScoreList[i].tie = true;					// �^�C�t���Oon
						ScoreList[i].length = 0.5;					// �����̒����𔪕���
						ScoreList[i+1].length = 0.5;				// ���̉����̒����𔪕���
						ScoreList[i+1].code[0] = ScoreList[i].code[0];	// �������R�s�[
						ScoreList[i+1].code[1] = ScoreList[i].code[1];	// �������R�s�[
						ScoreList[i+1].code[2] = ScoreList[i].code[2];	// �������R�s�[
						Console.WriteLine("DEBUG");
					}
				}
				i += (int)(ScoreList[i].length * 2) - 1;
			}

			// ���������z��̃��X�g��p�ӂ��ꂽ�e�������Ƃ̃t�B�[���h�ɃR�s�[����
			switch(animalmode)
			{
				case AnimalScoreMode.Sheep:
					for(i=0; i<this.MaxNote; i++) this.SheepScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Rabbit:
					for(i=0; i<this.MaxNote; i++) this.RabbitScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Bird:
					for(i=0; i<this.MaxNote; i++) this.BirdScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Dog:
					for(i=0; i<this.MaxNote; i++) this.DogScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Pig:
					for(i=0; i<this.MaxNote; i++) this.PigScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Voice:
					for(i=0; i<this.MaxNote; i++) this.VoiceScoreList.Add(ScoreList[i]);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// �w�肳�ꂽ�͈͂��S�ċx�����ǂ������`�F�b�N����
		/// </summary>
		/// <param name="data">�`�F�b�N���鉹�����X�g</param>
		/// <param name="i">�J�n�v�f�ԍ�</param>
		/// <param name="n">�`�F�b�N���鉹����</param>
		/// <returns>�͈͂̉����S�ċx���Ȃ�true �����łȂ��Ȃ�false</returns>
		public bool CheckRest(Score[] data, int i, int n)
		{
			int j=0;
			for(; j<n; j++)
			{
				if(data[i+j].code[0] != -1) return false;
			}
			return true;
		}

		/// <summary>
		/// ���X�g���Ɏw�肵�������������܂܂�Ă��邩�`�F�b�N���郁�\�b�h
		/// </summary>
		/// <param name="mode"></param>
		/// <returns>�܂܂�Ă�����</returns>
		public int CheckAnimalNumber(muphic.ScoreScr.AnimalScoreMode mode)
		{
			int num=0;
			for(int i=0; i<parent.AnimalList.Count; i++)
			{
				Animal a = ((Animal)parent.AnimalList[i]);
				if(a.AnimalName.Equals(mode.ToString())) num++;
			}
			return num;
		}

		/// <summary>
		/// �������X�g���`�F�b�N���� ���Ă��ꗗ��\������
		/// ��Ƀf�o�b�O�p
		/// </summary>
		/// <param name="data">�ꗗ��\�����鉹�����X�g</param>
		/// <param name="length">���X�g�̒���</param>
		public void CheckScoreList(Score[] data, int length)
		{
			int i=0;

			for(i=0; i<length; ++i)
			{
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data[i].length, data[i].code[0], data[i].code[1], data[i].code[2]);
			}
		}
		public void CheckScoreList(ArrayList list)
		{
			int i=0;

			for(i=0; i<list.Count; ++i)
			{
				Score data = (Score)list[i];
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data.length, data.code[0], data.code[1], data.code[2]);
			}
		}

		/// <summary>
		/// ��X�N���[���{�^�����������ۂ̓���
		/// </summary>
		public void UpScroll()
		{
			// �P�s�ڂ���ɂ͍s���Ȃ��悤�ɂ���
			if(this.offset >= 32) this.offset -= 32;

			// �����čĕ`��
			this.ReDraw();
		}

		/// <summary>
		/// ���X�N���[���{�^�����������ۂ̓���
		/// </summary>
		public void DownScroll()
		{
			if(this.nowScore != AnimalScoreMode.All)
			{
				// �t���X�R�A�ł͖����ꍇ�A�y�����V�s�ȏ�ɂȂ������̂݉��ɃX�N���[����
				if(this.MaxNote > 192 && this.offset < this.MaxNote-32) this.offset += 32;
			}
			else
			{
				// �t���X�R�A�̏ꍇ
				if(this.offset < this.MaxNote-32) this.offset += 32;
			}

			// �����čĕ`��
			this.ReDraw();
		}

		/// <summary>
		/// �I�t�Z�b�g���N���A���郁�\�b�h
		/// ���ƂȂ�private�ɂ������������
		/// </summary>
		public void ClearOffset()
		{
			this.offset = 0;
		}
	}
	
	*/
	#endregion

	#region XGA (ver.0.9.0�`)
	/*
	/// <summary>
	/// Score �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreMain : Screen
	{
		public ScoreScreen parent;
		public const int maxAnimals = 100;	// �z�u�\�ȓ����̐�
		private int offset;					// �`��J�n�̉�����32����(�P�s������)���炷
		
		// �e�������Ƃ̉������X�g
		public ArrayList SheepScoreList  = new ArrayList();
		public ArrayList RabbitScoreList = new ArrayList();
		public ArrayList BirdScoreList   = new ArrayList();
		public ArrayList DogScoreList    = new ArrayList();
		public ArrayList PigScoreList    = new ArrayList();
		public ArrayList CatScoreList    = new ArrayList();
		public ArrayList VoiceScoreList  = new ArrayList();
		
		public int MaxNote;					// �S�������X�g�̉�����(8�Ŋ���Ə��ߐ��A32�Ŋ���ƍs���ɂȂ�)
		public AnimalScoreMode nowScore;	// ���ݕ\�����Ă��鉹�����X�g
		
		public ArrayList DrawList = new ArrayList();	// �`�悷��f�[�^�̃��X�g| _-)/
		
		# region �e��T�C�Y��`
		const int NotePerBar = 8;							// �P�ʏ��߂�����̍ő剹���� ���ύX����Ɠ���ł��Ȃ��Ǝv��
		const int BarPerLine = 4;							// �P�ʍs������̏��ߐ�
		const int NotePerLine = NotePerBar * BarPerLine;	// �P�ʍs������̍ő剹����
		const int LinePerPage = 7;							// ��ʂɕ\���ł���ܐ����̍ő�s��
		const int NotePerPage = NotePerLine * LinePerPage;	// ��ʂɕ\���ł��鉹���̍ő吔
		const int MaxChord = 3;								// �a���ő吔
		const int NoteXBegin = 189;							// ����x���W�̊(�s1�Ԗڂ̉�����x���W)(px ��XGA�C���ς�)
		const int NoteYBegin = 141;							// ����y���W�̊(px)
		const int NoteXDifference = 21;						// �������m��x���W�̍�(��XGA�C���ς�)
		const int BarXDifference = 181;						// ���ߓ��m��x���W�̍�(px ��XGA�C���ς�)
		const int ScoreXBegin = 122;						// �ܐ���x���W�̊(�ܐ�����x���W)(px ��XGA�C���ς�)
		const int ScoreYBegin = 173;						// �ܐ���y���W�̊(1�s�ڂ̌ܐ�����y���W)(px ��XGA�C���ς�)
		const int ScoreYDifference = 67;					// �ܐ������m��y���W�̍�(px ��XGA�C���ς�)
		const int EndXBegin = 906;							// �I�[����x���W(px ��XGA�C���ς�)

		const int BigImageX = 600;			// ���܂��摜x���W
		const int BigImageY = 450;			// ���܂��摜y���W
		# endregion
		
		/// <summary>
		/// ScoreMain �R���X�g���N�^
		/// </summary>
		/// <param name="screen">��ʉ�ʃN���X</param>
		public ScoreMain(ScoreScreen screen)
		{
			this.parent = screen;
			this.offset = 0;
			this.nowScore = AnimalScoreMode.All;
			this.parent.scores.all.State = 1;
			
			#region �摜��ĳ۰� ( ߁��)�
			DrawManager.Regist("QuarterNotes", 0, 0, "image\\ScoreXGA\\note\\4buonpu.png");			// �l������-������
			DrawManager.Regist("QuarterNotes_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_.png");		// �l������-������
			DrawManager.Regist("QuarterNotes_do", 0, 0, "image\\ScoreXGA\\note\\4buonpu_do.png");	// �l������-�h
			DrawManager.Regist("QuarterNotes_wa", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa.png");	// �l������-������a���p
			DrawManager.Regist("QuarterNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa_.png");	// �l������-�������a���p
			DrawManager.Regist("EighthNotes", 0, 0, "image\\ScoreXGA\\note\\8buonpu.png");			// ��������-������
			DrawManager.Regist("EighthNotes_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_.png");		// ��������-������
			DrawManager.Regist("EighthNotes_do", 0, 0, "image\\ScoreXGA\\note\\8buonpu_do.png");	// ��������-�h
			DrawManager.Regist("EighthNotes_wa", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa.png");	// ��������-������a���p
			DrawManager.Regist("EighthNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa_.png");	// ��������-�������a���p
			DrawManager.Regist("AllRest", 0, 0, "image\\ScoreXGA\\note\\zenkyuhu.png");				// �S�x��/�񕪋x��
			DrawManager.Regist("PHalfRest", 0, 0, "image\\ScoreXGA\\note\\2bukyuhu_huten.png");		// �t�_�񕪋x��
			DrawManager.Regist("PQuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu_huten.png");	// �t�_�l���x��
			DrawManager.Regist("QuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu.png");			// �l���x��
			DrawManager.Regist("EighthRest", 0, 0, "image\\ScoreXGA\\note\\8bukyuhu.png");			// �����x��
			DrawManager.Regist("Meter", 0, 0, "image\\ScoreXGA\\note\\hyousi.png");					// 4/4���q�L��
			DrawManager.Regist("End", 0, 0, "image\\ScoreXGA\\note\\end.png");						// �I�[
			DrawManager.Regist("End_full", 0, 0, "image\\ScoreXGA\\note\\end_full.png");			// �I�[-�t���X�R�A�p
			DrawManager.Regist("Staff", 0, 0, "image\\ScoreXGA\\score\\gosen.png");					// �ܐ���
			DrawManager.Regist("Line", 0, 0, "image\\ScoreXGA\\score\\syousetu.png");				// ���ߋ�؂��
			DrawManager.Regist("Full", 0, 0, "image\\ScoreXGA\\score\\full_line.png");				// �t���X�R�A

			DrawManager.Regist("TieTop", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t.png");			// �^�C(��)
			DrawManager.Regist("TieTopHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h1.png");	// �^�C(��E�n�[)
			DrawManager.Regist("TieTopHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h2.png");	// �^�C(��E�I�[)
			DrawManager.Regist("TieUnder", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u.png");			// �^�C(��)
			DrawManager.Regist("TieUnderHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h1.png");	// �^�C(���E�n�[)
			DrawManager.Regist("TieUnderHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h2.png");	// �^�C(���E�I�[)
			
			DrawManager.Regist("SheepBig", 0, 0, "image\\ScoreXGA\\omake\\Sheep_big.png");			// �w�i�¼��
			DrawManager.Regist("RabbitBig", 0, 0, "image\\ScoreXGA\\omake\\Rabbit_big.png");		// �w�i�e
			DrawManager.Regist("BirdBig", 0, 0, "image\\ScoreXGA\\omake\\Bird_big.png");			// �w�i���o�[�h
			DrawManager.Regist("DogBig", 0, 0, "image\\ScoreXGA\\omake\\Dog_big.png");				// �w�iDog
			DrawManager.Regist("PigBig", 0, 0, "image\\ScoreXGA\\omake\\Pig_big.png");				// �w�i�x�C�u
			DrawManager.Regist("CatBig", 0, 0, "image\\ScoreXGA\\omake\\Cat_big.png");				// �w�i�ʂ�
			DrawManager.Regist("VoiceBig", 0, 0, "image\\ScoreXGA\\omake\\Voice_big.png");			// �w�i���J������
			#endregion
			
			// �l�̏������Ɖ������X�g�̾���� ( ߁��)�
			this.CreateScoreListAll();
			
			// �y�����ޮ���� ( ߁��)�
			this.ReDraw();
		}
		
		public override void Draw()
		{
			base.Draw();
			
			// �`�惊�X�g����`�悷��f�[�^��ǂݏo��
			for(int i=0; i<this.DrawList.Count; i++)
			{
				DrawData data = (DrawData)DrawList[i];
				muphic.DrawManager.Draw(data.Image, data.x, data.y);
			}
		}
		
		/// <summary>
		/// �`�惊�X�g���Đ������郁�\�b�h
		/// </summary>
		public void ReDraw()
		{
			DrawData drawdata;
			
			// �����̕`�惊�X�g���N���A����
			this.DrawList.Clear();	
			
			#region DrawScore���\�b�h���`�惊�X�g���Đ�������
			switch(this.nowScore)
			{
				case AnimalScoreMode.All:
					this.DrawScore(this.SheepScoreList, 1, 0);
					this.DrawScore(this.RabbitScoreList, 1, ScoreYDifference);
					this.DrawScore(this.BirdScoreList, 1, ScoreYDifference * 2);
					this.DrawScore(this.DogScoreList, 1, ScoreYDifference * 3);
					this.DrawScore(this.PigScoreList, 1, ScoreYDifference * 4);
					this.DrawScore(this.CatScoreList, 1, ScoreYDifference * 5);
					this.DrawScore(this.VoiceScoreList, 1, ScoreYDifference * 6);
					this.DrawAll();	// �t���X�R�A��p�摜�̕`��
					break;
				case AnimalScoreMode.Sheep:
					drawdata = new DrawData("SheepBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.SheepScoreList, LinePerPage, 0);	// �`�惊�X�g�ɂЂ��̊y�����Z�b�g
					break;
				case AnimalScoreMode.Rabbit:
					drawdata = new DrawData("RabbitBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.RabbitScoreList, LinePerPage, 0);	// �`�惊�X�g�ɂ������̊y�����Z�b�g
					break;
				case AnimalScoreMode.Bird:
					drawdata = new DrawData("BirdBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.BirdScoreList, LinePerPage, 0);	// �`�惊�X�g�Ƀo�[�h�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Dog:
					drawdata = new DrawData("DogBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.DogScoreList, LinePerPage, 0);	// �`�惊�X�g�ɂ��ʂ̊y�����Z�b�g
					break;
				case AnimalScoreMode.Pig:
					drawdata = new DrawData("PigBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.PigScoreList, LinePerPage, 0);	// �`�惊�X�g�Ƀx�C�u�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Cat:
					drawdata = new DrawData("CatBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.CatScoreList, LinePerPage, 0);	// �`�惊�X�g�ɂʂ��̊y�����Z�b�g
					break;
				case AnimalScoreMode.Voice:
					drawdata = new DrawData("VoiceBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.VoiceScoreList, LinePerPage, 0);	// �`�惊�X�g�ɳޫ���̊y�����Z�b�g
					break;
				default:
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// �S�Ẳ������X�g�𐶐����郁�\�b�h
		/// </summary>
		public void CreateScoreListAll()
		{
			int max;
			try
			{
				// �������X�g�̍Ō�̓����̈ʒu����A�S�������X�g�ɂ�����Œ��̈ʒu�𓾂�
				Animal animal = ((Animal)parent.AnimalList[parent.AnimalList.Count-1]);
				max = animal.place;
			}
			catch(Exception) // ����0�C�̏�ԂŌĂяo���Ɣ͈͊O�Q�Ƃ��������Ă��܂����ߖ�����c
			{
				max = 0;
			}
			
			// ���̒l����A�������X�g�̉��������Z�o(32�̔{���ɂȂ�悤����)
			this.MaxNote = max + (NotePerLine - max % NotePerLine);
			
			// �e�������X�g��������
			this.SheepScoreList.Clear();
			this.RabbitScoreList.Clear();
			this.BirdScoreList.Clear();
			this.DogScoreList.Clear();
			this.PigScoreList.Clear();
			this.CatScoreList.Clear();
			this.VoiceScoreList.Clear();
			
			this.CheckScoreList(this.SheepScoreList);
			
			// �������X�g�̾���� ( ߁��)�
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Sheep);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Rabbit);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Bird);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Dog);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Pig);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Cat);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Voice);
		}
		
		#region �y���`�惊�X�g�������\�b�h�Q

		/// <summary>
		/// �`�惊�X�gDrawList�Ɋy���f�[�^��o�^���郁�\�b�h
		/// </summary>
		/// <param name="data">�`�悷�鉹�����X�g</param>
		/// <param name="mode">�`�悷��s��</param>
		/// <param name="yoffset">�t���X�R�A�p y���W�I�t�Z�b�g</param>
		public void DrawScore(ArrayList data, int mode, int yoffset)
		{
			int i=this.offset, j, end;
			Point p = new Point(0, 0);				
			String filename;
			DrawData drawdata;
			
			// ���[�v�I�������̐ݒ�
			end = i + mode * NotePerLine;
			if(end > NotePerPage) end = NotePerPage;	// �������U�s���𒴂����ꍇ�͂U�s�܂łƂ���
			if(end > data.Count) end = data.Count;		// ����ɉ������X�g�̉������𒴂����ꍇ�͉������ɂ���
			
			yoffset -= ( this.offset / NotePerLine ) * ScoreYDifference;
			
			// offset����X�^�[�g
			while(i < end)
			{
				Score score = (Score)data[i];
				
				// �s�̍ŏ��̏��߂̕`�掞�Ɍܐ������`�悷��
				if(i%NotePerLine == 0)
				{
					// y���W��1�s��134px�{�s���~67px
					drawdata = new DrawData( "Staff", ScoreXBegin, ScoreYBegin + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
					
					if(i/NotePerLine == 0)
					{
						// ����ɁA1�s�ڂł�4/4���q�̕`��
						drawdata = new DrawData( "Meter", ScoreXBegin+40, ScoreYBegin+13 + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
						DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
					}
				}
				
				// �����`��
				for(j=0; j<3; j++)
				{
					// �x���̘a���͍݂�܂��� �a���Q�ڈȍ~�ɉ������Ȃ���Ύ��ɐi�ނ悤�ɂ���
					if( (j==1 && score.code[1]==-1) || (j==2 && score.code[2]==-1) ) break;

					p = getScoreCoordinate(score, j, i);	// ���W�Q�b�c��
					drawdata = new DrawData(getScoreImage(score, j), p.X, p.Y +yoffset);
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
				}
				
				#region �^�C�̕`��

				if(score.tie == 1)
				{
					if(i%NotePerLine != NotePerLine-1)
					{
						// �s�̍Ō�łȂ������ꍇ�A���ʂɃ^�C�摜��`��
						
						// �����̌����Ń^�C�̈ʒu���ς��
						if( this.CheckCodeDirection(score) == 0)
						{
							// �����̌������ゾ�����ꍇ�A�^�C�摜�͉����̉��ɕ`�悷��
							p = getScoreCoordinate(score, score.chord-1, i);	// �܂������̍��W�𓾂�
							p.X += 2;											// ���������̍��W����ɂ���
							p.Y += 34;											// �摜�̈ʒu�𒲐߂���
							filename = "TieUnder";								// �摜�t�@�C�����̐ݒ�
						}
						else
						{
							// �����̌��������������ꍇ�A�^�C�摜�͉����̏�ɕ`�悷��
							p = getScoreCoordinate(score, 0, i);	// �܂������̍��W�𓾂�
							p.X += 3;								// ���������̍��W����ɂ���
							p.Y -= 10;								// �摜�̈ʒu�𒲐߂���
							filename = "TieTop";					// �摜�t�@�C�����̐ݒ�
						}
					}
					else
					{
						// �s�̍Ōゾ�����ꍇ�A�O��(�n�[��)�̂ݕ`��

						// �����̌����Ń^�C�̈ʒu���ς��
						if( this.CheckCodeDirection(score) == 0)
						{
							// �����̌������ゾ�����ꍇ�A�^�C�摜�͉����̉��ɕ`�悷��
							p = getScoreCoordinate(score, score.chord-1, i);	// �܂������̍��W�𓾂�
							p.X += 2;											// ���������̍��W����ɂ���
							p.Y += 34;											// �摜�̈ʒu�𒲐߂���
							filename = "TieUnderHalf1";							// �摜�t�@�C�����̐ݒ�
						}
						else
						{
							// �����̌��������������ꍇ�A�^�C�摜�͉����̏�ɕ`�悷��
							p = getScoreCoordinate(score, 0, i);	// �܂������̍��W�𓾂�
							p.X += 3;								// ���������̍��W����ɂ���
							p.Y -= 10;								// �摜�̈ʒu�𒲐߂���
							filename = "TieTopHalf1";				// �摜�t�@�C�����̐ݒ�
							
						}
					}

					// �����ĕ`�惊�X�g�ɒǉ�
					drawdata = new DrawData(filename, p.X, p.Y +yoffset);
					DrawList.Add(drawdata);
				}
				else if( (score.tie == 2) && (i%NotePerLine == 0) )
				{
					// �Q�s�ɓn�����^�C�̌㔼(�I�[��)�̕`��
					
					// �����̌����Ń^�C�̈ʒu���ς��
					if( this.CheckCodeDirection(score) == 0)
					{
						// �����̌������ゾ�����ꍇ�A�^�C�摜�͉����̉��ɕ`�悷��
						p = getScoreCoordinate(score, score.chord-1, i);	// �܂������̍��W�𓾂�
						p.X -= 12;											// ���������̍��W����ɂ���
						p.Y += 34;											// �摜�̈ʒu�𒲐߂���
						filename = "TieUnderHalf2";							// �摜�t�@�C�����̐ݒ�
					}
					else
					{
						// �����̌��������������ꍇ�A�^�C�摜�͉����̏�ɕ`�悷��
						p = getScoreCoordinate(score, 0, i);	// �܂������̍��W�𓾂�
						p.X -= 12;								// ���������̍��W����ɂ���
						p.Y -= 10;								// �摜�̈ʒu�𒲐߂���
						filename = "TieTopHalf2";				// �摜�t�@�C�����̐ݒ�	
					}

					// �����ĕ`�惊�X�g�ɒǉ�
					drawdata = new DrawData(filename, p.X, p.Y +yoffset);
					DrawList.Add(drawdata);
				}
				
				#endregion
				
				// �����̒�������ɐi�߂�
				i += (int)(score.length * 2);
			}
			
			// �I�[�̕`�� i���ő剹�����������ꍇ�A�y���̏I�[�ł��邽��
			if(i == this.MaxNote)
			{
				// �s��*67�����ĕ`��
				if(this.nowScore != muphic.ScoreScr.AnimalScoreMode.All)
				{
					drawdata = new DrawData( "End", EndXBegin, ScoreYBegin+12 + (int)(Math.Floor(i/NotePerLine) - 1) * ScoreYDifference );
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
				}
			}
		}
		
		
		/// <summary>
		/// �t���X�R�A���̕`��
		/// </summary>
		public void DrawAll()
		{
			// �t���X�R�A���͂܂Ƃ߂�J�b�R��`�悷��
			DrawData drawdata = new DrawData("Full", 64, 174);
			DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�

			// �I�t�Z�b�g+32���������������ꍇ�A�y���̏I�[�Ɣ��f
			if(this.offset+NotePerLine == this.MaxNote)
			{
				drawdata = new DrawData( "End_full", EndXBegin+1, ScoreYBegin+12 );
				DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
			}
		}
		
		
		/// <summary>
		/// �^����ꂽ�����̕\���ʒu�����肷�郁�\�b�h
		/// </summary>
		/// <param name="score">�ʒu�����肷�鉹��</param>
		/// <param name="num">�a�����Ԗڂ̉�����</param>
		/// <param name="i">�������X�g���Ԗڂ̉�����</param>
		/// <returns>�\���ʒu���W</returns>
		public Point getScoreCoordinate(Score score, int num, int i)
		{
			Point p = new Point(0,0);
			int line = i/NotePerLine;	// ���s�ڂ̉����Ȃ̂�
			int n = i%NotePerLine;		// �s���̉��Ԗڂ̉����Ȃ̂�
			
			if(score.code[0] == -1 )
			{
				// �x���̏ꍇ
				if(score.length == 0.5) p.Y = ScoreYBegin + 20 + line * ScoreYDifference;			// �����x��
				else if(score.length == 1)   p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// �l���x��
				else if(score.length == 1.5) p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// �t�_�l���x��
				else if(score.length == 2)   p.Y = ScoreYBegin + 24 + line * ScoreYDifference;		// �񕪋x��
				else if(score.length == 3)   p.Y = ScoreYBegin + 21 + line * ScoreYDifference;		// �t�_�񕪋x��
				else if(score.length == 4)   p.Y = ScoreYBegin + 19 + line * ScoreYDifference;		// �S�x��
			}
			else
			{
				// �x������Ȃ��ꍇ ���K���Ƃ�y���W��ς���
				p.Y = ScoreYBegin-3 + score.code[num]*4-4 + line * ScoreYDifference;
	
				if( this.CheckCodeDirection(score) == 1 )
				{
					// �V����̉��������畄�������̉����ɂȂ�
					p.Y += 22;
					p.X += 3;
					
					//  �ׂ荇�킹�̘a���ŃY���������������ꍇ ���ɂ��炷
					if( this.CheckChordMode(score, num) ) p.X -= 8;

					// �Y�����a���Ȃ�΍X�ɉE�֏������炵�ăo�����X����
					int j=0;
					for(; j<score.chord; j++) if(this.CheckChordMode(score, j)) break;
					if(j != score.chord) p.X += 5;�@
				}
				else
				{
					// ��������(�ʏ�)�ŗׂ荇�킹�̘a���ŃY���������������ꍇ �E�ɂ��炷
					if( this.CheckChordMode(score, num) ) p.X += 8;
				}
			}

			// ������x���W������
			// 1�Ԗڂ̉������W189px + ���ߐ�(0�`3)*181px + ���ߓ��̉�����(0�`7)*16px
			p.X += NoteXBegin + (n/NotePerBar * BarXDifference) + (n%NotePerBar * NoteXDifference);
			
			// �����ȊO�̏ꍇ�͂��ꂼ��x���W�����ăo�����X����
			if(score.length == 1) p.X += (int)Math.Round(NoteXDifference / 2.0, 0);
			else if(score.length == 1.5) p.X += (int)Math.Round((NoteXDifference / 2.0) * 2, 0);
			else if(score.length == 2)   p.X += (int)Math.Round((NoteXDifference / 2.0) * 3, 0);
			else if(score.length == 3)   p.X += (int)Math.Round((NoteXDifference / 2.0) * 5, 0);
			else if(score.length == 4)   p.X += (int)Math.Round((NoteXDifference / 2.0) * 7, 0);

			// ���̃h�������ꍇ�������ɂ��炷
			if(score.code[num] == 8) p.X -= 3;

			// �X�ɁA�Q�s�ڈȍ~�̂P���ߖڂ̏ꍇ 1px�����ɂ��炵��4/4���q�̋󔒕����𖄂߂�
			if( (line != 0) && (n < NotePerBar) ) p.X -= (NotePerBar - n) * 2;
			
			return p;
		}
		

		/// <summary>
		/// �^����ꂽ�����̉摜�����肷�郁�\�b�h
		/// </summary>
		/// <param name="score">�摜�����肷�鉹��</param>
		/// <param name="num">�a�����Ԗڂ̉�����</param>
		/// <returns>�\���摜������</returns>
		public String getScoreImage(Score score, int num)
		{
			// �a����ڂ̉���(code[0])���x���������ꍇ
			if(num == 0 && score.code[0] == -1)
			{
				// �����x���Ɍ���
				if(score.length == 0.5) return "EighthRest";

				// �l���x���Ɍ���
				if(score.length == 1) return "QuarterRest";

				// �t�_�l���x���Ɍ���
				if(score.length == 1.5) return "PQuarterRest";

				// �t�_�񕪋x���Ɍ���
				if(score.length == 3) return "PHalfRest";

				// ����ȊO�͓񕪋x�����S�x��
				return "AllRest";
			}
			
			if(score.length == 0.5)
			{
				// ��������
				
				if(num == 0 && this.CheckCodeDirection(score) == 0)
				{
					// ������̏ꍇ�A�����̉摜�͘a����ԏ�ł����g��Ȃ�

					// ���̃h��������h�̉����ɂ���
					if(score.code[num] == 8) return "EighthNotes_do";

					// �ׂ荇�킹�̘a����������Y��������������
					if( this.CheckChordMode(score, num) ) return "EighthNotes_wa";

					// �����ɂ�����Ȃ���Ε��ʂ̔�������
					return "EighthNotes";
				}
				if(num == score.chord-1 && this.CheckCodeDirection(score) == 1)
				{
					// �������̏ꍇ�A�����̉摜�͘a����ԉ��ł����g��Ȃ�
					
					// �ׂ荇�킹�̘a����������Y��������������
					if( this.CheckChordMode(score, num) ) return "EighthNotes_wa_";
					
					// �����ɂ�����Ȃ���Ε��ʂ̔�������
					return "EighthNotes_";
				}
			}

			// ����ȊO�͎l�������ɂȂ�܂���

			// ���̃h��������h�̉����ɂ���
			if(score.code[num] == 8) return "QuarterNotes_do";

			// �V����̉��ō\������Ă���ꍇ�͕��������ɂȂ�
			if( this.CheckCodeDirection(score) == 1 )
			{
				// �ׂ荇�킹�̘a����������Y��������������
				if( this.CheckChordMode(score, num) ) return "QuarterNotes_wa_";
				
				// ����ȊO�͕��ʂ̕���������
				return "QuarterNotes_";
			}
			
			// �ׂ荇�킹�̘a����������Y��������������
			if( this.CheckChordMode(score, num) ) return "QuarterNotes_wa";
			
			// �����ɂ�����Ȃ���Ε��ʂ̎l������
			return "QuarterNotes";
		}
		
		
		/// <summary>
		/// �^����ꂽ�����̕����̌��������߂郁�\�b�h
		/// </summary>
		/// <param name="score">�Ώۂ̉���(�a���܂�)</param>
		/// <returns>
		/// �����̌���
		/// -1:�x��
		/// 0:��(�ʏ�)
		/// 1:��(�V�E�h�݂̂̉�/�a��)
		/// </returns>
		public int CheckCodeDirection(Score score)
		{
			// ���������x����������
			if(score.code[0] == -1) return -1;

			// ���������ɂȂ�����́A�V�E�h(������)�ł��邱��
			for(int i=0; i<score.code.Length; i++)
			{
				// �a�����ɃV��艺�̉����������� �����͏�(�ʏ�)�ɂȂ�
				if(score.code[i] > 2) return 0;
			}

			// ��̃��[�v�ň���������Ȃ���Ε����͉��ɂȂ�
			return 1;
		}
		
		
		/// <summary>
		/// �ׂ荇�킹�̘a���𔻒肷�郁�\�b�h
		/// </summary>
		/// <param name="score"></param>
		/// <param name="num"></param>
		/// <returns>�Y���������Ȃ�true</returns>
		public bool CheckChordMode(Score score, int num)
		{
			// �a������Ȃ������綴�!
			if(score.chord == 1) return false;
			
			if(this.CheckCodeDirection(score) == 0)
			{
				// �����̌�������(�ʏ�)�Ȃ��

				// �a����ԉ��̉��̓Y���������ɂ͂Ȃ�Ȃ�
				if(num == 2) return false;

				// �a�����Q�̏ꍇ
				if(score.chord == 2)
				{
					// �a���̏�̉��ŗׂ荇�킹��������Y���������ɂȂ�
					if( (num == 0) && (score.code[1] - score.code[0] == 1) ) return true;
					
					// ����ȊO�͕��ʂ̉���
					return false;
				}
				
				// �a�����R�̏ꍇ
				if(score.chord == 3)
				{
					// �R�̉��S���ׂ荇�킹�������ꍇ
					if( (score.code[2]-score.code[1] == 1) && (score.code[1]-score.code[0] == 1) )
					{
						// �^�񒆂̉��ł���΃Y���������ɂȂ�
						if(num == 1) return true;

						// ����ȊO�͕��ʂ̉���
						return false;
					}

					// �a���̏�̉��ŗׂ荇�킹��������Y���������ɂȂ�
					if( (num == 0) && (score.code[0] - score.code[1] == 1) ) return true;
					
					// �a���Q�Ԗڂ̉��ŁA�㉺�Ɨׂ肠�킹�������炻�ꂼ��Y���������ɂȂ�
					if( (num == 1) && (score.code[1] - score.code[0] == 1) ) return true;
					if( (num == 1) && (score.code[2] - score.code[1] == 1) ) return true;
					
					// ����ȊO�͕��ʂ̉���
					return false;
				}
			}
			else if(this.CheckCodeDirection(score) == 1)
			{
				// ��������������(�V�ƃh)�̏ꍇ
				// �a�������Q�̃V�ƃh�ȊO���肦�Ȃ�
				
				// �a���Q�Ԗڂ̉��ŁA��Ɨׂ荇�킹��������Y���������ɂȂ�
				if( (num == 1) && (score.code[1] - score.code[0] == 1) ) return true;
				
				// ����ȊO�͑S�ĕ��ʂ̉���
				return false;
			}
			
			// ���ƑS�����!
			return false;
		}
		
		#endregion
		
		#region �������X�g�������\�b�h�Q

		/// <summary>
		/// AnimalList���炻�ꂼ��̓����̉������X�g�����
		/// </summary>
		/// <param name="animalmode">���X�g�쐬�̑Ώۂ̓���</param>
		public void CreateScoreList(muphic.ScoreScr.AnimalScoreMode animalmode)
		{
			int i,n;
			int temp=-1;	// �a���J�E���g�p�ϐ�
			Animal animal = new Animal(0,0);
			Score[] ScoreList = new Score[this.MaxNote];
			for(i=0; i<this.MaxNote; i++) ScoreList[i] = new Score();
			i=n=0;

			// �������X�g�Ɋ܂܂�Ă���Ώۂ̓����̐����擾
			int num = this.CheckAnimalNumber(animalmode);

			#region �������X�g����

			// 0�Ԗڂ̓����̃f�[�^���擾 �Ώۂ̓����Ƀq�b�g����܂Ńf�[�^�擾�������� 
			if(num > 0)
			{
				animal = ((Animal)parent.AnimalList[n]);
				while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);
			}

			// �������X�g�쐬���C�����[�v
			while(num > 0)
			{
				// �y���ʒui�Ɠ����ʒuplace����v���A���Ώۂ̓����ł��邩�ǂ��� 
				if( i==animal.place && animal.AnimalName.Equals(animalmode.ToString()) )
				{
					// ��L�����𖞂������ꍇ�A���K���y���ɃR�s�[
					ScoreList[i].AddCode(animal.code);
					
					// �y���ʒui���O�Ɠ����������ꍇ�A�a���Ɣ���
					if(temp == i) ScoreList[i].chord++; 
					temp = i;

					// ����ɁA���X�g���̑Ώۓ��������P���炷
					// ���X�g���̑Ώۓ�������0�ɂȂ����烋�[�v�I��
					if(--num == 0) break;

					// ���̓����f�[�^���擾 �Ώۂ̓����Ƀq�b�g����܂Ńf�[�^�擾��������
					animal = ((Animal)parent.AnimalList[++n]);
					while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);

					// ���̓��������̊y���ʒu�ɖ����ꍇ�A�l�������ɂ���
					if(animal.place > i+1) ScoreList[i].length = 1;

					// �a�������ő�l�ɂȂ����狭���I��i��i�߂�
					if(ScoreList[i].chord >= MaxChord) { i++; temp=0; }
				}
				else
				{
					// �����𖞂����Ȃ������ꍇ��i��i�߂�
					i++;
					temp = 0;
				}
			}
			
			// �Ō�̉������l���ɂ���(�����s�̍Ō�̉�����������A�^�C�����ł���1�s���Ă̂��A���Ȃ�Ŗ��������Ⴂ�܂��傤)
			//if( (i % NotePerLine != NotePerLine-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;
			if( (i % this.MaxNote != this.MaxNote-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;
			
			#endregion
			
			#region �^�C�E�x���̔��� ����(�` ver.0.10.1)
//			
//			i = -1;
//			while(++i < this.MaxNote)
//			{
//				// �x���ł͂Ȃ������ꍇ
//				if(ScoreList[i].code[0] != -1)
//				{
//					// �^�C�̔���
//					// �l��������������
//					if( (ScoreList[i].length >= 1) && (i%NotePerBar == NotePerBar-1) )
//					{
//						ScoreList[i+1].length = ScoreList[i].length - 0.5;	// ���̉����̒����𔪕���
//						ScoreList[i+1].code[0] = ScoreList[i].code[0];		// �������R�s�[
//						ScoreList[i+1].code[1] = ScoreList[i].code[1];		// �������R�s�[
//						ScoreList[i+1].code[2] = ScoreList[i].code[2];		// �������R�s�[
//						ScoreList[i].length = 0.5;							// �����̒����𔪕���
//						ScoreList[i].tie = true;							// �^�C�t���Oon
//					}
//
//					// i���x���łȂ���Α�����
//					i += (int)(ScoreList[i].length * 2) - 1;
//					continue;
//				}
//
//				// �S�x������
//				// ���߂̍ŏ��̉����Ŕ�������{
//				if( (i%NotePerBar == NotePerBar-4*2) && CheckRest(ScoreList, i, 8) )
//				{
//					ScoreList[i].length = 4;	// ���ߓ��̂��ׂẲ������x����������A�S�x���ɂ���
//					i += 7; continue;			// �ꏬ�ߕ���ɐi�߂�
//				}
//
//				// �t�_�񕪋x������
//				// ���ߓ���7�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
//				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-3*2) && CheckRest(ScoreList, i, 6) )
//				{
//					ScoreList[i].length = 3;	// �t�_�񕪋x��
//					i += 5; continue;			// �ꕪ��ɐi�߂�
//				}
//				
//				// �񕪋x������
//				// ���ߓ���5�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
//				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-2*2) && CheckRest(ScoreList, i, 4) )
//				{
//				
//					ScoreList[i].length = 2;	// �񕪋x��
//					i += 3; continue;			// �񕪐�ɐi�߂�
//				}
//
//				// �t�_�l���x������
//				// ���ߓ���6�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
//				if( (i%NotePerBar <= NotePerBar-1.5*2) && CheckRest(ScoreList, i, 3) )
//				{
//				
//					ScoreList[i].length = 1.5;	// �t�_�l���x��
//					i += 2; continue;			// �O����ɐi�߂�
//				}
//
//				// �l���x������
//				// ���ߓ���7�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
//				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-1*2) && CheckRest(ScoreList, i, 2) )
//				{
//					ScoreList[i].length = 1;	// �l���x��
//					i += 1; continue;			// �l����ɐi�߂�
//				}
//				
//				// ����ɂ��Y�����Ȃ���΁A8���x���ƂȂ�
//				//i += (int)(ScoreList[i].length * 2) - 1;
//			}
//			
			#endregion
			
			#region �^�C�E�x���̔��� (ver.0.10.2 �`)

			i = -1;
			while(++i < this.MaxNote)
			{
				// �x���ł͂Ȃ������ꍇ
				if(ScoreList[i].code[0] != -1)
				{
					// �^�C�̔���
					// �l��������������
					if( (ScoreList[i].length >= 1) && (i%NotePerBar == NotePerBar-1) )
					{
						for(int j=0; j<MaxChord; j++)
							ScoreList[i+1].code[j] = ScoreList[i].code[j];	// ������
						ScoreList[i+1].length = ScoreList[i].length - 0.5;	// ���̉����̒����𔪕���
						ScoreList[i+1].chord = ScoreList[i].chord;			// �a�����̃R�s�[
						ScoreList[i].length = 0.5;							// �����̒����𔪕���
						ScoreList[i].tie = 1;								// �^�C�t���Oon
						ScoreList[i+1].tie = 2;								// �^�C�t���Oon
					}

					// i���x���łȂ���Α�����
					i += (int)(ScoreList[i].length * 2) - 1;
					continue;
				}
				
				// i���܂߂��A�������x���̐� �ȉ���
				//  restnum == 2 �l���x���ɂȂ��
				//  restnum == 4 �񕪋x���ɂȂ��
				//  restnum == 8 �S�x���ɂȂ��
				int restnum = this.CheckRestNum(ScoreList, i);

				switch(i%NotePerBar)
				{
					case 0:
						if( restnum == 8 )
						{
							// ���ߓ����S�ċx���������ꍇ
							ScoreList[i].length = 4;	// ���ߓ��̂��ׂẲ������x����������A�S�x���ɂ���
							i += NotePerBar-1;			// ���̏��߂֐i�߂�
							continue;			
						}
						goto case 2;
					case 1:
						if( restnum >= 7 )
						{
							ScoreList[i+1].length = 3;	// �����x��+�t�_�񕪋x��
							i += 6;						// ���̏��߂֐i�߂�
							continue;
						}
						goto case 2;
					case 2:
						if( restnum >= 6 )
						{
							ScoreList[i].length = 3;	// �t�_�񕪋x��
							i += 5;						// �ꕪ��ɐi�߂�
							continue;
						}
						goto case 4;
					case 3:
						if( restnum >= 5 )
						{
							ScoreList[i+1].length = 2;	// �����x��+�񕪋x��
							i += 4;						// ���̏��߂֐i�߂�
							continue;
						}
						goto case 4;
					case 4:
						if( restnum >= 4 )
						{
							ScoreList[i].length = 2;	// �񕪋x��
							i += 3;						// �񕪕���ɐi�߂�
							continue;
						}
						goto case 5;
					case 5:
						if( restnum >= 3 )
						{
							ScoreList[i].length = 1.5;	// �t�_�l���x��
							i += 2;						// �񕪕���ɐi�߂�
							continue;
						}
						goto case 6;
					case 6:
						if( restnum >= 2 )
						{
							ScoreList[i].length = 1;	// �l���x��
							i += 1;						// �񕪕���ɐi�߂�
							continue;
						}
						goto case 7;
					case 7:
                        break;	// ����ɂ��Y�����Ȃ���΁A�����x���ƂȂ�
					default:
						// �����ɓ��B����ꍇ�A�P�ʏ��߂�����̍ő剹������8�łȂ��\����
						System.Console.WriteLine("NotePerBar != 8");
						break;
				}
			}
			#endregion
			
			#region ���������z��̃��X�g��p�ӂ��ꂽ�e�������Ƃ̃t�B�[���h�ɃR�s�[����
			switch(animalmode)
			{
				case AnimalScoreMode.Sheep:
					for(i=0; i<this.MaxNote; i++) this.SheepScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Rabbit:
					for(i=0; i<this.MaxNote; i++) this.RabbitScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Bird:
					for(i=0; i<this.MaxNote; i++) this.BirdScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Dog:
					for(i=0; i<this.MaxNote; i++) this.DogScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Pig:
					for(i=0; i<this.MaxNote; i++) this.PigScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Cat:
					for(i=0; i<this.MaxNote; i++) this.CatScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Voice:
					for(i=0; i<this.MaxNote; i++) this.VoiceScoreList.Add(ScoreList[i]);
					break;
				default:
					break;
			}
			#endregion
		}
		
		
		/// <summary>
		/// �w�肳�ꂽ�͈͂��S�ċx�����ǂ������`�F�b�N����
		/// </summary>
		/// <param name="data">�`�F�b�N���鉹�����X�g</param>
		/// <param name="i">�J�n�v�f�ԍ�</param>
		/// <param name="n">�`�F�b�N���鉹����</param>
		/// <returns>�͈͂̉����S�ċx���Ȃ�true �����łȂ��Ȃ�false</returns>
		public bool CheckRest(Score[] data, int i, int n)
		{
			int j=0;
			for(; j<n; j++)
			{
				if(data[i+j].code[0] != -1) return false;
			}
			return true;
		}
		
		
		/// <summary>
		/// �w�肳�ꂽ�v�f����̘A�������x���̐��𐔂���(�P�ʏ��ߕ��̂�)
		/// </summary>
		/// <param name="data">�`�F�b�N���鉹�����X�g</param>
		/// <param name="i">�J�n�v�f�ԍ�</param>
		/// <returns>�x���̐�</returns>
		public int CheckRestNum(Score[] data, int i)
		{
			int cnt = 0;	// �x���̐�
			int max = NotePerBar - i%NotePerBar;	// �`�F�b�N����ő吔 �P�ʏ��߂𒴂��Ȃ��悤�ɒ���

			while(cnt < max)
			{
				// �x���ȊO�̗v�f�𔭌�������
				if( data[i+cnt].code[0] != -1) return cnt;
				cnt++;
			}
			return cnt;
		}
		
		
		/// <summary>
		/// ���X�g���Ɏw�肵�������������܂܂�Ă��邩�`�F�b�N���郁�\�b�h
		/// </summary>
		/// <param name="mode"></param>
		/// <returns>�܂܂�Ă�����</returns>
		public int CheckAnimalNumber(muphic.ScoreScr.AnimalScoreMode mode)
		{
			int num=0;
			for(int i=0; i<parent.AnimalList.Count; i++)
			{
				Animal a = ((Animal)parent.AnimalList[i]);
				if(a.AnimalName.Equals(mode.ToString())) num++;
			}
			return num;
		}

		#endregion

		/// <summary>
		/// ��X�N���[���{�^�����������ۂ̓���
		/// </summary>
		public void UpScroll()
		{
			// �P�s�ڂ���ɂ͍s���Ȃ��悤�ɂ���
			if(this.offset >= 32) this.offset -= 32;

			// �����čĕ`��
			this.ReDraw();
		}
		

		/// <summary>
		/// ���X�N���[���{�^�����������ۂ̓���
		/// </summary>
		public void DownScroll()
		{
			if(this.nowScore != AnimalScoreMode.All)
			{
				// �t���X�R�A�ł͖����ꍇ�A�y�����V�s�ȏ�ɂȂ������̂݉��ɃX�N���[����
				if(this.MaxNote > 192 && this.offset < this.MaxNote-32) this.offset += 32;
			}
			else
			{
				// �t���X�R�A�̏ꍇ
				if(this.offset < this.MaxNote-32) this.offset += 32;
			}

			// �����čĕ`��
			this.ReDraw();
		}
		

		/// <summary>
		/// �I�t�Z�b�g���N���A���郁�\�b�h
		/// ���ƂȂ�private�ɂ������������
		/// </summary>
		public void ClearOffset()
		{
			this.offset = 0;
		}
		
		#region �f�o�b�O�p���\�b�h�Q
		
		/// <summary>
		/// �������X�g���`�F�b�N���� ���Ă��ꗗ��\������
		/// ��Ƀf�o�b�O�p
		/// </summary>
		/// <param name="data">�ꗗ��\�����鉹�����X�g</param>
		/// <param name="length">���X�g�̒���</param>
		public void CheckScoreList(Score[] data, int length)
		{
			int i=0;

			for(i=0; i<length; ++i)
			{
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data[i].length, data[i].code[0], data[i].code[1], data[i].code[2]);
			}
		}
		public void CheckScoreList(ArrayList list)
		{
			int i=0;

			for(i=0; i<list.Count; ++i)
			{
				Score data = (Score)list[i];
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data.length, data.code[0], data.code[1], data.code[2]);
			}
		}
		
		
		/// <summary>
		/// �f�o�b�O�p���b�Z�[�W�o�̓��\�b�h
		/// </summary>
		/// <param name="str">���炩�̕�����</param>
		/// <param name="num">���炩�̒l</param>
		public void Debug(String str, int num)
		{
			System.Console.WriteLine(str+num);
		}
		/// <summary>
		/// �f�o�b�O�p���b�Z�[�W�o�̓��\�b�h
		/// </summary>
		/// <param name="str">���炩�̕�����</param>
		/// <param name="num">���炩�̒l</param>
		public void Debug(String str, double num)
		{
			System.Console.WriteLine(str+num);
		}

		#endregion
		
	}
	*/
	#endregion

	#region XGA ����@�\�t�� (ver.2.0.0�`)

	/// <summary>
	/// Score �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreMain : Screen
	{
		public ScoreScreen parent;
		public const int maxAnimals = 100;	// �z�u�\�ȓ����̐�
		private int offset;					// �`��J�n�̉�����32����(�P�s������)���炷

		// �e�������Ƃ̉������X�g
		public ArrayList SheepScoreList = new ArrayList();
		public ArrayList RabbitScoreList = new ArrayList();
		public ArrayList BirdScoreList = new ArrayList();
		public ArrayList DogScoreList = new ArrayList();
		public ArrayList PigScoreList = new ArrayList();
		public ArrayList CatScoreList = new ArrayList();
		public ArrayList VoiceScoreList = new ArrayList();

		public int MaxNote;					// �S�������X�g�̉�����(8�Ŋ���Ə��ߐ��A32�Ŋ���ƍs���ɂȂ�)
		public AnimalScoreMode nowScore;	// ���ݕ\�����Ă��鉹�����X�g

		public ArrayList DrawList = new ArrayList();	// �`�悷��f�[�^�̃��X�g| _-)/

		# region �e��T�C�Y��`
		const int NotePerBar = 8;							// �P�ʏ��߂�����̍ő剹���� ���ύX����Ɠ���ł��Ȃ��Ǝv��
		const int BarPerLine = 4;							// �P�ʍs������̏��ߐ�
		const int NotePerLine = NotePerBar * BarPerLine;	// �P�ʍs������̍ő剹����
		const int LinePerPage = 7;							// ��ʂɕ\���ł���ܐ����̍ő�s��
		const int NotePerPage = NotePerLine * LinePerPage;	// ��ʂɕ\���ł��鉹���̍ő吔
		const int MaxChord = 3;								// �a���ő吔
		const int NoteXBegin = 189;							// ����x���W�̊(�s1�Ԗڂ̉�����x���W)(px ��XGA�C���ς�)
		const int NoteYBegin = 141;							// ����y���W�̊(px)
		const int NoteXDifference = 21;						// �������m��x���W�̍�(��XGA�C���ς�)
		const int BarXDifference = 181;						// ���ߓ��m��x���W�̍�(px ��XGA�C���ς�)
		const int ScoreXBegin = 122;						// �ܐ���x���W�̊(�ܐ�����x���W)(px ��XGA�C���ς�)
		const int ScoreYBegin = 173;						// �ܐ���y���W�̊(1�s�ڂ̌ܐ�����y���W)(px ��XGA�C���ς�)
		const int ScoreYDifference = 67;					// �ܐ������m��y���W�̍�(px ��XGA�C���ς�)
		const int EndXBegin = 906;							// �I�[����x���W(px ��XGA�C���ς�)

		const int BigImageX = 600;			// ���܂��摜x���W
		const int BigImageY = 450;			// ���܂��摜y���W
		# endregion

		/// <summary>
		/// ScoreMain �R���X�g���N�^
		/// </summary>
		/// <param name="screen">��ʉ�ʃN���X</param>
		public ScoreMain(ScoreScreen screen)
		{
			this.parent = screen;
			this.offset = 0;

			// �Ȃ��ĉ��y���͗r�{�^���ȊO�͕\�������Ȃ�
			if (this.parent.ParentScreenMode == muphic.ScreenMode.LinkScreen || this.parent.ParentScreenMode == muphic.ScreenMode.LinkMakeScreen)
			{
				this.nowScore = AnimalScoreMode.Sheep;
				this.parent.scores.sheep.State = 1;

				this.parent.scores.all.Visible = false;
				this.parent.scores.bird.Visible = false;
				this.parent.scores.cat.Visible = false;
				this.parent.scores.dog.Visible = false;
				this.parent.scores.pig.Visible = false;
				this.parent.scores.rabbit.Visible = false;
				this.parent.scores.voice.Visible = false;
			}
			else
			{
				this.nowScore = AnimalScoreMode.All;
				this.parent.scores.all.State = 1;
			}

			#region �摜��ĳ۰� ( ߁��)�
			DrawManager.Regist("QuarterNotes", 0, 0, "image\\ScoreXGA\\note\\4buonpu.png");			// �l������-������
			DrawManager.Regist("QuarterNotes_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_.png");		// �l������-������
			DrawManager.Regist("QuarterNotes_do", 0, 0, "image\\ScoreXGA\\note\\4buonpu_do.png");	// �l������-�h
			DrawManager.Regist("QuarterNotes_wa", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa.png");	// �l������-������a���p
			DrawManager.Regist("QuarterNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa_.png");	// �l������-�������a���p
			DrawManager.Regist("EighthNotes", 0, 0, "image\\ScoreXGA\\note\\8buonpu.png");			// ��������-������
			DrawManager.Regist("EighthNotes_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_.png");		// ��������-������
			DrawManager.Regist("EighthNotes_do", 0, 0, "image\\ScoreXGA\\note\\8buonpu_do.png");	// ��������-�h
			DrawManager.Regist("EighthNotes_wa", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa.png");	// ��������-������a���p
			DrawManager.Regist("EighthNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa_.png");	// ��������-�������a���p

			DrawManager.Regist("AllRest", 0, 0, "image\\ScoreXGA\\note\\zenkyuhu.png");				// �S�x��/�񕪋x��
			DrawManager.Regist("PHalfRest", 0, 0, "image\\ScoreXGA\\note\\2bukyuhu_huten.png");		// �t�_�񕪋x��
			DrawManager.Regist("PQuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu_huten.png");	// �t�_�l���x��
			DrawManager.Regist("QuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu.png");			// �l���x��
			DrawManager.Regist("EighthRest", 0, 0, "image\\ScoreXGA\\note\\8bukyuhu.png");			// �����x��

			DrawManager.Regist("Meter", 0, 0, "image\\ScoreXGA\\note\\hyousi.png");					// 4/4���q�L��
			DrawManager.Regist("End", 0, 0, "image\\ScoreXGA\\note\\end.png");						// �I�[
			DrawManager.Regist("End_full", 0, 0, "image\\ScoreXGA\\note\\end_full.png");			// �I�[-�t���X�R�A�p
			DrawManager.Regist("Staff", 0, 0, "image\\ScoreXGA\\score\\gosen.png");					// �ܐ���
			DrawManager.Regist("Line", 0, 0, "image\\ScoreXGA\\score\\syousetu.png");				// ���ߋ�؂��
			DrawManager.Regist("Full", 0, 0, "image\\ScoreXGA\\score\\full_line.png");				// �t���X�R�A

			DrawManager.Regist("TieTop", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t.png");			// �^�C(��)
			DrawManager.Regist("TieTopHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h1.png");	// �^�C(��E�n�[)
			DrawManager.Regist("TieTopHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h2.png");	// �^�C(��E�I�[)
			DrawManager.Regist("TieUnder", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u.png");			// �^�C(��)
			DrawManager.Regist("TieUnderHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h1.png");	// �^�C(���E�n�[)
			DrawManager.Regist("TieUnderHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h2.png");	// �^�C(���E�I�[)

			DrawManager.Regist("SheepBig", 0, 0, "image\\ScoreXGA\\omake\\Sheep_big.png");			// �w�i�¼��
			DrawManager.Regist("RabbitBig", 0, 0, "image\\ScoreXGA\\omake\\Rabbit_big.png");		// �w�i�e
			DrawManager.Regist("BirdBig", 0, 0, "image\\ScoreXGA\\omake\\Bird_big.png");			// �w�i���o�[�h
			DrawManager.Regist("DogBig", 0, 0, "image\\ScoreXGA\\omake\\Dog_big.png");				// �w�iDog
			DrawManager.Regist("PigBig", 0, 0, "image\\ScoreXGA\\omake\\Pig_big.png");				// �w�i�x�C�u
			DrawManager.Regist("CatBig", 0, 0, "image\\ScoreXGA\\omake\\Cat_big.png");				// �w�i�ʂ�
			DrawManager.Regist("VoiceBig", 0, 0, "image\\ScoreXGA\\omake\\Voice_big.png");			// �w�i���J������

			DrawManager.Regist("Logo", 730, 35, "image\\ScoreXGA\\logo.png");						// ����p���S
			#endregion

			// �l�̏������Ɖ������X�g�̾���� ( ߁��)�
			this.CreateScoreListAll();

			// �y�����ޮ���� ( ߁��)�
			this.ReDraw();
		}


		/// <summary>
		/// ������s�����\�b�h
		/// </summary>
		public void Print()
		{
			int page = 0;	// �摜���������Regist����ۂ̃y�[�W
			int allpage;	// ������鑍�y�[�W��

			if (this.nowScore == muphic.ScoreScr.AnimalScoreMode.All)
			{
				// �t���X�R�A���́A�ő剹��������1�s���̉��������������������y�[�W���ɂȂ�
				allpage = (int)System.Math.Ceiling((double)this.MaxNote / NotePerLine);
			}
			else
			{
				// �t���X�R�A�ȊO�̎��́A�ő剹��������1�y�[�W���̉��������������������y�[�W���ɂȂ�
				allpage = (int)System.Math.Ceiling((double)this.MaxNote / NotePerPage);
			}

			// ���݂̃I�t�Z�b�g���o�b�N�A�b�v
			int offset_backup = this.offset;

			// �I�t�Z�b�g���O�ɂ���
			this.offset = 0;

			do
			{
				// �I�t�Z�b�g�ύX��Ȃ̂ŕ`�惊�X�g�X�V
				this.ReDraw();

				// ����̃y�[�W���Z�b�g
				muphic.PrintManager.ChangePage(++page);

				// �w�i�̃Z�b�g
				muphic.PrintManager.Regist(parent.scorewindow.ToString(), 1);
				muphic.PrintManager.Regist("Logo");

				// Draw���Ɠ����v�̂ŁA�������摜��o�^���Ă���
				for (int i = 0; i < this.DrawList.Count; i++)
				{
					DrawData data = (DrawData)DrawList[i];
					muphic.PrintManager.Regist(data.Image, data.x, data.y);
				}

				// �y�[�W�������o�^
				muphic.PrintManager.RegistString("�y�[�W " + page.ToString() + " / " + allpage.ToString(), 830, 690, 16);

				// �^�C�g���̓o�^
				string title = this.parent.tarea.ScoreTitle;						// �薼�\���̈悩��Ȗ��������Ă���
				if (title == null || title == "") title = "�u�����炵�����傭�v";	// �ۑ��_�C�A���O�̃^�C�g��������Ȃ�΁A�������ŏ���Ɍ��߂�
				else title = "�u" + title + "�v";									// �w�x������
				muphic.PrintManager.RegistString("�����߂�", 40, 40, 14);
				muphic.PrintManager.RegistString(title, 65, 70, 24);

				// �t���X�R�A���͉��ւ̃X�N���[�����K�v
				// �\�Ȍ��艺�փX�N���[�����Ă����A���̓s�x�V�����y�[�W�Ɉ�����Ă���
			}
			while (this.DownScroll());

			// ����J�n
			muphic.PrintManager.Print(false);
			System.Console.WriteLine("�y����� " + allpage.ToString() + "�y�[�W");

			// �I�t�Z�b�g�����ɖ߂�
			this.offset = offset_backup;
			this.ReDraw();
		}


		/// <summary>
		/// �`�惁�\�b�h
		/// �`�惊�X�g�ɓo�^���ꂽ�摜�ƍ��W��ǂݏo�������Ȃ�ŏd���Ȃ��n�Y
		/// </summary>
		public override void Draw()
		{
			base.Draw();

			// �`�惊�X�g����`�悷��f�[�^��ǂݏo��
			for (int i = 0; i < this.DrawList.Count; i++)
			{
				DrawData data = (DrawData)DrawList[i];
				muphic.DrawManager.Draw(data.Image, data.x, data.y);
			}
		}

		/// <summary>
		/// �`�惊�X�g���Đ������郁�\�b�h
		/// </summary>
		public void ReDraw()
		{
			DrawData drawdata;

			// �����̕`�惊�X�g���N���A����
			this.DrawList.Clear();

			#region DrawScore���\�b�h���`�惊�X�g���Đ�������
			switch (this.nowScore)
			{
				case AnimalScoreMode.All:
					this.DrawScore(this.SheepScoreList, 1, 0);
					this.DrawScore(this.RabbitScoreList, 1, ScoreYDifference);
					this.DrawScore(this.BirdScoreList, 1, ScoreYDifference * 2);
					this.DrawScore(this.DogScoreList, 1, ScoreYDifference * 3);
					this.DrawScore(this.PigScoreList, 1, ScoreYDifference * 4);
					this.DrawScore(this.CatScoreList, 1, ScoreYDifference * 5);
					this.DrawScore(this.VoiceScoreList, 1, ScoreYDifference * 6);
					this.DrawAll();		// �t���X�R�A��p�摜�̕`��
					break;
				case AnimalScoreMode.Sheep:
					drawdata = new DrawData("SheepBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.SheepScoreList, LinePerPage, 0);		// �`�惊�X�g���¼�݂̊y�����Z�b�g
					break;
				case AnimalScoreMode.Rabbit:
					drawdata = new DrawData("RabbitBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.RabbitScoreList, LinePerPage, 0);		// �`�惊�X�g�ɓe�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Bird:
					drawdata = new DrawData("BirdBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.BirdScoreList, LinePerPage, 0);			// �`�惊�X�g�ɒ��o�[�h�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Dog:
					drawdata = new DrawData("DogBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.DogScoreList, LinePerPage, 0);			// �`�惊�X�g�Ɍ�Dog�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Pig:
					drawdata = new DrawData("PigBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.PigScoreList, LinePerPage, 0);			// �`�惊�X�g�Ƀx�C�u�̊y�����Z�b�g
					break;
				case AnimalScoreMode.Cat:
					drawdata = new DrawData("CatBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.CatScoreList, LinePerPage, 0);			// �`�惊�X�g�ɂʂ��̊y�����Z�b�g
					break;
				case AnimalScoreMode.Voice:
					drawdata = new DrawData("VoiceBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.VoiceScoreList, LinePerPage, 0);		// �`�惊�X�g�ɳޫ���̊y�����Z�b�g
					break;
				default:
					break;
			}
			#endregion
		}

		/// <summary>
		/// �S�Ẳ������X�g�𐶐����郁�\�b�h
		/// </summary>
		public void CreateScoreListAll()
		{
			int max;
			try
			{
				// �������X�g�̍Ō�̓����̈ʒu����A�S�������X�g�ɂ�����Œ��̈ʒu�𓾂�
				Animal animal = ((Animal)parent.AnimalList[parent.AnimalList.Count - 1]);
				max = animal.place;
			}
			catch (Exception) // ����0�C�̏�ԂŌĂяo���Ɣ͈͊O�Q�Ƃ��������Ă��܂����ߖ�����c
			{
				max = 0;
			}

			// ���̒l����A�������X�g�̉��������Z�o(32�̔{���ɂȂ�悤����)
			this.MaxNote = max + (NotePerLine - max % NotePerLine);

			// �e�������X�g��������
			this.SheepScoreList.Clear();
			this.RabbitScoreList.Clear();
			this.BirdScoreList.Clear();
			this.DogScoreList.Clear();
			this.PigScoreList.Clear();
			this.CatScoreList.Clear();
			this.VoiceScoreList.Clear();

			this.CheckScoreList(this.SheepScoreList);

			// �������X�g�̾���� ( ߁��)�
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Sheep);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Rabbit);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Bird);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Dog);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Pig);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Cat);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Voice);
		}

		#region �y���`�惊�X�g�������\�b�h�Q

		/// <summary>
		/// �`�惊�X�gDrawList�Ɋy���f�[�^��o�^���郁�\�b�h
		/// </summary>
		/// <param name="data">�`�悷�鉹�����X�g</param>
		/// <param name="mode">�`�悷��s��</param>
		/// <param name="yoffset">�t���X�R�A�p y���W�I�t�Z�b�g</param>
		public void DrawScore(ArrayList data, int mode, int yoffset)
		{
			int i = this.offset, j, end;
			Point p = new Point(0, 0);
			String filename;
			DrawData drawdata;

			// ���[�v�I�������̐ݒ�
			end = i + mode * NotePerLine;
			if (end > NotePerPage) end = NotePerPage;	// �������U�s���𒴂����ꍇ�͂U�s�܂łƂ���
			if (end > data.Count) end = data.Count;		// ����ɉ������X�g�̉������𒴂����ꍇ�͉������ɂ���

			yoffset -= (this.offset / NotePerLine) * ScoreYDifference;

			// offset����X�^�[�g
			while (i < end)
			{
				Score score = (Score)data[i];

				// �s�̍ŏ��̏��߂̕`�掞�Ɍܐ������`�悷��
				if (i % NotePerLine == 0)
				{
					// y���W��1�s��134px�{�s���~67px
					drawdata = new DrawData("Staff", ScoreXBegin, ScoreYBegin + (int)(Math.Floor(0.0 + i / NotePerLine) * ScoreYDifference) + yoffset);
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�

					if (i / NotePerLine == 0)
					{
						// ����ɁA1�s�ڂł�4/4���q�̕`��
						drawdata = new DrawData("Meter", ScoreXBegin + 40, ScoreYBegin + 13 + (int)(Math.Floor(0.0 + i / NotePerLine) * ScoreYDifference) + yoffset);
						DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
					}
				}

				// �����`��
				for (j = 0; j < 3; j++)
				{
					// �x���̘a���͍݂�܂��� �a���Q�ڈȍ~�ɉ������Ȃ���Ύ��ɐi�ނ悤�ɂ���
					if ((j == 1 && score.code[1] == -1) || (j == 2 && score.code[2] == -1)) break;

					p = getScoreCoordinate(score, j, i);	// ���W�Q�b�c��
					drawdata = new DrawData(getScoreImage(score, j), p.X, p.Y + yoffset);
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
				}

				#region �^�C�̕`��

				if (score.tie == 1)
				{
					if (i % NotePerLine != NotePerLine - 1)
					{
						// �s�̍Ō�łȂ������ꍇ�A���ʂɃ^�C�摜��`��

						// �����̌����Ń^�C�̈ʒu���ς��
						if (this.CheckCodeDirection(score) == 0)
						{
							// �����̌������ゾ�����ꍇ�A�^�C�摜�͉����̉��ɕ`�悷��
							p = getScoreCoordinate(score, score.chord - 1, i);	// �܂������̍��W�𓾂�
							p.X += 2;											// ���������̍��W����ɂ���
							p.Y += 34;											// �摜�̈ʒu�𒲐߂���
							filename = "TieUnder";								// �摜�t�@�C�����̐ݒ�
						}
						else
						{
							// �����̌��������������ꍇ�A�^�C�摜�͉����̏�ɕ`�悷��
							p = getScoreCoordinate(score, 0, i);	// �܂������̍��W�𓾂�
							p.X += 3;								// ���������̍��W����ɂ���
							p.Y -= 10;								// �摜�̈ʒu�𒲐߂���
							filename = "TieTop";					// �摜�t�@�C�����̐ݒ�
						}
					}
					else
					{
						// �s�̍Ōゾ�����ꍇ�A�O��(�n�[��)�̂ݕ`��

						// �����̌����Ń^�C�̈ʒu���ς��
						if (this.CheckCodeDirection(score) == 0)
						{
							// �����̌������ゾ�����ꍇ�A�^�C�摜�͉����̉��ɕ`�悷��
							p = getScoreCoordinate(score, score.chord - 1, i);	// �܂������̍��W�𓾂�
							p.X += 2;											// ���������̍��W����ɂ���
							p.Y += 34;											// �摜�̈ʒu�𒲐߂���
							filename = "TieUnderHalf1";							// �摜�t�@�C�����̐ݒ�
						}
						else
						{
							// �����̌��������������ꍇ�A�^�C�摜�͉����̏�ɕ`�悷��
							p = getScoreCoordinate(score, 0, i);	// �܂������̍��W�𓾂�
							p.X += 3;								// ���������̍��W����ɂ���
							p.Y -= 10;								// �摜�̈ʒu�𒲐߂���
							filename = "TieTopHalf1";				// �摜�t�@�C�����̐ݒ�

						}
					}

					// �����ĕ`�惊�X�g�ɒǉ�
					drawdata = new DrawData(filename, p.X, p.Y + yoffset);
					DrawList.Add(drawdata);
				}
				else if ((score.tie == 2) && (i % NotePerLine == 0))
				{
					// �Q�s�ɓn�����^�C�̌㔼(�I�[��)�̕`��

					// �����̌����Ń^�C�̈ʒu���ς��
					if (this.CheckCodeDirection(score) == 0)
					{
						// �����̌������ゾ�����ꍇ�A�^�C�摜�͉����̉��ɕ`�悷��
						p = getScoreCoordinate(score, score.chord - 1, i);	// �܂������̍��W�𓾂�
						p.X -= 12;											// ���������̍��W����ɂ���
						p.Y += 34;											// �摜�̈ʒu�𒲐߂���
						filename = "TieUnderHalf2";							// �摜�t�@�C�����̐ݒ�
					}
					else
					{
						// �����̌��������������ꍇ�A�^�C�摜�͉����̏�ɕ`�悷��
						p = getScoreCoordinate(score, 0, i);	// �܂������̍��W�𓾂�
						p.X -= 12;								// ���������̍��W����ɂ���
						p.Y -= 10;								// �摜�̈ʒu�𒲐߂���
						filename = "TieTopHalf2";				// �摜�t�@�C�����̐ݒ�	
					}

					// �����ĕ`�惊�X�g�ɒǉ�
					drawdata = new DrawData(filename, p.X, p.Y + yoffset);
					DrawList.Add(drawdata);
				}

				#endregion

				// �����̒�������ɐi�߂�
				i += (int)(score.length * 2);
			}

			// �I�[�̕`�� i���ő剹�����������ꍇ�A�y���̏I�[�ł��邽��
			if (i == this.MaxNote)
			{
				// �s��*67�����ĕ`��
				if (this.nowScore != muphic.ScoreScr.AnimalScoreMode.All)
				{
					drawdata = new DrawData("End", EndXBegin, ScoreYBegin + 12 + (int)(Math.Floor(0.0 + i / NotePerLine) - 1) * ScoreYDifference);
					DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
				}
			}
		}


		/// <summary>
		/// �t���X�R�A���̕`��
		/// </summary>
		public void DrawAll()
		{
			// �t���X�R�A���͂܂Ƃ߂�J�b�R��`�悷��
			DrawData drawdata = new DrawData("Full", 64, 174);
			DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�

			// �I�t�Z�b�g+32���������������ꍇ�A�y���̏I�[�Ɣ��f
			if (this.offset + NotePerLine == this.MaxNote)
			{
				drawdata = new DrawData("End_full", EndXBegin + 1, ScoreYBegin + 12);
				DrawList.Add(drawdata);	// �`�惊�X�g�ɒǉ�
			}
		}


		/// <summary>
		/// �^����ꂽ�����̕\���ʒu�����肷�郁�\�b�h
		/// </summary>
		/// <param name="score">�ʒu�����肷�鉹��</param>
		/// <param name="num">�a�����Ԗڂ̉�����</param>
		/// <param name="i">�������X�g���Ԗڂ̉�����</param>
		/// <returns>�\���ʒu���W</returns>
		public Point getScoreCoordinate(Score score, int num, int i)
		{
			Point p = new Point(0, 0);
			int line = i / NotePerLine;	// ���s�ڂ̉����Ȃ̂�
			int n = i % NotePerLine;		// �s���̉��Ԗڂ̉����Ȃ̂�

			if (score.code[0] == -1)
			{
				// �x���̏ꍇ
				if (score.length == 0.5) p.Y = ScoreYBegin + 20 + line * ScoreYDifference;			// �����x��
				else if (score.length == 1) p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// �l���x��
				else if (score.length == 1.5) p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// �t�_�l���x��
				else if (score.length == 2) p.Y = ScoreYBegin + 24 + line * ScoreYDifference;		// �񕪋x��
				else if (score.length == 3) p.Y = ScoreYBegin + 21 + line * ScoreYDifference;		// �t�_�񕪋x��
				else if (score.length == 4) p.Y = ScoreYBegin + 19 + line * ScoreYDifference;		// �S�x��
			}
			else
			{
				// �x������Ȃ��ꍇ ���K���Ƃ�y���W��ς���
				p.Y = ScoreYBegin - 3 + score.code[num] * 4 - 4 + line * ScoreYDifference;

				if (this.CheckCodeDirection(score) == 1)
				{
					// �V����̉��������畄�������̉����ɂȂ�
					p.Y += 22;
					p.X += 3;

					//  �ׂ荇�킹�̘a���ŃY���������������ꍇ ���ɂ��炷
					if (this.CheckChordMode(score, num)) p.X -= 8;

					// �Y�����a���Ȃ�΍X�ɉE�֏������炵�ăo�����X����
					int j = 0;
					for (; j < score.chord; j++) if (this.CheckChordMode(score, j)) break;
					if (j != score.chord) p.X += 5;
				}
				else
				{
					// ��������(�ʏ�)�ŗׂ荇�킹�̘a���ŃY���������������ꍇ �E�ɂ��炷
					if (this.CheckChordMode(score, num)) p.X += 8;
				}
			}

			// ������x���W������
			// 1�Ԗڂ̉������W189px + ���ߐ�(0�`3)*181px + ���ߓ��̉�����(0�`7)*16px
			p.X += NoteXBegin + (n / NotePerBar * BarXDifference) + (n % NotePerBar * NoteXDifference);

			// �����ȊO�̏ꍇ�͂��ꂼ��x���W�����ăo�����X����
			if (score.length == 1) p.X += (int)Math.Round(NoteXDifference / 2.0, 0);
			else if (score.length == 1.5) p.X += (int)Math.Round((NoteXDifference / 2.0) * 2, 0);
			else if (score.length == 2) p.X += (int)Math.Round((NoteXDifference / 2.0) * 3, 0);
			else if (score.length == 3) p.X += (int)Math.Round((NoteXDifference / 2.0) * 5, 0);
			else if (score.length == 4) p.X += (int)Math.Round((NoteXDifference / 2.0) * 7, 0);

			// ���̃h�������ꍇ�������ɂ��炷
			if (score.code[num] == 8) p.X -= 3;

			// �X�ɁA�Q�s�ڈȍ~�̂P���ߖڂ̏ꍇ 1px�����ɂ��炵��4/4���q�̋󔒕����𖄂߂�
			if ((line != 0) && (n < NotePerBar)) p.X -= (NotePerBar - n) * 2;

			return p;
		}


		/// <summary>
		/// �^����ꂽ�����̉摜�����肷�郁�\�b�h
		/// </summary>
		/// <param name="score">�摜�����肷�鉹��</param>
		/// <param name="num">�a�����Ԗڂ̉�����</param>
		/// <returns>�\���摜������</returns>
		public String getScoreImage(Score score, int num)
		{
			// �a����ڂ̉���(code[0])���x���������ꍇ
			if (num == 0 && score.code[0] == -1)
			{
				// �����x���Ɍ���
				if (score.length == 0.5) return "EighthRest";

				// �l���x���Ɍ���
				if (score.length == 1) return "QuarterRest";

				// �t�_�l���x���Ɍ���
				if (score.length == 1.5) return "PQuarterRest";

				// �t�_�񕪋x���Ɍ���
				if (score.length == 3) return "PHalfRest";

				// ����ȊO�͓񕪋x�����S�x��
				return "AllRest";
			}

			if (score.length == 0.5)
			{
				// ��������

				if (num == 0 && this.CheckCodeDirection(score) == 0)
				{
					// ������̏ꍇ�A�����̉摜�͘a����ԏ�ł����g��Ȃ�

					// ���̃h��������h�̉����ɂ���
					if (score.code[num] == 8) return "EighthNotes_do";

					// �ׂ荇�킹�̘a����������Y��������������
					if (this.CheckChordMode(score, num)) return "EighthNotes_wa";

					// �����ɂ�����Ȃ���Ε��ʂ̔�������
					return "EighthNotes";
				}
				if (num == score.chord - 1 && this.CheckCodeDirection(score) == 1)
				{
					// �������̏ꍇ�A�����̉摜�͘a����ԉ��ł����g��Ȃ�

					// �ׂ荇�킹�̘a����������Y��������������
					if (this.CheckChordMode(score, num)) return "EighthNotes_wa_";

					// �����ɂ�����Ȃ���Ε��ʂ̔�������
					return "EighthNotes_";
				}
			}

			// ����ȊO�͎l�������ɂȂ�܂���

			// ���̃h��������h�̉����ɂ���
			if (score.code[num] == 8) return "QuarterNotes_do";

			// �V����̉��ō\������Ă���ꍇ�͕��������ɂȂ�
			if (this.CheckCodeDirection(score) == 1)
			{
				// �ׂ荇�킹�̘a����������Y��������������
				if (this.CheckChordMode(score, num)) return "QuarterNotes_wa_";

				// ����ȊO�͕��ʂ̕���������
				return "QuarterNotes_";
			}

			// �ׂ荇�킹�̘a����������Y��������������
			if (this.CheckChordMode(score, num)) return "QuarterNotes_wa";

			// �����ɂ�����Ȃ���Ε��ʂ̎l������
			return "QuarterNotes";
		}


		/// <summary>
		/// �^����ꂽ�����̕����̌��������߂郁�\�b�h
		/// </summary>
		/// <param name="score">�Ώۂ̉���(�a���܂�)</param>
		/// <returns>
		/// �����̌���
		/// -1:�x��
		/// 0:��(�ʏ�)
		/// 1:��(�V�E�h�݂̂̉�/�a��)
		/// </returns>
		public int CheckCodeDirection(Score score)
		{
			// ���������x����������
			if (score.code[0] == -1) return -1;

			// ���������ɂȂ�����́A�V�E�h(������)�ł��邱��
			for (int i = 0; i < score.code.Length; i++)
			{
				// �a�����ɃV��艺�̉����������� �����͏�(�ʏ�)�ɂȂ�
				if (score.code[i] > 2) return 0;
			}

			// ��̃��[�v�ň���������Ȃ���Ε����͉��ɂȂ�
			return 1;
		}


		/// <summary>
		/// �ׂ荇�킹�̘a���𔻒肷�郁�\�b�h
		/// </summary>
		/// <param name="score"></param>
		/// <param name="num"></param>
		/// <returns>�Y���������Ȃ�true</returns>
		public bool CheckChordMode(Score score, int num)
		{
			// �a������Ȃ������綴�!
			if (score.chord == 1) return false;

			if (this.CheckCodeDirection(score) == 0)
			{
				// �����̌�������(�ʏ�)�Ȃ��

				// �a����ԉ��̉��̓Y���������ɂ͂Ȃ�Ȃ�
				if (num == 2) return false;

				// �a�����Q�̏ꍇ
				if (score.chord == 2)
				{
					// �a���̏�̉��ŗׂ荇�킹��������Y���������ɂȂ�
					if ((num == 0) && (score.code[1] - score.code[0] == 1)) return true;

					// ����ȊO�͕��ʂ̉���
					return false;
				}

				// �a�����R�̏ꍇ
				if (score.chord == 3)
				{
					// �R�̉��S���ׂ荇�킹�������ꍇ
					if ((score.code[2] - score.code[1] == 1) && (score.code[1] - score.code[0] == 1))
					{
						// �^�񒆂̉��ł���΃Y���������ɂȂ�
						if (num == 1) return true;

						// ����ȊO�͕��ʂ̉���
						return false;
					}

					// �a���̏�̉��ŗׂ荇�킹��������Y���������ɂȂ�
					if ((num == 0) && (score.code[0] - score.code[1] == 1)) return true;

					// �a���Q�Ԗڂ̉��ŁA�㉺�Ɨׂ肠�킹�������炻�ꂼ��Y���������ɂȂ�
					if ((num == 1) && (score.code[1] - score.code[0] == 1)) return true;
					if ((num == 1) && (score.code[2] - score.code[1] == 1)) return true;

					// ����ȊO�͕��ʂ̉���
					return false;
				}
			}
			else if (this.CheckCodeDirection(score) == 1)
			{
				// ��������������(�V�ƃh)�̏ꍇ
				// �a�������Q�̃V�ƃh�ȊO���肦�Ȃ�

				// �a���Q�Ԗڂ̉��ŁA��Ɨׂ荇�킹��������Y���������ɂȂ�
				if ((num == 1) && (score.code[1] - score.code[0] == 1)) return true;

				// ����ȊO�͑S�ĕ��ʂ̉���
				return false;
			}

			// ���ƑS�����!
			return false;
		}


		#endregion

		#region �������X�g�������\�b�h�Q

		/// <summary>
		/// AnimalList���炻�ꂼ��̓����̉������X�g�����
		/// </summary>
		/// <param name="animalmode">���X�g�쐬�̑Ώۂ̓���</param>
		public void CreateScoreList(muphic.ScoreScr.AnimalScoreMode animalmode)
		{
			int i, n;
			int temp = -1;	// �a���J�E���g�p�ϐ�
			Animal animal = new Animal(0, 0);
			Score[] ScoreList = new Score[this.MaxNote];
			for (i = 0; i < this.MaxNote; i++) ScoreList[i] = new Score();
			i = n = 0;

			// �������X�g�Ɋ܂܂�Ă���Ώۂ̓����̐����擾
			int num = this.CheckAnimalNumber(animalmode);

			#region �������X�g����

			// 0�Ԗڂ̓����̃f�[�^���擾 �Ώۂ̓����Ƀq�b�g����܂Ńf�[�^�擾�������� 
			if (num > 0)
			{
				animal = ((Animal)parent.AnimalList[n]);
				while (!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);
			}

			// �������X�g�쐬���C�����[�v
			while (num > 0)
			{
				// �y���ʒui�Ɠ����ʒuplace����v���A���Ώۂ̓����ł��邩�ǂ��� 
				if (i == animal.place && animal.AnimalName.Equals(animalmode.ToString()))
				{
					// ��L�����𖞂������ꍇ�A���K���y���ɃR�s�[
					ScoreList[i].AddCode(animal.code);

					// �y���ʒui���O�Ɠ����������ꍇ�A�a���Ɣ���
					if (temp == i) ScoreList[i].chord++;
					temp = i;

					// ����ɁA���X�g���̑Ώۓ��������P���炷
					// ���X�g���̑Ώۓ�������0�ɂȂ����烋�[�v�I��
					if (--num == 0) break;

					// ���̓����f�[�^���擾 �Ώۂ̓����Ƀq�b�g����܂Ńf�[�^�擾��������
					animal = ((Animal)parent.AnimalList[++n]);
					while (!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);

					// ���̓��������̊y���ʒu�ɖ����ꍇ�A�l�������ɂ���
					if (animal.place > i + 1) ScoreList[i].length = 1;

					// �a�������ő�l�ɂȂ����狭���I��i��i�߂�
					if (ScoreList[i].chord >= MaxChord) { i++; temp = 0; }
				}
				else
				{
					// �����𖞂����Ȃ������ꍇ��i��i�߂�
					i++;
					temp = 0;
				}
			}

			// �Ō�̉������l���ɂ���(�����s�̍Ō�̉�����������A�^�C�����ł���1�s���Ă̂��A���Ȃ�Ŗ��������Ⴂ�܂��傤)
			//if( (i % NotePerLine != NotePerLine-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;
			if ((i % this.MaxNote != this.MaxNote - 1) && ScoreList[i].code[0] != -1) ScoreList[i].length = 1;

			#endregion

			#region �^�C�E�x���̔��� ����(�` ver.0.10.1)
			/*
			i = -1;
			while(++i < this.MaxNote)
			{
				// �x���ł͂Ȃ������ꍇ
				if(ScoreList[i].code[0] != -1)
				{
					// �^�C�̔���
					// �l��������������
					if( (ScoreList[i].length >= 1) && (i%NotePerBar == NotePerBar-1) )
					{
						ScoreList[i+1].length = ScoreList[i].length - 0.5;	// ���̉����̒����𔪕���
						ScoreList[i+1].code[0] = ScoreList[i].code[0];		// �������R�s�[
						ScoreList[i+1].code[1] = ScoreList[i].code[1];		// �������R�s�[
						ScoreList[i+1].code[2] = ScoreList[i].code[2];		// �������R�s�[
						ScoreList[i].length = 0.5;							// �����̒����𔪕���
						ScoreList[i].tie = true;							// �^�C�t���Oon
					}

					// i���x���łȂ���Α�����
					i += (int)(ScoreList[i].length * 2) - 1;
					continue;
				}

				// �S�x������
				// ���߂̍ŏ��̉����Ŕ�������{
				if( (i%NotePerBar == NotePerBar-4*2) && CheckRest(ScoreList, i, 8) )
				{
					ScoreList[i].length = 4;	// ���ߓ��̂��ׂẲ������x����������A�S�x���ɂ���
					i += 7; continue;			// �ꏬ�ߕ���ɐi�߂�
				}

				// �t�_�񕪋x������
				// ���ߓ���7�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-3*2) && CheckRest(ScoreList, i, 6) )
				{
					ScoreList[i].length = 3;	// �t�_�񕪋x��
					i += 5; continue;			// �ꕪ��ɐi�߂�
				}
				
				// �񕪋x������
				// ���ߓ���5�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-2*2) && CheckRest(ScoreList, i, 4) )
				{
				
					ScoreList[i].length = 2;	// �񕪋x��
					i += 3; continue;			// �񕪐�ɐi�߂�
				}

				// �t�_�l���x������
				// ���ߓ���6�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
				if( (i%NotePerBar <= NotePerBar-1.5*2) && CheckRest(ScoreList, i, 3) )
				{
				
					ScoreList[i].length = 1.5;	// �t�_�l���x��
					i += 2; continue;			// �O����ɐi�߂�
				}

				// �l���x������
				// ���ߓ���7�ڈȍ~�̉����ł͔��肷��K�v���Ȃ�
				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-1*2) && CheckRest(ScoreList, i, 2) )
				{
					ScoreList[i].length = 1;	// �l���x��
					i += 1; continue;			// �l����ɐi�߂�
				}
				
				// ����ɂ��Y�����Ȃ���΁A8���x���ƂȂ�
				//i += (int)(ScoreList[i].length * 2) - 1;
			}
			*/
			#endregion

			#region �^�C�E�x���̔��� (ver.0.10.2 �`)

			i = -1;
			while (++i < this.MaxNote)
			{
				// �x���ł͂Ȃ������ꍇ
				if (ScoreList[i].code[0] != -1)
				{
					// �^�C�̔���
					// �l��������������
					if ((ScoreList[i].length >= 1) && (i % NotePerBar == NotePerBar - 1))
					{
						for (int j = 0; j < MaxChord; j++)
							ScoreList[i + 1].code[j] = ScoreList[i].code[j];	// ������
						ScoreList[i + 1].length = ScoreList[i].length - 0.5;	// ���̉����̒����𔪕���
						ScoreList[i + 1].chord = ScoreList[i].chord;			// �a�����̃R�s�[
						ScoreList[i].length = 0.5;							// �����̒����𔪕���
						ScoreList[i].tie = 1;								// �^�C�t���Oon
						ScoreList[i + 1].tie = 2;								// �^�C�t���Oon
					}

					// i���x���łȂ���Α�����
					i += (int)(ScoreList[i].length * 2) - 1;
					continue;
				}

				// i���܂߂��A�������x���̐� �ȉ���
				//  restnum == 2 �l���x���ɂȂ��
				//  restnum == 4 �񕪋x���ɂȂ��
				//  restnum == 8 �S�x���ɂȂ��
				int restnum = this.CheckRestNum(ScoreList, i);

				switch (i % NotePerBar)
				{
					case 0:
						if (restnum == 8)
						{
							// ���ߓ����S�ċx���������ꍇ
							ScoreList[i].length = 4;	// ���ߓ��̂��ׂẲ������x����������A�S�x���ɂ���
							i += NotePerBar - 1;			// ���̏��߂֐i�߂�
							continue;
						}
						goto case 2;
					case 1:
						if (restnum >= 7)
						{
							ScoreList[i + 1].length = 3;	// �����x��+�t�_�񕪋x��
							i += 6;						// ���̏��߂֐i�߂�
							continue;
						}
						goto case 2;
					case 2:
						if (restnum >= 6)
						{
							ScoreList[i].length = 3;	// �t�_�񕪋x��
							i += 5;						// �ꕪ��ɐi�߂�
							continue;
						}
						goto case 4;
					case 3:
						if (restnum >= 5)
						{
							ScoreList[i + 1].length = 2;	// �����x��+�񕪋x��
							i += 4;						// ���̏��߂֐i�߂�
							continue;
						}
						goto case 4;
					case 4:
						if (restnum >= 4)
						{
							ScoreList[i].length = 2;	// �񕪋x��
							i += 3;						// �񕪕���ɐi�߂�
							continue;
						}
						goto case 5;
					case 5:
						if (restnum >= 3)
						{
							ScoreList[i].length = 1.5;	// �t�_�l���x��
							i += 2;						// �񕪕���ɐi�߂�
							continue;
						}
						goto case 6;
					case 6:
						if (restnum >= 2)
						{
							ScoreList[i].length = 1;	// �l���x��
							i += 1;						// �񕪕���ɐi�߂�
							continue;
						}
						goto case 7;
					case 7:
						break;	// ����ɂ��Y�����Ȃ���΁A�����x���ƂȂ�
					default:
						// �����ɓ��B����ꍇ�A�P�ʏ��߂�����̍ő剹������8�łȂ��\����
						System.Console.WriteLine("NotePerBar != 8");
						break;
				}
			}
			#endregion

			#region ���������z��̃��X�g��p�ӂ��ꂽ�e�������Ƃ̃t�B�[���h�ɃR�s�[����
			switch (animalmode)
			{
				case AnimalScoreMode.Sheep:
					for (i = 0; i < this.MaxNote; i++) this.SheepScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Rabbit:
					for (i = 0; i < this.MaxNote; i++) this.RabbitScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Bird:
					for (i = 0; i < this.MaxNote; i++) this.BirdScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Dog:
					for (i = 0; i < this.MaxNote; i++) this.DogScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Pig:
					for (i = 0; i < this.MaxNote; i++) this.PigScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Cat:
					for (i = 0; i < this.MaxNote; i++) this.CatScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Voice:
					for (i = 0; i < this.MaxNote; i++) this.VoiceScoreList.Add(ScoreList[i]);
					break;
				default:
					break;
			}
			#endregion
		}


		/// <summary>
		/// �w�肳�ꂽ�͈͂��S�ċx�����ǂ������`�F�b�N����
		/// </summary>
		/// <param name="data">�`�F�b�N���鉹�����X�g</param>
		/// <param name="i">�J�n�v�f�ԍ�</param>
		/// <param name="n">�`�F�b�N���鉹����</param>
		/// <returns>�͈͂̉����S�ċx���Ȃ�true �����łȂ��Ȃ�false</returns>
		public bool CheckRest(Score[] data, int i, int n)
		{
			int j = 0;
			for (; j < n; j++)
			{
				if (data[i + j].code[0] != -1) return false;
			}
			return true;
		}


		/// <summary>
		/// �w�肳�ꂽ�v�f����̘A�������x���̐��𐔂���(�P�ʏ��ߕ��̂�)
		/// </summary>
		/// <param name="data">�`�F�b�N���鉹�����X�g</param>
		/// <param name="i">�J�n�v�f�ԍ�</param>
		/// <returns>�x���̐�</returns>
		public int CheckRestNum(Score[] data, int i)
		{
			int cnt = 0;	// �x���̐�
			int max = NotePerBar - i % NotePerBar;	// �`�F�b�N����ő吔 �P�ʏ��߂𒴂��Ȃ��悤�ɒ���

			while (cnt < max)
			{
				// �x���ȊO�̗v�f�𔭌�������
				if (data[i + cnt].code[0] != -1) return cnt;
				cnt++;
			}
			return cnt;
		}


		/// <summary>
		/// ���X�g���Ɏw�肵�������������܂܂�Ă��邩�`�F�b�N���郁�\�b�h
		/// </summary>
		/// <param name="mode"></param>
		/// <returns>�܂܂�Ă�����</returns>
		public int CheckAnimalNumber(muphic.ScoreScr.AnimalScoreMode mode)
		{
			int num = 0;
			for (int i = 0; i < parent.AnimalList.Count; i++)
			{
				Animal a = ((Animal)parent.AnimalList[i]);
				if (a.AnimalName.Equals(mode.ToString())) num++;
			}
			return num;
		}

		#endregion

		/// <summary>
		/// ��X�N���[���{�^�����������ۂ̓���
		/// </summary>
		/// <returns>�X�N���[���������ǂ���</returns>
		public bool UpScroll()
		{
			// �P�s�ڂ���ɂ͍s���Ȃ��悤�ɂ���
			if (this.offset >= 32)
			{
				// �I�t�Z�b�g����1�s���̉�����32������
				this.offset -= 32;

				// �����čĕ`��
				this.ReDraw();

				// true��Ԃ�
				return true;
			}

			// �X�N���[�����Ȃ������ꍇ��false��Ԃ�
			return false;
		}


		/// <summary>
		/// ���X�N���[���{�^�����������ۂ̓���
		/// </summary>
		/// <returns>�X�N���[���������ǂ���</returns>
		public bool DownScroll()
		{
			if (this.nowScore != AnimalScoreMode.All)
			{
				// �t���X�R�A�ł͖����ꍇ�A�y�����V�s�ȏ�ɂȂ������̂݉��ɃX�N���[����
				if (this.MaxNote > 192 && this.offset < this.MaxNote - 32)
				{
					// �I�t�Z�b�g��1�s���̉�����32�𑫂�
					this.offset += 32;

					// �����čĕ`��
					this.ReDraw();

					// true��Ԃ�
					return true;
				}
			}
			else
			{
				// �t���X�R�A�̏ꍇ
				if (this.offset < this.MaxNote - 32)
				{
					// �I�t�Z�b�g��1�s���̉�����32�𑫂�
					this.offset += 32;

					// �����čĕ`��
					this.ReDraw();

					// true��Ԃ�
					return true;
				}
			}

			// �X�N���[�����Ȃ�������false��Ԃ�
			return false;
		}


		/// <summary>
		/// �I�t�Z�b�g���N���A���郁�\�b�h
		/// ���ƂȂ�private�ɂ������������
		/// </summary>
		public void ClearOffset()
		{
			this.offset = 0;
		}

		#region �f�o�b�O�p���\�b�h�Q

		/// <summary>
		/// �������X�g���`�F�b�N���� ���Ă��ꗗ��\������
		/// ��Ƀf�o�b�O�p
		/// </summary>
		/// <param name="data">�ꗗ��\�����鉹�����X�g</param>
		/// <param name="length">���X�g�̒���</param>
		public void CheckScoreList(Score[] data, int length)
		{
			int i = 0;

			for (i = 0; i < length; ++i)
			{
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data[i].length, data[i].code[0], data[i].code[1], data[i].code[2]);
			}
		}
		public void CheckScoreList(ArrayList list)
		{
			int i = 0;

			for (i = 0; i < list.Count; ++i)
			{
				Score data = (Score)list[i];
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data.length, data.code[0], data.code[1], data.code[2]);
			}
		}


		/// <summary>
		/// �f�o�b�O�p���b�Z�[�W�o�̓��\�b�h
		/// </summary>
		/// <param name="str">���炩�̕�����</param>
		/// <param name="num">���炩�̒l</param>
		public void Debug(String str, int num)
		{
			System.Console.WriteLine(str + num);
		}
		/// <summary>
		/// �f�o�b�O�p���b�Z�[�W�o�̓��\�b�h
		/// </summary>
		/// <param name="str">���炩�̕�����</param>
		/// <param name="num">���炩�̒l</param>
		public void Debug(String str, double num)
		{
			System.Console.WriteLine(str + num);
		}

		#endregion
	}

	#endregion
}
