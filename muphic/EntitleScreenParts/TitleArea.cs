using System.Drawing;
using Muphic.Manager;

namespace Muphic.EntitleScreenParts
{
	/// <summary>
	/// [使用しないでくりゃれ。] 題名表示領域。現在は汎用題名クラス Common.Title を使用する。
	/// </summary>
	public class TitleArea : Common.Screen
	{
		///// <summary>
		///// 親にあたる題名入力画面。
		///// </summary>
		//public EntitleScreen Parent { get; private set; }

		///// <summary>
		///// 文字列と背景の描画開始座標 (中央座標) を取得または設定する。
		///// </summary>
		//public Point StartLocation { get; private set; }

		///// <summary>
		///// 題名表示領域の新しいインスタンスを初期化する。
		///// </summary>
		///// <param name="parent">親にあたる題名入力画面。</param>
		//public TitleArea(EntitleScreen parent)
		//{
		//    this.Parent = parent;

		//    // 題名表示領域背景の先頭部分を登録
		//    DrawManager.Regist(this.ToString(), Locations.Title, "IMAGE_ENTITLESCR_TITLE_BG_1");

		//    // 題名表示領域
		//    this.StartLocation = new Point(Locations.TitleString.X, RectangleManager.Get(this.ToString()).Top); 
		//}


		///// <summary>
		///// 題名表示領域(背景部と題名文字列)の描画を行う。
		///// </summary>
		///// <param name="drawStatus">描画時の状態データ。</param>
		//public override void Draw(DrawStatusArgs drawStatus)
		//{
		//    base.Draw(drawStatus);

		//    for (int i = 0; i < this.Parent.MaxLength; i++)
		//    {
		//        // 文字列の最大文字数分だけ、背景テクスチャを横に描画していく
		//        DrawManager.Draw("IMAGE_ENTITLESCR_TITLE_BG_2", this.StartLocation.X + TextureFileManager.GetRectangle("IMAGE_ENTITLESCR_TITLE_BG_2").Width * i, this.StartLocation.Y);
		//    }

		//    // 背景の末尾テクスチャを描画
		//    DrawManager.Draw("IMAGE_ENTITLESCR_TITLE_BG_3", this.StartLocation.X + TextureFileManager.GetRectangle("IMAGE_ENTITLESCR_TITLE_BG_2").Width * this.Parent.MaxLength, this.StartLocation.Y);

		//    // 題名文字列を描画
		//    StringManager.Draw(this.Parent.Text, Locations.TitleString);

		//    // 0.5秒毎に、入力中の文字の位置を表わすアンダーバーを点滅させる
		//    if ((FrameManager.PlayTime.Milliseconds < 500) && this.Parent.Text.Length < this.Parent.MaxLength)
		//    {
		//        DrawManager.Draw("IMAGE_ENTITLESCR_TITLE_UNDER",
		//            this.StartLocation.X + TextureFileManager.GetRectangle("IMAGE_ENTITLESCR_TITLE_BG_2").Width * this.Parent.Text.Length,
		//            Locations.TitleString.Y + StringManager.StringSize.Height);
		//    }
		//}
	}
}
