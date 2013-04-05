using System;

namespace muphic.Common
{
	/// <summary>
	/// AutoSave �̊T�v�̐����ł��B
	/// </summary>
	public class AutoSave
	{
		private const double SaveTime = 1.0;	// �����Z�[�u����Ԋu(��)
		private static bool autosave;			// �����Z�[�uon/off
		private static int framecount;			// 
		private static bool saveflag;			// �����Z�[�u
		
		public static void initAutoSave()
		{
			AutoSave.autosave = false;
			AutoSave.framecount = 0;
			AutoSave.saveflag = false;
		}
		
		public static void onAutoSave()
		{
			AutoSave.autosave = true;
		}
		
		public static void offAutoSave()
		{
			AutoSave.autosave = false;
		}
		
		public static void Count()
		{
			if(!AutoSave.autosave) return;	// �����Z�[�uoff�Ȃ�J�E���g���Ă��Ӗ����Ȃ�
			AutoSave.framecount++;			// �t���[�����J�E���g
			AutoSave.saveflag = false;		// �Z�[�u�t���O�N���A
			if(AutoSave.framecount == (int)(AutoSave.SaveTime * 60 * 60))
			{
				// ���Ԃ�������Z�[�u
				AutoSave.saveflag = true;
				AutoSave.framecount = 0;
			}
		}
		
		public static bool getSaveFlag()
		{
			return AutoSave.saveflag;
		}
		
	}
}
