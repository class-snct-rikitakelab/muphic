
namespace Muphic.Common
{
	/// <summary>
	/// 汎用部品クラス
	/// <para>画面を構成する部品はこのクラスを継承して作成する。</para>
	/// </summary>
	[System.Serializable]
	public abstract class Parts
	{
		/// <summary>
		/// 部品の可視性を示す値を取得または設定する。
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public virtual bool Visible { get; set; }

		/// <summary>
		/// 部品の状態を示す整数値を取得または設定する。普通は 0 が通常状態。
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public virtual int State { get; set; }

		/// <summary>
		/// 現在の構成設定を取得する。
		/// </summary>
		public Configuration.ConfigurationData CurrentSettings
		{
			get { return Manager.ConfigurationManager.Current; }
		}


		/// <summary>
		/// 汎用部品クラスの初期化を行う。
		/// </summary>
		protected Parts()
		{
			this.Visible = true;
			this.State = 0;
		}


		/// <summary>
		/// マウスが部品内に入った時に呼び出される。
		/// </summary>
		public virtual void MouseEnter()
		{
		}

		/// <summary>
		/// マウスが部品内から出た時に呼び出される。
		/// </summary>
		public virtual void MouseLeave()
		{
		}

		/// <summary>
		/// マウスが部品内でクリックされた (厳密にはマウスボタンが離された) 時に呼び出される。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public virtual void Click(MouseStatusArgs mouseStatus)
		{
		}
	}
}
