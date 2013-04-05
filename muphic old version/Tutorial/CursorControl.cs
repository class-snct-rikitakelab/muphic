using System;
using System.Drawing;
using System.Collections;
using System.Runtime.InteropServices ;  // for DllImport, Marshal

namespace muphic.Tutorial
{
	/// <summary>
	/// �}�E�X�J�[�\���̎�������Ɋւ��郁�\�b�h
	/// MNDM�ɂ��ꎞ����
	/// </summary>
	public class CursorControl
	{
		public TutorialMain parent;
		
		
		[DllImport("user32.dll")]
		extern static uint SendInput(
			uint       nInputs,   // INPUT �\���̂̐�(�C�x���g��)
			INPUT[]    pInputs,   // INPUT �\����
			int        cbSize     // INPUT �\���̂̃T�C�Y
			) ;
		
		[StructLayout(LayoutKind.Sequential)]  // �A���}�l�[�W DLL �Ή��p struct �L�q�錾
			struct INPUT
		{ 
			public int        type ;  // 0 = INPUT_MOUSE(�f�t�H���g), 1 = INPUT_KEYBOARD
			public MOUSEINPUT mi   ;
		}
		
		[StructLayout(LayoutKind.Sequential)]  // �A���}�l�[�W DLL �Ή��p struct �L�q�錾
			struct MOUSEINPUT
		{
			public int    dx ;
			public int    dy ;
			public int    mouseData ;  // amount of wheel movement
			public int    dwFlags   ;
			public int    time      ;  // time stamp for the event
			public IntPtr dwExtraInfo ;
			// Note: struct �̏ꍇ�A�f�t�H���g(�p�����[�^�Ȃ���)�R���X�g���N�^�́A
			//       ���ꑤ�Œ�`�ς݂ŁA�t�B�[���h�� 0 �ɏ���������B
		}
		
		// dwFlags
		const int MOUSEEVENTF_MOVED      = 0x0001 ;
		const int MOUSEEVENTF_LEFTDOWN   = 0x0002 ;  // ���{�^�� Down
		const int MOUSEEVENTF_LEFTUP     = 0x0004 ;  // ���{�^�� Up
		const int MOUSEEVENTF_RIGHTDOWN  = 0x0008 ;  // �E�{�^�� Down
		const int MOUSEEVENTF_RIGHTUP    = 0x0010 ;  // �E�{�^�� Up
		const int MOUSEEVENTF_MIDDLEDOWN = 0x0020 ;  // ���{�^�� Down
		const int MOUSEEVENTF_MIDDLEUP   = 0x0040 ;  // ���{�^�� Up
		const int MOUSEEVENTF_WHEEL      = 0x0080 ;
		const int MOUSEEVENTF_XDOWN      = 0x0100 ;
		const int MOUSEEVENTF_XUP        = 0x0200 ;
		const int MOUSEEVENTF_ABSOLUTE   = 0x8000 ;
		
		const int screen_length = 0x10000 ;			 // for MOUSEEVENTF_ABSOLUTE (���̒l�͌Œ�)
		
		
		[DllImport("user32.dll")]
		private static extern void mouse_event( 
			UInt32 dwFlags, 
			UInt32 dx, 
			UInt32 dy, 
			UInt32 dwData, 
			IntPtr dwExtraInfo 
			); 
		
		/// <summary>
		/// �Ƃ肠�����R���X�g���N�^
		/// </summary>
		/// <param name="tm"></param>
		public CursorControl(TutorialMain tm)
		{
			this.parent = tm;
		}
		
		/// <summary>
		/// �}�E�X�J�[�\�����w�肳�ꂽ���W�܂ňړ������郁�\�b�h
		/// </summary>
		public void CursorMove(Point begin, Point after)
		{
			// �}�E�X�J�[�\���̃X�N���[�����W��muphic���W����Amuphic���W(0,0)�ƂȂ�X�N���[�����WZeroPoint���v�Z�ɂ�苁�߂�
			Point NowPointMuphic = this.parent.parent.parent.parent.nowPoint;
			Point NowPointCursor = this.parent.parent.parent.parent.getCursorPos();
			Point ZeroPoint = new Point(NowPointCursor.X - NowPointMuphic.X, NowPointCursor.Y - NowPointMuphic.Y);
			
			// �ړ��J�n���W�̐ݒ�
			Point beginPoint = new Point(ZeroPoint.X + begin.X, ZeroPoint.Y + begin.Y);
			
			// �}�E�X�J�[�\���̈ʒu���ړ��J�n���W�ֈړ�������
			parent.parent.parent.parent.setCursorPos(beginPoint);
			
			// �v1�C�x���g���i�[
			INPUT[] input = new INPUT[1];  
			
			// �ړ����W�̌v�Z ����Ȃ�ł����̂��������^��
			input[0].mi.dx      = after.X - begin.X + (int)Math.Round((after.X - begin.X) / 50.0) + 5 ;
			input[0].mi.dy      = after.Y - begin.Y + (int)Math.Round((after.Y - begin.Y) / 50.0) + 5 ;
			input[0].mi.dwFlags = MOUSEEVENTF_MOVED;
			
			// �J�[�\���ړ�����̎��s �v1�C�x���g�̈ꊇ����
			SendInput(1, input, Marshal.SizeOf(input[0])) ;
		}
		
		
		/// <summary>
		/// �}�E�X�J�[�\�����w�肵�����W�ֈړ������郁�\�b�h
		/// </summary>
		/// <param name="point"></param>
		public void CursorSet(Point point)
		{
			// �}�E�X�J�[�\���̈ʒu(muphic���W)
			Point NowPointMuphic = this.parent.parent.parent.parent.nowPoint;
			
			// �v1�C�x���g���i�[
			INPUT[] input = new INPUT[1];  
			
			// �ړ����W�̌v�Z ����Ȃ�ł����̂��������^��
			if(NowPointMuphic.X > point.X) input[0].mi.dx = -(NowPointMuphic.X - point.X + (int)Math.Round((NowPointMuphic.X - point.X) / 50.0) + 5) ;
			else                           input[0].mi.dx = point.X - NowPointMuphic.X + (int)Math.Round((point.X - NowPointMuphic.X) / 50.0) + 5 ;
			
			if(NowPointMuphic.Y > point.Y) input[0].mi.dy = -(NowPointMuphic.Y - point.Y + (int)Math.Round((NowPointMuphic.Y - point.Y) / 50.0) + 5) ;
			else                           input[0].mi.dy = point.Y - NowPointMuphic.Y + (int)Math.Round((point.Y - NowPointMuphic.Y) / 50.0) + 5 ;

			input[0].mi.dwFlags = MOUSEEVENTF_MOVED;
			
			// �J�[�\���ړ�����̎��s �v1�C�x���g�̈ꊇ����
			SendInput(1, input, Marshal.SizeOf(input[0]));
		}
		
		
		/// <summary>
		/// ���̎��_�ł̃J�[�\���ʒu�ŃN���b�N���s�����\�b�h
		/// </summary>
		public void CursorClick()
		{
			// �v2�C�x���g���i�[
			INPUT[] input = new INPUT[2];
			
			// ��1,��2�C�x���g���A���ꂼ�ꍶ�{�^��Down�ƍ��{�^��Up�ɐݒ�
			input[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN ;
			input[1].mi.dwFlags = MOUSEEVENTF_LEFTUP ;
			
			// �N���b�N����̎��s �v2�C�x���g�̈ꊇ����
			SendInput(2, input, Marshal.SizeOf(input[0])) ;
		}
	}
}
