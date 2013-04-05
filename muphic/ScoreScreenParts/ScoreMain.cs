using System.Collections.Generic;
using System.Drawing;

using Muphic.Common;
using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.ScoreScreenParts
{
	using Animal = Muphic.CompositionScreenParts.Animal;
	using AnimalName = Muphic.CompositionScreenParts.AnimalName;


	/// <summary>
	/// 動物リストから楽譜を生成し、表示する楽譜メイン処理クラス。
	/// </summary>
	public class ScoreMain : Screen
	{

		#region フィールド / プロパティ

		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		private readonly ScoreScreen __parent;

		/// <summary>
		/// 親にあたる楽譜画面を取得する。
		/// </summary>
		public ScoreScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 描画する楽譜の最初の行を示す整数値 (行オフセット)。
		/// </summary>
		private int __lineOffset;

		/// <summary>
		/// 描画する楽譜の最初の行を示す整数値 (行オフセット) を取得または設定する。
		/// </summary>
		public int LineOffset
		{
			get
			{
				return this.__lineOffset;
			}
			private set
			{
				if (value <= 0)
				{
					this.__lineOffset = 0;
				}
				else if (value >= this.MeterNum / ScoreMain.MeterPerLine)
				{
					this.__lineOffset = this.MeterNum / ScoreMain.MeterPerLine;
				}
				else
				{
					this.__lineOffset = value;
				}

				// スクロールボタン設定
				this.SetScrollButtonEnabled();

				// 楽譜を再生成
				this.CreateTextureList(this.ScoreMode, false);
			}
		}


		/// <summary>
		/// 1 フレームで描画するテクスチャのリスト。
		/// </summary>
		private TextureList TextureList { get; set; }

		/// <summary>
		/// 表示すべき楽譜の種類を取得またはは設定する。
		/// </summary>
		private ScoreScreenScoreMode ScoreMode { get; set; }


		#region 楽譜の表示に関する数値

		/// <summary>
		/// 生成される楽譜の拍の数。
		/// </summary>
		private readonly int __maxMeter;

		/// <summary>
		/// 生成される楽譜の拍の数を取得する。このプロパティで得られる値を 8 で割ると小節数に、32 で割ると行数になる。
		/// </summary>
		private int MeterNum
		{
			get { return this.__maxMeter; }
		}

		/// <summary>
		/// 単位小節あたりの拍の数を取得する (通常は 8)。
		/// </summary>
		public static int MeterPerBar
		{
			get { return 8; }
		}

		/// <summary>
		/// 単位行あたりの小節の数を取得する (通常は 4)。
		/// </summary>
		public static int BarPerLine
		{
			get { return 4; }
		}

		/// <summary>
		/// 単位行あたりの拍の数を取得する (通常は 32)。
		/// </summary>
		public static int MeterPerLine
		{
			get { return ScoreMain.MeterPerBar * ScoreMain.BarPerLine; }
		}

		/// <summary>
		/// ページあたりの行の数 (画面内に最大いくつの五線を引けるか) を取得する (通常は 7)。
		/// </summary>
		public static int LinePerPage
		{
			get { return 7; }
		}

		/// <summary>
		/// ページあたりの拍の数を取得する (通常は 224)。
		/// </summary>
		public static int MeterPerPage
		{
			get { return ScoreMain.MeterPerLine * ScoreMain.LinePerPage; }
		}

		#endregion


		#region 楽譜データ

		/// <summary>
		/// 楽譜生成元となる動物リスト。
		/// </summary>
		private readonly List<Animal> __sourceScore;

		/// <summary>
		/// 楽譜生成元となる動物リストを取得する。
		/// </summary>
		private List<Animal> SourceScore
		{
			get { return this.__sourceScore; }
		}


		/// <summary>
		/// ヒツジの楽譜データを取得または設定する。
		/// </summary>
		private Meter[] SheepScore { get; set; }

		/// <summary>
		/// 鳥の楽譜データを取得または設定する。
		/// </summary>
		private Meter[] BirdScore { get; set; }

		/// <summary>
		/// ウサギの楽譜データを取得または設定する。
		/// </summary>
		private Meter[] RabbitScore { get; set; }

		/// <summary>
		/// イヌの楽譜データを取得または設定する。
		/// </summary>
		private Meter[] DogScore { get; set; }

		/// <summary>
		/// ブタの楽譜データを取得または設定する。
		/// </summary>
		private Meter[] PigScore { get; set; }

		/// <summary>
		/// ぬこの楽譜データを取得または設定する。
		/// </summary>
		private Meter[] CatScore { get; set; }

		/// <summary>
		/// 声の楽譜データを取得または設定する。
		/// </summary>
		private Meter[] VoiceScore { get; set; }

		#endregion

		#endregion


		#region コンストラクタ

		/// <summary>
		/// 楽譜メイン処理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		/// <param name="sourceScore">楽譜生成元の動物リスト。</param>
		public ScoreMain(ScoreScreen parent, List<Animal> sourceScore) : this(sourceScore)
		{
			this.__parent = parent;
		}

		/// <summary>
		/// 楽譜メイン処理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="sourceScore">楽譜生成元の動物リスト。</param>
		public ScoreMain(List<Animal> sourceScore)
		{
			this.__parent = null;
			this.__sourceScore = sourceScore;
			this.__lineOffset = 0;
			this.TextureList = new TextureList();
			Locations.SetDefaultBasePoint();

			this.Initialization();

			// 動物リストの最後の要素の位置から、楽譜の長さを算出
			int max = 0;
			if (this.SourceScore.Count > 0) max = this.SourceScore[this.SourceScore.Count - 1].Place;
			this.__maxMeter = max + (ScoreMain.MeterPerLine - max % ScoreMain.MeterPerLine);
			if (this.__maxMeter <= 32) this.__maxMeter = 64;

			// 各動物の楽譜を生成
			this.SheepScore = this.CreateScore(AnimalName.Sheep);
			this.BirdScore = this.CreateScore(AnimalName.Bird);
			this.RabbitScore = this.CreateScore(AnimalName.Rabbit);
			this.DogScore = this.CreateScore(AnimalName.Dog);
			this.PigScore = this.CreateScore(AnimalName.Pig);
			this.CatScore = this.CreateScore(AnimalName.Cat);
			this.VoiceScore = this.CreateScore(AnimalName.Voice);

			this.SetScrollButtonEnabled();
		}

		/// <summary>
		/// 楽譜メイン処理クラスを初期化する。
		/// </summary>
		protected override void Initialization()
		{
			DrawManager.Regist(this.ToString(), Locations.ScoreBackground, "IMAGE_SCORESCR_SCOREBG");
		}

		#endregion


		#region 動物リストから楽譜生成

		/// <summary>
		/// 動物リストから、指定した動物での楽譜を生成する。
		/// </summary>
		/// <param name="animalName">生成する楽譜の動物の種類。</param>
		/// <returns>生成された楽譜。</returns>
		private Meter[] CreateScore(AnimalName animalName)
		{
			// 楽譜を生成  配列要素数は拍の数
			Meter[] score = new Meter[this.MeterNum];
			for (int i = 0; i < score.Length; i++) score[i] = new Meter();

			#region 昔の古い方法による楽譜生成
			// 動物リスト内に、対象の動物が何匹配置されているかを調べる
			//for (int i = 0; i < this.SourceScore.Count; i++) if (this.SourceScore[i].AnimalName == animalName) num++;

			//int animalNum = 0;				// 動物リスト内での、対象の動物の数
			//int nowAnimalIndex = -1;		// 動物リスト内での、現在のインデックス番号 (対象の動物と一致する動物のインデックス番号にしかならない)
			//int nowScoreIndex = 0;			// 楽譜内での、現在のインデックス番号

			//for (int i = 0; i < this.SourceScore.Count; i++)
			//{
			//    if (this.SourceScore[i].AnimalName == animalName)		// 動物リスト i 番目の動物が対象の動物だった場合
			//    {
			//        if (nowAnimalIndex == -1) nowAnimalIndex = i;		// 対象の動物に一番最初にヒットしたインデックス番号を記憶
			//        animalNum++;										// 対象の動物の数をカウントアップ
			//    }
			//}

			//for (int i = 0; i < animalNum; )
			//{
			//    // 楽譜生成メインループのようなもの
			//    // 繰り返し制御変数 i は、動物リスト内の対象の動物のうち楽譜に追加された動物の数を表す

			//    // 楽譜の現在の位置と、ヒットした対象の動物の位置が一致しているかを確認
			//    if(nowScoreIndex == this.SourceScore[nowAnimalIndex].Place)
			//    {
			//        // 一致した場合
			//        score[nowScoreIndex].AddCode(this.SourceScore[nowAnimalIndex].Code);
			//    }
			//    else
			//    {
			//        // 一致していない場合、楽譜の現在の位置を 1 つ進める
			//        nowScoreIndex++;
			//    }
			//}
			#endregion

			int lastIndex = -1;		//見つかった対象の動物の、楽譜内での位置を記憶する変数

			for (int i = 0; i < this.SourceScore.Count; i++)		// 対象の動物の楽譜を生成する
			{
				if (this.SourceScore[i].AnimalName == animalName)		// 動物リスト i 番目の動物が対象の動物だった場合
				{
					// 前回見つかった対象の動物の位置と、今回見つかった対象の動物の位置の差が 2 以上あれば、
					// 前回見つかった対象の動物の位置の拍を四分にする
					if (lastIndex != -1 && this.SourceScore[i].Place > lastIndex + 1) score[lastIndex].Length = MeterLength.L1_0;

					// 楽譜の同じ位置に当該動物の音階を追加 (この時点では、追加された拍は八分)
					lastIndex = this.SourceScore[i].Place;
					score[lastIndex].AddCode(this.SourceScore[i].Code);
				}
			}

			// 最後に見つかった対象の動物の位置が楽譜の最後でなければ、最後の拍を四分にする
			// (楽譜の最後だった場合、タイ + 8 分音符だけで五線譜が 1 行増えてしまうので、四分音符にはしない)
			if (lastIndex != -1 && (lastIndex % this.MeterNum != this.MeterNum - 1)) score[lastIndex].Length = MeterLength.L1_0;


			for (int i = 0; i < this.MeterNum; i++)		// 生成された楽譜に、タイと休符の設定を施す
			{
				#region i が休符でなければ、タイの判定と設定

				if (!score[i].IsRest)
				{
					if ((score[i].Length != MeterLength.L0_5) && (i % MeterPerBar == MeterPerBar - 1))		// 四分音符かつ小節の最後の拍だった場合、その拍と次の拍で
					{																						// タイを作る (i が四分音符なら、次の拍は必ず空いている)
						foreach (int code in score[i].Enumerable) score[i + 1].AddCode(code);	// 次の拍に音階をコピー
						score[i].Length = score[i + 1].Length = MeterLength.L0_5;				// 対象の拍と次の拍を八分にする
						score[i].Tie = Tie.Start;												// 対象の拍をタイの始端に設定
						score[i + 1].Tie = Tie.End;												// 次の拍をタイの終端に設定
						i++; continue;								// 次の拍はタイ終端であり休符でもないと
					}												// 判っているので飛ばす

					else if (score[i].Length == MeterLength.L1_0)	// タイでなくても
					{												// 四分音符であれば、次の拍を飛ばす
						i++; continue;
					}
				}

				#endregion

				#region i が休符であれば、休符の設定 (連続して続く休符を検出し、適切な長さに置き換える)

				else
				{
					// i から連続して続く休符の数
					int restCount = 0;
					// restCount が 2 なら、i は四分休符になれる
					// restCount が 4 なら、i は二分休符になれる
					// restCount が 8 なら、i は全休符になれる

					while (restCount < ScoreMain.MeterPerBar - i % MeterPerBar)		// 小節を超えない範囲で、連続して続く休符の数を数える
					{
						if (score[i + restCount].IsRest) restCount++;
						else break;
					}

					switch (i % ScoreMain.MeterPerBar)	// i が小節内の何番目の拍かによって処理を変える
					{
						case 0:
							if (restCount == 8)
							{											// 小節内全て休符の場合
								score[i].Length = MeterLength.L4_0;		// i を全休符に設定
								i += 7; continue;						// 次の小節へ進める
							}
							else goto case 2;

						case 1:
							if (restCount >= 7)
							{												// 小節の i 番目の拍から 7 連続休符の場合
								score[i].Length = MeterLength.L0_5;			// i を八分休符、
								score[i + 1].Length = MeterLength.L3_0;		// i の次の拍を付点二分休符に設定
								i += 6; continue;							// 休符とした拍 6 つを飛ばす
							}
							else goto case 2;

						case 2:
							if (restCount >= 6)
							{											// 小節の i 番目の拍から 6 連続休符の場合
								score[i].Length = MeterLength.L3_0;		// i を付点二分休符に設定
								i += 5; continue;						// 休符とした拍 5 つを飛ばす
							}
							else goto case 4;

						case 3:
							if (restCount >= 5)
							{												// 小節の i 番目の拍から 5 連続休符の場合
								score[i].Length = MeterLength.L0_5;			// i を八分休符、
								score[i + 1].Length = MeterLength.L2_0;		// i の次の拍を二分休符に設定
								i += 4; continue;							// 休符とした拍 4 つを飛ばす
							}
							else goto case 4;

						case 4:
							if (restCount >= 4)
							{											// 小節の i 番目の拍から 4 連続休符の場合
								score[i].Length = MeterLength.L2_0;		// i を二分休符に設定
								i += 3; continue;						// 休符とした拍 3 つを飛ばす
							}
							else goto case 5;

						case 5:
							if (restCount >= 3)
							{											// 小節の i 番目の拍から 3 連続休符の場合
								score[i].Length = MeterLength.L1_5;		// i を付点四分休符に設定
								i += 2; continue;						// 休符とした拍 2 つを飛ばす
							}
							else goto case 6;

						case 6:
							if (restCount >= 2)
							{											// 小節の i 番目の拍から 2 連続休符の場合
								score[i].Length = MeterLength.L1_0;		// i を四分休符に設定
								i += 1; continue;						// 休符とした拍 1 つを飛ばす
							}
							else goto case 7;

						case 7:
							if (restCount >= 1)
							{											// 小節の i 番目の拍が単独での休符の場合
								score[i].Length = MeterLength.L0_5;		// i を八分休符に設定
							}
							break;

						default:
							LogFileManager.WriteLineError("内部エラー[楽譜生成]", "単位小節あたりの拍の数が 8 でない可能性があります。");
							break;
					}



				}
				#endregion
			}

			return score;
		}


		/// <summary>
		/// 指定した動物の楽譜を取得する。
		/// </summary>
		/// <param name="animal">動物。</param>
		/// <returns>指定した動物の楽譜。</returns>
		private Meter[] GetScore(AnimalName animal)
		{
			switch (animal)
			{
				case AnimalName.Sheep:
					return this.SheepScore;

				case AnimalName.Bird:
					return this.BirdScore;

				case AnimalName.Rabbit:
					return this.RabbitScore;

				case AnimalName.Dog:
					return this.DogScore;

				case AnimalName.Pig:
					return this.PigScore;

				case AnimalName.Cat:
					return this.CatScore;

				case AnimalName.Voice:
					return this.VoiceScore;

				default:
					return default(Meter[]);
			}
		}

		#endregion


		#region テクスチャリスト生成
		
		/// <summary>
		/// 行オフセットをクリアし、指定した楽譜の描画用テクスチャリストを生成する。
		/// </summary>
		/// <param name="scoreMode"></param>
		public void CreateTextureList(ScoreScreenScoreMode scoreMode)
		{
			this.ScoreMode = scoreMode;
			this.CreateTextureList(scoreMode, true);
		}

		/// <summary>
		/// 指定した楽譜の描画用テクスチャリストを生成する。
		/// </summary>
		/// <param name="scoreMode">描画用テクスチャリストを生成する楽譜の種類。</param>
		/// <param name="isClearOffset">行のオフセットをクリアする場合は true、それ以外は false。</param>
		private void CreateTextureList(ScoreScreenScoreMode scoreMode, bool isClearOffset)
		{
			if (isClearOffset) this.LineOffset = 0;

			switch (scoreMode)
			{
				case ScoreScreenScoreMode.FullScore:
					this.CreateFullScoreTextureList();
					break;

				case ScoreScreenScoreMode.Sheep:
					this.CreateAnimalTextureList(AnimalName.Sheep);
					break;

				case ScoreScreenScoreMode.Bird:
					this.CreateAnimalTextureList(AnimalName.Bird);
					break;

				case ScoreScreenScoreMode.Rabbit:
					this.CreateAnimalTextureList(AnimalName.Rabbit);
					break;

				case ScoreScreenScoreMode.Dog:
					this.CreateAnimalTextureList(AnimalName.Dog);
					break;

				case ScoreScreenScoreMode.Pig:
					this.CreateAnimalTextureList(AnimalName.Pig);
					break;

				case ScoreScreenScoreMode.Cat:
					this.CreateAnimalTextureList(AnimalName.Cat);
					break;

				case ScoreScreenScoreMode.Voice:
					this.CreateAnimalTextureList(AnimalName.Voice);
					break;

				default:
					goto case ScoreScreenScoreMode.FullScore;
			}
		}


		#region フルスコア/各動物のテクスチャリスト生成 (請け負って丸投げ)

		/// <summary>
		/// 描画用テクスチャリストをクリアし、全ての動物の描画用テクスチャリストを再生成する。
		/// </summary>
		private void CreateFullScoreTextureList()
		{
			// 楽譜描画用テクスチャリストをクリア
			this.TextureList.Clear();

			// 各動物の楽譜を生成し、テクスチャリストへ追加
			this.CreateAnimalTextureList(AnimalName.Sheep, true);
			this.CreateAnimalTextureList(AnimalName.Bird, true);
			this.CreateAnimalTextureList(AnimalName.Rabbit, true);
			this.CreateAnimalTextureList(AnimalName.Dog, true);
			this.CreateAnimalTextureList(AnimalName.Pig, true);
			this.CreateAnimalTextureList(AnimalName.Cat, true);
			this.CreateAnimalTextureList(AnimalName.Voice, true);

			// フルスコア時はフルスコア表示のテクスチャをリストへ追加
			ScoreTools.RegistFullScoreParts(this.TextureList);
			
			// 更に、楽譜の最後であれば終端記号を追加
			if (!this.EnabledScrollNext) ScoreTools.RegistFullScoreEnd(this.TextureList);
		}

		/// <summary>
		/// 描画用テクスチャリストをクリアし、指定された動物の描画用テクスチャリストを生成する。
		/// </summary>
		/// <param name="animalName">テクスチャリストを生成する動物。</param>
		private void CreateAnimalTextureList(AnimalName animalName)
		{
			// 楽譜描画用テクスチャリストをクリア
			this.TextureList.Clear();

			// 描画用テクスチャリストに動物毎のラベルを追加
			this.TextureList.Add(ScoreTools.GetAnimalLabel(animalName), Locations.AnimalLabel);

			// 楽譜のテクスチャリスト生成
			this.CreateAnimalTextureList(animalName, false);
		}

		/// <summary>
		/// 現在のテクスチャリストに、指定された動物の描画用テクスチャリストを追加する。
		/// </summary>
		/// <param name="animalName">テクスチャリストに追加する動物。</param>
		/// <param name="isFullScore">フルスコア表示の場合は true、それ以外は false。</param>
		private void CreateAnimalTextureList(AnimalName animalName, bool isFullScore)
		{
			// 指定された動物のテクスチャリスト生成
			this.CreateTextureList(
				this.TextureList,
				this.GetScore(animalName),
				this.LineOffset,
				isFullScore ? ScoreTools.GetAnimalLineNum(animalName) : 1,
				isFullScore ? 1 : ScoreMain.LinePerPage
			);
		}

		#endregion


		#region 楽譜からテクスチャリスト生成 (実際に生成してるもの)

		/// <summary>
		/// 指定した楽譜から、描画に必要なテクスチャの選択とその座標の計算を行い、楽譜描画用テクスチャリストへ追加する。
		/// </summary>
		/// <param name="textureList">追加する対象となる楽譜描画用テクスチャリスト。</param>
		/// <param name="score">描画する対象となる楽譜。</param>
		/// <param name="offset">対象の楽譜のうち、描画を開始する行数 (行オフセット)。</param>
		/// <param name="lineStart">1 行目を描画する画面内の位置 (通常は 1、フルスコア時のみ動物毎に違う 1 ～ 7)。</param>
		/// <param name="lineNum">描画する行数 (通常、フルスコア描画時は 1、それ以外は LinePerPage プロパティ値)。</param>
		private void CreateTextureList(TextureList textureList, Meter[] score, int offset, int lineStart, int lineNum)
		{
			lineStart--;			// 計算のため描画の開始行 1～7 を 0～6 にする
			int nowMeterNum = 0;	// 計算用変数  対称の拍
			int nowLine = 1;		// 計算用変数  nowMeterNum 番目の拍がある行数

			int maxMeterNum = score.Length;														// 拍の最大数 (楽譜描画用テクスチャリスト生成ループの終了条件) の設定
			if (maxMeterNum > (offset + lineNum) * ScoreMain.MeterPerLine)						
				maxMeterNum = (offset + lineNum) * ScoreMain.MeterPerLine;

			if (maxMeterNum > score.Length) maxMeterNum = score.Length;							//   与えられた楽譜の拍の数を超えた場合、その拍の数で制限する

			for (nowMeterNum = offset * ScoreMain.MeterPerLine; nowMeterNum < maxMeterNum; )	// テクスチャリスト生成ループ
			{
				nowLine = lineStart + ScoreTools.MeterToLine(nowMeterNum) - offset;				// 現在の拍の行数を予め計算

				#region 五線の登録
				if (nowMeterNum % ScoreMain.MeterPerLine == 0)			// 行の最初の拍だった場合
				{														// 五線譜を登録する (描画位置は、描画中の現在の拍)
					ScoreTools.RegistStaff(textureList, nowLine);
				}
				#endregion

				#region 登録を行わない場合
				if (score[nowMeterNum].Length == MeterLength.None)	// 長さ無しの拍だった場合
				{													// 描画すべきものが無いため拍だけ１つ先に進める
					nowMeterNum++;
				}
				#endregion

				#region 登録を行う場合
				else
				{
					#region 拍子記号の登録
					if (nowMeterNum == 0)								// 楽譜の最初の拍だった場合
					{													// 4/4 拍子記号を登録する
						ScoreTools.RegistTimeSigneture(textureList, lineStart + 1);
					}
					#endregion

					#region 休符の登録
					if (score[nowMeterNum].IsRest)						// 休符だった場合
					{													// 長さに応じた休符のテクスチャを登録
						ScoreTools.RegistRest(textureList, score[nowMeterNum].Length, nowLine, nowMeterNum % ScoreMain.MeterPerLine);
						nowMeterNum += (int)score[nowMeterNum].Length;	// 休符の長さの分だけ先に進める
					}
					#endregion

					#region 音符の登録
					else												// 長さなしでなく、休符でもない場合
					{													// 長さと音階に応じた音符のテクスチャを登録
						ScoreTools.RegistNote(textureList, score[nowMeterNum], nowLine, nowMeterNum % ScoreMain.MeterPerLine);

						#region タイの登録
						//if (score[nowMeterNum].Tie == Tie.Start && nowMeterNum + 1 % this.MeterPerLine == 0)
						//{												
						//    // タイ始端を発見し、かつ、その拍が行の最後だった場合、タイ始端のみ登録
						//    this.RegistTie(textureList, score[nowMeterNum],nowLine, nowMeterNum % this.MeterPerLine, Tie.Start);
						//}

						//else if (score[nowMeterNum].Tie == Tie.End && nowMeterNum > 0)	// タイ終端を発見した場合
						//{
						//    if (nowMeterNum % this.MeterPerLine == 0)	// タイ終端の拍が行の最初だった場合は
						//    {											// タイ終端のみ登録
						//        this.RegistTie(textureList, score[nowMeterNum], nowLine, nowMeterNum % this.MeterPerLine, Tie.End);
						//    }
						//    else
						//    {											// タイ終端の拍が行頭でない場合は、終端の拍から逆に進みタイ始端の探索
						//        for (int i = nowMeterNum - 1; i >= nowMeterNum - nowMeterNum % this.MeterPerLine; i--)
						//        {
						//            if (score[i].Tie == Tie.Start)		// タイ始端を発見した場合
						//            {									// タイ始端から終端の登録
						//                this.RegistTie(textureList, score[i], score[nowMeterNum], nowLine, i % this.MeterPerLine, nowMeterNum % this.MeterPerLine);
						//                break;
						//            }
						//        }
						//    }
						//}
						#endregion

						#region タイの登録 -- (意味のない) 汎用性向上版
						if (score[nowMeterNum].Tie == Tie.Start && nowMeterNum + 1 % ScoreMain.MeterPerLine == 0)
						{
							// タイ始端を発見し、かつ、その拍が行の最後だった場合、タイ始端のみ登録
							ScoreTools.RegistTie(textureList, score[nowMeterNum], nowLine, nowMeterNum % ScoreMain.MeterPerLine, Tie.Start);
						}

						else if (score[nowMeterNum].Tie == Tie.End && nowMeterNum > 0)
						{
							// タイ終端を発見した場合、その拍から逆に進み始端の探索 (行頭の拍を超えたら終了)
							int tieStartMeterNum = nowMeterNum - 1;
							for (; tieStartMeterNum >= nowMeterNum - nowMeterNum % ScoreMain.MeterPerLine; tieStartMeterNum--)
							{
								if (score[tieStartMeterNum].Tie == Tie.Start)		// タイ始端を発見した場合
								{													// タイ始端から終端の登録
									ScoreTools.RegistTie(
										textureList,
										score[tieStartMeterNum],
										score[nowMeterNum],
										nowLine,
										tieStartMeterNum % ScoreMain.MeterPerLine,
										nowMeterNum % ScoreMain.MeterPerLine
									);
									break;
								}
							}

							if (tieStartMeterNum < nowMeterNum - nowMeterNum % ScoreMain.MeterPerLine)
							{
								// タイ始端を発見できなかった場合、行頭からのタイ終端のみ登録
								ScoreTools.RegistTie(textureList, score[nowMeterNum], nowLine + lineStart, tieStartMeterNum % ScoreMain.MeterPerLine, Tie.End);
							}
						}
						#endregion

						nowMeterNum += (int)score[nowMeterNum].Length;	// 音符の長さの分だけ先に進める
					}
					#endregion
				}
				#endregion
			}

			#region 終端記号の登録
			if (nowMeterNum >= score.Length)
			{
				ScoreTools.RegistEndLine(textureList, nowLine);
			}
			#endregion
		}

		#endregion

		#endregion


		#region スクロール

		/// <summary>
		/// 次のページへスクロールする。
		/// </summary>
		public void ScrollNext()
		{
			if (!this.EnabledScrollNext) return;
			if (this.ScoreMode == ScoreScreenScoreMode.FullScore) this.LineOffset++;
			else this.LineOffset += ScoreMain.LinePerPage;
		}

		/// <summary>
		/// 前のページへスクロールする。
		/// </summary>
		public void ScrollBack()
		{
			if (this.LineOffset <= 0) return;
			if (this.ScoreMode == ScoreScreenScoreMode.FullScore) this.LineOffset--;
			else this.LineOffset -= ScoreMain.LinePerPage;
		}

		/// <summary>
		/// 現在の行オフセットの状態に基づき、スクロールボタンの有効/無効の切り替えを行う。
		/// </summary>
		public void SetScrollButtonEnabled()
		{
			if (this.Parent == null) return;

			this.Parent.ScrollNextButton.Enabled = true;
			this.Parent.ScrollBackButton.Enabled = true;

			if (this.LineOffset <= 0)
			{
				// オフセットが 0 ならそれ以前には行けない　当然。
				this.Parent.ScrollBackButton.Enabled = false;
			}

			this.Parent.ScrollNextButton.Enabled = this.EnabledScrollNext;
		}


		/// <summary>
		/// 次のページへのスクロールが可能であるかどうかを確認する。
		/// </summary>
		public bool EnabledScrollNext
		{
			get
			{
				if (this.ScoreMode == ScoreScreenScoreMode.FullScore)
				{
					// フルスコア時は最大の行数になったらそれ以降には行けない　当然。
					if (this.LineOffset >= this.MeterNum / ScoreMain.MeterPerLine - 1)
					{
						return false;
					}
				}
				else
				{
					// フルスコアでない時

					// 最大の行数になった場合 (楽譜の行数からオフセット行数を引き、それが１ページ内に収まれば) それ以降には行けない
					if (this.MeterNum / ScoreMain.MeterPerLine - this.LineOffset <= ScoreMain.LinePerPage)
					{
						return false;
					}
				}

				return true;
			}
		}

		#endregion


		#region 描画 / 印刷

		/// <summary>
		/// 楽譜の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			foreach (KeyValuePair<string, Point> texture in this.TextureList.Enumerable)
			{
				DrawManager.Draw(texture.Key, texture.Value);
			}
		}


		/// <summary>
		/// 楽譜を印刷する。
		/// </summary>
		public void Print()
		{
			int maxPage = this.MaxPage, nowPage = 1;

			int offsetBackup = this.LineOffset;		// 計算に使用するため、行オフセットを一時バックアップ
			this.LineOffset = 0;					// 行オフセットをクリア

			while (true)
			{
				this.Print(nowPage, true, 1.25F);		// ページの印刷
				if (!this.EnabledScrollNext) break;		// 次ページへのスクロールができなければ、印刷ループ終了
				nowPage++;								// ループから脱しなかった場合は
				PrintManager.NextPage();				// 次のページへ進む
			}

			PrintManager.Print();				// 印刷開始

			this.LineOffset = offsetBackup;		// 行オフセットを元に戻し楽譜を再生成 (これで印刷前の状態に戻った)
		}

		/// <summary>
		/// 楽譜の指定したページを印刷する。
		/// </summary>
		/// <param name="page">印刷するページ。</param>
		/// <param name="isPrintFrame">枠を印刷する場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="baseLocation">描画する基点座標。</param>
		public void Print(int page, bool isPrintFrame, float scaling, Point baseLocation)
		{
			// 基点座標が指定されている場合は基点座標をセットし、印刷後に元に戻す
			Locations.SetBasePoint(baseLocation.X, baseLocation.Y);
			this.Print(page, isPrintFrame, scaling);
			Locations.SetDefaultBasePoint();
		}

		/// <summary>
		/// 楽譜の指定したページを印刷する。
		/// </summary>
		/// <param name="page">印刷するページ。</param>
		/// <param name="isPrintFrame">枠を印刷する場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public void Print(int page, bool isPrintFrame, float scaling)
		{
			if (isPrintFrame)
			{
				PrintManager.Regist(this.ToString(), scaling);									// 背景の登録
				PrintManager.Regist("IMAGE_COMMON_LOGO", Locations.MuphicLogo, scaling);		// muphic ロゴの登録
				//PrintManager.RegistString("だいめい", new Point(10, 10), 20);
			}

			this.LineOffset = (this.ScoreMode == ScoreScreenScoreMode.FullScore) ? page - 1 : ScoreMain.LinePerPage * (page - 1);

			foreach (KeyValuePair<string, Point> texture in this.TextureList.Enumerable)
			{
				PrintManager.Regist(texture.Key, texture.Value, scaling);						// テクスチャリストから楽譜の登録
			}
		}


		/// <summary>
		/// 楽譜の最大ページを取得する。フルスコアの場合とそれ以外の場合で値が異なる。
		/// </summary>
		public int MaxPage
		{
			get
			{
				if (this.ScoreMode == ScoreScreenScoreMode.FullScore)
				{
					// フルスコア時の最大ページ数は、拍数を１行分の拍数で割った値
					return this.MeterNum / ScoreMain.MeterPerLine;
				}
				else
				{
					// フルスコアでない時の最大ページ数は、拍数を１ページ分の拍数で割った値
					return (int)System.Math.Ceiling((double)this.MeterNum / ScoreMain.MeterPerPage);
				}
			}
		}

		#endregion


		#region キーボード

		/// <summary>
		/// 何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態。</param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			if (keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.Up) this.ScrollBack();
			else if (keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.Down) this.ScrollNext();
		}

		#endregion


		#region 解放

		/// <summary>
		/// この画面で使用したリソースを解放する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion
	}
}
