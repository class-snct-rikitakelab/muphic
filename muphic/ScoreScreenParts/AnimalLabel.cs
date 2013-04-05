
namespace Muphic.ScoreScreenParts
{
	/// <summary>
	/// 楽譜の背景に表示される動物のラベルを管理するクラス。
	/// </summary>
	public class AnimalLabel : Common.Parts
	{
		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		public ScoreScreen Parent { get; private set; }

		/// <summary>
		/// 楽譜の背景に表示される動物のラベル管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public AnimalLabel(ScoreScreen parent)
		{
			this.Parent = parent;

			Manager.DrawManager.Regist(
				this.ToString(),
				Locations.AnimalLabel,
				"IMAGE_DUMMY",
				"IMAGE_COMMON_ANIMAL_SHEEP",
				"IMAGE_COMMON_ANIMAL_BIRD",
				"IMAGE_COMMON_ANIMAL_RABBIT",
				"IMAGE_COMMON_ANIMAL_DOG",
				"IMAGE_COMMON_ANIMAL_PIG",
				"IMAGE_COMMON_ANIMAL_CAT",
				"IMAGE_COMMON_ANIMAL_VOICE"
			);
		}


		/// <summary>
		/// 楽譜の背景に表示される動物のラベルを設定する。
		/// </summary>
		/// <param name="scoreMode">表示される楽譜の種類。</param>
		public void SetLabel(ScoreScreenScoreMode scoreMode)
		{
			switch (scoreMode)
			{
				case ScoreScreenScoreMode.FullScore:
					this.State = 0;
					break;

				case ScoreScreenScoreMode.Sheep:
					this.State = 1;
					break;

				case ScoreScreenScoreMode.Bird:
					this.State = 2;
					break;

				case ScoreScreenScoreMode.Rabbit:
					this.State = 3;
					break;

				case ScoreScreenScoreMode.Dog:
					this.State = 4;
					break;

				case ScoreScreenScoreMode.Pig:
					this.State = 5;
					break;

				case ScoreScreenScoreMode.Cat:
					this.State = 6;
					break;

				case ScoreScreenScoreMode.Voice:
					this.State = 7;
					break;

				default:
					goto case ScoreScreenScoreMode.FullScore;
			}
		}
	}
}
