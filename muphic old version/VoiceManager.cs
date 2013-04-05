using System;
using AxWMPLib;

namespace muphic
{
	/// <summary>
	/// VoiceManager �̊T�v�̐����ł��B
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
		/// �����t�@�C���̍Đ������郁�\�b�h
		/// </summary>
		/// <param name="FileName"></param>
		public void PlayVoice(String FileName)
		{
			wmp.URL = FileName;
			wmp.Ctlcontrols.play();
		}

		/// <summary>
		/// �����t�@�C���̍Đ����~���郁�\�b�h
		/// </summary>
		public void StopVoice()
		{
			wmp.Ctlcontrols.stop();
		}


		/// <summary>
		/// mp3�Ȃǂ̉����t�@�C�����Đ����郁�\�b�h
		/// </summary>
		/// <param name="FileName"></param>
		public static void Play(String FileName)
		{
			muphic.VoiceManager.voiceManager.PlayVoice(FileName);
		}

		/// <summary>
		/// mp3�Ȃǂ̉����t�@�C���̍Đ����~���郁�\�b�h
		/// </summary>
		public static void Stop()
		{
			muphic.VoiceManager.voiceManager.StopVoice();
		}
	}
}
