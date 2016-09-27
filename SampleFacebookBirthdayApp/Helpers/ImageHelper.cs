using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace SampleFacebookBirthdayApp.Helpers
{
    public static class ImageHelper
    {
        public static Image RoundCorners(Image StartImage, int CornerRadius,Color borderColor,int borderThickness)
        {
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(StartImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            g.FillPath(brush, gp);
            g.DrawPath(new Pen(borderColor, borderThickness), gp);
            return RoundedImage;
        }
        public static Bitmap DrawRoundedRectangle(Image im, Int32 Radius)
        {
            Bitmap Bmp = new Bitmap(im, im.Width, im.Height);
            Graphics G = Graphics.FromImage(Bmp);
            Brush brush = new System.Drawing.SolidBrush(Color.Red);

            for (int i = 0; i < 4; i++)
            {
                Point[] CornerUpLeft = new Point[3];

                CornerUpLeft[0].X = 0;
                CornerUpLeft[0].Y = 0;

                CornerUpLeft[1].X = Radius;
                CornerUpLeft[1].Y = 0;

                CornerUpLeft[2].X = 0;
                CornerUpLeft[2].Y = Radius;

                System.Drawing.Drawing2D.GraphicsPath pathCornerUpLeft = new System.Drawing.Drawing2D.GraphicsPath();

                pathCornerUpLeft.AddArc(CornerUpLeft[0].X, CornerUpLeft[0].Y,
                    Radius, Radius, 180, 90);
                pathCornerUpLeft.AddLine(CornerUpLeft[0].X, CornerUpLeft[0].Y,
                    CornerUpLeft[1].X, CornerUpLeft[1].Y);
                pathCornerUpLeft.AddLine(CornerUpLeft[0].X, CornerUpLeft[0].Y,
                    CornerUpLeft[2].X, CornerUpLeft[2].Y);

                G.FillPath(brush, pathCornerUpLeft);
                pathCornerUpLeft.Dispose();

                Bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            brush.Dispose();
            G.Dispose();

            Color backColor = Bmp.GetPixel(0, 0);

            Bmp.MakeTransparent(backColor);

            return Bmp;

        }
        public static Stream GetStreamFromUrl(string url)
        {
            byte[] imageData = null;

            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData(url);

            return new MemoryStream(imageData);
        }
    }
}