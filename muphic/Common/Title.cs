using System.Drawing;
using Muphic.Manager;

namespace Muphic.Common
{
	/// <summary>
	/// 指定した長さの背景画像と共に、題名等の文字列を描画する。
	/// </summary>
	public class Title : Screen
	{
		/// <summary>
		/// 表示される題名を示す文字列。
		/// <para>
		/// Text プロパティを使用すること。
		/// </para>
		/// </summary>
		private string __text;

		/// <summary>
		/// 実際に表示される文字列を取得または設定する。
		/// </summary>
		public string Text
		{
			get
			{
				return this.__text;
			}
			set
			{
				if (value.Length > this.MaxLength)
				{
					this.__text = value.Substring(0, this.MaxLength);
				}
				else
				{
					this.__text = value;
				}
			}
		}

		/// <summary>
		/// 文字列の最大の長さ。
		/// </summary>
		private int __maxLength;

		/// <summary>
		/// 文字列の最大の長さを示す 1 以上の整数を取得または設定する。この数値が背景画像の長さとなる。
		/// </summary>
		public int MaxLength
		{
			get
			{
				return this.__maxLength;
			}
			set
			{
				if (value <= 0) this.__maxLength = 1;
				else this.__maxLength = value;
			}
		}


		/// <summary>
		/// この部品を構成する背景画像の座標群。
		/// 0 番目は左端背景画像、Length - 1 番目は右端背景画像、それ以外は全て文字背景画像の座標となる。
		/// </summary>
		private Point[] __locations;

		/// <summary>
		/// この部品を構成する背景画像の座標群を取得する。
		/// </summary>
		private Point[] Locations
		{
			get { return this.__locations; }
		}

		/// <summary>
		/// この部品を表示する座標を取得または設定する。
		/// </summary>
		public Point Location
		{
			get
			{
				return this.__locations[0];
			}
			set
			{
				this.__locations = new Point[this.MaxLength + 2];

				// 0 番目に左端背景画像の座標、1 番目に 1 文字目の背景画像の座標を登録
				this.__locations[0] = value;
				this.__locations[1] = new Point(
					this.__locations[0].X + TextureFileManager.GetRectangle("IMAGE_TITLE_LEFT").Width,
					this.__locations[0].Y
				);

				// 文字の背景画像の横幅を取得
				int width = TextureFileManager.GetRectangle("IMAGE_TITLE_CENTER").Width;

				// 文字列の長さとなるよう、背景画像の座標を計算し登録
				for (int i = 2; i < this.MaxLength + 1; i++)
				{
					this.__locations[i] = new Point(this.__locations[1].X + (width * (i - 1)), this.__locations[0].Y);
				}

				// 配列の最後に右端背景画像の座標を登録
				this.__locations[this.__locations.Length - 1] = new Point(
					this.__locations[this.__locations.Length - 2].X + width,
					this.__locations[0].Y
				);

				// 文字を描画する位置を設定
				this.TextLocation = new Point(value.X + 13, value.Y + 12);
			}
		}


		/// <summary>
		/// 題名と文字の透過度を取得または設定する。
		/// </summary>
		public byte Alpha { get; protected set; }


		/// <summary>
		/// 題名と文字の有効性を示す。
		/// <para>Enabled プロパティを使用すること。</para>
		/// </summary>
		private bool __enabled;

		/// <summary>
		/// 題名と文字の有効性を示す値を取得または設定する。
		/// </summary>
		public virtual bool Enabled
		{
			get
			{
				return this.__enabled;
			}
			set
			{
				this.__enabled = value;

				if (value)
				{
					this.Alpha = Settings.System.Default.AlphaBlending_PartsEnabled;
				}
				else
				{																		// 無効時は
					this.Alpha = Settings.System.Default.AlphaBlending_PartsDisabled;	// 半透明に設定
				}
			}
		}


		/// <summary>
		/// 入力中の文字であることを示す点滅を行うかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsBlink { get; set; }


		/// <summary>
		/// 日本語入力を行い、未確定の文字列を表示するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsJapaneseInput { get; set; }


		/// <summary>
		/// この部品で表示する文字列を描画する座標を取得または設定する。
		/// </summary>
		private Point TextLocation { get; set; }


		/// <summary>
		/// 親クラスを表す文字列。
		/// </summary>
		private string ParentName { get; set; }


		/// <summary>
		/// 題名表示クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parentName">このクラスを利用する親クラスの名前 (通常は ToString() メソッドで得られる文字列)。</param>
		/// <param name="location">題名を表示する座標 (背景画像の左上座標)。</param>
		/// <param name="maxLength">題名の背景画像の長さ (文字数)。</param>
		public Title(string parentName, Point location, int maxLength)
		{
			this.MaxLength = maxLength;
			this.Location = location;
			this.Text = "";
			this.ParentName = parentName;
			this.Enabled = true;
			this.IsBlink = false;
			this.IsJapaneseInput = false;

			DrawManager.Regist(this.ToString(), new Point(0, 0), "IMAGE_DUMMY");
		}

		/// <summary>
		/// 題名表示クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parentName">このクラスを利用する親クラスの名前 (通常は ToString() メソッドで得られる文字列)。</param>
		/// <param name="location">題名を表示する座標 (背景画像の左上座標)。</param>
		/// <param name="maxLength">題名の背景画像の長さ (文字数)。</param>
		/// <param name="defaultText">初期状態で表示される文字列。</param>
		public Title(string parentName, Point location, int maxLength, string defaultText)
			: this(parentName, location, maxLength)
		{
			this.Text = defaultText;
		}


		/// <summary>
		/// 背景画像と共に Text プロパティで指定された文字列を描画する。
		/// </summary>
		/// <param name="drawStatus"></param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			// 題名背景画像 (始端) の描画
			DrawManager.Draw("IMAGE_TITLE_LEFT", this.Locations[0], this.Alpha);

			// 許可された文字数分だけ題名背景画像 (本体) の描画
			for (int i = 0; i < this.MaxLength; i++)
			{
				DrawManager.Draw("IMAGE_TITLE_CENTER", this.Locations[i + 1], this.Alpha);
			}

			// 題名背景画像 (終端) の描画
			DrawManager.Draw("IMAGE_TITLE_RIGHT", this.Locations[this.Locations.Length - 1], this.Alpha);

			// 題名の描画
			StringManager.Draw(this.IsJapaneseInput ? this.Text + JpnLangInputManager.NowKeys : this.Text, this.TextLocation, this.Alpha);

			// 0.5秒毎に、入力中の文字の位置を表わすアンダーバーを点滅させる
			if (this.IsBlink && (FrameManager.PlayTime.Milliseconds < 500) && this.Text.Length < this.MaxLength)
			{
				// DrawManager.Draw("IMAGE_ENTITLESCR_TITLE_UNDER", this.Locations[this.Text.Length]);
				DrawManager.Draw("＿",
					this.TextLocation.X + StringManager.StringSize.Width * this.Text.Length,
					this.TextLocation.Y
				);
			}
		}

		/// <summary>
		/// 現在の System.Object を表す文字列に、親クラスを表す文字列が付加された文字列を返す。
		/// </summary>
		/// <returns>現在の System.Object を表す文字列に、親クラスを表す文字列が付加された文字列。</returns>
		public override string ToString()
		{
			return base.ToString() + "--" + this.ParentName;
		}
	}
}
