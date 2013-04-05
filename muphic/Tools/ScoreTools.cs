using System.Collections.Generic;
using System.Drawing;

using Muphic.ScoreScreenParts;

namespace Muphic.Tools
{
	using Animal = Muphic.CompositionScreenParts.Animal;
	using AnimalName = Muphic.CompositionScreenParts.AnimalName;


	/// <summary>
	/// 楽譜生成用ツールクラス (継承不可)
	/// <para>楽譜画面の楽譜生成に関するツールとなる静的メソッドを公開する。</para>
	/// </summary>
	public static class ScoreTools
	{
		/// <summary>
		/// 楽譜内の拍の位置から、その拍が何行目に属するかを計算する。
		/// </summary>
		/// <param name="meterIndex">楽譜内の拍の位置。</param>
		/// <returns>meterIndex で指定された拍属する行。</returns>
		public static int MeterToLine(int meterIndex)
		{
			// 拍が属する行の先頭の拍の番号を算出
			meterIndex -= meterIndex % ScoreMain.MeterPerLine;

			// 先頭の拍の番号を単位行あたりの拍の数で割り (先頭の拍が属する 0 で始まる行数を取得)、行オフセットを加算
			return meterIndex / ScoreMain.MeterPerLine + 1;
		}

		/// <summary>
		/// フルスコア表示時に、指定された動物が何行目に表示されるかを返す。
		/// </summary>
		/// <param name="animal">動物。</param>
		/// <returns>指定された動物が表示される行数。</returns>
		public static int GetAnimalLineNum(AnimalName animal)
		{
			switch (animal)
			{
				case AnimalName.Sheep:
					return 1;

				case AnimalName.Rabbit:
					return 2;

				case AnimalName.Bird:
					return 3;

				case AnimalName.Dog:
					return 4;

				case AnimalName.Pig:
					return 5;

				case AnimalName.Cat:
					return 6;

				case AnimalName.Voice:
					return 7;

				default:
					return 0;
			}
		}

		/// <summary>
		/// それぞれの動物の楽譜の背景に表示するラベルのテクスチャ名を取得する。
		/// </summary>
		/// <param name="animal">取得するラベルの動物。</param>
		/// <returns>動物に対応するラベル。</returns>
		public static string GetAnimalLabel(AnimalName animal)
		{
			switch (animal)
			{
				case AnimalName.Sheep:
					return "IMAGE_COMMON_ANIMAL_SHEEP";

				case AnimalName.Bird:
					return "IMAGE_COMMON_ANIMAL_BIRD";

				case AnimalName.Rabbit:
					return "IMAGE_COMMON_ANIMAL_RABBIT";

				case AnimalName.Dog:
					return "IMAGE_COMMON_ANIMAL_DOG";

				case AnimalName.Pig:
					return "IMAGE_COMMON_ANIMAL_PIG";

				case AnimalName.Cat:
					return "IMAGE_COMMON_ANIMAL_CAT";

				case AnimalName.Voice:
					return "IMAGE_COMMON_ANIMAL_VOICE";

				default:
					return "";
			}
		}


