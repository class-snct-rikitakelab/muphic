using System;
using System.Collections.Generic;
using System.Diagnostics;

using Muphic.Tools;

namespace Muphic.Manager
{
	/// <summary>
	/// フレーム管理クラス (シングルトン・継承不可) 
	/// <para>
	/// muphic の起動時間や単位フレーム当たりの実行時間及び FPS の計算・制御を行う。
	/// </para>
	/// </summary>
	public sealed class FrameManager : Manager
	{
		/// <summary>
		/// フレーム管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static FrameManager __instance = new FrameManager();

		/// <summary>
		/// フレーム管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static FrameManager Instance
		{
			get { return FrameManager.__instance; }
		}


		#region 自動保存イベント関連

		/// <summary>
		/// システムの自動保存の設定に基づいた、一定の時間間隔毎に発生する。
		/// <para>各画面での楽譜や物語の自動的なバックアップを行う際に使用する。</para>
		/// </summary>
		public static event AutoSaveEventHandler AutoSaveEvent;

		/// <summary>
		/// 自動保存時に使用するデータ。
		/// </summary>
		private AutoSaveEventArgs AutoSaveEventArgs { get; set; }

		/// <summary>
		/// 前回自動保存した時の経過時間 (分単位) を表わす実数値を取得または設定する。
		/// </summary>
		private double AutoSaveTime { get; set; }

		#endregion


		#region コンストラクタ/初期化

		/// <summary>
		/// フレーム管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private FrameManager()
		{
			this.StopWatch = new Stopwatch();
			this.Process = new List<float>();
			this.AutoSaveTime = 0.0;
			this.MemoryCheckTime = double.MinValue;
			this.AutoSaveEventArgs = new AutoSaveEventArgs();
		}

		/// <summary>
		/// フレーム管理クラスの静的インスタンスの初期化を行う。
		/// <para>メイン画面生成時にこのメソッドを呼び出すことで、シングルトンパターンの静的インスタンスを生成する。</para>
		/// </summary>
		private bool _Initialize()
		{
			if (this._IsInitialized) return false;

			this.__idealFps = ConfigurationManager.Current.Fps;
			this.__idealTimePerFrame = 1000.0F / this.IdealFps;

			Tools.DebugTools.ConsolOutputMessage("FrameManager -Initialize", "フレーム管理クラス生成完了", true);

			return this._IsInitialized = true;
		}

		#endregion


		#region FPS 制御プロパティ群

		/// <summary>
		/// 設定された (理想的な) FPS 値。
		/// </summary>
		private int __idealFps;

		/// <summary>
		/// 設定された (理想的な) FPS 値を取得する。通常は、設定ファイルで指定された 60 または 30。
		/// </summary>
		private int IdealFps
		{
			get { return this.__idealFps; }
		}

		/// <summary>
		/// 現在の実 FPS 値を取得する。
		/// </summary>
		private float NowFps { get; set; }

		/// <summary>
		/// 現在の実 FPS 値を取得する。
		/// </summary>
		public static float Fps
		{
			get { return FrameManager.Instance.NowFps; }
		}

		/// <summary>
		/// muphic が起動されてからの時間を表わす System.TimeSpan 構造体を取得する。
		/// </summary>
		/// <remarks>
		/// 正確には、管理クラスの生成が完了しメインウィンドウが表示される直前から計測が開始された時間となる。
		/// </remarks>
		public static TimeSpan PlayTime
		{
			get { return Muphic.Manager.FrameManager.Instance.StopWatch.Elapsed; }
		}

		/// <summary>
		/// 1 フレーム毎の処理時間のリストを取得する。
		/// </summary>
		private List<float> Process { get; set; }

		/// <summary>
		/// 1 フレームにかかる時間の理想値。
		/// </summary>
		private float __idealTimePerFrame;

		/// <summary>
		/// 1 フレームにかかる時間の理想値を取得する。
		/// </summary>
		private float IdealTimePerFrame
		{
			get { return this.__idealTimePerFrame; }
		}

		/// <summary>
		/// 更新前の時間を取得または設定する。
		/// </summary>
		private float OldTime { get; set; }

		/// <summary>
		/// ストップウォッチ
		/// </summary>
		private Stopwatch StopWatch { get; set; }

		/// <summary>
		/// 前回メモリ使用量を計測した時の経過時間 (分単位) を表わす実数値を取得または設定する。
		/// </summary>
		private double MemoryCheckTime { get; set; }

		#endregion


		#region FPS 制御

		/// <summary>
		/// フレームのカウントを開始する
		/// </summary>
		private void _Start()
		{
			this.StopWatch.Start();
			this.OldTime = this.StopWatch.ElapsedMilliseconds;
		}


