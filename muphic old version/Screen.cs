using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic
{
	/// <summary>
	/// Screen の概要の説明です。
	/// </summary>
	public class Screen : Base
	{
		public ArrayList BaseList;
		private int prevNum;											//以前のフレームの時点でマウスが指していた部品の要素番号
		
		//ドラッグ用フィールド
		public int beginPartsNum = -1;								//ドラッグ開始したときにクリックしたパーツ
		//public int endPartsNum = -1;									//ドラッグ終了したときにクリックしたパーツ(これとbeginのほうが、同じだとClickメソッドを呼ぶ)
		//private bool isClickAble;									//ドラッグ終了時のこと、ドラッグ開始とドラッグ終了時に参照しているものが同じ場合はtrueになる。
		public Screen()
		{
			//prevPoint = new Point(0, 0)
			prevNum = -1;
			BaseList = new ArrayList();
		}

		/// <summary>
		/// テクスチャ等の描画処理を記述するメソッド。ここでそれぞれの部品のデータを元に画面に
		/// 描画することになる
		/// </summary>
		public virtual void Draw()
		{
			if(this.Visible == false) return;
			muphic.DrawManager.Draw(this.ToString());					//まず自分自身を描画
			for(int i=0;i<BaseList.Count;i++)
			{
				if(BaseList[i] is Screen)								//もし部品がScreen型であったら
				{
					if(((Screen)BaseList[i]).Visible)
					{
						((Screen)BaseList[i]).Draw();						//普通に描画せず、それのDrawメソッドを呼ぶ
					}
				}
				else													//もしただの部品だったら
				{
					if(((Base)BaseList[i]).Visible)
					{
						muphic.DrawManager.Draw(BaseList[i].ToString(), ((Base)BaseList[i]).State);//普通に描画
					}
				}
			}
		}

		/// <summary>
		/// マウスが画面内で動いたときに呼ばれるメソッド。ここでそれぞれの部品のEnterやLeaveの
		/// メソッドを呼ぶことになる
		/// </summary>
		/// <param name="p"></param>
		public virtual void MouseMove(Point p)
		{
			int nowNum = -1;
			for(int i=BaseList.Count-1;i>=0;i--)				//現在の座標がどこの部品に入っているか探す
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());//座標管理から座標をゲットする
				if(inRect(p, r))								//もし現在の座標が部品の中に入っているなら
				{
					if(((Base)BaseList[i]).Visible == false)
					{											//もし非表示中なら、
						continue;								//無視する
					}
					nowNum = i;									//探索完了
					if(BaseList[i] is Screen)					//もし部品がScreen型だったら
					{
						((Screen)BaseList[i]).MouseMove(p);		//MouseMoveも呼んでやる
					}
					break;
				}
			}

			if(prevNum != nowNum)								//前と現在に何らかの変化があった場合、対応するEnterかLeaveを呼ぶ
			{
				if(prevNum == -1)
				{												//もし前の状態で部品を指していないなら
					((Base)BaseList[nowNum]).MouseEnter();		//今回になって何らかの部品を指したということなので
				}												//部品のEnterメソッドを呼ぶ
				else if(nowNum == -1)
				{												//もし現在の状態で部品を指していないなら
					((Base)BaseList[prevNum]).MouseLeave();		//今回になって何らかの部品を示さなくなったということなので、
				}												//部品のLeaveメソッドを呼ぶ
				else
				{												//もし前の部品から違う部品に指すものが変わったら
					((Base)BaseList[prevNum]).MouseLeave();		//部品のEnterメソッドとLeaveメソッドを一度に呼ぶ
					((Base)BaseList[nowNum]).MouseEnter();
				}
			}
			prevNum = nowNum;									//次に呼ばれたときに使うから代入する
		}

		/// <summary>
		/// ドラッグが開始されたときに呼ばれるメソッド
		/// </summary>
		/// <param name="begin">開始座標</param>
		public virtual void DragBegin(System.Drawing.Point begin)
		{
			for(int i=BaseList.Count-1;i>=0;i--)				//現在の座標がどこの部品に入っているか探す
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());//座標管理から座標をゲットする
				if(inRect(begin, r))							//もし現在の座標が部品の中に入っているなら
				{
					if(BaseList[i] is Screen)					//もしそれがScreen型だったら
					{
						((Screen)BaseList[i]).DragBegin(begin);	//そっちのDragBeginもよんでやる
					}
					this.beginPartsNum = i;						//探索完了
					break;
				}
			}
		}

		/// <summary>
		/// ドラッグが終了したときに呼ばれるメソッド
		/// </summary>
		/// <param name="begin">開始座標</param>
		/// <param name="p">現在の座標</param>
		public virtual void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
//			for(int i=BaseList.Count-1;i>=0;i--)				//現在の座標がどこの部品に入っているか探す
//			{
//				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());//座標管理から座標をゲットする
//				if(inRect(p, r))								//もし現在の座標が部品の中に入っているなら
//				{
//					this.endPartsNum = i;						//探索完了
//					break;
//				}
//			}
			if(this.beginPartsNum == -1) return;				//背景をドラッグした場合-1のままもあり得る

			if(BaseList[this.beginPartsNum] is Screen)							//もしそれがScreen型だったら
			{
				((Screen)BaseList[this.beginPartsNum]).DragEnd(begin, p);		//そっちのDragEndもよんでやる
			}

