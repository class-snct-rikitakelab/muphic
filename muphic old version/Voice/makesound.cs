using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using muphic.Change;

namespace muphic.tag
{
	/// <summary>
	/// tag の概要の説明です。
	/// </summary>
	public class MakeSound
	{
		public MakeSound()
		{
		}

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		public void Voice2Wave()
		{
			#region ver.FFT 1.0
//			ReadMediaFile rmf = new ReadMediaFile("Voice.wav");
//			ReadWaveFile_Riff rwf = new ReadWaveFile_Riff("Voice.wav");
//			Wave_Riff wr = rwf.ReadWave();
//			int d = 0;
//			for (int i = 0; i < wr.chunk.Length; i++)
//			{
//				if (wr.chunk[i].aFormat.Equals("data"))
//				{
//					d = i;
//				}
//			}
//
//			int n = 0;
//
//			VoiceChange vc = new VoiceChange();
//			double[] i_data;
//
//			vc.ByteToDouble(((data)(wr.chunk[d])).WaveData[0], out i_data, ((data)(wr.chunk[d])).bByte, out n);
//			double[] i_dataIm = new double[n];
//			double[] data_real = new double[n];
//			double[] data_imagin = new double[n];
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = 0;
//			}
//
//			int n2 = vc.CountStage(n);
//
//			vc.FFTfunc(i_data, i_dataIm, n2, 1);
//
//			fmt f_out = new fmt();
//			f_out.aFormat = "fmt ";
//			f_out.bByte = 16;
//			f_out.cFormatID = 1;			//フォーマットID											 1→01 00
//			f_out.dChannel = 2;				//チャンネル数(モノラル・ステレオ)					ステレオ 2→02 00
//			f_out.eSamplingRate = 44100;	//サンプリングレート								   44100Hz→44 AC 00 00
//			f_out.fDataSpeed = 44100 * 2 * 2;	//データ速度(Byte/Sec) 44.1kHz,16bitステレオなら  44100*2*2=176400→10 B1 02 00 (0x2B110)
//			f_out.gBlockSize = 2 * 2;			//ブロックサイズ(Byte/Sample*チャンネル数)16bitステレオなら  2*2=4→04 00
//			f_out.hBitperSample = 16;		//サンプル当りのビット数(bit/Sample)Waveでは8か16			16→10 00
//			f_out.iExtensionSize = 0;		//拡張部分のサイズ(FormatID = 1なら存在しない)
//			f_out.Extention = null;			//拡張部分
//
//			data d_out;
//			Wave_Riff w;
//			WriteWaveFile_Riff wwf;
//
//			int size;
//			rmf.CloseMediaFile();
//			rwf.CloseWaveFile();
//
//			#region create CL
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.CL);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice8.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
//
//			#region create D
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.D);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice7.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
//
//			#region create E
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.E);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			//size = 0;
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice6.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
//
//			#region create F
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.F);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			//size = 0;
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice5.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
//
//			#region create G
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.G);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice4.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
//
//			#region create A
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.A);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice3.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
//
//			#region create B
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.B);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice2.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
//
//			#region create CH
//			for (int i = 0; i < n; i++)
//			{
//				data_real[i] = i_data[i];
//				data_imagin[i] = i_dataIm[i];
//			}
//
//			vc.TransTo(data_real, data_imagin, n2, Change.Scale.CH);
//
//			vc.FFTfunc(data_real, data_imagin, n2, -1);
//
//			vc.Normalize(data_real, n2);
//
//			d_out = new data();
//			d_out.aFormat = "data";
//			d_out.bByte = n2 * 2 * 2;
//			d_out.WaveData = new byte[f_out.dChannel][];
//			for (int i = 0; i < f_out.dChannel; i++)
//			{
//				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
//			}
//
//			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);
//
//			for (int i = 0; i < n2*2; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice1.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			#endregion
			#endregion

			#region ver.FFT 1.5
			ReadWaveFile_Riff rwf = new ReadWaveFile_Riff("Voice.wav");
			Wave_Riff wr = rwf.ReadWave();
			int d = 0;
			int Peak = 0;
			for (int i = 0; i < wr.chunk.Length; i++)
			{
				if (wr.chunk[i].aFormat.Equals("data"))
				{
					d = i;
				}
			}

			int n = 0;

			VoiceChange vc = new VoiceChange();
			double[] i_data;

			vc.ByteToDouble(((data)(wr.chunk[d])).WaveData[0], out i_data, ((data)(wr.chunk[d])).bByte, out n);
			double[] i_dataIm = new double[n];
			double[] data_real = new double[n];
			double[] data_imagin = new double[n];
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = 0;
			}

			int n2 = vc.CountStage(n);
			
			Coordinate(i_data, i_dataIm, n, n2);

			vc.FFTfunc(i_data, i_dataIm, n2, 1);

			Peak = vc.PeakCheck(i_data,i_dataIm);

			fmt f_out = new fmt();
			f_out.aFormat = "fmt ";
			f_out.bByte = 16;
			f_out.cFormatID = 1;			//フォーマットID											 1→01 00
			f_out.dChannel = 2;				//チャンネル数(モノラル・ステレオ)					ステレオ 2→02 00
			f_out.eSamplingRate = 44100;	//サンプリングレート								   44100Hz→44 AC 00 00
			f_out.fDataSpeed = 44100 * 2 * 2;	//データ速度(Byte/Sec) 44.1kHz,16bitステレオなら  44100*2*2=176400→10 B1 02 00 (0x2B110)
			f_out.gBlockSize = 2 * 2;			//ブロックサイズ(Byte/Sample*チャンネル数)16bitステレオなら  2*2=4→04 00
			f_out.hBitperSample = 16;		//サンプル当りのビット数(bit/Sample)Waveでは8か16			16→10 00
			f_out.iExtensionSize = 0;		//拡張部分のサイズ(FormatID = 1なら存在しない)
			f_out.Extention = null;			//拡張部分

			data d_out;
			Wave_Riff w;
			WriteWaveFile_Riff wwf;

			int size;
			rwf.CloseWaveFile();

			#region create CL
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}

			vc.TransTo(data_real, data_imagin, n2, Change.Scale.CL,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice8.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion

			#region create D
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}

			vc.TransTo(data_real, data_imagin, n2, Change.Scale.D,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice7.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion

			#region create E
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}
			vc.TransTo(data_real, data_imagin, n2, Change.Scale.E,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			//size = 0;
			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice6.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion

			#region create F
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}

			vc.TransTo(data_real, data_imagin, n2, Change.Scale.F,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			//size = 0;
			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice5.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion

			#region create G
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}

			vc.TransTo(data_real, data_imagin, n2, Change.Scale.G,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice4.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion

			#region create A
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}

			vc.TransTo(data_real, data_imagin, n2, Change.Scale.A,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice3.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion

			#region create B
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}

			vc.TransTo(data_real, data_imagin, n2, Change.Scale.B,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice2.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion

			#region create CH
			for (int i = 0; i < n; i++)
			{
				data_real[i] = i_data[i];
				data_imagin[i] = i_dataIm[i];
			}

			vc.TransTo(data_real, data_imagin, n2, Change.Scale.CH,Peak);

			vc.FFTfunc(data_real, data_imagin, n2, -1);

			vc.Normalize(data_real, n2);

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = n2 * 2 * 2;
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			vc.DoubleToByte(data_real, out d_out.WaveData[0], n2, out size);

			for (int i = 0; i < n2*2; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice1.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			#endregion
			#endregion
			for(int i = 1;i <= 8;i++)
			{
				Coordinate(i.ToString());
			}
			//MessageBox.Show("Create Finished");
		}

		public void VoiceToWave()
		{
			#region ver.STFT 1.0
//			ReadMediaFile rmf = new ReadMediaFile("Voice.wav");
//			ReadWaveFile_Riff rwf = new ReadWaveFile_Riff("Voice.wav");
//			Wave_Riff wr = rwf.ReadWave();
//			int d = 0;
//			for (int i = 0; i < wr.chunk.Length; i++)
//			{
//				if (wr.chunk[i].aFormat.Equals("data"))
//				{
//					d = i;
//				}
//			}
//
//			VoiceChange vc = new VoiceChange();
//
//			fmt f_out = new fmt();
//			f_out.aFormat = "fmt ";
//			f_out.bByte = 16;
//			f_out.cFormatID = 1;			//フォーマットID											 1→01 00
//			f_out.dChannel = 2;				//チャンネル数(モノラル・ステレオ)					ステレオ 2→02 00
//			f_out.eSamplingRate = 44100;	//サンプリングレート								   44100Hz→44 AC 00 00
//			f_out.fDataSpeed = 44100 * 2 * 2;	//データ速度(Byte/Sec) 44.1kHz,16bitステレオなら  44100*2*2=176400→10 B1 02 00 (0x2B110)
//			f_out.gBlockSize = 2 * 2;			//ブロックサイズ(Byte/Sample*チャンネル数)16bitステレオなら  2*2=4→04 00
//			f_out.hBitperSample = 16;		//サンプル当りのビット数(bit/Sample)Waveでは8か16			16→10 00
//			f_out.iExtensionSize = 0;		//拡張部分のサイズ(FormatID = 1なら存在しない)
//			f_out.Extention = null;			//拡張部分
//
//			data d_out = (data)(wr.chunk[d]);
//
//			rmf.CloseMediaFile();
//			rwf.CloseWaveFile();
//			Wave_Riff w;
//			WriteWaveFile_Riff wwf;
//			Byte[] i_data = new Byte[((data)(wr.chunk[d])).bByte / f_out.dChannel];
//			for(int i = 0;i < ((data)(wr.chunk[d])).WaveData[0].Length;i++)
//			{
//				i_data[i] = ((data)(wr.chunk[d])).WaveData[0][i];
//			}
//
//			#region create CH
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.CH);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice1.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE CH END");
//			#endregion
//
//			#region create B
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.B);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice2.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE B END");
//			#endregion
//
//			#region create A
//
//			vc.STFTfunc(i_data,out d_out.WaveData[0], d_out.bByte, Change.Scale.A);
//
//			for (int i = 0; i < d_out.bByte/f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice3.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE A END");
//			#endregion
//
//			#region create G
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.G);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice4.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE G END");
//			#endregion
//
//			#region create F
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.F);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice5.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE F END");
//			#endregion
//
//			#region create E
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.E);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice6.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE E END");
//			#endregion
//
//			#region create D
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.D);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice7.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE D END");
//			#endregion
//
//			#region create CL
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.CL);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("Voice8.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE CL END");
//			#endregion
//			
			#endregion

			#region ver.STFT 1.5
			ReadWaveFile_Riff rwf = new ReadWaveFile_Riff("Voice.wav");
			Wave_Riff wr = rwf.ReadWave();
			int d = 0;
			for (int i = 0; i < wr.chunk.Length; i++)
			{
				if (wr.chunk[i].aFormat.Equals("data"))
				{
					d = i;
				}
			}

			VoiceChange vc = new VoiceChange();

			fmt f_out = new fmt();
			f_out.aFormat = "fmt ";
			f_out.bByte = 16;
			f_out.cFormatID = 1;			//フォーマットID											 1→01 00
			f_out.dChannel = 2;				//チャンネル数(モノラル・ステレオ)					ステレオ 2→02 00
			f_out.eSamplingRate = 44100;	//サンプリングレート								   44100Hz→44 AC 00 00
			f_out.fDataSpeed = 44100 * 2 * 2;	//データ速度(Byte/Sec) 44.1kHz,16bitステレオなら  44100*2*2=176400→10 B1 02 00 (0x2B110)
			f_out.gBlockSize = 2 * 2;			//ブロックサイズ(Byte/Sample*チャンネル数)16bitステレオなら  2*2=4→04 00
			f_out.hBitperSample = 16;		//サンプル当りのビット数(bit/Sample)Waveでは8か16			16→10 00
			f_out.iExtensionSize = 0;		//拡張部分のサイズ(FormatID = 1なら存在しない)
			f_out.Extention = null;			//拡張部分

			data d_out = (data)(wr.chunk[d]);

			rwf.CloseWaveFile();
			Wave_Riff w;
			WriteWaveFile_Riff wwf;
			Byte[] i_data = new Byte[((data)(wr.chunk[d])).bByte / f_out.dChannel];
			for(int i = 0;i < ((data)(wr.chunk[d])).WaveData[0].Length;i++)
			{
				i_data[i] = ((data)(wr.chunk[d])).WaveData[0][i];
			}

			#region create CH

			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.CH);

			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice1.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE CH END");
			#endregion

			#region create B

			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.B);

			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice2.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE B END");
			#endregion

			#region create A

			vc.STFTfunc(i_data,out d_out.WaveData[0], d_out.bByte, Change.Scale.A);

			for (int i = 0; i < d_out.bByte/f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice3.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE A END");
			#endregion

			#region create G

			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.G);

			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice4.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE G END");
			#endregion

			#region create F

			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.F);

			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice5.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE F END");
			#endregion

			#region create E

			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.E);

			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice6.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE E END");
			#endregion

			#region create D

			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.D);

			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice7.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE D END");
			#endregion

			#region create CL

			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.CL);

			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
			{
				d_out.WaveData[1][i] = d_out.WaveData[0][i];
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff("Voice8.wav", w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
			System.Diagnostics.Debug.WriteLine("CREATE CL END");
			#endregion
			
			#endregion

			for(long wait = 0;wait < 100000;wait++)
			{
			}
			for(int i = 1;i <= 8;i++)
			{
				Coordinate(i.ToString());
			}
			//MessageBox.Show("Create Finished");
		}

		public void MakeCheckVoice()
		{
//			#region MakeCheckVoice
//			ReadWaveFile_Riff rwf = new ReadWaveFile_Riff("Voice.wav");
//			Wave_Riff wr = rwf.ReadWave();
//			int d = 0;
//			for (int i = 0; i < wr.chunk.Length; i++)
//			{
//				if (wr.chunk[i].aFormat.Equals("data"))
//				{
//					d = i;
//				}
//			}
//
//			VoiceChange vc = new VoiceChange();
//
//			fmt f_out = new fmt();
//			f_out.aFormat = "fmt ";
//			f_out.bByte = 16;
//			f_out.cFormatID = 1;			//フォーマットID											 1→01 00
//			f_out.dChannel = 2;				//チャンネル数(モノラル・ステレオ)					ステレオ 2→02 00
//			f_out.eSamplingRate = 44100;	//サンプリングレート								   44100Hz→44 AC 00 00
//			f_out.fDataSpeed = 44100 * 2 * 2;	//データ速度(Byte/Sec) 44.1kHz,16bitステレオなら  44100*2*2=176400→10 B1 02 00 (0x2B110)
//			f_out.gBlockSize = 2 * 2;			//ブロックサイズ(Byte/Sample*チャンネル数)16bitステレオなら  2*2=4→04 00
//			f_out.hBitperSample = 16;		//サンプル当りのビット数(bit/Sample)Waveでは8か16			16→10 00
//			f_out.iExtensionSize = 0;		//拡張部分のサイズ(FormatID = 1なら存在しない)
//			f_out.Extention = null;			//拡張部分
//
//			data d_out = (data)(wr.chunk[d]);
//
//			rwf.CloseWaveFile();
//			Wave_Riff w;
//			WriteWaveFile_Riff wwf;
//			Byte[] i_data = new Byte[((data)(wr.chunk[d])).bByte / f_out.dChannel];
//			for(int i = 0;i < ((data)(wr.chunk[d])).WaveData[0].Length;i++)
//			{
//				i_data[i] = ((data)(wr.chunk[d])).WaveData[0][i];
//			}
//
//			#region create CheckVoice
//
//			vc.STFTfunc(i_data, out d_out.WaveData[0], d_out.bByte, Change.Scale.CH);
//
//			for (int i = 0; i < d_out.bByte / f_out.dChannel; i++)
//			{
//				d_out.WaveData[1][i] = d_out.WaveData[0][i];
//			}
//
//			w = MakeWave(f_out, d_out);
//			wwf = new WriteWaveFile_Riff("VoiceCheck.wav", w);
//			wwf.WriteWave();
//			wwf.CloseWaveFile();
//			System.Diagnostics.Debug.WriteLine("CREATE CheckVoice END");
//			#endregion
//
//			for(long wait = 0;wait < 100000;wait++)
//			{
//			}
//			Coordinate("Check");
//
//			#endregion
		}

		public bool VoiceCheck()
		{
//			int count = 0;
//			string filename = "VoiceCheck.wav";
//			ReadWaveFile_Riff rwf = new ReadWaveFile_Riff(filename);
//			Wave_Riff wr = rwf.ReadWave();
//
//			int d = 0;
//			int f = 0;
//			for (int i = 0; i < wr.chunk.Length; i++)
//			{
//				if (wr.chunk[i].aFormat.Equals("fmt"))
//				{
//					f = i;
//				}
//				else if (wr.chunk[i].aFormat.Equals("data"))
//				{
//					d = i;
//				}
//			}
//
//			rwf.CloseWaveFile();
//
//		
//			string filename2 = "Voice1.wav";
//			ReadWaveFile_Riff rwf2 = new ReadWaveFile_Riff(filename2);
//			Wave_Riff wr2 = rwf2.ReadWave();
//
//			int d2 = 0;
//			int f2 = 0;
//			for (int i = 0; i < wr2.chunk.Length; i++)
//			{
//				if (wr2.chunk[i].aFormat.Equals("fmt"))
//				{
//					f2 = i;
//				}
//				else if (wr2.chunk[i].aFormat.Equals("data"))
//				{
//					d2 = i;
//				}
//			}
//
//			count = 0;
//			for(int i = 0;i < ((data)(wr.chunk[d])).bByte/((fmt)(wr.chunk[f])).dChannel;i++)
//			{
//				for(int j = 0;j < ((fmt)(wr.chunk[f])).dChannel;j++)
//				{
//					if((((data)wr.chunk[d]).WaveData[j][i] >= ((data)wr2.chunk[d2]).WaveData[j][i])
//						&&(((data)wr2.chunk[d]).WaveData[j][i] >= ((data)wr.chunk[d2]).WaveData[j][i]*0.7))
//					{
//						count++;
//					}
//				}
//			}
//			rwf2.CloseWaveFile();
//			if(count > ((data)(wr.chunk[d])).bByte *0.8)
//			{
				return true;
//			}
//			
//			return false;
		}

		public void Coordinate(String str)
		{
			string filename = "Voice"+str+".wav";
			ReadWaveFile_Riff rwf = new ReadWaveFile_Riff(filename);
			Wave_Riff wr = rwf.ReadWave();

			int d = 0;
			int f = 0;
			for (int i = 0; i < wr.chunk.Length; i++)
			{
				if (wr.chunk[i].aFormat.Equals("fmt"))
				{
					f = i;
				}
				else if (wr.chunk[i].aFormat.Equals("data"))
				{
					d = i;
				}
			}

			fmt f_out = (fmt)(wr.chunk[f]);

			data d_out = new data();
			Wave_Riff w;
			WriteWaveFile_Riff wwf;

			rwf.CloseWaveFile();

			d_out = new data();
			d_out.aFormat = "data";
			d_out.bByte = (int)(((data)wr.chunk[d]).bByte * 0.9);
			d_out.WaveData = new byte[f_out.dChannel][];
			for (int i = 0; i < f_out.dChannel; i++)
			{
				d_out.WaveData[i] = new byte[d_out.bByte / f_out.dChannel];
			}

			int spread = ((data)(wr.chunk[d])).bByte/20;
			for(int i = 0;i < d_out.bByte/f_out.dChannel;i++)
			{
				for(int j = 0;j < f_out.dChannel;j++)
				{
					d_out.WaveData[j][i] = ((data)wr.chunk[d]).WaveData[j][i+spread];
				}
			}

			w = MakeWave(f_out, d_out);
			wwf = new WriteWaveFile_Riff(filename, w);
			wwf.WriteWave();
			wwf.CloseWaveFile();
		}

		public void Coordinate(double[] dr,double[] di,int n,int n2)
		{
			for(int i = 0;i < n2;i++)
			{
				dr[i] = dr[i+(int)(n/2 -n2/2)];
				di[i] = di[i+(int)(n/2 -n2/2)];
			}
		}

		
		private fmt MakeFmt()
		{
			fmt f = new fmt();
			f.aFormat = "fmt ";
			f.bByte = 16;
			f.cFormatID = 1;			//フォーマットID											 1→01 00
			f.dChannel = 2;				//チャンネル数(モノラル・ステレオ)					ステレオ 2→02 00
			f.eSamplingRate = 44100;	//サンプリングレート								   44100Hz→44 AC 00 00
			f.fDataSpeed = 44100*2*2;	//データ速度(Byte/Sec) 44.1kHz,16bitステレオなら  44100*2*2=176400→10 B1 02 00 (0x2B110)
			f.gBlockSize = 2*2;			//ブロックサイズ(Byte/Sample*チャンネル数)16bitステレオなら  2*2=4→04 00
			f.hBitperSample = 16;		//サンプル当りのビット数(bit/Sample)Waveでは8か16			16→10 00
			f.iExtensionSize = 0;		//拡張部分のサイズ(FormatID = 1なら存在しない)
			f.Extention = null;			//拡張部分

			return f;
		}

		private data MakeData(fmt f)
		{
			data d = new data();
			d.aFormat = "data";
			d.bByte = f.fDataSpeed*3;	//3秒間だから3
			d.WaveData = new byte[f.dChannel][];
			for(int i=0;i<f.dChannel;i++)
			{
				d.WaveData[i] = new byte[d.bByte/f.dChannel];
			}
			//データ部分(面倒なのでステレオと仮定して作ってる)
			//1/44100 = 0.0000226 1/440 = 0.0022727
			//0.00227 / 0.0000226 = 100.56194
			//ということで、44100Hzのなかでラ(440Hz)を出すには、約100サンプルで1周期にすればよい
			for(int i=0;i<1;i++)
			{
				for(int j=0;j<d.WaveData[i].Length/2;j++)
				{
					double rad = (double)(j % 100) / 100 * 2 * System.Math.PI;
					double ans = System.Math.Sin(rad);	//これで、1～-1の範囲16bitなので、-32768 ～ +32767に直す
					ans *= 32768;
					int upper = (((int)ans) & 0xFF00) >> 8;
					int lower = ((int)ans) & 0xFF;
					d.WaveData[0][j*2] = (byte)upper;
					d.WaveData[0][j*2+1] = (byte)lower;
					d.WaveData[1][j*2] = 0;//(byte)upper;
					d.WaveData[1][j*2+1] = 0;//(byte)lower;
				}
			}
			return d;
		}

		private Wave_Riff MakeWave(fmt f, data d)
		{
			Wave_Riff w = new Wave_Riff();
			w.aIdentity = "RIFF";
			w.bFileSize = 4 + 8 + f.bByte + 8 + d.bByte;
			w.cRiffKind = "WAVE";
			w.chunk = new Chunk[2];
			w.chunk[0] = f;
			w.chunk[1] = d;
			return w;
		}
	}
}

