namespace Muphic.CompositionScreenParts
{
	/// <summary>
	/// 作曲で使用できる動物を表わす識別子を指定する。
	/// </summary>
	public enum AnimalName
	{
		/// <summary>
		/// ヒツジ
		/// </summary>
		Sheep,

		/// <summary>
		/// ウサギ
		/// </summary>
		Rabbit,

		/// <summary>
		/// 鳥
		/// </summary>
		Bird,

		/// <summary>
		/// 犬
		/// </summary>
		Dog,

		/// <summary>
		/// ブタ
		/// </summary>
		Pig,

		/// <summary>
		/// ぬこ
		/// </summary>
		Cat,

		/// <summary>
		/// 声
		/// </summary>
		Voice,
	}


	/// <summary>
	/// 動物クラス
	/// </summary>
	[System.Serializable]
	[System.Xml.Serialization.XmlRoot("動物データ")]
	public class Animal : Common.Parts, System.ICloneable
	{
		/// <summary>
		/// 動物の種類を指定する。
		/// </summary>
		[System.Xml.Serialization.XmlElement("種類")]
		public AnimalName AnimalName { get; set; }

		/// <summary>
		/// 動物の楽譜上での位置(横)を表す。
		/// </summary>
		[System.Xml.Serialization.XmlElement("位置")]
		public int Place { get; set; }

		/// <summary>
		/// 動物の楽譜上での音階(縦)を表す。
		/// </summary>
		[System.Xml.Serialization.XmlElement("音階")]
		public int Code { get; set; }


		/// <summary>
		/// 動物クラスの新しいインスタンスを初期化する。
		/// </summary>
		public Animal()
		{
		}

		/// <summary>
		/// 動物クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="place">楽譜上での位置(横)。</param>
		/// <param name="code">楽譜上での音階(縦)。</param>
		/// <param name="animal">動物名。</param>
		public Animal(int place, int code, AnimalName animal)
		{
			this.Initialize(place, code, animal);
		}

		/// <summary>
		/// 動物クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="place">楽譜上での位置(横)。</param>
		/// <param name="code">楽譜上での音階(縦)。</param>
		/// <param name="animal">動物名。</param>
		public Animal(int place, int code, AnimalButtonMode animal)
		{
			// AnimalButtonMode列挙型から文字列の値を得る
			string animalStr = System.Enum.GetName(typeof(AnimalButtonMode), animal);

			// AnimalName列挙型にその文字列と等価の定数が存在した場合は
			if (System.Enum.IsDefined(typeof(AnimalName), animalStr))
			{
				this.Initialize(place, code, (AnimalName)System.Enum.Parse(typeof(AnimalName), animalStr));
			}
			else
			{
				Tools.DebugTools.ConsolOutputError("Animal", "\"" + animalStr + "\"と等価の AnimalName 列挙型の定数が存在しないため、登録失敗");
			}
		}


		/// <summary>
		/// 動物の設定を行う。
		/// </summary>
		/// <param name="place">楽譜上での位置(横)。</param>
		/// <param name="code">楽譜上での音階(縦)。</param>
		/// <param name="animalName">動物名。</param>
		private void Initialize(int place, int code, AnimalName animalName)
		{
			this.Place = place;
			this.Code = code;
			this.AnimalName = animalName;
		}


		/// <summary>
		/// 指定された位置・音階とこの動物の位置・音階が等しいかどうかをチェックする。
		/// </summary>
		/// <param name="place">位置。</param>
		/// <param name="code">音階。</param>
		/// <returns>等しければtrue、それ以外はfalse。</returns>
		public bool Equals(int place, int code)
		{
			if (this.Place == place && this.Code == code) return true;
			else return false;
		}


		/// <summary>
		/// 現在の動物名を表す System.String を返す。
		/// </summary>
		/// <returns>。</returns>
		public override string ToString()
		{
			return System.Enum.GetName(typeof(AnimalName), this.AnimalName);
		}

		/// <summary>
		/// 現在の動物名を表わす System.String を返す。
		/// </summary>
		/// <param name="imageName">動物名に対応したテクスチャ名を得る場合はtrue、単に動物名だけを得る場合はfalse。</param>
		/// <returns>。</returns>
		public string ToString(bool imageName)
		{
			if (imageName)
			{
				switch (this.AnimalName)
				{
					case AnimalName.Sheep:
						return "IMAGE_COMPOSITIONSCR_SHEEP";

					case AnimalName.Rabbit:
						return "IMAGE_COMPOSITIONSCR_RABBIT";
					
					case AnimalName.Bird:
						return "IMAGE_COMPOSITIONSCR_BIRD";
					
					case AnimalName.Dog:
						return "IMAGE_COMPOSITIONSCR_DOG";

					case AnimalName.Pig:
						return "IMAGE_COMPOSITIONSCR_PIG";

					case AnimalName.Cat:
						return "IMAGE_COMPOSITIONSCR_CAT";

					case AnimalName.Voice:
						return "IMAGE_COMPOSITIONSCR_VOICE";

					default:
						return "";
				}
			}
			else
			{
				return this.ToString();
			}
		}

		
		/// <summary>
		/// 現在のインスタンスと等価の動物データを作成する。
		/// </summary>
		/// <returns>現在の動物データと同じ値を持つインスタンス。</returns>
		public object Clone()
		{
			return new Animal(this.Place, this.Code, this.AnimalName);
		}
	}
}
