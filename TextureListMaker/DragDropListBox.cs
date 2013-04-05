using System.Drawing;
using System.Windows.Forms;

namespace TextureListMaker
{
	/// <summary>
	/// ドラッグ＆ドロップによる並べ替えが可能な項目のリストを表示する Windows コントロールを表しますん。
	/// </summary>
	public class DragDropListBox : ListBox
	{
		/// <summary>
		/// ドラッグ＆ドロップされるアイテム。
		/// </summary>
		private struct DragDropItemData
		{
			public DragDropItemData(int index)
			{
				__index = index;
			}

			private int __index;

			public int Index
			{
				get { return __index; }
			}
		}


		/// <summary>
		/// DragDropListBox の新しいインスタンスを初期化する。
		/// </summary>
		public DragDropListBox()
			: base()
		{
			this.AllowDrop = true;
		}


		/// <summary>
		/// ドラッグ＆ドロップの開始点。
		/// </summary>
		private Point mouseDownPoint;

		/// <summary>
		/// ドラッグするアイテムの index 番号。
		/// </summary>
		private int dragDropSourceItemIndex;


		/// <summary>
		/// MouseDown
		/// </summary>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				// ドラッグアンドドロップの開始点
				mouseDownPoint = new Point(e.X, e.Y);

				dragDropSourceItemIndex = this.IndexFromPoint(e.X, e.Y);
			}
			else
			{
				mouseDownPoint = Point.Empty;

				dragDropSourceItemIndex = ListBox.NoMatches;
			}

			base.OnMouseDown(e);
		}


		/// <summary>
		/// MouseUp
		/// </summary>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			mouseDownPoint = Point.Empty;

			base.OnMouseUp(e);
		}

		/// <summary>
		/// MouseMove
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				// ドラッグアンドドロップの範囲にあるか否かをチェックする
				Rectangle dragBound = new Rectangle(e.X - SystemInformation.DragSize.Width / 2, e.Y - SystemInformation.DragSize.Height / 2, SystemInformation.DragSize.Width, SystemInformation.DragSize.Height);

				if (!dragBound.Contains(mouseDownPoint) && dragDropSourceItemIndex != ListBox.NoMatches)
				{
					// ドラッグアンドドロップ用のデータを作成
					DragDropItemData itemData = new DragDropItemData(dragDropSourceItemIndex);

					// アイテムをドラッグアンドドロップする
					DragDropEffects effects = this.DoDragDrop(itemData, DragDropEffects.Move);
				}
			}

			base.OnMouseMove(e);
		}


		/// <summary>
		/// DragEnter
		/// </summary>
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (drgevent.Data.GetDataPresent(typeof(DragDropItemData)))
			{
				drgevent.Effect = DragDropEffects.Move;
			}
			else
			{
				drgevent.Effect = DragDropEffects.None;
			}

			base.OnDragEnter(drgevent);
		}


		/// <summary>
		/// DragDrop
		/// </summary>
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if (drgevent.Data.GetDataPresent(typeof(DragDropItemData)))
			{
				// 入れ替え先のインデックス
				int targetIndex = this.IndexFromPoint(this.PointToClient(new Point(drgevent.X, drgevent.Y)));

				if (targetIndex == ListBox.NoMatches) targetIndex = this.Items.Count - 1;

				// ドラッグアンドドロップするアイテムのデータを取得
				DragDropItemData itemData = (DragDropItemData)drgevent.Data.GetData(typeof(DragDropItemData));

				// アイテムを入れ替え、入れ替えたアイテムを選択する
				if (itemData.Index > targetIndex)
				{
					this.Items.Insert(targetIndex, this.Items[itemData.Index]);
					this.Items.RemoveAt(itemData.Index + 1);
					this.SelectedIndex = targetIndex;
				}
				else
				{
					this.Items.Insert(targetIndex+1, this.Items[itemData.Index]);
					this.Items.RemoveAt(itemData.Index);
					this.SelectedIndex = targetIndex;
				}


			}

			base.OnDragDrop(drgevent);
		}

	}
}
