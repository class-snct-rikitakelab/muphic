using System;
using System.ComponentModel;
using System.Windows.Forms;

using Microsoft.DirectX.Direct3D;
using ConfigurationTool.Manager;

namespace ConfigurationTool
{
	using DXManager = Microsoft.DirectX.Direct3D.Manager;

	public partial class ConfigurationWindow : Form
	{
		// muphic 動作設定ウィンドウ
		// ページ１ "画面とサウンド"タブの動作の記述


		#region 設定の読込と保存

		/// <summary>
		/// 指定した構成設定データから、"画面とサウンド" タブのコントロールの状態を決定する。
		/// </summary>
		/// <param name="loadData">読み込む構成設定データ。</param>
		private void TabPage1_Load(ConfigurationData loadData)
		{
			// DirectX エラーのラベルはデフォで非表示
			this.directxErrorLabel.Visible = false;

			try
			{
				// ディスプレイアダプタの設定のロード
				this.GetAdapterInfo(loadData.AdapterNum);
			}
			catch (Exception e)
			{
				// ここで何らかの例外が起きたら、多分 DirectX のエラー
				// メッセージを出力してこのタブのコントロールは全て無効化する
				string path = Program.WriteErrorLog(e.ToString());

				MessageBox.Show(
					Properties.Resources.ErrorMsg_Show_FailureDirectX.Replace("%str1%", path),
					Properties.Resources.ErrorMsg_Show_FailureDirectX_Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				this.adapterSelectComboBox.Items.Clear();
				this.graphicGroupBox.Enabled = false;
				this.soundGroupBox.Enabled = false;
				this.systemInfoTabResetButton.Enabled = false;
				this.directxErrorLabel.Visible = true;

				return;
			}

			// FPS の設定のロード (といいつつロードせず 60 固定)
			this.fpsSelectComboBox.Items.Clear();
			this.fpsSelectComboBox.Items.Add(60);
			this.fpsSelectComboBox.SelectedIndex = 0;

			// ウィンドウモードの設定のロード
			this.isFullScreenModeCheckBox.Checked = !loadData.IsWindow;
			this.SetEnabledRefreshrate();

			// リフレッシュレートの設定のロード
			this.GetRefreshRateInfo(this.adapterSelectComboBox.SelectedIndex, loadData.RefreshRate);

			// テクスチャ先読みの設定のロード
			this.isLoadTexturePreliminarilyCheckBox.Checked = loadData.IsLoadTextureFilePreliminarily;

			// ボリュームの設定のロード
			this.SetVolume(loadData.DirectSoundVolume);
		}


		/// <summary>
		/// "画面とサウンド" タブのコントロールの状態から、指定した構成設定データへ各設定情報を書き込む。
		/// </summary>
		/// <param name="saveData">保存先の構成設定データ。</param>
		private void TabPage1_Save(ConfigurationData saveData)
		{
			// DirectX エラーのラベルが可視だったらセーブしちゃダメ
			if (this.directxErrorLabel.Visible) return;

			// アダプタの設定のセーブ
			saveData.AdapterNum = this.adapterSelectComboBox.SelectedIndex;

			// FPS の設定のセーブ
			saveData.Fps = (int)this.fpsSelectComboBox.SelectedItem;

			// ウィンドウモードの設定のセーブ
			saveData.IsWindow = !this.isFullScreenModeCheckBox.Checked;

			// リフレッシュレートの設定のセーブ
			saveData.RefreshRate = (int)this.refreshrateSelectComboBox.SelectedItem;

			// テクスチャ先読みの設定のセーブ
			saveData.IsLoadTextureFilePreliminarily = this.isLoadTexturePreliminarilyCheckBox.Checked;

			// ボリュームの設定のセーブ
			saveData.DirectSoundVolume = this.volumeTrackBar.Value;
		}


		/// <summary>
		/// "規定値に戻す" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void systemInfoTabResetButton_Click(object sender, EventArgs e)
		{
			if (!this.ConfirmReset()) return;

			this.TabPage1_Load(new ConfigurationData());
		}

		#endregion


		#region 画面の設定

		#region アダプタ取得

		/// <summary>
		/// コンピュータにインストールされているグラフィックアダプタの情報を取得し、アダプタ選択コンボボックスを作成する。
		/// </summary>
		/// <param name="displayNum">構成設定で指定されているアダプタ番号。</param>
		private void GetAdapterInfo(int displayNum)
		{
			// まず既存のアダプタ選択コンボボックスクリア
			this.adapterSelectComboBox.Items.Clear();

			// アダプタの数だけループし、それぞれのアダプタの名称と解像度からリストを作成する。
			for (int i = 0; i < DXManager.Adapters.Count; i++)
			{
				AdapterInformation adapterInfo = DXManager.Adapters[i];

				string item = string.Format(
					"{0}. {1} ({2}x{3})", i + 1,
					adapterInfo.Information.Description,
					adapterInfo.CurrentDisplayMode.Width,
					adapterInfo.CurrentDisplayMode.Height
				);

				this.adapterSelectComboBox.Items.Add(item);
			}

			// アダプタが一つも検出できなかった場合 (そんなことありえなくね？)
			// デフォルトのリストを追加し選択できなくする
			if (this.adapterSelectComboBox.Items.Count < 1)
			{
				this.adapterSelectComboBox.Items.Add(Properties.Resources.Form_AdapterSelectComboBox_FailureAdapter);
				this.adapterSelectComboBox.Enabled = false;
			}

			// 最後に、構成設定で指定されているアダプタ番号と同じ要素を選択させる
			// アダプタ番号が要素外だったら、仕方ないから 0 番目を選択させる
			if (displayNum < this.adapterSelectComboBox.Items.Count)
			{
				this.adapterSelectComboBox.SelectedIndex = displayNum;
			}
			else
			{
				this.adapterSelectComboBox.SelectedIndex = 0;
			}
		}

		#endregion


		#region リフレッシュレート

		/// <summary>
		/// アダプタ選択コンボボックスの選択状態が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AdapterSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.GetRefreshRateInfo(this.adapterSelectComboBox.SelectedIndex, this.Current.RefreshRate_DefaultValue);
		}

