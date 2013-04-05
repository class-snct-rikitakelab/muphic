using System.Drawing;
using Muphic.Manager;

namespace Muphic.CompositionScreenParts.CompositionMainParts
{
	/// <summary>
	/// 再生時の家の窓を管理するクラス。
	/// </summary>
	public class HouseLight : Common.Parts
	{
		/// <summary>
		/// 窓の発行時間を管理する配列。
		/// </summary>
		private int[] LightCount { get; set; }

		/// <summary>
		/// 家の窓を配置する位置を示す座標の配列。9 つ全ての窓の座標を予めこの配列に生成しておく。
		/// </summary>
		private readonly Point[] houseWindow;


		/// <summary>
		/// 再生時の窓管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		public HouseLight()
		{
			this.LightCount = new int[9];					// ド～ド＋レの９音分
			this.Reset();									// すべて初期化

			this.houseWindow = new Point[9];				// ド～ド＋レの９音分
			for (int i = 0; i < houseWindow.Length; i++)
			{												// X 座標は固定で、Y 座標は高さに応じ変更し窓の座標を生成
				this.houseWindow[i] = new Point(Locations.HouseWindow.X, Locations.HouseWindow.Y + Locations.ScoreAnimalSize.Height * i);
			}

			DrawManager.Regist(this.ToString(), 0, 0, "IMAGE_COMPOSITIONSCR_HOUSEWINDOW");
		}


		/// <summary>
		/// 窓の発光を全てキャンセルする。
		/// </summary>
		public void Reset()
		{
			for (int i = 0; i < this.LightCount.Length; i++) this.LightCount[i] = 0;
		}


		/// <summary>
		/// 窓の発光を追加する。
		/// 設定されたフレーム数だけ発光する
		/// </summary>
		/// <param name="code">発光させる音階。</param>
		public void Add(int code)
		{
			this.LightCount[code - 1] = Settings.System.Default.Composition_HouseLightTime;
		}


		/// <summary>
		/// 窓の発光を描画する
		/// </summary>
		public void Draw()
		{
			for (int i = 0; i < this.LightCount.Length; i++)
			{
				if (this.LightCount[i] > 0)
				{
					// 所定の位置に窓を描画
					// 残りフレーム数が10以下の場合は光を弱めていく演出を加える
					DrawManager.Draw(this.ToString(),
						this.houseWindow[i].X,
						this.houseWindow[i].Y,
						(this.LightCount[i] < 10)? (byte)(this.LightCount[i] / 10.0 * 255) : (byte)255);

					// カウントを一つ減らす
					this.LightCount[i]--;
				}
			}
		}
	}
}
