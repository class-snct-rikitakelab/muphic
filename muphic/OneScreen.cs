using Muphic.CompositionScreenParts;
using Muphic.PlayerWorks;
using Muphic.Manager;
using Muphic.OneScreenParts.Buttons;
using Muphic.Tools.IO;

namespace Muphic
{
	/// <summary>
	/// ひとりでおんがく画面クラス。
	/// </summary>
	public class OneScreen : CompositionScreen
	{
		/// <summary>
		/// 親にあたるトップ画面
		/// </summary>
		public TopScreen Parent { get; private set; }

		/// <summary>
		/// "もどる"ボタン
		/// </summary>
		public BackButton BackButton { get; private set; }

		/// <summary>
		/// DrawManager への登録番号。クラス解放時に DrawManager に登録したキーとテクスチャを一括削除するため。
		/// </summary>
		private int RegistNum { get; set; }


		/// <summary>
		/// ひとりでおんがくモードで使用される自動保存ファイルのパスを取得する。
		/// </summary>
		private static string AutoSaveFilePath
		{
			get { return System.IO.Path.Combine(Configuration.ConfigurationData.UserDataFolder, Settings.ResourceNames.OneScreenAutoSaveFilePath); }
		}


		/// <summary>
		/// ひとりでおんがく画面クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="topScreen">親にあたるトップ画面。</param>
		public OneScreen(TopScreen topScreen)
			: base(System.IO.File.Exists(OneScreen.AutoSaveFilePath) ? XmlFileReader.ReadScoreData(OneScreen.AutoSaveFilePath) : new ScoreData(), CompositionMode.OneScreen)
		{
			// 親にあたるトップ画面の設定
			this.Parent = topScreen;

			// パーツのインスタンス化等
			this.Initialization(); 

			// 自動保存イベントの登録
			FrameManager.AutoSaveEvent += new AutoSaveEventHandler(AutoSaveEvent);
		}


		/// <summary>
		/// ひとりでおんがく画面を構成する各パーツのインスタンス化等を行う。
		/// </summary>
		protected new void Initialization()
		{
			// 登録開始を管理クラスへ通知
			this.RegistNum = DrawManager.BeginRegist(false);

			#region 部品のインスタンス化

			this.BackButton = new BackButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.BackButton);

			#endregion

			// 登録終了を管理クラスへ通知
			DrawManager.EndRegist();
		}

		/// <summary>
		/// ひとりでおんがく画面で使用したリソースを解放し、登録したキーとテクスチャ名を削除する。
		/// </summary>
		public new void Dispose()
		{
			// 自動保存イベントの登録を削除
			FrameManager.AutoSaveEvent -= new AutoSaveEventHandler(AutoSaveEvent);

			// 汎用作曲画面のリソースを解放
			base.Dispose();

			// 登録されていた使用テクスチャ名を削除
			DrawManager.Delete(this.RegistNum);

			// 正常な画面遷移なので、自動保存データを削除する
			if (System.IO.File.Exists(OneScreen.AutoSaveFilePath))
			{
				try
				{
					System.IO.File.Delete(OneScreen.AutoSaveFilePath);
				}
				catch (System.Exception e)
				{
					LogFileManager.WriteLineError(Properties.Resources.Msg_OneScr_AutoSaveFileDelete_Failure, OneScreen.AutoSaveFilePath);
					LogFileManager.WriteLineError(e.ToString());
				}
			}
		}


		/// <summary>
		/// 楽譜の自動保存を行う。
		/// </summary>
		/// <param name="sender">。</param>
		/// <param name="e">。</param>
		private void AutoSaveEvent(object sender, AutoSaveEventArgs e)
		{
			LogFileManager.WriteLine(Properties.Resources.Msg_OneScr_AutoSave, string.IsNullOrEmpty(this.ScoreTitle.Text) ? "題名なし" : this.ScoreTitle.Text);
			XmlFileWriter.WriteScoreData(this.CurrentScoreData, true, OneScreen.AutoSaveFilePath);
		}

	}
}
