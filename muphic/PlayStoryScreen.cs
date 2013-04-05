using System;
using System.Drawing;

using Muphic.Common;
using Muphic.PlayerWorks;
using Muphic.MakeStoryScreenParts;
using Muphic.Manager;
using Muphic.PlayStoryScreenParts.Buttons;
using Muphic.Tools.Player;

namespace Muphic
{
	/// <summary>
	/// 物語の再生状態を表す識別子を指定する。
	/// </summary>
	public enum StoryPlayMode
	{
		/// <summary>
		/// 開幕
		/// </summary>
		CurtainUp,

		/// <summary>
		/// 再生中
		/// </summary>
		Play,

		/// <summary>
		/// 閉幕
		/// </summary>
		DownCurtain,

		/// <summary>
		/// 停止中
		/// </summary>
		Stop,
	}


	/// <summary>
	/// 汎用物語再生画面クラス
	/// <para>各部の物語再生画面 (ものがたりおんがくモード) は、このクラスを継承して作成する。</para>
	/// </summary>
	public abstract class PlayStoryScreen : Screen, IDisposable
	{

		#region フィールドとプロパティ

		/// <summary>
		/// "はじまりはじまり" (物語の再生/停止を行う) ボタン。
		/// </summary>
		public PlayButton PlayButton { get; private set; }

		/// <summary>
		/// 物語再生管理クラス。
		/// </summary>
		public StoryPlayer StoryPlayer { get; private set; }


		/// <summary>
		/// 現在の物語の再生状態。
		/// <para>PlayMode プロパティを使用すること。</para>
		/// </summary>
		private StoryPlayMode __storyPlayMode;

		/// <summary>
		/// 現在の物語の再生状態を取得 (または設定) する。
		/// </summary>
		public StoryPlayMode PlayMode
		{
			get
			{
				return this.__storyPlayMode;
			}
			private set
			{
				this.__storyPlayMode = value;

				// 再生・停止ボタンの有効/無効を切り替える
				switch (value)
				{
					case StoryPlayMode.Play:
						this.PlayButton.Enabled = true;
						this.PlayButton.Pressed = true;
						break;

					case StoryPlayMode.Stop:
						this.PlayButton.Enabled = true;
						this.PlayButton.Pressed = false;
						break;

					case StoryPlayMode.CurtainUp:
					case StoryPlayMode.DownCurtain:
						this.PlayButton.Enabled = false;
						break;
				}
			}
		}


		/// <summary>
		/// 再生を行う物語の題名を取得する。題名が設定されていない場合、このプロパティの値はシステム設定で定義されるデフォルトタイトルとなる。
		/// </summary>
		public string StoryTitle
		{
			get
			{
				return string.IsNullOrEmpty(this.StoryPlayer.PlayData.Title) ? Settings.System.Default.StoryMake_DefaultTitle : this.StoryPlayer.PlayData.Title;
			}
		}

		#endregion


		#region コンストラクタ/初期化

		/// <summary>
		/// 汎用物語再生画面の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="playData">再生を行う物語データ。</param>
		protected PlayStoryScreen(StoryData playData)
		{
			// 部品のインスタンス化など
			this.Initialization();

			// 再生クラスの生成
			this.StoryPlayer = new StoryPlayer();
			this.StoryPlayer.PlayData = playData;

			this.PlayMode = StoryPlayMode.Stop;
		}


		/// <summary>
		/// 汎用物語再生画面を構成する各パーツのインスタンス化と登録を行う。
		/// </summary>
		protected override void Initialization()
		{
			// 登録開始
			base.Initialization(Settings.ResourceNames.PlayStoryScreenImages);

			#region 統合画像の読み込み

			this.LoadImageFiles();

			#endregion

			#region 部品のインスタンス化

			this.PlayButton = new PlayButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.PlayButton);

			#endregion

			#region テクスチャと座標の登録

			DrawManager.Regist(
				this.ToString(),
				Settings.PartsLocation.Default.PlayStoryScr.Location,
				"IMAGE_PLAYSTORYSCR_BG"
			);

			#endregion

			// 登録終了
			DrawManager.EndRegist();
		}

		#endregion


		#region 描画

		/// <summary>
		/// 汎用物語再生画面の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			// タイトルの描画
			StringManager.DrawCenter(this.StoryTitle, Settings.PartsLocation.Default.PlayStoryScr_TitleCenter);
			
			switch (this.PlayMode)
			{
				case StoryPlayMode.Stop:
					this.DrawStage(drawStatus, Color.White);
					break;

				case StoryPlayMode.CurtainUp:
					this.DrawStage(drawStatus, Color.White);
					this.PlayMode = StoryPlayMode.Play;
					this.StoryPlayer.Start();
					break;

				case StoryPlayMode.DownCurtain:
					this.DrawStage(drawStatus, Color.White);
					this.PlayMode = StoryPlayMode.Stop;
					break;

				case StoryPlayMode.Play:
					// 再生中のスライドの絵と文章、その上からステージの幕を描画
					PictureWindow.DrawSlide(this.StoryPlayer.CurrentSlide, Settings.PartsLocation.Default.PlayStoryScr_PictureArea.Location, 1.0F);
					StringManager.DrawCenter(this.StoryPlayer.CurrentSlide.Sentence, Settings.PartsLocation.Default.PlayStoryScr_SentenceCenter);
					this.DrawStage(drawStatus, Color.FromArgb(180, 180, 180));

					// 更に、曲の再生処理を実行し、再生が完了したら再生終了処理に移行
					if (!this.StoryPlayer.Play()) this.PlayMode = StoryPlayMode.DownCurtain;
					break;
			}
		}


		/// <summary>
		/// ステージの幕の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態。</param>
		/// <param name="filter">色フィルタ。</param>
		public void DrawStage(DrawStatusArgs drawStatus, Color filter)
		{
			DrawManager.Draw("IMAGE_PLAYSTORYSCR_STAGE_TOP", Settings.PartsLocation.Default.PlayStoryScr_StageTop, filter);
			DrawManager.Draw("IMAGE_PLAYSTORYSCR_STAGE_LEFT", Settings.PartsLocation.Default.PlayStoryScr_StageLeft, filter);
			DrawManager.Draw("IMAGE_PLAYSTORYSCR_STAGE_RIGHT", Settings.PartsLocation.Default.PlayStoryScr_StageRight, filter);
		}

		#endregion


		#region 物語の再生/停止

		/// <summary>
		/// 物語再生画面上で、物語の再生を開始する。
		/// </summary>
		public void PlayStart()
		{
			this.PlayMode = StoryPlayMode.CurtainUp;
			Tools.DebugTools.ConsolOutputMessage("PlayStoryScreen -PlayStart", "物語再生 -- \"" + this.StoryTitle + "\"", true);
		}

		/// <summary>
		/// 物語再生画面上で、物語の再生を停止する。
		/// </summary>
		public void PlayStop()
		{
			this.StoryPlayer.Stop();
			this.PlayMode = StoryPlayMode.Stop;
			Tools.DebugTools.ConsolOutputMessage("PlayStoryScreen -PlayStop", "物語再生停止 (ユーザー操作)", true);
		}

		#endregion


		#region 解放

		/// <summary>
		/// 汎用物語再生画面で登録したテクスチャ名を、描画管理クラスから削除する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion

	}
}
