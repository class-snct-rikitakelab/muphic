﻿using System.Collections.Generic;
using System.Text;

using Muphic.PlayerWorks;
using Muphic.MakeStoryScreenParts;
using Muphic.Manager;

namespace Muphic.Tools.Printer
{
	/// <summary>
	/// 物語を印刷する。
	/// </summary>
	public sealed class StoryPrinter
	{
		/// <summary>
		/// 印刷する対象の物語データ。
		/// </summary>
		private readonly StoryData __target;

		/// <summary>
		/// 印刷する対象の物語データを取得する。
		/// </summary>
		public StoryData Target
		{
			get { return this.__target; }
		}


		#region コンストラクタ

		/// <summary>
		/// 印刷する物語を指定し、印刷クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="target">印刷する対象の物語データ。</param>
		public StoryPrinter(StoryData target)
		{
			this.__target = target;
		}

		#endregion


		#region 印刷メソッド

		/// <summary>
		/// 児童向けの物語を印刷する。主に出前授業等で利用する。
		/// muphic 構成設定ファイルで指定された作者の人数分印刷され、それぞれの作者名がリストの上になったパターンが 1 枚ずつ印刷される。
		/// </summary>
		public void PrintForStudent()
		{
			this.PrintForStudent(this.Target);
		}


		/// <summary>
		/// 児童向けの物語を印刷する。
		/// muphic 構成設定ファイルで指定された作者の人数分印刷され、それぞれの作者名がリストの上になったパターンが 1 枚ずつ印刷される。
		/// </summary>
		private void PrintForStudent(StoryData printData)
		{
			// 改変されても問題ないように、ディープコピーを作成しそちらを印刷する
			printData = (StoryData)Tools.CommonTools.DeepCopy(printData);

			// 現在印刷中のページと印刷する最大ページの変数
			int nowPage, maxPage = printData.MaxEditedPage + 1;

			int nowPrintPage, maxPrintPage;

			if (ConfigurationManager.Current.IsReversePrintNumber)
			{
				#region 最後のページから逆順に印刷する場合
				nowPrintPage = ConfigurationManager.Current.UserNum * (printData.MaxEditedPage + 1) - 1;
				maxPrintPage = 0;

				while (nowPrintPage >= maxPrintPage)
				{
					for (nowPage = 0; nowPage < maxPage; nowPage++)
					{
						// 印刷するページをセット
						PrintManager.ChangePage(nowPrintPage);

						this.RegistTitle(printData.Title);										// "だいめい" + 題名 の登録
						this.RegistPicture(printData.SlideList[nowPage]);						// 絵の登録
						this.RegistScore(printData.SlideList[nowPage].ScoreData.AnimalList);	// 楽譜の生成
						this.RegistPageNum(nowPage + 1);										// ページ数の登録
						this.RegistSentence(printData.SlideList[nowPage].Sentence);				// 文章の登録
						this.RegistMuphicLogo();												// muphic ロゴの登録
						this.RegistAuthors(printData.Authors);									// 作者の印刷

						nowPrintPage--;
					}

					// リストの一番下の作者名を一番上に登録し直す
					if (printData.Authors != null && printData.Authors.Count >= 2)
					{
						printData.Authors.Insert(0, printData.Authors[printData.Authors.Count - 1]);
						printData.Authors.RemoveAt(printData.Authors.Count - 1);
					}
				}
				#endregion
			}
			else
			{
				#region １ページ目から順に印刷する場合
				nowPrintPage = 0;
				maxPrintPage = ConfigurationManager.Current.UserNum * (printData.MaxEditedPage + 1);

				while (nowPrintPage < maxPrintPage)
				{
					for (nowPage = 0; nowPage < maxPage; nowPage++)
					{
						// 印刷するページをセット
						PrintManager.ChangePage(nowPrintPage);

						this.RegistTitle(printData.Title);										// "だいめい" + 題名 の登録
						this.RegistPicture(printData.SlideList[nowPage]);						// 絵の登録
						this.RegistScore(printData.SlideList[nowPage].ScoreData.AnimalList);	// 楽譜の生成
						this.RegistPageNum(nowPage + 1);										// ページ数の登録
						this.RegistSentence(printData.SlideList[nowPage].Sentence);				// 文章の登録
						this.RegistMuphicLogo();												// muphic ロゴの登録
						this.RegistAuthors(printData.Authors);									// 作者の印刷

						nowPrintPage++;
					}

					// リストの一番下の作者名を一番上に登録し直す
					if (printData.Authors != null && printData.Authors.Count >= 2)
					{
						printData.Authors.Insert(0, printData.Authors[printData.Authors.Count - 1]);
						printData.Authors.RemoveAt(printData.Authors.Count - 1);
					}
				}
				#endregion
			}

			PrintManager.Print();
		}


