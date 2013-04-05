using System.Drawing;

namespace Muphic.MakeStoryScreenParts
{
	/// <summary>
	/// 物語の文章の表示を行うクラス。
	/// </summary>
	public class StorySentence : Common.Screen
	{
		/// <summary>
		/// 親にあたる物語作成画面クラス。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// 現在編集中のスライドの文章を表す文字列を取得する。
		/// </summary>
		public string Sentence
		{
			get
			{
				return this.Parent.CurrentStoryData.SlideList[this.Parent.CurrentPage].Sentence;
			}
		}


		/// <summary>
		/// 文章表示クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public StorySentence(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			Manager.DrawManager.Regist(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_Sentence, "IMAGE_MAKESTORYSCR_SENTENCEAREA");
		}


		/// <summary>
		/// 文章の文字列及びその背景テクスチャの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			Manager.StringManager.Draw(this.Sentence, Settings.PartsLocation.Default.MakeStoryScr_Sentence);
		}
	}
}
