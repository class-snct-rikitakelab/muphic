using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// VoiceButton の概要の説明です。
	/// </summary>
	public class VoiceButton : Base
	{
		StoryButtons parent;
		public VoiceButton(StoryButtons stories)
		{
			parent = stories;

			// 録音・音声無しの設定の場合は押させない
			if (!muphic.Common.CommonSettings.getEnableVoice()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Voice)//既に声ボタンを選択した状態なら
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;	//何も選択していない状態にする
				this.State = 0;												//自分の選択解除
			}
			else															//声ボタンを選択していないなら
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Voice;	//声を選択している状態にする
				this.State = 1;												//自分を選択状態にする
			}
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave ();
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Voice)
			{
				this.State = 0;
			}
		}
	}
}