		#region 印刷用サブメソッド群

		/// <summary>
		/// 印刷対象のページに、印刷する題名を登録する。
		/// </summary>
		/// <param name="title">印刷する題名。</param>
		private void RegistTitle(string title)
		{
			PrintManager.RegistString(
				Locations.PrintForStudentTitleDesc,
				Locations.PrintForStudentTitleDescLocation,
				Locations.PrintForStudentTitleDescFontSize
			);
			PrintManager.RegistString(
				Locations.PrintForStudentTitle.Replace("%str1%", title),
				Locations.PrintForStudentTitleLocation,
				Locations.PrintForStudentTitleFontSize
			);
		}

		/// <summary>
		/// 印刷対象のページに、印刷する絵を登録する。
		/// </summary>
		/// <param name="picture">印刷する絵。</param>
		private void RegistPicture(Slide picture)
		{
			PictureWindow.PrintSlide(
				picture,
				Locations.PrintForStudentSlideLocation,
				Locations.PrintForStudentSlideScaling
			);
		}

		/// <summary>
		/// 印刷対象のページに、印刷する楽譜を登録する。
		/// </summary>
		/// <param name="animalList">印刷する楽譜。</param>
		private void RegistScore(List<Muphic.CompositionScreenParts.Animal> animalList)
		{
			int registNum = DrawManager.BeginRegist(false);
			var scoreMain = new ScoreScreenParts.ScoreMain(animalList);
			DrawManager.EndRegist();

			// 楽譜の登録
			for (int i = 0; i < scoreMain.MaxPage; i++)
				scoreMain.Print(i + 1, false, Locations.PrintForStudentScorePageScaling, Locations.PrintForStudentScorePageLocation[i]);

			DrawManager.Delete(registNum);
			scoreMain.Dispose();
		}

		/// <summary>
		/// 印刷対象のページに、印刷するページ番号を登録する。
		/// </summary>
		/// <param name="page">印刷するページ番号。</param>
		private void RegistPageNum(int page)
		{
			PrintManager.RegistString(
				Locations.PrintForStudentPageDesc.Replace("%str1%", page.ToString()),
				Locations.PrintForStudentPageDescLocation,
				Locations.PrintForStudentPageDescFontSize
			);
		}

		/// <summary>
		/// 印刷対象のページに、印刷する文章を登録する。
		/// </summary>
		/// <param name="sentence">印刷する文章。</param>
		private void RegistSentence(string sentence)
		{
			StringBuilder[] sentenceList = new StringBuilder[] { new StringBuilder(), new StringBuilder(), new StringBuilder() };

			for (int i = 0, j = 0; i < sentence.Length; i++)
			{
				sentenceList[j].Append(sentence[i]);
				if (i % 12 == 11) j++;
			}
			for (int i = 0; i < sentenceList.Length; i++)
			{
				PrintManager.RegistString(
					sentenceList[i].ToString() + "　",
					Locations.PrintForStudentSentenceLocation[i],
					Locations.PrintForStudentSentenceFontSize
				);
			}
		}

		/// <summary>
		/// 印刷対象のページに、印刷する作者名を登録する。
		/// </summary>
		/// <param name="authors">印刷する作者名。</param>
		private void RegistAuthors(List<Author> authors)
		{
			// Null 値だった場合は当然何もしない
			if (authors == null) return;

			// 作者名しか使わないので作者名のみのジェネリックコレクションを別途作る
			List<string> authorNames = new List<string>();
			foreach (Author author in authors) authorNames.Add(author.Name);

			// 空の作者名を全て削除する
			while (authorNames.Contains("")) authors.RemoveAt(authorNames.IndexOf(""));

			// 作者名が登録されていなければ何もしない
			if (authors.Count < 1) return;

			PrintManager.RegistString(
				Locations.PrintForStudentAuthorDesc,
				Locations.PrintForStudentAuthorDescLocation,
				Locations.PrintForStudentAuthorDescFontSize
			);

			for (int i = 0, max = (authorNames.Count < 2) ? authorNames.Count : 2; i < max; i++)
			{
				PrintManager.RegistString(
					authorNames[i],
					Locations.PrintForStudentAuthorLocations[i],
					Locations.PrintForStudentAuthorFontSize
				);
			}
		}

		/// <summary>
		/// 印刷対象のページに、muphic のロゴを登録する。
		/// </summary>
		private void RegistMuphicLogo()
		{
			PrintManager.Regist("IMAGE_COMMON_LOGO", Locations.PrintForStudentLogoLocation);
		}

		#endregion

		#endregion
		
	}
}
