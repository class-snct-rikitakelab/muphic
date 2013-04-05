using System;

namespace muphic.ADV.MsgWindowParts
{
	/// <summary>
	/// VoiceButton の概要の説明です。
	/// </summary>
	public class VoiceButton : Base
	{
		public MsgWindow parent;
		
		public VoiceButton(MsgWindow msgwindow)
		{
			this.parent = msgwindow;
			this.State = 1;				// デフォルトで音声をONにしておく
		}

		/// <summary>
		/// 音声を再生するか否かを問い合わせる
		/// </summary>
		/// <returns></returns>
		public bool getIsVoice()
		{
			if(this.State == 0) return false;
			else return true;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			if(this.State == 0)
			{
				// ボタン押したら直ぐ文の頭から音声を流すべきか、次の文から流すべきか
				this.State = 1;
				((TutorialScreen)this.parent.parent.parent).tutorialmain.isPlayVoice = true;
			}
			else 
			{
				// ボタンをOFFにし、音声の停止
				this.State = 0;
				((TutorialScreen)this.parent.parent.parent).tutorialmain.StopVoice();
				((TutorialScreen)this.parent.parent.parent).tutorialmain.isPlayVoice = false;
			}
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
		}
	}
}
