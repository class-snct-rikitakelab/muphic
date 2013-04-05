using System;
using System.Collections.Generic;
using System.Drawing;
using Muphic.Manager;

namespace Muphic.Common.DialogParts
{
	/// <summary>
	/// 待機中のアニメーション表示クラス。
	/// </summary>
	public class WaitingAnimation : Screen
	{
		/// <summary>
		/// アニメーションを実行する間隔を示す整数値 (ミリ秒)。
		/// </summary>
		private readonly TimeSpan __span;

		/// <summary>
		/// アニメーションを実行する間隔を示す整数値 (ミリ秒) を取得する。
		/// </summary>
		private TimeSpan Span
		{
			get { return this.__span; }
		}

		/// <summary>
		/// 前回のアニーメーション実行時での起動時間。
		/// </summary>
		private TimeSpan PrevPlayTime { get; set; }

		/// <summary>
		/// アニメーションのテクスチャを表示する座標のリスト。
		/// </summary>
		private List<Point> Locations { get; set; }

		/// <summary>
		/// Span プロパティで指定される時間間隔毎にカウントアップされる、Locations プロパティのインデックス番号。
		/// </summary>
		private int NowLocationIndex { get; set; }


		/// <summary>
		/// アニメーション表示クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="timeSpan">アニメーションを切り替える時間間隔 (ミリ秒)。</param>
		/// <param name="startLocation">アニメーションを表示する基点位置。</param>
		/// <param name="num">アニメーションを切り替える回数。</param>
		/// <param name="locationSpan">アニメーション毎の表示間隔。</param>
		public WaitingAnimation(int timeSpan, Point startLocation, int num, int locationSpan)
		{
			this.__span = new TimeSpan(0, 0, 0, 0, timeSpan);
			this.PrevPlayTime = new TimeSpan();
			this.Locations = new List<Point>();

			if (num <= 1) num = 2;
			for (int i = 0; i < num; i++)
			{
				this.Locations.Add(new Point(startLocation.X + i * locationSpan, startLocation.Y + ((i % 2 == 0) ? -5 : 5)));
			}
		}


		/// <summary>
		/// アニメーションをリセットし、基点位置から描画し直す。
		/// </summary>
		public void Start()
		{
			this.NowLocationIndex = 0;
			this.PrevPlayTime = FrameManager.PlayTime;
		}

		/// <summary>
		/// 次のアニメーションへ切り替える。
		/// </summary>
		private void Next()
		{
			// 基本的に次のインデックス番号となるが、次のインデックス番号で座標リストの範囲外となる場合のみ 0 とする
			this.NowLocationIndex = (this.NowLocationIndex + 1 < this.Locations.Count) ? this.NowLocationIndex + 1 : 0;
		}


		/// <summary>
		/// アニメーションを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			if (FrameManager.PlayTime - this.PrevPlayTime > this.Span)
			{
				this.Next();
				this.PrevPlayTime = FrameManager.PlayTime;
			}

			if (this.NowLocationIndex <= 0)
			{
				DrawManager.Draw("IMAGE_FOOTMARK", this.Locations[0]);
			}
			else if (this.NowLocationIndex >= this.Locations.Count - 1)
			{
				DrawManager.Draw("IMAGE_FOOTMARK", this.Locations[this.NowLocationIndex - 1], (byte)80);
			}
			else
			{
				DrawManager.Draw("IMAGE_FOOTMARK", this.Locations[this.NowLocationIndex - 1], (byte)80);
				DrawManager.Draw("IMAGE_FOOTMARK", this.Locations[this.NowLocationIndex], (byte)200);
			}
		}
	}
}
