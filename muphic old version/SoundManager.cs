using System;
using System.Collections;
using System.Windows.Forms;
using Microsoft.DirectX.DirectSound;

//�}�C�N�p
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Data;

namespace muphic
{
	#region Ver.1
	/*
	/// <summary>
	/// SoundManager �̊T�v�̐����ł��B
	/// </summary>
	public class SoundManager
	{
		private static SoundManager soundManager;
		private Hashtable BufferTable;
		private Device device;

		public SoundManager(Form form)
		{
			try
			{
				device = new Device();
				device.SetCooperativeLevel(form, CooperativeLevel.Priority);
			}
			catch
			{
				MessageBox.Show("�T�E���h�f�o�C�X���쐬�ł��܂���B");
				Application.Exit();
			}
			BufferTable = new Hashtable();

			//static�����o��soundManager�Ɏ�����ݒ�
			muphic.SoundManager.soundManager = this;
		}

		public void RegistSound(String FileName)
		{
			SecondaryBuffer sb = null;
			try
			{
				sb = new SecondaryBuffer(FileName, device);
			}
			catch(Exception)
			{
				MessageBox.Show("���y�t�@�C��" + FileName + "�̃��[�h�Ɏ��s���܂���");
				Application.Exit();
			}
			BufferTable.Add(FileName, sb);
		}

		public void PlaySound(String FileName)
		{
			SecondaryBuffer sb = null;
			if(!BufferTable.ContainsKey(FileName))
			{
				this.RegistSound(FileName);
			}
			sb = (SecondaryBuffer)BufferTable[FileName];
			sb.Play(0, BufferPlayFlags.Default);
		}

		
		public void StopSound(String fname)
		{
			SecondaryBuffer sb = null;
			if(!BufferTable.ContainsKey(fname))
			{
				return;
			}
			else
			{
				sb = (SecondaryBuffer)BufferTable[fname];
				sb.Stop();
			}
		}

		public static void Play(String fname)
		{
			muphic.SoundManager.soundManager.PlaySound(fname);
		}

		public static void Stop(String fname)
		{
			muphic.SoundManager.soundManager.StopSound(fname);
		}
	}*/
	#endregion

	#region Ver.2
	/*
	/// <summary>
	/// SoundManager version2 8������2�A���ɑΉ�
	/// </summary>
	public class SoundManager
	{
		private static SoundManager soundManager;
		private Hashtable BufferTable;
		private Device device;

		public SoundManager(Form form)
		{
			try
			{
				device = new Device();
				device.SetCooperativeLevel(form, CooperativeLevel.Priority);
			}
			catch
			{
				MessageBox.Show("�T�E���h�f�o�C�X���쐬�ł��܂���B");
				Application.Exit();
			}
			BufferTable = new Hashtable();

			//static�����o��soundManager�Ɏ�����ݒ�
			muphic.SoundManager.soundManager = this;
		}

		public void RegistSound(String FileName)
		{
			SecondaryBuffer sb = null;
			try
			{
				sb = new SecondaryBuffer(FileName, device);
			}
			catch(Exception)
			{
				MessageBox.Show("���y�t�@�C��" + FileName + "�̃��[�h�Ɏ��s���܂���");
				Application.Exit();
			}
			BufferTable.Add(FileName, sb);
		}

		public void PlaySound(String FileName)
		{
			SecondaryBuffer sb = null;
			if(!BufferTable.ContainsKey(FileName))
			{
				this.RegistSound(FileName);
				sb = (SecondaryBuffer)BufferTable[FileName];
				sb.Play(0, BufferPlayFlags.Default);
			}
			else
			{
				sb = (SecondaryBuffer)BufferTable[FileName];
				sb.Stop();
				sb.SetCurrentPosition(0);
				sb.Play(0, BufferPlayFlags.Default);
			}
		}

		
		public void StopSound(String fname)
		{
			SecondaryBuffer sb = null;
			if(!BufferTable.ContainsKey(fname))
			{
				return;
			}
			else
			{
				sb = (SecondaryBuffer)BufferTable[fname];
				sb.Stop();
			}
		}

		public static void Play(String fname)
		{
			muphic.SoundManager.soundManager.PlaySound(fname);
		}

		public static void Stop(String fname)
		{
			muphic.SoundManager.soundManager.StopSound(fname);
		}
	}*/
	#endregion