		/// <summary>
		/// 前回呼ばれてから何ミリ秒経過したかを蓄積し、FPS の計算を行う。
		/// </summary>
		private void _FPSUpdate()
		{
			// ストップウォッチを起動してから現在までの経過時間
			float currentTime = this.StopWatch.ElapsedMilliseconds;

			// 前回から今回までの経過時間を処理時間リストに追加
			this.Process.Add(currentTime - this.OldTime);

			if (this.Process.Count == this.IdealFps)
			{
				// 経過時間リストが指定されたFPS値に達した場合

				float sum = 0.0F;

				// 経過時間リスト内の処理時間の平均を算出する
				for (int i = 0; i < this.Process.Count; i++) sum += this.Process[i];
				sum /= this.Process.Count;

				// 1秒から平均処理時間を割り、1秒間のフレーム数を求める(FPS値更新)
				this.NowFps = 1000.0F / sum;

				// 経過時間リストをクリアする
				this.Process.Clear();

				// 現在の経過時間(分)から、前回自動保存した時間を引く その差が自動保存間隔(分)を超えていたら、自動保存イベント発生。
				if (this.StopWatch.Elapsed.TotalMinutes - this.AutoSaveTime > ConfigurationManager.Current.AutoSaveInterval && ConfigurationManager.Current.AutoSaveInterval > 0)
				{
					// ハンドラが1つ以上登録されていれば、自動保存イベントを発生させる。
					if (FrameManager.AutoSaveEvent != null)
					{
						// イベント発生を通知
						DebugTools.ConsolOutputMessage("FrameManager", "自動保存イベント発生", true);

						FrameManager.AutoSaveEvent(FrameManager.Instance, this.AutoSaveEventArgs);
					}

					// 自動保存が発生した時間を保存しておく
					this.AutoSaveTime = this.StopWatch.Elapsed.TotalMinutes;
				}

				// 自動的にメモリ使用量の計測
				if (this.StopWatch.Elapsed.TotalMinutes - this.MemoryCheckTime > Settings.System.Default.MemoryCheckSpan)
				{
					this.MemoryCheckTime = this.StopWatch.Elapsed.TotalMinutes;

					LogFileManager.WriteLine(CommonTools.GetResourceMessage(
						Properties.Resources.Msg_FrameMgr_CheckUsedMemory,
						this.MemoryCheckTime.ToString("0"),
						SystemInfoManager.UsedPhysicalMemory)
					);

					//Process current = System.Diagnostics.Process.GetCurrentProcess();
					//DebugTools.ConsolOutputMessage(String.Format("WorkingSet64                {0} MB", (current.WorkingSet64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("VirtualMemorySize64         {0} MB", (current.VirtualMemorySize64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("NonpagedSystemMemorySize64  {0} MB", (current.NonpagedSystemMemorySize64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("PagedMemorySize64           {0} MB", (current.PagedMemorySize64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("PagedSystemMemorySize64     {0} MB", (current.PagedSystemMemorySize64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("PeakPagedMemorySize64       {0} MB", (current.PeakPagedMemorySize64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("PeakVirtualMemorySize64     {0} MB", (current.PeakVirtualMemorySize64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("PrivateMemorySize64         {0} MB", (current.PrivateMemorySize64) / 1024 / 1024));
					//DebugTools.ConsolOutputMessage(String.Format("PeakWorkingSet64            {0} MB", (current.PeakWorkingSet64) / 1024 / 1024));
				}
			}

			//if (currentTime - this.OldTime > this.OneFrame)
			//{
			//    // 1フレームにかかる時間の理想値をオーバーしていた場合
			//}
			//else
			//{
			//    // 理想値に達していない場合,理想値になるまで遅延させる
			//    // Releaseで実行すると何故か落ちるため保留
			//    // それ以前に遅延させなくても60で安定する謎 … DirectX の仕業？
			//    // System.Threading.Thread.Sleep((int)(this.oneFrame - (currentTime - this.oldTime)));
			//}

			// 現在の経過時間でoldTimeを更新
			this.OldTime = currentTime;
		}

		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// フレーム管理クラスの静的インスタンスを生成する。
		/// <para>シングルトンパターンとして一度生成された後はこのメソッドは意味を為さないので注意。</para>
		/// </summary>
		public static void Initialize()
		{
			Muphic.Manager.FrameManager.Instance._Initialize();
		}


		/// <summary>
		/// フレームのカウントを開始する。
		/// </summary>
		public static void CountStart()
		{
			Muphic.Manager.FrameManager.Instance._Start();
		}


		/// <summary>
		/// フレーム制御を行う。
		/// </summary>
		public static void Update()
		{
			Muphic.Manager.FrameManager.Instance._FPSUpdate();
		}

		#endregion

	}


	#region 自動保存イベント

	/// <summary>
	/// 自動保存のイベントハンドラ。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e">自動保存イベントのデータ。</param>
	public delegate void AutoSaveEventHandler(object sender, AutoSaveEventArgs e);


	/// <summary>
	/// 自動保存に関するイベントのデータを提供する。
	/// </summary>
	public class AutoSaveEventArgs : System.EventArgs
	{
		/// <summary>
		/// 保存先のパスを表わす System.String 。
		/// </summary>
		public string SaveLocation { get; private set; }

		/// <summary>
		/// Muphic.Manager.Event.AutoSaveEventArgs の新しインスタンスを初期化する。
		/// </summary>
		public AutoSaveEventArgs()
			: this("")
		{
		}

		/// <summary>
		/// Muphic.Manager.Event.AutoSaveEventArgs の新しインスタンスを初期化する。
		/// </summary>
		/// <param name="saveLocation">保存先のパス。</param>
		public AutoSaveEventArgs(string saveLocation)
		{
			this.SaveLocation = saveLocation;
		}
	}

	#endregion

}
