using System;
using System.Collections;
using System.Drawing;
using muphic.ADV.MsgWindowParts;

namespace muphic.ADV
{
	/// <summary>
	/// ADV�p�[�g�p�̃��b�Z�[�W�\���E�B���h�E
	/// ADV�p�[�g�p�Ƃ������Ă����Ȃ�����͌��\�`���[�g���A���d�l
	/// </summary>
	public class MsgWindow : Screen
	{
		public AdventureMain parent;
			
		public MainWindow mainwindow;
		public NextButton nextbutton;
		public EndButton endbutton;
		public VoiceButton voicebutton;
		
		public string[] Text;		// �e�L�X�g�{��
		public Point TextP;			// �e�L�X�g�̍��W
		public int TextX;			// �e�L�X�g��x���W
		public int TextY;			// �e�L�X�g��y���W
		
		public string Assistant;	// �E�B���h�E���̃A�V�X�^���g����
		public Point AssistantP;	// �A�V�X�^���g�����̍��W(���̂Ƃ���S�������ɂȂ�悤���ߍς�)
		
		public ArrayList NBRegist;	// �x���`��I����Ɏ��փ{�^���\�����O�ŉ摜��regist����ꍇ�̃f�[�^
		
		// �ȉ��e�L�X�g�̒x���`��p�̃t�B�[���h
		public bool DelayDraw;		// �x���`������s�����ǂ����̃t���O
		public int FrameCount;		// �t���[����
		public int LineCount;		// �\��������s��
		public int CharCount;		// �\�������镶����
		const int DelayTime = 7;	// �ꕶ���\������̂ɕK�v�ȃt���[����
		
		public bool nextbuttonvisible;	// ���փ{�^����\�������邩�ǂ���
		public bool endbuttonvisible;	// �I���{�^����\�������邩�ǂ���
		
