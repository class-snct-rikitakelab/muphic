using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ConfigurationTool
{
	public partial class ConfigurationWindow : Form
	{
		// muphic 動作設定ウィンドウ
		// ページ４ "バージョン情報" タブの動作の記述

		
		/// <summary>
		/// "バージョン情報"タブのコントロールの初期状態を決定する。
		/// </summary>
		private void TabPage4_Load()
		{
			if (this.tabControl.TabPages[4].IsDisposed) return;

			// 同じディレクトリに muphic.exe が存在するかを確認
			if (File.Exists(Properties.Settings.Default.MuphicFileName))
			{
				// muphic.exe が存在する場合、バージョン情報を取得
				var versionInfo = FileVersionInfo.GetVersionInfo(Properties.Settings.Default.MuphicFileName);

				// ただし、プロダクト名から正規の muphic.exe でないと判断した場合、バージョン情報タブを表示しない
				if (versionInfo.ProductName != Properties.Settings.Default.MuphicProductName)
				{
					this.tabControl.TabPages[4].Dispose();
					this.muphicStartButton.Visible = false;
					return;
				}

				// 各コントロールに muphic.exe の情報を表示
				this.softwareNameLable.Text = versionInfo.ProductName;
				this.softwareDescriptionLabel.Text = versionInfo.Comments;
				this.softwareVersionLabel.Text = "Version " + versionInfo.ProductVersion;
				this.softwareCopyrightLabel.Text = versionInfo.LegalCopyright;
				this.softwareOrganizationLabel.Text = versionInfo.CompanyName;
				this.softwareDeveloperAddressLabel.Text = Properties.Settings.Default.DeveloperAddress;
			}
			else
			{
				// muphic.exe が存在しなかった場合、バージョン情報タブを表示しない
				this.tabControl.TabPages[4].Dispose();
				this.muphicStartButton.Visible = false;
				return;
			}
		}
	}
}
