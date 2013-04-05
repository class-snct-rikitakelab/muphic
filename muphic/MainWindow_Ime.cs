//using System;
//using System.Drawing;
//using System.Runtime.InteropServices;
//using System.Text;
using System.Windows.Forms;

namespace Muphic
{
    /// <summary>
    /// muphic メインウィンドウ
    /// </summary>
    public partial class MainWindow : Form
    {
//        [Flags]
//        public enum ImmAssociateContextExFlags : uint
//        {
//            /// <summary>
//            /// 
//            /// </summary>
//            IACE_CHILDREN = 0x0001,
			
//            /// <summary>
//            /// 
//            /// </summary>
//            IACE_DEFAULT = 0x0010,
			
//            /// <summary>
//            /// 
//            /// </summary>
//            IACE_IGNORENOCONTEXT = 0x0020
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        [DllImport("imm32.dll")]
//        private static extern IntPtr ImmCreateContext();

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="hWnd"></param>
//        /// <param name="hIMC"></param>
//        /// <param name="dwFlags"></param>
//        /// <returns></returns>
//        [DllImport("Imm32.dll")]
//        public static extern bool ImmAssociateContextEx(IntPtr hWnd, IntPtr hIMC, ImmAssociateContextExFlags dwFlags);

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="hWnd"></param>
//        /// <param name="hIMC"></param>
//        /// <returns></returns>
//        [DllImport("Imm32.dll", CharSet = CharSet.Auto)]
//        public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

//        IntPtr immHandle;
//        string str = string.Empty;
//        Font font; SolidBrush brush;


//        void key_press(object obj, KeyPressEventArgs e)
//        {
//            str = str + e.KeyChar.ToString();
//            Tools.DebugTools.ConsolOutputMessage("key_press", e.KeyChar.ToString());
//            //Refresh();
//        }
//        protected override void OnPaint(PaintEventArgs e)
//        {
//            //e.Graphics.DrawString(str, font, brush, 0, 0);
//        }

//        protected override void WndProc(ref Message m)
//        {
//            switch (m.Msg)
//            {
//                case 0x0281:    //WM_IME_SETCONTEXT  0x0281
//                    immHandle = ImmCreateContext();
//                    bool rc = ImmAssociateContextEx(this.Handle, immHandle, ImmAssociateContextExFlags.IACE_DEFAULT);
//                    break;
//            }
//            base.WndProc(ref m);
//        }


//        private void testIme_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            ImmReleaseContext(this.Handle, immHandle);
//        }

	}
}
