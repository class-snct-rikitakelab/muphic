using Muphic.Manager;

namespace Muphic.CompositionScreenParts.CompositionMainParts
{
	/// <summary>
	/// 作曲部の楽譜(道)の背景を管理するクラス。
	/// </summary>
	public class ScoreBackground : Common.Screen
	{
		/// <summary>
		/// 親にあたる作曲部
		/// </summary>
		public CompositionMain Parent { get; private set; }


		/// <summary>
		/// 楽譜背景管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionMain">親にあたる作曲部。</param>
		public ScoreBackground(CompositionMain compositionMain)
		{
			this.Parent = compositionMain;

			DrawManager.Regist(this.ToString(), Locations.Score, "IMAGE_COMPOSITIONSCR_SCORE");
		}


		/// <summary>
		/// 楽譜の背景を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			DrawManager.Draw(this.ToString());
		}
	}
}
