using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TIG
{
    public static class GenerateImages
    {
        private const TextFormatFlags TightFormatFlags =
            TextFormatFlags.NoPadding
            | TextFormatFlags.NoClipping
            | TextFormatFlags.SingleLine
            | TextFormatFlags.Top
            | TextFormatFlags.Left;
        public static void Run(string fontName, string outputPath, int targetHeight)
        {
            if (Directory.Exists(outputPath) == false)
            {
                Directory.CreateDirectory(outputPath);
            }

            Font originalFont = new Font(fontName, 32);
            int finalSize = CalculateFontSize(originalFont, targetHeight);
            Font actualFont = new Font(fontName, finalSize);
            int sizeDifference = (finalSize - targetHeight) >> 1; // mystery divide by two
            foreach (KeyValuePair<string, string> pair in GardinerToUnicode.Map)
            {
                string name = pair.Key;
                if(name == "?")
                {
                    continue;
                }

                if(name.StartsWith("AA"))
                {
                    name = name.Replace("AA", "J");
                }

                Image image = GenerateTightImage(pair.Value, actualFont, targetHeight, sizeDifference);
                image.Save(Path.Combine(outputPath, name + ".png"));
                image.Dispose();
            }
        }

        private static Image GenerateTightImage(string glyph, Font font, int targetHeight, int sizeDifference)
        {
            Bitmap image = new Bitmap(2 * targetHeight, targetHeight, PixelFormat.Format32bppArgb);
            TextRenderer.DrawText(
                Graphics.FromImage(image), glyph, font, new Point(0, sizeDifference), Color.Black, TightFormatFlags);
            Rectangle tightBounds = FindEdges(image);
            Image tightImage = new Bitmap(tightBounds.Width, tightBounds.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(tightImage);
            g.DrawImage(image, new Rectangle(new Point(0, 0), tightBounds.Size), tightBounds, GraphicsUnit.Pixel);
            image.Dispose();
            return tightImage;
        }

        private static Rectangle FindEdges(Bitmap image)
        {
            Rectangle area = new Rectangle(new Point(0, 0), new Size(image.Width, image.Height));
            // find the non-transparent pixels with min/max x/y
            int minX = image.Width;
            int maxX = 0;
            int minY = image.Height;
            int maxY = 0;
            for(int j = 0; j < image.Height; ++j)
            {
                for(int i = 0; i < image.Width; ++i)
                {
                    if(image.GetPixel(i, j).A == 0)
                    {
                        continue;
                    }

                    if(i > maxX)
                    {
                        maxX = i;
                    }

                    if(i < minX)
                    {
                        minX = i;
                    }

                    if(j > maxY)
                    {
                        maxY = j;
                    }

                    if(j < minY)
                    {
                        minY = j;
                    }
                }
            }

            area.X = minX;
            area.Width = maxX - minX;
            area.Y = minY;
            area.Height = maxY - minY;
            return area;
        }

        private static int CalculateFontSize(Font originalFont, int targetSize)
        {
            FontFamily family = originalFont.FontFamily;
            int descent = family.GetCellDescent(originalFont.Style);
            int emHeight = family.GetEmHeight(originalFont.Style);
            float desiredHeightRatio = (float)emHeight / (emHeight - descent);
            return (int)(targetSize * desiredHeightRatio * 0.5f);
        }
    }
}
