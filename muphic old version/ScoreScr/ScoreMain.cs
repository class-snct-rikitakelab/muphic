using System;
using System.Collections;
using System.Drawing;

namespace muphic.ScoreScr
{
	#region SVGA (～ver.0.8.8)
	/*
	
	/// <summary>
	/// Score の概要の説明です。
	/// </summary>
	public class ScoreMain : Screen
	{
		public ScoreScreen parent;
		public const int maxAnimals = 100;	// 配置可能な動物の数
		private int offset;					// 描画開始の音符を32ずつ(１行分ずつ)ずらす
		
		// 各動物ごとの音符リスト
		public ArrayList SheepScoreList  = new ArrayList();
		public ArrayList RabbitScoreList = new ArrayList();
		public ArrayList BirdScoreList   = new ArrayList();
		public ArrayList DogScoreList    = new ArrayList();
		public ArrayList PigScoreList    = new ArrayList();
		public ArrayList VoiceScoreList  = new ArrayList();
		
		public int MaxNote;					// 全音符リストの音符数(8で割ると小節数、32で割ると行数になる)
		public AnimalScoreMode nowScore;	// 現在表示している音符リスト
		
		public ArrayList DrawList = new ArrayList();	// 描画するデータのリスト| _-)/
		
	# region 各種サイズ定義
		const int NotePerBar = 8;							// 単位小節あたりの最大音符数
		const int BarPerLine = 4;							// 単位行あたりの小節数
		const int NotePerLine = NotePerBar * BarPerLine;	// 単位行あたりの最大音符数
		const int LinePerPage = 6;							// 画面に表示できる五線譜の最大行数
		const int NotePerPage = NotePerLine * LinePerPage;	// 画面に表示できる音符の最大数
		const int MaxChord = 3;								// 和音最大数
		const int NoteXBegin = 148;							// 音符x座標の基準(行1番目の音符のx座標)(px)
		const int NoteYBegin = 141;							// 音符y座標の基準(px)
		const int NoteXDifference = 16;						// 音符同士のx座標の差
		const int BarXDifference = 141;						// 小節同士のx座標の差(px)
		const int ScoreXBegin = 99;							// 五線譜x座標の基準(五線譜のx座標)(px)
		const int ScoreYBegin = 134;						// 五線譜y座標の基準(1行目の五線譜のy座標)(px)
		const int ScoreYDifference = 66;					// 五線譜同士のy座標の差(px)
		const int EndXBegin = 696;							// 終端線のx座標(px)
	# endregion
		
		public ScoreMain(ScoreScreen screen)
		{
			this.parent = screen;
			this.offset = 0;
			this.nowScore = AnimalScoreMode.All;
			this.parent.scores.all.State = 1;
			
	#region 画像のﾄｳﾛｰｸ ( ﾟ∀ﾟ)ﾉ
			DrawManager.Regist("QuarterNotes", 0, 0, "image\\Score\\note\\4buonpu.png");		// 四分音符-符幹上
			DrawManager.Regist("QuarterNotes_", 0, 0, "image\\Score\\note\\4buonpu_.png");		// 四分音符-符幹下
			DrawManager.Regist("QuarterNotes_do", 0, 0, "image\\Score\\note\\4buonpu_do.png");	// 四分音符-ド
			DrawManager.Regist("EighthNotes", 0, 0, "image\\Score\\note\\8buonpu.png");			// 八分音符-符幹上
			DrawManager.Regist("EighthNotes_", 0, 0, "image\\Score\\note\\8buonpu_.png");		// 八分音符-符幹下
			DrawManager.Regist("EighthNotes_do", 0, 0, "image\\Score\\note\\8buonpu_do.png");	// 八分音符-ド
			DrawManager.Regist("QuarterRest", 0, 0, "image\\Score\\note\\4bukyuhu.png");		// 四分休符
			DrawManager.Regist("EighthRest", 0, 0, "image\\Score\\note\\8bukyuhu.png");			// 八分休符
			DrawManager.Regist("AllRest", 0, 0, "image\\Score\\note\\zenkyuhu.png");			// 全休符
			DrawManager.Regist("Meter", 0, 0, "image\\Score\\note\\hyousi.png");				// 4/4拍子記号
			DrawManager.Regist("End", 0, 0, "image\\Score\\note\\end.png");						// 終端
			DrawManager.Regist("End_full", 0, 0, "image\\Score\\note\\end_full.png");			// 終端-フルスコア用
			DrawManager.Regist("Staff", 0, 0, "image\\Score\\score\\gosen.png");				// 五線譜
			DrawManager.Regist("Line", 0, 0, "image\\Score\\score\\syousetu.png");				// 小節区切り線
			DrawManager.Regist("Full", 0, 0, "image\\Score\\score\\full_line.png");				// フルスコア
			
			DrawManager.Regist("SheepBig", 0, 0, "image\\Score\\omake\\Sheep_big.png");			// 背景ﾋﾂｼﾞﾝ
			DrawManager.Regist("RabbitBig", 0, 0, "image\\Score\\omake\\Rabbit_big.png");		// 背景兎
			DrawManager.Regist("BirdBig", 0, 0, "image\\Score\\omake\\Bird_big.png");			// 背景鳥バード
			DrawManager.Regist("DogBig", 0, 0, "image\\Score\\omake\\Dog_big.png");				// 背景Dog
			DrawManager.Regist("PigBig", 0, 0, "image\\Score\\omake\\Pig_big.png");				// 背景ベイブ
	#endregion
			
			// 値の初期化と音符リストのｾｲｾｰｲ ( ﾟ∀ﾟ)ﾉ
			this.CreateScoreListAll();
			
			// 楽譜をﾋﾞｮｳｰｶﾞ ( ﾟ∀ﾟ)ﾉ
			this.ReDraw();
		}
		
		public override void Draw()
		{
			base.Draw();
			
			// 描画リストから描画するデータを読み出す
			for(int i=0; i<this.DrawList.Count; i++)
			{
				DrawData data = (DrawData)DrawList[i];
				muphic.DrawManager.Draw(data.Image, data.x, data.y);
			}
		}
		
		/// <summary>
		/// 描画リストを再生成するメソッド
		/// </summary>
		public void ReDraw()
		{
			DrawData drawdata;
			
			// 既存の描画リストをクリアする
			this.DrawList.Clear();	
			
			// DrawScoreメソッドより描画リストを再生成する
			switch(this.nowScore)
			{
				case AnimalScoreMode.All:
					this.DrawScore(this.SheepScoreList, 1, 0);
					this.DrawScore(this.RabbitScoreList, 1, ScoreYDifference);
					this.DrawScore(this.BirdScoreList, 1, ScoreYDifference * 2);
					this.DrawScore(this.DogScoreList, 1, ScoreYDifference * 3);
					this.DrawScore(this.PigScoreList, 1, ScoreYDifference * 4);
					this.DrawScore(this.VoiceScoreList, 1, ScoreYDifference * 5);
					this.DrawAll();	// フルスコア専用画像の描画
					break;
				case AnimalScoreMode.Sheep:
					drawdata = new DrawData("SheepBig", 508, 366); this.DrawList.Add(drawdata);
					this.DrawScore(this.SheepScoreList, 6, 0);	// 描画リストにひつじの楽譜をセット
					break;
				case AnimalScoreMode.Rabbit:
					drawdata = new DrawData("RabbitBig", 581, 351); this.DrawList.Add(drawdata);
					this.DrawScore(this.RabbitScoreList, 6, 0);	// 描画リストにうさぎの楽譜をセット
					break;
				case AnimalScoreMode.Bird:
					drawdata = new DrawData("BirdBig", 549, 388); this.DrawList.Add(drawdata);
					this.DrawScore(this.BirdScoreList, 6, 0);	// 描画リストにバードの楽譜をセット
					break;
				case AnimalScoreMode.Dog:
					drawdata = new DrawData("DogBig", 525, 376); this.DrawList.Add(drawdata);
					this.DrawScore(this.DogScoreList, 6, 0);	// 描画リストにいぬの楽譜をセット
					break;
				case AnimalScoreMode.Pig:
					drawdata = new DrawData("PigBig", 514, 386); this.DrawList.Add(drawdata);
					this.DrawScore(this.PigScoreList, 6, 0);	// 描画リストにベイブの楽譜をセット
					break;
				case AnimalScoreMode.Voice:
					this.DrawScore(this.VoiceScoreList, 6, 0);	// 描画リストにｳﾞｫｲｽの楽譜をセット
					break;
				default:
					break;
			}
		}
		
		/// <summary>
		/// 全ての音符リストを生成するメソッド
		/// </summary>
		public void CreateScoreListAll()
		{
			int max;
			try
			{
				// 動物リストの最後の動物の位置から、全音符リストにおける最長の位置を得る
				Animal animal = ((Animal)parent.AnimalList[parent.AnimalList.Count-1]);
				max = animal.place;
			}
			catch(Exception) // 動物0匹の状態で呼び出すと範囲外参照が発生してしまうため無理矢理…
			{
				max = 0;
			}
			
			// その値から、音符リストの音符数を算出(32の倍数になるよう調整)
			this.MaxNote = max + (NotePerLine - max % NotePerLine);
			
			// 各音符リストを初期化
			this.SheepScoreList.Clear();
			this.RabbitScoreList.Clear();
			this.BirdScoreList.Clear();
			this.DogScoreList.Clear();
			this.PigScoreList.Clear();
			this.VoiceScoreList.Clear();
			
			this.CheckScoreList(this.SheepScoreList);
			
			// 音符リストのｾｲｾｰｲ ( ﾟ∀ﾟ)ﾉ
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Sheep);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Rabbit);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Bird);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Dog);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Pig);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Voice);
		}
		
		/// <summary>
		/// 描画リストDrawListに楽譜データを登録するメソッド
		/// </summary>
		/// <param name="data">描画する音符リスト</param>
		/// <param name="mode">描画する行数</param>
		/// <param name="yoffset">フルスコア用 y座標オフセット</param>
		public void DrawScore(ArrayList data, int mode, int yoffset)
		{
			int i=this.offset, j, end;
			DrawData drawdata;
			
			// ループ終了条件の設定
			end = i + mode * NotePerLine;
			if(end > NotePerPage) end = NotePerPage;	// ただし６行分を超えた場合は６行までとする
			if(end > data.Count) end = data.Count;		// さらに音符リストの音符数を超えた場合は音符数にする
			
			yoffset -= ( this.offset / NotePerLine ) * ScoreYDifference;
			
			// offsetからスタート
			while(i < end)
			{
				Score score = (Score)data[i];
				
				// 行の最初の小節の描画時に五線譜も描画する
				if(i%NotePerLine == 0)
				{
					// y座標は1行目134px＋行数×66px
					drawdata = new DrawData( "Staff", ScoreXBegin, ScoreYBegin + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
					DrawList.Add(drawdata);	// 描画リストに追加
					
					if(i/NotePerLine == 0)
					{
						// さらに、1行目では4/4拍子の描画
						drawdata = new DrawData( "Meter", ScoreYBegin, ScoreYBegin-2 + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
						DrawList.Add(drawdata);	// 描画リストに追加
					}
				}
				
				// 音符描画
				for(j=0; j<3; j++)
				{
					// 休符の和音は在りませぬ 和音２つ目以降に音符がなければ次に進むようにする
					if( (j==1 && score.code[1]==-1) || (j==2 && score.code[2]==-1) ) break;

					Point p = getScoreCoordinate(score, j, i);	// 座標ゲッツ☆
					drawdata = new DrawData(getScoreImage(score, j), p.X, p.Y +yoffset);
					DrawList.Add(drawdata);	// 描画リストに追加
				}

				// 音符の長さ分先に進める
				i += (int)(score.length * 2);
			}

			// 終端の描画 iが最大音符数だった場合、楽譜の終端であるため
			if(i == this.MaxNote)
			{
				// 行数*66加えて描画
				if(this.nowScore != muphic.ScoreScr.AnimalScoreMode.All)
				{
					drawdata = new DrawData( "End", EndXBegin, NoteXBegin-2 + (int)(Math.Floor(i/NotePerLine) - 1) * ScoreYDifference );
					DrawList.Add(drawdata);	// 描画リストに追加
				}
			}
		}

		/// <summary>
		/// フルスコア時の描画
		/// </summary>
		public void DrawAll()
		{
			// フルスコア時はまとめるカッコを描画する
			DrawData drawdata = new DrawData("Full", 53, 135);
			DrawList.Add(drawdata);	// 描画リストに追加

			// オフセット+32が音符数だった場合、楽譜の終端と判断
			if(this.offset+NotePerLine == this.MaxNote)
			{
				drawdata = new DrawData( "End_full", EndXBegin+1, NoteXBegin-2 );
				DrawList.Add(drawdata);	// 描画リストに追加
			}

		}
		
		/// <summary>
		/// 与えられた音符の表示位置を決定するメソッド
		/// </summary>
		/// <param name="score">位置を決定する音符</param>
		/// <param name="num">和音何番目の音符か</param>
		/// <param name="i">何番目の音符か</param>
		/// <returns>表示位置座標</returns>
		public Point getScoreCoordinate(Score score, int num, int i)
		{
			Point p = new Point(0,0);
			int n = i%32;	// 行内の何番目の音符かを
			
			if(score.code[0] == -1 )
			{
				// 休符の場合
				if(score.length == 0.5) p.Y = ScoreYBegin + 20 + i/NotePerLine * ScoreYDifference;		// 八分休符
				else if(score.length == 1) p.Y = ScoreYBegin + 14 + i/NotePerLine * ScoreYDifference;	// 四分休符
				else if(score.length == 2) p.Y = ScoreYBegin + 24 + i/NotePerLine * ScoreYDifference;	// 二分休符
				else if(score.length == 4) p.Y = ScoreYBegin + 19 + i/NotePerLine * ScoreYDifference;	// 全休符
			}
			else
			{
				// 休符じゃない場合 音階ごとにy座標を変える
				p.Y = ScoreYBegin-3 + score.code[num]*4-4 + i/NotePerLine * ScoreYDifference;
	
				// シより上の音だったら符幹が下の音符になる
				if( -1 < score.code[num] && score.code[num] < 3 )
					// ただし、ラよし下の音符がある和音の場合はその限りではない
					if( !(score.code[0] > 2 || score.code[1] > 2 || score.code[2] > 2) ) 
						p.Y += 22;
			}

			// 音符のx座標を決定
			// 1番目の音符座標148px + 小節数(0～3)*141px + 小節内の音符番(0～7)*16px
			p.X = NoteXBegin + (n/NotePerBar * BarXDifference) + (n%NotePerBar * NoteXDifference);
			
			// 八分以外の場合はそれぞれx座標加えてバランス調整
			if(score.length == 1) p.X += NoteXDifference / 2;
			else if(score.length == 2) p.X += (NoteXDifference / 2) * 3;
			else if(score.length == 4) p.X += (NoteXDifference / 2) * 7;

			// 下のドだった場合少し左にずらす
			if(score.code[num] == 8) p.X -= 3;

			return p;
		}
		
		/// <summary>
		/// 与えられた音符の画像を決定するメソッド
		/// </summary>
		/// <param name="score">画像を決定する音符</param>
		/// <param name="num">和音何番目の音符か</param>
		/// <returns>表示画像文字列</returns>
		public String getScoreImage(Score score, int num)
		{
			// 和音一つ目の音符(code[0]) 休符もありえる
			if(num == 0 && score.code[0] == -1)
			{
				// 八分休符に決定
				if(score.length == 0.5) return "EighthRest";

				// 四分休符に決定
				if(score.length == 1) return "QuarterRest";

				// それ以外は二分休符か全休符
				return "AllRest";
			}
			// 和音二つ目以降は休符は無し
			// そしてなんかアレ 符幹が下の音符使わなくていい気がしてきた

			// 八分音符に決定 ただし八分の画像は和音一番上でしか使いません
			if(score.length == 0.5 && num == 0)
			{
				// 下のドだったらドの音符にする
				if(score.code[num] == 8) return "EighthNotes_do";

				// シより上の音だったら符幹が下の音符になる
				if( -1 < score.code[num] && score.code[num] < 3 )
					// ただし、ラよし下の音符がある和音の場合はその限りではない
					if( !(score.code[0] > 2 || score.code[1] > 2 || score.code[2] > 2) ) 
						return "EighthNotes_";

				// 条件にかからなければ普通の八分音符
				return "EighthNotes";
			}
			// それ以外は四分音符になりまする
			else
			{
				// 下のドだったらドの音符にする
				if(score.code[num] == 8) return "QuarterNotes_do";

				// シより上の音だったら符幹が下の音符になる
				if( -1 < score.code[num] && score.code[num] < 3 )
					// ただし、ラよし下の音符がある和音の場合はその限りではない
					if( !(score.code[0] > 2 || score.code[1] > 2 || score.code[2] > 2) ) 
						return "QuarterNotes_";

				// 条件にかからなければ普通の四分音符
				return "QuarterNotes";
			}
		}

		/// <summary>
		/// AnimalListからそれぞれの動物の音符リストを作る
		/// </summary>
		/// <param name="animalmode">リスト作成の対象の動物</param>
		public void CreateScoreList(muphic.ScoreScr.AnimalScoreMode animalmode)
		{
			int i,n;
			Animal animal = new Animal(0,0);
			Score[] ScoreList = new Score[this.MaxNote];
			for(i=0; i<this.MaxNote; i++) ScoreList[i] = new Score();
			i=n=0;

			// 動物リストに含まれている対象の動物の数を取得
			int num = this.CheckAnimalNumber(animalmode);

			// 0番目の動物のデータを取得 対象の動物にヒットするまでデータ取得し続ける 
			if(num > 0)
			{
				animal = ((Animal)parent.AnimalList[n]);
				while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);
			}

			// 音符リスト作成メインループ
			while(num > 0)
			{
				// 楽譜位置iと動物位置placeが一致し、かつ対象の動物であるかどうか 
				if( i==animal.place && animal.AnimalName.Equals(animalmode.ToString()) )
				{
					// 上記条件を満たした場合、音階を楽譜にコピー
					ScoreList[i].AddCode(animal.code);

					// さらに、リスト内の対象動物数を１減らす
					// リスト内の対象動物数が0になったらループ終了
					if(--num == 0) break;

					// 次の動物データを取得 対象の動物にヒットするまでデータ取得し続ける
					animal = ((Animal)parent.AnimalList[++n]);
					while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);

					// 次の動物が次の楽譜位置に無い場合、四分音符にする
					if(i % 32 != 31 && animal.place > i+1) ScoreList[i].length = 1;
				}
				else
				{
					// 条件を満たさなかった場合はiを進める
					i++;
				}
			}

			// 最後の音符を四分にする
			if( (i % NotePerLine != NotePerLine-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;

			// 音符リスト洗練ループ
			i = -1;
			while(++i < this.MaxNote)
			{
				// 全休符判定
				// 小節の最初の音符で判定を実施
				if( (i%NotePerBar == NotePerBar-4*2) && CheckRest(ScoreList, i, 8) )
				{
					ScoreList[i].length = 4;	// 小節内のすべての音符が休符だったら、全休符にする
					i += 7; continue;			// 一小節分先に進める
				}
				
				// 二分休符判定
				// 小節内の5つ目以降の音符では判定する必要がない
				if( (i%NotePerBar <= NotePerBar-2*2) && CheckRest(ScoreList, i, 4) )
				{
				
					ScoreList[i].length = 2;	// 二分休符
					i += 3; continue;			// 二分先に進める
				}
				
				// 四分休符判定
				// 小節内の7つ目以降の音符では判定する必要がない
				if( (i%NotePerBar <= NotePerBar-1*2) && CheckRest(ScoreList, i, 2) )
				{
					ScoreList[i].length = 1;	// 四分休符
					i += 1; continue;			// 四分先に進める
				}
				
				// タイの判定
				// その小節が楽譜の最後の小節でない場合
				if( (i%NotePerLine == NotePerBar-1) && (this.MaxNote > i) )
				{
					// また休符でなく四分音符だった場合が該当
					if ( (ScoreList[i].code[0] != -1) && (ScoreList[i].length >= 1) )
					{
						ScoreList[i].tie = true;					// タイフラグon
						ScoreList[i].length = 0.5;					// 音符の長さを八分に
						ScoreList[i+1].length = 0.5;				// 次の音符の長さを八分に
						ScoreList[i+1].code[0] = ScoreList[i].code[0];	// 音符をコピー
						ScoreList[i+1].code[1] = ScoreList[i].code[1];	// 音符をコピー
						ScoreList[i+1].code[2] = ScoreList[i].code[2];	// 音符をコピー
						Console.WriteLine("DEBUG");
					}
				}
				i += (int)(ScoreList[i].length * 2) - 1;
			}

			// 生成した配列のリストを用意された各動物ごとのフィールドにコピーする
			switch(animalmode)
			{
				case AnimalScoreMode.Sheep:
					for(i=0; i<this.MaxNote; i++) this.SheepScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Rabbit:
					for(i=0; i<this.MaxNote; i++) this.RabbitScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Bird:
					for(i=0; i<this.MaxNote; i++) this.BirdScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Dog:
					for(i=0; i<this.MaxNote; i++) this.DogScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Pig:
					for(i=0; i<this.MaxNote; i++) this.PigScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Voice:
					for(i=0; i<this.MaxNote; i++) this.VoiceScoreList.Add(ScoreList[i]);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 指定された範囲が全て休符かどうかをチェックする
		/// </summary>
		/// <param name="data">チェックする音符リスト</param>
		/// <param name="i">開始要素番号</param>
		/// <param name="n">チェックする音符数</param>
		/// <returns>範囲の音符全て休符ならtrue そうでないならfalse</returns>
		public bool CheckRest(Score[] data, int i, int n)
		{
			int j=0;
			for(; j<n; j++)
			{
				if(data[i+j].code[0] != -1) return false;
			}
			return true;
		}

		/// <summary>
		/// リスト内に指定した動物がいくつ含まれているかチェックするメソッド
		/// </summary>
		/// <param name="mode"></param>
		/// <returns>含まれていた数</returns>
		public int CheckAnimalNumber(muphic.ScoreScr.AnimalScoreMode mode)
		{
			int num=0;
			for(int i=0; i<parent.AnimalList.Count; i++)
			{
				Animal a = ((Animal)parent.AnimalList[i]);
				if(a.AnimalName.Equals(mode.ToString())) num++;
			}
			return num;
		}

		/// <summary>
		/// 音符リストをチェックする ってか一覧を表示する
		/// 主にデバッグ用
		/// </summary>
		/// <param name="data">一覧を表示する音符リスト</param>
		/// <param name="length">リストの長さ</param>
		public void CheckScoreList(Score[] data, int length)
		{
			int i=0;

			for(i=0; i<length; ++i)
			{
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data[i].length, data[i].code[0], data[i].code[1], data[i].code[2]);
			}
		}
		public void CheckScoreList(ArrayList list)
		{
			int i=0;

			for(i=0; i<list.Count; ++i)
			{
				Score data = (Score)list[i];
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data.length, data.code[0], data.code[1], data.code[2]);
			}
		}

		/// <summary>
		/// 上スクロールボタンを押した際の動作
		/// </summary>
		public void UpScroll()
		{
			// １行目より上には行かないようにする
			if(this.offset >= 32) this.offset -= 32;

			// そして再描画
			this.ReDraw();
		}

		/// <summary>
		/// 下スクロールボタンを押した際の動作
		/// </summary>
		public void DownScroll()
		{
			if(this.nowScore != AnimalScoreMode.All)
			{
				// フルスコアでは無い場合、楽譜が７行以上になった時のみ下にスクロール可
				if(this.MaxNote > 192 && this.offset < this.MaxNote-32) this.offset += 32;
			}
			else
			{
				// フルスコアの場合
				if(this.offset < this.MaxNote-32) this.offset += 32;
			}

			// そして再描画
			this.ReDraw();
		}

		/// <summary>
		/// オフセットをクリアするメソッド
		/// 何となくprivateにしたかったんで
		/// </summary>
		public void ClearOffset()
		{
			this.offset = 0;
		}
	}
	
	*/
	#endregion

