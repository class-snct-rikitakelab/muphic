using Muphic.PlayerWorks;
using Muphic.MakeStoryScreenParts;
using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.Common.ExplorerParts
{
	/// <summary>
	/// ファイル エクスプローラ上の作品情報表示領域。
	/// </summary>
	public class WorkInfoArea : Screen
	{
		/// <summary>
		/// 親にあたるエクスプローラ。
		/// </summary>
		public Explorer Parent { get; private set; }


		/// <summary>
		/// 作品の種類を示す文字列を取得する。
		/// </summary>
		public string WorkType { get; private set; }

		/// <summary>
		/// 作品の題名を示す文字列を取得する。
		/// </summary>
		public string WorkTitle { get; private set; }

		/// <summary>
		/// 作品の作者を示す文字列を取得する。
		/// </summary>
		public string[] WorkAuthors { get; private set; }

		/// <summary>
		/// 表示する対象の作品データを取得する。
		/// </summary>
		public object Target { get; private set; }

		/// <summary>
		/// 対象の作品データが印刷済みかどうかを取得する。
		/// </summary>
		public bool IsPrinted { get; private set; }


		/// <summary>
		/// 作品情報表示領域の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるエクスプローラ。</param>
		public WorkInfoArea(Explorer parent)
		{
			this.Parent = parent;

			DrawManager.Regist(this.ToString(), Settings.WorkSelectArea.Location, "IMAGE_EXPLORER_WORK_BG");
		}


		/// <summary>
		/// 指定したパスの作品データを表示する。
		/// </summary>
		/// <param name="targetPath">表示する作品のパス。</param>
		public void Show(string targetPath)
		{
			if (this.Parent.TargetType == ExplorerTargetType.StoryData)
			{
				this.Show(Tools.IO.XmlFileReader.ReadStoryData(targetPath));
			}
		}


		/// <summary>
		/// 指定した物語の作品データを表示する。
		/// </summary>
		/// <param name="storyData">表示する物語データ。</param>
		private void Show(StoryData storyData)
		{
			this.Target = storyData;

			this.Update();
		}


		/// <summary>
		/// 作品データの表示を更新する。
		/// </summary>
		public void Update()
		{
			if (this.Parent.TargetType == ExplorerTargetType.StoryData)
			{
				StoryData target = (StoryData)(this.Target);

				// 作品情報 (種別 / 題名 / 作者 / 印刷) を更新
				this.WorkType = "ものがたり";
				this.WorkTitle = CommonTools.TrimDisplayString(target.Title, Muphic.Settings.System.Default.StoryMake_MaxTitleLength);
				this.IsPrinted = this.Parent.FileSelectArea.ContainsPrintedFilePath();
				// 作者名は作者情報クラスから名前のみ取り出したリストを別途作成する
				var authorNames = new System.Collections.Generic.List<string>();
				foreach(Author author in target.Authors) authorNames.Add(author.Name);
				this.WorkAuthors = authorNames.ToArray();

				// 印刷と読込のボタンを有効化
				this.Parent.LoadButton.Enabled = true;
				this.Parent.PrintButton.Enabled = true;
			}
		}


		/// <summary>
		/// 作品データの表示を初期化する。
		/// </summary>
		public void Clear()
		{
			this.WorkType = "";
			this.WorkTitle = "";
			this.WorkAuthors = new string[] { "" };
			this.IsPrinted = false;

			this.Parent.LoadButton.Enabled = false;
			this.Parent.PrintButton.Enabled = false;
		}


		/// <summary>
		/// 作品情報表示領域を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			if (!string.IsNullOrEmpty(this.WorkType))
			{
				StringManager.Draw(this.WorkType, Settings.GetWorkInfoTextLocation(0));
			}
			if (!string.IsNullOrEmpty(this.WorkTitle))
			{
				StringManager.Draw(this.WorkTitle, Settings.GetWorkInfoTextLocation(1));
			}

			for (int i = 0; i < this.WorkAuthors.Length; i++)
			{
				StringManager.Draw(this.WorkAuthors[i], Settings.GetWorkInfoTextLocation(2 + i));
			}

			if (this.IsPrinted)
			{
				DrawManager.Draw("IMAGE_EXPLORER_PRINTEDLABEL", Settings.GetWorkInfoTextLocation(5));
			}
		}

	}
}
