using Muphic.CompositionScreenParts.Buttons.Animals;

namespace Muphic.CompositionScreenParts
{
	/// <summary>
	/// 作曲画面で、現在選択されている動物を示す識別子を指定する。
	/// </summary>
	public enum AnimalButtonMode
	{
		/// <summary>
		/// 何も選択されていないことを示す。
		/// </summary>
		None,

		/// <summary>
		/// ヒツジボタンが選択されていることを示す。
		/// </summary>
		Sheep,

		/// <summary>
		/// ウサギボタンが選択されていることを示す。
		/// </summary>
		Rabbit,

		/// <summary>
		/// 鳥バードボタンが選択されていることを示す。
		/// </summary>
		Bird,

		/// <summary>
		/// 犬ドッグボタンが選択されていることを示す。
		/// </summary>
		Dog,

		/// <summary>
		/// ブタボタンが選択されていることを示す。
		/// </summary>
		Pig,

		/// <summary>
		/// ぬこボタンが選択されていることを示す。
		/// </summary>
		Cat,

		/// <summary>
		/// 声ボタンが選択されていることを示す。
		/// </summary>
		Voice,

		/// <summary>
		/// 削除ボタンが選択されていることを示す。
		/// </summary>
		Delete
	}


	/// <summary>
	/// 作曲画面で、動物を選択するボタンを管理するクラス。
	/// </summary>
	public class AnimalButtons : Common.Screen
	{
		/// <summary>
		/// 親にあたる作曲画面クラス
		/// </summary>
		public CompositionScreen Parent { get; private set; }


		#region 各動物ボタン

		/// <summary>
		/// ヒツジ選択ボタン
		/// </summary>
		private SheepButton SheepButton { get; set; }

		/// <summary>
		/// ウサギ選択ボタン
		/// </summary>
		private RabbitButton RabbitButton { get; set; }

		/// <summary>
		/// 鳥選択ボタン
		/// </summary>
		private BirdButton BirdButton { get; set; }

		/// <summary>
		/// 犬選択ボタン
		/// </summary>
		private DogButton DogButton { get; set; }

		/// <summary>
		/// ブタ選択ボタン
		/// </summary>
		private PigButton PigButton { get; set; }

		/// <summary>
		/// ぬこ選択ボタン
		/// </summary>
		private CatButton CatButton { get; set; }

		/// <summary>
		/// 声選択ボタン
		/// </summary>
		private VoiceButton VoiceButton { get; set; }

		/// <summary>
		/// 削除選択ボタン
		/// </summary>
		private DeleteButton DeleteButton { get; set; }

		#endregion


		/// <summary>
		/// 動物ボタンの有効性を表わす bool 値。
		/// プロパティ "Enabled" を使用すること。
		/// </summary>
		private bool __enabled;

		/// <summary>
		/// 動物ボタンの有効性を表わす bool 値。
		/// 全ての動物ボタンが連動するので注意。
		/// </summary>
		public bool Enabled
		{
			get
			{
				return this.__enabled;
			}
			set
			{
				this.__enabled = value;
				this.SheepButton.Enabled = value;
				this.BirdButton.Enabled = value;
				this.RabbitButton.Enabled = value;
				this.DogButton.Enabled = value;
				this.PigButton.Enabled = value;
				this.CatButton.Enabled = value;
				this.VoiceButton.Enabled = value;
				this.DeleteButton.Enabled = value;
			}
		}


		/// <summary>
		/// 動物ボタン領域内での動物の追尾の有効性を示す bool 値を取得または設定する。
		/// <para>このプロパティ値が true の間のみ追尾を行う。</para>
		/// </summary>
		public bool EnabledHoming { get; set; }


		/// <summary>
		/// 現在選択されている動物ボタンを表わす。
		/// </summary>
		private AnimalButtonMode __nowAnimalButtonMode;