	#region Ver.3.1
	/// <summary>
	/// SoundManager version2 8������2�A���ɑΉ�
	/// SoundManager version3 �}�C�N���͑Ή�
	/// SoundManager version3.1 ���C���Ȃ��̂ŁA�����ς��Ă݂�
	/// </summary>
	public class SoundManager
	{
		private static SoundManager soundManager;
		private Hashtable BufferTable;
		private Device device;

		//�}�C�N�p�ϐ�
		public BufferPositionNotify[] PositionNotify = new BufferPositionNotify[NumberRecordNotifications + 1];  
		public const int NumberRecordNotifications	= 16;
		public AutoResetEvent NotificationEvent	= null;//��
		public CaptureBuffer captureBuffer = null;				//�T�E���h �L���v�`�� �o�b�t�@�̑���Ɏg��
		public Guid CaptureDeviceGuid = Guid.Empty;				//�L���v�`���Ɏg�p����f�o�C�X��Guid�Ȃ���̂������Ă���
		public Capture capture = null;							//�T�E���h �L���v�`�� �o�b�t�@�̍쐬�Ɏg��
		private string FileName = "Voice.wav";
		public Notify notify = null;							//�Đ��o�b�t�@�܂��̓L���v�`�� �o�b�t�@�̒ʒm�C�x���g��ݒ肷��
		private Thread NotifyThread = null;//��
		private FileStream WaveFile = null;
		private BinaryWriter Writer = null;
		//		private string Path = string.Empty;
		public int CaptureBufferSize = 0;						//captureBuffer�̃T�C�Y�H
		public int NextCaptureOffset = 0;
		private bool isRecording = false;						//�^�������ǂ���
		public WaveFormat InputFormat;
		private int SampleCount = 0;							//����T���v�����O���������Ă��ƁH
		public int NotifySize = 0;								//notify�̃T�C�Y?
		private bool Capturing = false;//��


		public SoundManager(Form form)
		{
			try
			{
				device = new Device();
				device.SetCooperativeLevel(form, CooperativeLevel.Priority);
			}
			catch
			{
				MessageBox.Show("�T�E���h�f�o�C�X���쐬�ł��܂���B");
				Application.Exit();
			}
			BufferTable = new Hashtable();

			//this.InitMicrophone();

			//static�����o��soundManager�Ɏ�����ݒ�
			muphic.SoundManager.soundManager = this;
		}

		public void RegistSound(String FileName)
		{
			SecondaryBuffer sb = null;
			try
			{
				sb = new SecondaryBuffer(FileName, device);
			}
			catch(Exception)
			{
				MessageBox.Show("���y�t�@�C��" + FileName + "�̃��[�h�Ɏ��s���܂���");
				Application.Exit();
			}
			BufferTable.Add(FileName, sb);
		}

		public void DeleteSound(String FileName)
		{
			if(!BufferTable.ContainsKey(FileName))
			{
				return;
			}
			SecondaryBuffer sb = (SecondaryBuffer)BufferTable[FileName];
			sb.Dispose();
			BufferTable.Remove(FileName);
		}

		public void PlaySound(String FileName)
		{
			SecondaryBuffer sb = null;
			if(!BufferTable.ContainsKey(FileName))
			{
				this.RegistSound(FileName);
				sb = (SecondaryBuffer)BufferTable[FileName];
				sb.Play(0, BufferPlayFlags.Default);
			}
			else
			{
				sb = (SecondaryBuffer)BufferTable[FileName];
				sb.Stop();
				sb.SetCurrentPosition(0);
				sb.Play(0, BufferPlayFlags.Default);
			}
		}

		
		public void StopSound(String fname)
		{
			SecondaryBuffer sb = null;
			if(!BufferTable.ContainsKey(fname))
			{
				return;
			}
			else
			{
				sb = (SecondaryBuffer)BufferTable[fname];
				sb.Stop();
			}
		}

		public static void Delete(String fname)
		{
			muphic.SoundManager.soundManager.DeleteSound(fname);
		}

		public static void Play(String fname)
		{
			muphic.SoundManager.soundManager.PlaySound(fname);
		}

		public static void Stop(String fname)
		{
			muphic.SoundManager.soundManager.StopSound(fname);
		}

		public static void StartRecord()
		{
			//if(muphic.SoundManager.soundManager.isRecording == false)
			//{
				muphic.SoundManager.soundManager.RecordStartOrStop();
			//}
		}

		public static void StopRecord()
		{
			//if(muphic.SoundManager.soundManager.isRecording == true)
			//{
				muphic.SoundManager.soundManager.RecordStartOrStop();
			//}
		}

