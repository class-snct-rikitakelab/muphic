using System;
using AxWMPLib;

namespace muphic
{
	/// <summary>
	/// VoiceManager の概要の説明です。
	/// </summary>
	public class VoiceManager
	{
		private static VoiceManager voiceManager;
		public AxWindowsMediaPlayer wmp;
		public VoiceManager(AxWindowsMediaPlayer awmp)
		{
			wmp = awmp;
			voiceManager = this;
		}

		/// <summary>
		/// 音声ファイルの再生をするメソッド
		/// </summary>
		/// <param name="FileName"></param>
		public void PlayVoice(String FileName)
		{
			wmp.URL = FileName;
			wmp.Ctlcontrols.play();
		}

		/// <summary>
		/// 音声ファイルの再生を停止するメソッド
		/// </summary>
		public void StopVoice()
		{
			wmp.Ctlcontrols.stop();
		}


		/// <summary>
		/// mp3などの音声ファイルを再生するメソッド
		/// </summary>
		/// <param name="FileName"></param>
		public static void Play(String FileName)
		{
			muphic.VoiceManager.voiceManager.PlayVoice(FileName);
		}

		/// <summary>
		/// mp3などの音声ファイルの再生を停止するメソッド
		/// </summary>
		public static void Stop()
		{
			muphic.VoiceManager.voiceManager.StopVoice();
		}
	}
}