//			if(this.beginPartsNum == this.endPartsNum)
//			{
//				this.isClickAble = true;
//			}
			this.beginPartsNum = -1;											//初期化
//			this.endPartsNum = -1;
		}

		/// <summary>
		/// マウスが画面内でドラッグされたときに呼ばれるメソッド。ここでそれぞれの部品のDrag
		/// メソッドを呼ぶことになる
		/// </summary>
		/// <param name="begin">ドラッグ開始座標</param>
		/// <param name="p">現在の座標</param>
		public override void Drag(Point begin, Point p)
		{
			//DrawManager.DrawString(begin + "now" + p, 0, 100);
			
			if(this.beginPartsNum == -1) return;
			((Base)BaseList[this.beginPartsNum]).Drag(begin, p);			//Base,Screen構いなしに呼ぶ。
		}

		#region 改造前Clickメソッド
		/*
		/// <summary>
		/// マウスが画面内でクリックされたときに呼ばれるメソッド。ここでそれぞれの部品のClick
		/// メソッドを呼ぶことになる
		/// </summary>
		/// <param name="p"></param>
		public override void Click(Point p)
		{
			for(int i=BaseList.Count-1;i>=0;i--)
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());		//該当する部品の座標を取得
				if(inRect(p, r) && ((Base)BaseList[i]).Visible)						//クリック時の座標が部品の中かどうかチェック
				{
					((Base)BaseList[i]).Click(p);									//入っているならその部品のClickメソッドを呼ぶ
					return;
				}
			}
		}
		*/
		#endregion

		/// <summary>
		/// マウスが画面内でクリックされたときに呼ばれるメソッド。ここでそれぞれの部品のClick
		/// メソッドを呼ぶことになる
		/// </summary>
		/// <param name="p"></param>
		public override void Click(Point p)
		{
//			if(this.isClickAble == false)
//			{
//				return;
//			}
			for(int i=BaseList.Count-1;i>=0;i--)
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());		//該当する部品の座標を取得
				if(inRect(p, r) && ((Base)BaseList[i]).Visible)						//クリック時の座標が部品の中かどうかチェック
				{
					// チュートリアル実行中だった場合
					if( TutorialStatus.getIsTutorial() )
					{
						if( TutorialStatus.checkTriggerPartsList(BaseList[i].ToString()) )
						{
							// 次のステートへ進むトリガーとなるパーツだった場合、次のステートへ進むフラグをonにする
							TutorialStatus.setEnableNextState();
						}
						else
						{
							// トリガーではなかった場合、許可リストに入っているかチェックし、入っていなければクリックさせない
							if( !TutorialStatus.checkPermissionPartsList(BaseList[i].ToString()) ) return;
						}
					}
					
					// ☆☆☆ クリックのお知らせ ☆☆☆
					Console.WriteLine("Click : " + BaseList[i].ToString());
						
					// クリック時の座標が部品の中に入っているならその部品のClickメソッドを呼ぶ
					((Base)BaseList[i]).Click(p);
					
					return;
				}
			}
			//this.isClickAble = false;			//1回クリックしたのでfalseにする
		}

		/// <summary>
		/// 許可リストつきクリックメソッド。再生中はbase.Clickより、
		/// こちらを呼んでやったほうがいいと思う。
		/// </summary>
		/// <param name="p"></param>
		/// <param name="PermissionList">許可リスト(ToStringで指定)</param>
		public void Click(Point p, String[] PermissionList)
		{
//			if(this.isClickAble == false)
//			{
//				return;
//			}
			for(int i=BaseList.Count-1;i>=0;i--)
			{
				Rectangle r = muphic.PointManager.Get(BaseList[i].ToString());		//該当する部品の座標を取得
				if(inRect(p, r) && ((Base)BaseList[i]).Visible)						//クリック時の座標が部品の中かどうかチェック
				{
					// クリックされたものが許可リストに入っているかどうか確認
					for(int j=0;j<PermissionList.Length;j++)
					{
						if(BaseList[i].ToString() == PermissionList[j])				//クリックしたものが
						{															//許可リストに入っていた
							// ☆☆☆ クリックのお知らせ ☆☆☆
							Console.WriteLine("Click : " + BaseList[i].ToString());
						
							// クリック時の座標が部品の中に入っているならその部品のClickメソッドを呼ぶ
							((Base)BaseList[i]).Click(p);
					
							return;
						}
					}
				}
			}
			//this.isClickAble = false;			//1回クリックしたのでfalseにする
		}

		protected bool inRect(Point p, Rectangle r)
		{
			if(r.Left <= p.X && p.X <= r.Right)
			{
				if(r.Top <= p.Y && p.Y <= r.Bottom)
				{
					return true;
				}
			}
			return false;
		}
	}
}
