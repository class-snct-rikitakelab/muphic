using System.Drawing;
using Muphic.Manager;

namespace Muphic.Common
{
	/// <summary>
	/// 汎用ボタンクラス
	/// <para>画面を構成するボタンはこのクラスを継承して作成する。</para>
	/// </summary>
	public abstract class Button : Parts, System.IDisposable
	{

		#region フィールドとそのプロパティたち

		/// <summary>
		/// 描画管理クラスへの登録用となる、ボタンラベルのテクスチャのキー名を取得または設定する。
		/// </summary>
		public string LabelName { get; protected set; }

		/// <summary>
		/// 描画管理クラスへの登録用となる、ボタン背景のテクスチャのキー名を取得または設定する。
		/// </summary>
		public string BackgroundName { get; protected set; }

		/// <summary>
		/// ボタンの透過度。
		/// </summary>
		public byte Alpha { get; protected set; }

		/// <summary>
		/// [使用しないでくりゃれ。] 描画管理クラスに登録する配置座標が画像中央である場合は true、それ以外は false。
		/// </summary>
		private bool IsCenter { get; set; }


		/// <summary>
		/// ボタンの有効性を示す。
		/// <para>Enabled プロパティを使用すること。</para>
		/// </summary>
		private bool __enabled;

		/// <summary>
		/// ボタンの有効性を示す値を取得または設定する。
		/// <para>可視状態でも無効状態であればクリックできない。</para>
		/// </summary>
		public virtual bool Enabled
		{
			get
			{
				return this.__enabled;
			}
			set
			{
				this.__enabled = value;

				if (value)				// __enabled 値と同時にボタンの透過度も設定する
				{
					this.Alpha = Settings.System.Default.AlphaBlending_PartsEnabled;
				}
				else
				{																		// ボタン無効時は
					this.Alpha = Settings.System.Default.AlphaBlending_PartsDisabled;	// 半透明に設定
					if(this.State == 1) this.SetState();								// マウスオーバーを解除
				}
			}
		}

		/// <summary>
		/// ボタンが押されていることを表わす値。
		/// <para>Pressed プロパティを使用すること。</para>
		/// </summary>
		private bool __pressed;

		/// <summary>
		/// ボタンが押されていることを表わす値を取得または設定する。
		/// <para>このプロパティが true の時、(マウスオーバー時を除いて) State プロパティが 2 になる。</para>
		/// </summary>
		public bool Pressed
		{
			get
			{
				return this.__pressed;
			}
			set
			{
				this.__pressed = value;
				this.SetState();			// Stateの設定
			}
		}

		#endregion


		#region コンストラクタ

		/// <summary>
		/// 汎用ボタンクラスの初期化を行う。
		/// </summary>
		protected Button() : this(true)
		{
		}

		/// <summary>
		/// 汎用ボタンクラスの初期化を行う。
		/// </summary>
		/// <param name="enabled">初期状態でボタンを有効にする場合は true、無効にする場合は false。</param>
		protected Button(bool enabled)
		{
			this.Enabled = enabled;
			this.Pressed = false;
			this.IsCenter = false;
		}

		#endregion


		#region ボタンテクスチャ設定メソッド群


