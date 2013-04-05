using Muphic.PlayerWorks;
using Muphic.CompositionScreenParts;

namespace Muphic.Tools.Player
{
	/// <summary>
	/// muphic で作曲された曲を再生するためのメソッドとプロパティのセットを提供する。
	/// </summary>
	public class ScorePlayer
	{
		/// <summary>
		/// 再生する対象となる曲の楽譜データ。
		/// </summary>
		private ScoreData __playData;

		/// <summary>
		/// 再生する対象となる曲の楽譜データを取得または設定する。
		/// </summary>
		public ScoreData PlayData
		{
			get
			{
				return this.__playData;
			}
			set
			{
				if (this.IsPlaying)
				{
					Tools.DebugTools.ConsolOutputError("ScorePlayer -PlayData", "再生中のため、楽譜データをセットできません。");
				}
				else
				{
					this.__playData = value;	// 新しい曲を設定し、
					this.Reset();				// シーク位置を先頭に戻す
				}
			}
		}


		/// <summary>
		/// 曲の再生中であることを示す値。
		/// </summary>
		private bool __isPlaying;

		/// <summary>
		/// 曲の再生中であることを示す値を取得する。
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
					this.__isPlaying = false;			// 楽譜データが null の時は、再生状態にできないようにする
					Tools.DebugTools.ConsolOutputError("ScorePlayer -IsPlaying", "楽譜データがセットされていないため、再生を開始できません。");
				}
				else
				{
					this.__isPlaying = value;
				}
			}
		}


		/// <summary>
		/// 再生時のピクセル単位でのオフセットを表す実数を取得または設定する。
		/// <para>フレーム毎にこのプロパティの値にテンポ値を加えていくことで、再生開始から動物が何ピクセル動いたかを表す。</para>
		/// </summary>
		private float PlayOffset { get; set; }

		/// <summary>
		/// 再生のチェックを行う動物の AnimalList 内での開始番号を表す整数を取得または設定する。
		/// <para>既に再生が完了した動物はこの Index より前にある状態となるため、無駄なループを省くことができる。</para>
		/// </summary>
		private int NowAnimalIndex { get; set; }

		/// <summary>
		/// 家の中央部分を示す X 座標。再生中に動物がこの線を越えると音を鳴らす。
		/// </summary>
		private readonly int houseCenter = Locations.HouseCenter.X;


		/// <summary>
		/// Muphic.Player.ScorePlayer クラスの新しいインスタンスを初期化する。
		/// </summary>
		public ScorePlayer()
		{
			this.IsPlaying = false;

			var s = new System.Diagnostics.Stopwatch();
		}


		/// <summary>
		/// PlayData プロパティで設定された楽譜データの再生を再開する。
		/// </summary>
		public void Start()
		{
			if (this.IsPlaying) return;

			Tools.DebugTools.ConsolOutputMessage(
				"ScorePlayer -Start",
				string.Format("曲再生 -- \"{0}\"", string.IsNullOrEmpty(this.PlayData.ScoreName) ? "(無題)" : this.PlayData.ScoreName),
				true
			);

			this.IsPlaying = true;
		}

		/// <summary>
		/// 曲の再生を一時停止する。
		/// </summary>
		public void Stop()
		{
			if (!this.IsPlaying) return;

			Tools.DebugTools.ConsolOutputMessage(
				"ScorePlayer -Stop",
				string.Format("曲停止 -- \"{0}\"", string.IsNullOrEmpty(this.PlayData.ScoreName) ? "(無題)" : this.PlayData.ScoreName),
				true
			);

			this.IsPlaying = false;
		}

		/// <summary>
		/// PlayData プロパティで設定された楽譜データのシーク位置を、曲の先頭に戻す。
		/// </summary>
		public void Reset()
		{
			this.NowAnimalIndex = 0;
			this.PlayOffset = 0.0F;
		}


		/// <summary>
		/// 1 フレーム単位で実行される、曲の再生処理。
		/// </summary>
		/// <returns>次のフレームでも再生が継続される場合は true、再生が停止している場合は false。</returns>
		public bool Play()
		{
			// 再生中でなければ、再生処理を行わず終了
			if (!this.IsPlaying) return false;

			// 以下、汎用作曲画面と同一の方法で曲の再生

			// フレーム毎にテンポ値をオフセットに加算していく
			this.PlayOffset += (this.PlayData.Tempo > 0) ? this.PlayData.Tempo : 0.5F;

			// 再生チェック
			for (int i = this.NowAnimalIndex; i < this.PlayData.AnimalList.Count; i++)
			{
				// i番目の動物の横位置からx座標を得て、オフセットを引く (動物の移動の計算)
				int location = Tools.CompositionTools.ScoreToPoint(this.PlayData.AnimalList[i]).X - (int)this.PlayOffset;

				if (location <= houseCenter)
				{
					// i番目の動物のx座標が家の左側だった場合
					Manager.SoundManager.Play(this.PlayData.AnimalList[i]);		// その動物の音を再生
					this.NowAnimalIndex = i + 1;								// i番目の動物が再生済みとなったため、
				}																// 次のフレームではi+1番目の動物から再生処理を行うようにする
			}

			if (this.NowAnimalIndex >= this.PlayData.AnimalList.Count)
			{
				// 全ての動物の再生処理が完了した場合、再生を停止し、シーク位置を先頭に戻す。
				this.Stop();
				this.Reset();
			}

			return this.IsPlaying;
		}

	}
}
