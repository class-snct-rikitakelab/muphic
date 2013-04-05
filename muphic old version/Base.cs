using System;
using System.Drawing;
using muphic.Common;

namespace muphic
{
	/// <summary>
	/// Base �̊T�v�̐����ł��B
	/// </summary>
	public class Base
	{
		private bool visible;
		private int state;

		public bool Visible 
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
			}
		}

		public int State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;
			}
		}

		public Base()
		{
			this.Visible = true;
			this.State = 0;
		}

		/// <summary>
		/// �}�E�X�����i���ɓ����Ă����Ƃ��ɌĂ΂�郁�\�b�h
		/// </summary>
		public virtual void MouseEnter()
		{
		}

		/// <summary>
		/// �}�E�X�����i������o�čs�����Ƃ��ɌĂ΂�郁�\�b�h
		/// </summary>
		public virtual void MouseLeave()
		{
		}

		/// <summary>
		/// �}�E�X�����i���ŃN���b�N(�����ɂ̓}�E�X�{�^���������ꂽ)�����ɌĂ΂�郁�\�b�h
		/// </summary>
		/// <param name="p">���݂̃}�E�X���W</param>
		public virtual void Click(Point p)
		{
		}

		/// <summary>
		/// �}�E�X�����i���Ńh���b�O���ꂽ���ɌĂ΂�郁�\�b�h
		/// </summary>
		/// <param name="begin">�h���b�O�J�n���W</param>
		/// <param name="p">���݂̃}�E�X���W</param>
		public virtual void Drag(Point begin, Point p)
		{
		}
	}
}
