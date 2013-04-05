using System;
using System.Collections;

namespace muphic.Common
{
	/// <summary>
	/// TutorialStatus �̊T�v�̐����ł��B
	/// </summary>
	public class TutorialStatus
	{
		private static bool isTutorial;										// �`���[�g���A�����s�����ǂ����̃t���O
		private static ArrayList TriggerPartsList = new ArrayList();		// �`���[�g���A�����s���Ɏ��̃X�e�[�g�ɐi�ރg���K�[�ɂȂ�p�[�c�̃��X�g
		private static ArrayList PermissionPartsList = new ArrayList();		// �`���[�g���A�����s���Ƀg���K�[�p�[�c�ȊO�ŃN���b�N��������p�[�c�̃��X�g
		private static bool NextStateFlag;									// ���̃X�e�[�g�ɐi��ł������ǂ����̃t���O
		private static bool NextStateStandBy;								// ���炩�̓����҂��đҋ@���Ă���łɎg�p����
		private static bool ClickControl;									// �N���b�N������֎~����ۂɎg�p����
		private static bool isTutorialDialog;								// �`���[�g���A���{�̂��_�C�A���O��\�����Ă���ꍇ�ɗ��Ă�t���O
		private static bool isDrawString;									// �`���[�g���A���Ǘ����ȊO�ł̕�����`����s�����ǂ���
		private static string isSPMode;										// ���R������s���ꍇ�Ɏg�p
		private static int FrameCount;										// ���炩�̒x���𔭐�������ۂɎg�p����i�p�~
		
		public TutorialStatus()
		{
			// �i�E�ÁE�j
		}
		
		/// <summary>
		/// ���̃N���X��private static�t�B�[���h��S�ď��������郁�\�b�h
		/// </summary>
		public static void initTutorialStatus()
		{
			TutorialStatus.setDisableTutorial();			// �`���[�g���A���t���Ooff
			TutorialStatus.setDisableNextState();			// �X�e�[�g�i�s�t���Ooff
			TutorialStatus.setDisableNextStateStandBy();	// �ҋ@�t���Ooff
			TutorialStatus.setDisableTutorialDialog();		// �_�C�A���O�t���Ooff
			TutorialStatus.setDisableClickControl();		// �N���b�N����
			TutorialStatus.initTriggerPartsList();			// �g���K�[���X�g�N���A
			TutorialStatus.initPermissionPartsList();		// �����X�g�N���A
			TutorialStatus.setDisableIsSPMode();			// ���R����off
		}
		
		#region isTutorial �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// isTutorial��Ԃ�
		/// </summary>
		/// <returns>true�Ȃ�Tutorial���s��</returns>
		public static bool getIsTutorial()
		{
			return TutorialStatus.isTutorial;
		}
		
		/// <summary>
		/// isTutorial��true�ɂ��郁�\�b�h
		/// Tutorial�J�n���Ɏ��s���ׂ�
		/// </summary>
		public static void setEnableTutorial()
		{
			TutorialStatus.isTutorial = true;
		}
		
		/// <summary>
		/// isTutorial��false�ɂ��郁�\�b�h
		/// �v���O�����J�n����Tutorial�I�����Ɏ��s���ׂ�
		/// </summary>
		public static void setDisableTutorial()
		{
			TutorialStatus.isTutorial = false;
		}
		
		#endregion
		
		#region TriggerPartsList �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// ���̃X�e�[�g�ւ̃g���K�[�p�[�c���X�g���N���A����
		/// </summary>
		public static void initTriggerPartsList()
		{
			TutorialStatus.TriggerPartsList.Clear();
		}
		
		/// <summary>
		/// �g���K�[�p�[�c���X�g���쐬���郁�\�b�h
		/// </summary>
		/// <param name="data">�g���K�[�p�[�c���X�g</param>
		public static void setTriggerPartsList(string[] data)
		{
			// ���X�g���N���A
			TutorialStatus.initTriggerPartsList();
			
			for(int i=0; i<data.Length; i++)
			{
				TutorialStatus.TriggerPartsList.Add(data[i]);
			}
		}
		
		/// <summary>
		/// �g���K�[�p�[�c���X�g�ɒǉ����郁�\�b�h
		/// </summary>
		/// <param name="data"></param>
		public static void addTriggerPartsList(string data)
		{
			TutorialStatus.TriggerPartsList.Add(data);
		}
		
		/// <summary>
		/// �^����ꂽ�����񂪃g���K�[�p�[�c���X�g���ɂ��邩���`�F�b�N���郁�\�b�h
		/// </summary>
		/// <param name="data">�`�F�b�N���镶����</param>
		/// <returns>true�Ȃ�g���K�[�p�[�c���X�g���Ɋ܂܂�Ă���</returns>
		public static bool checkTriggerPartsList(string data)
		{
			for(int i=0; i<TutorialStatus.TriggerPartsList.Count; i++)
			{
				if( data.Equals(TutorialStatus.TriggerPartsList[i].ToString()) ) return true;
			}
			return false;
		}
		