		///////////////////////////////////////////////////////////////////////
		//�}�C�N�p���\�b�h�Q
		///////////////////////////////////////////////////////////////////////

		public void InitMicrophone()
		{
			CaptureBufferSize = 0;						//captureBuffer�̃T�C�Y�H
			NextCaptureOffset = 0;
			SampleCount = 0;							//����T���v�����O���������Ă��ƁH
			NotifySize = 0;								//notify�̃T�C�Y?

			DecideCaptureDevice();							//�L���v�`������f�o�C�X�̌���

			InitDirectSound();								//DirectSound�̏�����
			if (null == capture)							//Capture�N���X�̃C���X�^���X���Ɏ��s������
			{
				Application.Exit();									//�ǂ����悤���Ȃ�
			}
			DecideInputFormat();							//Wave�t�@�C���t�H�[�}�b�g�̌���
			CreateCaptureBuffer();

			
			OnCreateSoundFile();
			//this.NotifyThread.Abort();
		}

		public void RecordStartOrStop()
		{
			//isRecording = !isRecording;
			StartOrStopRecord(!isRecording);		//���ݔ�^����(false)�Ȃ�A�^�����ɂ��邽��true������K�v������
			if(isRecording)
			{
				//�^���J�n
			}
			else
			{
				//�^���I��
			}
		}

		/// <summary>
		/// �L���v�`������f�o�C�X�����肷�郁�\�b�h
		/// �ʓ|�Ȃ̂ŁA�f�t�H���g�̃f�o�C�X���g�p����
		/// </summary>
		private void DecideCaptureDevice()
		{
			CaptureDevicesCollection devices = new CaptureDevicesCollection();
			this.CaptureDeviceGuid = devices[0].DriverGuid;				//�v�f0�Ȃ̂ŁA�f�t�H���g�̂��̂ɂȂ�
		}

		/// <summary>
		/// Wave�t�@�C���̃t�H�[�}�b�g�����肷�郁�\�b�h
		/// �ʓ|�Ȃ̂ŁA44.1kHz,16bit,�X�e���I�ɂ��Ă݂�
		/// </summary>
		private void DecideInputFormat()
		{
			InputFormat.FormatTag = WaveFormatTag.Pcm;
			InputFormat.SamplesPerSecond = 44100;
			InputFormat.BitsPerSample = 16;
			InputFormat.Channels = 2;
			InputFormat.BlockAlign = (short)(InputFormat.Channels * (InputFormat.BitsPerSample / 8));
			InputFormat.AverageBytesPerSecond = InputFormat.BlockAlign * InputFormat.SamplesPerSecond;
		}

		private void InitDirectSound()
		{
			CaptureBufferSize = 0;
			NotifySize = 0;

			// Create DirectSound.Capture using the preferred capture device
			try
			{
				capture = new Capture(CaptureDeviceGuid);
			}
			catch {}
		}

		/// <summary>
		/// CaptureBuffer�N���X�̃C���X�^���X�����s�����\�b�h
		/// </summary>
		void CreateCaptureBuffer()
		{
			//-----------------------------------------------------------------------------
			// Name: CreateCaptureBuffer()
			// Desc: Creates a capture buffer and sets the format 
			//-----------------------------------------------------------------------------
			CaptureBufferDescription dscheckboxd = new CaptureBufferDescription();	//�L���v�`�� �o�b�t�@���L�q����\����

			if (null != notify)
			{
				notify.Dispose();
				notify = null;
			}
			if (null != captureBuffer)
			{
				captureBuffer.Dispose();
				captureBuffer = null;
			}

			if (0 == InputFormat.Channels)								//�t�H�[�}�b�g�������Ɛݒ肳��Ă��Ȃ�
				return;

			// Set the notification size
			NotifySize = (1024 > InputFormat.AverageBytesPerSecond / 8) ? 1024 : (InputFormat.AverageBytesPerSecond / 8);
			NotifySize -= NotifySize % InputFormat.BlockAlign;   

			// Set the buffer sizes
			CaptureBufferSize = NotifySize * NumberRecordNotifications;

			// Create the capture buffer
			dscheckboxd.BufferBytes = CaptureBufferSize;
			InputFormat.FormatTag = WaveFormatTag.Pcm;
			dscheckboxd.Format = InputFormat; // Set the format during creatation
		
			captureBuffer = new CaptureBuffer(dscheckboxd, capture);
			NextCaptureOffset = 0;

			InitNotifications();
		}

