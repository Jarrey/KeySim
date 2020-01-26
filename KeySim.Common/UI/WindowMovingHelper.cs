using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KeySim.Common.UI
{
    public class WindowMovingHelper
    {
        public WindowMovingHelper(Window window)
        {
            Window = window;
        }

        private Window Window { get; set; }

        #region Window move methods
        private bool isMoving = false;
        private Point currentPosition = new Point();
        public void Caption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMoving = true;
            (sender as UIElement).CaptureMouse();
            currentPosition = e.GetPosition(Window);

            currentPosition.Y = Convert.ToInt16(Window.Top) + currentPosition.Y;
            currentPosition.X = Convert.ToInt16(Window.Left) + currentPosition.X;
        }

        public void Caption_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMoving = false;
            (sender as UIElement).ReleaseMouseCapture();
        }

        public void Caption_MouseMove(MouseEventArgs e)
        {
            if (isMoving)
            {
                Point p = e.GetPosition(Window);
                Point MousePositionAbs = new Point
                {
                    X = Convert.ToInt16(Window.Left) + p.X,
                    Y = Convert.ToInt16(Window.Top) + p.Y
                };
                Window.Left = Window.Left + (MousePositionAbs.X - currentPosition.X);
                Window.Top = Window.Top + (MousePositionAbs.Y - currentPosition.Y);
                currentPosition = MousePositionAbs;
            }
        }
        #endregion
    }
}
