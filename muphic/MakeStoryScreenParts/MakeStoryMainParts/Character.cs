
namespace Muphic.MakeStoryScreenParts.MakeStoryMainParts
{
	/// <summary>
	/// 登場人物の種類を表わす識別子を指定する。
	/// </summary>
	public enum CharacterType
	{
		/// <summary>
		/// 未設定
		/// </summary>
		None,

		/// <summary>
		/// 男性
		/// </summary>
		Man,

		/// <summary>
		/// 男児
		/// </summary>
		Boy,

		/// <summary>
		/// 女性
		/// </summary>
		Woman,

		/// <summary>
		/// 女児
		/// </summary>
		Girl,

		/// <summary>
		/// カメ
		/// </summary>
		Turtle,

		/// <summary>
		/// クマ
		/// </summary>
		Bear,

		/// <summary>
		/// 鳥
		/// </summary>
		Bird,

		/// <summary>
		/// 犬
		/// </summary>
		Dog,
	}

	/// <summary>
	/// 登場人物の表情を表わす識別子を指定する。
	/// </summary>
	public enum CharacterAspect : int
	{
		/// <summary>
		/// 未設定
		/// </summary>
		None = -1,

		/// <summary>
		/// 嬉
		/// </summary>
		Joyful = 0,

		/// <summary>
		/// 怒
		/// </summary>
		Angry = 1,

		/// <summary>
		/// 哀
		/// </summary>
		Sad = 2,

		/// <summary>
		/// 楽
		/// </summary>
		Enjoy = 3,
	}

	/// <summary>
	/// 登場人物が向く方向を表わす識別子を指定する。
	/// </summary>
	public enum CharacterDirection : int
	{
		/// <summary>
		/// 未設定
		/// </summary>
		None = -1,

		/// <summary>
		/// 正面
		/// </summary>
		Front = 4,

		/// <summary>
		/// 左
		/// </summary>
		Left = 5,

		/// <summary>
		/// 右
		/// </summary>
		Right = 6,

		/// <summary>
		/// 背面
		/// </summary>
		Rear = 7,
	}


	/// <summary>
	/// ものがたりの絵を構成する登場人物クラス。
	/// </summary>
	[System.Serializable]
	[System.Xml.Serialization.XmlRoot("登場人物データ")]
	public class Character : Stamp, System.ICloneable
	{
		/// <summary>
		/// 登場人物の種類を取得または設定する。
		/// </summary>
		[System.Xml.Serialization.XmlElement("種類")]
		public CharacterType Type { get; set; }

		/// <summary>
		/// 登場人物の表情を取得または設定する。
		/// </summary>
		[System.Xml.Serialization.XmlElement("表情")]
		public CharacterAspect Aspect { get; set; }

		/// <summary>
		/// 登場人物の向きを取得または設定する。
		/// </summary>
		[System.Xml.Serialization.XmlElement("向き")]
		public CharacterDirection Direction { get; set; }


		/// <summary>
		/// 現在の登場人物が有効な状態 (種類・表情・向き全て設定されている状態) であるかを取得する。
		/// <para>有効な状態であれば true 、それ以外は false 。</para>
		/// </summary>
		public bool Enabled
		{
			get
			{
				return (this.Type != CharacterType.None && this.Aspect != CharacterAspect.None && this.Direction != CharacterDirection.None);
			}
		}


		/// <summary>
		/// 登場人物クラスの新しいインスタンスを初期化する。
		/// </summary>
		public Character()
			: this(CharacterType.None, CharacterAspect.None, CharacterDirection.None)
		{
		}
		/// <summary>
		/// 登場人物クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="type">登場人物の種類。</param>
		/// <param name="aspect">登場人物の表情。</param>
		/// <param name="direction">登場人物の向き。</param>
		public Character(CharacterType type, CharacterAspect aspect, CharacterDirection direction)
			: this(new System.Drawing.Point(-100, -100), type, aspect, direction)
		{
		}
		/// <summary>
		/// 登場人物クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="location">登場人物の位置。</param>
		/// <param name="type">登場人物の種類。</param>
		/// <param name="aspect">登場人物の表情。</param>
		/// <param name="direction">登場人物の向き。</param>
		public Character(System.Drawing.Point location, CharacterType type, CharacterAspect aspect, CharacterDirection direction)
			: base(location)
		{
			this.Type = type;
			this.Aspect = aspect;
			this.Direction = direction;

			this.SetStampImageName();
		}


		/// <summary>
		/// このインスタンスの登場人物の種類・表情・向きが全て設定されていれば、それらに応じたスタンプ画像名を StampImageName プロパティに設定する。
		/// </summary>
		/// <returns>スタンプ画像名を設定した場合は true、それ以外は false。</returns>
		public bool SetStampImageName()
		{
			if (this.Enabled)
			{
				this.StampImageName = Tools.MakeStoryTools.GetCharacterStampImageName(this.Type, this.Aspect, this.Direction);
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 現在のインスタンスと等価の Character オブジェクトを生成する。
		/// </summary>
		/// <returns>生成された新しいインスタンス。</returns>
		public override object Clone()
		{
			return new Character(this.Location, this.Type, this.Aspect, this.Direction);
		}


		/// <summary>
		/// このキャラクターを表す System.String を返す。
		/// </summary>
		/// <returns>キャラクターの種類・表情・向きを含む System.String。</returns>
		public override string ToString()
		{
			return string.Format("Character/{0}({1},{2})",
				System.Enum.GetName(typeof(CharacterType), this.Type),
				System.Enum.GetName(typeof(CharacterAspect), this.Aspect),
				System.Enum.GetName(typeof(CharacterDirection), this.Direction)
			);
		}

	}
}
