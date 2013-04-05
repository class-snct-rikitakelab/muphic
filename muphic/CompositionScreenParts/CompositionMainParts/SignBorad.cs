using System.Drawing;
using Muphic.Manager;

namespace Muphic.CompositionScreenParts.CompositionMainParts
{
	/// <summary>
	/// 小節区切りを表わす看板のクラス。
	/// </summary>
	public class SignBorad : Common.Screen
	{
		/// <summary>
		/// 親にあたる作曲部
		/// </summary>
		public CompositionMain Parent { get; private set; }


		#region 描画位置に関するプロパティ 描画時の処理緩和のため

		/// <summary>
		/// 看板同士の間隔
		/// </summary>
		private int BoardSpace { get; set; }

		/// <summary>
		/// 最初の看板の描画x座標
		/// </summary>
		private int BoardStart { get; set; }

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
		/// 看板の表示位置を示す座標。
		/// </summary>
		private readonly Point __signBoard = Locations.SignBoard;

		/// <summary>
		/// 看板の表示位置を示す座標を取得する。
		/// </summary>
		private Point SignBoard
		{
			get { return this.__signBoard; }
		}

		#endregion


		/// <summary>
		/// 小節区切りを表わす看板クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionMain">親にあたる作曲部。</param>
		public SignBorad(CompositionMain compositionMain)
		{
			this.Parent = compositionMain;

			this.Initialization();
		}

		/// <summary>
		/// 看板の初期設定を行う。
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();

			// 看板テクスチャの登録 座標は描画時に計算する
			DrawManager.Regist(this.ToString(), 0, 0, "IMAGE_COMPOSITIONSCR_SIGNBOARD");

			// 看板と看板の間隔を設定
			this.BoardSpace = (Locations.ScoreAnimalSize.Width) * 4;

			// 最初の看板の描画位置を設定  楽譜開始位置から看板テクスチャの横幅の半分の値を引き、看板1つ分の間隔を足すと1枚目の看板の座標になる
			this.BoardStart = Locations.ScoreBasePoint.X - TextureFileManager.GetRectangle("IMAGE_COMPOSITIONSCR_SIGNBOARD").Width / 2 + this.BoardSpace;
		}


		/// <summary>
		/// 小節区切りを表す看板を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			// 最初の看板のx座標を得る
			int location = this.BoardStart;

			int num = (1 + this.Parent.NowPlace + Settings.System.Default.CompositionMeterNum) / (4 * Settings.System.Default.CompositionMeterNum);

			// 看板のx座標が楽譜外に出るまで看板を連続的に描画する
			while (location < this.ScoreArea.Right)
			{
				DrawManager.Draw(this.ToString(), location, this.SignBoard.Y);
				StringManager.SystemDrawCenter(++num, location + RectangleManager.Get(this.ToString()).Width / 2, this.SignBoard.Y + 18);
				
				location += this.BoardSpace;
			}
		}
	}
}
