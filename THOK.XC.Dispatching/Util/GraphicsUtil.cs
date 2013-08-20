using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace THOK.XC.Dispatching.Util
{
    public class GraphicsUtil
    {
        public static Image CreateRandomBitmap(out string text)
        {
            Random rd = new Random();

            text = rd.Next(0, 1000000).ToString().PadLeft(6, "0"[0]);
            Image image = new Bitmap(80, 16);
            Graphics g = Graphics.FromImage(image);

            Font font = new Font("ËÎÌו", 10);

            g.Clear(Color.White);
            g.DrawRectangle(Pens.Red, 0, 0, 80, 16);

            g.DrawString(text, font, new SolidBrush(Color.Green), 0, 0);

            return image;
        }

        public static Image  CreateBitmap(string text)
        {
            Image image = new Bitmap(272, 96);
            Graphics g = Graphics.FromImage(image);

            Font font = new Font("ËÎÌו", 10);

            g.Clear(Color.White);
            g.DrawRectangle(Pens.Red, 0, 0, 272, 96);

            try
            {
                g.DrawString(text,font, new SolidBrush(Color.Red),0,0);
            }
            catch (Exception)
            {

            }         

            return image;
        }
    }
}
