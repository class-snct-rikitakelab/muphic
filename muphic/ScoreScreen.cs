using System;
using System.Collections.Generic;

using Muphic.Common;
using Muphic.Manager;
using Muphic.ScoreScreenParts;
using Muphic.ScoreScreenParts.Buttons;
using Muphic.ScoreScreenParts.Dialogs;

namespace Muphic
{
	using Animal = Muphic.CompositionScreenParts.Animal;
	using AnimalName = Muphic.CompositionScreenParts.AnimalName;


	#region 楽譜画面関連の列挙型

	/// <summary>
	/// 楽譜画面で、現在表示すべき画面を示す識別子を指定する。
	/// </summary>
	public enum ScoreScreenMode
	{
		/// <summary>
		/// 楽譜画面。
		/// </summary>
		ScoreScreen,

		/// <summary>
		/// 印刷確認画面。
		/// </summary>
		PrintDialog,
	}


	/// <summary>
	/// 楽譜画面で、現在表示すべき楽譜を示す識別子を指定する。
	/// </summary>
	public enum ScoreScreenScoreMode
	{
		/// <summary>
		/// フルスコア。
		/// </summary>
		FullScore,

		/// <summary>
		/// 鳥。
		/// </summary>
		Bird,

		/// <summary>
		/// ヒツジ。
		/// </summary>
		Sheep,

		/// <summary>
		/// ウサギ。
		/// </summary>
		Rabbit,

		/// <summary>
		/// 犬。
		/// </summary>
		Dog,

		/// <summary>
		/// ブタ。
		/// </summary>
		Pig,

		/// <summary>
		/// ネコ。
		/// </summary>
		Cat,

		/// <summary>
		/// 音声。
		/// </summary>
		Voice,
	}

	#endregion


	/// <summary>
	/// 楽譜画面クラス。
	/// <para>各部の作曲画面で作曲した曲から、一般的な 4 分の 4 拍子の楽譜を生成し表示する。</para>
	/// </summary>
	public class ScoreScreen : Screen, IDisposable
	{

		#region フィールドとそのプロパティ

		/// <summary>
		/// 親にあたる汎用作曲画面クラス。
		/// </summary>
		private readonly CompositionScreen __parent;

		/// <summary>
		/// 親にあたる汎用作曲画面クラスを取得する。
		/// </summary>
		public CompositionScreen Parent
		{
			get { return this.__parent; }
		}


		/// <summary>
		/// 楽譜生成メイン処理クラス。
		/// </summary>
		public ScoreMain ScoreMain { get; private set; }

		/// <summary>
		/// 楽譜の背景に表示される動物のラベルを管理するクラス。
		/// </summary>
		public AnimalLabel AnimalLabel { get; private set; }


		/// <summary>
		/// 現在作曲画面で作曲されており、この楽譜画面で表示すべき楽譜データを取得する。
		/// </summary>
		public List<Animal> Score
		{
			get { return this.Parent.CurrentScoreData.AnimalList; }
		}


		#region 各種ボタン / ダイアログ

		/// <summary>
		/// フルスコア表示ボタン。
		/// </summary>
		public FullScoreButton FullScoreButton { get; private set; }

		/// <summary>
		/// ヒツジの楽譜表示ボタン。
		/// </summary>
		public SheepButton SheepButton { get; private set; }

		/// <summary>
		/// 鳥の楽譜表示ボタン。
		/// </summary>
		public BirdButton BirdButton { get; private set; }

		/// <summary>
		/// ウサギの楽譜表示ボタン。
		/// </summary>
		public RabbitButton RabbitButton { get; private set; }

		/// <summary>
		/// イヌの楽譜表示ボタン。
		/// </summary>
		public DogButton DogButton { get; private set; }

		/// <summary>
		/// ブタの楽譜表示ボタン。
		/// </summary>
		public PigButton PigButton { get; private set; }

		/// <summary>
		/// ネコの楽譜表示ボタン。
		/// </summary>
		public CatButton CatButton { get; private set; }

		/// <summary>
		/// 声の楽譜表示ボタン。
		/// </summary>
		public VoiceButton VoiceButton { get; private set; }

		/// <summary>
		/// 楽譜画面から汎用作曲画面へ戻るボタン。
		/// </summary>
		public BackButton BackButton { get; private set; }

		/// <summary>
		/// 楽譜を印刷するボタン。
		/// </summary>
		public PrintButton PrintButton { get; private set; }

		/// <summary>
		/// 楽譜を 1 行次にスクロールするボタン。
		/// </summary>
		public ScrollNextButton ScrollNextButton { get; private set; }

		/// <summary>
		/// 楽譜を 1 行前にスクロールするボタン。
		/// </summary>
		public ScrollBackButton ScrollBackButton { get; private set; }

		/// <summary>
		/// 印刷確認ダイアログ。
		/// </summary>
		public PrintDialog PrintDialog { get; private set; }

		/// <summary>
		/// 題名表示領域。
		/// </summary>
		public Title ScoreTitle { get; private set; }
		//public TitleArea TitleArea { get; set; }

		#endregion


		#region 画面モード

		/// <summary>
		/// 楽譜画面が表示すべき画面の種類。
		/// </summary>
		private ScoreScreenMode __scoreScreenMode;

		/// <summary>
		/// 楽譜画面が表示すべき画面の種類を取得または設定する。
		/// </summary>
		public ScoreScreenMode ScreenMode
		{
			get
			{
				return this.__scoreScreenMode;
			}
			set
			{
				this.__scoreScreenMode = value;
			}
		}

		#endregion


		#region 楽譜モード

		/// <summary>
		/// 楽譜画面が現在表示すべき楽譜の種類。
		/// </summary>
		private ScoreScreenScoreMode __scoreScreenScoreMode;

