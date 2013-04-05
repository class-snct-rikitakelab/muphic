using System;

namespace muphic.Titlemode
{
	/// <summary>
	/// HiraButton の概要の説明です。
	/// </summary>
	public class HiraButton : Base
	{
		public HiraScreen parent;
		public int num;
		public string ch;

		public HiraButton(HiraScreen parent,string c,int i)
		{
			this.parent = parent;
			ch = c;
			num = i;
		}

		private void del()
		{
			if((parent.parent.Text != null)&&(parent.parent.Text.Length > 0))
				parent.parent.Text = parent.parent.Text.Remove(parent.parent.Text.Length-1,1);
		}
		private int check()
		{
			if(this.ch.Equals("゛"))
			{
				//濁音
				//対ひらがな
				if(parent.parent.Text.EndsWith("か"))
				{
					this.del();
					parent.parent.Text += "が";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("き"))
				{
					this.del();
					parent.parent.Text += "ぎ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("く"))
				{
					this.del();
					parent.parent.Text += "ぐ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("け"))
				{
					this.del();
					parent.parent.Text += "げ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("こ"))
				{
					this.del();
					parent.parent.Text += "ご";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("さ"))
				{
					this.del();
					parent.parent.Text += "ざ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("し"))
				{
					this.del();
					parent.parent.Text += "じ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("す"))
				{
					this.del();
					parent.parent.Text += "ず";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("せ"))
				{
					this.del();
					parent.parent.Text += "ぜ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("そ"))
				{
					this.del();
					parent.parent.Text += "ぞ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("た"))
				{
					this.del();
					parent.parent.Text += "だ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ち"))
				{
					this.del();
					parent.parent.Text += "ぢ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("つ"))
				{
					this.del();
					parent.parent.Text += "づ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("て"))
				{
					this.del();
					parent.parent.Text += "で";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("と"))
				{
					this.del();
					parent.parent.Text += "ど";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("は"))
				{
					this.del();
					parent.parent.Text += "ば";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ひ"))
				{
					this.del();
					parent.parent.Text += "び";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ふ"))
				{
					this.del();
					parent.parent.Text += "ぶ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("へ"))
				{
					this.del();
					parent.parent.Text += "べ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ほ"))
				{
					this.del();
					parent.parent.Text += "ぼ";
					return 1;
				}//対カタカナ
				else if(parent.parent.Text.EndsWith("カ"))
				{
					this.del();
					parent.parent.Text += "ガ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("キ"))
				{
					this.del();
					parent.parent.Text += "ギ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ク"))
				{
					this.del();
					parent.parent.Text += "グ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ケ"))
				{
					this.del();
					parent.parent.Text += "ゲ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("コ"))
				{
					this.del();
					parent.parent.Text += "ゴ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("サ"))
				{
					this.del();
					parent.parent.Text += "ザ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("シ"))
				{
					this.del();
					parent.parent.Text += "ジ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ス"))
				{
					this.del();
					parent.parent.Text += "ズ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("セ"))
				{
					this.del();
					parent.parent.Text += "ゼ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ソ"))
				{
					this.del();
					parent.parent.Text += "ゾ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("タ"))
				{
					this.del();
					parent.parent.Text += "ダ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("チ"))
				{
					this.del();
					parent.parent.Text += "ヂ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ツ"))
				{
					this.del();
					parent.parent.Text += "ヅ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("テ"))
				{
					this.del();
					parent.parent.Text += "デ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ト"))
				{
					this.del();
					parent.parent.Text += "ド";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ハ"))
				{
					this.del();
					parent.parent.Text += "バ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ヒ"))
				{
					this.del();
					parent.parent.Text += "ビ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("フ"))
				{
					this.del();
					parent.parent.Text += "ブ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ヘ"))
				{
					this.del();
					parent.parent.Text += "ベ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ホ"))
				{
					this.del();
					parent.parent.Text += "ボ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ウ"))
				{
					this.del();
					parent.parent.Text += "ヴ";
					return 1;
				}
				else
				{
					return 1;
				}
			}
			if(this.ch.Equals("゜"))
			{
				//半濁音
				//対ひらがな
				if(parent.parent.Text.EndsWith("は"))
				{
					this.del();
					parent.parent.Text += "ぱ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ひ"))
				{
					this.del();
					parent.parent.Text += "ぴ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ふ"))
				{
					this.del();
					parent.parent.Text += "ぷ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("へ"))
				{
					this.del();
					parent.parent.Text += "ぺ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ほ"))
				{
					this.del();
					parent.parent.Text += "ぽ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ハ"))
				{
					this.del();
					parent.parent.Text += "パ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ヒ"))
				{
					this.del();
					parent.parent.Text += "ピ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("フ"))
				{
					this.del();
					parent.parent.Text += "プ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ヘ"))
				{
					this.del();
					parent.parent.Text += "ペ";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("ホ"))
				{
					this.del();
					parent.parent.Text += "ポ";
					return 1;
				}
				else
				{
					return 1;
				}
			}
			return 0;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			int i = check();
			if(i == 0)
			{
				if ((parent.parent.Text == null) || (parent.parent.Text.Length < parent.parent.maxlength))
					parent.parent.Text += ch;
			}
			System.Diagnostics.Debug.WriteLine(true,parent.parent.Text);
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

		public override string ToString()
		{
			return "HiraButton" + this.num;					//力技
		}
	}
}
