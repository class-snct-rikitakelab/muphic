using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic
{
	/// <summary>
	/// Screen �̊T�v�̐����ł��B
	/// </summary>
	public class Screen : Base
	{
		public ArrayList BaseList;
		private int prevNum;											//�ȑO�̃t���[���̎��_�Ń}�E�X���w���Ă������i�̗v�f�ԍ�
		
		//�h���b�O�p�t�B�[���h
		public int beginPartsNum = -1;								//�h���b�O�J�n�����Ƃ��ɃN���b�N�����p�[�c
		//public int endPartsNum = -1;									//�h���b�O�I�������Ƃ��ɃN���b�N�����p�[�c(�����begin�̂ق����A��������Click���\�b�h���Ă�)
		//private bool isClickAble;									//�h���b�O�I�����̂��ƁA�h���b�O�J�n�ƃh���b�O�I�����ɎQ�Ƃ��Ă�����̂������ꍇ��true�ɂȂ�B
		public Screen()
		{
			//prevPoint = new Point(0, 0)
			prevNum = -1;
			BaseList = new ArrayList();
		}

		/// <summary>
		/// �e�N�X�`�����̕`�揈�����L�q���郁�\�b�h�B�����ł��ꂼ��̕��i�̃f�[�^�����ɉ�ʂ�
		/// �`�悷�邱�ƂɂȂ�
		/// </summary>
		public virtual void Draw()
		{
			if(this.Visible == false) return;
			muphic.DrawManager.Draw(this.ToString());					//�܂��������g��`��
			for(int i=0;i<BaseList.Count;i++)
			{
				if(BaseList[i] is Screen)								//�������i��Screen�^�ł�������
				{
					if(((Screen)BaseList[i]).Visible)
					{
						((Screen)BaseList[i]).Draw();						//���ʂɕ`�悹���A�����Draw���\�b�h���Ă�
					}
				}
				else													//���������̕��i��������
				{
					if(((Base)BaseList[i]).Visible)
					{
						muphic.DrawManager.Draw(BaseList[i].ToString(), ((Base)BaseList[i]).State);//���ʂɕ`��
					}
				}
			}
		}

		/// <summary>
		/// �}�E�X����ʓ��œ������Ƃ��ɌĂ΂�郁�\�b�h�B�����ł��ꂼ��̕��i��Enter��Leave��
		/// ���\�b�h���ĂԂ��ƂɂȂ�
		/// </summary>
		/// <param name="p"></param>
		public virtual void MouseMove(Point p)
		{
			int nowNum = -1;
			for(int i=BaseList.Count-1;i>=0;i--)				//���݂̍��W���ǂ��̕��i�ɓ����Ă��邩�T��
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());//���W�Ǘ�������W���Q�b�g����
				if(inRect(p, r))								//�������݂̍��W�����i�̒��ɓ����Ă���Ȃ�
				{
					if(((Base)BaseList[i]).Visible == false)
					{											//������\�����Ȃ�A
						continue;								//��������
					}
					nowNum = i;									//�T������
					if(BaseList[i] is Screen)					//�������i��Screen�^��������
					{
						((Screen)BaseList[i]).MouseMove(p);		//MouseMove���Ă�ł��
					}
					break;
				}
			}

			if(prevNum != nowNum)								//�O�ƌ��݂ɉ��炩�̕ω����������ꍇ�A�Ή�����Enter��Leave���Ă�
			{
				if(prevNum == -1)
				{												//�����O�̏�Ԃŕ��i���w���Ă��Ȃ��Ȃ�
					((Base)BaseList[nowNum]).MouseEnter();		//����ɂȂ��ĉ��炩�̕��i���w�����Ƃ������ƂȂ̂�
				}												//���i��Enter���\�b�h���Ă�
				else if(nowNum == -1)
				{												//�������݂̏�Ԃŕ��i���w���Ă��Ȃ��Ȃ�
					((Base)BaseList[prevNum]).MouseLeave();		//����ɂȂ��ĉ��炩�̕��i�������Ȃ��Ȃ����Ƃ������ƂȂ̂ŁA
				}												//���i��Leave���\�b�h���Ă�
				else
				{												//�����O�̕��i����Ⴄ���i�Ɏw�����̂��ς������
					((Base)BaseList[prevNum]).MouseLeave();		//���i��Enter���\�b�h��Leave���\�b�h����x�ɌĂ�
					((Base)BaseList[nowNum]).MouseEnter();
				}
			}
			prevNum = nowNum;									//���ɌĂ΂ꂽ�Ƃ��Ɏg������������
		}

		/// <summary>
		/// �h���b�O���J�n���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
		/// </summary>
		/// <param name="begin">�J�n���W</param>
		public virtual void DragBegin(System.Drawing.Point begin)
		{
			for(int i=BaseList.Count-1;i>=0;i--)				//���݂̍��W���ǂ��̕��i�ɓ����Ă��邩�T��
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());//���W�Ǘ�������W���Q�b�g����
				if(inRect(begin, r))							//�������݂̍��W�����i�̒��ɓ����Ă���Ȃ�
				{
					if(BaseList[i] is Screen)					//�������ꂪScreen�^��������
					{
						((Screen)BaseList[i]).DragBegin(begin);	//��������DragBegin�����ł��
					}
					this.beginPartsNum = i;						//�T������
					break;
				}
			}
		}

		/// <summary>
		/// �h���b�O���I�������Ƃ��ɌĂ΂�郁�\�b�h
		/// </summary>
		/// <param name="begin">�J�n���W</param>
		/// <param name="p">���݂̍��W</param>
		public virtual void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