		/// <summary>
		/// 現在選択されている動物ボタンを表わす。
		/// </summary>
		public AnimalButtonMode NowAnimalButtonMode
		{
			get
			{
				return this.__nowAnimalButtonMode;
			}
			set
			{
				this.AllClear();
				this.__nowAnimalButtonMode = value;

				switch (value)
				{
					case AnimalButtonMode.None:
						this.Parent.AnimalHoming.State = 0;
						break;

					case AnimalButtonMode.Sheep:
						this.Parent.AnimalHoming.State = 1;
						break;

					case AnimalButtonMode.Rabbit:
						this.Parent.AnimalHoming.State = 2;
						break;

					case AnimalButtonMode.Bird:
						this.Parent.AnimalHoming.State = 3;
						break;

					case AnimalButtonMode.Dog:
						this.Parent.AnimalHoming.State = 4;
						break;

					case AnimalButtonMode.Pig:
						this.Parent.AnimalHoming.State = 5;
						break;

					case AnimalButtonMode.Cat:
						this.Parent.AnimalHoming.State = 6;
						break;

					case AnimalButtonMode.Voice:
						this.Parent.AnimalHoming.State = 7;
						break;

					case AnimalButtonMode.Delete:
						this.Parent.AnimalHoming.State = 8;
						break;

					default:
						goto case AnimalButtonMode.None;
				}

				// ボタン領域内での追尾を有効にする
				this.EnabledHoming = true;
			}
		}


		/// <summary>
		/// 動物ボタン管理クラスの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる作曲画面。</param>
		public AnimalButtons(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.Initialization();
			this.Enabled = true;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();

			// ==============================
			//      部品のインスタンス化
			// ==============================
			this.SheepButton = new SheepButton(this);
			this.BirdButton = new BirdButton(this);
			this.RabbitButton = new RabbitButton(this);
			this.DogButton = new DogButton(this);
			this.PigButton = new PigButton(this);
			this.CatButton = new CatButton(this);
			this.VoiceButton = new VoiceButton(this);
			this.DeleteButton = new DeleteButton(this);

			// ==============================
			//      部品のリストへの登録
			// ==============================
			this.PartsList.Add(this.SheepButton);
			this.PartsList.Add(this.BirdButton);
			this.PartsList.Add(this.RabbitButton);
			this.PartsList.Add(this.DogButton);
			this.PartsList.Add(this.PigButton);
			this.PartsList.Add(this.CatButton);
			this.PartsList.Add(this.VoiceButton);
			this.PartsList.Add(this.DeleteButton);


			// ==============================
			//     テクスチャと座標の登録
			// ==============================
			Muphic.Manager.DrawManager.Regist(this.ToString(), Locations.AnimalButtonArea.Location, "IMAGE_COMPOSITIONSCR_ANIMALBTNAREA");
		}


		/// <summary>
		/// 全ての動物ボタンの選択を解除し、Pressed プロパティ値を false にする。
		/// </summary>
		public void AllClear()
		{
			foreach (Common.Button button in PartsList)
			{
				button.Pressed = false;
			}
		}


		/// <summary>
		/// マウスが動物ボタン領域外へ出た際の処理。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();

			// ボタンから直接領域外へ出ると そのボタンの MouseLeave メソッドが呼ばれないため、
			// ボタン領域から出たら全てのボタンの MouseLeave メソッドを呼ぶ
			foreach (Common.Button button in PartsList)
			{
				button.MouseLeave();
			}

			// ボタン領域から一度出たら、新しい動物ボタンが選択されるまで
			// ボタン領域内での追尾は行わない
			this.EnabledHoming = false;
		}


		/// <summary>
		/// ボタン領域に含まれる各動物ボタンを描画する。
		/// </summary>
		/// <param name="drawStatus">。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			// 基底クラスのパーツ描画メソッドを呼ぶ
			//（無駄にボタン領域のテクスチャを描画するのを防ぐため）
			base.DrawParts(drawStatus);
		}
	}
}
