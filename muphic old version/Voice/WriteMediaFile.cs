using System;
using System.IO;
using System.Windows.Forms;

namespace muphic.tag
{
	/// <summary>
	/// WriteMediaFile の概要の説明です。
	/// </summary>
	public class WriteMediaFile
	{
		public String FileName;
		private FileStream fs;

		public WriteMediaFile(String fname)
		{
			this.FileName = fname;
			try
			{
				fs = new FileStream(fname, FileMode.Create);
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
		/// String型で与えられたものをメディアファイルに書き込む
		/// </summary>
		/// <param name="num">書き込む文字数</param>
		public void WriteStrings(String s)
		{
			byte[] bytes = this.StringtoByte(s);
			this.Write(bytes);
		}

		/// <summary>
		/// int型のものを指定されたバイト分だけメディアファイルに書き込む
		/// </summary>
		/// <param name="a">書き込む対象の整数</param>
		/// <param name="num">書き込むバイト数(intの性質上4バイトが限界)</param>
		/// <param name="isByteOrder">バイトオーダ(最下位バイトが最初のアドレスにくるかどうか)を示すフラグ(WAVEに必要)</param>
		public void WriteInteger(int a, int num, bool isByteOrder)
		{
			byte[] bytes = new byte[num];
			for(int i=0;i<num;i++)
			{
				bytes[bytes.Length-i-1] = (byte)(a & 0xFF);		//最下位バイトをbytesに代入する
				a = a >> 8;										//最下位バイトを消去する
			}
			bytes = this.ByteOrder(bytes);
			this.Write(bytes);
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
		/// String型をbyte型の配列にに変換するメソッド
		/// </summary>
		/// <param name="chars">変換するbyte型の配列</param>
		/// <returns>変換されたString型</returns>
		private byte[] StringtoByte(String s)
		{
			byte[] bytes = new byte[s.Length];
			for(int i=0;i<s.Length;i++)
			{
				bytes[i] = (byte)s[i];
			}
			return bytes;
		}

		/// <summary>
		/// メディアファイルに指定したバイト分だけ書き込むメソッド
		/// </summary>
		/// <param name="bytes">書き込むもの</param>
		public void Write(byte[] bytes)
		{
			fs.Write(bytes, 0, bytes.Length);
		}
	}
}
