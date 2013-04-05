using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.LinkMake.Dialog.Save
{
	/// <summary>
	/// SaveButton の概要の説明です。
	/// </summary>
	public class SaveButton : Base
	{
		public LinkSaveDialog parent;

		public SaveButton(LinkSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// 題名が空なら何もしない
			if (this.parent.titlename == null ) return;

			/////////////こっから上書き機能用処理////////////////////
			bool flag = false;

			for (int i = 0; i < parent.parent.dfList.Index.Count; i++)
			{
				DataIndex di = (DataIndex)parent.parent.dfList.Index[i];
				if (di.Num < 10) continue;
				if (parent.titlename.Equals(di.Title))	//上書き発生
				{
					parent.parent.filenum = di.Num;
					parent.parent.title = di.Title;
					DataIndex di_new = new DataIndex();
					di_new.Num = di.Num;
					di_new.Title = di.Title;
					di_new.Level = parent.num;
					
					if (parent.parent.dfList.Index.Count-1 == i)
					{
						parent.parent.dfList.Index.RemoveAt(i);
						parent.parent.dfList.Index.Add(di_new);
						
						
					}
					else
					{
						parent.parent.dfList.Index.RemoveAt(i);
						parent.parent.dfList.Index.Insert(i, di_new);
						
						
					}
					flag = true;
					break;
				}

			}

			if (!flag)
			{
				//上書きなし
				{
					DataIndex di = (DataIndex)parent.parent.dfList.Index[parent.parent.dfList.Index.Count-1];
					parent.parent.filenum = di.Num+1;
					parent.parent.title = parent.titlename;
					
					DataIndex di_new = new DataIndex();
					di_new.Num = di.Num+1;
					di_new.Title = parent.titlename;
					di_new.Level = parent.num;

					if (di_new.Num < 10) di_new.Num = 10;

					parent.parent.dfList.Index.Add(di_new);
					
				}
			}

			////Index書き込み
			String filename = "LinkData\\QuesPatt.mdi";
			StreamWriter sw = null;
			try
			{
				System.Console.WriteLine("muphicMDIファイル へ書き出し");
				// 書き込みバッファの設定
				sw = new StreamWriter(filename, false, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException e)
			{
				// ファイルオープンに失敗した場合 書き込み失敗
				MessageBox.Show("いいいいやっほおおおぉおおう！");
				//return false;
			}

			for (int i = 0; i < parent.parent.dfList.Index.Count; i++)
			{
				String buf = "";
				DataIndex diw = (DataIndex)parent.parent.dfList.Index[i];

				buf = diw.Num + " " + diw.Title + " " + diw.Level;
				sw.WriteLine(buf);
			}
			
			sw.Close();

			///////////////////////////////////////////////////

			// ファイル書き込みクラスをインスタンス化してみる
			LinkFileWriter sfw = new LinkFileWriter(this.parent.parent.score.Animals.AnimalList, parent.parent.tempo_l.TempoMode, parent.titlename, parent.parent.filenum, parent.num);//parent.level);
			
			// 実際に書き込む ただし、書き込みに失敗したらそのまま
			if (!sfw.Write()) return;

			// 戻るボタンを押したことにして、ダイアログを閉じる
			parent.back.Click(System.Drawing.Point.Empty);
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}

	}
}
