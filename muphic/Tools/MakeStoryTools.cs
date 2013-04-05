using System.Text;

using Muphic.PlayerWorks;
using Muphic.MakeStoryScreenParts;
using Muphic.MakeStoryScreenParts.MakeStoryMainParts;

namespace Muphic.Tools
{
	/// <summary>
	/// 物語作成用ツールクラス（継承不可）
	/// <para>物語作成画面で使用する静的メソッドを公開する。</para>
	/// </summary>
	public static class MakeStoryTools
	{
		/// <summary>
		/// スタンプ画像名の共通部文字列。
		/// </summary>
		private const string baseImageName = "IMAGE_MAKESTORYSCR";

		/// <summary>
		/// スタンプ画像名の最大文字数。
		/// </summary>
		private const int maxImageNameLength = 40;


		/// <summary>
		/// 与えられた登場人物の種類と表情と体の向きから、追尾・貼り付け用スタンプ画像のテクスチャ名を生成する。
		/// </summary>
		/// <param name="characterType">登場人物の種類。</param>
		/// <param name="characterAspect">登場人物の表情。</param>
		/// <param name="characterDirection">登場人物の体の向き。</param>
		/// <exception cref="System.FormatException">登場人物の種類・表情・体の向きに何らかの問題があり、テクスチャ名が生成できない場合</exception>
		/// <returns>与えられた登場人物に対応するスタンプ画像名。</returns>
		public static string GetCharacterStampImageName(CharacterType characterType, CharacterAspect characterAspect, CharacterDirection characterDirection)
		{
			// 生成されるテクスチャ名が格納される文字列（初期値はスタンプ画像名の共通部分文字列）
			StringBuilder imageName = new StringBuilder(MakeStoryTools.baseImageName + "_STMP", MakeStoryTools.maxImageNameLength);

			// 登場人物の種類に対応する文字列の追加
			imageName.Append("_");
			imageName.Append(MakeStoryTools.GetCharacterStampImageNameParts(characterType));

			// 登場人物の体の向きに対応する文字列の追加
			imageName.Append("_");
			imageName.Append(MakeStoryTools.GetCharacterStampImageNameParts(characterDirection));

			// 登場人物の表情に対応する文字列の追加（後ろ向きの場合を除く）
			if (characterDirection != CharacterDirection.Rear)
			{
				imageName.Append("_");
				imageName.Append(MakeStoryTools.GetCharacterStampImageNameParts(characterAspect));
			}

			// テクスチャ名生成完了
			return imageName.ToString();
		}


		/// <summary>
		/// 追尾・貼り付け用スタンプ画像のテクスチャ名のうち、指定した登場人物の種類に対応する文字列を返す。
		/// </summary>
		/// <param name="characterType">登場人物の種類。</param>
		/// <exception cref="System.FormatException">登場人物の種類の指定に何らかの問題がある場合</exception>
		/// <returns>登場人物の種類に対応する文字列。</returns>
		public static string GetCharacterStampImageNameParts(CharacterType characterType)
		{
			switch (characterType)
			{
				case CharacterType.Man:
					return "MAN";

				case CharacterType.Boy:
					return "BOY";

				case CharacterType.Woman:
					return "WOMAN";

				case CharacterType.Girl:
					return "GIRL";

				case CharacterType.Turtle:
					return "TURTLE";

				case CharacterType.Bear:
					return "BEAR";

				case CharacterType.Bird:
					return "BIRD";

				case CharacterType.Dog:
					return "DOG";

				default:
					// 上記以外の場合はエラー扱い 例外投げる
					string emsg = "GetStampImageNameParts（スタンプ画像名生成メソッド） - 不正な登場人物名指定 : " + System.Enum.GetName(typeof(CharacterType), characterType);
					throw new System.FormatException(emsg);
			}
		}


		/// <summary>
		/// 追尾・貼り付け用スタンプ画像のテクスチャ名のうち、指定した登場人物の表情に対応する文字列を返す。
		/// </summary>
		/// <param name="characterAspect">登場人物の表情。</param>
		/// <exception cref="System.FormatException">登場人物の表情の指定に何らかの問題がある場合</exception>
		/// <returns>登場人物の表情に対応する文字列。</returns>
		public static string GetCharacterStampImageNameParts(CharacterAspect characterAspect)
		{
			// 登場人物の表情に応じた文字列の付加
			switch (characterAspect)
			{
				case CharacterAspect.Joyful:
					return "J";

				case CharacterAspect.Angry:
					return "A";

				case CharacterAspect.Sad:
					return "S";

				case CharacterAspect.Enjoy:
					return "E";

				default:
					// 上記以外の場合はエラー扱い 例外投げる
					string emsg = "GetStampImageNameParts（スタンプ画像名生成メソッド） - 不正な登場人物の表情指定 : " + System.Enum.GetName(typeof(CharacterAspect), characterAspect);
					throw new System.FormatException(emsg);
			}
		}