		/// <summary>
		/// 指定された拍が行内の何番目の小節に属するかを調べる。
		/// </summary>
		/// <param name="meter">行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		/// <returns>属する小節 (通常、0 ～ 3)。</returns>
		public static int MeterToBar(int meter)
		{
			return (meter - (meter % ScoreMain.MeterPerBar)) / ScoreMain.MeterPerBar;
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、五線及びト音記号のテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		/// <param name="line">五線及びト音記号を描画する位置 (描画する五線の画面内での行数)。</param>
		public static void RegistStaff(TextureList textureList, int line)
		{
			textureList.Add("IMAGE_SCORESCR_NOTE_STAFF", Locations.GetStaffLocation(line));
			textureList.Add("IMAGE_SCORESCR_NOTE_GCLEF", Locations.GetGClefLocation(line));
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、休符のテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		/// <param name="length">休符の長さ。</param>
		/// <param name="line">休符を描画する対象の行。</param>
		/// <param name="meterLocation">描画する休符の行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		public static void RegistRest(TextureList textureList, MeterLength length, int line, int meterLocation)
		{
			Point location;

			switch (length)
			{
				case MeterLength.L0_5:					// 八部休符
					textureList.Add("IMAGE_SCORESCR_NOTE_REST8", Locations.GetMeterLocation(line, meterLocation));
					break;

				case MeterLength.L1_0:					// 四分休符
					textureList.Add("IMAGE_SCORESCR_NOTE_REST4", Locations.GetMeterLocation(line, meterLocation));
					break;

				case MeterLength.L1_5:					// 付点四分休符		付点は四分休符の位置から縦に5pixel
					location = Locations.GetMeterLocation(line, meterLocation);
					textureList.Add("IMAGE_SCORESCR_NOTE_REST4", location);
					textureList.Add("IMAGE_SCORESCR_NOTE_RESTDOT", new Point(location.X, location.Y + 5));
					break;

				case MeterLength.L2_0:					// 二分休符
					textureList.Add("IMAGE_SCORESCR_NOTE_REST2", Locations.GetMeterLocation(line, meterLocation));
					break;

				case MeterLength.L3_0:					// 付点二分休符		付点は二分休符と同じ位置
					location = Locations.GetMeterLocation(line, meterLocation);
					textureList.Add("IMAGE_SCORESCR_NOTE_REST2", location);
					textureList.Add("IMAGE_SCORESCR_NOTE_RESTDOT", location);
					break;

				case MeterLength.L4_0:					// 全休符			位置を小節の中央に調整
					location = new Point(Locations.GetX(meterLocation + (ScoreMain.MeterPerBar / 2 - 1)) + 10, Locations.GetY(line) - 3);
					textureList.Add("IMAGE_SCORESCR_NOTE_REST2", location);
					break;

				default:
					break;
			}
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、音符のテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		/// <param name="meter">登録する音符の拍。</param>
		/// <param name="line">音符を描画する対象の行。</param>
		/// <param name="meterLocation">描画する音符の行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		public static void RegistNote(TextureList textureList, Meter meter, int line, int meterLocation)
		{
			int nowCode = meter.Count - 1;		// 
			bool isRightSide = false;			// 符幹の右側に符頭を配置する場合に true となるフラグ
			bool isNoteLineLeftSide = true;		// 高い音階の際に符幹を左下に配置する場合に true となるフラグ

			// まず最も低い音階の音符の符頭を登録 (ドの音であれば加線)
			textureList.Add(
				(meter[nowCode] == 9) ? "IMAGE_SCORESCR_NOTEPARTS_TAMA_" : "IMAGE_SCORESCR_NOTEPARTS_TAMA",
				Locations.GetNoteLocation(line, meterLocation, meter[nowCode])
			); ;

			// ある場合は、二つ目以降の符頭を登録
			for (nowCode--; nowCode >= 0; nowCode--)
			{
				// 一つ前の符頭と隣合わせ (音階の差が１) の場合、符幹の右側に符頭を配置する必要がある
				if (meter[nowCode + 1] - meter[nowCode] <= 1)
				{
					isRightSide = !isRightSide;		// この符頭は右側に配置される
					isNoteLineLeftSide = false;		// この拍では高い音階でも符幹を左側に配置できなくなった
				}
				else
				{
					isRightSide = false;
				}

				// 符頭の登録 (符幹の右側に配置する場合は、横に 6pixel ずらす)
				textureList.Add(
					(meter[nowCode] == 9) ? "IMAGE_SCORESCR_NOTEPARTS_TAMA_" : "IMAGE_SCORESCR_NOTEPARTS_TAMA",
					new Point(Locations.GetX(meterLocation) + (isRightSide ? 6 : 0), Locations.GetY(line, meter[nowCode]))
				);
			}

			// 最も低い音階がシから上だった場合、符幹を符頭から下向きに表示する
			bool isUnder = meter[meter.Count - 1] <= 3;

			// 符幹を符頭から下向きに表示する場合で、符頭が右側に配置されていない場合のみ、符幹が左側になる
			isNoteLineLeftSide = isNoteLineLeftSide && isUnder;

			// 符幹の登録  最初と最後の音階にそれぞれ符幹を付ければ殆どカバーできる (本当はライン描画できればいいんだけど)
			textureList.Add(
				"IMAGE_SCORESCR_NOTEPARTS_BOU",
				Locations.GetNoteLineLocation(line, meterLocation, meter[0], isNoteLineLeftSide, isUnder)
			);
			textureList.Add(
				"IMAGE_SCORESCR_NOTEPARTS_BOU",
				Locations.GetNoteLineLocation(line, meterLocation, meter[meter.Count - 1], isNoteLineLeftSide, isUnder)
			);

			// 唯一最大音階と最小音階の時のみ符幹が足りないのでもう１つ描画してごまかし
			if (meter[meter.Count - 1] - meter[0] >= 8 && meter.Count < 3)
			{
				textureList.Add(
					"IMAGE_SCORESCR_NOTEPARTS_BOU",
					Locations.GetNoteLineLocation(line, meterLocation, 3, isNoteLineLeftSide, isUnder)
				);
			}

			// 符尾の登録　位置は符幹と同じ
			if (meter.Length == MeterLength.L0_5)
			{
				textureList.Add(
					isUnder ? "IMAGE_SCORESCR_NOTEPARTS_HATAD" : "IMAGE_SCORESCR_NOTEPARTS_HATAU",
					Locations.GetNoteLineLocation(line, meterLocation, meter[isUnder ? meter.Count - 1 : 0], isNoteLineLeftSide, isUnder)
				);
			}
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、タイのテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		/// <param name="meter">タイの拍。</param>
		/// <param name="line">タイを描画する対象の行。</param>
		/// <param name="meterLocation">描画するタイの行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		/// <param name="tie">タイの種類 (始端もしくは終端のどちらかを指定)。</param>
		public static void RegistTie(TextureList textureList, Meter meter, int line, int meterLocation, Tie tie)
		{
		}
		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、タイのテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		/// <param name="startMeter">タイ始端の拍。行頭で終端の拍のみのタイを描画する場合は null。</param>
		/// <param name="endMeter">タイ終端の拍。行末で始端の拍のみのタイを描画する場合は null。</param>
		/// <param name="line">タイを描画する対象の行。</param>
		/// <param name="startMeterLocation">描画するタイ始端の行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		/// <param name="endMeterLocation">描画するタイ終端の行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		public static void RegistTie(TextureList textureList, Meter startMeter, Meter endMeter, int line, int startMeterLocation, int endMeterLocation)
		{
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、終止線のテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		/// <param name="line">終止線を描画する対象の行。</param>
		public static void RegistEndLine(TextureList textureList, int line)
		{
			textureList.Add("IMAGE_SCORESCR_NOTE_ENDLINE", Locations.GetEndLineLocation(line));
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、拍子記号のテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		/// <param name="line">拍子記号を描画する対象の行。</param>
		public static void RegistTimeSigneture(TextureList textureList, int line)
		{
			textureList.Add("IMAGE_SCORESCR_NOTE_METERSIGN", Locations.GetTimeSignetureLocation(line));
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、フルスコア表示用のテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		public static void RegistFullScoreParts(TextureList textureList)
		{
			textureList.Add("IMAGE_SCORESCR_FULLSCORE", Locations.FullScoreLabel);
			textureList.Add("IMAGE_SCORESCR_FULLSCORE2", Locations.GetStaffEndLocation(1));
		}


		/// <summary>
		/// 指定した楽譜描画用テクスチャリストに、フルスコア表示用の終端記号のテクスチャを登録する。
		/// </summary>
		/// <param name="textureList">登録する対象の楽譜描画用テクスチャリスト。</param>
		public static void RegistFullScoreEnd(TextureList textureList)
		{
			textureList.Add("IMAGE_SCORESCR_FULLSCOREEND", Locations.GetEndLineLocation(1));
		}
	}
}