		void InitNotifications()
		{
			//-----------------------------------------------------------------------------
			// Name: InitNotifications()
			// Desc: Inits the notifications on the capture buffer which are handled
			//       in the notify thread.
			//-----------------------------------------------------------------------------

			if (null == captureBuffer)
				throw new NullReferenceException();
		
			// Create a thread to monitor the notify events
			if (null == NotifyThread)
			{
				NotifyThread = new Thread(new ThreadStart(WaitThread));
				Capturing = true;
				NotifyThread.Start();

				// Create a notification event, for when the sound stops playing
				NotificationEvent = new AutoResetEvent(false);
			}


			// Setup the notification positions
			for (int i = 0; i < NumberRecordNotifications; i++)
			{
				PositionNotify[i].Offset = (NotifySize * i) + NotifySize - 1;
				PositionNotify[i].EventNotifyHandle = NotificationEvent.Handle;
			}
		
			notify = new Notify(captureBuffer);

			// Tell DirectSound when to notify the app. The notification will come in the from 
			// of signaled events that are handled in the notify thread.
			notify.SetNotificationPositions(PositionNotify, NumberRecordNotifications);
		}

		void StartOrStopRecord(bool StartRecording)
		{
			//-----------------------------------------------------------------------------
			// Name: StartOrStopRecord()
			// Desc: Starts or stops the capture buffer from Recording
			//-----------------------------------------------------------------------------

			if (StartRecording)
			{
				// Create a capture buffer, and tell the capture 
				// buffer to start recording   
				this.InitMicrophone();		////////////////////////////////////////////////////////////////////////
				CreateCaptureBuffer();
				this.isRecording = true;
				captureBuffer.Start(true);
				this.isRecording = true;
			}
			else
			{
				// Stop the buffer, and read any data that was not 
				// caught by a notification
				this.NotifyThread.Abort();		/////////////////////////////////////////////////////////////////
				this.isRecording = false;
				captureBuffer.Stop();
				this.isRecording = false;

				RecordCapturedData();
				this.isRecording = false;
			
				Writer.Seek(4, SeekOrigin.Begin); // Seek to the length descriptor of the RIFF file.
				Writer.Write((int)(SampleCount + 36));	// Write the file length, minus first 8 bytes of RIFF description.
				Writer.Seek(40, SeekOrigin.Begin); // Seek to the data length descriptor of the RIFF file.
				Writer.Write(SampleCount); // Write the length of the sample data in bytes.
			
				Writer.Close();	// Close the file now.
				WaveFile.Close();
				Writer = null;	// Set the writer to null.
				WaveFile = null; // Set the FileStream to null.
				this.isRecording = false;
			}
		}
		void CreateRIFF()
		{
			/**************************************************************************
			 
				Here is where the file will be created. A
				wave file is a RIFF file, which has chunks
				of data that describe what the file contains.
				A wave RIFF file is put together like this:
			 
				The 12 byte RIFF chunk is constructed like this:
				Bytes 0 - 3 :	'R' 'I' 'F' 'F'
				Bytes 4 - 7 :	Length of file, minus the first 8 bytes of the RIFF description.
								(4 bytes for "WAVE" + 24 bytes for format chunk length +
								8 bytes for data chunk description + actual sample data size.)
				Bytes 8 - 11:	'W' 'A' 'V' 'E'
			
				The 24 byte FORMAT chunk is constructed like this:
				Bytes 0 - 3 :	'f' 'm' 't' ' '
				Bytes 4 - 7 :	The format chunk length. This is always 16.
				Bytes 8 - 9 :	File padding. Always 1.
				Bytes 10- 11:	Number of channels. Either 1 for mono,  or 2 for stereo.
				Bytes 12- 15:	Sample rate.
				Bytes 16- 19:	Number of bytes per second.
				Bytes 20- 21:	Bytes per sample. 1 for 8 bit mono, 2 for 8 bit stereo or
								16 bit mono, 4 for 16 bit stereo.
				Bytes 22- 23:	Number of bits per sample.
			
				The DATA chunk is constructed like this:
				Bytes 0 - 3 :	'd' 'a' 't' 'a'
				Bytes 4 - 7 :	Length of data, in bytes.
				Bytes 8 -...:	Actual sample data.
			
			***************************************************************************/

			// Open up the wave file for writing.
			try
			{
				WaveFile = new FileStream(FileName, FileMode.Create, FileAccess.Write);
				Writer = new BinaryWriter(WaveFile);
			}
			catch(Exception e)
			{
				MessageBox.Show("FileOpenError! Voice.wav");
				System.Diagnostics.Debug.WriteLine(e);
			}

			// Set up file with RIFF chunk info.
			char[] ChunkRiff = {'R','I','F','F'};
			char[] ChunkType = {'W','A','V','E'};
			char[] ChunkFmt	= {'f','m','t',' '};
			char[] ChunkData = {'d','a','t','a'};
			
			short shPad = 1; // File padding
			int nFormatChunkLength = 0x10; // Format chunk length.
			int nLength = 0; // File length, minus first 8 bytes of RIFF description. This will be filled in later.
			short shBytesPerSample = 0; // Bytes per sample.

			// Figure out how many bytes there will be per sample.
			if (8 == InputFormat.BitsPerSample && 1 == InputFormat.Channels)
				shBytesPerSample = 1;
			else if ((8 == InputFormat.BitsPerSample && 2 == InputFormat.Channels) || (16 == InputFormat.BitsPerSample && 1 == InputFormat.Channels))
				shBytesPerSample = 2;
			else if (16 == InputFormat.BitsPerSample && 2 == InputFormat.Channels)
				shBytesPerSample = 4;

			// Fill in the riff info for the wave file.
			Writer.Write(ChunkRiff);
			Writer.Write(nLength);
			Writer.Write(ChunkType);

			// Fill in the format info for the wave file.
			Writer.Write(ChunkFmt);
			Writer.Write(nFormatChunkLength);
			Writer.Write(shPad);
			Writer.Write(InputFormat.Channels);
			Writer.Write(InputFormat.SamplesPerSecond);
			Writer.Write(InputFormat.AverageBytesPerSecond);
			Writer.Write(shBytesPerSample);
			Writer.Write(InputFormat.BitsPerSample);
			
			// Now fill in the data chunk.
			Writer.Write(ChunkData);
			Writer.Write((int)0);	// The sample length will be written in later.
		}

