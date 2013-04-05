using System.Drawing;
using Muphic.Manager;

namespace Muphic.Common.ExplorerParts.Buttons
{
	/// <summary>
	/// エクスプローラ上のファイルを選択するボタン。
	/// </summary>
	public class FileSelectButton : Button
	{
		/// <summary>
		/// 親にあたるファイル選択領域。
		/// </summary>
		public FileSelectArea Parent { get; private set; }

		/// <summary>
		/// 選択領域上で上から何番目のボタンかを示す整数を取得または設定する。
		/// </summary>
		public int Num { get; private set; }

		/// <summary>
		/// 選択領域を表示する位置を示す座標。
		/// </summary>
		private Point Location { get; set; }


		/// <summary>
		/// エクスプローラ上のファイル選択ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるファイル選択領域。</param>
		/// <param name="num">選択領域上で上から何番目のボタンかを示す整数。</param>
		public FileSelectButton(FileSelectArea parent, int num)
		{
			this.Parent = parent;
			this.Num = num;

			this.Location = Settings.GetFileSelectButtonLocation(num);

			this.SetBgTexture(
				this.ToString(),
				this.Location,
				"IMAGE_EXPLORER_SELECTBTN",
				"IMAGE_EXPLORER_SELECTBTN_MON",
				"IMAGE_EXPLORER_SELECTBTN_ON"
			);
			this.LabelName = "";

			this.Location = Settings.GetSelectButtonTextLocation(this.Location);
		}


		/// <summary>
		/// エクスプローラ上のディレクトリ選択ボタンをクリックする。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Select(this.Num + this.Parent.NowPage);
		}

		/// <summary>
		/// エクスプローラ上のファイル選択ボタンを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			StringManager.Draw(this.Parent[this.Num + this.Parent.NowPage], this.Location);
		}


		/// <summary>
		/// 現在の System.Object に、自身のボタンの番号と親クラスの情報を加えた System.String を返す。
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Num.ToString() + "." + this.Parent.Parent.ParentName;
		}
	}
}