	#region XGA (ver.0.9.0～)
	/*
	/// <summary>
	/// Score の概要の説明です。
	/// </summary>
	public class ScoreMain : Screen
	{
		public ScoreScreen parent;
		public const int maxAnimals = 100;	// 配置可能な動物の数
		private int offset;					// 描画開始の音符を32ずつ(１行分ずつ)ずらす
		
		// 各動物ごとの音符リスト
		public ArrayList SheepScoreList  = new ArrayList();
		public ArrayList RabbitScoreList = new ArrayList();
		public ArrayList BirdScoreList   = new ArrayList();
		public ArrayList DogScoreList    = new ArrayList();
		public ArrayList PigScoreList    = new ArrayList();
		public ArrayList CatScoreList    = new ArrayList();
		public ArrayList VoiceScoreList  = new ArrayList();
		
		public int MaxNote;					// 全音符リストの音符数(8で割ると小節数、32で割ると行数になる)
		public AnimalScoreMode nowScore;	// 現在表示している音符リスト
		
		public ArrayList DrawList = new ArrayList();	// 描画するデータのリスト| _-)/
		
		# region 各種サイズ定義
		const int NotePerBar = 8;							// 単位小節あたりの最大音符数 ※変更すると動作できないと思う
		const int BarPerLine = 4;							// 単位行あたりの小節数
		const int NotePerLine = NotePerBar * BarPerLine;	// 単位行あたりの最大音符数
		const int LinePerPage = 7;							// 画面に表示できる五線譜の最大行数
		const int NotePerPage = NotePerLine * LinePerPage;	// 画面に表示できる音符の最大数
		const int MaxChord = 3;								// 和音最大数
		const int NoteXBegin = 189;							// 音符x座標の基準(行1番目の音符のx座標)(px ※XGA修正済み)
		const int NoteYBegin = 141;							// 音符y座標の基準(px)
		const int NoteXDifference = 21;						// 音符同士のx座標の差(※XGA修正済み)
		const int BarXDifference = 181;						// 小節同士のx座標の差(px ※XGA修正済み)
		const int ScoreXBegin = 122;						// 五線譜x座標の基準(五線譜のx座標)(px ※XGA修正済み)
		const int ScoreYBegin = 173;						// 五線譜y座標の基準(1行目の五線譜のy座標)(px ※XGA修正済み)
		const int ScoreYDifference = 67;					// 五線譜同士のy座標の差(px ※XGA修正済み)
		const int EndXBegin = 906;							// 終端線のx座標(px ※XGA修正済み)

		const int BigImageX = 600;			// おまけ画像x座標
		const int BigImageY = 450;			// おまけ画像y座標
		# endregion
		
		/// <summary>
		/// ScoreMain コンストラクタ
		/// </summary>
		/// <param name="screen">上位画面クラス</param>
		public ScoreMain(ScoreScreen screen)
		{
			this.parent = screen;
			this.offset = 0;
			this.nowScore = AnimalScoreMode.All;
			this.parent.scores.all.State = 1;
			
			#region 画像のﾄｳﾛｰｸ ( ﾟ∀ﾟ)ﾉ
			DrawManager.Regist("QuarterNotes", 0, 0, "image\\ScoreXGA\\note\\4buonpu.png");			// 四分音符-符幹上
			DrawManager.Regist("QuarterNotes_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_.png");		// 四分音符-符幹下
			DrawManager.Regist("QuarterNotes_do", 0, 0, "image\\ScoreXGA\\note\\4buonpu_do.png");	// 四分音符-ド
			DrawManager.Regist("QuarterNotes_wa", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa.png");	// 四分音符-符幹上和音用
			DrawManager.Regist("QuarterNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa_.png");	// 四分音符-符幹下和音用
			DrawManager.Regist("EighthNotes", 0, 0, "image\\ScoreXGA\\note\\8buonpu.png");			// 八分音符-符幹上
			DrawManager.Regist("EighthNotes_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_.png");		// 八分音符-符幹下
			DrawManager.Regist("EighthNotes_do", 0, 0, "image\\ScoreXGA\\note\\8buonpu_do.png");	// 八分音符-ド
			DrawManager.Regist("EighthNotes_wa", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa.png");	// 八分音符-符幹上和音用
			DrawManager.Regist("EighthNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa_.png");	// 八分音符-符幹下和音用
			DrawManager.Regist("AllRest", 0, 0, "image\\ScoreXGA\\note\\zenkyuhu.png");				// 全休符/二分休符
			DrawManager.Regist("PHalfRest", 0, 0, "image\\ScoreXGA\\note\\2bukyuhu_huten.png");		// 付点二分休符
			DrawManager.Regist("PQuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu_huten.png");	// 付点四分休符
			DrawManager.Regist("QuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu.png");			// 四分休符
			DrawManager.Regist("EighthRest", 0, 0, "image\\ScoreXGA\\note\\8bukyuhu.png");			// 八分休符
			DrawManager.Regist("Meter", 0, 0, "image\\ScoreXGA\\note\\hyousi.png");					// 4/4拍子記号
			DrawManager.Regist("End", 0, 0, "image\\ScoreXGA\\note\\end.png");						// 終端
			DrawManager.Regist("End_full", 0, 0, "image\\ScoreXGA\\note\\end_full.png");			// 終端-フルスコア用
			DrawManager.Regist("Staff", 0, 0, "image\\ScoreXGA\\score\\gosen.png");					// 五線譜
			DrawManager.Regist("Line", 0, 0, "image\\ScoreXGA\\score\\syousetu.png");				// 小節区切り線
			DrawManager.Regist("Full", 0, 0, "image\\ScoreXGA\\score\\full_line.png");				// フルスコア

			DrawManager.Regist("TieTop", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t.png");			// タイ(上)
			DrawManager.Regist("TieTopHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h1.png");	// タイ(上・始端)
			DrawManager.Regist("TieTopHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h2.png");	// タイ(上・終端)
			DrawManager.Regist("TieUnder", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u.png");			// タイ(下)
			DrawManager.Regist("TieUnderHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h1.png");	// タイ(下・始端)
			DrawManager.Regist("TieUnderHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h2.png");	// タイ(下・終端)
			
			DrawManager.Regist("SheepBig", 0, 0, "image\\ScoreXGA\\omake\\Sheep_big.png");			// 背景ﾋﾂｼﾞﾝ
			DrawManager.Regist("RabbitBig", 0, 0, "image\\ScoreXGA\\omake\\Rabbit_big.png");		// 背景兎
			DrawManager.Regist("BirdBig", 0, 0, "image\\ScoreXGA\\omake\\Bird_big.png");			// 背景鳥バード
			DrawManager.Regist("DogBig", 0, 0, "image\\ScoreXGA\\omake\\Dog_big.png");				// 背景Dog
			DrawManager.Regist("PigBig", 0, 0, "image\\ScoreXGA\\omake\\Pig_big.png");				// 背景ベイブ
			DrawManager.Regist("CatBig", 0, 0, "image\\ScoreXGA\\omake\\Cat_big.png");				// 背景ぬこ
			DrawManager.Regist("VoiceBig", 0, 0, "image\\ScoreXGA\\omake\\Voice_big.png");			// 背景う゛ぉいす
			#endregion
			
			// 値の初期化と音符リストのｾｲｾｰｲ ( ﾟ∀ﾟ)ﾉ
			this.CreateScoreListAll();
			
			// 楽譜をﾋﾞｮｳｰｶﾞ ( ﾟ∀ﾟ)ﾉ
			this.ReDraw();
		}
		
		public override void Draw()
		{
			base.Draw();
			
			// 描画リストから描画するデータを読み出す
			for(int i=0; i<this.DrawList.Count; i++)
			{
				DrawData data = (DrawData)DrawList[i];
				muphic.DrawManager.Draw(data.Image, data.x, data.y);
			}
		}
		
		/// <summary>
		/// 描画リストを再生成するメソッド
		/// </summary>
		public void ReDraw()
		{
			DrawData drawdata;
			
			// 既存の描画リストをクリアする
			this.DrawList.Clear();	
			
			#region DrawScoreメソッドより描画リストを再生成する
			switch(this.nowScore)
			{
				case AnimalScoreMode.All:
					this.DrawScore(this.SheepScoreList, 1, 0);
					this.DrawScore(this.RabbitScoreList, 1, ScoreYDifference);
					this.DrawScore(this.BirdScoreList, 1, ScoreYDifference * 2);
					this.DrawScore(this.DogScoreList, 1, ScoreYDifference * 3);
					this.DrawScore(this.PigScoreList, 1, ScoreYDifference * 4);
					this.DrawScore(this.CatScoreList, 1, ScoreYDifference * 5);
					this.DrawScore(this.VoiceScoreList, 1, ScoreYDifference * 6);
					this.DrawAll();	// フルスコア専用画像の描画
					break;
				case AnimalScoreMode.Sheep:
					drawdata = new DrawData("SheepBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.SheepScoreList, LinePerPage, 0);	// 描画リストにひつじの楽譜をセット
					break;
				case AnimalScoreMode.Rabbit:
					drawdata = new DrawData("RabbitBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.RabbitScoreList, LinePerPage, 0);	// 描画リストにうさぎの楽譜をセット
					break;
				case AnimalScoreMode.Bird:
					drawdata = new DrawData("BirdBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.BirdScoreList, LinePerPage, 0);	// 描画リストにバードの楽譜をセット
					break;
				case AnimalScoreMode.Dog:
					drawdata = new DrawData("DogBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.DogScoreList, LinePerPage, 0);	// 描画リストにいぬの楽譜をセット
					break;
				case AnimalScoreMode.Pig:
					drawdata = new DrawData("PigBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.PigScoreList, LinePerPage, 0);	// 描画リストにベイブの楽譜をセット
					break;
				case AnimalScoreMode.Cat:
					drawdata = new DrawData("CatBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.CatScoreList, LinePerPage, 0);	// 描画リストにぬこの楽譜をセット
					break;
				case AnimalScoreMode.Voice:
					drawdata = new DrawData("VoiceBig", BigImageX,  BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.VoiceScoreList, LinePerPage, 0);	// 描画リストにｳﾞｫｲｽの楽譜をセット
					break;
				default:
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// 全ての音符リストを生成するメソッド
		/// </summary>
		public void CreateScoreListAll()
		{
			int max;
			try
			{
				// 動物リストの最後の動物の位置から、全音符リストにおける最長の位置を得る
				Animal animal = ((Animal)parent.AnimalList[parent.AnimalList.Count-1]);
				max = animal.place;
			}
			catch(Exception) // 動物0匹の状態で呼び出すと範囲外参照が発生してしまうため無理矢理…
			{
				max = 0;
			}
			
			// その値から、音符リストの音符数を算出(32の倍数になるよう調整)
			this.MaxNote = max + (NotePerLine - max % NotePerLine);
			
			// 各音符リストを初期化
			this.SheepScoreList.Clear();
			this.RabbitScoreList.Clear();
			this.BirdScoreList.Clear();
			this.DogScoreList.Clear();
			this.PigScoreList.Clear();
			this.CatScoreList.Clear();
			this.VoiceScoreList.Clear();
			
			this.CheckScoreList(this.SheepScoreList);
			
			// 音符リストのｾｲｾｰｲ ( ﾟ∀ﾟ)ﾉ
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Sheep);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Rabbit);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Bird);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Dog);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Pig);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Cat);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Voice);
		}
		
		#region 楽譜描画リスト生成メソッド群

		/// <summary>
		/// 描画リストDrawListに楽譜データを登録するメソッド
		/// </summary>
		/// <param name="data">描画する音符リスト</param>
		/// <param name="mode">描画する行数</param>
		/// <param name="yoffset">フルスコア用 y座標オフセット</param>
		public void DrawScore(ArrayList data, int mode, int yoffset)
		{
			int i=this.offset, j, end;
			Point p = new Point(0, 0);				
			String filename;
			DrawData drawdata;
			
			// ループ終了条件の設定
			end = i + mode * NotePerLine;
			if(end > NotePerPage) end = NotePerPage;	// ただし６行分を超えた場合は６行までとする
			if(end > data.Count) end = data.Count;		// さらに音符リストの音符数を超えた場合は音符数にする
			
			yoffset -= ( this.offset / NotePerLine ) * ScoreYDifference;
			
			// offsetからスタート
			while(i < end)
			{
				Score score = (Score)data[i];
				
				// 行の最初の小節の描画時に五線譜も描画する
				if(i%NotePerLine == 0)
				{
					// y座標は1行目134px＋行数×67px
					drawdata = new DrawData( "Staff", ScoreXBegin, ScoreYBegin + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
					DrawList.Add(drawdata);	// 描画リストに追加
					
					if(i/NotePerLine == 0)
					{
						// さらに、1行目では4/4拍子の描画
						drawdata = new DrawData( "Meter", ScoreXBegin+40, ScoreYBegin+13 + (int)(Math.Floor(i/NotePerLine) * ScoreYDifference) +yoffset );
						DrawList.Add(drawdata);	// 描画リストに追加
					}
				}
				
				// 音符描画
				for(j=0; j<3; j++)
				{
					// 休符の和音は在りませぬ 和音２つ目以降に音符がなければ次に進むようにする
					if( (j==1 && score.code[1]==-1) || (j==2 && score.code[2]==-1) ) break;

					p = getScoreCoordinate(score, j, i);	// 座標ゲッツ☆
					drawdata = new DrawData(getScoreImage(score, j), p.X, p.Y +yoffset);
					DrawList.Add(drawdata);	// 描画リストに追加
				}
				
				#region タイの描画

				if(score.tie == 1)
				{
					if(i%NotePerLine != NotePerLine-1)
					{
						// 行の最後でなかった場合、普通にタイ画像を描画
						
						// 符幹の向きでタイの位置も変わる
						if( this.CheckCodeDirection(score) == 0)
						{
							// 符幹の向きが上だった場合、タイ画像は音符の下に描画する
							p = getScoreCoordinate(score, score.chord-1, i);	// まず音符の座標を得る
							p.X += 2;											// 得た音符の座標を基準にして
							p.Y += 34;											// 画像の位置を調節する
							filename = "TieUnder";								// 画像ファイル名の設定
						}
						else
						{
							// 符幹の向きが下だった場合、タイ画像は音符の上に描画する
							p = getScoreCoordinate(score, 0, i);	// まず音符の座標を得る
							p.X += 3;								// 得た音符の座標を基準にして
							p.Y -= 10;								// 画像の位置を調節する
							filename = "TieTop";					// 画像ファイル名の設定
						}
					}
					else
					{
						// 行の最後だった場合、前半(始端側)のみ描画

						// 符幹の向きでタイの位置も変わる
						if( this.CheckCodeDirection(score) == 0)
						{
							// 符幹の向きが上だった場合、タイ画像は音符の下に描画する
							p = getScoreCoordinate(score, score.chord-1, i);	// まず音符の座標を得る
							p.X += 2;											// 得た音符の座標を基準にして
							p.Y += 34;											// 画像の位置を調節する
							filename = "TieUnderHalf1";							// 画像ファイル名の設定
						}
						else
						{
							// 符幹の向きが下だった場合、タイ画像は音符の上に描画する
							p = getScoreCoordinate(score, 0, i);	// まず音符の座標を得る
							p.X += 3;								// 得た音符の座標を基準にして
							p.Y -= 10;								// 画像の位置を調節する
							filename = "TieTopHalf1";				// 画像ファイル名の設定
							
						}
					}

					// そして描画リストに追加
					drawdata = new DrawData(filename, p.X, p.Y +yoffset);
					DrawList.Add(drawdata);
				}
				else if( (score.tie == 2) && (i%NotePerLine == 0) )
				{
					// ２行に渡ったタイの後半(終端側)の描画
					
					// 符幹の向きでタイの位置も変わる
					if( this.CheckCodeDirection(score) == 0)
					{
						// 符幹の向きが上だった場合、タイ画像は音符の下に描画する
						p = getScoreCoordinate(score, score.chord-1, i);	// まず音符の座標を得る
						p.X -= 12;											// 得た音符の座標を基準にして
						p.Y += 34;											// 画像の位置を調節する
						filename = "TieUnderHalf2";							// 画像ファイル名の設定
					}
					else
					{
						// 符幹の向きが下だった場合、タイ画像は音符の上に描画する
						p = getScoreCoordinate(score, 0, i);	// まず音符の座標を得る
						p.X -= 12;								// 得た音符の座標を基準にして
						p.Y -= 10;								// 画像の位置を調節する
						filename = "TieTopHalf2";				// 画像ファイル名の設定	
					}

					// そして描画リストに追加
					drawdata = new DrawData(filename, p.X, p.Y +yoffset);
					DrawList.Add(drawdata);
				}
				
				#endregion
				
				// 音符の長さ分先に進める
				i += (int)(score.length * 2);
			}
			
			// 終端の描画 iが最大音符数だった場合、楽譜の終端であるため
			if(i == this.MaxNote)
			{
				// 行数*67加えて描画
				if(this.nowScore != muphic.ScoreScr.AnimalScoreMode.All)
				{
					drawdata = new DrawData( "End", EndXBegin, ScoreYBegin+12 + (int)(Math.Floor(i/NotePerLine) - 1) * ScoreYDifference );
					DrawList.Add(drawdata);	// 描画リストに追加
				}
			}
		}
		
		
		/// <summary>
		/// フルスコア時の描画
		/// </summary>
		public void DrawAll()
		{
			// フルスコア時はまとめるカッコを描画する
			DrawData drawdata = new DrawData("Full", 64, 174);
			DrawList.Add(drawdata);	// 描画リストに追加

			// オフセット+32が音符数だった場合、楽譜の終端と判断
			if(this.offset+NotePerLine == this.MaxNote)
			{
				drawdata = new DrawData( "End_full", EndXBegin+1, ScoreYBegin+12 );
				DrawList.Add(drawdata);	// 描画リストに追加
			}
		}
		
		
		/// <summary>
		/// 与えられた音符の表示位置を決定するメソッド
		/// </summary>
		/// <param name="score">位置を決定する音符</param>
		/// <param name="num">和音何番目の音符か</param>
		/// <param name="i">音符リスト何番目の音符か</param>
		/// <returns>表示位置座標</returns>
		public Point getScoreCoordinate(Score score, int num, int i)
		{
			Point p = new Point(0,0);
			int line = i/NotePerLine;	// 何行目の音符なのか
			int n = i%NotePerLine;		// 行内の何番目の音符なのか
			
			if(score.code[0] == -1 )
			{
				// 休符の場合
				if(score.length == 0.5) p.Y = ScoreYBegin + 20 + line * ScoreYDifference;			// 八分休符
				else if(score.length == 1)   p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// 四分休符
				else if(score.length == 1.5) p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// 付点四分休符
				else if(score.length == 2)   p.Y = ScoreYBegin + 24 + line * ScoreYDifference;		// 二分休符
				else if(score.length == 3)   p.Y = ScoreYBegin + 21 + line * ScoreYDifference;		// 付点二分休符
				else if(score.length == 4)   p.Y = ScoreYBegin + 19 + line * ScoreYDifference;		// 全休符
			}
			else
			{
				// 休符じゃない場合 音階ごとにy座標を変える
				p.Y = ScoreYBegin-3 + score.code[num]*4-4 + line * ScoreYDifference;
	
				if( this.CheckCodeDirection(score) == 1 )
				{
					// シより上の音だったら符幹が下の音符になる
					p.Y += 22;
					p.X += 3;
					
					//  隣り合わせの和音でズレた音符だった場合 左にずらす
					if( this.CheckChordMode(score, num) ) p.X -= 8;

					// ズレた和音ならば更に右へ少しずらしてバランス調整
					int j=0;
					for(; j<score.chord; j++) if(this.CheckChordMode(score, j)) break;
					if(j != score.chord) p.X += 5;　
				}
				else
				{
					// 符幹が上(通常)で隣り合わせの和音でズレた音符だった場合 右にずらす
					if( this.CheckChordMode(score, num) ) p.X += 8;
				}
			}

			// 音符のx座標を決定
			// 1番目の音符座標189px + 小節数(0～3)*181px + 小節内の音符番(0～7)*16px
			p.X += NoteXBegin + (n/NotePerBar * BarXDifference) + (n%NotePerBar * NoteXDifference);
			
			// 八分以外の場合はそれぞれx座標加えてバランス調整
			if(score.length == 1) p.X += (int)Math.Round(NoteXDifference / 2.0, 0);
			else if(score.length == 1.5) p.X += (int)Math.Round((NoteXDifference / 2.0) * 2, 0);
			else if(score.length == 2)   p.X += (int)Math.Round((NoteXDifference / 2.0) * 3, 0);
			else if(score.length == 3)   p.X += (int)Math.Round((NoteXDifference / 2.0) * 5, 0);
			else if(score.length == 4)   p.X += (int)Math.Round((NoteXDifference / 2.0) * 7, 0);

			// 下のドだった場合少し左にずらす
			if(score.code[num] == 8) p.X -= 3;

			// 更に、２行目以降の１小節目の場合 1pxずつ左にずらして4/4拍子の空白部分を埋める
			if( (line != 0) && (n < NotePerBar) ) p.X -= (NotePerBar - n) * 2;
			
			return p;
		}
		

		/// <summary>
		/// 与えられた音符の画像を決定するメソッド
		/// </summary>
		/// <param name="score">画像を決定する音符</param>
		/// <param name="num">和音何番目の音符か</param>
		/// <returns>表示画像文字列</returns>
		public String getScoreImage(Score score, int num)
		{
			// 和音一つ目の音符(code[0])が休符だった場合
			if(num == 0 && score.code[0] == -1)
			{
				// 八分休符に決定
				if(score.length == 0.5) return "EighthRest";

				// 四分休符に決定
				if(score.length == 1) return "QuarterRest";

				// 付点四分休符に決定
				if(score.length == 1.5) return "PQuarterRest";

				// 付点二分休符に決定
				if(score.length == 3) return "PHalfRest";

				// それ以外は二分休符か全休符
				return "AllRest";
			}
			
			if(score.length == 0.5)
			{
				// 八分音符
				
				if(num == 0 && this.CheckCodeDirection(score) == 0)
				{
					// 符幹上の場合、八分の画像は和音一番上でしか使わない

					// 下のドだったらドの音符にする
					if(score.code[num] == 8) return "EighthNotes_do";

					// 隣り合わせの和音だったらズレた符幹下音符
					if( this.CheckChordMode(score, num) ) return "EighthNotes_wa";

					// 条件にかからなければ普通の八分音符
					return "EighthNotes";
				}
				if(num == score.chord-1 && this.CheckCodeDirection(score) == 1)
				{
					// 符幹下の場合、八分の画像は和音一番下でしか使わない
					
					// 隣り合わせの和音だったらズレた符幹下音符
					if( this.CheckChordMode(score, num) ) return "EighthNotes_wa_";
					
					// 条件にかからなければ普通の八分音符
					return "EighthNotes_";
				}
			}

			// それ以外は四分音符になりまする

			// 下のドだったらドの音符にする
			if(score.code[num] == 8) return "QuarterNotes_do";

			// シより上の音で構成されている場合は符幹が下になる
			if( this.CheckCodeDirection(score) == 1 )
			{
				// 隣り合わせの和音だったらズレた符幹下音符
				if( this.CheckChordMode(score, num) ) return "QuarterNotes_wa_";
				
				// それ以外は普通の符幹下音符
				return "QuarterNotes_";
			}
			
			// 隣り合わせの和音だったらズレた符幹下音符
			if( this.CheckChordMode(score, num) ) return "QuarterNotes_wa";
			
			// 条件にかからなければ普通の四分音符
			return "QuarterNotes";
		}
		
		
		/// <summary>
		/// 与えられた音符の符幹の向きを決めるメソッド
		/// </summary>
		/// <param name="score">対象の音符(和音含め)</param>
		/// <returns>
		/// 符幹の向き
		/// -1:休符
		/// 0:上(通常)
		/// 1:下(シ・ドのみの音/和音)
		/// </returns>
		public int CheckCodeDirection(Score score)
		{
			// そもそも休符だったら
			if(score.code[0] == -1) return -1;

			// 符幹が下になる条件は、シ・ド(高い方)であること
			for(int i=0; i<score.code.Length; i++)
			{
				// 和音内にシより下の音が合ったら 符幹は上(通常)になる
				if(score.code[i] > 2) return 0;
			}

			// 上のループで引っかからなければ符幹は下になる
			return 1;
		}
		
		
		/// <summary>
		/// 隣り合わせの和音を判定するメソッド
		/// </summary>
		/// <param name="score"></param>
		/// <param name="num"></param>
		/// <returns>ズレた音符ならtrue</returns>
		public bool CheckChordMode(Score score, int num)
		{
			// 和音じゃなかったらｶｴﾚ!
			if(score.chord == 1) return false;
			
			if(this.CheckCodeDirection(score) == 0)
			{
				// 符幹の向きが上(通常)ならば

				// 和音一番下の音はズレた音符にはならない
				if(num == 2) return false;

				// 和音数２の場合
				if(score.chord == 2)
				{
					// 和音の上の音で隣り合わせだったらズレた音符になる
					if( (num == 0) && (score.code[1] - score.code[0] == 1) ) return true;
					
					// それ以外は普通の音符
					return false;
				}
				
				// 和音数３の場合
				if(score.chord == 3)
				{
					// ３つの音全部隣り合わせだった場合
					if( (score.code[2]-score.code[1] == 1) && (score.code[1]-score.code[0] == 1) )
					{
						// 真ん中の音であればズレた音符になる
						if(num == 1) return true;

						// それ以外は普通の音符
						return false;
					}

					// 和音の上の音で隣り合わせだったらズレた音符になる
					if( (num == 0) && (score.code[0] - score.code[1] == 1) ) return true;
					
					// 和音２番目の音で、上下と隣りあわせだったらそれぞれズレた音符になる
					if( (num == 1) && (score.code[1] - score.code[0] == 1) ) return true;
					if( (num == 1) && (score.code[2] - score.code[1] == 1) ) return true;
					
					// それ以外は普通の音符
					return false;
				}
			}
			else if(this.CheckCodeDirection(score) == 1)
			{
				// 符幹が向きが下(シとド)の場合
				// 和音数が２のシとド以外ありえない
				
				// 和音２番目の音で、上と隣り合わせだったらズレた音符になる
				if( (num == 1) && (score.code[1] - score.code[0] == 1) ) return true;
				
				// それ以外は全て普通の音符
				return false;
			}
			
			// あと全部ｶｴﾚ!
			return false;
		}
		
		#endregion
		
		#region 音符リスト生成メソッド群

		/// <summary>
		/// AnimalListからそれぞれの動物の音符リストを作る
		/// </summary>
		/// <param name="animalmode">リスト作成の対象の動物</param>
		public void CreateScoreList(muphic.ScoreScr.AnimalScoreMode animalmode)
		{
			int i,n;
			int temp=-1;	// 和音カウント用変数
			Animal animal = new Animal(0,0);
			Score[] ScoreList = new Score[this.MaxNote];
			for(i=0; i<this.MaxNote; i++) ScoreList[i] = new Score();
			i=n=0;

			// 動物リストに含まれている対象の動物の数を取得
			int num = this.CheckAnimalNumber(animalmode);

			#region 音符リスト生成

			// 0番目の動物のデータを取得 対象の動物にヒットするまでデータ取得し続ける 
			if(num > 0)
			{
				animal = ((Animal)parent.AnimalList[n]);
				while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);
			}

			// 音符リスト作成メインループ
			while(num > 0)
			{
				// 楽譜位置iと動物位置placeが一致し、かつ対象の動物であるかどうか 
				if( i==animal.place && animal.AnimalName.Equals(animalmode.ToString()) )
				{
					// 上記条件を満たした場合、音階を楽譜にコピー
					ScoreList[i].AddCode(animal.code);
					
					// 楽譜位置iが前と同じだった場合、和音と判定
					if(temp == i) ScoreList[i].chord++; 
					temp = i;

					// さらに、リスト内の対象動物数を１減らす
					// リスト内の対象動物数が0になったらループ終了
					if(--num == 0) break;

					// 次の動物データを取得 対象の動物にヒットするまでデータ取得し続ける
					animal = ((Animal)parent.AnimalList[++n]);
					while(!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);

					// 次の動物が次の楽譜位置に無い場合、四分音符にする
					if(animal.place > i+1) ScoreList[i].length = 1;

					// 和音数が最大値になったら強制的にiを進める
					if(ScoreList[i].chord >= MaxChord) { i++; temp=0; }
				}
				else
				{
					// 条件を満たさなかった場合はiを進める
					i++;
					temp = 0;
				}
			}
			
			// 最後の音符を四分にする(もし行の最後の音符だったら、タイだけでもう1行ってのもアレなんで無視しちゃいましょう)
			//if( (i % NotePerLine != NotePerLine-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;
			if( (i % this.MaxNote != this.MaxNote-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;
			
			#endregion
			
			#region タイ・休符の判定 旧式(～ ver.0.10.1)
//			
//			i = -1;
//			while(++i < this.MaxNote)
//			{
//				// 休符ではなかった場合
//				if(ScoreList[i].code[0] != -1)
//				{
//					// タイの判定
//					// 四分音符だったら
//					if( (ScoreList[i].length >= 1) && (i%NotePerBar == NotePerBar-1) )
//					{
//						ScoreList[i+1].length = ScoreList[i].length - 0.5;	// 次の音符の長さを八分に
//						ScoreList[i+1].code[0] = ScoreList[i].code[0];		// 音符をコピー
//						ScoreList[i+1].code[1] = ScoreList[i].code[1];		// 音符をコピー
//						ScoreList[i+1].code[2] = ScoreList[i].code[2];		// 音符をコピー
//						ScoreList[i].length = 0.5;							// 音符の長さを八分に
//						ScoreList[i].tie = true;							// タイフラグon
//					}
//
//					// iが休符でなければ即次へ
//					i += (int)(ScoreList[i].length * 2) - 1;
//					continue;
//				}
//
//				// 全休符判定
//				// 小節の最初の音符で判定を実施
//				if( (i%NotePerBar == NotePerBar-4*2) && CheckRest(ScoreList, i, 8) )
//				{
//					ScoreList[i].length = 4;	// 小節内のすべての音符が休符だったら、全休符にする
//					i += 7; continue;			// 一小節分先に進める
//				}
//
//				// 付点二分休符判定
//				// 小節内の7つ目以降の音符では判定する必要がない
//				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-3*2) && CheckRest(ScoreList, i, 6) )
//				{
//					ScoreList[i].length = 3;	// 付点二分休符
//					i += 5; continue;			// 一分先に進める
//				}
//				
//				// 二分休符判定
//				// 小節内の5つ目以降の音符では判定する必要がない
//				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-2*2) && CheckRest(ScoreList, i, 4) )
//				{
//				
//					ScoreList[i].length = 2;	// 二分休符
//					i += 3; continue;			// 二分先に進める
//				}
//
//				// 付点四分休符判定
//				// 小節内の6つ目以降の音符では判定する必要がない
//				if( (i%NotePerBar <= NotePerBar-1.5*2) && CheckRest(ScoreList, i, 3) )
//				{
//				
//					ScoreList[i].length = 1.5;	// 付点四分休符
//					i += 2; continue;			// 三分先に進める
//				}
//
//				// 四分休符判定
//				// 小節内の7つ目以降の音符では判定する必要がない
//				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-1*2) && CheckRest(ScoreList, i, 2) )
//				{
//					ScoreList[i].length = 1;	// 四分休符
//					i += 1; continue;			// 四分先に進める
//				}
//				
//				// 何れにも該当しなければ、8部休符となる
//				//i += (int)(ScoreList[i].length * 2) - 1;
//			}
//			
			#endregion
			
			#region タイ・休符の判定 (ver.0.10.2 ～)

			i = -1;
			while(++i < this.MaxNote)
			{
				// 休符ではなかった場合
				if(ScoreList[i].code[0] != -1)
				{
					// タイの判定
					// 四分音符だったら
					if( (ScoreList[i].length >= 1) && (i%NotePerBar == NotePerBar-1) )
					{
						for(int j=0; j<MaxChord; j++)
							ScoreList[i+1].code[j] = ScoreList[i].code[j];	// 音符を
						ScoreList[i+1].length = ScoreList[i].length - 0.5;	// 次の音符の長さを八分に
						ScoreList[i+1].chord = ScoreList[i].chord;			// 和音情報のコピー
						ScoreList[i].length = 0.5;							// 音符の長さを八分に
						ScoreList[i].tie = 1;								// タイフラグon
						ScoreList[i+1].tie = 2;								// タイフラグon
					}

					// iが休符でなければ即次へ
					i += (int)(ScoreList[i].length * 2) - 1;
					continue;
				}
				
				// iを含めた連続した休符の数 以下例
				//  restnum == 2 四分休符になれる
				//  restnum == 4 二分休符になれる
				//  restnum == 8 全休符になれる
				int restnum = this.CheckRestNum(ScoreList, i);

				switch(i%NotePerBar)
				{
					case 0:
						if( restnum == 8 )
						{
							// 小節内が全て休符だった場合
							ScoreList[i].length = 4;	// 小節内のすべての音符が休符だったら、全休符にする
							i += NotePerBar-1;			// 次の小節へ進める
							continue;			
						}
						goto case 2;
					case 1:
						if( restnum >= 7 )
						{
							ScoreList[i+1].length = 3;	// 八分休符+付点二分休符
							i += 6;						// 次の小節へ進める
							continue;
						}
						goto case 2;
					case 2:
						if( restnum >= 6 )
						{
							ScoreList[i].length = 3;	// 付点二分休符
							i += 5;						// 一分先に進める
							continue;
						}
						goto case 4;
					case 3:
						if( restnum >= 5 )
						{
							ScoreList[i+1].length = 2;	// 八分休符+二分休符
							i += 4;						// 次の小節へ進める
							continue;
						}
						goto case 4;
					case 4:
						if( restnum >= 4 )
						{
							ScoreList[i].length = 2;	// 二分休符
							i += 3;						// 二分分先に進める
							continue;
						}
						goto case 5;
					case 5:
						if( restnum >= 3 )
						{
							ScoreList[i].length = 1.5;	// 付点四分休符
							i += 2;						// 二分分先に進める
							continue;
						}
						goto case 6;
					case 6:
						if( restnum >= 2 )
						{
							ScoreList[i].length = 1;	// 四分休符
							i += 1;						// 二分分先に進める
							continue;
						}
						goto case 7;
					case 7:
                        break;	// 何れにも該当しなければ、八分休符となる
					default:
						// ここに到達する場合、単位小節あたりの最大音符数が8でない可能性が
						System.Console.WriteLine("NotePerBar != 8");
						break;
				}
			}
			#endregion
			
			#region 生成した配列のリストを用意された各動物ごとのフィールドにコピーする
			switch(animalmode)
			{
				case AnimalScoreMode.Sheep:
					for(i=0; i<this.MaxNote; i++) this.SheepScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Rabbit:
					for(i=0; i<this.MaxNote; i++) this.RabbitScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Bird:
					for(i=0; i<this.MaxNote; i++) this.BirdScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Dog:
					for(i=0; i<this.MaxNote; i++) this.DogScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Pig:
					for(i=0; i<this.MaxNote; i++) this.PigScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Cat:
					for(i=0; i<this.MaxNote; i++) this.CatScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Voice:
					for(i=0; i<this.MaxNote; i++) this.VoiceScoreList.Add(ScoreList[i]);
					break;
				default:
					break;
			}
			#endregion
		}
		
		
		/// <summary>
		/// 指定された範囲が全て休符かどうかをチェックする
		/// </summary>
		/// <param name="data">チェックする音符リスト</param>
		/// <param name="i">開始要素番号</param>
		/// <param name="n">チェックする音符数</param>
		/// <returns>範囲の音符全て休符ならtrue そうでないならfalse</returns>
		public bool CheckRest(Score[] data, int i, int n)
		{
			int j=0;
			for(; j<n; j++)
			{
				if(data[i+j].code[0] != -1) return false;
			}
			return true;
		}
		
		
		/// <summary>
		/// 指定された要素からの連続した休符の数を数える(単位小節分のみ)
		/// </summary>
		/// <param name="data">チェックする音符リスト</param>
		/// <param name="i">開始要素番号</param>
		/// <returns>休符の数</returns>
		public int CheckRestNum(Score[] data, int i)
		{
			int cnt = 0;	// 休符の数
			int max = NotePerBar - i%NotePerBar;	// チェックする最大数 単位小節を超えないように調整

			while(cnt < max)
			{
				// 休符以外の要素を発見したら
				if( data[i+cnt].code[0] != -1) return cnt;
				cnt++;
			}
			return cnt;
		}
		
		
		/// <summary>
		/// リスト内に指定した動物がいくつ含まれているかチェックするメソッド
		/// </summary>
		/// <param name="mode"></param>
		/// <returns>含まれていた数</returns>
		public int CheckAnimalNumber(muphic.ScoreScr.AnimalScoreMode mode)
		{
			int num=0;
			for(int i=0; i<parent.AnimalList.Count; i++)
			{
				Animal a = ((Animal)parent.AnimalList[i]);
				if(a.AnimalName.Equals(mode.ToString())) num++;
			}
			return num;
		}

		#endregion

		/// <summary>
		/// 上スクロールボタンを押した際の動作
		/// </summary>
		public void UpScroll()
		{
			// １行目より上には行かないようにする
			if(this.offset >= 32) this.offset -= 32;

			// そして再描画
			this.ReDraw();
		}
		

		/// <summary>
		/// 下スクロールボタンを押した際の動作
		/// </summary>
		public void DownScroll()
		{
			if(this.nowScore != AnimalScoreMode.All)
			{
				// フルスコアでは無い場合、楽譜が７行以上になった時のみ下にスクロール可
				if(this.MaxNote > 192 && this.offset < this.MaxNote-32) this.offset += 32;
			}
			else
			{
				// フルスコアの場合
				if(this.offset < this.MaxNote-32) this.offset += 32;
			}

			// そして再描画
			this.ReDraw();
		}
		

		/// <summary>
		/// オフセットをクリアするメソッド
		/// 何となくprivateにしたかったんで
		/// </summary>
		public void ClearOffset()
		{
			this.offset = 0;
		}
		
		#region デバッグ用メソッド群
		
		/// <summary>
		/// 音符リストをチェックする ってか一覧を表示する
		/// 主にデバッグ用
		/// </summary>
		/// <param name="data">一覧を表示する音符リスト</param>
		/// <param name="length">リストの長さ</param>
		public void CheckScoreList(Score[] data, int length)
		{
			int i=0;

			for(i=0; i<length; ++i)
			{
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data[i].length, data[i].code[0], data[i].code[1], data[i].code[2]);
			}
		}
		public void CheckScoreList(ArrayList list)
		{
			int i=0;

			for(i=0; i<list.Count; ++i)
			{
				Score data = (Score)list[i];
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data.length, data.code[0], data.code[1], data.code[2]);
			}
		}
		
		
		/// <summary>
		/// デバッグ用メッセージ出力メソッド
		/// </summary>
		/// <param name="str">何らかの文字列</param>
		/// <param name="num">何らかの値</param>
		public void Debug(String str, int num)
		{
			System.Console.WriteLine(str+num);
		}
		/// <summary>
		/// デバッグ用メッセージ出力メソッド
		/// </summary>
		/// <param name="str">何らかの文字列</param>
		/// <param name="num">何らかの値</param>
		public void Debug(String str, double num)
		{
			System.Console.WriteLine(str+num);
		}

		#endregion
		
	}
	*/
	#endregion