		public MsgWindow(AdventureMain adv)
		{
			this.parent = adv;
			this.Assistant = "None";
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			mainwindow = new MainWindow(this);
			nextbutton = new NextButton(this);
			endbutton = new EndButton(this);
			voicebutton = new VoiceButton(this);
			Text = new string[3];
			
			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(mainwindow.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\window_alpha.png");
			muphic.DrawManager.Regist(nextbutton.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\next_off.png", "image\\TutorialXGA\\MsgWindow\\next_on.png");
			muphic.DrawManager.Regist(endbutton.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\end_off.png", "image\\TutorialXGA\\MsgWindow\\end_on.png");
			muphic.DrawManager.Regist(voicebutton.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\voice_off.png", "image\\TutorialXGA\\MsgWindow\\voice_on.png");
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(mainwindow);
			BaseList.Add(nextbutton);
			BaseList.Add(endbutton);
			BaseList.Add(voicebutton);
			
			this.ChangeWindowCoordinate(1);
		}
		
		
		/// <summary>
		/// �e�L�X�g�{���𓾂郁�\�b�h
		/// </summary>
		/// <param name="text">�e�L�X�g�{��</param>
		/// <param name="visible">���փ{�^����`�悷�邩</param>
		public void getText(string[] text)
		{
			getText(text, true);
			this.Visible = true;
		}
		
		/// <summary>
		/// �e�L�X�g�{���𓾂郁�\�b�h
		/// </summary>
		/// <param name="text">�e�L�X�g�{��</param>
		/// <param name="visible">true�Ȃ�x���`����J�n����</param>
		public void getText(string[] text, bool visible)
		{
			this.Text = text;
			this.trimText();					// ������̖����̋󔒂��폜
			if(visible) this.StartDelayDraw();	// �V���ȕ�����Œx���`����J�n
		}
		
		
		/// <summary>
		/// ���փ{�^����`�悷�邩�ǂ����𓾂郁�\�b�h
		/// </summary>
		/// <param name="visible">���փ{�^����`�悷��Ȃ�true</param>
		public void getNextButtonVisible(bool visible)
		{
			this.nextbuttonvisible = visible;
		}
		
		
		/// <summary>
		/// �e�L�X�g�{���̍s���Ƃ̕����̋󔒂��폜����
		/// </summary>
		public void trimText()
		{
			for(int i=0; i<this.Text.Length; i++)
			{
				this.Text[i] = this.Text[i].TrimEnd();
			}
		}
		
		
		/// <summary>
		/// ���b�Z�[�W�E�B���h�E�̉��ɃN���b�N�𓧉߂����邩�ǂ���
		/// </summary>
		/// <returns>true�Ȃ牺�̉�ʂ̃N���b�N</returns>
		public bool ClickThrough()
		{
			System.Console.WriteLine(this.Visible);
			System.Console.WriteLine(this.nextbutton.Visible);
			System.Console.WriteLine(!(this.Visible & this.nextbutton.Visible));
			return !(this.Visible & this.nextbutton.Visible);
		}
		
		
		/// <summary>
		/// �A�V�X�^���g�������Z�b�g����
		/// </summary>
		/// <param name="assistant"></param>
		public void setAssistant(string assistant)
		{
			this.Assistant = assistant;
		}
		
		
		/// <summary>
		/// �E�B���h�E�̍��W��ύX���郁�\�b�h
		/// mode�t�B�[���h�̒l�ɂ���ĕω�����
		/// </summary>
		/// <param name="mode">���[�h(1���ʏ�̍��W)</param>
		public void ChangeWindowCoordinate(int mode)
		{
			int x=0, y=0;
			
			// ���[�h�ɂ���č��W��ς���
			switch(mode)
			{
				case 0:
					this.Visible = false;
					break;
				case 1:
					// �ʏ�̈ʒu
					x = 45;
					y = 571;
					goto default;
				case 2:
					// ��ԏ�̈ʒu
					x = 45;
					y = 6;
					goto default;
				case 3:
					// �^��
					x = 45;
					y = 232;
					// �Ŕ̕��������Ȃ��Ƃ����Ȃ�
					muphic.Common.TutorialStatus.setDisableDrawString();
					goto default;
				default:
					this.Visible = true;
					break;
			}
			
			// �o�^����Ă�����W��ύX����
			PointManager.Set(mainwindow.ToString(),  new Rectangle[] {new Rectangle(x,     y,     937, 157), new Rectangle(x,     y,     937, 157)} );
			PointManager.Set(nextbutton.ToString(),  new Rectangle[] {new Rectangle(x+815, y+91,  101, 54),  new Rectangle(x+815, y+91,  101, 54 )} );
			PointManager.Set(endbutton.ToString(),   new Rectangle[] {new Rectangle(x+815, y+28,  101, 54),  new Rectangle(x+815, y+28,  101, 54 )} );
			PointManager.Set(voicebutton.ToString(), new Rectangle[] {new Rectangle(x+125, y+104, 54,  52),  new Rectangle(x+125, y+104, 54,  52 )} );
			
			// �e�L�X�g�̕\�����W���ύX����
			this.TextP = new Point(x+170-7+40, y+39-6);
			
			// �A�V�X�^���g�����̕\�����W���ύX����
			this.AssistantP = new Point(x-7, y-6);
		}
		
		
		/// <summary>
		/// �e�L�X�g�̒x���`����J�n���郁�\�b�h
		/// </summary>
		public void StartDelayDraw()
		{
			// �e�t�B�[���h�̏��������s��
			this.LineCount = 1;
			this.CharCount = 0;
			this.FrameCount = 0;
			
			// �N���b�N������֎~����
			muphic.Common.TutorialStatus.setEnableClickControl();
			
			// ���փ{�^�����\���ɂ���
			this.nextbutton.Visible = false;
			
			// �x���`��J�n
			this.DelayDraw = true;
			
			// ���ł�Voice�Đ�
			//((TutorialScreen)this.parent.parent).tutorialmain.PlayVoice();
		}
		
		
		/// <summary>
		/// �e�L�X�g�̒x���`����I�����郁�\�b�h
		/// </summary>
		public void EndDelayDraw()
		{
			// �x���`��I��
			this.DelayDraw = false;
			
			// ���փ{�^�����o���O�ɉ摜��Regist����ݒ�ɂȂ��Ă���΍s��
			if(this.NBRegist.Count != 0)
			{
				for(int i=0; i<this.NBRegist.Count/4; i++)
				{
					DrawManager.Regist((string)this.NBRegist[i*4], (int)this.NBRegist[i*4+1], (int)this.NBRegist[i*4+2], (string[])this.NBRegist[i*4+3]);
				}

				// �N���A���Ă��܂���
				this.NBRegist.Clear();
			}
			
			// �N���b�N�����������
			muphic.Common.TutorialStatus.setDisableClickControl();
			
			// ���փ{�^�����\�������ݒ�ɂȂ��Ă���ꍇ�͕\��������
			if(this.nextbuttonvisible) this.nextbutton.Visible = true;
		}
		
		
		public override void Draw()
		{
			base.Draw ();
			
			// �A�V�X�^���g�����̕`�� "None"�������ꍇ��t�@�C�����e�[�u���ɓo�^�������������ꍇ�͔�΂�
			if(this.Assistant != "None" && FileNameManager.GetFileExist(this.Assistant) && this.Visible)
			{
				DrawManager.Draw(this.Assistant, AssistantP.X, AssistantP.Y);
			}
			
			// �e�L�X�g�{���̕`��
			if(this.Visible)
			{
				// �e�L�X�g�̒x���`������s�����ǂ����ŏ����������
				if(!this.DelayDraw)
				{
					// �x���`������s���łȂ��ꍇ�͕��ʂɕ`��
					
					for(int i=0; i<this.Text.Length; i++)
					{
						DrawManager.DrawString(this.Text[i], this.TextP.X, this.TextP.Y + i*36);
					}
				}
				else
				{
					// �x���`������s���������ꍇ
					
					int i=0;
					for(; i<this.Text.Length; i++)
					{
						// �܂��A�s�J�E���g�𒴂����s���͕\�����Ȃ�
						if(this.LineCount <= i) break;
						
						// �����J�E���g���s�̕������𒴂��ĂȂ���΁A�����J�E���g�ȍ~�̕�������폜
						string temp = this.Text[i];
						if( this.LineCount-1 == i && this.CharCount < this.Text[i].Length )
						{
							temp = temp.Remove(this.CharCount, this.Text[i].Length-this.CharCount);
						}
						
						// ������̕`��
						DrawManager.DrawString(temp, this.TextP.X, this.TextP.Y + i*36);
					}
					
					this.FrameCount++;
					
					if(this.FrameCount == DelayTime)
					{
						this.CharCount++;
						this.FrameCount = 0;
					}
					
					// �s�ƕ����̍Ō�܂ŃJ�E���g���I��������A�x���`����I��点��
					if( (this.LineCount >= this.Text.Length) && (this.CharCount > this.Text[this.Text.Length-1].Length) )
					{
						this.EndDelayDraw();
					}
					
					// �����J�E���g���s�̕������ɒB���Ă�����
					if(this.CharCount > this.Text[i-1].Length)
					{
						// �s�J�E���g�𑝂₵�A�����J�E���g�����Z�b�g
						this.LineCount++;
						this.CharCount = 0;
					}
				}
			}
		}
		
		public override void Click(Point p)
		{
			base.Click (p);
		}

		public override void MouseMove(Point p)
		{
			base.MouseMove (p);
		}
		
	}
}