		/// <summary>
		/// �^����ꂽ��������g���K�[�p�[�c���X�g����폜���郁�\�b�h
		/// </summary>
		/// <param name="data">�폜���镶����</param>
		/// <returns>true�Ȃ�폜����</returns>
		public static bool deleteTriggerParts(string data)
		{
			if(TutorialStatus.checkTriggerPartsList(data))
			{
				TutorialStatus.TriggerPartsList.Remove(data);
				return true;
			}
			else
			{
				return false;
			}
		}
		
		#endregion
		
		#region PermissionPartsList �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// �`���[�g���A�����s���̃N���b�N�����X�g�����������郁�\�b�h
		/// </summary>
		public static void initPermissionPartsList()
		{
			TutorialStatus.PermissionPartsList.Clear();
		}
				
		/// <summary>
		/// �N���b�N�����X�g���쐬���郁�\�b�h
		/// </summary>
		/// <param name="data">�����X�g</param>
		public static void setPermissionPartsList(string[] data)
		{
			// ���X�g���N���A
			TutorialStatus.initPermissionPartsList();
			
			for(int i=0; i<data.Length; i++)
			{
				TutorialStatus.PermissionPartsList.Add(data[i]);
			}
		}
		
		/// <summary>
		/// �N���b�N�����X�g�ɒǉ����郁�\�b�h
		/// </summary>
		/// <param name="data">�ǉ����镶����</param>
		public static void addPermissionPartsList(string data)
		{
			TutorialStatus.PermissionPartsList.Add(data);
		}
		
		/// <summary>
		/// �^����ꂽ�����񂪃N���b�N�����X�g���ɂ��邩���`�F�b�N���郁�\�b�h
		/// </summary>
		/// <param name="data">�`�F�b�N���镶����</param>
		/// <returns>true�Ȃ狖���X�g���Ɋ܂܂�Ă���</returns>
		public static bool checkPermissionPartsList(string data)
		{
			for(int i=0; i<TutorialStatus.PermissionPartsList.Count; i++)
			{
				string parts = (string)TutorialStatus.PermissionPartsList[i];
				if( parts == data ) return true;
			}
			return false;
		}
		
		/// <summary>
		/// �^����ꂽ������������X�g����폜���郁�\�b�h
		/// </summary>
		/// <param name="data">�폜���镶����</param>
		/// <returns>true�Ȃ�폜����</returns>
		public static bool deletePermissionParts(string data)
		{
			if(TutorialStatus.checkPermissionPartsList(data))
			{
				TutorialStatus.PermissionPartsList.Remove(data);
				return true;
			}
			else
			{
				return false;
			}
		}
		
		#endregion
		
		#region NextStateFlag �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// ���̃X�e�[�g�ɐi��ł������ǂ����₢���킹�郁�\�b�h
		/// </summary>
		/// <returns>true�Ȃ玟�̃X�e�[�g�ɐi��ł悵</returns>
		public static bool getNextStateFlag()
		{
			return TutorialStatus.NextStateFlag;
		}
		
		/// <summary>
		/// ���̃X�e�[�g�֐i��ł������ǂ����̃t���O true�ɃZ�b�g
		/// </summary>
		public static void setEnableNextState()
		{
			TutorialStatus.NextStateFlag = true;
		}
		
		/// <summary>
		/// ���̃X�e�[�g�֐i��ł������ǂ����̃t���O false�ɃZ�b�g
		/// </summary>
		public static void setDisableNextState()
		{
			TutorialStatus.NextStateFlag = false;
		}
		
		#endregion
		
		#region NextStateStandBy �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// �ҋ@�t���O�𗧂Ă郁�\�b�h
		/// </summary>
		public static void setEnableNextStateStandBy()
		{
			TutorialStatus.NextStateStandBy = true;
		}
		
		/// <summary>
		/// �ҋ@���������郁�\�b�h
		/// </summary>
		public static void setDisableNextStateStandBy()
		{
			TutorialStatus.NextStateStandBy = false;
		}
		
		/// <summary>
		/// �ҋ@�����ǂ����𓾂郁�\�b�h
		/// </summary>
		/// <returns></returns>
		public static bool getNextStateStandBy()
		{
			return TutorialStatus.NextStateStandBy;
		}
		
		/// <summary>
		/// �ҋ@���I�������郁�\�b�h
		/// </summary>
		public static void EndStandBy()
		{
			// �`���[�g���A�����s���ŁA�ҋ@�t���O�������Ă��ăX�e�[�g�i�s�t���O�������Ă��Ȃ������ꍇ
			if( TutorialStatus.isTutorial && (TutorialStatus.NextStateStandBy && !TutorialStatus.NextStateFlag) )
			{
				TutorialStatus.setEnableNextState();	// �X�e�[�g�i�s�t���O�𗧂Ă�
			}
		}
		
		#endregion
		