	#region XGA 印刷機能付加 (ver.2.0.0～)

	/// <summary>
	/// Score の概要の説明です。
	/// </summary>
	public class ScoreMain : Screen
	{
		public ScoreScreen parent;
		public const int maxAnimals = 100;	// 配置可能な動物の数
		private int offset;					// 描画開始の音符を32ずつ(１行分ずつ)ずらす

		// 各動物ごとの音符リスト
		public ArrayList SheepScoreList = new ArrayList();
		public ArrayList RabbitScoreList = new ArrayList();
		public ArrayList BirdScoreList = new ArrayList();
		public ArrayList DogScoreList = new ArrayList();
		public ArrayList PigScoreList = new ArrayList();
		public ArrayList CatScoreList = new ArrayList();
		public ArrayList VoiceScoreList = new ArrayList();

		public int MaxNote;					// 全音符リストの音符数(8で割ると小節数、32で割ると行数になる)
		public AnimalScoreMode nowScore;	// 現在表示している音符リスト

		public ArrayList DrawList = new ArrayList();	// 描画するデータのリスト| _-)/

		# region 各種サイズ定義
		const int NotePerBar = 8;							// 単位小節あたりの最大音符数 ※変更すると動作できないと思う
		const int BarPerLine = 4;							// 単位行あたりの小節数
		const int NotePerLine = NotePerBar * BarPerLine;	// 単位行あたりの最大音符数
		const int LinePerPage = 7;							// 画面に表示できる五線譜の最大行数
		const int NotePerPage = NotePerLine * LinePerPage;	// 画面に表示できる音符の最大数
		const int MaxChord = 3;								// 和音最大数
		const int NoteXBegin = 189;							// 音符x座標の基準(行1番目の音符のx座標)(px ※XGA修正済み)
		const int NoteYBegin = 141;							// 音符y座標の基準(px)
		const int NoteXDifference = 21;						// 音符同士のx座標の差(※XGA修正済み)
		const int BarXDifference = 181;						// 小節同士のx座標の差(px ※XGA修正済み)
		const int ScoreXBegin = 122;						// 五線譜x座標の基準(五線譜のx座標)(px ※XGA修正済み)
		const int ScoreYBegin = 173;						// 五線譜y座標の基準(1行目の五線譜のy座標)(px ※XGA修正済み)
		const int ScoreYDifference = 67;					// 五線譜同士のy座標の差(px ※XGA修正済み)
		const int EndXBegin = 906;							// 終端線のx座標(px ※XGA修正済み)