		void RecordCapturedData() 
		{
			//-----------------------------------------------------------------------------
			// Name: RecordCapturedData()
			// Desc: Copies data from the capture buffer to the output buffer 
			//-----------------------------------------------------------------------------
			byte[] CaptureData = null;
			int ReadPos;
			int CapturePos;
			int LockSize;

			captureBuffer.GetCurrentPosition(out CapturePos, out ReadPos);
			LockSize = ReadPos - NextCaptureOffset;
			if (LockSize < 0)
				LockSize += CaptureBufferSize;

			// Block align lock size so that we are always write on a boundary
			LockSize -= (LockSize % NotifySize);

			if (0 == LockSize)
				return;

			// Read the capture buffer.
			CaptureData = (byte[])captureBuffer.Read(NextCaptureOffset, typeof(byte), LockFlag.None, LockSize);

			// Write the data into the wav file
			Writer.Write(CaptureData, 0, CaptureData.Length);
		
			// Update the number of samples, in bytes, of the file so far.
			SampleCount += CaptureData.Length;

			// Move the capture offset along
			NextCaptureOffset += CaptureData.Length; 
			NextCaptureOffset %= CaptureBufferSize; // Circular buffer
		}

		private void OnCreateSoundFile()
		{
			//-----------------------------------------------------------------------------
			// Name: OnCreateSoundFile()
			// Desc: Called when the user requests to save to a sound file
			//-----------------------------------------------------------------------------

			WaveFormat wf = new WaveFormat();
			/*
						// Get the default media path (something like C:\WINDOWS\MEDIA)
						if (string.Empty == Path)
							Path = Environment.SystemDirectory.Substring(0, Environment.SystemDirectory.LastIndexOf("\\")) + "\\media";
			*/
			if (isRecording)
			{
				// Stop the capture and read any data that 
				// was not caught by a notification
				StartOrStopRecord(false);
				isRecording = false;
			}
			//FileName = ofd.FileName;		

			try
			{
				CreateRIFF();
			}
			catch
			{
				MessageBox.Show("Could not create wave file.");
				return;
			}

			// Remember the path for next time
			//Path = FileName.Substring(0, FileName.LastIndexOf("\\"));
		}

		private void WaitThread()
		{
			while(Capturing)
			{
				//Sit here and wait for a message to arrive
				try
				{
					NotificationEvent.WaitOne(Timeout.Infinite, true);
					RecordCapturedData();
				}
				catch(NullReferenceException e)
				{
					//MessageBox.Show("�ʂ��ӂ���񂷂��������Ղ����");
					System.Diagnostics.Debug.Write("�ʂ��ӂ���񂷂��������Ղ����");
				}
			}
		}
	}
	#endregion
}
