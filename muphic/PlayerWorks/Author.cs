using System;
using System.Xml.Serialization;

namespace Muphic.PlayerWorks
{
	/// <summary>
	/// 作品の作者に関する情報を定義するクラス。
	/// </summary>
	[Serializable]
	[XmlRoot("作者情報")]
	public class Author
	{
		/// <summary>
		/// 作者名を取得または設定する。
		/// </summary>
		[XmlElement("作者名")]
		public string Name { get; set; }
		
		/// <summary>
		/// 作者の性別を取得または設定する。
		/// </summary>
		[XmlElement("性別")]
		public int Gender { get; set; }

		/// <summary>
		/// 作者の年齢を取得または設定する。
		/// </summary>
		[XmlElement("年齢")]
		public int Age { get; set; }


		/// <summary>
		/// 作者情報クラスの新しいインスタンスを初期化する。
		/// </summary>
		public Author()
		{
		}

		/// <summary>
		/// 作者情報クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="name">作者名。</param>
		/// <param name="gender">作者の性別。</param>
		public Author(string name, int gender)
			: this(name, gender, -1)
		{
		}

		/// <summary>
		/// 作者情報クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="name">作者名。</param>
		/// <param name="gender">作者の性別。</param>
		/// <param name="age">作者の年齢。</param>
		public Author(string name, AuthorGender gender, int age)
			: this(name, Author.AuthorGenderToInt(gender), age)
		{
		}

		/// <summary>
		/// 作者情報クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="name">作者名。</param>
		/// <param name="gender">作者の性別。</param>
		/// <param name="age">作者の年齢。</param>
		public Author(string name, int gender, int age)
		{
			this.Name = name;
			this.Gender = gender;
			this.Age = age;
		}



		/// <summary>
		/// 構成設定のプレイヤー性別を示す整数値から、作者性別列挙子に変換する。
		/// </summary>
		/// <param name="genderNum">構成設定のプレイヤー性別を示す整数値。</param>
		/// <returns>プレイヤー性別列挙子。</returns>
		public static AuthorGender IntToAuthorGender(int genderNum)
		{
			switch (genderNum)
			{
				case 1:
					return AuthorGender.Boy;
				case 2:
					return AuthorGender.Girl;
				default:
					return AuthorGender.None;
			}
		}

		/// <summary>
		/// 作者性別列挙子から、構成設定のプレイヤー性別を示す整数値に変換する。
		/// </summary>
		/// <param name="gender">プレイヤー性別列挙子。</param>
		/// <returns>構成設定のプレイヤー性別を示す整数値。</returns>
		public static int AuthorGenderToInt(AuthorGender gender)
		{
			switch (gender)
			{
				case AuthorGender.Boy:
					return 1;
				case AuthorGender.Girl:
					return 2;
				default:
					return 0;
			}
		}


		/// <summary>
		/// 現在のプレイヤー1 の情報を Author 形式で取得する。
		/// </summary>
		public static Author CurrentPlayer1Data
		{
			get { return new Author(Manager.ConfigurationManager.Current.Player1, Manager.ConfigurationManager.Current.Player1Gender); }
		}

		/// <summary>
		/// 現在のプレイヤー2 の情報を Author 形式で取得する。
		/// </summary>
		public static Author CurrentPlayer2Data
		{
			get { return new Author(Manager.ConfigurationManager.Current.Player2, Manager.ConfigurationManager.Current.Player2Gender); }
		}
	}



	/// <summary>
	/// プレイヤーの性別を示す識別子を指定する。
	/// </summary>
	public enum AuthorGender : int
	{
		/// <summary>
		/// 未設定。
		/// </summary>
		None = 0,

		/// <summary>
		/// 男児。
		/// </summary>
		Boy = 1,

		/// <summary>
		/// 女児。
		/// </summary>
		Girl = 2,
	}
}
