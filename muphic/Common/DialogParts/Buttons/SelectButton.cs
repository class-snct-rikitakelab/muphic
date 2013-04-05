using System.Drawing;
using System.IO;

namespace Muphic.Common.DialogParts.Buttons
{
	/// <summary>
	/// ファイル選択ボタン
	/// </summary>
	public class SelectButton : Button
	{
		/// <summary>
		/// 親にあたるファイル選択領域。
		/// </summary>
		public SelectArea Parent { get; private set; }

		/// <summary>
		/// 選択領域上での上から何番目のボタンかを表わす整数値。
		/// </summary>
		public int Num { get; set; }

		/// <summary>
		/// ボタンを表示する位置を表す System.Drawing.Point 構造体。
		/// </summary>
		private Point Location { get; set; }


		/// <summary>
		/// ファイル選択ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="selectArea">親にあたるファイル選択領域。</param>
		/// <param name="num">選択領域上での上から何番目のボタンかを表わす整数値。</param>
		public SelectButton(SelectArea selectArea, int num)
		{
			this.Parent = selectArea;
			this.Num = num;

			this.Location = Tools.CommonTools.AddPoints(this.Parent.Parent.Location, new Point(Locations.ListBase.X, Locations.ListBase.Y + num * 20));

			this.SetBgTexture(this.ToString(), this.Location, "IMAGE_DIALOG_FILESELECT_OFF", "IMAGE_DIALOG_FILESELECT_ON");
			this.LabelName = "";
		}


		/// <summary>
		/// ファイル選択ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Result = this.Parent.NowPage + this.Num;
			this.Parent.Parent.DialogResult = DialogResult.OK;
		}


		/// <summary>
		/// ファイル選択ボタンと付随する文字列を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			//if (this.Parent.Parent.FileNameList.Length - 4 > this.Parent.NowPage + this.Num) return;

			base.Draw(drawStatus);

			Manager.StringManager.Draw(Path.GetFileNameWithoutExtension(this.Parent.Parent.FileNameList[this.Parent.NowPage + this.Num]), this.Location.X + 24, this.Location.Y + 3);
		}


		/// <summary>
		/// 現在の System.Object に、自身のボタンの番号とダイアログの親クラスの情報を加えた System.String を返す。
		/// </summary>
		/// <returns>。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Num.ToString() + "." + this.Parent.Parent.ParentName;
		}

	}
}
