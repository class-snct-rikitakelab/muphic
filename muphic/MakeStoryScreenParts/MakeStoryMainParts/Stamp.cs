using System;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace Muphic.MakeStoryScreenParts.MakeStoryMainParts
{
	/// <summary>
	/// ものがたりの絵を構成するスタンプクラス。
	/// </summary>
	/// <remarks>
	/// このクラスは抽象クラスであり、登場人物やアイテムはそれぞれこのクラスを継承した別なクラスで実装する。
	/// </remarks>
	[Serializable]
	public abstract class Stamp : System.ICloneable
	{
		/// <summary>
		/// スタンプの絵の中での位置（テクスチャの左上座標）を取得または設定する。
		/// <para>スタンプが絵に配置された際に決定し、物語保存時はこちらの座標がファイルに書き出される。</para>
		/// </summary>
		[System.Xml.Serialization.XmlElement("位置")]
		public Point LocationLocal { get; set; }


		#region 追尾/描画用 プロパティ

		/// <summary>
		/// スタンプの画面上での位置（テクスチャの中央座標）とテクスチャのサイズを保持する。
		/// <para>追尾や描画時に使用される値であり、物語保存時には破棄される。</para>
		/// </summary>
		private Rectangle __rectangle;

		/// <summary>
		/// スタンプの画面上での位置（テクスチャの中央座標）を取得または設定する。
		/// <para>追尾や描画時に使用される座標であり、物語保存時には破棄される。</para>
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public Point Location
		{
			get
			{
				return this.__rectangle.Location;
			}
			set
			{
				this.__rectangle.Location = value;
			}
		}

		/// <summary>
		/// スタンプのテクスチャのサイズを取得または設定する。
		/// <para>追尾や描画時に使用される値であり、物語保存時には破棄される。</para>
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public Size Size
		{
			get
			{
				return this.__rectangle.Size;
			}
			set
			{
				this.__rectangle.Size = value;
			}
		}

		/// <summary>
		/// スタンプの画面上での位置（テクスチャの中央座標）とテクスチャのサイズを取得する。
		/// <para>この矩形に含まれる座標はテクスチャの左上の座標。テクスチャの中心の座標を含む矩形は RectangleCenter プロパティで取得する。</para>
		/// </summary>
		public Rectangle Rectangle
		{
			get
			{
				return this.__rectangle;
			}
		}

		/// <summary>
		/// スタンプの画面上での位置（テクスチャの中央座標）とテクスチャのサイズを取得する。
		/// <para>この矩形に含まれる座標はテクスチャの中心の座標。テクスチャの左上の座標を含む矩形は Rectangle プロパティで取得する。</para>
		/// </summary>
		public Rectangle RectangleCenter
		{
			get
			{
				return new Rectangle(this.Location.X - this.Size.Width / 2, this.Location.Y - this.Size.Height / 2, this.Size.Width, this.Size.Height);
			}
		}


		/// <summary>
		/// 描画時のスタンプ画像名。
		/// <para>StampImageName プロパティを使用すること。</para>
		/// </summary>
		private string __stampImageName;

		/// <summary>
		/// 描画時のスタンプ画像名を取得または設定する。
		/// <para>追尾や描画に使用される文字列であり、物語保存時には破棄される。</para>
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public string StampImageName
		{
			get
			{
				return this.__stampImageName;
			}
			set
			{
				this.__stampImageName = value;

				// 設定されたスタンプ画像名が管理クラスに登録済みであれば、そのサイズを取得
				if(Manager.TextureFileManager.Exist(value))
				{
					this.Size = Manager.TextureFileManager.GetRectangle(value).Size;
				}
			}
		}

		#endregion


		#region コンストラクタ

		/// <summary>
		/// スタンプクラスの新しいインスタンスを初期化する。
		/// </summary>
		public Stamp()
			: this(new Point(-100, -100), "")
		{
		}
		/// <summary>
		/// スタンプクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="location">スタンプの位置。</param>
		public Stamp(Point location)
			: this(location, "")
		{
		}
		/// <summary>
		/// スタンプクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="location">スタンプの位置。</param>
		/// <param name="stampImageName">スタンプのテクスチャ名。</param>
		public Stamp(Point location, string stampImageName)
		{
			this.__rectangle = new Rectangle(location, new Size());
			this.StampImageName = stampImageName;

			this.SetLocationLocal();
		}

		#endregion


		/// <summary>
		/// 与えられたスタンプの画面上での位置とテクスチャ名が、このインスタンスと同じかどうかを判定する。
		/// </summary>
		/// <param name="stamp">判定するスタンプ。</param>
		/// <returns>同じであれば true、それ以外は false。</returns>
		public bool Equals(Stamp stamp)
		{
			return (this.Location == stamp.Location && this.StampImageName == stamp.StampImageName);
		}


		/// <summary>
		/// このスタンプの絵の中での位置を表す LocationLocal プロパティを設定する。
		/// </summary>
		public void SetLocationLocal()
		{
			this.LocationLocal = Tools.CommonTools.CenterToOnreft(PictureWindow.GetLocalPoint(this.Location), this.Size);
		}

		/// <summary>
		/// このスタンプの画面上での位置を表す Location プロパティを設定する。 
		/// </summary>
		public void SetLocationScreen()
		{
			this.Location = Tools.CommonTools.OnreftToCenter(PictureWindow.GetScreenPoint(this.LocationLocal), this.Size);
			
		}

		/// <summary>
		/// 現在のインスタンスと等価の Stamp オブジェクトを生成する。
		/// </summary>
		/// <remarks>
		/// このクラスは抽象クラスのため、このメソッドがオーバーライドされずに呼び出されることは無い。
		/// <para>このクラスを継承した Character クラス、もしくは Item クラスがこのメソッドをオーバーライドし、
		/// それぞれが生成した新しいインスタンスを返す形となる。</para>
		/// </remarks>
		/// <returns>生成された新しいインスタンス。</returns>
		public virtual object Clone()
		{
			return this;
		}

	}
}
