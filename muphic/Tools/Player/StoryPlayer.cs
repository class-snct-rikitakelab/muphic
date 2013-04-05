using Muphic.PlayerWorks;
using Muphic.MakeStoryScreenParts;

namespace Muphic.Tools.Player
{
	/// <summary>
	/// muphic で作成された物語を再生するためのメソッドとプロパティのセットを提供する。
	/// </summary>
	public class StoryPlayer
	{
		#region 物語データ/スライドに関するプロパティ

		/// <summary>
		/// 再生する対象となる物語のデータ。
		/// </summary>
		private StoryData __playData;

		/// <summary>
		/// 再生する対象となる物語データを取得または設定する。
		/// </summary>
		public StoryData PlayData
		{
			get
			{
				return this.__playData;
			}
			set
			{
				if (this.IsPlaying)
				{
					Tools.DebugTools.ConsolOutputError("StoryPlayer -PlayData", "再生中のため、物語データをセットできません。");
				}
				else
				{
					this.__playData = value;
				}
			}
		}


		/// <summary>
		/// 現在再生中のスライドの SlideList 内の番号。
		/// <para>CurrentSlideNumber プロパティを使用すること。</para>
		/// </summary>
		private int __currentSlideNumber;

		/// <summary>
		/// 現在再生中のスライドの SlideList 内の番号を取得 (または設定) する。
		/// </summary>
		public int CurrentSlideNumber
		{
			get
			{
				return this.__currentSlideNumber;
			}
			private set
			{
				// 再生スライドが編集済みのページを超えたら再生を停止する。
				if (value > this.PlayData.MaxEditedPage)
				{
					this.Stop();
					return;
				}

				Tools.DebugTools.ConsolOutputMessage(
					"StoryPlayer -CurrentSlideNumber",
					string.Format("物語再生 -- {0}/{1} ページ", value, this.PlayData.MaxEditedPage),
					true
				);

				this.__currentSlideNumber = value;							// 再生対象をセット
				this.ScorePlayer.PlayData = this.CurrentSlide.ScoreData;	// 曲の再生対象を、設定されたスライドの曲にセット
				this.ScorePlayer.Reset();									// 曲のシーク位置
				this.ScorePlayer.Start();									// 曲の再生開始を通知
				this.InterludeFrame = 0;									// スライド間フレームを初期化
			}
		}

		/// <summary>
		/// 現在再生中のスライドを取得する。
		/// </summary>
		public Slide CurrentSlide
		{
			get
			{
				return PlayData.SlideList[this.CurrentSlideNumber];
			}
		}

		#endregion


		#region 物語の再生に関するプロパティ

		/// <summary>
		/// 物語の再生中であることを示す値。
		/// </summary>
		private bool __isPlaying;

		/// <summary>
		/// 物語の再生中であることを示す値を取得する。
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				return this.__isPlaying;
			}
			set
			{
				if (value && this.PlayData == null)
				{
					this.__isPlaying = false;			// 物語データが null の時は、再生状態にできないようにする
					Tools.DebugTools.ConsolOutputError("ScorePlayer -IsPlaying", "物語データがセットされていないため、再生を開始できません。");
				}
				else
				{
					this.__isPlaying = value;
				}
			}
		}

		/// <summary>
		/// 曲の再生に使用するプレイヤー。
		/// </summary>
		private ScorePlayer ScorePlayer { get; set; }
		

		/// <summary>
		/// 次のスライドへ遷移するまでのフレーム数を表す整数を取得または設定する。
		/// <para>スライドの曲の再生が終わった後も、このプロパティの値が 0 になるまで CurrentSlideNumber の値は更新されず、次のスライドへは遷移しない。</para>
		/// </summary>
		private int InterludeFrame { get; set; }

		#endregion

		/// <summary>
		/// Muphic.Player.StoryPlayer クラスの新しいインスタンスを初期化する。
		/// </summary>
		public StoryPlayer()
		{
			this.IsPlaying = false;
			this.ScorePlayer = new ScorePlayer();
		}


		/// <summary>
		/// PlayData プロパティで設定された物語の再生を再開する。
		/// </summary>
		public void Start()
		{
			this.Start(0);
		}

		/// <summary>
		/// PlayData プロパティで設定された物語の指定したページから再生を再開する。
		/// </summary>
		/// <param name="startNumber">再生を開始するページの SlideList 内の番号。</param>
		public void Start(int startNumber)
		{
			if (this.IsPlaying) return;

			Tools.DebugTools.ConsolOutputMessage(
				"StoryPlayer -Start",
				string.Format("物語 {0}/{1} ページから再生開始", this.CurrentSlideNumber, this.PlayData.MaxEditedPage),
				true
			);

			this.IsPlaying = true;
			if (this.IsPlaying) this.CurrentSlideNumber = startNumber;
		}


		/// <summary>
		/// 物語の再生を停止する。
		/// </summary>
		public void Stop()
		{
			if (!this.IsPlaying) return;

			this.ScorePlayer.Stop();

			Tools.DebugTools.ConsolOutputMessage(
				"StoryPlayer -Stop",
				string.Format("物語 {0}/{1} ページで再生停止", this.CurrentSlideNumber, this.PlayData.MaxEditedPage),
				true
			);

			this.IsPlaying = false;
		}


		/// <summary>
		/// 1 フレーム単位で実行される、スライドの曲の再生処理。
		/// </summary>
		/// <returns>次のフレームでも再生が継続される場合は true、再生が停止している場合は false。</returns>
		public bool Play()
		{
			if (!this.IsPlaying) return false;

			if (this.InterludeFrame > 0)
			{																// スライドとスライドの間だった場合
				this.InterludeFrame--;										// 幕間フレーム数をカウントダウン
				if (this.InterludeFrame <= 0) this.CurrentSlideNumber++;	// 幕間フレーム数が 0 に達したら、次のスライドへ移行
			}
			else
			{																// 上記以外の場合、スライドの曲の再生を行う。
				if (!this.ScorePlayer.Play())								// 曲の再生が終了したら (ScorePlayer.Play() から false が返ってきたら)、
				{															// 幕間フレームに移行
					this.InterludeFrame = Settings.System.Default.StoryPlayer_InterludeFrame;
				}
			}

			return this.IsPlaying;
		}

	}
}
