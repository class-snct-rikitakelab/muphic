using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Muphic.MakeStoryScreenParts;
using Muphic.MakeStoryScreenParts.MakeStoryMainParts;
using Muphic.PlayerWorks;

namespace Muphic.PlayerWorks
{
	/// <summary>
	/// ものがたりおんがくモードで作成される物語データクラス。
	/// </summary>
	[Serializable]
	[XmlInclude(typeof(Character))]
	[XmlInclude(typeof(Item))]
	[XmlInclude(typeof(List<string>))]
	[XmlInclude(typeof(Author))]
	//[XmlRoot(ElementName = "物語データ", Namespace = Settings.SystemSettings.XmlNameSpace)
	[XmlRoot(ElementName = "物語データ")]
	public class StoryData : ICloneable
	{
		/// <summary>
		/// muphic のバージョンを表す文字列。
		/// </summary>
		[XmlElement("バージョン")]
		public string Version { get; set; }


		/// <summary>
		/// 物語の制作者名を取得または設定する。
		/// </summary>
		[XmlElement("作者")]
		public List<Author> Authors { get; set; }


		/// <summary>
		/// 物語の題名を表す文字列。
		/// <para>Title プロパティを使用すること。</para>
		/// </summary>
		private string __title;

		/// <summary>
		/// 物語の題名を表す文字列を取得または設定する。
		/// <para>システムで設定された題名の規定の文字数より多い場合、規定数にカットされる。</para>
		/// </summary>
		[XmlElement("題名")]
		public string Title
		{
			get
			{
				return this.__title;
			}
			set
			{
				if (value.Length > Settings.System.Default.StoryMake_MaxTitleLength)
				{
					this.__title = value.Substring(0, Settings.System.Default.StoryMake_MaxTitleLength);
				}
				else
				{
					this.__title = value;
				}
			}
		}


		/// <summary>
		/// 物語を構成するスライドのリストを取得または設定する。
		/// </summary>
		[XmlElement("スライド")]
		public Slide[] SlideList { get; set; }


		/// <summary>
		/// 物語の最大ページ数を示す整数値を取得する。
		/// </summary>
		public int MaxPage
		{
			get
			{
				return this.SlideList.Length;
			}
		}

		/// <summary>
		/// 物語内の編集されているページのうち、最後のページ番号を示す整数値を取得する。編集されているページが１つも無い場合、このプロパティの値は -1 となる。
		/// </summary>
		public int MaxEditedPage
		{
			get
			{
				for (int i = this.SlideList.Length - 1; i >= 0; i--)
				{
					if (!this.SlideList[i].IsEmpty) return i;
				}

				return -1;
			}
		}


		/// <summary>
		/// 物語データの新しいインスタンスを初期化する。
		/// </summary>
		public StoryData()
			:this("", new Slide[Settings.System.Default.StoryMake_MaxPage])
		{
			for (int i = 0; i < this.SlideList.Length; i++) this.SlideList[i] = new Slide(i);
		}

		/// <summary>
		/// 物語データの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="storyTitle">物語の題名。</param>
		/// <param name="slideList">スライドリスト。</param>
		public StoryData(string storyTitle, Slide[] slideList)
		{
			this.Version = System.Windows.Forms.Application.ProductVersion;
			this.Title = storyTitle;
			this.SlideList = slideList;

			this.Authors = new List<Author>();
		}


		/// <summary>
		/// 現在の物語データのディープコピーを作成する。
		/// </summary>
		/// <returns>このインスタンスと同じ値を持つ物語データ。</returns>
		public object Clone()
		{
			return (StoryData)Tools.CommonTools.DeepCopy(this);
		}

	}
}
