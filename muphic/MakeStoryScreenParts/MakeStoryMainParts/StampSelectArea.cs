using Muphic.MakeStoryScreenParts.Buttons;

namespace Muphic.MakeStoryScreenParts.MakeStoryMainParts
{
	/// <summary>
	/// 物語作成画面
	/// <para>スタンプ選択ボタン群管理クラス。</para>
	/// <para>物語作成画面下部のスタンプ選択ボタン群の管理を行う。</para>
	/// </summary>
	public class StampSelectArea : Common.Screen
	{
		/// <summary>
		/// 親にあたる物語作成画面
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// スタンプ選択ボタン群
		/// </summary>
		public StampSelectButton[] StampSelectButtons { get; private set; }

		/// <summary>
		/// カテゴリの種類を識別するための数値。
		/// <para>0:無選択　1:背景　2:登場人物　3:アイテム</para>
		/// </summary>
		public int CategoryType { get; private set; }


		/// <summary>
		/// スタンプ選択領域内での追尾の有効性を表す bool 値を取得または設定する。
		/// <para>このプロパティ値が true の間のみ追尾を行う。</para>
		/// </summary>
		public bool EnabledHoming { get; set; }


		/// <summary>
		/// 現在選択されているカテゴリを表わす。
		/// <para>CategoryMode プロパティを使用すること。</para>
		/// </summary>
		private CategoryMode __categoryMode;
		/// <summary>
		/// 現在のカテゴリを取得または設定する。
		/// <para>カテゴリを設定すると、画面下部のスタンプ選択ボタンが自動的に切り替わる。</para>
		/// </summary>
		public CategoryMode CategoryMode
		{
			get
			{
				return this.__categoryMode;
			}
			set
			{
				this.__categoryMode = value;

				// 設定されたカテゴリ別に、画面下部のスタンプ選択ボタンを切り替える。
				switch (value)
				{
					#region 背景

					// 背景のカテゴリが選択された場合、現在編集中のスライドの背景に合わせ、ボタンの押下状態も設定する
					// （スライドの背景を同じスタンプ選択ボタンは、切り替え時に既に押下状態にするため）

					case CategoryMode.BgForest:
						this.StampSelectButtons[0].Visible = this.StampSelectButtons[7].Visible = false;
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_F1S", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_F1C", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_F1N", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_F2S");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_F2C");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_F2N");

						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.Forest1)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[1].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[2].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[3].Pressed = true;
									break;
							}
						}
						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.Forest2)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[4].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[5].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[6].Pressed = true;
									break;
							}
						}
						break;

					case CategoryMode.BgRiver:
						this.StampSelectButtons[0].Visible = this.StampSelectButtons[7].Visible = false;
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_R1S", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_R1C", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_R1N", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_R2S");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_R2C");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_R2N");

						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.River1)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[1].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[2].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[3].Pressed = true;
									break;
							}
						}
						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.River2)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[4].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[5].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[6].Pressed = true;
									break;
							}
						}
						break;

					case CategoryMode.BgTown:
						this.StampSelectButtons[0].Visible = this.StampSelectButtons[7].Visible = false;
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_T1S", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_T1C", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_T1N", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_T2S");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_T2C");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_T2N");

						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.Town1)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[1].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[2].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[3].Pressed = true;
									break;
							}
						}
						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.Town2)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[4].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[5].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[6].Pressed = true;
									break;
							}
						}
						break;

					case CategoryMode.BgHouse:
						this.StampSelectButtons[0].Visible = this.StampSelectButtons[7].Visible = false;
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_H1S", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_H1C", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_H1N", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_H2S");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_H2C");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BG_H2N");

						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.House1)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[1].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[2].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[3].Pressed = true;
									break;
							}
						}
						if (this.Parent.CurrentSlide.BackgroundPlace == BackgroundPlace.House2)
						{
							switch (this.Parent.CurrentSlide.BackgroundSky)
							{
								case BackgroundSky.Sunny:
									this.StampSelectButtons[4].Pressed = true;
									break;
								case BackgroundSky.Cloudy:
									this.StampSelectButtons[5].Pressed = true;
									break;
								case BackgroundSky.Night:
									this.StampSelectButtons[6].Pressed = true;
									break;
							}
						}
						break;

					#endregion

					#region 人物

					case CategoryMode.HumanMan:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_MAN_DR");
						break;

					case CategoryMode.HumanBoy:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BOY_DR");
						break;

					case CategoryMode.HumanWoman:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_WOMAN_DR");
						break;

					case CategoryMode.HumanGirl:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_GIRL_DR");
						break;

					#endregion

					#region 動物

					case CategoryMode.AnimalTurtle:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_TURTLE_DR");
						break;

					case CategoryMode.AnimalBear:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BEAR_DR");
						break;

					case CategoryMode.AnimalBird:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_BIRD_DR");
						break;

					case CategoryMode.AnimalDog:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_FJ", true);
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_FA", true);
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_FS", true);
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_FE", true);
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_DF");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_DB");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_DL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_DOG_DR");
						break;

					#endregion

					#region アイテム

					case CategoryMode.ItemFashion:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CAPB");
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CAPG");
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_HATY");
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_HATP");
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_STRAW");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_RIBBN");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_BAGR");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_BAGB");
						break;

					case CategoryMode.ItemOutdoor:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CNET");
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_ICASE");
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_BEETLE");
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_RABBIT");
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_BUGLE");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CPHONE");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_BALL");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CAR");
						break;

					case CategoryMode.ItemFood:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_RBALL");
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_HUMB");
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_PUD");
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CAFFE");
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_DFOOD");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_FISH");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_ACORN");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_APLE");
						break;

					case CategoryMode.ItemFurniture:
						this.StampSelectButtons[0].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CHAIL");
						this.StampSelectButtons[1].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CHAIR");
						this.StampSelectButtons[2].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_TABLE");
						this.StampSelectButtons[3].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_CLOCK");
						this.StampSelectButtons[4].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_TELEV");
						this.StampSelectButtons[5].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_DESBO");
						this.StampSelectButtons[6].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_BRACK");
						this.StampSelectButtons[7].SetLabel("IMAGE_MAKESTORYSCR_STMPBTN_ITEM_DRAWE");
						break;

					#endregion

					case CategoryMode.None:
						foreach (StampSelectButton ssb in this.StampSelectButtons) ssb.Visible = false;
						foreach (CategoryButton cb in this.Parent.CategoryButtons) cb.Pressed = false;
						break;

					default:
						goto case CategoryMode.None;
				}

				// 選択されているカテゴリが切り替わったら、追尾状態をクリアする
				this.Parent.StampHoming.ClearHoming();

				// メッセージ出力
				Tools.DebugTools.ConsolOutputMessage(
					"StampSelectArea -CategoryMode",
					"カテゴリ選択 -- " + System.Enum.GetName(typeof(CategoryMode), value),
					true
				);
			}
		}



		/// <summary>
		/// スタンプ選択ボタン群管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public StampSelectArea(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			// スタンプ選択ボタン群を生成 個数は８
			this.StampSelectButtons = new StampSelectButton[8];

			// スタンプ選択ボタンのインスタンス化とリスト登録
			for (int i = 0; i < this.StampSelectButtons.Length; i++)
			{
				this.StampSelectButtons[i] = new StampSelectButton(this, i);
				this.PartsList.Add(this.StampSelectButtons[i]);
				this.StampSelectButtons[i].Visible = false;			// 画面遷移時は不可視にする
			}

			this.EnabledHoming = false;

			Manager.DrawManager.Regist(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_StampSelectArea.Location, "IMAGE_MAKESTORYSCR_STAMPSELECTAREA");
		}



		/// <summary>
		/// 何らかのスタンプ選択ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="number">クリックされたスタンプ選択ボタンの番号。</param>
		public void Selected(int number)
		{
			// 列挙体の定数名を取得
			string mode = System.Enum.GetName(typeof(CategoryMode), this.CategoryMode);

			// 定数名の先頭部分から、現在どの種類のカテゴリが選択されているかを判別
			// 種類別のクリック処理を行う

			if (mode.StartsWith("Bg"))
			{
				#region 背景だった場合

				if (this.StampSelectButtons[number].Pressed)	// 編集中のスライドの背景と同じボタンが押されたら、背景を白紙に戻す
				{
					this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.None;
					this.Parent.CurrentSlide.BackgroundSky = BackgroundSky.None;
					this.AllUnPress();
				}
				else											// 上記でなければ、押されたボタンに合わせ編集中のスライドの背景を設定する
				{
					if (number < 4)						// クリックされたボタンが１～３番（選択ボタン左３つ）だった場合
					{									// 各場所の種類１を設定
						switch (this.CategoryMode)
						{
							case CategoryMode.BgForest:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.Forest1;
								break;

							case CategoryMode.BgRiver:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.River1;
								break;

							case CategoryMode.BgTown:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.Town1;
								break;

							case CategoryMode.BgHouse:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.House1;
								break;
						}
					}
					else								// クリックされたボタンが４～６番（選択ボタン右３つ）だった場合
					{									// 各場所の種類２を設定
						switch (this.CategoryMode)
						{
							case CategoryMode.BgForest:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.Forest2;
								break;

							case CategoryMode.BgRiver:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.River2;
								break;

							case CategoryMode.BgTown:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.Town2;
								break;

							case CategoryMode.BgHouse:
								this.Parent.CurrentSlide.BackgroundPlace = BackgroundPlace.House2;
								break;
						}
					}


					if (number == 1 || number == 4)										// クリックされたボタンが１番か４番だった場合
						this.Parent.CurrentSlide.BackgroundSky = BackgroundSky.Sunny;	// 空の状態は昼（晴れ）

					else if (number == 2 || number == 5)								// クリックされたボタンが２番か５番だった場合
						this.Parent.CurrentSlide.BackgroundSky = BackgroundSky.Cloudy;	// 空の状態は昼（曇）

					else if (number == 3 || number == 6)								// クリックされたボタンが３番か６番だった場合
						this.Parent.CurrentSlide.BackgroundSky = BackgroundSky.Night;	// 空の状態は夜（晴れ）

					this.AllUnPress();										// スタンプ選択ボタンを全て無選択状態にする
					this.StampSelectButtons[number].Pressed = true;			// クリックされたスタンプ選択ボタンを選択状態にする
				}

				Tools.DebugTools.ConsolOutputMessage(					// メッセージ
					"StampSelectArea -Selected",
					string.Format("背景設定 -- {0}({1})",
						System.Enum.GetName(typeof(BackgroundPlace), this.Parent.CurrentSlide.BackgroundPlace),
						System.Enum.GetName(typeof(BackgroundSky), this.Parent.CurrentSlide.BackgroundSky)),
					true
				);

				this.Parent.CurrentSlideChanged();						// 

				#endregion
			}

			else if (mode.StartsWith("Human") || mode.StartsWith("Animal"))
			{
				#region 登場人物だった場合

				Character newCharacter = new Character();				// 追尾用の登場人物クラスを生成

				if (number < 4)
				{														// クリックされたボタンが０～３番（表情）だった場合
					for (int i = 0; i < 4; i++)
					{
						this.StampSelectButtons[i].Pressed = false;
					}														// 一度全ての表情ボタンを無選択状態にし、
					this.StampSelectButtons[number].Pressed = true;			// クリックされた表情ボタンを選択状態にする。
					newCharacter.Aspect = (CharacterAspect)number;			// 登場人物の表情を設定

					for (int i = 4; i < 8; i++)
					{
						if (this.StampSelectButtons[i].Pressed)				// 向きボタンの中で、選択されているものがあるかを探す
						{													// 選択済みの向きボタンがあれば、そのボタンの番号で
							newCharacter.Direction = (CharacterDirection)i;	// 登場人物の向きを設定
							break;
						}
					}

					if (newCharacter.Direction == CharacterDirection.None)	// 登場人物の向きが未指定だった場合
					{														// 登場人物は生成できないため、追尾クラスに渡せない
						return;												// ここで終了
					}
				}
				else
				{														// クリックされたボタンが４～７番（向き）だった場合
					for (int i = 4; i < 8; i++)
					{
						this.StampSelectButtons[i].Pressed = false;
					}														// 一度全ての向きボタンを無選択状態にし、
					this.StampSelectButtons[number].Pressed = true;			// クリックされた向きボタンを選択状態にする。
					newCharacter.Direction = (CharacterDirection)number;	// 登場人物の表情を設定

					for (int i = 0; i < 4; i++)
					{
						if (this.StampSelectButtons[i].Pressed)				// 表情ボタンの中で、選択されているものがあるかを探す
						{													// 選択済みの表情ボタンがあれば、そのボタンの番号で
							newCharacter.Aspect = (CharacterAspect)i;		// 登場人物の表情を設定
							break;
						}
					}

					if (newCharacter.Aspect == CharacterAspect.None)		// 登場人物の表情が未指定だった場合
					{														// 登場人物は生成できないため、追尾クラスに渡せない
						return;												// ここで終了
					}
				}

				switch (this.CategoryMode)								// 現在選択されているカテゴリに応じ、登場人物の設定
				{
					case CategoryMode.HumanMan:
						newCharacter.Type = CharacterType.Man;
						break;

					case CategoryMode.HumanBoy:
						newCharacter.Type = CharacterType.Boy;
						break;

					case CategoryMode.HumanWoman:
						newCharacter.Type = CharacterType.Woman;
						break;

					case CategoryMode.HumanGirl:
						newCharacter.Type = CharacterType.Girl;
						break;

					case CategoryMode.AnimalTurtle:
						newCharacter.Type = CharacterType.Turtle;
						break;

					case CategoryMode.AnimalBear:
						newCharacter.Type = CharacterType.Bear;
						break;

					case CategoryMode.AnimalBird:
						newCharacter.Type = CharacterType.Bird;
						break;

					case CategoryMode.AnimalDog:
						newCharacter.Type = CharacterType.Dog;
						break;

					default:
						// 上記以外の場合はエラー扱い 例外投げる
						string emsg = "StampSelectArea.Selected（スタンプ選択ボタン管理） - 未知の登場人物が指定されている謎" + System.Enum.GetName(typeof(CategoryMode), this.CategoryMode);
						throw new System.Exception(emsg);
				}

				// この時点で、登場人物の種類、表情、体の向きが決定

				// スタンプ画像のテクスチャ名を設定
				newCharacter.SetStampImageName();

				// 追尾クラスに渡す
				this.Parent.StampHoming.HomingTarget = (Stamp)newCharacter;

				// 追尾を有効にする
				this.EnabledHoming = true;

				// メッセージ
				Tools.DebugTools.ConsolOutputMessage("StampSelectArea -Selected", "追尾対象設定 -- Stamp:" + newCharacter.ToString(), true);

				#endregion
			}

			else if (mode.StartsWith("Item"))
			{
				#region アイテムだった場合

				// 追尾用のアイテムクラスを生成
				Item item = new Item();

				// 現在選択されているカテゴリに応じ、アイテムの設定
				// ItemKind列挙型は、それぞれ0から整数値が割り当てられており、それを利用
				switch (this.CategoryMode)
				{
					case CategoryMode.ItemFashion:
						item.ItemKind = (ItemKind)(number % 8);
						break;

					case CategoryMode.ItemOutdoor:
						item.ItemKind = (ItemKind)(number % 8 + 8);
						break;

					case CategoryMode.ItemFood:
						item.ItemKind = (ItemKind)(number % 8 + 16);
						break;

					case CategoryMode.ItemFurniture:
						item.ItemKind = (ItemKind)(number % 8 + 24);
						break;
				}

				// スタンプ選択ボタンを全て無選択状態にする
				this.AllUnPress();

				// クリックされたスタンプ選択ボタンを選択状態にする
				this.StampSelectButtons[number].Pressed = true;

				// スタンプ画像名を設定
				item.SetStampImageName();

				// 追尾クラスに渡す
				this.Parent.StampHoming.HomingTarget = (Stamp)item;

				// 追尾を有効にする
				this.EnabledHoming = true;

				// メッセージ
				Tools.DebugTools.ConsolOutputMessage("StampSelectArea -Selected", "追尾対象設定 -- Stamp:" + item.ToString(), true);

				#endregion
			}
		}


		/// <summary>
		/// マウスがスタンプ選択ボタン領域外へ出た際の処理。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();

			// ボタンから直接領域外へ出ると そのボタンの MouseLeave メソッドが呼ばれないため、
			// ボタン領域から出たら全てのボタンの MouseLeave メソッドを呼ぶ
			foreach (Common.Button button in StampSelectButtons)
			{
				button.MouseLeave();
			}
		}


		/// <summary>
		/// スタンプ選択ボタン領域を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			// 基底クラスのパーツ描画メソッドを呼ぶ
			//（無駄にボタン領域のテクスチャを描画するのを防ぐため）
			base.DrawParts(drawStatus);
		}


		/// <summary>
		/// 全てのスタンプ選択ボタンの押下状態を解除する。
		/// </summary>
		private void AllUnPress()
		{
			foreach (StampSelectButton button in this.StampSelectButtons)
			{
				button.Pressed = false;
			}
		}

		/// <summary>
		/// カテゴリが背景以外の場合のみ、全てのスタンプ選択ボタンの押下状態を解除する。
		/// </summary>
		/// <returns>全てのスタンプ選択ボタンの押下状態が解除された場合は true、それ以外は false。</returns>
		public bool AllUnPressWithoutBackground()
		{
			// 列挙体の定数名を取得
			string mode = System.Enum.GetName(typeof(CategoryMode), this.CategoryMode);

			if (!mode.StartsWith("Bg"))
			{								// 現在のカテゴリが背景でなければ
				this.AllUnPress();			// 全てのスタンプ選択ボタンの押下状態を解除する
				return true;
			}
			else
			{								// 現在のカテゴリが背景であれば
				return false;				// しっぱいにおわった！
			}
		}

	}
}
