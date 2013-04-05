using System;
using System.Collections;

namespace muphic
{
	/// <summary>
	/// FileNameManager の概要の説明です。
	/// </summary>
	public class FileNameManager
	{
		private static FileNameManager fileNameManager;
		private Hashtable FileNameTable;						//クラス名とファイル名の配列を関連付けている

		public FileNameManager()
		{
			FileNameTable = new Hashtable();
			muphic.FileNameManager.fileNameManager = this;
		}

		public bool RegistFNames(String ClassName, String[] FileNames)
		{
			if(FileNameTable.ContainsKey(ClassName))					//すでに登録されているなら
			{
				return false;											//無理
			}
			FileNameTable.Add(ClassName, FileNames);					//違うなら普通に登録
			return true;
		}

		public void DeleteFNames(String ClassName)
		{
			FileNameTable.Remove(ClassName);
		}

		public String[] GetFNames(String ClassName)
		{
			if(!FileNameTable.ContainsKey(ClassName))
			{
				System.Windows.Forms.MessageBox.Show("クラス：" + ClassName + "は登録されていません");
			}
			return (String[])FileNameTable[ClassName];
		}
		
		public bool GetFExist(String ClassName)
		{
			return FileNameTable.Contains(ClassName);
		}

		public static bool Regist(String ClassName, String FileName1)
		{
			return muphic.FileNameManager.fileNameManager.RegistFNames(ClassName, new String[1] {FileName1});
		}

		public static bool Regist(String ClassName, String FileName1, String FileName2)
		{
			return muphic.FileNameManager.fileNameManager.RegistFNames(ClassName, new String[2] {FileName1, FileName2});
		}

		public static bool Regist(String ClassName, String[] FileNames)
		{
			return muphic.FileNameManager.fileNameManager.RegistFNames(ClassName, FileNames);
		}

		public static void Delete(String ClassName)
		{
			muphic.FileNameManager.fileNameManager.DeleteFNames(ClassName);
		}

		public static String GetFileName(String ClassName, int state)
		{
			return muphic.FileNameManager.fileNameManager.GetFNames(ClassName)[state];
		}

		public static String[] GetFileNames(String ClassName)
		{
			return muphic.FileNameManager.fileNameManager.GetFNames(ClassName);
		}
		
		public static bool GetFileExist(String ClassName)
		{
			return muphic.FileNameManager.fileNameManager.GetFExist(ClassName);
		}
	}
}
