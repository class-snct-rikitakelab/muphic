using System;

namespace Muphic.NameInputScreenParts
{
	/// <summary>
	/// プレイヤー名のバックアップ保存に使用するクラス。XML シリアル化して一時ファイルとして保存する。
	/// </summary>
	[Serializable]
	public class PlayerNameBackup
	{
		/// <summary>
		/// プレイヤー1 の名前を取得または設定する。
		/// </summary>
		public string Player1 { get; set; }

		/// <summary>
		/// プレイヤー2 の名前を取得または設定する。
		/// </summary>
		public string Player2 { get; set; }

		/// <summary>
		/// プレイヤー1 の性別を取得または設定する。
		/// </summary>
		public int Player1Gender { get; set; }

		/// <summary>
		/// プレイヤー2 の性別を取得または設定する。
		/// </summary>
		public int Player2Gender { get; set; }


		/// <summary>
		/// プレイヤー名バックアップ用クラスの新しいインスタンスを初期化する。
		/// </summary>
		public PlayerNameBackup()
		{
		}

		/// <summary>
		/// プレイヤー名バックアップ用クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="player1">プレイヤー1 の名前。</param>
		/// <param name="player2">プレイヤー2 の名前。</param>
		/// <param name="player1Gender">プレイヤー1 の性別。</param>
		/// <param name="player2Gender">プレイヤー2 の性別。</param>
		public PlayerNameBackup(string player1, string player2, int player1Gender, int player2Gender)
		{
			this.Player1 = player1;
			this.Player2 = player2;
			this.Player1Gender = player1Gender;
			this.Player2Gender = player2Gender;
		}


		/// <summary>
		/// プレイヤー名をバックアップするファイルのパスを取得する。
		/// </summary>
		public static string BackupFilePath
		{
			get { return System.IO.Path.Combine(Configuration.ConfigurationData.UserDataFolder, Settings.ResourceNames.PlayerNameAutoSaveFilePath); }
		}
	}
}
