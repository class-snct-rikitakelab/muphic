using System;
using muphic;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;
using muphic.Titlemode;

namespace muphic
{
	public enum MakeStoryScreenMode { MakeStoryScreen, StoryScreen, ScoreScreen, SelectDialog, SaveDialog, MakeDialog, PrintDialog, PlayStoryScreen, TitleScreen, AllClearDialog};
	/// <summary>
	/// MakeStoryScreen の概要の説明です。
	/// </summary>
	public class MakeStoryScreen : Screen
	{
        public TopScreen parent;
		public Screen Screen;

		//トップ画面戻る
		public muphic.MakeStory.BackButton bb;
		//画面上部のサムネイル用
		public Thumbnail thumb;
		//画面中央の編集中のスライド表示用
		public muphic.MakeStory.Window wind;
		//スライドのクリア，モノのクリア
		public AllClearButton acb;
		public ClearButton cb;
		public bool isClear = false;
		public int BeforeState = 0;
		public int BeforeTsuibi = 0;

		//タイトル編集画面へのボタン
		public TitleButton tb;
		public SentenceButton sentB;
		//
		public muphic.Titlemode.TargetMode TitOrSent;
		

		public StoryButton storyB;

		public StoryMakeButton storymakeB;
		public StorySelectButton storyselectB;
		public StorySaveButton storysaveB;
		public StoryPrintButton storyprintB;

		public StoryPlayButton storyplayB;

		public DesignList design;
		//モノ：人関係
		public ManButton man;
		public LadyButton lady;
		public GirlButton girl;
		public BoyButton boy;

		//モノ：動物関係
		public DogButton dog;
		public BearButton bear;
		public TurtleButton turtle;
		public BirdButton bird;
		public LivesDesignList[] LivesDl;

		//モノ：背景関係
		public ForestButton forest;
		public RiverButton river;
		public TownButton town;
		public RoomButton room;
		public BGDesignList[] BGDl;

		//モノ：小物関係
		public GoodsButton goods;
		public FashionButton fashion;
		public FoodButton food;
		public FurnitureButton furniture;
		public ItemDesignList[] ItemDl;
		//配置する際にマウスカーソルについてくるモノ
		public muphic.MakeStory.Tsuibi tsuibi;

