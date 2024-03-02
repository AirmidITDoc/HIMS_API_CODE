using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace HIMS.Api.Models.Login
{
    public static class Captcha
    {
        private const string Letters = "0123456789";
        public static string GenerateCaptchaCode()
        {
            Random rand = new();
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new();

            for (int i = 0; i < 4; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }
        public static CaptchaResult GenerateCaptchaImage(int width, int height, string captchaCode)
        {
            using Bitmap baseMap = new(width, height);
            using Graphics graph = Graphics.FromImage(baseMap);
            Random rand = new();

            graph.Clear(GetRandomLightColor(rand));

            DrawCaptchaCode(rand, height, width, captchaCode, graph);

            DrawDisorderLine(rand, height, width, graph);

            AdjustRippleEffect(baseMap);

            MemoryStream ms = new();

            baseMap.Save(ms, ImageFormat.Png);
            return new CaptchaResult { CaptchaCode = captchaCode, CaptchaByteData = ms.ToArray(), Timestamp = DateTime.Now };
        }
        private static Color GetRandomLightColor(Random rand)
        {
            const int low = 180, high = 255;

            int nRend = (rand.Next(high) % (high - low)) + low;
            int nGreen = (rand.Next(high) % (high - low)) + low;
            int nBlue = (rand.Next(high) % (high - low)) + low;

            return Color.FromArgb(nRend, nGreen, nBlue);
        }
        private static void DrawCaptchaCode(Random rand, int height, int width, string captchaCode, Graphics graph)
        {
            SolidBrush fontBrush = new(Color.Black);
            int fontSize = GetFontSize(width, captchaCode.Length);
            Font font = new(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            for (int i = 0; i < captchaCode.Length; i++)
            {
                fontBrush.Color = GetRandomDeepColor(rand);

                int shiftPx = fontSize / 6;

                float x = (i * fontSize) + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
                int maxY = height - fontSize;
                if (maxY < 0)
                {
                    maxY = 0;
                }

                float y = rand.Next(0, maxY);

                graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
            }
        }
        private static int GetFontSize(int imageWidth, int captchCodeCount)
        {
            int averageSize = imageWidth / captchCodeCount;

            return Convert.ToInt32(averageSize);
        }
        private static Color GetRandomDeepColor(Random rand)
        {
            const int redlow = 160, greenLow = 100, blueLow = 160;
            return Color.FromArgb(rand.Next(redlow), rand.Next(greenLow), rand.Next(blueLow));
        }
        private static void DrawDisorderLine(Random rand, int height, int width, Graphics graph)
        {
            Pen linePen = new(new SolidBrush(Color.Black), 3);
            for (int i = 0; i < rand.Next(3, 5); i++)
            {
                linePen.Color = GetRandomDeepColor(rand);

                Point startPoint = new(rand.Next(0, width), rand.Next(0, height));
                Point endPoint = new(rand.Next(0, width), rand.Next(0, height));
                graph.DrawLine(linePen, startPoint, endPoint);

                Point bezierPoint1 = new(rand.Next(0, width), rand.Next(0, height));
                Point bezierPoint2 = new(rand.Next(0, width), rand.Next(0, height));

                graph.DrawBezier(linePen, startPoint, bezierPoint1, bezierPoint2, endPoint);
            }
        }
        private static void AdjustRippleEffect(Bitmap baseMap)
        {
            const short nWave = 6;
            int nWidth = baseMap.Width;
            int nHeight = baseMap.Height;

            Point[,] pt = new Point[nWidth, nHeight];

            double newX, newY;
            double xo, yo;

            for (int x = 0; x < nWidth; ++x)
            {
                for (int y = 0; y < nHeight; ++y)
                {
                    xo = (nWave * Math.Sin(2.0 * 3.1415 * (float)y / 128.0));
                    yo = (nWave * Math.Cos(2.0 * 3.1415 * (float)x / 128.0));

                    newX = (x + xo);
                    newY = (y + yo);

                    if (newX > 0 && newX < nWidth)
                    {
                        pt[x, y].X = (int)newX;
                    }
                    else
                    {
                        pt[x, y].X = 0;
                    }

                    if (newY > 0 && newY < nHeight)
                    {
                        pt[x, y].Y = (int)newY;
                    }
                    else
                    {
                        pt[x, y].Y = 0;
                    }
                }
            }

            Bitmap bSrc = (Bitmap)baseMap.Clone();

            BitmapData bitmapData = baseMap.LockBits(new Rectangle(0, 0, baseMap.Width, baseMap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            baseMap.UnlockBits(bitmapData);
            bSrc.UnlockBits(bmSrc);
            bSrc.Dispose();
        }
    }
}
