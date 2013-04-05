using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// DeleteTexture の概要の説明です。
	/// </summary>
	public class DeleteTexture : Screen
	{
		public ScoreScreen parent;

		public DeleteTexture(ScoreScreen screen)
		{
			this.parent = screen;
		}

		/// <summary>
		/// 楽譜画面で使用したテクスチャを全て削除する
		/// </summary>
		public void DeleteAllScoreTexture()
		{
			DrawManager.Delete(parent.ToString());
			DrawManager.Delete(parent.back.ToString());
			DrawManager.Delete(parent.read.ToString());
			DrawManager.Delete(parent.write.ToString());
			DrawManager.Delete(parent.up.ToString());
			DrawManager.Delete(parent.down.ToString());
			DrawManager.Delete(parent.scorewindow.ToString());
			DrawManager.Delete(parent.scores.ToString());

			DrawManager.Delete("QuarterNotes", 0, 0, "image\\Score\\note\\4buonpu.png");
			DrawManager.Delete("QuarterNotes_", 0, 0, "image\\Score\\note\\4buonpu_.png");
			DrawManager.Delete("QuarterNotes_do", 0, 0, "image\\Score\\note\\4buonpu_do.png");
			DrawManager.Delete("EighthNotes", 0, 0, "image\\Score\\note\\8buonpu.png");
			DrawManager.Delete("EighthNotes_", 0, 0, "image\\Score\\note\\8buonpu_.png");
			DrawManager.Delete("EighthNotes_do", 0, 0, "image\\Score\\note\\8buonpu_do.png");
			DrawManager.Delete("QuarterRest", 0, 0, "image\\Score\\note\\4bukyuhu.png");
			DrawManager.Delete("EighthRest", 0, 0, "image\\Score\\note\\8bukyuhu.png");
			DrawManager.Delete("AllRest", 0, 0, "image\\Score\\note\\zenkyuhu.png");
			DrawManager.Delete("Meter", 0, 0, "image\\Score\\note\\hyousi.png");
			DrawManager.Delete("End", 0, 0, "image\\Score\\note\\end.png");
			DrawManager.Delete("End_full", 0, 0, "image\\Score\\note\\end_full.png");
			DrawManager.Delete("Staff", 0, 0, "image\\Score\\score\\gosen.png");
			DrawManager.Delete("Line", 0, 0, "image\\Score\\score\\syousetu.png");
			DrawManager.Delete("Full", 0, 0, "image\\Score\\score\\full_line.png");

			DrawManager.Delete("SheepBig", 0, 0, "image\\Score\\omake\\Sheep_big.png");
			DrawManager.Delete("RabbitBig", 0, 0, "image\\Score\\omake\\Rabbit_big.png");
			DrawManager.Delete("BirdBig", 0, 0, "image\\Score\\omake\\Bird_big.png");
			DrawManager.Delete("DogBig", 0, 0, "image\\Score\\omake\\Dog_big.png");
			DrawManager.Delete("PigBig", 0, 0, "image\\Score\\omake\\Pig_big.png");

			DrawManager.Delete(parent.sedialog.ToString());
			DrawManager.Delete(parent.sedialog.sbs.ToString());
			DrawManager.Delete(parent.sedialog.upper.ToString());
			DrawManager.Delete(parent.sedialog.lower.ToString());
			DrawManager.Delete(parent.sedialog.back.ToString());
		}
	}
}
