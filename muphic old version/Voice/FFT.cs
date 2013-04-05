using System;
using System.IO;
using System.Text;

namespace muphic.Change
{
	/// <summary>
	/// コード
	/// L : 低音,H : 高音
	/// s : #
	/// </summary>
	public enum Scale : int
	{
		CL		=	262,
		Cs		=	277,
		D		=	294,
		Ds		=	311,
		E		=	330,
		F		=	349,
		Fs		=	370,
		G		=	392,
		Gs		=	415,
		A		=	440,
		As		=	466,
		B		=	494,
		CH		=	523
	}

	class VoiceChange
	{
		//データ数最大値
		const int N_MAX = 32768;
		//まぁ円周率ですね
		const double pai = Math.PI;
		//FFTかIFFTかのフラグ用ですから
		const int FFT = 1;
		const int IFFT = -1;

		public VoiceChange()
		{
		}

		public void ReadData(string filename, double[] data,ref int n)
		{
			FileInfo source;
			StreamReader sr;
			
			source = new FileInfo(filename);
			sr = source.OpenText();
			
			string text;
			string[] s;
			int i = 0;
			char[] sepa = {' '};

			text = sr.ReadLine();
			while(text != null)
			{
				if ((s = text.Split(sepa)) != null)
				{
					data[i] = double.Parse(s[1]);
					i++;
				}
				text = sr.ReadLine();
			}
			n = i;

			sr.Close();
		}

		public void WriteData(string filename,double[] data,int n)
		{
			StreamWriter result = new StreamWriter(filename, false);
			for (int i = 0; i < n; i++)
			{
				result.WriteLine("{0} {1}", i, (float)data[i]);
			}
			result.Close();
		}

		public int CountStage(int n)
		{
			int stage = 0;
			int l = n;
			int n2 = 0;

			while ((int)Math.Pow(2, stage + 1) < n)
			{
				l = (int)l / 2;
				stage++;
			}
			n2 = (int)Math.Pow(2, stage);

			return n2;
		}
		
		/// <summary>
		/// 高速フーリエ
		/// </summary>
		/// <param name="dr">実数部データ</param>
		/// <param name="di">虚数部データ</param>
		/// <param name="n">データ数</param>
		/// <param name="flag">フラグ:FFT or 1:IFFT or -1</param>
		/// <returns></returns>
		public void FFTfunc(double[] dr, double[] di,int n, int flag)
		{
			int i, j, k;      /* 作業用 */
			double tmp;
			int stage, l, l2;
			double C, S, Wr, Wi;
			double D;
			double[] rp,ip;
			double[] rp2, ip2;
			
			rp = new double[n];
			ip = new double[n];

			rp2 = new double[n];
			ip2 = new double[n];

			if (n % 2 != 0)
			{
				System.Diagnostics.Debug.WriteLine("Error : FFT : number is not even");
				Console.WriteLine("Error : FFT : number is not even");
			}

			l = n;
			stage = 0;
			if(l%2 == 1)
			{
				System.Diagnostics.Debug.WriteLine("Error : FFT : illegal number n");
				Console.WriteLine("Error : FFT : illegal number n");
			}
			while(l != 1)
			{
				l = (int)l/2;
				stage++;
			}
			
			//データの入れ替え
			for(i = 0;i < n; i++)
			{
				rp[i] = dr[i];
				rp2[i] = 0;
				ip[i] = di[i];
				ip2[i] = 0;
			}

			j = 0;l = n;
			for(i = 0;i < n-1;i++)
			{
				if(i < j)
				{
					tmp = rp[i]; rp[i] = rp[j]; rp[j] = tmp;
					tmp = ip[i]; ip[i] = ip[j]; ip[j] = tmp;
				}
				l2 = (int)l/2;
				while(j >= l2)
				{
					j -= l2;
					l2 = (int)l2/2;
				}
				j += l2;
			}

			//階数
			for(j = 1;j <= stage;j++)
			{
				l = (int)Math.Pow(2,j);
				l2 = (int)(l/2);
				D = -flag*pai/(double)l2;
				//
				for(k = 0;k < l2;k++)
				{
					C = Math.Cos(D*k);
					S = Math.Sin(D*k);
					for(i = k;i < n;i += l)
					{
						Wr = C*rp[i +l2] - S*ip[i +l2];
						Wi = S*rp[i +l2] + C*ip[i +l2];

						rp2[i] = rp[i] +Wr;
						ip2[i] = ip[i] +Wi;

						rp2[i +l2] = rp[i] -Wr;
						ip2[i +l2] = ip[i] -Wi;
					}
				}

				for (k = 0; k < n; k++)
				{
					rp[k] = rp2[k];
					ip[k] = ip2[k];
				}
			}
			if(flag == FFT)
			{
				for(i = 0;i < n; i++)
				{
					rp[i] /= (double)n;
					ip[i] /= (double)n;
				}
			}

			for (i = 0; i < n; i++)
			{
				dr[i] = rp[i];
				di[i] = ip[i];
			}
		}