		const int BigImageX = 600;			// おまけ画像x座標
		const int BigImageY = 450;			// おまけ画像y座標
		# endregion

		/// <summary>
		/// ScoreMain コンストラクタ
		/// </summary>
		/// <param name="screen">上位画面クラス</param>
		public ScoreMain(ScoreScreen screen)
		{
			this.parent = screen;
			this.offset = 0;

			// つなげて音楽時は羊ボタン以外は表示させない
			if (this.parent.ParentScreenMode == muphic.ScreenMode.LinkScreen || this.parent.ParentScreenMode == muphic.ScreenMode.LinkMakeScreen)
			{
				this.nowScore = AnimalScoreMode.Sheep;
				this.parent.scores.sheep.State = 1;

				this.parent.scores.all.Visible = false;
				this.parent.scores.bird.Visible = false;
				this.parent.scores.cat.Visible = false;
				this.parent.scores.dog.Visible = false;
				this.parent.scores.pig.Visible = false;
				this.parent.scores.rabbit.Visible = false;
				this.parent.scores.voice.Visible = false;
			}
			else
			{
				this.nowScore = AnimalScoreMode.All;
				this.parent.scores.all.State = 1;
			}

			#region 画像のﾄｳﾛｰｸ ( ﾟ∀ﾟ)ﾉ
			DrawManager.Regist("QuarterNotes", 0, 0, "image\\ScoreXGA\\note\\4buonpu.png");			// 四分音符-符幹上
			DrawManager.Regist("QuarterNotes_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_.png");		// 四分音符-符幹下
			DrawManager.Regist("QuarterNotes_do", 0, 0, "image\\ScoreXGA\\note\\4buonpu_do.png");	// 四分音符-ド
			DrawManager.Regist("QuarterNotes_wa", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa.png");	// 四分音符-符幹上和音用
			DrawManager.Regist("QuarterNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\4buonpu_wa_.png");	// 四分音符-符幹下和音用
			DrawManager.Regist("EighthNotes", 0, 0, "image\\ScoreXGA\\note\\8buonpu.png");			// 八分音符-符幹上
			DrawManager.Regist("EighthNotes_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_.png");		// 八分音符-符幹下
			DrawManager.Regist("EighthNotes_do", 0, 0, "image\\ScoreXGA\\note\\8buonpu_do.png");	// 八分音符-ド
			DrawManager.Regist("EighthNotes_wa", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa.png");	// 八分音符-符幹上和音用
			DrawManager.Regist("EighthNotes_wa_", 0, 0, "image\\ScoreXGA\\note\\8buonpu_wa_.png");	// 八分音符-符幹下和音用

			DrawManager.Regist("AllRest", 0, 0, "image\\ScoreXGA\\note\\zenkyuhu.png");				// 全休符/二分休符
			DrawManager.Regist("PHalfRest", 0, 0, "image\\ScoreXGA\\note\\2bukyuhu_huten.png");		// 付点二分休符
			DrawManager.Regist("PQuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu_huten.png");	// 付点四分休符
			DrawManager.Regist("QuarterRest", 0, 0, "image\\ScoreXGA\\note\\4bukyuhu.png");			// 四分休符
			DrawManager.Regist("EighthRest", 0, 0, "image\\ScoreXGA\\note\\8bukyuhu.png");			// 八分休符

			DrawManager.Regist("Meter", 0, 0, "image\\ScoreXGA\\note\\hyousi.png");					// 4/4拍子記号
			DrawManager.Regist("End", 0, 0, "image\\ScoreXGA\\note\\end.png");						// 終端
			DrawManager.Regist("End_full", 0, 0, "image\\ScoreXGA\\note\\end_full.png");			// 終端-フルスコア用
			DrawManager.Regist("Staff", 0, 0, "image\\ScoreXGA\\score\\gosen.png");					// 五線譜
			DrawManager.Regist("Line", 0, 0, "image\\ScoreXGA\\score\\syousetu.png");				// 小節区切り線
			DrawManager.Regist("Full", 0, 0, "image\\ScoreXGA\\score\\full_line.png");				// フルスコア

			DrawManager.Regist("TieTop", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t.png");			// タイ(上)
			DrawManager.Regist("TieTopHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h1.png");	// タイ(上・始端)
			DrawManager.Regist("TieTopHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_t_h2.png");	// タイ(上・終端)
			DrawManager.Regist("TieUnder", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u.png");			// タイ(下)
			DrawManager.Regist("TieUnderHalf1", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h1.png");	// タイ(下・始端)
			DrawManager.Regist("TieUnderHalf2", 0, 0, "image\\ScoreXGA\\note\\tie\\tie_u_h2.png");	// タイ(下・終端)

			DrawManager.Regist("SheepBig", 0, 0, "image\\ScoreXGA\\omake\\Sheep_big.png");			// 背景ﾋﾂｼﾞﾝ
			DrawManager.Regist("RabbitBig", 0, 0, "image\\ScoreXGA\\omake\\Rabbit_big.png");		// 背景兎
			DrawManager.Regist("BirdBig", 0, 0, "image\\ScoreXGA\\omake\\Bird_big.png");			// 背景鳥バード
			DrawManager.Regist("DogBig", 0, 0, "image\\ScoreXGA\\omake\\Dog_big.png");				// 背景Dog
			DrawManager.Regist("PigBig", 0, 0, "image\\ScoreXGA\\omake\\Pig_big.png");				// 背景ベイブ
			DrawManager.Regist("CatBig", 0, 0, "image\\ScoreXGA\\omake\\Cat_big.png");				// 背景ぬこ
			DrawManager.Regist("VoiceBig", 0, 0, "image\\ScoreXGA\\omake\\Voice_big.png");			// 背景う゛ぉいす

			DrawManager.Regist("Logo", 730, 35, "image\\ScoreXGA\\logo.png");						// 印刷用ロゴ
			#endregion

			// 値の初期化と音符リストのｾｲｾｰｲ ( ﾟ∀ﾟ)ﾉ
			this.CreateScoreListAll();

			// 楽譜をﾋﾞｮｳｰｶﾞ ( ﾟ∀ﾟ)ﾉ
			this.ReDraw();
		}


		/// <summary>
		/// 印刷を行うメソッド
		/// </summary>
		public void Print()
		{
			int page = 0;	// 画像＆文字列をRegistする際のページ
			int allpage;	// 印刷する総ページ数

			if (this.nowScore == muphic.ScoreScr.AnimalScoreMode.All)
			{
				// フルスコア時は、最大音符数から1行分の音符数を割った数が総ページ数になる
				allpage = (int)System.Math.Ceiling((double)this.MaxNote / NotePerLine);
			}
			else
			{
				// フルスコア以外の時は、最大音符数から1ページ分の音符数を割った数が総ページ数になる
				allpage = (int)System.Math.Ceiling((double)this.MaxNote / NotePerPage);
			}

			// 現在のオフセットをバックアップ
			int offset_backup = this.offset;

			// オフセットを０にする
			this.offset = 0;

			do
			{
				// オフセット変更後なので描画リスト更新
				this.ReDraw();

				// 印刷のページをセット
				muphic.PrintManager.ChangePage(++page);

				// 背景のセット
				muphic.PrintManager.Regist(parent.scorewindow.ToString(), 1);
				muphic.PrintManager.Regist("Logo");

				// Draw時と同じ要領で、印刷する画像を登録していく
				for (int i = 0; i < this.DrawList.Count; i++)
				{
					DrawData data = (DrawData)DrawList[i];
					muphic.PrintManager.Regist(data.Image, data.x, data.y);
				}

				// ページ文字列を登録
				muphic.PrintManager.RegistString("ページ " + page.ToString() + " / " + allpage.ToString(), 830, 690, 16);

				// タイトルの登録
				string title = this.parent.tarea.ScoreTitle;						// 題名表示領域から曲名を持ってくる
				if (title == null || title == "") title = "「あたらしいきょく」";	// 保存ダイアログのタイトルが無題ならば、こっちで勝手に決める
				else title = "「" + title + "」";									// 『』をつける
				muphic.PrintManager.RegistString("だいめい", 40, 40, 14);
				muphic.PrintManager.RegistString(title, 65, 70, 24);

				// フルスコア時は下へのスクロールが必要
				// 可能な限り下へスクロールしていき、その都度新しいページに印刷していく
			}
			while (this.DownScroll());

			// 印刷開始
			muphic.PrintManager.Print(false);
			System.Console.WriteLine("楽譜印刷 " + allpage.ToString() + "ページ");

			// オフセットを元に戻す
			this.offset = offset_backup;
			this.ReDraw();
		}


		/// <summary>
		/// 描画メソッド
		/// 描画リストに登録された画像と座標を読み出すだけなんで重くないハズ
		/// </summary>
		public override void Draw()
		{
			base.Draw();

			// 描画リストから描画するデータを読み出す
			for (int i = 0; i < this.DrawList.Count; i++)
			{
				DrawData data = (DrawData)DrawList[i];
				muphic.DrawManager.Draw(data.Image, data.x, data.y);
			}
		}

		/// <summary>
		/// 描画リストを再生成するメソッド
		/// </summary>
		public void ReDraw()
		{
			DrawData drawdata;

			// 既存の描画リストをクリアする
			this.DrawList.Clear();

			#region DrawScoreメソッドより描画リストを再生成する
			switch (this.nowScore)
			{
				case AnimalScoreMode.All:
					this.DrawScore(this.SheepScoreList, 1, 0);
					this.DrawScore(this.RabbitScoreList, 1, ScoreYDifference);
					this.DrawScore(this.BirdScoreList, 1, ScoreYDifference * 2);
					this.DrawScore(this.DogScoreList, 1, ScoreYDifference * 3);
					this.DrawScore(this.PigScoreList, 1, ScoreYDifference * 4);
					this.DrawScore(this.CatScoreList, 1, ScoreYDifference * 5);
					this.DrawScore(this.VoiceScoreList, 1, ScoreYDifference * 6);
					this.DrawAll();		// フルスコア専用画像の描画
					break;
				case AnimalScoreMode.Sheep:
					drawdata = new DrawData("SheepBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.SheepScoreList, LinePerPage, 0);		// 描画リストにﾋﾂｼﾞﾝの楽譜をセット
					break;
				case AnimalScoreMode.Rabbit:
					drawdata = new DrawData("RabbitBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.RabbitScoreList, LinePerPage, 0);		// 描画リストに兎の楽譜をセット
					break;
				case AnimalScoreMode.Bird:
					drawdata = new DrawData("BirdBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.BirdScoreList, LinePerPage, 0);			// 描画リストに鳥バードの楽譜をセット
					break;
				case AnimalScoreMode.Dog:
					drawdata = new DrawData("DogBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.DogScoreList, LinePerPage, 0);			// 描画リストに犬Dogの楽譜をセット
					break;
				case AnimalScoreMode.Pig:
					drawdata = new DrawData("PigBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.PigScoreList, LinePerPage, 0);			// 描画リストにベイブの楽譜をセット
					break;
				case AnimalScoreMode.Cat:
					drawdata = new DrawData("CatBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.CatScoreList, LinePerPage, 0);			// 描画リストにぬこの楽譜をセット
					break;
				case AnimalScoreMode.Voice:
					drawdata = new DrawData("VoiceBig", BigImageX, BigImageY); this.DrawList.Add(drawdata);
					this.DrawScore(this.VoiceScoreList, LinePerPage, 0);		// 描画リストにｳﾞｫｲｽの楽譜をセット
					break;
				default:
					break;
			}
			#endregion
		}

		/// <summary>
		/// 全ての音符リストを生成するメソッド
		/// </summary>
		public void CreateScoreListAll()
		{
			int max;
			try
			{
				// 動物リストの最後の動物の位置から、全音符リストにおける最長の位置を得る
				Animal animal = ((Animal)parent.AnimalList[parent.AnimalList.Count - 1]);
				max = animal.place;
			}
			catch (Exception) // 動物0匹の状態で呼び出すと範囲外参照が発生してしまうため無理矢理…
			{
				max = 0;
			}

			// その値から、音符リストの音符数を算出(32の倍数になるよう調整)
			this.MaxNote = max + (NotePerLine - max % NotePerLine);

			// 各音符リストを初期化
			this.SheepScoreList.Clear();
			this.RabbitScoreList.Clear();
			this.BirdScoreList.Clear();
			this.DogScoreList.Clear();
			this.PigScoreList.Clear();
			this.CatScoreList.Clear();
			this.VoiceScoreList.Clear();

			this.CheckScoreList(this.SheepScoreList);

			// 音符リストのｾｲｾｰｲ ( ﾟ∀ﾟ)ﾉ
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Sheep);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Rabbit);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Bird);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Dog);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Pig);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Cat);
			this.CreateScoreList(muphic.ScoreScr.AnimalScoreMode.Voice);
		}

		#region 楽譜描画リスト生成メソッド群

		/// <summary>
		/// 描画リストDrawListに楽譜データを登録するメソッド
		/// </summary>
		/// <param name="data">描画する音符リスト</param>
		/// <param name="mode">描画する行数</param>
		/// <param name="yoffset">フルスコア用 y座標オフセット</param>
		public void DrawScore(ArrayList data, int mode, int yoffset)
		{
			int i = this.offset, j, end;
			Point p = new Point(0, 0);
			String filename;
			DrawData drawdata;

			// ループ終了条件の設定
			end = i + mode * NotePerLine;
			if (end > NotePerPage) end = NotePerPage;	// ただし６行分を超えた場合は６行までとする
			if (end > data.Count) end = data.Count;		// さらに音符リストの音符数を超えた場合は音符数にする

			yoffset -= (this.offset / NotePerLine) * ScoreYDifference;

			// offsetからスタート
			while (i < end)
			{
				Score score = (Score)data[i];

				// 行の最初の小節の描画時に五線譜も描画する
				if (i % NotePerLine == 0)
				{
					// y座標は1行目134px＋行数×67px
					drawdata = new DrawData("Staff", ScoreXBegin, ScoreYBegin + (int)(Math.Floor(0.0 + i / NotePerLine) * ScoreYDifference) + yoffset);
					DrawList.Add(drawdata);	// 描画リストに追加

					if (i / NotePerLine == 0)
					{
						// さらに、1行目では4/4拍子の描画
						drawdata = new DrawData("Meter", ScoreXBegin + 40, ScoreYBegin + 13 + (int)(Math.Floor(0.0 + i / NotePerLine) * ScoreYDifference) + yoffset);
						DrawList.Add(drawdata);	// 描画リストに追加
					}
				}

				// 音符描画
				for (j = 0; j < 3; j++)
				{
					// 休符の和音は在りませぬ 和音２つ目以降に音符がなければ次に進むようにする
					if ((j == 1 && score.code[1] == -1) || (j == 2 && score.code[2] == -1)) break;

					p = getScoreCoordinate(score, j, i);	// 座標ゲッツ☆
					drawdata = new DrawData(getScoreImage(score, j), p.X, p.Y + yoffset);
					DrawList.Add(drawdata);	// 描画リストに追加
				}

				#region タイの描画

				if (score.tie == 1)
				{
					if (i % NotePerLine != NotePerLine - 1)
					{
						// 行の最後でなかった場合、普通にタイ画像を描画

						// 符幹の向きでタイの位置も変わる
						if (this.CheckCodeDirection(score) == 0)
						{
							// 符幹の向きが上だった場合、タイ画像は音符の下に描画する
							p = getScoreCoordinate(score, score.chord - 1, i);	// まず音符の座標を得る
							p.X += 2;											// 得た音符の座標を基準にして
							p.Y += 34;											// 画像の位置を調節する
							filename = "TieUnder";								// 画像ファイル名の設定
						}
						else
						{
							// 符幹の向きが下だった場合、タイ画像は音符の上に描画する
							p = getScoreCoordinate(score, 0, i);	// まず音符の座標を得る
							p.X += 3;								// 得た音符の座標を基準にして
							p.Y -= 10;								// 画像の位置を調節する
							filename = "TieTop";					// 画像ファイル名の設定
						}
					}
					else
					{
						// 行の最後だった場合、前半(始端側)のみ描画

						// 符幹の向きでタイの位置も変わる
						if (this.CheckCodeDirection(score) == 0)
						{
							// 符幹の向きが上だった場合、タイ画像は音符の下に描画する
							p = getScoreCoordinate(score, score.chord - 1, i);	// まず音符の座標を得る
							p.X += 2;											// 得た音符の座標を基準にして
							p.Y += 34;											// 画像の位置を調節する
							filename = "TieUnderHalf1";							// 画像ファイル名の設定
						}
						else
						{
							// 符幹の向きが下だった場合、タイ画像は音符の上に描画する
							p = getScoreCoordinate(score, 0, i);	// まず音符の座標を得る
							p.X += 3;								// 得た音符の座標を基準にして
							p.Y -= 10;								// 画像の位置を調節する
							filename = "TieTopHalf1";				// 画像ファイル名の設定

						}
					}

					// そして描画リストに追加
					drawdata = new DrawData(filename, p.X, p.Y + yoffset);
					DrawList.Add(drawdata);
				}
				else if ((score.tie == 2) && (i % NotePerLine == 0))
				{
					// ２行に渡ったタイの後半(終端側)の描画

					// 符幹の向きでタイの位置も変わる
					if (this.CheckCodeDirection(score) == 0)
					{
						// 符幹の向きが上だった場合、タイ画像は音符の下に描画する
						p = getScoreCoordinate(score, score.chord - 1, i);	// まず音符の座標を得る
						p.X -= 12;											// 得た音符の座標を基準にして
						p.Y += 34;											// 画像の位置を調節する
						filename = "TieUnderHalf2";							// 画像ファイル名の設定
					}
					else
					{
						// 符幹の向きが下だった場合、タイ画像は音符の上に描画する
						p = getScoreCoordinate(score, 0, i);	// まず音符の座標を得る
						p.X -= 12;								// 得た音符の座標を基準にして
						p.Y -= 10;								// 画像の位置を調節する
						filename = "TieTopHalf2";				// 画像ファイル名の設定	
					}

					// そして描画リストに追加
					drawdata = new DrawData(filename, p.X, p.Y + yoffset);
					DrawList.Add(drawdata);
				}

				#endregion

				// 音符の長さ分先に進める
				i += (int)(score.length * 2);
			}

			// 終端の描画 iが最大音符数だった場合、楽譜の終端であるため
			if (i == this.MaxNote)
			{
				// 行数*67加えて描画
				if (this.nowScore != muphic.ScoreScr.AnimalScoreMode.All)
				{
					drawdata = new DrawData("End", EndXBegin, ScoreYBegin + 12 + (int)(Math.Floor(0.0 + i / NotePerLine) - 1) * ScoreYDifference);
					DrawList.Add(drawdata);	// 描画リストに追加
				}
			}
		}


		/// <summary>
		/// フルスコア時の描画
		/// </summary>
		public void DrawAll()
		{
			// フルスコア時はまとめるカッコを描画する
			DrawData drawdata = new DrawData("Full", 64, 174);
			DrawList.Add(drawdata);	// 描画リストに追加

			// オフセット+32が音符数だった場合、楽譜の終端と判断
			if (this.offset + NotePerLine == this.MaxNote)
			{
				drawdata = new DrawData("End_full", EndXBegin + 1, ScoreYBegin + 12);
				DrawList.Add(drawdata);	// 描画リストに追加
			}
		}


		/// <summary>
		/// 与えられた音符の表示位置を決定するメソッド
		/// </summary>
		/// <param name="score">位置を決定する音符</param>
		/// <param name="num">和音何番目の音符か</param>
		/// <param name="i">音符リスト何番目の音符か</param>
		/// <returns>表示位置座標</returns>
		public Point getScoreCoordinate(Score score, int num, int i)
		{
			Point p = new Point(0, 0);
			int line = i / NotePerLine;	// 何行目の音符なのか
			int n = i % NotePerLine;		// 行内の何番目の音符なのか

			if (score.code[0] == -1)
			{
				// 休符の場合
				if (score.length == 0.5) p.Y = ScoreYBegin + 20 + line * ScoreYDifference;			// 八分休符
				else if (score.length == 1) p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// 四分休符
				else if (score.length == 1.5) p.Y = ScoreYBegin + 14 + line * ScoreYDifference;		// 付点四分休符
				else if (score.length == 2) p.Y = ScoreYBegin + 24 + line * ScoreYDifference;		// 二分休符
				else if (score.length == 3) p.Y = ScoreYBegin + 21 + line * ScoreYDifference;		// 付点二分休符
				else if (score.length == 4) p.Y = ScoreYBegin + 19 + line * ScoreYDifference;		// 全休符
			}
			else
			{
				// 休符じゃない場合 音階ごとにy座標を変える
				p.Y = ScoreYBegin - 3 + score.code[num] * 4 - 4 + line * ScoreYDifference;

				if (this.CheckCodeDirection(score) == 1)
				{
					// シより上の音だったら符幹が下の音符になる
					p.Y += 22;
					p.X += 3;

					//  隣り合わせの和音でズレた音符だった場合 左にずらす
					if (this.CheckChordMode(score, num)) p.X -= 8;

					// ズレた和音ならば更に右へ少しずらしてバランス調整
					int j = 0;
					for (; j < score.chord; j++) if (this.CheckChordMode(score, j)) break;
					if (j != score.chord) p.X += 5;
				}
				else
				{
					// 符幹が上(通常)で隣り合わせの和音でズレた音符だった場合 右にずらす
					if (this.CheckChordMode(score, num)) p.X += 8;
				}
			}

			// 音符のx座標を決定
			// 1番目の音符座標189px + 小節数(0～3)*181px + 小節内の音符番(0～7)*16px
			p.X += NoteXBegin + (n / NotePerBar * BarXDifference) + (n % NotePerBar * NoteXDifference);

			// 八分以外の場合はそれぞれx座標加えてバランス調整
			if (score.length == 1) p.X += (int)Math.Round(NoteXDifference / 2.0, 0);
			else if (score.length == 1.5) p.X += (int)Math.Round((NoteXDifference / 2.0) * 2, 0);
			else if (score.length == 2) p.X += (int)Math.Round((NoteXDifference / 2.0) * 3, 0);
			else if (score.length == 3) p.X += (int)Math.Round((NoteXDifference / 2.0) * 5, 0);
			else if (score.length == 4) p.X += (int)Math.Round((NoteXDifference / 2.0) * 7, 0);

			// 下のドだった場合少し左にずらす
			if (score.code[num] == 8) p.X -= 3;

			// 更に、２行目以降の１小節目の場合 1pxずつ左にずらして4/4拍子の空白部分を埋める
			if ((line != 0) && (n < NotePerBar)) p.X -= (NotePerBar - n) * 2;

			return p;
		}


		/// <summary>
		/// 与えられた音符の画像を決定するメソッド
		/// </summary>
		/// <param name="score">画像を決定する音符</param>
		/// <param name="num">和音何番目の音符か</param>
		/// <returns>表示画像文字列</returns>
		public String getScoreImage(Score score, int num)
		{
			// 和音一つ目の音符(code[0])が休符だった場合
			if (num == 0 && score.code[0] == -1)
			{
				// 八分休符に決定
				if (score.length == 0.5) return "EighthRest";

				// 四分休符に決定
				if (score.length == 1) return "QuarterRest";

				// 付点四分休符に決定
				if (score.length == 1.5) return "PQuarterRest";

				// 付点二分休符に決定
				if (score.length == 3) return "PHalfRest";

				// それ以外は二分休符か全休符
				return "AllRest";
			}

			if (score.length == 0.5)
			{
				// 八分音符

				if (num == 0 && this.CheckCodeDirection(score) == 0)
				{
					// 符幹上の場合、八分の画像は和音一番上でしか使わない

					// 下のドだったらドの音符にする
					if (score.code[num] == 8) return "EighthNotes_do";

					// 隣り合わせの和音だったらズレた符幹下音符
					if (this.CheckChordMode(score, num)) return "EighthNotes_wa";

					// 条件にかからなければ普通の八分音符
					return "EighthNotes";
				}
				if (num == score.chord - 1 && this.CheckCodeDirection(score) == 1)
				{
					// 符幹下の場合、八分の画像は和音一番下でしか使わない

					// 隣り合わせの和音だったらズレた符幹下音符
					if (this.CheckChordMode(score, num)) return "EighthNotes_wa_";

					// 条件にかからなければ普通の八分音符
					return "EighthNotes_";
				}
			}

			// それ以外は四分音符になりまする

			// 下のドだったらドの音符にする
			if (score.code[num] == 8) return "QuarterNotes_do";

			// シより上の音で構成されている場合は符幹が下になる
			if (this.CheckCodeDirection(score) == 1)
			{
				// 隣り合わせの和音だったらズレた符幹下音符
				if (this.CheckChordMode(score, num)) return "QuarterNotes_wa_";

				// それ以外は普通の符幹下音符
				return "QuarterNotes_";
			}

			// 隣り合わせの和音だったらズレた符幹下音符
			if (this.CheckChordMode(score, num)) return "QuarterNotes_wa";

			// 条件にかからなければ普通の四分音符
			return "QuarterNotes";
		}


		/// <summary>
		/// 与えられた音符の符幹の向きを決めるメソッド
		/// </summary>
		/// <param name="score">対象の音符(和音含め)</param>
		/// <returns>
		/// 符幹の向き
		/// -1:休符
		/// 0:上(通常)
		/// 1:下(シ・ドのみの音/和音)
		/// </returns>
		public int CheckCodeDirection(Score score)
		{
			// そもそも休符だったら
			if (score.code[0] == -1) return -1;

			// 符幹が下になる条件は、シ・ド(高い方)であること
			for (int i = 0; i < score.code.Length; i++)
			{
				// 和音内にシより下の音が合ったら 符幹は上(通常)になる
				if (score.code[i] > 2) return 0;
			}

			// 上のループで引っかからなければ符幹は下になる
			return 1;
		}


		/// <summary>
		/// 隣り合わせの和音を判定するメソッド
		/// </summary>
		/// <param name="score"></param>
		/// <param name="num"></param>
		/// <returns>ズレた音符ならtrue</returns>
		public bool CheckChordMode(Score score, int num)
		{
			// 和音じゃなかったらｶｴﾚ!
			if (score.chord == 1) return false;

			if (this.CheckCodeDirection(score) == 0)
			{
				// 符幹の向きが上(通常)ならば

				// 和音一番下の音はズレた音符にはならない
				if (num == 2) return false;

				// 和音数２の場合
				if (score.chord == 2)
				{
					// 和音の上の音で隣り合わせだったらズレた音符になる
					if ((num == 0) && (score.code[1] - score.code[0] == 1)) return true;

					// それ以外は普通の音符
					return false;
				}

				// 和音数３の場合
				if (score.chord == 3)
				{
					// ３つの音全部隣り合わせだった場合
					if ((score.code[2] - score.code[1] == 1) && (score.code[1] - score.code[0] == 1))
					{
						// 真ん中の音であればズレた音符になる
						if (num == 1) return true;

						// それ以外は普通の音符
						return false;
					}

					// 和音の上の音で隣り合わせだったらズレた音符になる
					if ((num == 0) && (score.code[0] - score.code[1] == 1)) return true;

					// 和音２番目の音で、上下と隣りあわせだったらそれぞれズレた音符になる
					if ((num == 1) && (score.code[1] - score.code[0] == 1)) return true;
					if ((num == 1) && (score.code[2] - score.code[1] == 1)) return true;

					// それ以外は普通の音符
					return false;
				}
			}
			else if (this.CheckCodeDirection(score) == 1)
			{
				// 符幹が向きが下(シとド)の場合
				// 和音数が２のシとド以外ありえない

				// 和音２番目の音で、上と隣り合わせだったらズレた音符になる
				if ((num == 1) && (score.code[1] - score.code[0] == 1)) return true;

				// それ以外は全て普通の音符
				return false;
			}

			// あと全部ｶｴﾚ!
			return false;
		}


		#endregion

		#region 音符リスト生成メソッド群

		/// <summary>
		/// AnimalListからそれぞれの動物の音符リストを作る
		/// </summary>
		/// <param name="animalmode">リスト作成の対象の動物</param>
		public void CreateScoreList(muphic.ScoreScr.AnimalScoreMode animalmode)
		{
			int i, n;
			int temp = -1;	// 和音カウント用変数
			Animal animal = new Animal(0, 0);
			Score[] ScoreList = new Score[this.MaxNote];
			for (i = 0; i < this.MaxNote; i++) ScoreList[i] = new Score();
			i = n = 0;

			// 動物リストに含まれている対象の動物の数を取得
			int num = this.CheckAnimalNumber(animalmode);

			#region 音符リスト生成

			// 0番目の動物のデータを取得 対象の動物にヒットするまでデータ取得し続ける 
			if (num > 0)
			{
				animal = ((Animal)parent.AnimalList[n]);
				while (!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);
			}

			// 音符リスト作成メインループ
			while (num > 0)
			{
				// 楽譜位置iと動物位置placeが一致し、かつ対象の動物であるかどうか 
				if (i == animal.place && animal.AnimalName.Equals(animalmode.ToString()))
				{
					// 上記条件を満たした場合、音階を楽譜にコピー
					ScoreList[i].AddCode(animal.code);

					// 楽譜位置iが前と同じだった場合、和音と判定
					if (temp == i) ScoreList[i].chord++;
					temp = i;

					// さらに、リスト内の対象動物数を１減らす
					// リスト内の対象動物数が0になったらループ終了
					if (--num == 0) break;

					// 次の動物データを取得 対象の動物にヒットするまでデータ取得し続ける
					animal = ((Animal)parent.AnimalList[++n]);
					while (!animal.AnimalName.Equals(animalmode.ToString())) animal = ((Animal)parent.AnimalList[++n]);

					// 次の動物が次の楽譜位置に無い場合、四分音符にする
					if (animal.place > i + 1) ScoreList[i].length = 1;

					// 和音数が最大値になったら強制的にiを進める
					if (ScoreList[i].chord >= MaxChord) { i++; temp = 0; }
				}
				else
				{
					// 条件を満たさなかった場合はiを進める
					i++;
					temp = 0;
				}
			}

			// 最後の音符を四分にする(もし行の最後の音符だったら、タイだけでもう1行ってのもアレなんで無視しちゃいましょう)
			//if( (i % NotePerLine != NotePerLine-1) && ScoreList[i].code[0] != -1 ) ScoreList[i].length = 1;
			if ((i % this.MaxNote != this.MaxNote - 1) && ScoreList[i].code[0] != -1) ScoreList[i].length = 1;

			#endregion

			#region タイ・休符の判定 旧式(～ ver.0.10.1)
			/*
			i = -1;
			while(++i < this.MaxNote)
			{
				// 休符ではなかった場合
				if(ScoreList[i].code[0] != -1)
				{
					// タイの判定
					// 四分音符だったら
					if( (ScoreList[i].length >= 1) && (i%NotePerBar == NotePerBar-1) )
					{
						ScoreList[i+1].length = ScoreList[i].length - 0.5;	// 次の音符の長さを八分に
						ScoreList[i+1].code[0] = ScoreList[i].code[0];		// 音符をコピー
						ScoreList[i+1].code[1] = ScoreList[i].code[1];		// 音符をコピー
						ScoreList[i+1].code[2] = ScoreList[i].code[2];		// 音符をコピー
						ScoreList[i].length = 0.5;							// 音符の長さを八分に
						ScoreList[i].tie = true;							// タイフラグon
					}

					// iが休符でなければ即次へ
					i += (int)(ScoreList[i].length * 2) - 1;
					continue;
				}

				// 全休符判定
				// 小節の最初の音符で判定を実施
				if( (i%NotePerBar == NotePerBar-4*2) && CheckRest(ScoreList, i, 8) )
				{
					ScoreList[i].length = 4;	// 小節内のすべての音符が休符だったら、全休符にする
					i += 7; continue;			// 一小節分先に進める
				}

				// 付点二分休符判定
				// 小節内の7つ目以降の音符では判定する必要がない
				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-3*2) && CheckRest(ScoreList, i, 6) )
				{
					ScoreList[i].length = 3;	// 付点二分休符
					i += 5; continue;			// 一分先に進める
				}
				
				// 二分休符判定
				// 小節内の5つ目以降の音符では判定する必要がない
				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-2*2) && CheckRest(ScoreList, i, 4) )
				{
				
					ScoreList[i].length = 2;	// 二分休符
					i += 3; continue;			// 二分先に進める
				}

				// 付点四分休符判定
				// 小節内の6つ目以降の音符では判定する必要がない
				if( (i%NotePerBar <= NotePerBar-1.5*2) && CheckRest(ScoreList, i, 3) )
				{
				
					ScoreList[i].length = 1.5;	// 付点四分休符
					i += 2; continue;			// 三分先に進める
				}

				// 四分休符判定
				// 小節内の7つ目以降の音符では判定する必要がない
				if( (i%2 == 0) && (i%NotePerBar <= NotePerBar-1*2) && CheckRest(ScoreList, i, 2) )
				{
					ScoreList[i].length = 1;	// 四分休符
					i += 1; continue;			// 四分先に進める
				}
				
				// 何れにも該当しなければ、8部休符となる
				//i += (int)(ScoreList[i].length * 2) - 1;
			}
			*/
			#endregion

			#region タイ・休符の判定 (ver.0.10.2 ～)

			i = -1;
			while (++i < this.MaxNote)
			{
				// 休符ではなかった場合
				if (ScoreList[i].code[0] != -1)
				{
					// タイの判定
					// 四分音符だったら
					if ((ScoreList[i].length >= 1) && (i % NotePerBar == NotePerBar - 1))
					{
						for (int j = 0; j < MaxChord; j++)
							ScoreList[i + 1].code[j] = ScoreList[i].code[j];	// 音符を
						ScoreList[i + 1].length = ScoreList[i].length - 0.5;	// 次の音符の長さを八分に
						ScoreList[i + 1].chord = ScoreList[i].chord;			// 和音情報のコピー
						ScoreList[i].length = 0.5;							// 音符の長さを八分に
						ScoreList[i].tie = 1;								// タイフラグon
						ScoreList[i + 1].tie = 2;								// タイフラグon
					}

					// iが休符でなければ即次へ
					i += (int)(ScoreList[i].length * 2) - 1;
					continue;
				}

				// iを含めた連続した休符の数 以下例
				//  restnum == 2 四分休符になれる
				//  restnum == 4 二分休符になれる
				//  restnum == 8 全休符になれる
				int restnum = this.CheckRestNum(ScoreList, i);

				switch (i % NotePerBar)
				{
					case 0:
						if (restnum == 8)
						{
							// 小節内が全て休符だった場合
							ScoreList[i].length = 4;	// 小節内のすべての音符が休符だったら、全休符にする
							i += NotePerBar - 1;			// 次の小節へ進める
							continue;
						}
						goto case 2;
					case 1:
						if (restnum >= 7)
						{
							ScoreList[i + 1].length = 3;	// 八分休符+付点二分休符
							i += 6;						// 次の小節へ進める
							continue;
						}
						goto case 2;
					case 2:
						if (restnum >= 6)
						{
							ScoreList[i].length = 3;	// 付点二分休符
							i += 5;						// 一分先に進める
							continue;
						}
						goto case 4;
					case 3:
						if (restnum >= 5)
						{
							ScoreList[i + 1].length = 2;	// 八分休符+二分休符
							i += 4;						// 次の小節へ進める
							continue;
						}
						goto case 4;
					case 4:
						if (restnum >= 4)
						{
							ScoreList[i].length = 2;	// 二分休符
							i += 3;						// 二分分先に進める
							continue;
						}
						goto case 5;
					case 5:
						if (restnum >= 3)
						{
							ScoreList[i].length = 1.5;	// 付点四分休符
							i += 2;						// 二分分先に進める
							continue;
						}
						goto case 6;
					case 6:
						if (restnum >= 2)
						{
							ScoreList[i].length = 1;	// 四分休符
							i += 1;						// 二分分先に進める
							continue;
						}
						goto case 7;
					case 7:
						break;	// 何れにも該当しなければ、八分休符となる
					default:
						// ここに到達する場合、単位小節あたりの最大音符数が8でない可能性が
						System.Console.WriteLine("NotePerBar != 8");
						break;
				}
			}
			#endregion

			#region 生成した配列のリストを用意された各動物ごとのフィールドにコピーする
			switch (animalmode)
			{
				case AnimalScoreMode.Sheep:
					for (i = 0; i < this.MaxNote; i++) this.SheepScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Rabbit:
					for (i = 0; i < this.MaxNote; i++) this.RabbitScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Bird:
					for (i = 0; i < this.MaxNote; i++) this.BirdScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Dog:
					for (i = 0; i < this.MaxNote; i++) this.DogScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Pig:
					for (i = 0; i < this.MaxNote; i++) this.PigScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Cat:
					for (i = 0; i < this.MaxNote; i++) this.CatScoreList.Add(ScoreList[i]);
					break;
				case AnimalScoreMode.Voice:
					for (i = 0; i < this.MaxNote; i++) this.VoiceScoreList.Add(ScoreList[i]);
					break;
				default:
					break;
			}
			#endregion
		}


		/// <summary>
		/// 指定された範囲が全て休符かどうかをチェックする
		/// </summary>
		/// <param name="data">チェックする音符リスト</param>
		/// <param name="i">開始要素番号</param>
		/// <param name="n">チェックする音符数</param>
		/// <returns>範囲の音符全て休符ならtrue そうでないならfalse</returns>
		public bool CheckRest(Score[] data, int i, int n)
		{
			int j = 0;
			for (; j < n; j++)
			{
				if (data[i + j].code[0] != -1) return false;
			}
			return true;
		}


		/// <summary>
		/// 指定された要素からの連続した休符の数を数える(単位小節分のみ)
		/// </summary>
		/// <param name="data">チェックする音符リスト</param>
		/// <param name="i">開始要素番号</param>
		/// <returns>休符の数</returns>
		public int CheckRestNum(Score[] data, int i)
		{
			int cnt = 0;	// 休符の数
			int max = NotePerBar - i % NotePerBar;	// チェックする最大数 単位小節を超えないように調整

			while (cnt < max)
			{
				// 休符以外の要素を発見したら
				if (data[i + cnt].code[0] != -1) return cnt;
				cnt++;
			}
			return cnt;
		}


		/// <summary>
		/// リスト内に指定した動物がいくつ含まれているかチェックするメソッド
		/// </summary>
		/// <param name="mode"></param>
		/// <returns>含まれていた数</returns>
		public int CheckAnimalNumber(muphic.ScoreScr.AnimalScoreMode mode)
		{
			int num = 0;
			for (int i = 0; i < parent.AnimalList.Count; i++)
			{
				Animal a = ((Animal)parent.AnimalList[i]);
				if (a.AnimalName.Equals(mode.ToString())) num++;
			}
			return num;
		}

		#endregion

		/// <summary>
		/// 上スクロールボタンを押した際の動作
		/// </summary>
		/// <returns>スクロールしたかどうか</returns>
		public bool UpScroll()
		{
			// １行目より上には行かないようにする
			if (this.offset >= 32)
			{
				// オフセットから1行分の音符数32を引く
				this.offset -= 32;

				// そして再描画
				this.ReDraw();

				// trueを返す
				return true;
			}

			// スクロールしなかった場合はfalseを返す
			return false;
		}


		/// <summary>
		/// 下スクロールボタンを押した際の動作
		/// </summary>
		/// <returns>スクロールしたかどうか</returns>
		public bool DownScroll()
		{
			if (this.nowScore != AnimalScoreMode.All)
			{
				// フルスコアでは無い場合、楽譜が７行以上になった時のみ下にスクロール可
				if (this.MaxNote > 192 && this.offset < this.MaxNote - 32)
				{
					// オフセットに1行分の音符数32を足す
					this.offset += 32;

					// そして再描画
					this.ReDraw();

					// trueを返す
					return true;
				}
			}
			else
			{
				// フルスコアの場合
				if (this.offset < this.MaxNote - 32)
				{
					// オフセットに1行分の音符数32を足す
					this.offset += 32;

					// そして再描画
					this.ReDraw();

					// trueを返す
					return true;
				}
			}

			// スクロールしなかったらfalseを返す
			return false;
		}


		/// <summary>
		/// オフセットをクリアするメソッド
		/// 何となくprivateにしたかったんで
		/// </summary>
		public void ClearOffset()
		{
			this.offset = 0;
		}

		#region デバッグ用メソッド群

		/// <summary>
		/// 音符リストをチェックする ってか一覧を表示する
		/// 主にデバッグ用
		/// </summary>
		/// <param name="data">一覧を表示する音符リスト</param>
		/// <param name="length">リストの長さ</param>
		public void CheckScoreList(Score[] data, int length)
		{
			int i = 0;

			for (i = 0; i < length; ++i)
			{
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data[i].length, data[i].code[0], data[i].code[1], data[i].code[2]);
			}
		}
		public void CheckScoreList(ArrayList list)
		{
			int i = 0;

			for (i = 0; i < list.Count; ++i)
			{
				Score data = (Score)list[i];
				System.Console.WriteLine("{0:d2}:{1}:{2},{3},{4}", i, data.length, data.code[0], data.code[1], data.code[2]);
			}
		}


		/// <summary>
		/// デバッグ用メッセージ出力メソッド
		/// </summary>
		/// <param name="str">何らかの文字列</param>
		/// <param name="num">何らかの値</param>
		public void Debug(String str, int num)
		{
			System.Console.WriteLine(str + num);
		}
		/// <summary>
		/// デバッグ用メッセージ出力メソッド
		/// </summary>
		/// <param name="str">何らかの文字列</param>
		/// <param name="num">何らかの値</param>
		public void Debug(String str, double num)
		{
			System.Console.WriteLine(str + num);
		}

		#endregion
	}

	#endregion
}