		/// <summary>
		/// 追尾・貼り付け用スタンプ画像のテクスチャ名のうち、指定した登場人物の体の向きに対応する文字列を返す。
		/// </summary>
		/// <param name="characterDirection">登場人物の体の向き。</param>
		/// <exception cref="System.FormatException">登場人物の体の向きの指定に何らかの問題がある場合</exception>
		/// <returns>登場人物の表情に対応する文字列。</returns>
		public static string GetCharacterStampImageNameParts(CharacterDirection characterDirection)
		{
			// 登場人物の体の向きに応じた文字列の付加
			switch (characterDirection)
			{
				case CharacterDirection.Front:
					return "F";

				case CharacterDirection.Rear:
					return "B";

				case CharacterDirection.Left:
					return "L";

				case CharacterDirection.Right:
					return "R";

				default:
					// 上記以外の場合はエラー扱い 例外投げる
					string emsg = "GetStampImageNameParts（スタンプ画像名生成メソッド） - 不正な登場人物の向き指定 : " + System.Enum.GetName(typeof(CharacterDirection), characterDirection);
					throw new System.FormatException(emsg);
			}
		}


		/// <summary>
		/// 与えられた背景の場所から、背景テクスチャ名を生成する。
		/// </summary>
		/// <param name="place">背景の場所。</param>
		/// <exception cref="System.FormatException">背景の場所の指定に何らかの問題がある場合</exception>
		/// <returns>背景の場所に対応する文字列。</returns>
		public static string GetBackgroundImageName(BackgroundPlace place)
		{
			switch (place)
			{
				case BackgroundPlace.Forest1:
					return "IMAGE_MAKESTORYSCR_STBG_P_FOREST1";

				case BackgroundPlace.Forest2:
					return "IMAGE_MAKESTORYSCR_STBG_P_FOREST2";

				case BackgroundPlace.River1:
					return "IMAGE_MAKESTORYSCR_STBG_P_RIVER1";

				case BackgroundPlace.River2:
					return "IMAGE_MAKESTORYSCR_STBG_P_RIVER2";

				case BackgroundPlace.Town1:
					return "IMAGE_MAKESTORYSCR_STBG_P_TOWN1";

				case BackgroundPlace.Town2:
					return "IMAGE_MAKESTORYSCR_STBG_P_TOWN2";

				case BackgroundPlace.House1:
					return "IMAGE_MAKESTORYSCR_STBG_P_HOUSE1";

				case BackgroundPlace.House2:
					return "IMAGE_MAKESTORYSCR_STBG_P_HOUSE2";

				default:
					// 上記以外の場合はエラー扱い 例外投げる
					string emsg = "GetBackgroundImageName（背景テクスチャ名生成メソッド） - 不正な場所の指定 : " + System.Enum.GetName(typeof(BackgroundPlace), place);
					throw new System.FormatException(emsg);
			}
		}

		/// <summary>
		/// 与えられた空の状態から、背景テクスチャ名を生成する。
		/// </summary>
		/// <param name="sky">空の状態。</param>
		/// <exception cref="System.FormatException">背景の空の状態指定に何らかの問題がある場合</exception>
		/// <returns>背景の空の状態に対応する文字列。</returns>
		public static string GetBackgroundImageName(BackgroundSky sky)
		{
			switch (sky)
			{
				case BackgroundSky.Sunny:
					return "IMAGE_MAKESTORYSCR_STBG_S_SUNNY";

				case BackgroundSky.Cloudy:
					return "IMAGE_MAKESTORYSCR_STBG_S_CLOUD";

				case BackgroundSky.Night:
					return "IMAGE_MAKESTORYSCR_STBG_S_NIGHT";

				default:
					// 上記以外の場合はエラー扱い 例外投げる
					string emsg = "GetBackgroundImageName（背景テクスチャ名生成メソッド） - 不正な空の状態の指定 : " + System.Enum.GetName(typeof(BackgroundSky), sky);
					throw new System.FormatException(emsg);
			}
		}


		/// <summary>
		/// アイテムの追尾・貼り付け用スタンプ画像名。
		/// </summary>
		private static readonly string[] __itemStampImageNames = new string[] {
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CAPB",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CAPG",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_HATY",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_HATP",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_STRAW",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_RIBBN",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_BAGR",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_BAGB",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CNET",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_ICASE",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_BEETLE",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_RABBIT",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_BUGLE",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CPHONE",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_BALL",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CAR",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_RBALL",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_HUMB",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_PUD",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CAFFE",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_DFOOD",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_FISH",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_ACORN",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_APLE",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CHAIL",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_TABLE",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CHAIR",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_CLOCK",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_TELEV",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_DESBO",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_BRACK",
			"IMAGE_MAKESTORYSCR_STMP_ITEM_DRAWE",
		};

		/// <summary>
		/// アイテムの追尾・貼り付け用スタンプ画像名を取得する。
		/// </summary>
		private static string[] ItemStampImageNames
		{
			get
			{
				return __itemStampImageNames;
			}
		}

		/// <summary>
		/// 与えられたアイテムの種類から、追尾・貼り付け用スタンプ画像のテクスチャ名を生成する。
		/// </summary>
		/// <param name="itemKind">。</param>
		/// <returns>。</returns>
		public static string GetItemStampImageName(ItemKind itemKind)
		{
			return ItemStampImageNames[(int)itemKind];
		}


		/// <summary>
		/// 指定した物語データの全てのスタンプに画面上の座標とテクスチャ名を設定する。
		/// </summary>
		/// <param name="targetData">設定する物語データ。</param>
		public static void SetStampImageName(StoryData targetData)
		{
			foreach (Slide slide in targetData.SlideList)
			{
				foreach (Stamp stamp in slide.StampList)
				{
					if (stamp is Character) ((Character)stamp).SetStampImageName();
					else if (stamp is Item) ((Item)stamp).SetStampImageName();
					stamp.SetLocationScreen();
				}
			}
		}

	}
}