		/// <summary>
		/// 短時間フーリエ
		/// </summary>
		/// <param name="dr">実数部</param>
		/// <param name="di">虚数部</param>
		/// <param name="n">データ数</param>
		/// <param name="s">設定音階</param>
		public void STFTfunc(Byte[] i_data,out Byte[] o_data, int size, Scale s)
		{
			int count = 0, count2 = 0, start = 0;
			int i, j, k;
			int Peak;

			//データ数
			int n;
			//データ本体
			double[] dr;
			double[] di;

			this.ByteToDouble(i_data, out dr, size, out n);
			//窓幅
			int width = this.CountStage(n);
			//重複幅
			int dup = 0;

			di = new double[n];
			//出力用
			double[] odr = new double[n];
			double[] odi = new double[n];
			//短時間用
			double[] sdr = new double[width];
			double[] sdi = new double[width];
			for (i = 0; i < n; i++)
			{
				di[i] = 0;
				odr[i] = 0;
				odi[i] = 0;
			}


			count = (int)(n / width);

			for (i = 0; i < 5; i++)
			{
				dup = (int)(Math.Ceiling((double)((count + 1 + i) * width - n) / (count + i)));
				if ((dup > width / 5) && (dup < width / 3))
					break;
			}
			count2 = count + i;

			start = 0 * (width - dup);

			for (j = 0; j < width; j++)
			{
				sdr[j] = dr[j + start];
				sdi[j] = di[j + start];
			}
			this.Windowfunc(sdr, sdi, width, width);
			this.FFTfunc(sdr, sdi, width, FFT);
			Peak = this.PeakCheck(sdr,sdi);
			this.TransTo(sdr, sdi, width, s, Peak);
			this.FFTfunc(sdr, sdi, width, IFFT);
			for (j = 0; j < width; j++)
			{
				odr[j + start] += sdr[j];
				odi[j + start] += sdi[j];
			}

			for (i = 1; i < count2; i++)
			{
				start = i * (width - dup);

				for (j = 0; j < width; j++)
				{
					sdr[j] = dr[j + start];
					sdi[j] = di[j + start];
				}
				this.Windowfunc(sdr, sdi, width, width);
				this.FFTfunc(sdr, sdi, width, FFT);
				this.TransTo(sdr, sdi, width, s, Peak);
				this.FFTfunc(sdr, sdi, width, IFFT);
				for (j = 0; j < width; j++)
				{
					odr[j + start] += sdr[j];
					odi[j + start] += sdi[j];
				}

			}
			start = n - width;
			for (j = 0; j < width; j++)
			{
				sdr[j] = dr[j + start];
				sdi[j] = di[j + start];
			}
			this.Windowfunc(sdr, sdi, width, width);
			this.FFTfunc(sdr, sdi, width, FFT);
			this.TransTo(sdr, sdi, width, s, Peak);
			this.FFTfunc(sdr, sdi, width, IFFT);
			for (j = 0; j < width; j++)
			{
				odr[j + start] += sdr[j];
				odi[j + start] += sdi[j];
			}

			this.Normalize(odr, n);

			byte[] result = new byte[size];
			this.DoubleToByte(odr,out result, n,out size);
			o_data = result;
		}