		#region ClickContorol �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// �N���b�N�֎~��Ԃ��ǂ����₢���킹�邽�߂̃��\�b�h
		/// </summary>
		/// <returns>true�Ȃ�N���b�N�֎~</returns>
		public static bool getClickControl()
		{
			return TutorialStatus.ClickControl;
		}
		
		/// <summary>
		/// �N���b�N�֎~�̏�Ԃɂ��郁�\�b�h
		/// </summary>
		public static void setEnableClickControl()
		{
			TutorialStatus.ClickControl = true;
		}
		
		/// <summary>
		/// �N���b�N���̏�Ԃɂ��郁�\�b�h
		/// </summary>
		public static void setDisableClickControl()
		{
			TutorialStatus.ClickControl = false;
		}
		
		#endregion
		
		#region isTutorialDialog �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// isTutorialDialog��Ԃ�
		/// �܂�Ƃ���`���[�g���A���Ǘ����Ń_�C�A���O��\�����Ă��邩�ǂ���
		/// </summary>
		/// <returns>true�Ȃ�_�C�A���O�\���� �����񂳂�B�͎��d���Ă�������</returns>
		public static bool getisTutorialDialog()
		{
			return TutorialStatus.isTutorialDialog;
		}
		
		/// <summary>
		/// isTutorialDialog��true�ɂ��郁�\�b�h
		/// �`���[�g���A���Ǘ����̃_�C�A���O�\�����ɌĂяo��
		/// </summary>
		public static void setEnableTutorialDialog()
		{
			TutorialStatus.isTutorialDialog = true;
		}
		
		/// <summary>
		/// isTutorialDialog��false�ɂ��郁�\�b�h
		/// �`���[�g���A���Ǘ����̃_�C�A���O�����Ƃ��ɌĂяo��
		/// </summary>
		public static void setDisableTutorialDialog()
		{
			TutorialStatus.isTutorialDialog = false;
		}
		
		#endregion
		
		#region isSPMode�Ɋւ��郁�\�b�h
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static string getisSPMode()
		{
			return TutorialStatus.isSPMode;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static bool getIsSPModeBool()
		{
			if(TutorialStatus.isSPMode == "") return false;
			else return true;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static string getIsSPMode()
		{
			return TutorialStatus.isSPMode;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void setEnableIsSPMode(string str)
		{
			TutorialStatus.isSPMode = str;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void setDisableIsSPMode()
		{
			TutorialStatus.isSPMode = "";
		}
		
		#endregion
		
		#region DrawString �Ɋւ��郁�\�b�h
		
		/// <summary>
		/// isDrawString��Ԃ�
		/// </summary>
		/// <returns>true�Ȃ當�����`�悵�Ă悵</returns>
		public static bool getisDrawString()
		{
			return TutorialStatus.isDrawString;
		}
		
		/// <summary>
		/// isDrawString��true�ɂ���
		/// �`���[�g���A���Ǘ����ȊO�̕�����̕`���������(�����炪�ʏ펞)
		/// </summary>
		public static void setEnableDrawString()
		{
			TutorialStatus.isDrawString = true;
		}
		
		/// <summary>
		/// isDrawString��false�ɂ���
		/// �`���[�g���A���Ǘ����ȊO�̕�����̕`��������Ȃ�
		/// ��ɃX���C�h���̕\���̂���
		/// </summary>
		public static void setDisableDrawString()
		{
			TutorialStatus.isDrawString = false;
		}
		
		
		/// <summary>
		/// �`���[�g���A�����s���ŕ������`�悵�Ăق����Ȃ��ꍇ��false��Ԃ��܂�
		/// �`���[�g���A���Ǘ����Ń_�C�A���O��\�����Ă���ꍇ��A�X���C�h��O�ʂɕ`�悵�Ă���ꍇ�Ȃǂ��Y��
		/// </summary>
		/// <returns>�������`�悷�ׂ����ǂ���</returns>
		public static bool StringDrawFlag()
		{
			// �`���[�g���A�����s���ŁA�_�C�A���O���\������Ă��邩������`�悪������Ă��Ȃ���� False��Ԃ�
			return !(TutorialStatus.getIsTutorial() & (TutorialStatus.getisTutorialDialog() | !TutorialStatus.getisDrawString()) );
		}
		
		#endregion
		
		#region FrameCount �Ɋւ��郁�\�b�h
		
		public static void setFrameCount(int frame)
		{
			TutorialStatus.FrameCount = frame;
		}
		
		public static bool checkFrameCount(int frame, int num)
		{
			int thisframe = TutorialStatus.FrameCount + num;
			if(thisframe >= 60) thisframe -= 60;
			if(thisframe < frame) return false;
			else return true;
		}
		
		#endregion
		
		/// <summary>
		/// �J��/�f�o�b�O�p
		/// </summary>
		public static void test()
		{
			TutorialStatus.setEnableTutorial();
			TutorialStatus.setPermissionPartsList(new string[] { "muphic.Top.OneButton", "muphic.One.Score", "muphic.One.RightButtons.SheepButton" });
		}
	}
}
