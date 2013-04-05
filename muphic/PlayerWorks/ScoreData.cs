using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Muphic.CompositionScreenParts;

namespace Muphic.PlayerWorks
{
	/// <summary>
	/// ユーザが作成した楽譜データを扱う。
	/// </summary>
	[Serializable]
	[XmlRoot("楽譜データ")]
	public class ScoreData
	{
		/// <summary>
		/// muphic のバージョンを表す文字列。
		/// </summary>
		[XmlElement("バージョン")]
		public string Version { get; set; }


		/// <summary>
		/// 曲の名前を表す文字列。
		/// </summary>
		private string __scoreName;

		/// <summary>
		/// 曲の名前を表す文字列を取得または設定する。
		/// <para>システムで設定された曲名の規定の文字数より多い場合、規定数にカットされる。</para>
		/// </summary>
		[XmlElement("曲名")]
		public string ScoreName
		{
			get
			{
				return this.__scoreName;
			}
			set
			{
				if (value.Length > Settings.System.Default.Composition_MaxTitleLength)
				{
					this.__scoreName = value.Substring(0, Settings.System.Default.Composition_MaxTitleLength);
				}
				else
				{
					this.__scoreName = value;
				}
			}
		}


		/// <summary>
		/// 曲のテンポを示す整数値を取得または設定する。
		/// </summary>
		[XmlElement("テンポ")]
		public int Tempo { get; set; }


		/// <summary>
		/// 楽譜 (動物リスト) を表す Animal クラスのジェネリック・コレクションを取得または設定する。
		/// </summary>
		[XmlElement("動物")]
		public List<Animal> AnimalList { get; set; }


		/// <summary>
		/// この楽譜データが空の状態かどうかを示す値を取得する。
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return (string.IsNullOrEmpty(this.ScoreName) && this.AnimalList.Count == 0);
			}
		}

		/// <summary>
		/// この楽譜データに何らかの動物が配置されているかどうかを示す値を取得する。
		/// </summary>
		public bool IsComposed
		{
			get
			{
				return (this.AnimalList.Count != 0);
			}
		}



		/// <summary>
		/// 楽譜データの新しいインスタンスを初期化する。
		/// </summary>
		public ScoreData()
			:this("", new List<Animal>(), Muphic.CompositionScreenParts.Tempo.Default)
		{
		}


		/// <summary>
		/// 楽譜データの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="scoreName">新しい楽譜の曲名。</param>
		/// <param name="animalList">新しい楽譜 (動物リスト)。</param>
		/// <param name="tempo">新しい楽譜のテンポ。</param>
		public ScoreData(string scoreName, List<Animal> animalList, int tempo)
			: this(scoreName, animalList, tempo, Manager.SystemInfoManager.MuphicVersion)
		{
		}

		/// <summary>
		/// 楽譜データの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="scoreName">新しい楽譜の曲名。</param>
		/// <param name="animalList">新しい楽譜 (動物リスト)。</param>
		/// <param name="tempo">新しい楽譜のテンポ。</param>
		/// <param name="version">muphic の現在のバージョン。</param>
		public ScoreData(string scoreName, List<Animal> animalList, int tempo, string version)
		{
			this.ScoreName = scoreName;
			this.AnimalList = animalList;
			this.Tempo = tempo;
			this.Version = version;
		}

	}
}
