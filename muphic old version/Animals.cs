using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic
{
	#region Ver1.1.0
	/*
	/// <summary>
	/// ver1.1.0 Search,RePlace追加
	/// </summary>
	public class Animals
	{
		private HouseLight houselight;
		public ArrayList AnimalList;
		public int PlayOffset;							//再生中のオフセット。ピクセル単位
		int nowPlace;									//現在の表示位置(こっちのほうは一時的に格納しているだけで、本当の値はScoreにある)
		int CheckedAnimal = -1;							//現在選択状態にされている動物の要素番号
		public Animals()
		{
			AnimalList = new ArrayList();
			houselight = new HouseLight();
		}

		public Animal this[int x]
		{
			get
			{
				return (Animal)AnimalList[x];
			}
//			set
//			{
//				AnimalList[x] = value;
//			}
		}

		/// <summary>
		/// 現在の楽譜内での相対的な位置を割り出す
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Point PointtoScoreRelative(Point p)
		{
			Point pp = muphic.Common.ScoreTools.PointtoScore(p);				//まず絶対的な位置を割り出す。
			pp.X += this.nowPlace;									//現在表示している座標の一番左側の値を足す
			return pp;
		}

		/// <summary>
		/// 現在の楽譜内での相対的な座標を割り出す
		/// </summary>
		/// <param name="place"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public Point ScoretoPointRelative(int place, int code)
		{
			return muphic.Common.ScoreTools.ScoretoPoint(place-this.nowPlace, code);			//結局相対的な位置を割り出すので
			//現在表示している座標の一番左端の値を引く
		}

		/// <summary>
		/// 動物達を描画する処理
		/// </summary>
		/// <param name="nowPlace">現在の再生中の位置</param>
		/// <param name="TempoMode">現在押されているテンポボタン</param>
		/// <param name="isPlaying">現在再生中かどうか</param>
		/// <returns>(再生中の場合のみ)再生が完了したかどうか</returns>
		public bool Draw(int nowPlace, int TempoMode, bool isPlaying)
		{
			houselight.Draw();
			this.nowPlace = nowPlace;						//一時的にnowPlaceを格納する
			if(isPlaying)
			{
				return DrawPlaying(TempoMode);				//再生が完了するとtrueが返ってくる
			}
			else
			{
				DrawNotPlaying();
				return false;
			}
		}

		/// <summary>
		/// 再生時の描画処理
		/// </summary>
		/// <remarks>再生が完了したかどうか(完了するとtrue)</remarks>
		private bool DrawPlaying(int TempoMode)
		{
			this.PlayOffset += TempoMode;									//テンポの分だけオフセットを足しとく
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//動物の音階と位置から座標を割り出す
				p.X -= this.PlayOffset;									//再生中なので、オフセットを引いとく
				if(p.X < 0 || a.Visible == false)						//もし画面外に出ているか、描画禁止なら
				{
					continue;											//今回のforは飛ばす
				}
				else if(p.X <= 55)										//もし家にぶつかっていたら
				{
					muphic.SoundManager.Play(a.ToString() + a.code + ".wav");//音を鳴らして…
					houselight.Add(a.code);								//家を光らして…
					a.Visible = false;
					continue;											//次のforへ
				}
				else if(800 < p.X)										//もし、まだ楽譜まで到達していないなら
				{														//AnimalListは順番どおりに並んでいるので、これから先も
					break;												//楽譜まで到達していないことになるからfor文を終了する
				}

				
				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;			//動物を揺らすための処理を施す。

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//これらの条件を満たしていなければ、普通に描画する
			}
			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalListの最後の要素を取り出す
			if(!b.Visible)
			{
				return true;											//動物の最後の要素が家にぶつかり終えたら再生終了
			}
			return false;
		}

		/// <summary>
		/// 非再生時の描画処理
		/// </summary>
		private void DrawNotPlaying()
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);
				//System.Diagnostics.Debug.WriteLine(a.place);
				DrawManager.DrawCenter(AnimalList[i].ToString(), p.X, p.Y);
			}
			//動物選択のやつの描画
			if(this.CheckedAnimal == -1)
			{
				return;
			}
			Animal aa = (Animal)AnimalList[this.CheckedAnimal];
			Point pp = this.ScoretoPointRelative(aa.place, aa.code);
			DrawManager.DrawCenter("AnimalCheck_" + aa.AnimalName, pp.X, pp.Y);
		}

		/// <summary>
		/// 指定された場所に動物がいるかどうかを調べるメソッド
		/// いればAnimalListにおける要素番号を返す
		/// </summary>
		/// <param name="place">指定する場所</param>
		/// <param name="code">指定する音階</param>
		/// <returns>見つけた動物の要素番号(いなければ-1)</returns>
		public int Search(int place, int code)
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)
				{
					return i;
				}
			}
			return -1;
		}

		public bool Insert(int place, int code, String mode)
		{
			return Insert(place, code, mode, true);
		}

		/// <summary>
		/// 動物を新たに追加する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <param name="code">音階</param>
		/// <param name="isPlaySound">配置時に音を鳴らすかどうか</param>
		/// <returns>成功したかどうか</returns>
		public bool Insert(int place, int code, String mode, bool isPlaySound)
		{
			int i;
			int SamePlaceCount=0;												//和音を3つまでにする制限のために必要
			for(i=0;i<AnimalList.Count;i++)										//和音の制限
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//場所が同じだと、カウントをインクリメント
				}
			}
			if(SamePlaceCount >= 3)
			{
				return false;													//カウントが3以上だったら和音の制限を受けて追加不可
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
				{
					return false;
				}
				if(a.place == place && a.code > code)							//設定する位置と同じでも、
				{																//音階が設定する位置より遠いものが存在したら、
					break;														//遠いものが現れたということはすでにOK
				}
				if(a.place > place)												//設定する位置より遠いものが存在した
				{																//AnimalListは昇順にソートしてあるから、
					break;														//遠いものが現れたということはすでにOK
				}
			}
			System.Diagnostics.Debug.Write("Insert in Animals" + place + "," + code + " " + mode);
			Animal newAnimal = new Animal(place, code, mode);					//Animalオブジェクトをインスタンス化
			AnimalList.Insert(i, newAnimal);									//遠いようになったところに割り込む
			//こうすることによって昇順が保たれる
			if(isPlaySound)														//音を鳴らしたいなら
			{
				SoundManager.Play(mode + code + ".wav");						//設置したということで、音を鳴らす
			}
			return true;
		}

		public bool InsertL(int place, int code, String mode)
		{
			int i;
			int SamePlaceCount=0;												//和音を3つまでにする制限のために必要
			for(i=0;i<AnimalList.Count;i++)										//和音の制限
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//場所が同じだと、カウントをインクリメント
				}
			}
			if(SamePlaceCount >= 1)
			{
				return false;													//カウントが3以上だったら和音の制限を受けて追加不可
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
				{
					return false;
				}
				if(a.place == place && a.code > code)							//設定する位置と同じでも、
				{																//音階が設定する位置より遠いものが存在したら、
					break;														//遠いものが現れたということはすでにOK
				}
				if(a.place > place)												//設定する位置より遠いものが存在した
				{																//AnimalListは昇順にソートしてあるから、
					break;														//遠いものが現れたということはすでにOK
				}
			}
			Animal newAnimal = new Animal(place, code, mode);					//Animalオブジェクトをインスタンス化
			AnimalList.Insert(i, newAnimal);									//遠いようになったところに割り込む
			//こうすることによって昇順が保たれる
			return true;
		}

		/// <summary>
		/// 現在選択されている動物を削除する
		/// </summary>
		/// <returns>成功したかどうか</returns>
		public bool Delete()
		{
			if(this.CheckedAnimal == -1)
			{
				return false;
			}
			Animal a = (Animal)AnimalList[this.CheckedAnimal];
			return Delete(a.place, a.code);
		}

		/// <summary>
		/// 動物を削除する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <param name="code">音階</param>
		/// <returns>成功したかどうか</returns>
		public bool Delete(int place, int code)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
				{
					if(i == this.CheckedAnimal)
					{
						this.CheckedAnimal = -1;
					}
					AnimalList.RemoveAt(i);
					return true;
				}
				if(a.place > place)												//削除する位置より遠いものが存在した
				{																//つまり、それ以上探しても意味がないため
					break;
				}
			}
			return false;
		}

		/// <summary>
		/// 動物の配置を変更する
		/// </summary>
		/// <param name="OldPlace">変更前の場所</param>
		/// <param name="OldCode">変更前の音階</param>
		/// <param name="NewPlace">変更後の場所</param>
		/// <param name="NewCode">変更後の音階</param>
		/// <param name="isCheck">変更後の動物を選択状態にするかどうか</param>
		/// <returns>変更後の要素番号(失敗で-1)</returns>
		public int Replace(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//変更前の動物が存在しない
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(Insert(NewPlace, NewCode, a.AnimalName, false) == false)
			{
				return -1;														//変更後の場所に既に動物がいる
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("ありえないエラー");			//変更前の動物が存在するはずなのになぜか失敗した
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// 動物の配置を変更する(Link)
		/// </summary>
		/// <param name="OldPlace">変更前の場所</param>
		/// <param name="OldCode">変更前の音階</param>
		/// <param name="NewPlace">変更後の場所</param>
		/// <param name="NewCode">変更後の音階</param>
		/// <param name="isCheck">変更後の動物を選択状態にするかどうか</param>
		/// <returns>変更後の要素番号(失敗で-1)</returns>
		public int ReplaceL(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//変更前の動物が存在しない
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(InsertL(NewPlace, NewCode, a.AnimalName) == false)
			{
				return -1;														//変更後の場所に既に動物がいる
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("ありえないエラー");			//変更前の動物が存在するはずなのになぜか失敗した
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// 動物を選択状態にする
		/// </summary>
		/// <param name="place">位置</param>
		/// <param name="code">音階</param>
		/// <returns>選択された動物の要素番号(なければ-1)</returns>
		public int ClickAnimal(int place, int code)
		{
			this.CheckedAnimal = this.Search(place, code);
			return this.CheckedAnimal;
		}

		/// <summary>
		/// 動物をすべて削除する
		/// </summary>
		public void AllDelete()
		{
			AnimalList.Clear();
			this.CheckedAnimal = -1;
		}
	}*/
	#endregion
	
	#region Ver2.1
	/// <summary>
	/// ver1.1.0 Search,RePlace追加
	/// ver2     道の外側で動物が表示できないように
	/// ver2.1   AnimalCheckを描画しないようにしたのと、InsertLでも音がなるようにした
	/// </summary>
	public class Animals
	{
		private HouseLight houselight;
		public ArrayList AnimalList;
		public int PlayOffset;							//再生中のオフセット。ピクセル単位
		int nowPlace;									//現在の表示位置(こっちのほうは一時的に格納しているだけで、本当の値はScoreにある)
		int CheckedAnimal = -1;							//現在選択状態にされている動物の要素番号
		public Animals()
		{
			AnimalList = new ArrayList();
			houselight = new HouseLight();
		}

		public Animal this[int x]
		{
			get
			{
				return (Animal)AnimalList[x];
			}/*
			set
			{
				AnimalList[x] = value;
			}*/
		}
		/*
				public int Count
				{
					get
					{
						return AnimalList.Count;
					}
				}
		*/
		/// <summary>
		/// 現在の楽譜内での相対的な位置を割り出す
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Point PointtoScoreRelative(Point p)
		{
			Point pp = muphic.Common.ScoreTools.PointtoScore(p);				//まず絶対的な位置を割り出す。
			pp.X += this.nowPlace;									//現在表示している座標の一番左側の値を足す
			return pp;
		}

		/// <summary>
		/// 現在の楽譜内での相対的な座標を割り出す
		/// </summary>
		/// <param name="place"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public Point ScoretoPointRelative(int place, int code)
		{
			return muphic.Common.ScoreTools.ScoretoPoint(place-this.nowPlace, code);			//結局相対的な位置を割り出すので
			//現在表示している座標の一番左端の値を引く
		}

		/// <summary>
		/// 動物達を描画する処理
		/// </summary>
		/// <param name="nowPlace">現在の再生中の位置</param>
		/// <param name="TempoMode">現在押されているテンポボタン</param>
		/// <param name="isPlaying">現在再生中かどうか</param>
		/// <returns>(再生中の場合のみ)再生が完了したかどうか</returns>
		public bool Draw(int nowPlace, int TempoMode, bool isPlaying)
		{
			houselight.Draw();
			this.nowPlace = nowPlace;						//一時的にnowPlaceを格納する
			if(isPlaying)
			{
				return DrawPlaying(TempoMode);				//再生が完了するとtrueが返ってくる
			}
			else
			{
				DrawNotPlaying();
				return false;
			}
		}

		/// <summary>
		/// 再生時の描画処理
		/// </summary>
		/// <remarks>再生が完了したかどうか(完了するとtrue)</remarks>
		private bool DrawPlaying(int TempoMode)
		{
			this.PlayOffset += TempoMode;									//テンポの分だけオフセットを足しとく
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//動物の音階と位置から座標を割り出す
				p.X -= this.PlayOffset;									//再生中なので、オフセットを引いとく
				if(p.X < 55 && a.Visible == false)						//もし家を通り過ぎているなら
				{
					continue;											//今回のforは飛ばす
				}
				else if(p.X <= 55)										//もし家にぶつかっていたら
				{
					muphic.SoundManager.Play(a.ToString() + a.code + ".wav");//音を鳴らして…
					houselight.Add(a.code);								//家を光らして…
					a.Visible = false;
					continue;											//次のforへ
				}
				else if(p.X < ScoreTools.score.Right)					//もし、道の中に動物が入ったのなら
				{
					a.Visible = true;									//表示させる
				}
				else if(ScoreTools.score.Right < p.X)					//もし、まだ楽譜まで到達していないなら
				{														//AnimalListは順番どおりに並んでいるので、これから先も
					break;												//楽譜まで到達していないことになるからfor文を終了する
				}

				
				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;			//動物を揺らすための処理を施す。

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//これらの条件を満たしていなければ、普通に描画する
			}
			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalListの最後の要素を取り出す
			Point bp = this.ScoretoPointRelative(b.place, b.code);
			bp.X -= this.PlayOffset;
			if(!b.Visible && bp.X <= 55)
			{
				return true;											//動物の最後の要素が家にぶつかり終えたら再生終了
			}
			return false;
		}

		/// <summary>
		/// 非再生時の描画処理
		/// </summary>
		private void DrawNotPlaying()
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);
				if(ScoreTools.inScore(p))								//道の内側にいれば
				{
					a.Visible = true;									//表示させる
				}
				else													//外側にいれば
				{
					a.Visible = false;									//表示させない
				}
				//System.Diagnostics.Debug.WriteLine(a.place);
				if(a.Visible)											//道の内側にいるときだけ
				{														//描画させる
					DrawManager.DrawCenter(AnimalList[i].ToString(), p.X, p.Y);
				}
			}
			//動物選択のやつの描画