		/// <summary>
		/// ボタンラベルテクスチャを設定する。
		/// state0 のみ指定(通常のボタン)
		/// </summary>
		/// <param name="className">クラス名(通常は this.ToString() )。</param>
		/// <param name="buttonLocation">ボタンの表示座標。</param>
		/// <param name="textureName">登録する際のテクスチャ名。</param>
		protected void SetLabelTexture(string className, Point buttonLocation, string textureName)
		{
			this.SetLabelTexture(className, buttonLocation, textureName, textureName, textureName, textureName);
		}
		/// <summary>
		/// ボタンラベルテクスチャを設定する。
		/// state0 と state2 を指定
		/// </summary>
		/// <param name="className">クラス名(通常は this.ToString() )。</param>
		/// <param name="buttonLocation">ボタンの表示座標。</param>
		/// <param name="state0">ボタン OFF (テクスチャ名)。</param>
		/// <param name="state2">ボタン ON (テクスチャ名)。</param>
		protected void SetLabelTexture(string className, Point buttonLocation, string state0, string state2)
		{
			this.SetLabelTexture(className, buttonLocation, state0, state0, state2, state0);
		}
		/// <summary>
		/// ボタンラベルテクスチャを設定する。
		/// state0 ～ 3 全て指定
		/// </summary>
		/// <param name="className">クラス名(通常は this.ToString() )。</param>
		/// <param name="buttonLocation">ボタンの表示座標。</param>
		/// <param name="state0">ボタンOFF (テクスチャ名)。</param>
		/// <param name="state1">マウスオーバー (テクスチャ名)。</param>
		/// <param name="state2">ボタン ON (テクスチャ名)。</param>
		/// <param name="state3">クリック不能状態 (テクスチャ名)。</param>
		protected void SetLabelTexture(string className, Point buttonLocation, string state0, string state1, string state2, string state3)
		{
			this.LabelName = className + ".Label";
			DrawManager.Regist(this.LabelName, buttonLocation, state0, state1, state2, state3);
		}

		
		/// <summary>
		/// ボタン背景テクスチャを設定する。
		/// state0 と 1 のみ指定(旧版の通常のボタンと同じ)
		/// </summary>
		/// <param name="className">クラス名(通常は this.ToString() )。</param>
		/// <param name="buttonLocation">ボタンの表示座標。</param>
		/// <param name="state0">ボタン OFF (テクスチャ名)。</param>
		/// <param name="state1">マウスオーバー (テクスチャ名)。</param>
		protected void SetBgTexture(string className, Point buttonLocation, string state0, string state1)
		{
			this.SetBgTexture(className, buttonLocation, state0, state1, state0, state0);
		}
		/// <summary>
		/// ボタン背景テクスチャを設定する。
		/// state0、state1、state2 を指定
		/// </summary>
		/// <param name="className">クラス名(通常は this.ToString() )。</param>
		/// <param name="buttonLocation">ボタンの表示座標。</param>
		/// <param name="state0">ボタン OFF (テクスチャ名)。</param>
		/// <param name="state1">マウスオーバー (テクスチャ名)。</param>
		/// <param name="state2">ボタン ON (テクスチャ名)。</param>
		protected void SetBgTexture(string className, Point buttonLocation, string state0, string state1, string state2)
		{
			this.SetBgTexture(className, buttonLocation, state0, state1, state2, state0);
		}
		/// <summary>
		/// ボタン背景テクスチャを設定する。
		/// state0 ～ 3 全て指定
		/// </summary>
		/// <param name="className">クラス名(通常は this.ToString() )。</param>
		/// <param name="buttonLocation">ボタンの表示座標。</param>
		/// <param name="state0">ボタン OFF (テクスチャ名)。</param>
		/// <param name="state1">マウスオーバー (テクスチャ名)。</param>
		/// <param name="state2">ボタン ON (テクスチャ名)。</param>
		/// <param name="state3">クリック不能状態 (テクスチャ名)。</param>
		protected void SetBgTexture(string className, Point buttonLocation, string state0, string state1, string state2, string state3)
		{
			this.BackgroundName = className;
			DrawManager.Regist(this.BackgroundName, buttonLocation, state0, state1, state2, state3);
		}
		

		#endregion


		#region 描画

		/// <summary>
		/// ボタンテクスチャ等の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public virtual void Draw(DrawStatusArgs drawStatus)
		{
			// 現在の状態(State)に従い、ボタンの背景テクスチャを描画
			DrawManager.Draw(this.BackgroundName, this.IsCenter, this.State, this.Alpha);

			// ボタンのラベルテクスチャを描画
			if (!string.IsNullOrEmpty(this.LabelName)) DrawManager.Draw(this.LabelName, this.IsCenter, this.State, this.Alpha);
		}

		#endregion


		#region マウス関連メソッド群

		/// <summary>
		/// マウスが部品内でクリック(厳密にはマウスボタンが離された)た時に呼び出される。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);
		}


		/// <summary>
		/// マウスが部品内に入った時に呼び出される。
		/// </summary>
		public override void MouseEnter()
		{
			this.State = 1;			// マウスオーバー状態にする
		}


		/// <summary>
		/// マウスが部品内から出た時に呼び出される。
		/// </summary>
		public override void MouseLeave()
		{
			this.SetState();		// 背景テクスチャをボタンの状態に応じて設定する
		}

		#endregion


		#region その他

		/// <summary>
		/// Pressed プロパティ値に応じた State プロパティ値を設定する。
		/// </summary>
		private void SetState()
		{
			if (this.Pressed) this.State = 2;
			else this.State = 0;
		}


		/// <summary>
		/// 管理クラスに登録されたテクスチャ名を削除する。
		/// </summary>
		public void Dispose()
		{
			DrawManager.Delete(this.LabelName);
			DrawManager.Delete(this.BackgroundName);
		}

		#endregion

	}
}
