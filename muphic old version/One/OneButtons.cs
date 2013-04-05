using System;
using muphic.One.RightButtons;

namespace muphic.One
{
	public enum OneButtonsClickMode {None, Sheep, Rabbit, Bird, Dog, Pig, Cat, Voice, Cancel};//CancelはOneScreenの下にある
	/// <summary>
	/// OneButtons の概要の説明です。
	/// </summary>
	public class OneButtons : Screen
	{
		public OneScreen parent;
		private OneButtonsClickMode nowClick;
		private SheepButton sheep;
		private RabbitButton rabbit;
		private BirdButton bird;
		private DogButton dog;
		private PigButton pig;
		private CatButton cat;
		private VoiceButton voice;

		public OneButtonsClickMode NowClick
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
					case muphic.One.OneButtonsClickMode.None:
						parent.tsuibi.State = 0;
						break;
					case muphic.One.OneButtonsClickMode.Bird:
						parent.tsuibi.State = 1;
						break;
					case muphic.One.OneButtonsClickMode.Dog:
						parent.tsuibi.State = 2;
						break;
					case muphic.One.OneButtonsClickMode.Pig:
						parent.tsuibi.State = 3;
						break;
					case muphic.One.OneButtonsClickMode.Rabbit:
						parent.tsuibi.State = 4;
						break;
					case muphic.One.OneButtonsClickMode.Sheep:
						parent.tsuibi.State = 5;
						break;
					case muphic.One.OneButtonsClickMode.Cat:
						parent.tsuibi.State = 6;
						break;
					case muphic.One.OneButtonsClickMode.Voice:
						parent.tsuibi.State = 7;
						break;
					case muphic.One.OneButtonsClickMode.Cancel:
						parent.tsuibi.State = 8;
						break;
					default:
						parent.tsuibi.State = 0;						//とりあえず鳥にしとく
						break;
				}
			}
		}

		public OneButtons(OneScreen one)
		{
			parent = one;
	
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
			int space = 76;//679,176 after896,199
			//before 690,175
			System.Drawing.Point p = new System.Drawing.Point(904, 201);
			muphic.DrawManager.Regist(sheep.ToString(), p.X, p.Y+space*0, "image\\one\\button\\animal\\sheep\\sheep_off.png", "image\\one\\button\\animal\\sheep\\sheep_on.png");
			muphic.DrawManager.Regist(rabbit.ToString(), p.X, p.Y+space*1, "image\\one\\button\\animal\\rabbit\\rabbit_off.png", "image\\one\\button\\animal\\rabbit\\rabbit_on.png");
			muphic.DrawManager.Regist(bird.ToString(), p.X, p.Y+space*2, "image\\one\\button\\animal\\bird\\bird_off.png", "image\\one\\button\\animal\\bird\\bird_on.png");
			muphic.DrawManager.Regist(dog.ToString(), p.X, p.Y+space*3, "image\\one\\button\\animal\\dog\\dog_off.png", "image\\one\\button\\animal\\dog\\dog_on.png");
			muphic.DrawManager.Regist(pig.ToString(), p.X, p.Y+space*4, "image\\one\\button\\animal\\pig\\pig_off.png", "image\\one\\button\\animal\\pig\\pig_on.png");
			muphic.DrawManager.Regist(cat.ToString(), p.X, p.Y+space*5, "image\\one\\button\\animal\\cat\\cat_off.png", "image\\one\\button\\animal\\cat\\cat_on.png");
			muphic.DrawManager.Regist(voice.ToString(), p.X, p.Y+space*6, new string[] {"image\\one\\button\\animal\\voice\\voice_off.png", "image\\one\\button\\animal\\voice\\voice_on.png", "image\\one\\button\\animal\\voice\\voice_non.png"} );


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

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).MouseLeave();
			}
		}

		public void AllClear()
		{
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).State = 0;							//本来のクリック処理を行う前に
			}															//すべての要素をクリックしていない状態に戻す
			parent.score.clear.State = 0;
		}
	}
}
