
namespace Muphic.MakeStoryScreenParts.MakeStoryMainParts
{
	/// <summary>
	/// アイテムの種類を表わす識別子を指定する。
	/// </summary>
	public enum ItemKind : int
	{
		/// <summary>
		/// 未指定
		/// </summary>
		None = -1,

		#region カテゴリ 装飾

		/// <summary>
		/// 男性用帽子（青）
		/// </summary>
		CapBlue = 0,

		/// <summary>
		/// 男性用帽子（緑）
		/// </summary>
		CapGreen,

		/// <summary>
		/// 女性用帽子（黄色）
		/// </summary>
		HatYellow,

		/// <summary>
		/// 女性用帽子（ピンク）
		/// </summary>
		HatPink,

		/// <summary>
		/// 麦藁帽子
		/// </summary>
		Straw,

		/// <summary>
		/// リボン
		/// </summary>
		Ribbon,

		/// <summary>
		/// バッグ（赤）
		/// </summary>
		BagRed,

		/// <summary>
		/// バッグ（黒）
		/// </summary>
		BagBlack,

		#endregion

		#region カテゴリ 屋外

		/// <summary>
		/// 虫取り網
		/// </summary>
		CaptureNet,

		/// <summary>
		/// 虫かご
		/// </summary>
		InsectCase,

		/// <summary>
		/// カブトムシ
		/// </summary>
		Beetle,

		/// <summary>
		/// ウサ人形
		/// </summary>
		RabbitDoll,

		/// <summary>
		/// ラッパ
		/// </summary>
		Bugle,

		/// <summary>
		/// 携帯電話
		/// </summary>
		CellPhone,

		/// <summary>
		/// ボール
		/// </summary>
		Ball,

		/// <summary>
		/// 車
		/// </summary>
		Car,

		#endregion

		#region カテゴリ 食糧

		/// <summary>
		/// おにぎり
		/// </summary>
		RiceBall,

		/// <summary>
		/// ハンバーグ
		/// </summary>
		Hamburger,

		/// <summary>
		/// プリン
		/// </summary>
		Pudding,

		/// <summary>
		/// コーヒー
		/// </summary>
		Caffe,

		/// <summary>
		/// 犬の餌
		/// </summary>
		DogFood,

		/// <summary>
		/// 食用魚
		/// </summary>
		Fish,

		/// <summary>
		/// どんぐり
		/// </summary>
		Acorn,

		/// <summary>
		/// リンゴ
		/// </summary>
		Apple,

		#endregion

		#region カテゴリ 家具

		/// <summary>
		/// 椅子（左向き）
		/// </summary>
		ChairLeft,

		/// <summary>
		/// 椅子（右向き）
		/// </summary>
		ChairRight,

		/// <summary>
		/// テーブル
		/// </summary>
		Table,

		/// <summary>
		/// 時計
		/// </summary>
		Clock,

		/// <summary>
		/// テレビ
		/// </summary>
		Television,

		/// <summary>
		/// 化粧台
		/// </summary>
		DressingBoard,

		/// <summary>
		/// 本棚
		/// </summary>
		BookRack,

		/// <summary>
		/// 箪笥
		/// </summary>
		Drawer,

		#endregion
	}


	/// <summary>
	/// ものがたりの絵を構成するアイテムクラス
	/// </summary>
	[System.Serializable]
	[System.Xml.Serialization.XmlRoot("アイテムデータ")]
	public class Item : Stamp, System.ICloneable
	{
		/// <summary>
		/// アイテムの種類を指定する。
		/// </summary>
		[System.Xml.Serialization.XmlElement("種類")]
		public ItemKind ItemKind { get; set; }


		/// <summary>
		/// アイテムクラスの新しいインスタンスを初期化する。
		/// </summary>
		public Item()
			: this(ItemKind.None)
		{
		}
		/// <summary>
		/// アイテムクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="itemKind">アイテムの種類。</param>
		public Item(ItemKind itemKind)
			: this(new System.Drawing.Point(-100, -100), itemKind)
		{
		}
		/// <summary>
		/// アイテムクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="location">アイテムの位置。</param>
		/// <param name="itemKind">アイテムの種類。</param>
		public Item(System.Drawing.Point location, ItemKind itemKind)
			: base(location)
		{
			this.ItemKind = itemKind;

			this.SetStampImageName();
		}


		/// <summary>
		/// このインスタンスのアイテムの種類が設定されていれば、それに応じたスタンプ画像名を StampImageName プロパティに設定する。
		/// </summary>
		/// <returns>スタンプ画像名を設定した場合は true、それ以外は false。</returns>
		public bool SetStampImageName()
		{
			if (this.ItemKind != ItemKind.None)
			{
				this.StampImageName = Tools.MakeStoryTools.GetItemStampImageName(this.ItemKind);
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 現在のインスタンスと等価の Item オブジェクトを生成する。
		/// </summary>
		/// <returns>生成された新しいインスタンス。</returns>
		public override object Clone()
		{
			return new Item(this.Location, this.ItemKind);
		}


		/// <summary>
		/// このアイテムを表す System.String を返す。
		/// </summary>
		/// <returns>アイテムの種類を含む System.String。</returns>
		public override string ToString()
		{
			return string.Format("Item/{0}", System.Enum.GetName(typeof(ItemKind), this.ItemKind));
		}
	}
}
