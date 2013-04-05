using System.Drawing;

using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.CompositionScreenParts
{
	/// <summary>
	/// 作曲画面で、追尾しているスタンプの状態を表す識別子を指定する。
	/// </summary>
	public enum HomingMode
	{
		/// <summary>
		/// 何も追尾していない状態。
		/// </summary>
		Empty,

		/// <summary>
		/// 動物が追尾している状態。
		/// </summary>
		Animal,

		/// <summary>
		/// 動物を囲う線を描画する状態。
		/// </summary>
		Line,
	}


	/// <summary>
	/// 作曲画面の楽譜(道)内での動物の動きを管理する追尾クラス（ver.7以前はTsuibi）。
	/// </summary>
	/// <remarks>
	/// 追尾時の動物の描画位置 (AnimalPoint プロパティ) と、マウスポインタへの追尾の有無 (HomingMode プロパティ) の管理を行う。
	/// <para>追尾状態の場合 (マウスポインタが楽譜内の場合)、このクラスの親クラス (Screen 型) は、このクラス名で登録されたテクスチャと状態 (State プロパティ) で、AnimalPoint 座標に描画する。</para>
	/// <para>また、スタンプボタンが押された直後は楽譜外 (動物ボタン領域) でも追尾しなければならない。このクラスは、インスタンス生成時に6楽譜外で追尾を許可する領域 (__homingRectangle フィールド) の算出も行う。</para>
	/// </remarks>
	public class AnimalHoming : Common.Parts
	{
		/// <summary>
		/// 親にあたる汎用作曲画面クラス。
		/// </summary>
		public CompositionScreen Parent { get; private set; }

		/// <summary>
		/// 追尾する動物の座標を取得または設定する。
		/// </summary>
		public Point AnimalPoint { get; private set; }


		/// <summary>
		/// 楽譜外で追尾が有効となる領域を保持する。
		/// </summary>
		private readonly Rectangle __homingRectangle;

		/// <summary>
		/// 楽譜外で追尾が有効となる領域を取得する。
		/// </summary>
		private Rectangle HomingRectangle
		{
			get { return this.__homingRectangle; }
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
		/// 追尾の状態を取得または設定する。
		/// </summary>
		public HomingMode HomingMode { get; set; }


		/// <summary>
		/// 追尾クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる汎用作曲画面クラス。</param>
		public AnimalHoming(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			// 追尾動物の登録
			DrawManager.Regist(this.ToString(), new Point(0, 0),
				"IMAGE_COMPOSITIONSCR_NONE",
				"IMAGE_COMPOSITIONSCR_SHEEP",
				"IMAGE_COMPOSITIONSCR_RABBIT",
				"IMAGE_COMPOSITIONSCR_BIRD",
				"IMAGE_COMPOSITIONSCR_DOG",
				"IMAGE_COMPOSITIONSCR_PIG",
				"IMAGE_COMPOSITIONSCR_CAT",
				"IMAGE_COMPOSITIONSCR_VOICE",
				"IMAGE_COMPOSITIONSCR_DELETE",
				"IMAGE_COMPOSITIONSCR_DELETE_2"
			);
			
			// 追尾有効領域の算出
			Rectangle scoreRectangle = Locations.ScoreArea;		// 楽譜の領域
			Rectangle abtnsRectangle = RectangleManager.Get(this.Parent.AnimalButtons.ToString());		// 動物ボタンの領域
			this.__homingRectangle = new Rectangle(				// 楽譜外で追尾が有効な領域を設定
				scoreRectangle.Right,							// 左上x座標は楽譜領域の右端
				abtnsRectangle.Top,								// 左上y座標はボタン領域の上端	
				abtnsRectangle.Right - scoreRectangle.Right,	// 横幅はボタン領域の右端と楽譜領域の右端との差
				abtnsRectangle.Height							// 縦幅はボタン領域の縦幅
			);

			this.HomingMode = HomingMode.Empty;
		}


		/// <summary>
		/// 作曲画面上でマウスポインタが移動した際の、動物の追尾状態を決定する。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態データ。</param>
		public void MouseMove(MouseStatusArgs mouseStatus)
		{
			if (this.Parent.CompositionMain.IsPlaying)
			{
				// 再生中の場合は追尾しない
				this.HomingMode = HomingMode.Empty;
			}
			else if (mouseStatus.IsDrag && this.Parent.CompositionMain.DragBeginAnimalNum >= 0 && this.State != 8)
			{
				// 動物をドラッグ移動させている場合（削除追尾を除く）

				this.AnimalPoint = CompositionTools.ScoreToPoint(
					this.AnimalList[this.Parent.CompositionMain.DragBeginAnimalNum].Place - this.Parent.CompositionMain.NowPlace,
					this.AnimalList[this.Parent.CompositionMain.DragBeginAnimalNum].Code
				);												// 描画位置を決定（ドラッグ対象の動物の位置を座標に変換）
				this.HomingMode = HomingMode.Line;				// 追尾状態をライン描画にする
			}
			else if (this.ScoreArea.Contains(mouseStatus.NowLocation))
			{
				// マウス位置が楽譜内だった場合

				this.AnimalPoint = CompositionTools.GetAnimalDrawLocation(mouseStatus.NowLocation);	// 描画座標を決定
				this.HomingMode = HomingMode.Animal;											// 動物を追尾状態にする

				Point NowAnimalPlace = CompositionTools.GetNearestScore(mouseStatus.NowLocation);		// マウスポインタ座標が、楽譜のどの位置なのかを算出する
				NowAnimalPlace.X += this.Parent.CompositionMain.NowPlace;						// オフセット加算

				for (int i = 0; i < this.AnimalList.Count; i++)							// マウスポインタの楽譜上の位置に、既に動物が配置されているかをチェック
				{
					if (this.AnimalList[i].Equals(NowAnimalPlace.X, NowAnimalPlace.Y))
					{																	// 既に動物が配置されている場合
						this.Parent.AnimalHoming.HomingMode = HomingMode.Line;			// 動物の追尾ではなくライン描画に切り替える
						break;
					}

					if (this.AnimalList[i].Place > NowAnimalPlace.X) break;				// マウスポインタ位置より遠い位置の動物がヒットした時点で、
				}																		// それ以降マウスポインタ位置と同じ位置の動物は存在しない
			}
			else if (this.Parent.AnimalButtons.EnabledHoming && this.HomingRectangle.Contains(mouseStatus.NowLocation))
			{
				// 動物が選択された直後で(選択されてから一度も楽譜内にマウスが入っていない状態で)、楽譜右端～動物ボタン領域内だった場合
				this.HomingMode = HomingMode.Animal;			// 可視状態にする
				this.AnimalPoint = mouseStatus.NowLocation;		// 描画座標はマウス座標そのまま
			}
			else
			{
				// 上記以外の場合は追尾しない
				this.HomingMode = HomingMode.Empty;
			}
		}


		/// <summary>
		/// 楽譜データとなる動物リストを取得する。コードが長くなるのを防ぐための措置であり、実際には
		/// this.Parent.CompositionMain.AnimalScore.AnimalList を参照してるだけ。
		/// </summary>
		private System.Collections.Generic.List<Animal> AnimalList
		{
			get
			{
				return this.Parent.CompositionMain.AnimalScore.AnimalList;
			}
		}

	}
}
