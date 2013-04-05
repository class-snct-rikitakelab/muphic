using System;
using System.Drawing;


namespace muphic.Link.Dialog.Answer
{
	/// <summary>
	/// SelectDialog の概要の説明です。
	/// </summary>
	public class AnswerDialog : Screen
	{
		public LinkScreen parent;
		
		public AnswerBackButton back;
		public AnswerResult result;

		public AnswerDialog(LinkScreen link, bool state)
		{
			this.parent = link;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			result = new AnswerResult(this);
			back = new AnswerBackButton(this);


			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			if (state)
			{
				result.State = 0;
				muphic.SoundManager.Play("OK.wav");
			}
			else
			{
				result.State = 1;
				muphic.SoundManager.Play("NG.wav");
			}
			muphic.DrawManager.Regist(this.ToString(), 124+112, 167+84, "image\\link\\dialog\\answer\\dummy.png");
            muphic.DrawManager.Regist(result.ToString(), 124+112, 167+84, "image\\link\\dialog\\answer\\dialog_true.png", "image\\link\\dialog\\answer\\dialog_false.png");
			muphic.DrawManager.Regist(back.ToString(), 558+112, 368+70, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////

			BaseList.Add(result);
			BaseList.Add(back);
			

			//resultが原因か，backのMouseEnter，MouseLeaveが呼ばれないみたいなので，
			//苦肉の策，画像を切り取ってみました
			
			
			// チュートリアルがでしゃばっているようです。
			if(muphic.Common.TutorialStatus.getIsSPMode() == "PT02_Link30") this.getResult(state);
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}

		public override void Draw()
		{
			base.Draw ();
		}
		
		
		
		
		// チュートリアルがでしゃばっているようです。
		
		/// <summary>
		/// 答え合わせの結果をチュートリアルのメイン部に送る
		/// 自由操作終了の判定に使用
		/// </summary>
		/// <param name="result">結果</param>
		/// <returns></returns>
		public bool getResult(bool result)
		{
			this.parent.parent.tutorialparent.SPCommand("PT02_Link30_" + result.ToString());
			return result;
		}
		
	}
}