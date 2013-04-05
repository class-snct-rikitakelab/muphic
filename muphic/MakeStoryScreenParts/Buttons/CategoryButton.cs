using System.Drawing;

namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面で、現在選択されているカテゴリボタンを示す識別子を指定する。
	/// </summary>
	/// <remarks>
	/// 定数名からカテゴリの種類（背景・登場人物・アイテム）を判別する処理が存在するため、定数名は変更しないことが望ましい。
	/// やむを得ず変更する場合は、必要に応じて StampSelectArea クラスの Selected メソッドを書き換えること。
	/// </remarks>
	public enum CategoryMode:int
	{
		/// <summary>
		/// 無選択状態
		/// </summary>
		None = -1,

		#region 背景

		/// <summary>
		/// 背景 - 森
		/// </summary>
		BgForest = 0,

		/// <summary>
		/// 背景 - 川
		/// </summary>
		BgRiver = 1,

		/// <summary>
		/// 背景 - 町
		/// </summary>
		BgTown = 2,

		/// <summary>
		/// 背景 - 家
		/// </summary>
		BgHouse,

		#endregion

		#region アイテム

		/// <summary>
		/// アイテム - 屋外
		/// </summary>
		ItemOutdoor,

		/// <summary>
		/// アイテム - 装飾
		/// </summary>
		ItemFashion,

		/// <summary>
		/// アイテム - 食糧
		/// </summary>
		ItemFood,

		/// <summary>
		/// アイテム - 家具
		/// </summary>
		ItemFurniture,

		#endregion

		#region 人物

		/// <summary>
		/// 登場人物 - 男性
		/// </summary>
		HumanMan,

		/// <summary>
		/// 登場人物 - 女性
		/// </summary>
		HumanWoman,

		/// <summary>
		/// 登場人物 - 男児
		/// </summary>
		HumanBoy,

		/// <summary>
		/// 登場人物 - 女児
		/// </summary>
		HumanGirl,

		#endregion

		#region 動物

		/// <summary>
		/// 登場人物 - 犬
		/// </summary>
		AnimalDog,

		/// <summary>
		/// 登場人物 - クマ
		/// </summary>
		AnimalBear,

		/// <summary>
		/// 登場人物 - カメ
		/// </summary>
		AnimalTurtle,

		/// <summary>
		/// 登場人物 - 鳥
		/// </summary>
		AnimalBird,

		#endregion
	}


	/// <summary>
	/// 物語作成画面の、両サイドにある汎用カテゴリ選択ボタン。
	/// <para>16 種のカテゴリ選択ボタンは全てこのクラスで作成する。</para>
	/// </summary>
	public class CategoryButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// 担当するカテゴリを表わす CategoryMode 列挙型。
		/// <para>クリックされた際は、このカテゴリのボタン群を選択領域に表示する。</para>
		/// </summary>
		public CategoryMode CategoryMode { get; private set; }
		

		/// <summary>
		/// カテゴリ選択ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		/// <param name="categoryMode">担当させるカテゴリ。</param>
		/// <param name="location">ボタンの表示座標。</param>
		/// <param name="labelName">ボタンのラベルテクスチャ名。</param>
		public CategoryButton(MakeStoryScreen makeStoryScreen, CategoryMode categoryMode, Point location, string labelName)
		{
			this.Parent = makeStoryScreen;
			this.CategoryMode = categoryMode;

			this.SetBgTexture(this.ToString(categoryMode), location, "IMAGE_BUTTON_HEXAGON_YELLOW", "IMAGE_BUTTON_HEXAGON_ON", "IMAGE_BUTTON_HEXAGON_ORANGE");
			this.SetLabelTexture(this.ToString(categoryMode), location, labelName);
		}


		/// <summary>
		/// カテゴリ選択ボタンがクリックされた際に実行される。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.StampSelectArea.CategoryMode == this.CategoryMode)
			{
				// 既に担当するカテゴリが選択されていた場合は、選択を解除する
				this.Parent.StampSelectArea.CategoryMode = CategoryMode.None;
			}
			else
			{
				// 上記以外の場合は、全てのカテゴリボタンを無選択状態にリセットした上で、
				// 担当するカテゴリを選択状態にする。
				for (int i = 0; i < this.Parent.CategoryButtons.Length; i++)
				{
					this.Parent.CategoryButtons[i].Pressed = false;
				}
				this.Parent.StampSelectArea.CategoryMode = this.CategoryMode;
				this.Pressed = true;
			}
		}



		/// <summary>
		/// 現在の System.Object に、担当するカテゴリ名を追加した Sysyem.String 返す。
		/// </summary>
		/// <returns>現在の System.Object に、担当するカテゴリ名を追加した Sysyem.String。</returns>
		public override string ToString()
		{
			return this.ToString(this.CategoryMode);
		}

		/// <summary>
		/// 現在の System.Object に、指定したカテゴリ名を追加した Sysyem.String 返す。
		/// </summary>
		/// <param name="categoryMode">追加するカテゴリ。</param>
		/// <returns>現在の System.Object に、指定したカテゴリ名を追加した Sysyem.String。</returns>
		public string ToString(CategoryMode categoryMode)
		{
			return base.ToString() + "." + System.Enum.GetName(typeof(CategoryMode), categoryMode);
		}
	}
}
