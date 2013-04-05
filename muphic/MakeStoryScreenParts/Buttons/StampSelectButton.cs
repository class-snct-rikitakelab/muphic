using System.Drawing;

using Muphic.MakeStoryScreenParts.MakeStoryMainParts;

namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// ものがたりおんがく画面
	/// <para>物語作成画面下部のスタンプ選択ボタン</para>
	/// </summary>
	public class StampSelectButton : Common.Button
	{
		/// <summary>
		/// 親にあたるスタンプ選択ボタン群管理クラス
		/// </summary>
		public StampSelectArea Parent { get; private set; }

		/// <summary>
		/// 左から数えたスタンプ選択ボタンの番号（０～７）。
		/// </summary>
		public int Number { get; private set; }

		///// <summary>
		///// スタンプ選択ボタンのラベルテクスチャ。
		///// </summary>
		///// <remarks>
		///// ラベルテクスチャはカテゴリに合わせ変化するため、汎用ボタンクラスを使用する場合
		///// RectangleManager への登録と削除を頻繁に行う必要がある。
		///// そのため、スタンプ選択ボタンのラベルテクスチャは汎用ボタンクラスと別に用意し、描画を行う。
		///// </remarks>
		//public new string LabelName { get; private set; }

		/// <summary>
		/// ボタンの表示座標を取得または設定する。
		/// </summary>
		public Point Location { get; private set; }


		/// <summary>
		/// スタンプ選択ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="stampSelectArea">親にあたるスタンプ選択ボタン群管理クラス。</param>
		/// <param name="number">左から数えたボタンの番号（０～７）。</param>
		public StampSelectButton(StampSelectArea stampSelectArea, int number)
		{
			this.Parent = stampSelectArea;
			this.Number = number;
			this.Location = (Point)(Settings.PartsLocation.Default["MakeStoryScr_StampBtn" + number.ToString()]);

			this.SetBgTexture(this.ToString(), this.Location, "IMAGE_BUTTON_CIRCLE_BLUE", "IMAGE_BUTTON_CIRCLE_ON", "IMAGE_BUTTON_CIRCLE_YELLOW");
			this.SetLabelTexture(this.ToString(), this.Location, "IMAGE_MAKESTORYSCR_STMPBTN_BG_F1S"); // ここで登録するラベルはエラー対策のダミーなので注意
		}


		/// <summary>
		/// スタンプ選択ボタンに任意のラベルテクスチャを設定し、可視状態にする。
		/// </summary>
		/// <remarks>
		/// カテゴリ選択ボタンが押された際に、カテゴリに応じたラベルテクスチャを設定する際に使用する。
		/// 主に StampSelectArea クラスの CategoryMode プロパティから呼ばれる。
		/// </remarks>
		/// <param name="labelImageName">ラベルテクスチャ名。</param>
		public void SetLabel(string labelImageName)
		{
			this.SetLabel(labelImageName, false);
		}
		/// <summary>
		/// スタンプ選択ボタンに任意のラベルテクスチャを設定し、可視状態にする。
		/// </summary>
		/// <param name="labelImageName">ラベルテクスチャ名。</param>
		/// <param name="divide">《廃止されたため、指定しても動作に影響しないので注意》分割のため左にずらす場合は true 、それ以外は false 。主にアイテム以外の０～３番目のボタンは true となる。</param>
		public void SetLabel(string labelImageName, bool divide)
		{
			// 登録されているラベルテクスチャを削除
			Muphic.Manager.DrawManager.Delete(this.LabelName);

			// 新たに登録し直す　分割指定されている場合はｘ座標を左にずらす
			//this.SetLabelTexture(this.ToString(), new Point(divide ? this.Location.X - 10 : this.Location.X, this.Location.Y), labelImageName);
			this.SetLabelTexture(this.ToString(), new Point(this.Location.X, this.Location.Y), labelImageName);

			// 可視状態にする
			this.Visible = true;

			// 押下されていない状態にする
			this.Pressed = false;
		}



		/// <summary>
		/// スタンプ選択ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			// スタンプ選択ボタン群管理クラスに Number 番目の選択ボタンがクリックされたことを伝える
			this.Parent.Selected(this.Number);
		}


		/// <summary>
		/// 現在の System.Object にボタンの番号を付加した System.String を返す。
		/// </summary>
		/// <returns>現在の System.Object にボタンの番号を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Number.ToString();
		}
	}
}
