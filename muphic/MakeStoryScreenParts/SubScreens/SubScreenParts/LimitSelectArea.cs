using System.Collections.Generic;
using System.Drawing;

using Muphic.Common;

namespace Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// 作曲補助として使用する音階制限の種類を指定する識別子。
	/// </summary>
	public enum CompositionLimitMode : int
	{
		/// <summary>
		/// 制限無し。
		/// </summary>
		None = 4,

		/// <summary>
		/// ドミソ以外全て制限、四分音符のみ使用。
		/// </summary>
		LimitCode579AndCrotchet = 0,

		/// <summary>
		/// ドミソ以外全て制限、八分音符まで使用。
		/// </summary>
		LimitCode579AndEighthNote = 1,

		/// <summary>
		/// ドミソとレ以外全て制限、八分音符まで使用。
		/// </summary>
		LimitCode5789AndEighthNote = 2,

		/// <summary>
		/// 小節単位で和音組み合わせによる制限、八分音符まで使用。
		/// </summary>
		LimitCodeUnitLine = 3,
	}


	/// <summary>
	/// 物語作曲画面で、使用する音階制限の種類を選択する領域。
	/// </summary>
	public class LimitSelectArea : Screen
	{
		/// <summary>
		/// 親にあたる物語作曲画面。
		/// </summary>
		private readonly StoryCompositionScreen __parent;

		/// <summary>
		/// 親にあたる物語作曲画面を取得する。。
		/// </summary>
		public StoryCompositionScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 音階制限選択ボタン。
		/// </summary>
		public LimitSelectButton[] LimitSelectButtons { get; private set; }

		///// <summary>
		///// 音階制限解除ボタン。
		///// </summary>
		//public LimitSelectButton LimitCancelButton { get; private set; }

		/// <summary>
		/// 作曲時の制限モードを取得または設定する。
		/// </summary>
		public int LimitMode
		{
			get
			{
				return (int)this.Parent.Parent.LimitMode;
			}
			set
			{
				this.Parent.Parent.LimitMode = (CompositionLimitMode)value;

				if (value >= 0 && value < 4)
				{
					this.Parent.CompositionMain.AnimalScore.SetLimitMode(
						LimitSelectArea.CreateLimitCodes(value),
						LimitSelectArea.GetIsUseEighthNote(value),
						value < 3 ? 1 : Manager.ConfigurationManager.Locked.HarmonyNum
					);
				}
				else
				{
					this.Parent.CompositionMain.AnimalScore.SetLimitMode(
						null,
						Manager.ConfigurationManager.Locked.IsUseEighthNote,
						Manager.ConfigurationManager.Locked.HarmonyNum
					);
				}

				foreach (LimitSelectButton lsb in this.LimitSelectButtons)
				{
					lsb.Pressed = false;
					lsb.Enabled = true;
				}
				this.LimitSelectButtons[value].Pressed = true;
				this.LimitSelectButtons[value].Enabled = false;
			}
		}


		/// <summary>
		/// 制限選択領域の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent"></param>
		public LimitSelectArea(StoryCompositionScreen parent)
		{
			this.__parent = parent;

			// 制限ボタンズの生成
			this.LimitSelectButtons = new LimitSelectButton[5];
			for (int i = 0; i < this.LimitSelectButtons.Length; i++)
			{
				this.LimitSelectButtons[i] = new LimitSelectButton(this, i);
				this.PartsList.Add(this.LimitSelectButtons[i]);
			}

			//// 制限解除ボタンの生成
			//this.LimitCancelButton = new LimitSelectButton(this, 4);

			// 制限モードデフォルト値設定
			this.LimitMode = this.LimitMode;

			Manager.DrawManager.Regist(this.ToString(), Locations.LimiSelectArea.Location, "IMAGE_MAKESTORYSCR_LIMITSELECTAREA");
		}


		/// <summary>
		/// 音階制限領域を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.DrawParts(drawStatus);
		}

		/// <summary>
		/// 制限選択領域からマウスが出た際の処理。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();
			foreach (LimitSelectButton lsb in this.LimitSelectButtons)
			{
				lsb.MouseLeave();
			}
		}



		/// <summary>
		/// 音階制限リストを取得する。
		/// </summary>
		/// <remarks>
		/// 音階制限リストの記述 (簡易)
		/// 1 -レ   
		/// 2 -ド   
		/// 3 -シ   
		/// 4 -ラ   
		/// 5 -ソ   
		/// 6 -ファ 
		/// 7 -ミ   
		/// 8 -レ   
		/// 9 -ド   
		/// </remarks>
		private static int[][][] LimitCodes
		{
			get
			{
				return new int[][][]
				{
					new int[][] {
						new int[] { 1, 3, 4, 6, 8 }, 
						new int[] { 1, 3, 4, 6, 8 }, 
						new int[] { 1, 3, 4, 6, 8 }, 
						new int[] { 1, 3, 4, 6, 8 }, 
					},
					new int[][] {
						new int[] { 1, 3, 4, 6, 8 }, 
						new int[] { 1, 3, 4, 6, 8 }, 
						new int[] { 1, 3, 4, 6, 8 }, 
						new int[] { 1, 3, 4, 6, 8 }, 
					},
					new int[][] {
						new int[] { 1, 3, 4, 6 }, 
						new int[] { 1, 3, 4, 6 }, 
						new int[] { 1, 3, 4, 6 }, 
						new int[] { 1, 3, 4, 6 }, 
					},
					new int[][] {
						new int[] { 1, 3, 4, 6, 8 }, 
						new int[] { 1, 3, 5, 7, 8 },
						new int[] { 2, 4, 6, 7, 9 }, 
						new int[] { 1, 3, 4, 6, 8 } 
					}
				};
			}
		}


		/// <summary>
		/// 音階の制限に利用する制限リストを生成する。
		/// </summary>
		/// <returns>制限リスト。</returns>
		public static List<Point> CreateLimitCodes()
		{
			return LimitSelectArea.CreateLimitCodes(3);
		}

		/// <summary>
		/// 音階の制限に利用する制限リストを生成する。
		/// </summary>
		/// <param name="limitMode">制限の種類を示す整数。</param>
		/// <returns>制限リスト。</returns>
		public static List<Point> CreateLimitCodes(int limitMode)
		{
			var limitList = new List<Point>();

			int meterNum = Settings.System.Default.CompositionMeterNum;
			int barLineMax = 16;		// 制限する小節数
			int limitCodeIndex = 0;		// 制限音階リストのうち、対象の小節で使用する制限のインデックス番号

			int[][] limitCodes = LimitSelectArea.LimitCodes[limitMode];

			for (int i = 0; i < barLineMax; i++)
			{										// 小節数分ループし、各小説に必要な音階制限を行う
				int barLine = meterNum * 4 * i;		// i は小節数、barLine は i 番目の小節の最初の拍

				for (int j = 0; j < meterNum * 4; j++)
				{											// 小節内の拍数分ループし、同じ小節内全てに同じ音階制限を行う
					int targetMeter = barLine + j;			// targetMeter は音階制限を行う対象の拍

					for (int k = 0; k < limitCodes[limitCodeIndex].Length; k++)		// 制限する音階の数の分ループし、対象の拍に音階制限を行う
					{
						limitList.Add(new Point(targetMeter, limitCodes[limitCodeIndex][k]));
					}
				}

				limitCodeIndex = ++limitCodeIndex % 4;
			}	// 最後に、使用する制限音階のリスト番が 0 → 1 → 2 → 3 → 0 → 1 → 2 → 3 → 0 → … とループするようにする

			return limitList;
		}


		/// <summary>
		/// 作曲の制限において、指定したモードで八分音符を使用するかどうかを取得する。
		/// </summary>
		/// <param name="limitMode">制限のモードを示す整数。</param>
		/// <returns>八分音符を使用する場合は true、それ以外は false。</returns>
		public static bool GetIsUseEighthNote(int limitMode)
		{
			// そもそも八分音符が使用できなければ false
			if (!Manager.ConfigurationManager.Locked.IsUseEighthNote) return false;

			switch (limitMode)
			{
				case 0:
					return false;

				case 1:
				case 2:
				case 3:
				default:
					return true;
			}
		}

	}
}