//			if(this.CheckedAnimal == -1)
//			{
//				return;
//			}
//			Animal aa = (Animal)AnimalList[this.CheckedAnimal];
//			Point pp = this.ScoretoPointRelative(aa.place, aa.code);
//			if(ScoreTools.inScore(pp))									//もし、チェックされている動物が道の中にいるなら
//			{															//選択画像を描画
//				DrawManager.DrawCenter("AnimalCheck_" + aa.AnimalName, pp.X, pp.Y);
//			}
		}

		/// <summary>
		/// 指定された場所に動物がいるかどうかを調べるメソッド
		/// いればAnimalListにおける要素番号を返す
		/// </summary>
		/// <param name="place">指定する場所</param>
		/// <param name="code">指定する音階</param>
		/// <returns>見つけた動物の要素番号(いなければ-1)</returns>
		public int Search(int place, int code)
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// 動物を新たに追加する(音は鳴らす)
		/// </summary>
		/// <param name="place"></param>
		/// <param name="code"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public bool Insert(int place, int code, String mode)
		{
			return Insert(place, code, mode, true);
		}

		/// <summary>
		/// 動物を新たに追加する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <param name="code">音階</param>
		/// <param name="isPlaySound">配置時に音を鳴らすかどうか</param>
		/// <returns>成功したかどうか</returns>
		public bool Insert(int place, int code, String mode, bool isPlaySound)
		{
			int i;
			int SamePlaceCount=0;												//和音を3つまでにする制限のために必要
			for(i=0;i<AnimalList.Count;i++)										//和音の制限
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//場所が同じだと、カウントをインクリメント
				}
			}
			if(SamePlaceCount >= 3)
			{
				return false;													//カウントが3以上だったら和音の制限を受けて追加不可
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
				{
					return false;
				}
				if(a.place == place && a.code > code)							//設定する位置と同じでも、
				{																//音階が設定する位置より遠いものが存在したら、
					break;														//遠いものが現れたということはすでにOK
				}
				if(a.place > place)												//設定する位置より遠いものが存在した
				{																//AnimalListは昇順にソートしてあるから、
					break;														//遠いものが現れたということはすでにOK
				}
			}
			System.Diagnostics.Debug.Write("Insert in Animals" + place + "," + code + " " + mode);
			Animal newAnimal = new Animal(place, code, mode);					//Animalオブジェクトをインスタンス化
			AnimalList.Insert(i, newAnimal);									//遠いようになったところに割り込む
			//こうすることによって昇順が保たれる
			if(isPlaySound)														//音を鳴らしたいなら
			{
				SoundManager.Play(mode + code + ".wav");						//設置したということで、音を鳴らす
			}
			return true;
		}

		public bool InsertL(int place, int code, String mode)
		{
			int i;
			int SamePlaceCount=0;												//和音を3つまでにする制限のために必要
			for(i=0;i<AnimalList.Count;i++)										//和音の制限
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)
				{
					SamePlaceCount++;											//場所が同じだと、カウントをインクリメント
				}
			}
			if(SamePlaceCount >= 1)
			{
				return false;													//カウントが3以上だったら和音の制限を受けて追加不可
			}

			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
				{
					return false;
				}
				if(a.place == place && a.code > code)							//設定する位置と同じでも、
				{																//音階が設定する位置より遠いものが存在したら、
					break;														//遠いものが現れたということはすでにOK
				}
				if(a.place > place)												//設定する位置より遠いものが存在した
				{																//AnimalListは昇順にソートしてあるから、
					break;														//遠いものが現れたということはすでにOK
				}
			}
			Animal newAnimal = new Animal(place, code, mode);					//Animalオブジェクトをインスタンス化
			AnimalList.Insert(i, newAnimal);									//遠いようになったところに割り込む
			//こうすることによって昇順が保たれる
			SoundManager.Play(mode + code + ".wav");						//設置したということで、音を鳴らす
			return true;
		}

		/// <summary>
		/// 現在選択されている動物を削除する
		/// </summary>
		/// <returns>成功したかどうか</returns>
		public bool Delete()
		{
			if(this.CheckedAnimal == -1)
			{
				return false;
			}
			Animal a = (Animal)AnimalList[this.CheckedAnimal];
			return Delete(a.place, a.code);
		}

		/// <summary>
		/// 動物を削除する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <param name="code">音階</param>
		/// <returns>成功したかどうか</returns>
		public bool Delete(int place, int code)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
				{
					if(i == this.CheckedAnimal)
					{
						this.CheckedAnimal = -1;
					}
					AnimalList.RemoveAt(i);
					return true;
				}
				if(a.place > place)												//削除する位置より遠いものが存在した
				{																//つまり、それ以上探しても意味がないため
					break;
				}
			}
			return false;
		}

		/// <summary>
		/// 動物の配置を変更する(変更後の音を鳴らさない)
		/// </summary>
		/// <param name="OldPlace">変更前の場所</param>
		/// <param name="OldCode">変更前の音階</param>
		/// <param name="NewPlace">変更後の場所</param>
		/// <param name="NewCode">変更後の音階</param>
		/// <param name="isCheck">変更後の動物を選択状態にするかどうか</param>
		/// <returns>変更後の要素番号(失敗で-1)</returns>
		public int Replace(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			return Replace(OldPlace, OldCode, NewPlace, NewCode, isCheck, false);
		}

		/// <summary>
		/// 動物の配置を変更する
		/// </summary>
		/// <param name="OldPlace">変更前の場所</param>
		/// <param name="OldCode">変更前の音階</param>
		/// <param name="NewPlace">変更後の場所</param>
		/// <param name="NewCode">変更後の音階</param>
		/// <param name="isCheck">変更後の動物を選択状態にするかどうか</param>
		/// <param name="isPlaySound">変更時に音を鳴らすかどうか</param>
		/// <returns>変更後の要素番号(失敗で-1)</returns>
		public int Replace(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck, bool isPlaySound)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//変更前の動物が存在しない
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(isPlaySound)													//いろいろな関係で、
			{																//Insertで音を鳴らすのでは
				SoundManager.Play(a.AnimalName + a.code + ".wav");			//なく、ここで鳴らす
			}
			if(Insert(NewPlace, NewCode, a.AnimalName, false) == false)
			{
				return -1;														//変更後の場所に既に動物がいる
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("ありえないエラー");			//変更前の動物が存在するはずなのになぜか失敗した
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// 動物の配置を変更する(Link)
		/// </summary>
		/// <param name="OldPlace">変更前の場所</param>
		/// <param name="OldCode">変更前の音階</param>
		/// <param name="NewPlace">変更後の場所</param>
		/// <param name="NewCode">変更後の音階</param>
		/// <param name="isCheck">変更後の動物を選択状態にするかどうか</param>
		/// <returns>変更後の要素番号(失敗で-1)</returns>
		public int ReplaceL(int OldPlace, int OldCode, int NewPlace, int NewCode, bool isCheck)
		{
			int OldNum = Search(OldPlace, OldCode);
			if(OldNum == -1)
			{
				return -1;														//変更前の動物が存在しない
			}
			Animal a = (Animal)AnimalList[OldNum];

			if(InsertL(NewPlace, NewCode, a.AnimalName) == false)
			{
				return -1;														//変更後の場所に既に動物がいる
			}
			if(Delete(OldPlace, OldCode) == false)
			{
				System.Diagnostics.Debug.WriteLine("ありえないエラー");			//変更前の動物が存在するはずなのになぜか失敗した
				return -1;
			}
			if(isCheck)
			{
				this.CheckedAnimal = Search(NewPlace, NewCode);
			}
			return Search(NewPlace, NewCode);
		}

		/// <summary>
		/// 動物を選択状態にする
		/// </summary>
		/// <param name="place">位置</param>
		/// <param name="code">音階</param>
		/// <returns>選択された動物の要素番号(なければ-1)</returns>
		public int ClickAnimal(int place, int code)
		{
			this.CheckedAnimal = this.Search(place, code);
			return this.CheckedAnimal;
		}

		/// <summary>
		/// 動物をすべて削除する
		/// </summary>
		public void AllDelete()
		{
			AnimalList.Clear();
			this.CheckedAnimal = -1;
		}
	}
	#endregion
}
