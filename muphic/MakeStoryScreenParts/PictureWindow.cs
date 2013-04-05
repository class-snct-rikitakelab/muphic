using System.Drawing;

using Muphic.MakeStoryScreenParts.MakeStoryMainParts;
using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.MakeStoryScreenParts
{
	/// <summary>
	/// 物語作成画面
	/// <para>物語作成画面の絵の背景を描画／管理するクラス。</para>
	/// <para>物語の絵を構成する背景及びスタンプの描画等を行う。</para>
	/// </summary>
	public class PictureWindow : Common.Screen
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// 背景テクスチャ名を表す文字列。
		/// </summary>
		public string BackgroundImage { get; set; }


		/// <summary>
		/// 夜のシーンの描画で使用する色フィルタを取得する。
		/// </summary>
		public static Color NightFilter
		{
			get
			{
				return Color.FromArgb(180, 180, 180);
			}
		}

		/// <summary>
		/// 曇りのシーンの描画で使用する色フィルタを取得する。
		/// </summary>
		public static Color CloudyFilter
		{
			get
			{
				return Color.FromArgb(225, 225, 225);
			}
		}


		/// <summary>
		/// 絵の背景管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public PictureWindow(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			DrawManager.Regist(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_PictureArea.Location, "IMAGE_MAKESTORYSCR_STBG_P_FOREST1");
		}


		#region 描画

		/// <summary>
		/// 絵の背景を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			// 編集中のスライドを描画
			PictureWindow.DrawSlide(this.Parent.CurrentSlide);

			// 絵のフレームを描画
			this.DrawFrame(drawStatus);
		}


		/// <summary>
		/// 物語作成画面のメインウィンドウ内に、与えられたスライドを描画する。
		/// </summary>
		/// <param name="slide">描画するスライド。</param>
		public static void DrawSlide(Slide slide)
		{
			PictureWindow.DrawSlide(slide, Settings.PartsLocation.Default.MakeStoryScr_PictureArea.Location, 1.0F, true);
		}

		/// <summary>
		/// 指定された位置にスライドを描画する。
		/// </summary>
		/// <param name="slide">描画するスライド。</param>
		/// <param name="baseLocation">描画する基点位置 (スライドの左上座標を指定)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void DrawSlide(Slide slide, Point baseLocation, float scaling)
		{
			PictureWindow.DrawSlide(slide, baseLocation, scaling, false);
		}

		/// <summary>
		/// スライドの描画を描画する。
		/// </summary>
		/// <param name="slide">描画するスライド。</param>
		/// <param name="baseLocation">描画する基点位置 (スライドの左上座標を指定)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="isSimplifiedDraw">スタンプの描画を簡易的 (絵内座標ではなく画面座標を利用し固定位置に描画) にする場合は true、それ以外は false。</param>
		private static void DrawSlide(Slide slide, Point baseLocation, float scaling, bool isSimplifiedDraw)
		{
			// スライドの天気に応じた色フィルタを設定する
			Color filter;
			switch(slide.BackgroundSky)
			{
				case BackgroundSky.Sunny:					// 晴れのシーンの場合
					filter = Color.White;					// フィルタ無し 基のテクスチャのまま描画する
					break;	
				case BackgroundSky.Cloudy:					// 曇りのシーンの場合
					filter = PictureWindow.CloudyFilter;	// やや暗くするフィルタをかける
					break;
				case BackgroundSky.Night:					// 夜のシーンの場合
					filter = PictureWindow.NightFilter;		// 暗くするフィルタをかける
					break;

				case BackgroundSky.None:					// 背景未設定の場合
				default:									// またそれ以外の背景の場合（あり得ないけど）
					goto case BackgroundSky.Sunny;			// 晴れのシーンと同じ処理を施す
			}

			// 背景が指定されていた場合、背景の空→場所の順で背景を描画
			if (slide.BackgroundPlace != BackgroundPlace.None)
			{
				// 空の描画
				DrawManager.Draw(MakeStoryTools.GetBackgroundImageName(slide.BackgroundSky), baseLocation, scaling);

				// 背景の場所の描画
				DrawManager.Draw(MakeStoryTools.GetBackgroundImageName(slide.BackgroundPlace), baseLocation, scaling, filter);
			}

			// スタンプの描画
			if (isSimplifiedDraw)
			{
				foreach (Stamp stamp in slide.StampList)
				{													// 簡易描画指定の場合
					DrawManager.DrawCenter(							// 追尾・描画用の座標を直接指定
						stamp.StampImageName,
						stamp.Location,
						filter
					);
				}
			}
			else if (scaling == 1.0)
			{
				foreach (Stamp stamp in slide.StampList)
				{													// 拡大・縮小率が指定されていない場合
					DrawManager.Draw(								// 描画位置は、基点座標にスタンプの絵内座標を加えたものになる
						stamp.StampImageName,
						baseLocation.X + stamp.LocationLocal.X,
						baseLocation.Y + stamp.LocationLocal.Y,
						filter
					);
				}
			}
			else
			{
				foreach (Stamp stamp in slide.StampList)
				{													// 拡大・縮小率が指定されている場合
					DrawManager.Draw(								// 描画位置は、基点座標にスタンプの絵内座標を拡大・縮小し加えたものになる
						stamp.StampImageName,
						(int)(baseLocation.X + stamp.LocationLocal.X * scaling),
						(int)(baseLocation.Y + stamp.LocationLocal.Y * scaling), 
						scaling,
						filter
					);
				}
			}
		}


		/// <summary>
		/// 絵のフレームを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public void DrawFrame(DrawStatusArgs drawStatus)
		{
			DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_TOP", Settings.PartsLocation.Default.MakeStoryScr_PictureFrameTop);
			DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_BTM", Settings.PartsLocation.Default.MakeStoryScr_PictureFrameBottom);
			DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_LEFT", Settings.PartsLocation.Default.MakeStoryScr_PictureFrameLeft);
			DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_RIGHT", Settings.PartsLocation.Default.MakeStoryScr_PictureFrameRight);
		}

		#endregion


		#region 印刷

		/// <summary>
		/// スライドを印刷する。
		/// </summary>
		/// <param name="slide">印刷するスライド。</param>
		/// <param name="baseLocation">描画する基点位置 (スライドの左上座標を指定)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void PrintSlide(Slide slide, Point baseLocation, float scaling)
		{
			// スライドの天気に応じた色フィルタを設定する
			Color filter;
			switch (slide.BackgroundSky)
			{
				case BackgroundSky.Sunny:					// 晴れのシーンの場合
					filter = Color.White;					// フィルタ無し 基のテクスチャのまま描画する
					break;
				case BackgroundSky.Cloudy:					// 曇りのシーンの場合
					filter = PictureWindow.CloudyFilter;	// やや暗くするフィルタをかける
					break;
				case BackgroundSky.Night:					// 夜のシーンの場合
					filter = PictureWindow.NightFilter;		// 暗くするフィルタをかける
					break;

				case BackgroundSky.None:					// 背景未設定の場合
				default:									// またそれ以外の背景の場合（あり得ないけど）
					goto case BackgroundSky.Sunny;			// 晴れのシーンと同じ処理を施す
			}

			// 背景が指定されていた場合、背景の空→場所の順で背景を描画
			if (slide.BackgroundPlace != BackgroundPlace.None)
			{
				// 空の描画
				PrintManager.Regist(MakeStoryTools.GetBackgroundImageName(slide.BackgroundSky), baseLocation, scaling);

				// 背景の場所の描画
				PrintManager.Regist(MakeStoryTools.GetBackgroundImageName(slide.BackgroundPlace), baseLocation, scaling, filter);
			}

			foreach (Stamp stamp in slide.StampList)
			{												// 拡大・縮小率が指定されていない場合
				PrintManager.Regist(						// 描画位置は、基点座標にスタンプの絵内座標を加えたものになる
					stamp.StampImageName,
					baseLocation.X + stamp.LocationLocal.X,
					baseLocation.Y + stamp.LocationLocal.Y,
					scaling,
					filter
				);
			}
		}

		#endregion


		#region スタンプ追加 / 削除

		/// <summary>
		/// 絵の上でクリックされた場合の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			if (this.Parent.StampHoming.HomingMode == HomingMode.StampEnabled)
			{
				// 配置可能な位置で追尾中だった場合
				this.Parent.StampHoming.HomingTarget.Location = mouseStatus.NowLocation;	// 追尾対象のスタンプの現在位置をマウス座標に合わせる
				this.Insert((Stamp)(this.Parent.StampHoming.HomingTarget.Clone()));			// 追尾中のスタンプと等価なインスタンスを生成し、追加
			}
			else if (this.Parent.StampHoming.HomingMode == HomingMode.DeleteEnabled && this.Parent.StampHoming.DeleteTarget != -1)
			{
				// 絵の領域内での削除モードで、削除対象が決まっている場合
				this.Delete(this.Parent.StampHoming.DeleteTarget.Value);					// 当該番号のスタンプを削除する
				this.Parent.StampHoming.UpdateDeleteTarget(mouseStatus.NowLocation);		// 削除後もまだ削除できるスタンプがあるかを調べる
			}

			this.Parent.CurrentSlideChanged();
		}


		/// <summary>
		/// スタンプを新たに追加する。
		/// </summary>
		/// <param name="insertStamp">追加するスタンプ。</param>
		/// <returns>追加に成功した場合は true、それ以外は false。</returns>
		private bool Insert(Stamp insertStamp)
		{
			// 全く同じ位置に同一種類のスタンプが無いかどうかをチェック
			foreach (Stamp stamp in this.Parent.CurrentSlide.StampList)
			{
				if (stamp.Equals(insertStamp))
				{
					return false;
				}
			}

			// 保存用に絵の中での座標をセット
			insertStamp.SetLocationLocal();

			// 編集中のページのスタンプリストに、追尾中のスタンプを追加
			this.Parent.CurrentSlide.StampList.Add(insertStamp);

			DebugTools.ConsolOutputMessage(
				"PictureWindow -Insert",
				string.Format("追加 -- Index:{0} <<X:{1} Y:{2}, Stamp:{3}>>",
					this.Parent.CurrentSlide.StampList.Count - 1,
					insertStamp.LocationLocal.X,
					insertStamp.LocationLocal.Y,
					insertStamp.ToString()),
				true
			);

			return true;
		}

		/// <summary>
		/// スタンプを削除する。
		/// </summary>
		/// <param name="index">削除するスタンプの StampList 内ので番号。</param>
		/// <returns>削除に成功した場合は true、それ以外は false。</returns>
		private bool Delete(int index)
		{
			DebugTools.ConsolOutputMessage(
				"PictureWindow -Delete",
				string.Format("削除 -- Index:{0} <<X:{1} Y:{2}, Stamp:{3}>>",
					index,
					this.Parent.CurrentSlide.StampList[index].LocationLocal.X,
					this.Parent.CurrentSlide.StampList[index].LocationLocal.Y,
					this.Parent.CurrentSlide.StampList[index].ToString()),
				true
			);

			// 編集中のページのスタンプリストから、当該番号のスタンプを削除
			this.Parent.CurrentSlide.StampList.RemoveAt(index);

			return true;
		}

		#endregion


		#region 座標計算

		/// <summary>
		/// 画面上での座標から、絵の中での座標を算出する。
		/// </summary>
		/// <param name="screenPoint">画面上での座標。</param>
		/// <returns>絵の中での座標。</returns>
		public static Point GetLocalPoint(Point screenPoint)
		{
			return new Point(
				screenPoint.X - Settings.PartsLocation.Default.MakeStoryScr_PictureArea.X,
				screenPoint.Y - Settings.PartsLocation.Default.MakeStoryScr_PictureArea.Y
			);
		}

		/// <summary>
		/// 絵の中での座標から、画面上での座標を算出する。
		/// </summary>
		/// <param name="localPoint">絵の中での座標。</param>
		/// <returns>画面上での座標。</returns>
		public static Point GetScreenPoint(Point localPoint)
		{
			return new Point(
				localPoint.X + Settings.PartsLocation.Default.MakeStoryScr_PictureArea.X,
				localPoint.Y + Settings.PartsLocation.Default.MakeStoryScr_PictureArea.Y
			);
		}

		#endregion

	}
}
