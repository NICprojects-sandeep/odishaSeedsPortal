<%@ WebHandler Language="C#" Class="CaptchaHandler" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.SessionState;

//namespace CaptchaControl
//{

    public class CaptchaHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int iHeight = 70;
            int iWidth = 180;
            Random oRandom = new Random();

            int[] aBackgroundNoiseColor = new int[] { 150, 150, 150 };
            int[] aTextColor = new int[] { 0, 0, 0 };
            int[] aFontEmSizes = new int[] { 20, 25, 30 };

            string[] aFontNames = new string[] {"Comic Sans MS", "Arial", "Times New Roman", "Georgia", "Verdana"};
            //string[] aFontNames = new string[] { "Comic Sans MS" };
            //FontStyle[] aFontStyles = new FontStyle[] {FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Strikeout, FontStyle.Underline};
            FontStyle[] aFontStyles = new FontStyle[] { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular };
            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal,
                HatchStyle.DashedVertical, HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond,
                HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.HorizontalBrick, HatchStyle.LargeConfetti,
                HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal, HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
                HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal, HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
                HatchStyle.Shingle, HatchStyle.SmallConfetti, HatchStyle.Trellis, HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
                HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
            };
            // GET CAPTCHA IN SESSION
            string sCaptchaText = context.Session["Captcha"].ToString();

            // CREATES AN OUTPUT BITMAP
            Bitmap oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
            Graphics oGraphics = Graphics.FromImage(oOutputBitmap);
            oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            // CREATES A DRAWING AREA
            RectangleF oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
            Brush oBrush = default(Brush);

            // DRAW BACKGROUND (Lighter colors RGB 100 to 255)
            oBrush = new HatchBrush(aHatchStyles[oRandom.Next
                (aHatchStyles.Length - 1)], Color.FromArgb((oRandom.Next(100, 255)),
                (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
            oGraphics.FillRectangle(oBrush, oRectangleF);

            System.Drawing.Drawing2D.Matrix oMatrix = new System.Drawing.Drawing2D.Matrix();
            int i = 0;
            for (i = 0; i <= sCaptchaText.Length - 1; i++)
            {
                oMatrix.Reset();
                int iChars = sCaptchaText.Length;
                int x = iWidth / (iChars + 1) * i;
                int y = iHeight / 2;

                // ROTATE TEXT RANDOM
                oMatrix.RotateAt(oRandom.Next(-20, 20), new PointF(x, y));
                oGraphics.Transform = oMatrix;

                // DRAW LETTERS WITH RANDOM FONT TYPE, SIZE AND COLOR
                oGraphics.DrawString
                (
                    //Text
                sCaptchaText.Substring(i, 1),
                    //Random Font Name and Style
                new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)],
                   aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
                   aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
                    //Random Color (Darker colors RGB 0 to 100)
                new SolidBrush(Color.FromArgb(oRandom.Next(0, 100),
                   oRandom.Next(0, 100), oRandom.Next(0, 100))),
                x,
                oRandom.Next(10, 40)
                );
                oGraphics.ResetTransform();
            }
            MemoryStream oMemoryStream = new MemoryStream();
            oOutputBitmap.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] oBytes = oMemoryStream.GetBuffer();

            oOutputBitmap.Dispose();
            oMemoryStream.Close();

            context.Response.BinaryWrite(oBytes);
            context.Response.End();
            
            
            //
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }

//}