		/// <summary>
		/// STFT用？ピークチェック
		/// 時系列データ数は6800以上でお願いします
		/// </summary>
		/// <param name="dr">実数部データ</param>
		/// <param name="di">虚数部データ</param>
		/// <returns>ピーク</returns>
		public int PeakCheck(double[] dr,double[] di)
		{
			int Peak = 0;
			double PeakTotal = 0;
			double Total = 0;
			int i,j;

			for(i = 300;i < 3400;i += 10)
			{
				Total = 0;
				for(j = 0;j < 10;j++)
				{
					Total += Math.Abs(dr[i+j]);
					Total += Math.Abs(di[i+j]);
				}
				if(PeakTotal < Total)
				{
					Peak = i;
					PeakTotal = Total;
				}
			}

			System.Diagnostics.Debug.WriteLine("Peak:"+Peak);
			return Peak;
		}

		/// <summary>
		/// 周波数領域で変位させる
		/// </summary>
		/// <param name="dr">real part data : 実数部データ</param>
		/// <param name="di">imaginary part data : 虚数部データ</param>
		/// <param name="n">number : データ数</param>
		/// <param name="scale">
		/// コード固有の周波数ですね
		/// CL		262
		/// C#/Db	277
		/// D		294
		/// D#/Eb	311
		/// E		330
		/// F		349
		/// F#/Gb	370
		/// G		392
		/// G#/Ab	415
		/// A		440
		/// A#/Bb	466
		/// B		494
		/// CH		523
		/// </param>
		public void TransTo(double[] dr,double[] di,int n, Scale s)
		{
			int i;
			int dist = (int)s;
			int PeakRe = 0,PeakIm = 0,Peak = 0;
			double[] rp,ip;

			rp = new double[n];
			ip = new double[n];

			for (i = 0; i < n; i++)
			{
				rp[i] = dr[i];
				ip[i] = di[i];
			}

			//安易なスペクトルピークの算出
			for(i = 1;i < (int)n/2;i++)
			{
				double a = (double)1 / n;

				if ((Math.Abs(rp[PeakRe]) < Math.Abs(rp[i])) && (Math.Abs(rp[i] - rp[PeakRe]) > (double)1/n))
				{
					PeakRe = i;
				}
				if((Math.Abs(ip[PeakIm]) < Math.Abs(ip[i]))&& (Math.Abs(ip[i] - ip[PeakIm]) > (double)1/n))
				{
					PeakIm = i;
				}

				if((PeakRe != 0)&&(PeakIm != 0))
					Peak = (int)(PeakRe+PeakIm)/2;
				else if((PeakRe != 0)&&(PeakIm == 0))
					Peak = PeakRe;
				else if((PeakRe == 0)&&(PeakIm != 0))
					Peak = PeakIm;
			}

			if(Peak > dist)
			{
				for(i = 0;i < dist+(int)n/2-Peak;i++)
				{
					dr[i] = rp[Peak-dist+i];
					di[i] = ip[Peak-dist+i];
				}
				for(i = dist+(int)n/2-Peak;i < (int)n/2;i++)
				{
					dr[i] = 0;
					di[i] = 0;
				}
				for(i = 1;i < (int)n/2;i++)
				{
					dr[n-i] = dr[i];
					di[n-i] = -di[i];
				}
			}
			else if(Peak < dist)
			{
				for(i = 0;i < Peak+(int)n/2-dist;i++)
				{
					dr[dist-Peak+i] = rp[i];
					di[dist-Peak+i] = ip[i];
				}
				for(i = 0;i < dist-Peak;i++)
				{
					dr[i] = 0;
					di[i] = 0;
				}
				for(i = 1;i < (int)n/2;i++)
				{
					dr[n-i] = dr[i];
					di[n-i] = -di[i];
				}
			}
		}
		public void TransTo(double[] dr,double[] di,int n, Scale s,int Peak)
		{
			int i;
			int dist = (int)s;
			double[] rp,ip;

			rp = new double[n];
			ip = new double[n];

			for (i = 0; i < n; i++)
			{
				rp[i] = dr[i];
				ip[i] = di[i];
			}

			if(Peak > dist)
			{
				for(i = 0;i < dist+(int)n/2-Peak;i++)
				{
					dr[i] = rp[Peak-dist+i];
					di[i] = ip[Peak-dist+i];
				}
				for(i = dist+(int)n/2-Peak;i < (int)n/2;i++)
				{
					dr[i] = 0;
					di[i] = 0;
				}
				for(i = 1;i < (int)n/2;i++)
				{
					dr[n-i] = dr[i];
					di[n-i] = -di[i];
				}
			}
			else if(Peak < dist)
			{
				for(i = 0;i < Peak+(int)n/2-dist;i++)
				{
					dr[dist-Peak+i] = rp[i];
					di[dist-Peak+i] = ip[i];
				}
				for(i = 0;i < dist-Peak;i++)
				{
					dr[i] = 0;
					di[i] = 0;
				}
				for(i = 1;i < (int)n/2;i++)
				{
					dr[n-i] = dr[i];
					di[n-i] = -di[i];
				}
			}
		}