		//モノ配置用ボタンの選択による
		//リスト表示に使用
		ButtonsMode Bmode;
		public int buttonmode;
		public ButtonsMode ButtonsMode
		{
			get
			{
				return Bmode;
			}
			set
			{
				Bmode = value;
				switch(value)
				{
					case muphic.MakeStory.ButtonsMode.None:
						buttonmode = -1;
						tsuibi.State = 0;
						design = new DesignList();
						break;
					case muphic.MakeStory.ButtonsMode.Man:
						buttonmode = 13*0;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Man));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Lady:
						buttonmode = 13*1;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Lady));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Girl:
						buttonmode = 13*2;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Girl));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Boy:
						buttonmode = 13*3;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Boy));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Wolf:
						buttonmode = 13*4;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Wolf));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Bear:
						buttonmode = 13*5;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Bear));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Turtle:
						buttonmode = 13*6;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Turtle));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Sparrow:
						buttonmode = 13*7;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new LivesDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Sparrow));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Forest:
						buttonmode = 6*0;
						tsuibi.State = 0;
						this.wind.backscreen = 0;
						//BaseList.Remove(design);
						design = new BGDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Forest));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.River:
						buttonmode = 6*1;
						tsuibi.State = 0;
						this.wind.backscreen = 0;
						//BaseList.Remove(design);
						design = new BGDesignList(this, (int)(muphic.MakeStory.ButtonsMode.River));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Town:
						buttonmode = 6*2;
						tsuibi.State = 0;
						this.wind.backscreen = 0;
						//BaseList.Remove(design);
						design = new BGDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Town));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Room:
						buttonmode = 6*3;
						tsuibi.State = 0;
						this.wind.backscreen = 0;
						//BaseList.Remove(design);
						design = new BGDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Room));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Goods:
						buttonmode = 13*8 +8*0;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new ItemDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Goods));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Fashion:
						buttonmode = 13*8 +8*1;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new ItemDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Fashion));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Food:
						buttonmode = 13*8 +8*2;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new ItemDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Food));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Furniture:
						buttonmode = 13*8 +8*3;
						tsuibi.State = 0;
						//BaseList.Remove(design);
						design = new ItemDesignList(this, (int)(muphic.MakeStory.ButtonsMode.Furniture));
						//BaseList.Add(design);
						break;
					case muphic.MakeStory.ButtonsMode.Clear:
						tsuibi.State = 13*8 +8*4+1;
						break;
					default:
						tsuibi.State = 0;
						break;
				}
			}
		}

		private MakeStoryScreenMode storyscreenmode;
		/// <summary>
		/// ものがたり音楽画面のScreenMode
		/// 楽譜画面や物語作成画面、ダイアログなどへの切り替えに使用
		/// </summary>
		public MakeStoryScreenMode MakeStoryScreenMode
		{
			get
			{
				return storyscreenmode;
			}
			set
			{
				storyscreenmode = value;
				switch (value)
				{
					case MakeStoryScreenMode.MakeStoryScreen:
						//子のウィンドウは表示せず、自分自身を描画する
						Screen = null;
						break;
					case MakeStoryScreenMode.StoryScreen:
						//Storyをインスタンス化
						Screen = new StoryScreen(this, this.PictureStory.Slide[this.NowPage], this.NowPage);
						break;
					case MakeStoryScreenMode.SelectDialog:
						//SelectDialogをインスタンス化
						Screen = new StorySelectDialog(this);
						break;
					case MakeStoryScreenMode.SaveDialog:
						//SaveDialogをインスタンス化
						Screen = new StorySaveDialog(this);
						break;
					case MakeStoryScreenMode.MakeDialog:
						//MakeDialogをインスタンス化
						Screen = new StoryMakeDialog(this);
						break;
					case MakeStoryScreenMode.PrintDialog:
						//PrintDialogをインスタンス化
						Screen = new StoryPrintDialog(this);
						break;
					case MakeStoryScreenMode.PlayStoryScreen:
						Screen = new MakeStory.Play.PlayScreen(this);
						break;
					case MakeStoryScreenMode.TitleScreen:
						DrawManager.BeginRegist(138);
						Screen = new TitleScreen(this,TitOrSent);
						DrawManager.EndRegist();
						break;
					case MakeStoryScreenMode.AllClearDialog:
						Screen = new muphic.Common.AllClearDialog(this);
						break;
					default:
						Screen = null;
						break;
				}
			}
		}

		public PictStory PictureStory;
		public const int PageNum = 10;
		public int NowPage;											//現在何枚目のスライドの音楽を作曲しているか

        public MakeStoryScreen(TopScreen parent)
		{
			this.parent = parent;

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			DrawManager.BeginRegist(389);
			//Top画面に戻るボタン
			bb = new muphic.MakeStory.BackButton(this);
			//画面上部にサムネイルを表示するウィンドウ
			thumb = new Thumbnail(this);
			//画面中央編集中のスライドを表示するウィンドウ
			wind = new muphic.MakeStory.Window(this);
			//編集中のスライドの要素を全削除
			acb = new AllClearButton(this);
			//編集中のスライドで削除するものを選択するためのボタン
			cb = new ClearButton(this);
			//タイトル編集画面へ画面遷移させるボタン
			tb = new TitleButton(this);

			sentB = new SentenceButton(this);

			storyB = new StoryButton(this);

			storymakeB = new StoryMakeButton(this);
			storyselectB = new StorySelectButton(this);
			storysaveB = new StorySaveButton(this);
			storyprintB = new StoryPrintButton(this);

			storyplayB = new StoryPlayButton(this);
			
			design = new DesignList();

			man = new ManButton(this);
			lady = new LadyButton(this);
			girl = new GirlButton(this);
			boy = new BoyButton(this);

			dog = new DogButton(this);
			bear = new BearButton(this);
			turtle = new TurtleButton(this);
			bird = new BirdButton(this);
			LivesDl = new LivesDesignList[8];

			forest = new ForestButton(this);
			river = new RiverButton(this);
			town = new TownButton(this);
			room = new RoomButton(this);
			BGDl = new BGDesignList[4];
			
			goods = new GoodsButton(this);
			fashion = new FashionButton(this);
			food = new FoodButton(this);
			furniture = new FurnitureButton(this);
			ItemDl = new ItemDesignList[4];

			tsuibi = new muphic.MakeStory.Tsuibi(this);
			tsuibi.Visible = false;

            PictureStory = new PictStory();
			this.NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			int Xspace = 80;int Yspace = 36;
			//右側ボタンのX座標，上方ボタンのY座標
			int RButtonX = 855;int UButtonY = 220;
			//左側ボタンのX座標，下方ボタンのY座標
			int LButtonX = 10;int LButtonY = 430;
			//選択リストの座標
			int ListX = 150;int ListY = 645;

            muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\MakeStory\\background.png");
            muphic.DrawManager.Regist("MAKE", 0, 0, "image\\MakeStory\\MakeStoryXGA.png");
			
            muphic.DrawManager.Regist(bb.ToString(), 11, 3, "image\\MakeStory\\button\\back_off.png", "image\\MakeStory\\button\\back_on.png");
            muphic.DrawManager.Regist(thumb.ToString(), 182, 161, "image\\MakeStory\\thumbnail\\thumbnail_back.png");
			//number
			muphic.DrawManager.Regist("10", 0, 0, "image\\MakeStory\\thumbnail\\number\\0.png");
			muphic.DrawManager.Regist("1", 0, 0, "image\\MakeStory\\thumbnail\\number\\1.png");
			muphic.DrawManager.Regist("2", 0, 0, "image\\MakeStory\\thumbnail\\number\\2.png");
			muphic.DrawManager.Regist("3", 0, 0, "image\\MakeStory\\thumbnail\\number\\3.png");
			muphic.DrawManager.Regist("4", 0, 0, "image\\MakeStory\\thumbnail\\number\\4.png");
			muphic.DrawManager.Regist("5", 0, 0, "image\\MakeStory\\thumbnail\\number\\5.png");
			muphic.DrawManager.Regist("6", 0, 0, "image\\MakeStory\\thumbnail\\number\\6.png");
			muphic.DrawManager.Regist("7", 0, 0, "image\\MakeStory\\thumbnail\\number\\7.png");
			muphic.DrawManager.Regist("8", 0, 0, "image\\MakeStory\\thumbnail\\number\\8.png");
			muphic.DrawManager.Regist("9", 0, 0, "image\\MakeStory\\thumbnail\\number\\9.png");
			muphic.DrawManager.Regist("select", 0, 0, "image\\MakeStory\\thumbnail\\miniwindow_on.png");
			muphic.DrawManager.Regist("AddMusic", 0, 0, @"image\MakeStory\thumbnail\melody.png");
            
			
			muphic.DrawManager.Regist(wind.ToString(), 177, 262, "image\\MakeStory\\parts\\window.png");

            muphic.DrawManager.Regist(acb.ToString(), 745, 647, "image\\MakeStory\\button\\allclear_off.png", "image\\MakeStory\\button\\allclear_on.png");
            muphic.DrawManager.Regist(cb.ToString(), 629, 646, "image\\MakeStory\\button\\clear_off.png", "image\\MakeStory\\button\\clear_on.png");
			
			muphic.DrawManager.Regist(tb.ToString(), 176, 100, "image\\MakeStory\\button\\title_off.png", "image\\MakeStory\\button\\title_on.png");
			muphic.DrawManager.Regist("TitleNameWindow", 293, 100, "image\\MakeStory\\parts\\title.png");
			muphic.DrawManager.Regist(sentB.ToString(), 28, 710, "image\\MakeStory\\button\\sentence_off.png", "image\\MakeStory\\button\\sentence_on.png");
			muphic.DrawManager.Regist("SentenceWindow", 146, 709, "image\\MakeStory\\parts\\sentence.png");

			muphic.DrawManager.Regist(storyB.ToString(), 897, 658, "image\\MakeStory\\button\\putmelody_off.png", "image\\MakeStory\\button\\putmelody_on.png");

			muphic.DrawManager.Regist(storymakeB.ToString(), 174, 9, "image\\MakeStory\\button\\make_off.png", "image\\MakeStory\\button\\make_on.png");
			muphic.DrawManager.Regist(storyselectB.ToString(), 365, 10, "image\\MakeStory\\button\\read_off.png", "image\\MakeStory\\button\\read_on.png");
			muphic.DrawManager.Regist(storysaveB.ToString(), 549, 10, "image\\MakeStory\\button\\save_off.png", "image\\MakeStory\\button\\save_on.png");
			muphic.DrawManager.Regist(storyprintB.ToString(), 735, 10, new string[] {"image\\MakeStory\\button\\print_off.png", "image\\MakeStory\\button\\print_on.png", "image\\MakeStory\\button\\print_non.png"} );
			
			muphic.DrawManager.Regist(storyplayB.ToString(), 866, 94, "image\\MakeStory\\button\\play_off.png", "image\\MakeStory\\button\\play_on.png");
			
			muphic.DrawManager.Regist(design.ToString(), ListX, ListY, "image\\MakeStory\\parts\\listback.png");
			muphic.DrawManager.Regist("SignHuman", 862, UButtonY+10, "image\\MakeStory\\button\\human\\human.png");
			muphic.DrawManager.Regist(man.ToString(), RButtonX + Xspace * 1, UButtonY + Yspace * 1, "image\\MakeStory\\button\\human\\man\\off.png", "image\\MakeStory\\button\\human\\man\\on.png");
			muphic.DrawManager.Regist(lady.ToString(), RButtonX + Xspace * 0, UButtonY + Yspace * 2, "image\\MakeStory\\button\\human\\lady\\off.png", "image\\MakeStory\\button\\human\\lady\\on.png");
			muphic.DrawManager.Regist(girl.ToString(), RButtonX + Xspace * 1, UButtonY + Yspace * 3, "image\\MakeStory\\button\\human\\girl\\off.png", "image\\MakeStory\\button\\human\\girl\\on.png");
			muphic.DrawManager.Regist(boy.ToString(), RButtonX + Xspace * 0, UButtonY + Yspace * 4, "image\\MakeStory\\button\\human\\boy\\off.png", "image\\MakeStory\\button\\human\\boy\\on.png");

			muphic.DrawManager.Regist("SignAnimal", 862, LButtonY+10, "image\\MakeStory\\button\\animal\\animal.png");
			muphic.DrawManager.Regist(dog.ToString(), RButtonX + Xspace * 1, LButtonY + Yspace * 1, "image\\MakeStory\\button\\animal\\dog\\off.png", "image\\MakeStory\\button\\animal\\dog\\on.png");
			muphic.DrawManager.Regist(bear.ToString(), RButtonX + Xspace * 0, LButtonY + Yspace * 2, "image\\MakeStory\\button\\animal\\bear\\off.png", "image\\MakeStory\\button\\animal\\bear\\on.png");
			muphic.DrawManager.Regist(turtle.ToString(), RButtonX + Xspace * 1, LButtonY + Yspace * 3, "image\\MakeStory\\button\\animal\\turtle\\off.png", "image\\MakeStory\\button\\animal\\turtle\\on.png");
			muphic.DrawManager.Regist(bird.ToString(), RButtonX + Xspace * 0, LButtonY + Yspace * 4, "image\\MakeStory\\button\\animal\\bird\\off.png", "image\\MakeStory\\button\\animal\\bird\\on.png");

			muphic.DrawManager.Regist("SignBG",17, UButtonY+10, "image\\MakeStory\\button\\background\\background.png");
			muphic.DrawManager.Regist(forest.ToString(), LButtonX + Xspace * 1, UButtonY + Yspace * 1, "image\\MakeStory\\button\\background\\forest\\off.png", "image\\MakeStory\\button\\background\\forest\\on.png");
			muphic.DrawManager.Regist(river.ToString(), LButtonX + Xspace * 0, UButtonY + Yspace * 2, "image\\MakeStory\\button\\background\\river\\off.png", "image\\MakeStory\\button\\background\\river\\on.png");
			muphic.DrawManager.Regist(room.ToString(), LButtonX + Xspace * 1, UButtonY + Yspace * 3, "image\\MakeStory\\button\\background\\room\\off.png", "image\\MakeStory\\button\\background\\room\\on.png");
			muphic.DrawManager.Regist(town.ToString(), LButtonX + Xspace * 0, UButtonY + Yspace * 4, "image\\MakeStory\\button\\background\\town\\off.png", "image\\MakeStory\\button\\background\\town\\on.png");

            muphic.DrawManager.Regist("SignItem", 17, LButtonY+10, "image\\MakeStory\\button\\item\\item.png");
			muphic.DrawManager.Regist(goods.ToString(), LButtonX + Xspace * 1, LButtonY + Yspace * 1, "image\\MakeStory\\button\\item\\goods\\off.png", "image\\MakeStory\\button\\item\\goods\\on.png");
			muphic.DrawManager.Regist(fashion.ToString(), LButtonX + Xspace * 0, LButtonY + Yspace * 2, "image\\MakeStory\\button\\item\\fashion\\off.png", "image\\MakeStory\\button\\item\\fashion\\on.png");
			muphic.DrawManager.Regist(food.ToString(), LButtonX + Xspace * 1, LButtonY + Yspace * 3, "image\\MakeStory\\button\\item\\food\\off.png", "image\\MakeStory\\button\\item\\food\\on.png");
			muphic.DrawManager.Regist(furniture.ToString(), LButtonX + Xspace * 0, LButtonY + Yspace * 4, "image\\MakeStory\\button\\item\\furniture\\off.png", "image\\MakeStory\\button\\item\\furniture\\on.png");

			#region 追尾用画像登録
			muphic.DrawManager.Regist(tsuibi.ToString(), 0, 0, new String[] 
                    {
                        "image\\MakeStory\\none.png",

                        "image\\MakeStory\\button\\human\\man\\front\\glad.png", "image\\MakeStory\\button\\human\\man\\front\\angry.png",
                        "image\\MakeStory\\button\\human\\man\\front\\sad.png",	"image\\MakeStory\\button\\human\\man\\front\\enjoy.png",
                        "image\\MakeStory\\button\\human\\man\\left\\glad.png", "image\\MakeStory\\button\\human\\man\\left\\angry.png",
                        "image\\MakeStory\\button\\human\\man\\left\\sad.png", "image\\MakeStory\\button\\human\\man\\left\\enjoy.png",
                        "image\\MakeStory\\button\\human\\man\\right\\glad.png", "image\\MakeStory\\button\\human\\man\\right\\angry.png",
                        "image\\MakeStory\\button\\human\\man\\right\\sad.png", "image\\MakeStory\\button\\human\\man\\right\\enjoy.png",
                        "image\\MakeStory\\button\\human\\man\\back\\back.png",
						
                        "image\\MakeStory\\button\\human\\lady\\front\\glad.png", "image\\MakeStory\\button\\human\\lady\\front\\angry.png",
                        "image\\MakeStory\\button\\human\\lady\\front\\sad.png", "image\\MakeStory\\button\\human\\lady\\front\\enjoy.png",
                        "image\\MakeStory\\button\\human\\lady\\left\\glad.png", "image\\MakeStory\\button\\human\\lady\\left\\angry.png",
                        "image\\MakeStory\\button\\human\\lady\\left\\sad.png",	"image\\MakeStory\\button\\human\\lady\\left\\enjoy.png",
                        "image\\MakeStory\\button\\human\\lady\\right\\glad.png", "image\\MakeStory\\button\\human\\lady\\right\\angry.png",
                        "image\\MakeStory\\button\\human\\lady\\right\\sad.png", "image\\MakeStory\\button\\human\\lady\\right\\enjoy.png",
                        "image\\MakeStory\\button\\human\\lady\\back\\back.png",

                        "image\\MakeStory\\button\\human\\girl\\front\\glad.png", "image\\MakeStory\\button\\human\\girl\\front\\angry.png",
                        "image\\MakeStory\\button\\human\\girl\\front\\sad.png", "image\\MakeStory\\button\\human\\girl\\front\\enjoy.png",
                        "image\\MakeStory\\button\\human\\girl\\left\\glad.png", "image\\MakeStory\\button\\human\\girl\\left\\angry.png",
                        "image\\MakeStory\\button\\human\\girl\\left\\sad.png",	"image\\MakeStory\\button\\human\\girl\\left\\enjoy.png",
                        "image\\MakeStory\\button\\human\\girl\\right\\glad.png", "image\\MakeStory\\button\\human\\girl\\right\\angry.png",
                        "image\\MakeStory\\button\\human\\girl\\right\\sad.png", "image\\MakeStory\\button\\human\\girl\\right\\enjoy.png",
                        "image\\MakeStory\\button\\human\\girl\\back\\back.png",

                        "image\\MakeStory\\button\\human\\boy\\front\\glad.png", "image\\MakeStory\\button\\human\\boy\\front\\angry.png",
                        "image\\MakeStory\\button\\human\\boy\\front\\sad.png",	"image\\MakeStory\\button\\human\\boy\\front\\enjoy.png",
                        "image\\MakeStory\\button\\human\\boy\\left\\glad.png",	"image\\MakeStory\\button\\human\\boy\\left\\angry.png",
                        "image\\MakeStory\\button\\human\\boy\\left\\sad.png", "image\\MakeStory\\button\\human\\boy\\left\\enjoy.png",
                        "image\\MakeStory\\button\\human\\boy\\right\\glad.png", "image\\MakeStory\\button\\human\\boy\\right\\angry.png",
                        "image\\MakeStory\\button\\human\\boy\\right\\sad.png", "image\\MakeStory\\button\\human\\boy\\right\\enjoy.png",
                        "image\\MakeStory\\button\\human\\boy\\back\\back.png",

                        "image\\MakeStory\\button\\animal\\dog\\front\\glad.png",	"image\\MakeStory\\button\\animal\\dog\\front\\angry.png",
                        "image\\MakeStory\\button\\animal\\dog\\front\\sad.png",	"image\\MakeStory\\button\\animal\\dog\\front\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\dog\\left\\glad.png",	"image\\MakeStory\\button\\animal\\dog\\left\\angry.png",
                        "image\\MakeStory\\button\\animal\\dog\\left\\sad.png",		"image\\MakeStory\\button\\animal\\dog\\left\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\dog\\right\\glad.png",	"image\\MakeStory\\button\\animal\\dog\\right\\angry.png",
                        "image\\MakeStory\\button\\animal\\dog\\right\\sad.png",	"image\\MakeStory\\button\\animal\\dog\\right\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\dog\\back\\back.png",
						
                        "image\\MakeStory\\button\\animal\\bear\\front\\glad.png",	"image\\MakeStory\\button\\animal\\bear\\front\\angry.png",
                        "image\\MakeStory\\button\\animal\\bear\\front\\sad.png",	"image\\MakeStory\\button\\animal\\bear\\front\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\bear\\left\\glad.png",	"image\\MakeStory\\button\\animal\\bear\\left\\angry.png",
                        "image\\MakeStory\\button\\animal\\bear\\left\\sad.png",	"image\\MakeStory\\button\\animal\\bear\\left\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\bear\\right\\glad.png",	"image\\MakeStory\\button\\animal\\bear\\right\\angry.png",
                        "image\\MakeStory\\button\\animal\\bear\\right\\sad.png",	"image\\MakeStory\\button\\animal\\bear\\right\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\bear\\back\\back.png",

                        "image\\MakeStory\\button\\animal\\turtle\\front\\glad.png",	"image\\MakeStory\\button\\animal\\turtle\\front\\angry.png",
                        "image\\MakeStory\\button\\animal\\turtle\\front\\sad.png",		"image\\MakeStory\\button\\animal\\turtle\\front\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\turtle\\left\\glad.png",		"image\\MakeStory\\button\\animal\\turtle\\left\\angry.png",
                        "image\\MakeStory\\button\\animal\\turtle\\left\\sad.png",		"image\\MakeStory\\button\\animal\\turtle\\left\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\turtle\\right\\glad.png",	"image\\MakeStory\\button\\animal\\turtle\\right\\angry.png",
                        "image\\MakeStory\\button\\animal\\turtle\\right\\sad.png",		"image\\MakeStory\\button\\animal\\turtle\\right\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\turtle\\back\\back.png",

                        "image\\MakeStory\\button\\animal\\bird\\front\\glad.png",	"image\\MakeStory\\button\\animal\\bird\\front\\angry.png",
                        "image\\MakeStory\\button\\animal\\bird\\front\\sad.png",	"image\\MakeStory\\button\\animal\\bird\\front\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\bird\\left\\glad.png",	"image\\MakeStory\\button\\animal\\bird\\left\\angry.png",
                        "image\\MakeStory\\button\\animal\\bird\\left\\sad.png",	"image\\MakeStory\\button\\animal\\bird\\left\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\bird\\right\\glad.png",	"image\\MakeStory\\button\\animal\\bird\\right\\angry.png",
                        "image\\MakeStory\\button\\animal\\bird\\right\\sad.png",	"image\\MakeStory\\button\\animal\\bird\\right\\enjoy.png",
                        "image\\MakeStory\\button\\animal\\bird\\back\\back.png",

                        "image\\MakeStory\\button\\item\\goods\\goods\\ami.png",	"image\\MakeStory\\button\\item\\goods\\goods\\kago.png",
                        "image\\MakeStory\\button\\item\\goods\\goods\\musi.png",	"image\\MakeStory\\button\\item\\goods\\goods\\usa.png",
                        "image\\MakeStory\\button\\item\\goods\\goods\\rappa.png",	"image\\MakeStory\\button\\item\\goods\\goods\\denwa.png",
                        "image\\MakeStory\\button\\item\\goods\\goods\\ball.png",	"image\\MakeStory\\button\\item\\goods\\goods\\car.png",

                        "image\\MakeStory\\button\\item\\fashion\\fashion\\cap_b.png",	"image\\MakeStory\\button\\item\\fashion\\fashion\\hat_r.png",
                        "image\\MakeStory\\button\\item\\fashion\\fashion\\cap_g.png",	"image\\MakeStory\\button\\item\\fashion\\fashion\\hat_p.png",
                        "image\\MakeStory\\button\\item\\fashion\\fashion\\mugi.png",	"image\\MakeStory\\button\\item\\fashion\\fashion\\ribbon.png",
                        "image\\MakeStory\\button\\item\\fashion\\fashion\\bag_r.png",	"image\\MakeStory\\button\\item\\fashion\\fashion\\bag_p.png",
				
                        "image\\MakeStory\\button\\item\\food\\food\\onigiri.png",	"image\\MakeStory\\button\\item\\food\\food\\humburg.png",
                        "image\\MakeStory\\button\\item\\food\\food\\pudding.png",	"image\\MakeStory\\button\\item\\food\\food\\cocoa.png",
                        "image\\MakeStory\\button\\item\\food\\food\\dogfood.png",	"image\\MakeStory\\button\\item\\food\\food\\fish.png",
                        "image\\MakeStory\\button\\item\\food\\food\\dongri.png",	"image\\MakeStory\\button\\item\\food\\food\\apple.png",

                        "image\\MakeStory\\button\\item\\furniture\\furniture\\chair_l.png",	"image\\MakeStory\\button\\item\\furniture\\furniture\\table.png",
                        "image\\MakeStory\\button\\item\\furniture\\furniture\\chair_r.png",	"image\\MakeStory\\button\\item\\furniture\\furniture\\clock.png",
                        "image\\MakeStory\\button\\item\\furniture\\furniture\\tv.png",			"image\\MakeStory\\button\\item\\furniture\\furniture\\kyoudai.png",
                        "image\\MakeStory\\button\\item\\furniture\\furniture\\hondana.png",	"image\\MakeStory\\button\\item\\furniture\\furniture\\tansu.png",
						
                        "image\\MakeStory\\button\\modosu.png"
                    });
			#endregion

			#region マンダム画像登録
			//紙芝居用画像
			muphic.DrawManager.Regist("BGNone", 0, 0, "image\\MakeStory\\parts\\window.png");
			muphic.DrawManager.Regist("None", 0, 0, "image\\MakeStory\\none.png");

			muphic.DrawManager.Regist("ForestDay1", 0, 0, "image\\MakeStory\\button\\background\\forest\\forest_1\\forest1_sun.png");
			muphic.DrawManager.Regist("ForestNight1", 0, 0, "image\\MakeStory\\button\\background\\forest\\forest_1\\forest1_moon.png");
			muphic.DrawManager.Regist("ForestBad1", 0, 0, "image\\MakeStory\\button\\background\\forest\\forest_1\\forest1_cloudy.png");
			muphic.DrawManager.Regist("ForestDay2", 0, 0, "image\\MakeStory\\button\\background\\forest\\forest_2\\forest2_sun.png");
			muphic.DrawManager.Regist("ForestNight2", 0, 0, "image\\MakeStory\\button\\background\\forest\\forest_2\\forest2_moon.png");
			muphic.DrawManager.Regist("ForestBad2", 0, 0, "image\\MakeStory\\button\\background\\forest\\forest_2\\forest2_cloudy.png");

			muphic.DrawManager.Regist("RiverDay1", 0, 0, "image\\MakeStory\\button\\background\\river\\river_1\\river1_sun.png");
			muphic.DrawManager.Regist("RiverNight1", 0, 0, "image\\MakeStory\\button\\background\\river\\river_1\\river1_moon.png");
			muphic.DrawManager.Regist("RiverBad1", 0, 0, "image\\MakeStory\\button\\background\\river\\river_1\\river1_cloudy.png");
			muphic.DrawManager.Regist("RiverDay2", 0, 0, "image\\MakeStory\\button\\background\\river\\river_2\\river2_sun.png");
			muphic.DrawManager.Regist("RiverNight2", 0, 0, "image\\MakeStory\\button\\background\\river\\river_2\\river2_moon.png");
			muphic.DrawManager.Regist("RiverBad2", 0, 0, "image\\MakeStory\\button\\background\\river\\river_2\\river2_cloudy.png");

			muphic.DrawManager.Regist("TownDay1", 0, 0, "image\\MakeStory\\button\\background\\town\\town_1\\town1_sun.png");
			muphic.DrawManager.Regist("TownNight1", 0, 0, "image\\MakeStory\\button\\background\\town\\town_1\\town1_moon.png");
			muphic.DrawManager.Regist("TownBad1", 0, 0, "image\\makestory\\button\\background\\town\\town_1\\town1_cloudy.png");
			muphic.DrawManager.Regist("TownDay2", 0, 0, "image\\makestory\\button\\background\\town\\town_2\\town2_sun.png");
			muphic.DrawManager.Regist("TownNight2", 0, 0, "image\\makestory\\button\\background\\town\\town_2\\town2_moon.png");
			muphic.DrawManager.Regist("TownBad2", 0, 0, "image\\makestory\\button\\background\\town\\town_2\\town2_cloudy.png");

			muphic.DrawManager.Regist("RoomDay1", 0, 0, "image\\makestory\\button\\background\\room\\room_1\\room1_sun.png");
			muphic.DrawManager.Regist("RoomNight1", 0, 0, "image\\makestory\\button\\background\\room\\room_1\\room1_moon.png");
			muphic.DrawManager.Regist("RoomBad1", 0, 0, "image\\makestory\\button\\background\\room\\room_1\\room1_cloudy.png");
			muphic.DrawManager.Regist("RoomDay2", 0, 0, "image\\makestory\\button\\background\\room\\room_2\\room2_sun.png");
			muphic.DrawManager.Regist("RoomNight2", 0, 0, "image\\makestory\\button\\background\\room\\room_2\\room2_moon.png");
			muphic.DrawManager.Regist("RoomBad2", 0, 0, "image\\makestory\\button\\background\\room\\room_2\\room2_cloudy.png");

			DrawManager.Regist("ManFrontGlad", 0, 0, "image\\MakeStory\\button\\human\\man\\front\\glad.png");
			DrawManager.Regist("ManFrontAngry", 0, 0, "image\\MakeStory\\button\\human\\man\\front\\angry.png");
			DrawManager.Regist("ManFrontSad", 0, 0, "image\\MakeStory\\button\\human\\man\\front\\sad.png");
			DrawManager.Regist("ManFrontEnjoy", 0, 0, "image\\MakeStory\\button\\human\\man\\front\\enjoy.png");
			DrawManager.Regist("ManLeftGlad", 0, 0, "image\\MakeStory\\button\\human\\man\\left\\glad.png");
			DrawManager.Regist("ManLeftAngry", 0, 0, "image\\MakeStory\\button\\human\\man\\left\\angry.png");
			DrawManager.Regist("ManLeftSad", 0, 0, "image\\MakeStory\\button\\human\\man\\left\\sad.png");
			DrawManager.Regist("ManLeftEnjoy", 0, 0, "image\\MakeStory\\button\\human\\man\\left\\enjoy.png");
			DrawManager.Regist("ManRightGlad", 0, 0, "image\\MakeStory\\button\\human\\man\\right\\glad.png");
			DrawManager.Regist("ManRightAngry", 0, 0, "image\\MakeStory\\button\\human\\man\\right\\angry.png");
			DrawManager.Regist("ManRightSad", 0, 0, "image\\MakeStory\\button\\human\\man\\right\\sad.png");
			DrawManager.Regist("ManRightEnjoy", 0, 0, "image\\MakeStory\\button\\human\\man\\right\\enjoy.png");
			DrawManager.Regist("ManBack", 0, 0, "image\\MakeStory\\button\\human\\man\\back\\back.png");

			DrawManager.Regist("LadyFrontGlad", 0, 0, "image\\MakeStory\\button\\human\\lady\\front\\glad.png");
			DrawManager.Regist("LadyFrontAngry", 0, 0, "image\\MakeStory\\button\\human\\lady\\front\\angry.png");
			DrawManager.Regist("LadyFrontSad", 0, 0, "image\\MakeStory\\button\\human\\lady\\front\\sad.png");
			DrawManager.Regist("LadyFrontEnjoy", 0, 0, "image\\MakeStory\\button\\human\\lady\\front\\enjoy.png");
			DrawManager.Regist("LadyLeftGlad", 0, 0, "image\\MakeStory\\button\\human\\lady\\left\\glad.png");
			DrawManager.Regist("LadyLeftAngry", 0, 0, "image\\MakeStory\\button\\human\\lady\\left\\angry.png");
			DrawManager.Regist("LadyLeftSad", 0, 0, "image\\MakeStory\\button\\human\\lady\\left\\sad.png");
			DrawManager.Regist("LadyLeftEnjoy", 0, 0, "image\\MakeStory\\button\\human\\lady\\left\\enjoy.png");
			DrawManager.Regist("LadyRightGlad", 0, 0, "image\\MakeStory\\button\\human\\lady\\right\\glad.png");
			DrawManager.Regist("LadyRightAngry", 0, 0, "image\\MakeStory\\button\\human\\lady\\right\\angry.png");
			DrawManager.Regist("LadyRightSad", 0, 0, "image\\MakeStory\\button\\human\\lady\\right\\sad.png");
			DrawManager.Regist("LadyRightEnjoy", 0, 0, "image\\MakeStory\\button\\human\\lady\\right\\enjoy.png");
			DrawManager.Regist("LadyBack", 0, 0, "image\\MakeStory\\button\\human\\lady\\back\\back.png");

			DrawManager.Regist("GirlFrontGlad", 0, 0, "image\\MakeStory\\button\\human\\girl\\front\\glad.png");
			DrawManager.Regist("GirlFrontAngry", 0, 0, "image\\MakeStory\\button\\human\\girl\\front\\angry.png");
			DrawManager.Regist("GirlFrontSad", 0, 0, "image\\MakeStory\\button\\human\\girl\\front\\sad.png");
			DrawManager.Regist("GirlFrontEnjoy", 0, 0, "image\\MakeStory\\button\\human\\girl\\front\\enjoy.png");
			DrawManager.Regist("GirlLeftGlad", 0, 0, "image\\MakeStory\\button\\human\\girl\\left\\glad.png");
			DrawManager.Regist("GirlLeftAngry", 0, 0, "image\\MakeStory\\button\\human\\girl\\left\\angry.png");
			DrawManager.Regist("GirlLeftSad", 0, 0, "image\\MakeStory\\button\\human\\girl\\left\\sad.png");
			DrawManager.Regist("GirlLeftEnjoy", 0, 0, "image\\MakeStory\\button\\human\\girl\\left\\enjoy.png");
			DrawManager.Regist("GirlRightGlad", 0, 0, "image\\MakeStory\\button\\human\\girl\\right\\glad.png");
			DrawManager.Regist("GirlRightAngry", 0, 0, "image\\MakeStory\\button\\human\\girl\\right\\angry.png");
			DrawManager.Regist("GirlRightSad", 0, 0, "image\\MakeStory\\button\\human\\girl\\right\\sad.png");
			DrawManager.Regist("GirlRightEnjoy", 0, 0, "image\\MakeStory\\button\\human\\girl\\right\\enjoy.png");
			DrawManager.Regist("GirlBack", 0, 0, "image\\MakeStory\\button\\human\\girl\\back\\back.png");

			DrawManager.Regist("BoyFrontGlad", 0, 0, "image\\MakeStory\\button\\human\\boy\\front\\glad.png");
			DrawManager.Regist("BoyFrontAngry", 0, 0, "image\\MakeStory\\button\\human\\boy\\front\\angry.png");
			DrawManager.Regist("BoyFrontSad", 0, 0, "image\\MakeStory\\button\\human\\boy\\front\\sad.png");
			DrawManager.Regist("BoyFrontEnjoy", 0, 0, "image\\MakeStory\\button\\human\\boy\\front\\enjoy.png");
			DrawManager.Regist("BoyLeftGlad", 0, 0, "image\\MakeStory\\button\\human\\boy\\left\\glad.png");
			DrawManager.Regist("BoyLeftAngry", 0, 0, "image\\MakeStory\\button\\human\\boy\\left\\angry.png");
			DrawManager.Regist("BoyLeftSad", 0, 0, "image\\MakeStory\\button\\human\\boy\\left\\sad.png");
			DrawManager.Regist("BoyLeftEnjoy", 0, 0, "image\\MakeStory\\button\\human\\boy\\left\\enjoy.png");
			DrawManager.Regist("BoyRightGlad", 0, 0, "image\\MakeStory\\button\\human\\boy\\right\\glad.png");
			DrawManager.Regist("BoyRightAngry", 0, 0, "image\\MakeStory\\button\\human\\boy\\right\\angry.png");
			DrawManager.Regist("BoyRightSad", 0, 0, "image\\MakeStory\\button\\human\\boy\\right\\sad.png");
			DrawManager.Regist("BoyRightEnjoy", 0, 0, "image\\MakeStory\\button\\human\\boy\\right\\enjoy.png");
			DrawManager.Regist("BoyBack", 0, 0, "image\\MakeStory\\button\\human\\boy\\back\\back.png");

			DrawManager.Regist("WolfFrontGlad", 0, 0, "image\\MakeStory\\button\\animal\\dog\\front\\glad.png");
			DrawManager.Regist("WolfFrontAngry", 0, 0, "image\\MakeStory\\button\\animal\\dog\\front\\angry.png");
			DrawManager.Regist("WolfFrontSad", 0, 0, "image\\MakeStory\\button\\animal\\dog\\front\\sad.png");
			DrawManager.Regist("WolfFrontEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\dog\\front\\enjoy.png");
			DrawManager.Regist("WolfLeftGlad", 0, 0, "image\\MakeStory\\button\\animal\\dog\\left\\glad.png");
			DrawManager.Regist("WolfLeftAngry", 0, 0, "image\\MakeStory\\button\\animal\\dog\\left\\angry.png");
			DrawManager.Regist("WolfLeftSad", 0, 0, "image\\MakeStory\\button\\animal\\dog\\left\\sad.png");
			DrawManager.Regist("WolfLeftEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\dog\\left\\enjoy.png");
			DrawManager.Regist("WolfRightGlad", 0, 0, "image\\MakeStory\\button\\animal\\dog\\right\\glad.png");
			DrawManager.Regist("WolfRightAngry", 0, 0, "image\\MakeStory\\button\\animal\\dog\\right\\angry.png");
			DrawManager.Regist("WolfRightSad", 0, 0, "image\\MakeStory\\button\\animal\\dog\\right\\sad.png");
			DrawManager.Regist("WolfRightEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\dog\\right\\enjoy.png");
			DrawManager.Regist("WolfBack", 0, 0, "image\\MakeStory\\button\\animal\\dog\\back\\back.png");

			DrawManager.Regist("BearFrontGlad", 0, 0, "image\\MakeStory\\button\\animal\\bear\\front\\glad.png");
			DrawManager.Regist("BearFrontAngry", 0, 0, "image\\MakeStory\\button\\animal\\bear\\front\\angry.png");
			DrawManager.Regist("BearFrontSad", 0, 0, "image\\MakeStory\\button\\animal\\bear\\front\\sad.png");
			DrawManager.Regist("BearFrontEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\bear\\front\\enjoy.png");
			DrawManager.Regist("BearLeftGlad", 0, 0, "image\\MakeStory\\button\\animal\\bear\\left\\glad.png");
			DrawManager.Regist("BearLeftAngry", 0, 0, "image\\MakeStory\\button\\animal\\bear\\left\\angry.png");
			DrawManager.Regist("BearLeftSad", 0, 0, "image\\MakeStory\\button\\animal\\bear\\left\\sad.png");
			DrawManager.Regist("BearLeftEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\bear\\left\\enjoy.png");
			DrawManager.Regist("BearRightGlad", 0, 0, "image\\MakeStory\\button\\animal\\bear\\right\\glad.png");
			DrawManager.Regist("BearRightAngry", 0, 0, "image\\MakeStory\\button\\animal\\bear\\right\\angry.png");
			DrawManager.Regist("BearRightSad", 0, 0, "image\\MakeStory\\button\\animal\\bear\\right\\sad.png");
			DrawManager.Regist("BearRightEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\bear\\right\\enjoy.png");
			DrawManager.Regist("BearBack", 0, 0, "image\\MakeStory\\button\\animal\\bear\\back\\back.png");

			DrawManager.Regist("TurtleFrontGlad", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\front\\glad.png");
			DrawManager.Regist("TurtleFrontAngry", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\front\\angry.png");
			DrawManager.Regist("TurtleFrontSad", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\front\\sad.png");
			DrawManager.Regist("TurtleFrontEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\front\\enjoy.png");
			DrawManager.Regist("TurtleLeftGlad", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\left\\glad.png");
			DrawManager.Regist("TurtleLeftAngry", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\left\\angry.png");
			DrawManager.Regist("TurtleLeftSad", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\left\\sad.png");
			DrawManager.Regist("TurtleLeftEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\left\\enjoy.png");
			DrawManager.Regist("TurtleRightGlad", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\right\\glad.png");
			DrawManager.Regist("TurtleRightAngry", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\right\\angry.png");
			DrawManager.Regist("TurtleRightSad", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\right\\sad.png");
			DrawManager.Regist("TurtleRightEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\right\\enjoy.png");
			DrawManager.Regist("TurtleBack", 0, 0, "image\\MakeStory\\button\\animal\\turtle\\back\\back.png");

			DrawManager.Regist("SparrowFrontGlad", 0, 0, "image\\MakeStory\\button\\animal\\bird\\front\\glad.png");
			DrawManager.Regist("SparrowFrontAngry", 0, 0, "image\\MakeStory\\button\\animal\\bird\\front\\angry.png");
			DrawManager.Regist("SparrowFrontSad", 0, 0, "image\\MakeStory\\button\\animal\\bird\\front\\sad.png");
			DrawManager.Regist("SparrowFrontEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\bird\\front\\enjoy.png");
			DrawManager.Regist("SparrowLeftGlad", 0, 0, "image\\MakeStory\\button\\animal\\bird\\left\\glad.png");
			DrawManager.Regist("SparrowLeftAngry", 0, 0, "image\\MakeStory\\button\\animal\\bird\\left\\angry.png");
			DrawManager.Regist("SparrowLeftSad", 0, 0, "image\\MakeStory\\button\\animal\\bird\\left\\sad.png");
			DrawManager.Regist("SparrowLeftEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\bird\\left\\enjoy.png");
			DrawManager.Regist("SparrowRightGlad", 0, 0, "image\\MakeStory\\button\\animal\\bird\\right\\glad.png");
			DrawManager.Regist("SparrowRightAngry", 0, 0, "image\\MakeStory\\button\\animal\\bird\\right\\angry.png");
			DrawManager.Regist("SparrowRightSad", 0, 0, "image\\MakeStory\\button\\animal\\bird\\right\\sad.png");
			DrawManager.Regist("SparrowRightEnjoy", 0, 0, "image\\MakeStory\\button\\animal\\bird\\right\\enjoy.png");
			DrawManager.Regist("SparrowBack", 0, 0, "image\\MakeStory\\button\\animal\\bird\\back\\back.png");

			DrawManager.Regist("GoodsAmi", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\ami.png");
			DrawManager.Regist("GoodsKago", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\kago.png");
			DrawManager.Regist("GoodsMusi", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\musi.png");
			DrawManager.Regist("GoodsUsa", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\usa.png");
			DrawManager.Regist("GoodsRappa", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\rappa.png");
			DrawManager.Regist("GoodsDenwa", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\denwa.png");
			DrawManager.Regist("GoodsBall", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\ball.png");
			DrawManager.Regist("GoodsCar", 0, 0, "image\\MakeStory\\button\\item\\goods\\goods\\car.png");

			DrawManager.Regist("FashionCapB", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\cap_b.png");
			DrawManager.Regist("FashionHatR", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\hat_r.png");
			DrawManager.Regist("FashionCapG", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\cap_g.png");
			DrawManager.Regist("FashionHatP", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\hat_p.png");
			DrawManager.Regist("FashionMugi", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\mugi.png");
			DrawManager.Regist("FashionRibbon", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\ribbon.png");
			DrawManager.Regist("FashionBagR", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\bag_r.png");
			DrawManager.Regist("FashionBagP", 0, 0, "image\\MakeStory\\button\\item\\fashion\\fashion\\bag_p.png");

			DrawManager.Regist("FoodOnigiri", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\onigiri.png");
			DrawManager.Regist("FoodHumburg", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\humburg.png");
			DrawManager.Regist("FoodPudding", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\pudding.png");
			DrawManager.Regist("FoodCocoa", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\cocoa.png");
			DrawManager.Regist("FoodDogfood", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\dogfood.png");
			DrawManager.Regist("FoodFish", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\fish.png");
			DrawManager.Regist("FoodDongri", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\dongri.png");
			DrawManager.Regist("FoodApple", 0, 0, "image\\MakeStory\\button\\item\\food\\food\\apple.png");

			DrawManager.Regist("FurnitureChairL", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\chair_l.png");
			DrawManager.Regist("FurnitureTable", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\table.png");
			DrawManager.Regist("FurnitureChairR", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\chair_r.png");
			DrawManager.Regist("FurnitureClock", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\clock.png");
			DrawManager.Regist("FurnitureTV", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\tv.png");
			DrawManager.Regist("FurnitureKyoudai", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\kyoudai.png");
			DrawManager.Regist("FurnitureHondana", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\hondana.png");
			DrawManager.Regist("FurnitureTansu", 0, 0, "image\\MakeStory\\button\\item\\furniture\\furniture\\tansu.png");
			#endregion

			DrawManager.EndRegist();
            /////////////////////////////////////////////////////////////////////
            ////部品の画面への登録
            /////////////////////////////////////////////////////////////////////
            BaseList.Add(bb);
            BaseList.Add(thumb);
            BaseList.Add(wind);

            BaseList.Add(acb);
            BaseList.Add(cb);

            BaseList.Add(tb);
			BaseList.Add(sentB);

			BaseList.Add(storyB);

			BaseList.Add(storymakeB);
			BaseList.Add(storyselectB);
			BaseList.Add(storysaveB);
			BaseList.Add(storyprintB);

			BaseList.Add(storyplayB);

			BaseList.Add(boy);
			BaseList.Add(girl);
			BaseList.Add(lady);
			BaseList.Add(man);

			BaseList.Add(dog);
			BaseList.Add(bear);
			BaseList.Add(turtle);
			BaseList.Add(bird);

			BaseList.Add(forest);
			BaseList.Add(river);
			BaseList.Add(town);
			BaseList.Add(room);

			BaseList.Add(goods);
			BaseList.Add(fashion);
			BaseList.Add(food);
			BaseList.Add(furniture);
			
			// 自動セーブファイルが残っていれば読み込む
			if(System.IO.File.Exists("AutoSaveData\\STORY_AUTOSAVE.txt"))
			{
				StoryFileReader sfr = new StoryFileReader(this.PictureStory);
				sfr.Read("..\\AutoSaveData\\STORY_AUTOSAVE");
			}
			
		}

		public override void Click(System.Drawing.Point p)
		{
            this.State = this.State == 0 ? 1 : 0;
			switch (this.MakeStoryScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:
					base.Click(p);							//普段のデフォルト処理
					design.Click(p);
					break;
				case MakeStoryScreenMode.StoryScreen:
					Screen.Click(p);						//Storyであれば、あちら側での処理しかしない
					break;
				case MakeStoryScreenMode.SelectDialog:
					Screen.Click(p);						//ダイアログが開かれていると
					break;									//ダイアログ側にしか
				case MakeStoryScreenMode.SaveDialog:			//クリック処理が
					Screen.Click(p);						//いかない
					break;
				case MakeStoryScreenMode.MakeDialog:			//クリック処理が
					Screen.Click(p);						//いかない
					break;
				case MakeStoryScreenMode.PrintDialog:
					Screen.Click(p);
					break;
				case MakeStoryScreenMode.PlayStoryScreen:
					Screen.Click(p);
					break;
				case MakeStoryScreenMode.TitleScreen:
					Screen.Click(p);
					break;
				case MakeStoryScreenMode.AllClearDialog:
					Screen.Click(p);
					break;
				default:
					break;

			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			switch (this.MakeStoryScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:
					base.MouseMove(p);
					design.MouseMove(p);
					tsuibi.MouseMove(p);					//tsuibiはBase型だが、MouseMoveが必要なので、呼ぶ
					break;
				case MakeStoryScreenMode.StoryScreen:
					Screen.MouseMove(p);						//Storyであれば、あちら側での処理しかしない
					break;
				case MakeStoryScreenMode.SelectDialog:
					base.MouseMove(p);					//ダイアログが開かれていても
					tsuibi.MouseMove(p);				//もとの処理は行う
					Screen.MouseMove(p);
					break;
				case MakeStoryScreenMode.SaveDialog:
					base.MouseMove(p);
					tsuibi.MouseMove(p);
					Screen.MouseMove(p);
					break;
				case MakeStoryScreenMode.MakeDialog:
					base.MouseMove(p);
					tsuibi.MouseMove(p);
					Screen.MouseMove(p);
					break;
				case MakeStoryScreenMode.PrintDialog:
					base.MouseMove(p);
					tsuibi.MouseMove(p);
					Screen.MouseMove(p);
					break;
				case MakeStoryScreenMode.PlayStoryScreen:
					Screen.MouseMove(p);
					break;
				case MakeStoryScreenMode.TitleScreen:
					Screen.MouseMove(p);
					break;
				case MakeStoryScreenMode.AllClearDialog:
					Screen.MouseMove(p);
					break;
				default:
					break;

			}
		}

		public override void DragBegin(System.Drawing.Point begin)
		{
			//base.DragBegin (begin);
			switch(this.MakeStoryScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:
					base.DragBegin(begin);
					design.DragBegin(begin);
					break;
				case MakeStoryScreenMode.StoryScreen:
				case MakeStoryScreenMode.SelectDialog:
				case MakeStoryScreenMode.SaveDialog:
				case MakeStoryScreenMode.MakeDialog:
				case MakeStoryScreenMode.PrintDialog:
				case MakeStoryScreenMode.PlayStoryScreen:
				case MakeStoryScreenMode.TitleScreen:
				case MakeStoryScreenMode.AllClearDialog:
					Screen.DragBegin(begin);
					break;
				default:
					break;
			}
		}

		public override void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
			//base.DragEnd (begin, p);
			switch(this.MakeStoryScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:
					base.DragEnd(begin, p);
					design.DragEnd(begin, p);
					break;
				case MakeStoryScreenMode.StoryScreen:
				case MakeStoryScreenMode.SelectDialog:
				case MakeStoryScreenMode.SaveDialog:
				case MakeStoryScreenMode.MakeDialog:
				case MakeStoryScreenMode.PrintDialog:
				case MakeStoryScreenMode.PlayStoryScreen:
				case MakeStoryScreenMode.TitleScreen:
				case MakeStoryScreenMode.AllClearDialog:
					Screen.DragEnd(begin, p);
					break;
				default:
					break;
			}
		}

		public override void Drag(System.Drawing.Point begin, System.Drawing.Point p)
		{
			base.Drag (begin, p);
			switch(this.MakeStoryScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:
								base.Drag(begin, p);
								design.Drag(begin, p);
								break;
				case MakeStoryScreenMode.StoryScreen:
				case MakeStoryScreenMode.SelectDialog:
				case MakeStoryScreenMode.SaveDialog:
				case MakeStoryScreenMode.MakeDialog:
				case MakeStoryScreenMode.PrintDialog:
				case MakeStoryScreenMode.PlayStoryScreen:
				case MakeStoryScreenMode.TitleScreen:
				case MakeStoryScreenMode.AllClearDialog:
					Screen.Drag(begin, p);
					break;
				default:
					break;
			}
		}

		public override void Draw()
		{
			switch (this.MakeStoryScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:
					base.Draw();
					design.Draw();
					muphic.DrawManager.Draw("SignHuman");
					muphic.DrawManager.Draw("SignAnimal");
					muphic.DrawManager.Draw("SignBG");
					muphic.DrawManager.Draw("SignItem");
					muphic.DrawManager.Draw("TitleNameWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Title, 308, 115);
					muphic.DrawManager.Draw("SentenceWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Slide[NowPage].Sentence, 156, 722);
					
					if (tsuibi.Visible)
					{
						muphic.DrawManager.DrawCenter(tsuibi.ToString(), tsuibi.point.X, tsuibi.point.Y, tsuibi.State);
						//tsuibiはBaseListに登録してないので、別に描画する必要がある
					}
					break;
				case MakeStoryScreenMode.StoryScreen:
					Screen.Draw();							//Storyであれば、あちら側での処理しかしない
					break;
				case MakeStoryScreenMode.SelectDialog:
					base.Draw();					//ダイアログが開かれていてももとの処理は行う
					design.Draw();
					muphic.DrawManager.Draw("SignHuman");
					muphic.DrawManager.Draw("SignAnimal");
					muphic.DrawManager.Draw("SignBG");
					muphic.DrawManager.Draw("SignItem");
					muphic.DrawManager.Draw("TitleNameWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Title, 308, 115);
					muphic.DrawManager.Draw("SentenceWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Slide[NowPage].Sentence, 156, 722);
					Screen.Draw();
					break;
				case MakeStoryScreenMode.SaveDialog:
					base.Draw();
					design.Draw();
					muphic.DrawManager.Draw("SignHuman");
					muphic.DrawManager.Draw("SignAnimal");
					muphic.DrawManager.Draw("SignBG");
					muphic.DrawManager.Draw("SignItem");
					muphic.DrawManager.Draw("TitleNameWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Title, 308, 115);
					muphic.DrawManager.Draw("SentenceWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Slide[NowPage].Sentence, 156, 722);
					Screen.Draw();
					break;
				case MakeStoryScreenMode.MakeDialog:
					base.Draw();
					design.Draw();
					muphic.DrawManager.Draw("SignHuman");
					muphic.DrawManager.Draw("SignAnimal");
					muphic.DrawManager.Draw("SignBG");
					muphic.DrawManager.Draw("SignItem");
					muphic.DrawManager.Draw("TitleNameWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Title, 308, 115);
					muphic.DrawManager.Draw("SentenceWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Slide[NowPage].Sentence, 156, 722);
					Screen.Draw();
					break;
				case MakeStoryScreenMode.PrintDialog:
					base.Draw();
					design.Draw();
					muphic.DrawManager.Draw("SignHuman");
					muphic.DrawManager.Draw("SignAnimal");
					muphic.DrawManager.Draw("SignBG");
					muphic.DrawManager.Draw("SignItem");muphic.DrawManager.Draw("TitleNameWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Title, 308, 115);
					muphic.DrawManager.Draw("SentenceWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Slide[NowPage].Sentence, 156, 722);
					Screen.Draw();
					break;
				case MakeStoryScreenMode.PlayStoryScreen:
					Screen.Draw();
					break;
				case MakeStoryScreenMode.TitleScreen:
					Screen.Draw();
					break;
				case MakeStoryScreenMode.AllClearDialog:
					base.Draw();
					design.Draw();
					muphic.DrawManager.Draw("SignHuman");
					muphic.DrawManager.Draw("SignAnimal");
					muphic.DrawManager.Draw("SignBG");
					muphic.DrawManager.Draw("SignItem");
					muphic.DrawManager.Draw("TitleNameWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Title, 308, 115);
					muphic.DrawManager.Draw("SentenceWindow");
					muphic.DrawManager.DrawString(this.PictureStory.Slide[NowPage].Sentence, 156, 722);
					Screen.Draw();
					break;
				default:
					break;

			}
			
			if(muphic.Common.AutoSave.getSaveFlag()) this.AutoSave();
		}
		
		public void AutoSave()
		{
			StoryFileWriter sfw = new StoryFileWriter(this.PictureStory);
			sfw.Write("..\\AutoSaveData\\STORY_AUTOSAVE");
		}
		

		/// <summary>
		/// スライドを初期化するときに使用するメソッド。必ず0枚目から始まる。
		/// </summary>
		public void ChangeSlide0()
		{
			this.NowPage = 0;
			Slide s = PictureStory.Slide[this.NowPage];
		}
	}
}
