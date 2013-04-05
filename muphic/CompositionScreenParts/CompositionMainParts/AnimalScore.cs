using System.Collections.Generic;
using System.Drawing;

using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.CompositionScreenParts.CompositionMainParts
{
	/// <summary>
	/// 楽譜上での動物の動き全般の管理を行うクラス（ver.7以前は Animals）。
	/// </summary>
	public class AnimalScore
	{

		#region プロパティとインデクサとコンストラクタ

		/// <summary>
		/// このクラスの親となる作曲クラス。
		/// <para>Parent プロパティを使用すること。</para>
		/// </summary>
		private readonly CompositionMain __parent;

		/// <summary>
		/// このクラスの親となる作曲クラスを取得する。
		/// </summary>
		public CompositionMain Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 編集中の楽譜の動物リストを取得する。
		/// </summary>
		public List<Animal> AnimalList
		{
			get
			{
				return this.Parent.Parent.CurrentScoreData.AnimalList;
			}
		}

		#region 音階制限

		/// <summary>
		/// 音階の制限に利用する、動物の位置と音階のペアのリストを取得または設定する。
		/// </summary>
		public List<Point> LimitList { get; set; }

		///// <summary>
		///// 音階の制限を使用するかどうか。
		///// </summary>
		//private bool __isCodeLimit;

		///// <summary>
		///// 音階の制限を利用するかどうかを示す値を取得または設定する。
		///// </summary>
		//public bool IsCodeLimit
		//{
		//    get
		//    {
		//        return this.__isCodeLimit;
		//    }
		//    set
		//    {
		//        this.__isCodeLimit = value;
		//        this.CodeLimitMsgTime = 60;
		//        LogFileManager.WriteLine(value ?
		//            Properties.Resources.Msg_CompositionScr_CodeLimitMode_ON :
		//            Properties.Resources.Msg_CompositionScr_CodeLimitMode_OFF
		//        );
		//    }
		//}

		///// <summary>
		///// 音階の制限の切り換え時のメッセージを表示するフレーム数を取得または設定する。
		///// </summary>
		//private int CodeLimitMsgTime { get; set; }

		#endregion

		/// <summary>
		/// 窓の発光を管理する。
		/// </summary>
		private HouseLight HouseLight { get; set; }

		/// <summary>
		/// 現在選択状態となっている動物の AnimalList 内での要素番号を表わす整数を取得または設定する。
		/// </summary>
		private int CheckedAnimal { get; set; }

		/// <summary>
		/// 現在表示されている楽譜での基点位置を表わす整数を取得または設定する。
		/// </summary>
		private int NowPlace { get; set; }

		/// <summary>
		/// 家の中央部分を示す X 座標。再生中に動物がこの線を越えると音を鳴らす。
		/// </summary>
		private readonly int __houseCenter = Locations.HouseCenter.X;

		/// <summary>
		/// 家の中央部分を示す X 座標を取得する。。再生中に動物がこの線を越えると音を鳴らす。
		/// </summary>
		private int HouseCenter
		{
			get { return this.__houseCenter; }
		}

		/// <summary>
		/// 楽譜の道の部分を示す領域。
		/// </summary>
		private readonly Rectangle __scoreArea = Locations.ScoreArea;

		/// <summary>
		/// 楽譜の道の部分を示す矩形を取得する。
		/// </summary>
		private Rectangle ScoreArea
		{
			get { return this.__scoreArea; }
		}


		/// <summary>
		/// 再生時のピクセル単位でのオフセットを表す実数を取得または設定する。
		/// <para>フレーム毎にこのプロパティの値にテンポ値を加えていくことで、再生開始から動物が何ピクセル動いたかを表す。</para>
		/// </summary>
		public float PlayOffset { get; set; }


		/// <summary>
		/// 楽譜上の動物の番号から動物を得る。
		/// </summary>
		/// <param name="index">AnimalList内の要素番号(楽譜上の動物の番号)。</param>
		/// <returns>indexに対応した動物。</returns>
		public Animal this[int index]
		{
			get { return this.AnimalList[index]; }
		}


		/// <summary>
		/// 動物の動作全般管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		public AnimalScore(CompositionMain parent)
		{
			this.__parent = parent;
			this.HouseLight = new HouseLight();
			this.CheckedAnimal = -1;

			this.LimitList = new List<Point>();
		}

		#endregion


		#region 音階制限


		/// <summary>
		/// 作曲時の制限を設定する。
		/// </summary>
		/// <param name="limitList">制限する音階のリスト。音階を制限しない場合は Null。</param>
		/// <param name="isUseEighthNote">八分音符を使用する場合は true、それ以外は false。</param>
		/// <param name="harmonyNum">許可する和音の数。</param>
		public void SetLimitMode(List<Point> limitList, bool isUseEighthNote, int harmonyNum)
		{
			if (limitList == null) this.LimitList.Clear();
			else this.LimitList = limitList;
			ConfigurationManager.Current.IsUseEighthNote = isUseEighthNote;
			ConfigurationManager.Current.HarmonyNum = harmonyNum;
		}


		#endregion


		#region 座標計算

		/// <summary>
		/// マウス座標から、表示中の楽譜での相対位置と音階を得る。
		/// </summary>
		/// <param name="mousePoint">マウス座標。</param>
		/// <returns>ｘに楽譜内での位置、ｙに楽譜内での音階が格納された Point 型の値。</returns>
		public Point PointToRelativeScore(Point mousePoint)
		{
			// マウス座標から楽譜上の位置を得る
			Point score = CompositionTools.PointToScore(mousePoint);

			// 現在表示中の楽譜の左端の位置を加える
			score.X += this.NowPlace;

			return score;
		}


		/// <summary>
		/// 表示中の楽譜での位置と音階からマウス座標を得る。
		/// </summary>
		/// <param name="place">位置。</param>
		/// <param name="code">音階。</param>
		/// <returns>位置と音階に対応したマウス座標。</returns>
		public Point ScoreToRelativePoint(int place, int code)
		{
			// 現在表示中の楽譜の左端の位置を引いた上でマウス座標を得る
			return CompositionTools.ScoreToPoint(place - this.NowPlace, code);
		}

		/// <summary>
		/// 表示中の楽譜での位置と音階からマウス座標を得る。
		/// </summary>
		/// <param name="place">位置。</param>
		/// <param name="code">音階。</param>
		/// <param name="isCenter">画像の中央画像を取得する場合は true、それ以外は false。</param>
		/// <returns>位置と音階に対応したマウス座標。</returns>
		public Point ScoreToRelativePoint(int place, int code, bool isCenter)
		{
			// 現在表示中の楽譜の左端の位置を引いた上でマウス座標を得る
			return CompositionTools.ScoreToPoint(place - this.NowPlace, code, isCenter);
		}

		#endregion


		#region 動物の描画

		/// <summary>
		/// 楽譜上の動物の描画を行う。
		/// </summary>
		/// <param name="nowPlace">現在表示中の位置。</param>
		/// <param name="tempo">選択されているテンポ。</param>
		/// <param name="isPlaying">現在再生中なら true、それ以外なら false となる再生フラグ。</param>
		/// <param name="drawStatus">描画時の状態データ。</param>
		/// <returns>(引数 isPlaying が true の場合のみ) 再生が完了した場合は true、それ以外は false。</returns>
		public bool Draw(int nowPlace, int tempo, bool isPlaying, DrawStatusArgs drawStatus)
		{
			this.NowPlace = nowPlace;
			bool result = false;

			this.HouseLight.Draw();		// まず家の描画

			if (this.LimitList.Count != 0)		// 次に制限音階の描画
			{
				for (int i = 0; i < this.LimitList.Count; i++)
				{												// 位置と音階から表示座標を算出
					Point location = this.ScoreToRelativePoint(this.LimitList[i].X, this.LimitList[i].Y, false);

					if (this.ScoreArea.Contains(new Point(location.X, location.Y + 10)))		// 道の内側にいる場合、その制限の描画
						DrawManager.Draw("IMAGE_COMPOSITIONSCR_LIMIT", location.X, location.Y);
				}
			}

			this.Parent.SignBoard.Draw(drawStatus);			// 制限の上から看板描画

			if (isPlaying)
			{
				result = DrawPlaying(tempo, drawStatus);	// 再生中の場合は再生時の描画メソッドを呼ぶ
			}												// 再生が完了した場合に戻り値がtrueとなる
			else
			{
				DrawNotPlaying(drawStatus);					// 非再生中の場合は非再生時の描画メソッドを呼ぶ
			}

			DrawManager.Draw("IMAGE_COMPOSITIONSCR_SCOREAREARIGHT", 696, 141);

			//// 音階制限メッセージの描画
			//if (this.CodeLimitMsgTime > 0)
			//{
			//    this.CodeLimitMsgTime--;
			//    DrawManager.Draw(this.IsCodeLimit ? "IMAGE_COMPOSITIONSCR_LIMITON_MSG" : "IMAGE_COMPOSITIONSCR_LIMITOFF_MSG", Locations.LimitModeMessage);
			//}

			return result;
		}


		/// <summary>
		/// 再生時の動物の描画を行う。
		/// </summary>
		/// <param name="tempo">再生中のテンポ。</param>
		/// <param name="drawStatus">描画時の状態データ。</param>
		/// <returns>再生が終了したら true、それ以外は false。</returns>
		private bool DrawPlaying(int tempo, DrawStatusArgs drawStatus)
		{
			if (!drawStatus.ShowDialog)						// ダイアログ表示時は動物は停めておかなければならないため、以降の動物移動のオフセット加算は
			{												// drawStatusのダイアログ表示フラグが立っていない場合のみ実行する（フラグ立ってたら位置を変えずに動物描画）
				if (tempo > 0)
					this.PlayOffset += tempo;				// 再生時は描画の度にテンポ値をオフセットに加算していく（オフセット値が動物がこれまでに進んだ距離となる）
				else
					this.PlayOffset += 0.5F;				// ただし、テンポが0であれば最低値として0.5を加える
			}

			for (int i = 0; i < AnimalList.Count; i++)
			{
				if (AnimalList[i].Place < this.NowPlace - 1) continue;						// 再生開始位置より左にある動物は飛ばす

				Animal animal = AnimalList[i];
				Point location = this.ScoreToRelativePoint(animal.Place, animal.Code);		// 動物の位置と音階から表示座標を算出
				location.X -= (int)this.PlayOffset;											// 動物移動の計算：オフセットを引く（これで、フレーム数×テンポ値分だけ動物が左に移動する）

				// 動物のx座標によって3パターンの処理に分かれる
				if (location.X <= this.HouseCenter)
				{											// 動物のx座標が家の左側だった場合
					if (!animal.Visible) continue;			// 不可視状態であれば既に鳴らされた音と判断し次へ
					SoundManager.Play(animal);				// 音の再生
					this.HouseLight.Add(animal.Code);		// 発光の予約
					animal.Visible = false;					// 不可視にする
					continue;
				}
				else if (location.X < this.ScoreArea.Right)
				{											// 動物のx座標が道の中だった場合
					animal.Visible = true;					// 可視状態にする
				}
				else										// それ以外の場合(動物が楽譜部分まで達していない場合)
				{											// AnimalListが位置順番通りに並んでいるため、以降の動物も楽譜に達していない
					break;									// ループ終了
				}

				// 動物を描画（y座標の値が不定なのは動物を上下に揺らすため）
				DrawManager.DrawCenter(animal.ToString(true), location.X, (location.X % 30 < 15) ? location.Y + 2 : location.Y - 2);
			}

			// 最後の動物の再生が終了したかをチェックする 再生が終了していたら再生終了を知らせる
			Animal lastAnimal = this.AnimalList[AnimalList.Count - 1];
			if (!lastAnimal.Visible &&
				(this.ScoreToRelativePoint(lastAnimal.Place, lastAnimal.Code).X - this.PlayOffset <= this.HouseCenter))
			{
				return true;
			}

			return false;
		}


		/// <summary>
		/// 非再生時での動物の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		private void DrawNotPlaying(DrawStatusArgs drawStatus)
		{
			for (int i = 0; i < this.AnimalList.Count; i++)
			{
				// 動物の位置と音階から表示座標を算出
				Point location = this.ScoreToRelativePoint(this.AnimalList[i].Place, this.AnimalList[i].Code);

				// 動物の表示座標により2パターンの描画
				if (this.ScoreArea.Contains(location))
				{
					this.AnimalList[i].Visible = true;													// 道の内側にいる場合、その動物を可視化して
					DrawManager.DrawCenter(this.AnimalList[i].ToString(true), location.X, location.Y);	// 描画する
				}
				else
				{
					this.AnimalList[i].Visible = false;		// 道の外側にいる場合、その動物を不可視化する
				}
			}
		}

		#endregion


		#region 動物の追加

		/// <summary>
		/// 動物を新たに追加する。追加に成功した場合はその動物の音が鳴る。
		/// </summary>
		/// <param name="place">楽譜内の位置。</param>
		/// <param name="code">音階。</param>
		/// <param name="animalName">追加する動物。</param>
		/// <returns>動物の追加に成功した場合は true、それ以外は false。</returns>
		public bool Insert(int place, int code, AnimalButtonMode animalName)
		{
			return this.Insert(place, code, animalName, true);
		}

		/// <summary>
		/// 楽譜に動物を追加する。
		/// </summary>
		/// <param name="place">楽譜内の位置。</param>
		/// <param name="code">音階。</param>
		/// <param name="animalName">追加する動物。</param>
		/// <param name="isPlaying">追加する際に音を鳴らす場合は true、鳴らさない場合は false。</param>
		/// <returns>動物の追加に成功した場合は true、それ以外は false。</returns>
		public bool Insert(int place, int code, AnimalButtonMode animalName, bool isPlaying)
		{
			// AnimalButtonMode列挙型から文字列の値を得る
			string animalStr = System.Enum.GetName(typeof(AnimalButtonMode), animalName);

			// AnimalName列挙型にその文字列と等価の定数が存在した場合は
			if (System.Enum.IsDefined(typeof(AnimalName), animalStr))
			{
				return this.Insert(place, code, (AnimalName)System.Enum.Parse(typeof(AnimalName), animalStr), isPlaying);
			}
			else
			{
				Tools.DebugTools.ConsolOutputError("AnimalScore -Insert", "\"" + animalStr + "\"と等価の AnimalName 列挙型の定数が存在しないため、追加失敗", true);
				return false;
			}
		}

		/// <summary>
		/// 楽譜に動物を追加する。
		/// </summary>
		/// <param name="place">楽譜内の位置。</param>
		/// <param name="code">音階。</param>
		/// <param name="animalName">追加する動物。</param>
		/// <param name="isPlaySound">追加する際に音を鳴らす場合は true、鳴らさない場合は false。</param>
		/// <returns>動物の追加に成功した場合は true、それ以外は false。</returns>
		public bool Insert(int place, int code, AnimalName animalName, bool isPlaySound)
		{
			return this.Insert(place, code, animalName, isPlaySound, true);
		}

		/// <summary>
		/// 楽譜に動物を追加する。
		/// </summary>
		/// <param name="place">楽譜内の位置。</param>
		/// <param name="code">音階。</param>
		/// <param name="animalName">追加する動物。</param>
		/// <param name="isPlaySound">追加する際に音を鳴らす場合は true、鳴らさない場合は false。</param>
		/// <param name="constraintHarmonyNum">和音の数をシステムで規定された数で制限する場合は true、制限しない場合は false。</param>
		/// <returns>動物の追加に成功した場合は true、それ以外は false。</returns>
		public bool Insert(int place, int code, AnimalName animalName, bool isPlaySound, bool constraintHarmonyNum)
		{
			int i = 0;					// ループ制御変数 ループの外でも使うためここで宣言
			int samePlaceCount = 0;		// 和音の制限のチェックに使用するカウンタ

			if (this.LimitList.Count != 0 && this.LimitList.Contains(new Point(place, code))) return false;	// 音階制限が有効かつ追加位置が制限されていた場合は無効

			for (; i < this.AnimalList.Count; i++)								// 追加不可能なパターンを探る ここでreturnに引っ掛からなければ追加可能
			{
				if (this.AnimalList[i].Equals(place, code)) return false;		// 追加要求の位置と音階に既に動物がいる場合は失敗
				else if (this.AnimalList[i].Place == place) samePlaceCount++;	// 和音の制限のため、同位置の動物数をカウントする

				if (constraintHarmonyNum && samePlaceCount >= Manager.ConfigurationManager.Current.HarmonyNum)
					return false;												// 同位置の動物数が規定以上に達した場合、和音の制限により追加失敗
			}

			for (i = 0; i < this.AnimalList.Count; i++)							// リスト内での追加位置を求める(降順にソートされていなければならないため)
			{
				if (this.AnimalList[i].Place == place && this.AnimalList[i].Code > code) break;		// 追加要求位置と同じ位置でも、音階が下であれば追加可能
				if (this.AnimalList[i].Place > place) break;										// 追加要求位置より遠い位置の動物が初めてヒットした時点で位置確定
			}

			Tools.DebugTools.ConsolOutputMessage("AnimalScore -Insert", "位置:" + place + ", 音階:" + code + ", 動物:" + animalName.ToString(), true);

			Animal insertAnimal = new Animal(place, code, animalName);		// 新しい動物を作成
			AnimalList.Insert(i, insertAnimal);								// 降順を保ちつつ新しい動物を追加
			if (isPlaySound) SoundManager.Play(insertAnimal);				// 追加と同時に音を鳴らす

			return true;
		}

		#endregion


		#region 動物の削除

		/// <summary>
		/// 現在選択されている動物を削除する。
		/// </summary>
		/// <returns>削除に成功したら true、それ以外は false。</returns>
		public bool Delete()
		{
			if (this.CheckedAnimal == -1) return false;
			return this.Delete(this.AnimalList[this.CheckedAnimal].Place, this.AnimalList[this.CheckedAnimal].Code);
		}

		/// <summary>
		/// 指定された位置と音階の動物を削除する。
		/// </summary>
		/// <param name="place">削除位置。</param>
		/// <param name="code">音階。</param>
		/// <returns>削除に成功したら true、それ以外は false。</returns>
		public bool Delete(int place, int code)
		{
			for (int i = 0; i < this.AnimalList.Count; i++)					// 要求された位置・音階と一致する動物を探す
			{
				if (this.AnimalList[i].Equals(place, code))					// 一致する動物が存在した場合
				{
					DebugTools.ConsolOutputMessage("AnimalScore -Delete", "位置:" + place + ", 音階:" + code + ", 動物:" + AnimalList[i].ToString(), true);

					if (this.CheckedAnimal == i) this.CheckedAnimal = -1;	// その動物が選択されていれば選択を解除し、
					this.AnimalList.RemoveAt(i);							// 動物を削除
					return true;
				}

				if (this.AnimalList[i].Place > place) break;				// 削除要求された位置を超えた場合、それ以降探索しても意味がないため終了
			}

			return false;
		}

		/// <summary>
		/// 楽譜上の動物をクリア (全て削除) する。
		/// </summary>
		public void Clear()
		{
			this.AnimalList.Clear();
			this.CheckedAnimal = -1;
		}

		#endregion


		#region 動物の移動

		/// <summary>
		/// 動物の配置を音を鳴らさずに移動する。
		/// </summary>
		/// <param name="oldPlace">移動前の位置 (横)。</param>
		/// <param name="oldCode">移動前の音階 (縦)。</param>
		/// <param name="newPlace">移動後の位置 (横)。</param>
		/// <param name="newCode">移動前の音階 (縦)。</param>
		/// <param name="isCheck">移動後に動物を選択状態にする場合は true、それ以外は false。</param>
		/// <returns>移動後の動物の AnimalList 内での要素番号、変更に失敗した場合のみ -1。</returns>
		public int Replace(int oldPlace, int oldCode, int newPlace, int newCode, bool isCheck)
		{
			return this.Replace(oldPlace, oldCode, newPlace, newCode, isCheck, false);
		}

		/// <summary>
		/// 動物の配置を移動する。
		/// </summary>
		/// <param name="oldPlace">移動前の位置 (横)。</param>
		/// <param name="oldCode">移動前の音階 (縦)。</param>
		/// <param name="newPlace">移動後の位置 (横)。</param>
		/// <param name="newCode">移動前の音階 (縦)。</param>
		/// <param name="isCheck">移動後に動物を選択状態にする場合は true、それ以外は false。</param>
		/// <param name="isPlaySond">移動時に音を鳴らす場合は true、それ以外は false。</param>
		/// <returns>移動後の動物の AnimalList 内での要素番号、変更に失敗した場合のみ -1。</returns>
		public int Replace(int oldPlace, int oldCode, int newPlace, int newCode, bool isCheck, bool isPlaySond)
		{
			int oldAnimalNum = this.Exists(oldPlace, oldCode);
			if (oldAnimalNum == -1) return -1;							// 変更前の動物が存在しない場合は-1を返す

			// 移動後の位置に動物の追加を試みる  追加に失敗したら -1
			// 移動前の音階と移動後の音階が同じだった場合、和音の数による配置の制限を無視する (Insert メソッド第 5 引数)
			if (!this.Insert(newPlace, newCode, this.AnimalList[oldAnimalNum].AnimalName, false, oldPlace != newPlace)) return -1;

			// 移動前の位置の動物の削除を試みる  削除に失敗したら -1
			if (!this.Delete(oldPlace, oldCode)) return -1;

			if (isCheck)
			{
				this.CheckedAnimal = this.Exists(newPlace, newCode);	// 追加した動物を選択する設定だった場合は選択する
				return this.CheckedAnimal;								// 選択した動物のAnimalList内の要素番号を返す
			}

			return this.Exists(newPlace, newCode);						// 追加した動物のAnimalList内の要素番号を返す
		}

		#endregion


		#region 動物の検索

		/// <summary>
		/// 指定された位置と音階に動物が存在するかをチェックする。
		/// </summary>
		/// <param name="place">動物の位置。</param>
		/// <param name="code">動物の音階。</param>
		/// <returns>存在すればその動物の AnimalList 内の要素番号、存在しなければ -1。</returns>
		public int Exists(int place, int code)
		{
			for (int i = 0; i < this.AnimalList.Count; i++)
			{
				if (this.AnimalList[i].Equals(place, code)) return i;
			}

			return -1;
		}


		/// <summary>
		/// 動物を選択状態にし、その動物の AnimalList 内での要素番号を返す。
		/// </summary>
		/// <param name="place">。</param>
		/// <param name="code">。</param>
		/// <returns>。</returns>
		public int CheckAnimal(int place, int code)
		{
			this.CheckedAnimal = this.Exists(place, code);
			return this.CheckedAnimal;
		}

		#endregion

	}
}
