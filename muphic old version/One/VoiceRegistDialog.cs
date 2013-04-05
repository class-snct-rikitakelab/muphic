using System;
using muphic.One.VoiceDialog;
using muphic.tag;

namespace muphic.One
{
	/// <summary>
	/// VoiceDialog �̊T�v�̐����ł��B
	/// </summary>
	public class VoiceRegistDialog : Screen
	{
		public OneScreen parent;
		public StartButton start;
		public muphic.One.VoiceDialog.BackButton back;
		private bool isRecording = false;
		public bool isCountDown = false;
		int MaxRecordCount = 60;								//�^������t���[���J�E���g���@180�ɂ����3�b�^��
		int RecordCount = 0;									//���݂̘^���t���[���J�E���g

		public bool IsRecording
		{
			get
			{
				return isRecording;
			}
			set
			{
				isRecording = value;
				if(value)
				{
					start.Visible = false;
					back.Visible = false;
					RecordCount = MaxRecordCount;
					SoundManager.StartRecord();
				}
				else
				{
					SoundManager.Delete("Voice1.wav");
					SoundManager.Delete("Voice2.wav");
					SoundManager.Delete("Voice3.wav");
					SoundManager.Delete("Voice4.wav");
					SoundManager.Delete("Voice5.wav");
					SoundManager.Delete("Voice6.wav");
					SoundManager.Delete("Voice7.wav");
					SoundManager.Delete("Voice8.wav");
					SoundManager.Delete("VoiceCheck.wav");
					SoundManager.StopRecord();
					DrawManager.End();
					DrawManager.Begin(false);
					DrawManager.Draw("Nowloading_bak");	// �w�i�Z����
					DrawManager.Draw("Nowloading_all");
					DrawManager.End();
					DrawManager.Begin(true);
					for(long i = 0;i < 100000000;i++)
						for(int j = 0;j < 8;j++);
					MakeSound ms = new MakeSound();
					ms.VoiceToWave();
					ms.MakeCheckVoice();
					for(long i = 0;i < 100000000;i++)
						for(int j = 0;j < 2;j++);
					start.Visible = true;
					back.Visible = true;
					RecordCount = 0;
					if(ms.VoiceCheck())
					{
						System.Diagnostics.Debug.WriteLine("Voice Transform is Success");
						back.Click(System.Drawing.Point.Empty);	//OneScreen�ɖ߂�
					}
					else
					{
						System.Diagnostics.Debug.WriteLine("Voice Transform is failed");
						//back.Click(System.Drawing.Point.Empty);
						parent.OneScreenMode = muphic.OneScreenMode.VoiceRegistDialogOneMore;
					}
				}
			}
		}
		public VoiceRegistDialog(OneScreen one)
		{
			parent = one;
			
//			DrawManager.BeginRegist();
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			start = new StartButton(this);
			back = new muphic.One.VoiceDialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			DrawManager.Regist(this.ToString(), 236, 251, "image\\one\\Voice\\background.png");
			DrawManager.Regist("Recording", 359, 393, new String[] {"image\\one\\Voice\\parts\\record_off.png", "image\\one\\Voice\\parts\\record_on.png",
				"image\\one\\Voice\\parts\\1.png", "image\\one\\Voice\\parts\\2.png", "image\\one\\Voice\\parts\\3.png"});
			DrawManager.Regist("RecordState1", 470, 410, "image\\one\\Voice\\parts\\off.png", "image\\one\\Voice\\parts\\on.png");
			DrawManager.Regist("RecordState2", 550, 410, "image\\one\\Voice\\parts\\off.png", "image\\one\\Voice\\parts\\on.png");
			DrawManager.Regist("RecordState3", 630, 410, "image\\one\\Voice\\parts\\off.png", "image\\one\\Voice\\parts\\on.png");
			DrawManager.Regist(start.ToString(), 462, 450, "image\\one\\Voice\\button\\start_off.png", "image\\one\\Voice\\button\\start_on.png");
			DrawManager.Regist(back.ToString(), 670, 450, "image\\one\\Voice\\button\\back_off.png", "image\\one\\Voice\\button\\back_on.png");

//			DrawManager.EndRegist();
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(start);
			BaseList.Add(back);
		}

		public override void Draw()
		{
			base.Draw ();
			if(this.isRecording)
			{
				DrawRecording();
			}
			else if(this.isCountDown)
			{
				DrawCountDown();
			}
			else
			{
				DrawNotRecording();
			}
		}

		public void DrawNotRecording()
		{
			DrawManager.Draw("Recording", 0);
			DrawManager.Draw("RecordState1");
			DrawManager.Draw("RecordState2");
			DrawManager.Draw("RecordState3");
		}

		public void DrawCountDown()
		{
			int CountDownState = 0;
			if(RecordCount < 60)
			{
				CountDownState = 4;
			}
			else if(RecordCount < 120)
			{
				CountDownState = 3;
			}
			else if(RecordCount < 180)
			{
				CountDownState = 2;
			}
			else
			{
				this.isCountDown = false;
				this.IsRecording = true;
			}
			RecordCount++;
			DrawManager.Draw("Recording", CountDownState);
			DrawManager.Draw("RecordState1");
			DrawManager.Draw("RecordState2");
			DrawManager.Draw("RecordState3");
		}

		public void DrawRecording()
		{
			DrawManager.Draw("Recording", 1);
			DrawManager.Draw("RecordState1", (RecordCount <= 2) ? 0 : 1);
			DrawManager.Draw("RecordState2", (RecordCount <= MaxRecordCount/3) ? 0 : 1);
			DrawManager.Draw("RecordState3", (RecordCount <= MaxRecordCount/3*2) ? 0 : 1);
			RecordCount--;
			if(RecordCount <= 0)						//�J�E���g��0�ɂȂ����Ȃ�
			{
				IsRecording = false;					//�^���I��
			}
		}
	}
}