		public void Windowfunc(double[] dr,double[] di,int n,int n2)
		{
			double[] wind = new double[n2];

			for (int i = 0; i < n; i++)
			{
				if (i < n2)
				{
					wind[i] = 0.54 - 0.46 * Math.Cos(2 * Math.PI * i / (n - 1));
					dr[i] *= wind[i];
					di[i] *= wind[i];
				}
				else
				{
					dr[i] = 0;
					di[i] = 0;
				}
			}

		}
		//Byte型のデータをFFTで計算するためDoubleに変換
		//ステレオ，16bit固定作成
		public void ByteToDouble(Byte[] i_data, out double[] o_data,int size, out int n)
		{
			int ch = 2;
			int b = 2;
			double[] data = new double[size/(ch*b)];

			int num = 0;
			int result = 0;
			
			for (int i = 0; i < size/(ch*b); i++)
			{
				if ((result = i_data[i*2] + i_data[i*2 + 1] * 256) > 32767)
					result = -(((result ^ 65535) & 65535) + 1);
				data[i] = (double)result;
				//if ((data[i] = i_data[i] + i_data[i + 1] * 256) > 32767)
				//    data[i] = 256 * 256 - data[i];
				num++;
			}
			
			n = num;
			o_data = data;
		}
		//上の逆
		public void DoubleToByte(double[] i_data, out Byte[] o_data, int n, out int size)
		{
			int ch = 2;
			int b = 2;
			Byte[] data = new byte[n*b];
			int count = 0;

			for (int i = 0; i < n; i++)
			{
				//lower
				data[2*i] = (Byte)(i_data[i] % 256);
				//upper
				data[2*i + 1] = (Byte)(i_data[i] / 256);

				count += 2;
			}
			size = count;
			o_data = data;
		}

		/// <summary>
		/// 音量一定化推奨中
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="n"></param>
		public void Normalize(double[] dr, int n)
		{
			double Peak = 0;

			for (int i = 0; i < n; i++)
			{
				if (Math.Abs(dr[i]) > Peak)
					Peak = Math.Abs(dr[i]);
			}

			double Spread = 32767 / Peak;
			System.Diagnostics.Debug.WriteLine(Peak,"MaxSound");
			System.Diagnostics.Debug.WriteLine(Spread, "Spread");

			for (int i = 0; i < n; i++)
				dr[i] *= Spread;
		}

		/// <summary>
		/// いらないノイズは消しましょう
		/// </summary>
		/// <param name="dr">実数部</param>
		/// <param name="di">虚数部</param>
		/// <param name="n">実際のデータ数</param>
		/// <param name="n2">FFTで使ってるデータ数</param>
		/// <param name="s">何処の音を出すんだい？</param>
		public void NoiseCut(double[] dr, double[] di, int n, int n2, Scale s)
		{
			double[] norm = new double[n2];
			double rad = 2*Math.PI / n2;
			//double[] wind = new double[n2];

			for (int i = 0; i < n2/2; i++)
			{
				if ((i > (int)s + 10) || ((int)s - 10) > i)
				{
					dr[i] = 0;
					di[i] = 0;
					dr[n2 - i - 1] = 0;
					dr[n2 - i - 1] = 0;
				}
			}
		}
	}
}
