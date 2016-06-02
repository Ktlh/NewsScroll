using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

using Leap;
using System.Windows.Forms;

namespace MainProject
{
    internal static class LeapAsMouse
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtrainfo);

        private static System.Drawing.Point GetNormalizedXAndY(Frame frame)
        {
            var interactionBox = frame.InteractionBox;
            var vector = interactionBox.NormalizePoint(frame.Fingers.Frontmost.StabilizedTipPosition);
            var width = SystemInformation.VirtualScreen.Width;
            var height = SystemInformation.VirtualScreen.Height;
            var x = (vector.x * width);
            var y = height - (vector.y * height);

            return new System.Drawing.Point() { X = (int)x, Y = (int)y };
        }
        
        public static void EnableCursorLeap(Frame frame)
        {

            var cordinate = GetNormalizedXAndY(frame);
            if ((int)frame.Fingers[0].TipVelocity.Magnitude <= 25) return;
            SetCursorPos((int)cordinate.X, (int)cordinate.Y);
           //if (frame.Fingers.Count != 5) return;
            //mouse_event(0x0002 | 0x0004, 0, (int)cordinate.X, (int)cordinate.Y, 0);
        }
        public static void EnableClick(Frame frame)
        {
           // if (frame.Fingers.Count != 5) return;
            var cordinate = GetNormalizedXAndY(frame);
            mouse_event(0x0002 | 0x0004, 0, (int)cordinate.X, (int)cordinate.Y, 0);
        }
    }
}
