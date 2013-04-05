using System.Collections.Generic;

using Muphic.CompositionScreenParts;
using Muphic.PlayerWorks;
using Muphic.MakeStoryScreenParts.MakeStoryMainParts;

namespace Muphic.MakeStoryScreenParts
{
	/// <summary>
	/// 物語の背景の場所を表す識別子を指定する。
	/// </summary>
	public enum BackgroundPlace
	{
		/// <summary>
		/// 未設定
		/// </summary>
		None,

		/// <summary>
		/// 森（道）
		/// </summary>
		Forest1,

		/// <summary>
		/// 森（広場）
		/// </summary>
		Forest2,

		/// <summary>
		/// 川（川付近）
		/// </summary>
		River1,

		/// <summary>
		/// 川（池付近）
		/// </summary>
		River2,

		/// <summary>
		/// 町（道路）
		/// </summary>
		Town1,

		/// <summary>
		/// 町（広場）
		/// </summary>
		Town2,

		/// <summary>
		/// 家（青屋根）
		/// </summary>
		House1,

		/// <summary>
		/// 家（赤屋根）
		/// </summary>
		House2,
	}

	/// <summary>
	/// 物語の背景の天気を表す識別子を指定する。
	/// </summary>
	public enum BackgroundSky
	{
		/// <summary>
		/// 未設定
		/// </summary>
		None,

		/// <summary>
		/// 昼間（晴れ）
		/// </summary>
		Sunny,

		/// <summary>
		/// 昼間（曇り）
		/// </summary>
		Cloudy,

		/// <summary>
		/// 夜（晴れ）
		/// </summary>
		Night,
	}


	/// <summary>
	/// 物語を構成する、絵・楽譜データ・文章 を含むスライドを定義するクラス。
	/// </summary>
	[System.Serializable]
	[System.Xml.Serialization.XmlRoot("スライドデータ")]
	public class Slide
	{
		/// <summary>
		/// 背景の場所を表す BackgroundPlace 列挙型。
		/// </summary>
		[System.Xml.Serialization.XmlElement("場所")]
		public BackgroundPlace BackgroundPlace { get; set; }


		/// <summary>
		/// 背景の空の状態を表す BackgroundSky 列挙型。
		/// </summary>
		[System.Xml.Serialization.XmlElement("空")]
		public BackgroundSky BackgroundSky { get; set; }


		/// <summary>
		/// 絵を構成する Stamp クラスのジェネリック・コレクション。
		/// </summary>
		[System.Xml.Serialization.XmlElement("スタンプ")]
		public List<Stamp> StampList { get; set; }


		/// <summary>
		/// 絵に対する文章を表す文字列。
		/// <para>Sentence プロパティを使用すること。</para>
		/// </summary>
		private string __sentence;

		/// <summary>
		/// 絵に対する文章を表す文字列を取得または設定する。
		/// <para>システムで設定された規定の文章の文字数より多い場合、規定数にカットされる。</para>
		/// </summary>
		[System.Xml.Serialization.XmlElement("文章")]
		public string Sentence
		{
			get
			{
				return this.__sentence;
			}
			set
			{
				if (value.Length > Settings.System.Default.StoryMake_MaxSentenceLength)
				{
					this.__sentence = value.Substring(0, Settings.System.Default.StoryMake_MaxSentenceLength);
				}
				else
				{
					this.__sentence = value;
				}
			}
		}


		/// <summary>
		/// スライドの曲となる楽譜データを取得または設定する。
		/// </summary>
		[System.Xml.Serialization.XmlElement("楽譜データ")]
		public ScoreData ScoreData { get; set; }



		/// <summary>
		/// スライドの新しいインスタンスを初期化する。
		/// </summary>
		public Slide()
			: this(BackgroundPlace.None, BackgroundSky.None, new List<Stamp>(), string.Empty, new ScoreData())
		{
		}

		/// <summary>
		/// スライドの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="number">新しく作成するスライドの番号。</param>
		public Slide(int number)
			: this()
		{
			this.SetScoreName(number);
		}

		/// <summary>
		/// スライドの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="backgroundPlace">絵の背景の場所。</param>
		/// <param name="backgroundSky">絵の空の状態。</param>
		/// <param name="stampList">絵に含まれるスタンプのリスト。</param>
		/// <param name="sentence">スライドの文章。</param>
		/// <param name="scoreData">スライドの曲の楽譜データ。</param>
		public Slide(BackgroundPlace backgroundPlace, BackgroundSky backgroundSky, List<Stamp> stampList, string sentence, ScoreData scoreData)
		{
			this.BackgroundPlace = backgroundPlace;
			this.BackgroundSky = backgroundSky;
			this.StampList = stampList;
			this.Sentence = sentence;
			this.ScoreData = scoreData;
		}


		/// <summary>
		/// このスライドが空の状態かどうかを示す値を取得する。
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return (
					this.BackgroundPlace == BackgroundPlace.None &&
					this.StampList.Count == 0 &&
					!this.ScoreData.IsComposed &&
					string.IsNullOrEmpty(this.Sentence)
				);
			}
		}


		/// <summary>
		/// このスライドに曲がつけられているかどうかを示す値を取得する。
		/// </summary>
		public bool IsComposed
		{
			get
			{
				return this.ScoreData.IsComposed;
			}
		}


		/// <summary>
		/// このスライドの楽譜データに、楽譜タイトルを設定する。
		/// </summary>
		/// <param name="slideNumber">このスライドの番号。</param>
		public void SetScoreName(int slideNumber)
		{
			this.ScoreData.ScoreName =
				Manager.StringManager.ConvertHalfsizeToFullsize(
					Tools.CommonTools.GetResourceMessage(
						Settings.System.Default.StoryComposition_DefaultTitle,
						(slideNumber + 1).ToString()
					)
				);
		}

	}
}
