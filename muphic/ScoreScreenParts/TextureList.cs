using System.Collections.Generic;
using System.Drawing;

namespace Muphic.ScoreScreenParts
{
	/// <summary>
	/// 楽譜の描画の際、描画するテクスチャ名とその座標のペアを予め登録しておく為のコレクションクラス。
	/// </summary>
	public class TextureList
	{
		/// <summary>
		/// テクスチャ名を保持する。
		/// </summary>
		private List<string> TextureNameList { get; set; }

		/// <summary>
		/// テクスチャを描画する座標を保持する。
		/// </summary>
		private List<Point> LocationList { get; set; }

		/// <summary>
		/// このリストに登録されているテクスチャと座標のペアの数を取得する。
		/// </summary>
		public int Count
		{
			get { return this.TextureNameList.Count; }
		}

		/// <summary>
		/// foreach 文で利用可能なコレクションとして、このリストに含まれるテクスチャ名と座標のペアを取得する (イテレータ構文)。
		/// </summary>
		public IEnumerable<KeyValuePair<string, Point>> Enumerable
		{
			get
			{
				for (int i = 0; i < this.TextureNameList.Count; i++)
					yield return new KeyValuePair<string, Point>(this.TextureNameList[i], this.LocationList[i]);
			}
		}


		/// <summary>
		/// テクスチャ名とその座標のペアを保持するリストの新しいインスタンスを初期化する。
		/// </summary>
		public TextureList()
		{
			this.TextureNameList = new List<string>();
			this.LocationList = new List<Point>();
		}


		/// <summary>
		/// テクスチャ名と座標のペアをリストに登録する。
		/// </summary>
		/// <param name="textureName">登録するテクスチャ名。</param>
		/// <param name="location">登録するテクスチャを描画する座標。</param>
		public void Add(string textureName, Point location)
		{
			this.TextureNameList.Add(textureName);
			this.LocationList.Add(location);
		}

		/// <summary>
		/// 登録されているテクスチャ名とその座標のペアを全て削除する。
		/// </summary>
		public void Clear()
		{
			this.TextureNameList.Clear();
			this.LocationList.Clear();
		}

	}
}