		/// <summary>
		/// 楽譜画面が現在表示すべき楽譜の種類を取得または設定する。
		/// </summary>
		public ScoreScreenScoreMode ScoreMode
		{
			get
			{
				return this.__scoreScreenScoreMode;
			}
			set
			{
				this.__scoreScreenScoreMode = value;

				// 全てのボタンの表示を有効にする
				this.FullScoreButton.Enabled = true;
				this.SheepButton.Enabled = true;
				this.BirdButton.Enabled = true;
				this.RabbitButton.Enabled = true;
				this.DogButton.Enabled = true;
				this.PigButton.Enabled = true;
				this.CatButton.Enabled = true;
				this.VoiceButton.Enabled = true;

				// 列挙型の値に応じ、各楽譜の描画リストを生成し、該当するボタンを押せなくする
				switch (value)
				{
					case ScoreScreenScoreMode.FullScore:
						this.FullScoreButton.Enabled = false;
						break;

					case ScoreScreenScoreMode.Sheep:
						this.SheepButton.Enabled = false;
						break;

					case ScoreScreenScoreMode.Bird:
						this.BirdButton.Enabled = false;
						break;

					case ScoreScreenScoreMode.Rabbit:
						this.RabbitButton.Enabled = false;
						break;

					case ScoreScreenScoreMode.Dog:
						this.DogButton.Enabled = false;
						break;

					case ScoreScreenScoreMode.Pig:
						this.PigButton.Enabled = false;
						break;

					case ScoreScreenScoreMode.Cat:
						this.CatButton.Enabled = false;
						break;

					case ScoreScreenScoreMode.Voice:
						this.VoiceButton.Enabled = false;
						break;

					default:
						goto case ScoreScreenScoreMode.FullScore;
				}

				// 楽譜を再生成
				this.ScoreMain.CreateTextureList(value);
			}
		}

		#endregion

		#endregion


		/// <summary>
		/// 楽譜画面の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる汎用作曲画面。</param>
		public ScoreScreen(CompositionScreen parent)
		{
			this.__parent = parent;

			// 部品のインスタンス化など
			this.Initialization();

			this.ScoreMode = ScoreScreenScoreMode.FullScore;
		}


		/// <summary>
		/// 楽譜画面を構成する各部品のインスタンス化などを行う。
		/// </summary>
		protected override void Initialization()
		{
			// 登録開始
			base.Initialization(Settings.ResourceNames.ScoreScreenImages);

			#region 統合画像の読み込み

			this.LoadImageFiles();

			#endregion

			#region 部品のインスタンス化

			this.FullScoreButton = new FullScoreButton(this);
			this.SheepButton = new SheepButton(this);
			this.BirdButton = new BirdButton(this);
			this.RabbitButton = new RabbitButton(this);
			this.DogButton = new DogButton(this);
			this.PigButton = new PigButton(this);
			this.CatButton = new CatButton(this);
			this.VoiceButton = new VoiceButton(this);
			this.BackButton = new BackButton(this);
			this.PrintButton = new PrintButton(this);
			this.ScrollNextButton = new ScrollNextButton(this);
			this.ScrollBackButton = new ScrollBackButton(this);
			this.ScoreMain = new ScoreMain(this, this.Score);
			this.AnimalLabel = new AnimalLabel(this);
			this.PrintDialog = new PrintDialog(this);
			this.ScoreTitle = new Title(
				this.ToString(),
				Locations.Title, 
				Settings.System.Default.StoryMake_MaxTitleLength, 
				this.Parent.CurrentScoreData.ScoreName
			);
			

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.FullScoreButton);
			this.PartsList.Add(this.SheepButton);
			this.PartsList.Add(this.BirdButton);
			this.PartsList.Add(this.RabbitButton);
			this.PartsList.Add(this.DogButton);
			this.PartsList.Add(this.PigButton);
			this.PartsList.Add(this.CatButton);
			this.PartsList.Add(this.VoiceButton);
			this.PartsList.Add(this.BackButton);
			this.PartsList.Add(this.PrintButton);
			this.PartsList.Add(this.AnimalLabel);
			this.PartsList.Add(this.ScoreMain);
			this.PartsList.Add(this.ScrollNextButton);
			this.PartsList.Add(this.ScrollBackButton);
			this.PartsList.Add(this.ScoreTitle);

			#endregion

			#region テクスチャと座標の登録

			DrawManager.Regist(this.ToString(), Locations.ScoreScreenArea.Location, "IMAGE_COMMON_BACKGROUND");

			#endregion

			// 登録終了
			DrawManager.EndRegist();
		}


		/// <summary>
		/// 楽譜画面を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			switch (this.ScreenMode)
			{
				case ScoreScreenMode.ScoreScreen:
					base.Draw(drawStatus);
					break;

				case ScoreScreenMode.PrintDialog:
					base.Draw(drawStatus);
					this.PrintDialog.Draw(drawStatus);
					break;
			}
		}


		/// <summary>
		/// 楽譜画面上でマウスがクリックされた際の処理を行う。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case ScoreScreenMode.ScoreScreen:
					base.Click(mouseStatus);
					break;

				case ScoreScreenMode.PrintDialog:
					this.PrintDialog.Click(mouseStatus);
					break;
			}
		}


		/// <summary>
		/// 作曲画面上でマウスが動いた場合の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void MouseMove(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case ScoreScreenMode.ScoreScreen:
					base.MouseMove(mouseStatus);
					break;

				case ScoreScreenMode.PrintDialog:
					this.PrintDialog.MouseMove(mouseStatus);
					break;
			}
		}


		/// <summary>
		/// 何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態。</param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			this.ScoreMain.KeyDown(keyStatus);
		}


		/// <summary>
		/// 作曲画面で使用したリソースを破棄する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
		}

	}
}
