using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic
{
	#region Ver1.1.0
	/*
	/// <summary>
	/// ver1.1.0 Search,RePlace�ǉ�
	/// </summary>
	public class Animals
	{
		private HouseLight houselight;
		public ArrayList AnimalList;
		public int PlayOffset;							//�Đ����̃I�t�Z�b�g�B�s�N�Z���P��
		int nowPlace;									//���݂̕\���ʒu(�������̂ق��͈ꎞ�I�Ɋi�[���Ă��邾���ŁA�{���̒l��Score�ɂ���)
		int CheckedAnimal = -1;							//���ݑI����Ԃɂ���Ă��铮���̗v�f�ԍ�
		public Animals()
		{
			AnimalList = new ArrayList();
			houselight = new HouseLight();
		}

		public Animal this[int x]
		{
			get
			{
				return (Animal)AnimalList[x];
			}
//			set
//			{
//				AnimalList[x] = value;
//			}
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

		/// <summary>
		/// �����B��`�悷�鏈��
		/// </summary>
		/// <param name="nowPlace">���݂̍Đ����̈ʒu</param>
		/// <param name="TempoMode">���݉�����Ă���e���|�{�^��</param>
		/// <param name="isPlaying">���ݍĐ������ǂ���</param>
		/// <returns>(�Đ����̏ꍇ�̂�)�Đ��������������ǂ���</returns>
		public bool Draw(int nowPlace, int TempoMode, bool isPlaying)
		{
			houselight.Draw();
			this.nowPlace = nowPlace;						//�ꎞ�I��nowPlace���i�[����
			if(isPlaying)
			{
				return DrawPlaying(TempoMode);				//�Đ������������true���Ԃ��Ă���
			}
			else
			{
				DrawNotPlaying();
				return false;
			}
		}

		/// <summary>
		/// �Đ����̕`�揈��
		/// </summary>
		/// <remarks>�Đ��������������ǂ���(���������true)</remarks>
		private bool DrawPlaying(int TempoMode)
		{
			this.PlayOffset += TempoMode;									//�e���|�̕������I�t�Z�b�g�𑫂��Ƃ�
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//�����̉��K�ƈʒu������W������o��
				p.X -= this.PlayOffset;									//�Đ����Ȃ̂ŁA�I�t�Z�b�g�������Ƃ�
				if(p.X < 0 || a.Visible == false)						//������ʊO�ɏo�Ă��邩�A�`��֎~�Ȃ�
				{
					continue;											//�����for�͔�΂�
				}
				else if(p.X <= 55)										//�����ƂɂԂ����Ă�����
				{
					muphic.SoundManager.Play(a.ToString() + a.code + ".wav");//����炵�āc
					houselight.Add(a.code);								//�Ƃ����炵�āc
					a.Visible = false;
					continue;											//����for��
				}
				else if(800 < p.X)										//�����A�܂��y���܂œ��B���Ă��Ȃ��Ȃ�
				{														//AnimalList�͏��Ԃǂ���ɕ���ł���̂ŁA���ꂩ����
					break;												//�y���܂œ��B���Ă��Ȃ����ƂɂȂ邩��for�����I������
				}

				
				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;			//������h�炷���߂̏������{���B

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//�����̏����𖞂����Ă��Ȃ���΁A���ʂɕ`�悷��
			}
			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalList�̍Ō�̗v�f�����o��
			if(!b.Visible)
			{
				return true;											//�����̍Ō�̗v�f���ƂɂԂ���I������Đ��I��
			}
			return false;
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
				DrawManager.DrawCenter(AnimalList[i].ToString(), p.X, p.Y);
			}
			//�����I���̂�̕`��
			if(this.CheckedAnimal == -1)
			{
				return;
			}
			Animal aa = (Animal)AnimalList[this.CheckedAnimal];
			Point pp = this.ScoretoPointRelative(aa.place, aa.code);
			DrawManager.DrawCenter("AnimalCheck_" + aa.AnimalName, pp.X, pp.Y);
		}

		/// <summary>
		/// �w�肳�ꂽ�ꏊ�ɓ��������邩�ǂ����𒲂ׂ郁�\�b�h
		/// �����AnimalList�ɂ�����v�f�ԍ���Ԃ�
		/// </summary>
		/// <param name="place">�w�肷��ꏊ</param>
		/// <param name="code">�w�肷�鉹�K</param>
		/// <returns>�����������̗v�f�ԍ�(���Ȃ����-1)</returns>
		public int Search(int place, int code)
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)
				{
					return i;
				}
			}
			return -1;
		}

		public bool Insert(int place, int code, String mode)
		{
			return Insert(place, code, mode, true);
		}

		/// <summary>
		/// ������V���ɒǉ�����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <param name="code">���K</param>
		/// <param name="isPlaySound">�z�u���ɉ���炷���ǂ���</param>
		/// <returns>�����������ǂ���</returns>
		public bool Insert(int place, int code, String mode, bool isPlaySound)
		{
			int i;
			int SamePlaceCount=0;												//�a����3�܂łɂ��鐧���̂��߂ɕK�v
			for(i=0;i<AnimalList.Count;i++)										//�a���̐���
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//�ꏊ���������ƁA�J�E���g���C���N�������g
				}
			}
			if(SamePlaceCount >= 3)
			{
				return false;													//�J�E���g��3�ȏゾ������a���̐������󂯂Ēǉ��s��
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					return false;
				}
				if(a.place == place && a.code > code)							//�ݒ肷��ʒu�Ɠ����ł��A
				{																//���K���ݒ肷��ʒu��艓�����̂����݂�����A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
				if(a.place > place)												//�ݒ肷��ʒu��艓�����̂����݂���
				{																//AnimalList�͏����Ƀ\�[�g���Ă��邩��A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
			}
			System.Diagnostics.Debug.Write("Insert in Animals" + place + "," + code + " " + mode);
			Animal newAnimal = new Animal(place, code, mode);					//Animal�I�u�W�F�N�g���C���X�^���X��
			AnimalList.Insert(i, newAnimal);									//�����悤�ɂȂ����Ƃ���Ɋ��荞��
			//�������邱�Ƃɂ���ď������ۂ����
			if(isPlaySound)														//����炵�����Ȃ�
			{
				SoundManager.Play(mode + code + ".wav");						//�ݒu�����Ƃ������ƂŁA����炷
			}
			return true;
		}

		public bool InsertL(int place, int code, String mode)
		{
			int i;
			int SamePlaceCount=0;												//�a����3�܂łɂ��鐧���̂��߂ɕK�v
			for(i=0;i<AnimalList.Count;i++)										//�a���̐���
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//�ꏊ���������ƁA�J�E���g���C���N�������g
				}
			}
			if(SamePlaceCount >= 1)
			{
				return false;													//�J�E���g��3�ȏゾ������a���̐������󂯂Ēǉ��s��
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					return false;
				}
				if(a.place == place && a.code > code)							//�ݒ肷��ʒu�Ɠ����ł��A
				{																//���K���ݒ肷��ʒu��艓�����̂����݂�����A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
				if(a.place > place)												//�ݒ肷��ʒu��艓�����̂����݂���
				{																//AnimalList�͏����Ƀ\�[�g���Ă��邩��A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
			}
			Animal newAnimal = new Animal(place, code, mode);					//Animal�I�u�W�F�N�g���C���X�^���X��
			AnimalList.Insert(i, newAnimal);									//�����悤�ɂȂ����Ƃ���Ɋ��荞��
			//�������邱�Ƃɂ���ď������ۂ����
			return true;
		}

		/// <summary>
		/// ���ݑI������Ă��铮�����폜����
		/// </summary>
		/// <returns>�����������ǂ���</returns>
		public bool Delete()
		{
			if(this.CheckedAnimal == -1)
			{
				return false;
			}
			Animal a = (Animal)AnimalList[this.CheckedAnimal];
			return Delete(a.place, a.code);
		}

		/// <summary>
		/// �������폜����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <param name="code">���K</param>
		/// <returns>�����������ǂ���</returns>
		public bool Delete(int place, int code)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					if(i == this.CheckedAnimal)
					{
						this.CheckedAnimal = -1;
					}
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

		/// <summary>
		/// �����̔z�u��ύX����
		/// </summary>
		/// <param name="OldPlace">�ύX�O�̏ꏊ</param>
		/// <param name="OldCode">�ύX�O�̉��K</param>
		/// <param name="NewPlace">�ύX��̏ꏊ</param>
		/// <param name="NewCode">�ύX��̉��K</param>
		/// <param name="isCheck">�ύX��̓�����I����Ԃɂ��邩�ǂ���</param>
		/// <returns>�ύX��̗v�f�ԍ�(���s��-1)</returns>
		public int Replace(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//�ύX�O�̓��������݂��Ȃ�
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(Insert(NewPlace, NewCode, a.AnimalName, false) == false)
			{
				return -1;														//�ύX��̏ꏊ�Ɋ��ɓ���������
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("���肦�Ȃ��G���[");			//�ύX�O�̓��������݂���͂��Ȃ̂ɂȂ������s����
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// �����̔z�u��ύX����(Link)
		/// </summary>
		/// <param name="OldPlace">�ύX�O�̏ꏊ</param>
		/// <param name="OldCode">�ύX�O�̉��K</param>
		/// <param name="NewPlace">�ύX��̏ꏊ</param>
		/// <param name="NewCode">�ύX��̉��K</param>
		/// <param name="isCheck">�ύX��̓�����I����Ԃɂ��邩�ǂ���</param>
		/// <returns>�ύX��̗v�f�ԍ�(���s��-1)</returns>
		public int ReplaceL(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//�ύX�O�̓��������݂��Ȃ�
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(InsertL(NewPlace, NewCode, a.AnimalName) == false)
			{
				return -1;														//�ύX��̏ꏊ�Ɋ��ɓ���������
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("���肦�Ȃ��G���[");			//�ύX�O�̓��������݂���͂��Ȃ̂ɂȂ������s����
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// ������I����Ԃɂ���
		/// </summary>
		/// <param name="place">�ʒu</param>
		/// <param name="code">���K</param>
		/// <returns>�I�����ꂽ�����̗v�f�ԍ�(�Ȃ����-1)</returns>
		public int ClickAnimal(int place, int code)
		{
			this.CheckedAnimal = this.Search(place, code);
			return this.CheckedAnimal;
		}

		/// <summary>
		/// ���������ׂč폜����
		/// </summary>
		public void AllDelete()
		{
			AnimalList.Clear();
			this.CheckedAnimal = -1;
		}
	}*/
	#endregion
	
	#region Ver2.1
	/// <summary>
	/// ver1.1.0 Search,RePlace�ǉ�
	/// ver2     ���̊O���œ������\���ł��Ȃ��悤��
	/// ver2.1   AnimalCheck��`�悵�Ȃ��悤�ɂ����̂ƁAInsertL�ł������Ȃ�悤�ɂ���
	/// </summary>
	public class Animals
	{
		private HouseLight houselight;
		public ArrayList AnimalList;
		public int PlayOffset;							//�Đ����̃I�t�Z�b�g�B�s�N�Z���P��
		int nowPlace;									//���݂̕\���ʒu(�������̂ق��͈ꎞ�I�Ɋi�[���Ă��邾���ŁA�{���̒l��Score�ɂ���)
		int CheckedAnimal = -1;							//���ݑI����Ԃɂ���Ă��铮���̗v�f�ԍ�
		public Animals()
		{
			AnimalList = new ArrayList();
			houselight = new HouseLight();
		}

		public Animal this[int x]
		{
			get
			{
				return (Animal)AnimalList[x];
			}/*
			set
			{
				AnimalList[x] = value;
			}*/
		}
		/*
				public int Count
				{
					get
					{
						return AnimalList.Count;
					}
				}
		*/
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

		/// <summary>
		/// �����B��`�悷�鏈��
		/// </summary>
		/// <param name="nowPlace">���݂̍Đ����̈ʒu</param>
		/// <param name="TempoMode">���݉�����Ă���e���|�{�^��</param>
		/// <param name="isPlaying">���ݍĐ������ǂ���</param>
		/// <returns>(�Đ����̏ꍇ�̂�)�Đ��������������ǂ���</returns>
		public bool Draw(int nowPlace, int TempoMode, bool isPlaying)
		{
			houselight.Draw();
			this.nowPlace = nowPlace;						//�ꎞ�I��nowPlace���i�[����
			if(isPlaying)
			{
				return DrawPlaying(TempoMode);				//�Đ������������true���Ԃ��Ă���
			}
			else
			{
				DrawNotPlaying();
				return false;
			}
		}

		/// <summary>
		/// �Đ����̕`�揈��
		/// </summary>
		/// <remarks>�Đ��������������ǂ���(���������true)</remarks>
		private bool DrawPlaying(int TempoMode)
		{
			this.PlayOffset += TempoMode;									//�e���|�̕������I�t�Z�b�g�𑫂��Ƃ�
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//�����̉��K�ƈʒu������W������o��
				p.X -= this.PlayOffset;									//�Đ����Ȃ̂ŁA�I�t�Z�b�g�������Ƃ�
				if(p.X < 55 && a.Visible == false)						//�����Ƃ�ʂ�߂��Ă���Ȃ�
				{
					continue;											//�����for�͔�΂�
				}
				else if(p.X <= 55)										//�����ƂɂԂ����Ă�����
				{
					muphic.SoundManager.Play(a.ToString() + a.code + ".wav");//����炵�āc
					houselight.Add(a.code);								//�Ƃ����炵�āc
					a.Visible = false;
					continue;											//����for��
				}
				else if(p.X < ScoreTools.score.Right)					//�����A���̒��ɓ������������̂Ȃ�
				{
					a.Visible = true;									//�\��������
				}
				else if(ScoreTools.score.Right < p.X)					//�����A�܂��y���܂œ��B���Ă��Ȃ��Ȃ�
				{														//AnimalList�͏��Ԃǂ���ɕ���ł���̂ŁA���ꂩ����
					break;												//�y���܂œ��B���Ă��Ȃ����ƂɂȂ邩��for�����I������
				}

				
				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;			//������h�炷���߂̏������{���B

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//�����̏����𖞂����Ă��Ȃ���΁A���ʂɕ`�悷��
			}
			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalList�̍Ō�̗v�f�����o��
			Point bp = this.ScoretoPointRelative(b.place, b.code);
			bp.X -= this.PlayOffset;
			if(!b.Visible && bp.X <= 55)
			{
				return true;											//�����̍Ō�̗v�f���ƂɂԂ���I������Đ��I��
			}
			return false;
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
				if(ScoreTools.inScore(p))								//���̓����ɂ����
				{
					a.Visible = true;									//�\��������
				}
				else													//�O���ɂ����
				{
					a.Visible = false;									//�\�������Ȃ�
				}
				//System.Diagnostics.Debug.WriteLine(a.place);
				if(a.Visible)											//���̓����ɂ���Ƃ�����
				{														//�`�悳����
					DrawManager.DrawCenter(AnimalList[i].ToString(), p.X, p.Y);
				}
			}
			//�����I���̂�̕`��
//			if(this.CheckedAnimal == -1)
//			{
//				return;
//			}
//			Animal aa = (Animal)AnimalList[this.CheckedAnimal];
//			Point pp = this.ScoretoPointRelative(aa.place, aa.code);
//			if(ScoreTools.inScore(pp))									//�����A�`�F�b�N����Ă��铮�������̒��ɂ���Ȃ�
//			{															//�I���摜��`��
//				DrawManager.DrawCenter("AnimalCheck_" + aa.AnimalName, pp.X, pp.Y);
//			}
		}

		/// <summary>
		/// �w�肳�ꂽ�ꏊ�ɓ��������邩�ǂ����𒲂ׂ郁�\�b�h
		/// �����AnimalList�ɂ�����v�f�ԍ���Ԃ�
		/// </summary>
		/// <param name="place">�w�肷��ꏊ</param>
		/// <param name="code">�w�肷�鉹�K</param>
		/// <returns>�����������̗v�f�ԍ�(���Ȃ����-1)</returns>
		public int Search(int place, int code)
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// ������V���ɒǉ�����(���͖炷)
		/// </summary>
		/// <param name="place"></param>
		/// <param name="code"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public bool Insert(int place, int code, String mode)
		{
			return Insert(place, code, mode, true);
		}

		/// <summary>
		/// ������V���ɒǉ�����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <param name="code">���K</param>
		/// <param name="isPlaySound">�z�u���ɉ���炷���ǂ���</param>
		/// <returns>�����������ǂ���</returns>
		public bool Insert(int place, int code, String mode, bool isPlaySound)
		{
			int i;
			int SamePlaceCount=0;												//�a����3�܂łɂ��鐧���̂��߂ɕK�v
			for(i=0;i<AnimalList.Count;i++)										//�a���̐���
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//�ꏊ���������ƁA�J�E���g���C���N�������g
				}
			}
			if(SamePlaceCount >= 3)
			{
				return false;													//�J�E���g��3�ȏゾ������a���̐������󂯂Ēǉ��s��
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					return false;
				}
				if(a.place == place && a.code > code)							//�ݒ肷��ʒu�Ɠ����ł��A
				{																//���K���ݒ肷��ʒu��艓�����̂����݂�����A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
				if(a.place > place)												//�ݒ肷��ʒu��艓�����̂����݂���
				{																//AnimalList�͏����Ƀ\�[�g���Ă��邩��A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
			}
			System.Diagnostics.Debug.Write("Insert in Animals" + place + "," + code + " " + mode);
			Animal newAnimal = new Animal(place, code, mode);					//Animal�I�u�W�F�N�g���C���X�^���X��
			AnimalList.Insert(i, newAnimal);									//�����悤�ɂȂ����Ƃ���Ɋ��荞��
			//�������邱�Ƃɂ���ď������ۂ����
			if(isPlaySound)														//����炵�����Ȃ�
			{
				SoundManager.Play(mode + code + ".wav");						//�ݒu�����Ƃ������ƂŁA����炷
			}
			return true;
		}

		public bool InsertL(int place, int code, String mode)
		{
			int i;
			int SamePlaceCount=0;												//�a����3�܂łɂ��鐧���̂��߂ɕK�v
			for(i=0;i<AnimalList.Count;i++)										//�a���̐���
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//�ꏊ���������ƁA�J�E���g���C���N�������g
				}
			}
			if(SamePlaceCount >= 1)
			{
				return false;													//�J�E���g��3�ȏゾ������a���̐������󂯂Ēǉ��s��
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					return false;
				}
				if(a.place == place && a.code > code)							//�ݒ肷��ʒu�Ɠ����ł��A
				{																//���K���ݒ肷��ʒu��艓�����̂����݂�����A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
				if(a.place > place)												//�ݒ肷��ʒu��艓�����̂����݂���
				{																//AnimalList�͏����Ƀ\�[�g���Ă��邩��A
					break;														//�������̂����ꂽ�Ƃ������Ƃ͂��ł�OK
				}
			}
			Animal newAnimal = new Animal(place, code, mode);					//Animal�I�u�W�F�N�g���C���X�^���X��
			AnimalList.Insert(i, newAnimal);									//�����悤�ɂȂ����Ƃ���Ɋ��荞��
			//�������邱�Ƃɂ���ď������ۂ����
			SoundManager.Play(mode + code + ".wav");						//�ݒu�����Ƃ������ƂŁA����炷
			return true;
		}

		/// <summary>
		/// ���ݑI������Ă��铮�����폜����
		/// </summary>
		/// <returns>�����������ǂ���</returns>
		public bool Delete()
		{
			if(this.CheckedAnimal == -1)
			{
				return false;
			}
			Animal a = (Animal)AnimalList[this.CheckedAnimal];
			return Delete(a.place, a.code);
		}

		/// <summary>
		/// �������폜����
		/// </summary>
		/// <param name="place">(��ΓI)�ʒu</param>
		/// <param name="code">���K</param>
		/// <returns>�����������ǂ���</returns>
		public bool Delete(int place, int code)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//�ʒu�Ɖ��K�����Ԃ��Ă���̂����݂���
				{
					if(i == this.CheckedAnimal)
					{
						this.CheckedAnimal = -1;
					}
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

		/// <summary>
		/// �����̔z�u��ύX����(�ύX��̉���炳�Ȃ�)
		/// </summary>
		/// <param name="OldPlace">�ύX�O�̏ꏊ</param>
		/// <param name="OldCode">�ύX�O�̉��K</param>
		/// <param name="NewPlace">�ύX��̏ꏊ</param>
		/// <param name="NewCode">�ύX��̉��K</param>
		/// <param name="isCheck">�ύX��̓�����I����Ԃɂ��邩�ǂ���</param>
		/// <returns>�ύX��̗v�f�ԍ�(���s��-1)</returns>
		public int Replace(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			return Replace(OldPlace, OldCode, NewPlace, NewCode, isCheck, false);
		}

		/// <summary>
		/// �����̔z�u��ύX����
		/// </summary>
		/// <param name="OldPlace">�ύX�O�̏ꏊ</param>
		/// <param name="OldCode">�ύX�O�̉��K</param>
		/// <param name="NewPlace">�ύX��̏ꏊ</param>
		/// <param name="NewCode">�ύX��̉��K</param>
		/// <param name="isCheck">�ύX��̓�����I����Ԃɂ��邩�ǂ���</param>
		/// <param name="isPlaySound">�ύX���ɉ���炷���ǂ���</param>
		/// <returns>�ύX��̗v�f�ԍ�(���s��-1)</returns>
		public int Replace(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck, bool isPlaySound)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//�ύX�O�̓��������݂��Ȃ�
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(isPlaySound)													//���낢��Ȋ֌W�ŁA
			{																//Insert�ŉ���炷�̂ł�
				SoundManager.Play(a.AnimalName + a.code + ".wav");			//�Ȃ��A�����Ŗ炷
			}
			if(Insert(NewPlace, NewCode, a.AnimalName, false) == false)
			{
				return -1;														//�ύX��̏ꏊ�Ɋ��ɓ���������
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("���肦�Ȃ��G���[");			//�ύX�O�̓��������݂���͂��Ȃ̂ɂȂ������s����
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// �����̔z�u��ύX����(Link)
		/// </summary>
		/// <param name="OldPlace">�ύX�O�̏ꏊ</param>
		/// <param name="OldCode">�ύX�O�̉��K</param>
		/// <param name="NewPlace">�ύX��̏ꏊ</param>
		/// <param name="NewCode">�ύX��̉��K</param>
		/// <param name="isCheck">�ύX��̓�����I����Ԃɂ��邩�ǂ���</param>
		/// <returns>�ύX��̗v�f�ԍ�(���s��-1)</returns>
		public int ReplaceL(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//�ύX�O�̓��������݂��Ȃ�
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(InsertL(NewPlace, NewCode, a.AnimalName) == false)
			{
				return -1;														//�ύX��̏ꏊ�Ɋ��ɓ���������
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("���肦�Ȃ��G���[");			//�ύX�O�̓��������݂���͂��Ȃ̂ɂȂ������s����
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// ������I����Ԃɂ���
		/// </summary>
		/// <param name="place">�ʒu</param>
		/// <param name="code">���K</param>
		/// <returns>�I�����ꂽ�����̗v�f�ԍ�(�Ȃ����-1)</returns>
		public int ClickAnimal(int place, int code)
		{
			this.CheckedAnimal = this.Search(place, code);
			return this.CheckedAnimal;
		}

		/// <summary>
		/// ���������ׂč폜����
		/// </summary>
		public void AllDelete()
		{
			AnimalList.Clear();
			this.CheckedAnimal = -1;
		}
	}
	#endregion
}
