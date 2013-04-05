using System;
using muphic.Story.RightButtons;

namespace muphic.Story
{
	public enum StoryButtonsClickMode {None, Sheep, Rabbit, Bird, Dog, Pig, Cat, Voice, Cancel};//CancelはStoryScreenの下にある
	/// <summary>
	/// StoryButtons の概要の説明です。
	/// </summary>
	public class StoryButtons : Screen
	{
		public StoryScreen parent;
		private StoryButtonsClickMode nowClick;
		private SheepButton sheep;
		private RabbitButton rabbit;
		private BirdButton bird;
		private DogButton dog;
		private PigButton pig;
		private CatButton cat;
		private VoiceButton voice;

		public StoryButtonsClickMode NowClick
		{
			get
			{
				return nowClick;
			}
			set
			{
				AllClear();
				nowClick = value;
				switch(value)
				{
					case muphic.Story.StoryButtonsClickMode.None:
						parent.tsuibi.State = 0;
						break;
					case muphic.Story.StoryButtonsClickMode.Bird:
						parent.tsuibi.State = 1;
						break;
					case muphic.Story.StoryButtonsClickMode.Dog:
						parent.tsuibi.State = 2;
						break;
					case muphic.Story.StoryButtonsClickMode.Pig:
						parent.tsuibi.State = 3;
						break;
					case muphic.Story.StoryButtonsClickMode.Rabbit:
						parent.tsuibi.State = 4;
						break;
					case muphic.Story.StoryButtonsClickMode.Sheep:
						parent.tsuibi.State = 5;
						break;
					case muphic.Story.StoryButtonsClickMode.Cat:
						parent.tsuibi.State = 6;
						break;
					case muphic.Story.StoryButtonsClickMode.Voice:
						parent.tsuibi.State = 7;
						break;
					case muphic.Story.StoryButtonsClickMode.Cancel:
						parent.tsuibi.State = 8;
						break;
					default:
						parent.tsuibi.State = 0;						//とりあえずNoneにしとく
						break;
				}
			}
		}

		public StoryButtons(StoryScreen story)
		{
			parent = story;
	
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			sheep = new SheepButton(this);
			rabbit = new RabbitButton(this);
			bird = new BirdButton(this);
			dog = new DogButton(this);
			pig = new PigButton(this);
			cat = new CatButton(this);
			voice = new VoiceButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			int space = 76;
			System.Drawing.Point p = new System.Drawing.Point(904, 201);
			muphic.DrawManager.Regist(sheep.ToString(), p.X, p.Y+space*0, "image\\one\\button\\animal\\sheep\\sheep_off.png", "image\\one\\button\\animal\\sheep\\sheep_on.png");
			muphic.DrawManager.Regist(rabbit.ToString(), p.X, p.Y+space*1, "image\\one\\button\\animal\\rabbit\\rabbit_off.png", "image\\one\\button\\animal\\rabbit\\rabbit_on.png");
			muphic.DrawManager.Regist(bird.ToString(), p.X, p.Y+space*2, "image\\one\\button\\animal\\bird\\bird_off.png", "image\\one\\button\\animal\\bird\\bird_on.png");
			muphic.DrawManager.Regist(dog.ToString(), p.X, p.Y+space*3, "image\\one\\button\\animal\\dog\\dog_off.png", "image\\one\\button\\animal\\dog\\dog_on.png");
			muphic.DrawManager.Regist(pig.ToString(), p.X, p.Y+space*4, "image\\one\\button\\animal\\pig\\pig_off.png", "image\\one\\button\\animal\\pig\\pig_on.png");
			muphic.DrawManager.Regist(cat.ToString(), p.X, p.Y+space*5, "image\\one\\button\\animal\\cat\\cat_off.png", "image\\one\\button\\animal\\cat\\cat_on.png");
			muphic.DrawManager.Regist(voice.ToString(), p.X, p.Y + space * 6, new string[] { "image\\one\\button\\animal\\voice\\voice_off.png", "image\\one\\button\\animal\\voice\\voice_on.png", "image\\one\\button\\animal\\voice\\voice_non.png" });


			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(sheep);
			BaseList.Add(rabbit);
			BaseList.Add(bird);
			BaseList.Add(dog);
			BaseList.Add(pig);
			BaseList.Add(cat);
			BaseList.Add(voice);
		}

		public void AllClear()
		{
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).State = 0;							//本来のクリック処理を行う前に
			}															//すべての要素をクリックしていない状態に戻す
			parent.score.clear.State = 0;

			if (!muphic.Common.CommonSettings.getEnableVoice()) this.voice.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

	}
}
