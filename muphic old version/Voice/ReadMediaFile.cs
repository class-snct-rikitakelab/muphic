using System;
using System.IO;
using System.Windows.Forms;

namespace muphic.tag
{
	/// <summary>
	/// ReadMediaFile の概要の説明です。
	/// </summary>
	public class ReadMediaFile
	{
		public String FileName;
		private FileStream fs;
		public ReadMediaFile(String fname)
		{
			this.FileName = fname;
			try
			{
				fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
			}
			catch(IOException e)
			{
				MessageBox.Show("ファイル" + fname + "が見つかりません");
			}
		}

		public void CloseMediaFile()
		{
			fs.Close();
		}

		/// <summary>
		/// メディアファイルから指定された分だけ読み込みString型で返す
		/// </summary>
		/// <param name="num">読み込む文字数</param>
		/// <returns>読み込んだものをString型に変換したもの</returns>
		public String ReadStrings(int num)
		{
			byte[] bytes = this.Read(num);
			if(bytes == null)
			{
				return null;				//ファイルの末尾に達した
			}
			return this.BytestoString(bytes);
		}

		/// <summary>
		/// メディアファイルから指定された分だけ読み込みint型で返す
		/// </summary>
		/// <param name="num">読み込むバイト数(intの性質上4バイトが限界)</param>
		/// <param name="isByteOrder">バイトオーダ(最下位バイトが最初のアドレスにくるかどうか)を示すフラグ(WAVEに必要)</param>
		/// <returns></returns>
		public int ReadInteger(int num, bool isByteOrder)
		{
			int answer = 0;
			byte[] bytes = this.Read(num);
			if(bytes == null)
			{
				return -1;						//ファイルの末尾に達した
			}
			if(isByteOrder)						//もしバイトオーダの場合、正常な配列に
			{									//戻さないといけない(逆にする)
				bytes = this.ByteOrder(bytes);
			}
			for(int i=0;i<bytes.Length;i++)
			{
				//System.Diagnostics.Debug.WriteLine(((int)bytes[i]).ToString(), i.ToString());
				answer += (int)bytes[i] << (bytes.Length-i-1) * 8;
			}
			return answer;
		}

		/// <summary>
		/// バイトオーダを元に戻す(もしくはバイトオーダにする)メソッド
		/// </summary>
		/// <param name="Oldchars">元のbyte型配列</param>
		/// <returns>処理後のbyte型配列</returns>
		public byte[] ByteOrder(byte[] Oldbytes)
		{
			byte[] Newbytes = new byte[Oldbytes.Length];
			for(int i=0;i<Oldbytes.Length;i++)
			{
				Newbytes[i] = Oldbytes[Oldbytes.Length-i-1];
			}
			return Newbytes;
		}


		/// <summary>
		/// byte型の配列をString型に変換するメソッド
		/// </summary>
		/// <param name="chars">変換するbyte型の配列</param>
		/// <returns>変換されたString型</returns>
		private String BytestoString(byte[] bytes)
		{
			String s = "";
			for(int i=0;i<bytes.Length;i++)
			{
				s += ((char)bytes[i]).ToString();
			}
			return s;
		}

		/// <summary>
		/// メディアファイルからchar型の配列で指定されたバイト分だけ読み込む基本メソッド
		/// </summary>
		/// <param name="num">読み込むバイト数</param>
		/// <returns></returns>
		public byte[] Read(int num)
		{
			byte[] bytes = new byte[num];
			int size = fs.Read(bytes, 0, num);
			if(size == 0)
			{
				return null;
			}
			return bytes;
		}
	}
}