		/// <summary>
		/// 指定したアダプタでサポートされるリフレッシュレートを列挙し、コンボボックスを生成する。
		/// </summary>
		/// <param name="adapterNum"></param>
		/// <param name="refreshRate"></param>
		private void GetRefreshRateInfo(int adapterNum, int refreshRate)
		{
			// 同じ数値のリフレッシュレートが登録されるのを防ぐため
			var refreshRateList = new System.Collections.Generic.List<int>();

			// リフレッシュレート選択コンボボックスをクリア
			this.refreshrateSelectComboBox.Items.Clear();

			// 指定したアダプタがサポートするリフレッシュレートを列挙し、
			foreach (DisplayMode dm in DXManager.Adapters[adapterNum].SupportedDisplayModes)
			{
				// 800x600 以外は全て弾く
				if (!(dm.Width == 800 && dm.Height == 600)) continue;

				// リフレッシュレートが既にリストに登録されていないかをチェック
				if (refreshRateList.Contains(dm.RefreshRate)) continue;

				// 登録されていなければ、コンボボックスとリストへ追加
				this.refreshrateSelectComboBox.Items.Add(dm.RefreshRate);
				refreshRateList.Add(dm.RefreshRate);

				// 構成設定で登録されるリフレッシュレートと同じ数値なら、それを選択する
				if (dm.RefreshRate == refreshRate) this.refreshrateSelectComboBox.SelectedItem = dm.RefreshRate;
			}

			// リフレッシュレートが一つも無かった場合 (そんなことありえなくね？)
			// とりあえずデフォルト値入れて選択させとく
			if (this.refreshrateSelectComboBox.Items.Count < 1)
			{
				this.refreshrateSelectComboBox.Items.Add(this.Current.RefreshRate_DefaultValue);
				this.refreshrateSelectComboBox.SelectedIndex = 0;
			}

			// リフレッシュレートのどれも選択されていない場合
			if (this.refreshrateSelectComboBox.SelectedIndex == -1)
			{
				// まずデフォルト値がコンボボックスにあるかをチェック
				if (this.refreshrateSelectComboBox.Items.Contains(this.Current.RefreshRate_DefaultValue))
				{
					// あればそれを選択
					this.refreshrateSelectComboBox.SelectedItem = this.Current.RefreshRate_DefaultValue;
				}
				else
				{
					// 無ければ仕方ないので 0 番目を選択
					this.refreshrateSelectComboBox.SelectedIndex = 0;
				}
			}
		}

		#endregion


		#region フルスクリーンモード

		/// <summary>
		/// "フルスクリーンモード" チェックボックスの状態が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void isFullScreenModeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.SetEnabledRefreshrate();
		}

		/// <summary>
		/// フルスクリーンモードの設定に応じて、リフレッシュレートに関連するコントロールの有効 / 無効を切り替える。
		/// </summary>
		private void SetEnabledRefreshrate()
		{
			this.refreshrateLabel.Enabled = this.isFullScreenModeCheckBox.Checked;
			this.refreshrateSelectComboBox.Enabled = this.isFullScreenModeCheckBox.Checked;
		}


		#endregion

		#endregion


		#region ボリュームの設定

		/// <summary>
		/// 与えられた音量を、トラックバーの限界を超えない範囲で設定する。
		/// </summary>
		/// <param name="volume"></param>
		private void SetVolume(int volume)
		{
			if (volume < this.volumeTrackBar.Minimum) this.volumeTrackBar.Value = this.volumeTrackBar.Minimum;
			else if (this.volumeTrackBar.Maximum < volume) this.volumeTrackBar.Value = this.volumeTrackBar.Maximum;
			else this.volumeTrackBar.Value = volume;
		}

		#endregion


		#region MouseEnter

		/// <summary>
		/// "機能と作曲の動作"タブにマウスが入力された際に、ヘルプテキストをデフォルトに更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPage1_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescriptionDefault();
		}

		#endregion

	}
}