//			for(int i=BaseList.Count-1;i>=0;i--)				//���݂̍��W���ǂ��̕��i�ɓ����Ă��邩�T��
//			{
//				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());//���W�Ǘ�������W���Q�b�g����
//				if(inRect(p, r))								//�������݂̍��W�����i�̒��ɓ����Ă���Ȃ�
//				{
//					this.endPartsNum = i;						//�T������
//					break;
//				}
//			}
			if(this.beginPartsNum == -1) return;				//�w�i���h���b�O�����ꍇ-1�̂܂܂����蓾��

			if(BaseList[this.beginPartsNum] is Screen)							//�������ꂪScreen�^��������
			{
				((Screen)BaseList[this.beginPartsNum]).DragEnd(begin, p);		//��������DragEnd�����ł��
			}

//			if(this.beginPartsNum == this.endPartsNum)
//			{
//				this.isClickAble = true;
//			}
			this.beginPartsNum = -1;											//������
//			this.endPartsNum = -1;
		}

		/// <summary>
		/// �}�E�X����ʓ��Ńh���b�O���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h�B�����ł��ꂼ��̕��i��Drag
		/// ���\�b�h���ĂԂ��ƂɂȂ�
		/// </summary>
		/// <param name="begin">�h���b�O�J�n���W</param>
		/// <param name="p">���݂̍��W</param>
		public override void Drag(Point begin, Point p)
		{
			//DrawManager.DrawString(begin + "now" + p, 0, 100);
			
			if(this.beginPartsNum == -1) return;
			((Base)BaseList[this.beginPartsNum]).Drag(begin, p);			//Base,Screen�\���Ȃ��ɌĂԁB
		}

		#region �����OClick���\�b�h
		/*
		/// <summary>
		/// �}�E�X����ʓ��ŃN���b�N���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h�B�����ł��ꂼ��̕��i��Click
		/// ���\�b�h���ĂԂ��ƂɂȂ�
		/// </summary>
		/// <param name="p"></param>
		public override void Click(Point p)
		{
			for(int i=BaseList.Count-1;i>=0;i--)
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());		//�Y�����镔�i�̍��W���擾
				if(inRect(p, r) && ((Base)BaseList[i]).Visible)						//�N���b�N���̍��W�����i�̒����ǂ����`�F�b�N
				{
					((Base)BaseList[i]).Click(p);									//�����Ă���Ȃ炻�̕��i��Click���\�b�h���Ă�
					return;
				}
			}
		}
		*/
		#endregion

		/// <summary>
		/// �}�E�X����ʓ��ŃN���b�N���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h�B�����ł��ꂼ��̕��i��Click
		/// ���\�b�h���ĂԂ��ƂɂȂ�
		/// </summary>
		/// <param name="p"></param>
		public override void Click(Point p)
		{
//			if(this.isClickAble == false)
//			{
//				return;
//			}
			for(int i=BaseList.Count-1;i>=0;i--)
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());		//�Y�����镔�i�̍��W���擾
				if(inRect(p, r) && ((Base)BaseList[i]).Visible)						//�N���b�N���̍��W�����i�̒����ǂ����`�F�b�N
				{
					// �`���[�g���A�����s���������ꍇ
					if( TutorialStatus.getIsTutorial() )
					{
						if( TutorialStatus.checkTriggerPartsList(BaseList[i].ToString()) )
						{
							// ���̃X�e�[�g�֐i�ރg���K�[�ƂȂ�p�[�c�������ꍇ�A���̃X�e�[�g�֐i�ރt���O��on�ɂ���
							TutorialStatus.setEnableNextState();
						}
						else
						{
							// �g���K�[�ł͂Ȃ������ꍇ�A�����X�g�ɓ����Ă��邩�`�F�b�N���A�����Ă��Ȃ���΃N���b�N�����Ȃ�
							if( !TutorialStatus.checkPermissionPartsList(BaseList[i].ToString()) ) return;
						}
					}
					
					// ������ �N���b�N�̂��m�点 ������
					Console.WriteLine("Click : " + BaseList[i].ToString());
						
					// �N���b�N���̍��W�����i�̒��ɓ����Ă���Ȃ炻�̕��i��Click���\�b�h���Ă�
					((Base)BaseList[i]).Click(p);
					
					return;
				}
			}
			//this.isClickAble = false;			//1��N���b�N�����̂�false�ɂ���
		}

		/// <summary>
		/// �����X�g���N���b�N���\�b�h�B�Đ�����base.Click���A
		/// ��������Ă�ł�����ق��������Ǝv���B
		/// </summary>
		/// <param name="p"></param>
		/// <param name="PermissionList">�����X�g(ToString�Ŏw��)</param>
		public void Click(Point p, String[] PermissionList)
		{
//			if(this.isClickAble == false)
//			{
//				return;
//			}
			for(int i=BaseList.Count-1;i>=0;i--)
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());		//�Y�����镔�i�̍��W���擾
				if(inRect(p, r) && ((Base)BaseList[i]).Visible)						//�N���b�N���̍��W�����i�̒����ǂ����`�F�b�N
				{
					// �N���b�N���ꂽ���̂������X�g�ɓ����Ă��邩�ǂ����m�F
					for(int j=0;j<PermissionList.Length;j++)
					{
						if(BaseList[i].ToString() == PermissionList[j])				//�N���b�N�������̂�
						{															//�����X�g�ɓ����Ă���
							// ������ �N���b�N�̂��m�点 ������
							Console.WriteLine("Click : " + BaseList[i].ToString());
						
							// �N���b�N���̍��W�����i�̒��ɓ����Ă���Ȃ炻�̕��i��Click���\�b�h���Ă�
							((Base)BaseList[i]).Click(p);
					
							return;
						}
					}
				}
			}
			//this.isClickAble = false;			//1��N���b�N�����̂�false�ɂ���
		}

		protected bool inRect(Point p, Rectangle r)
		{
			if(r.Left <= p.X && p.X <= r.Right)
			{
				if(r.Top <= p.Y && p.Y <= r.Bottom)
				{
					return true;
				}
			}
			return false;
		}
	}
}
