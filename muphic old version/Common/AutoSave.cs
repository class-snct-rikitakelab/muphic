using System;

namespace muphic.Common
{
	/// <summary>
	/// AutoSave の概要の説明です。
	/// </summary>
	public class AutoSave
	{
		private const double SaveTime = 1.0;	// 自動セーブする間隔(分)
		private static bool autosave;			// 自動セーブon/off
		private static int framecount;			// 
		private static bool saveflag;			// 自動セーブ
		
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
			if(!AutoSave.autosave) return;	// 自動セーブoffならカウントしても意味がない
			AutoSave.framecount++;			// フレーム数カウント
			AutoSave.saveflag = false;		// セーブフラグクリア
			if(AutoSave.framecount == (int)(AutoSave.SaveTime * 60 * 60))
			{
				// 時間が来たらセーブ
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
