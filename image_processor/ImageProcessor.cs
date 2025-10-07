using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

/// <summary>
/// The ImageProcessor class provides static methods for performing basic
/// image manipulations such as color inversion, grayscale conversion,
/// black and white thresholding, and thumbnail creation.
/// </summary>
public class ImageProcessor
{
    /// <summary>
    /// Inverts the colors of each image in the given file list.
    /// </summary>
    /// <param name="filenames">An array of image file paths to process.</param>
    public static void Inverse(string[] filenames)
        => ProcessFiles(filenames, (B, G, R, A) => ((byte)(255 - B), (byte)(255 - G), (byte)(255 - R), A), "_inverse");

    /// <summary>
    /// Converts each image in the given file list to grayscale.
    /// </summary>
    /// <param name="filenames">An array of image file paths to process.</param>
    public static void Grayscale(string[] filenames)
        => ProcessFiles(filenames, (B, G, R, A) =>
        {
            // Weighted luminance formula for perceptual grayscale
            byte gray = (byte)Math.Round(0.114 * B + 0.587 * G + 0.299 * R);
            return (gray, gray, gray, A);
        }, "_grayscale");

    /// <summary>
    /// Converts each image to black and white based on a luminance threshold.
    /// </summary>
    /// <param name="filenames">An array of image file paths to process.</param>
    /// <param name="threshold">Luminance threshold (0–255) for black/white conversion.</param>
    public static void BlackWhite(string[] filenames, double threshold)
        => ProcessFiles(filenames, (B, G, R, A) =>
        {
            double luminance = 0.114 * B + 0.587 * G + 0.299 * R;
            byte bw = (byte)(luminance >= threshold ? 255 : 0);
            return (bw, bw, bw, A);
        }, "_bw");

    /// <summary>
    /// Creates a thumbnail for each image, preserving aspect ratio.
    /// </summary>
    /// <param name="filenames">An array of image file paths to process.</param>
    /// <param name="height">Desired thumbnail height in pixels.</param>
    public static void Thumbnail(string[] filenames, int height)
    {
        Parallel.ForEach(filenames, filename =>
        {
            using (var src = new Bitmap(filename))
            {
                // Maintain aspect ratio based on target height
                double ratio = (double)height / src.Height;
                int width = Math.Max(1, (int)Math.Round(src.Width * ratio));

                PixelFormat destFormat = HasAlpha(src.PixelFormat)
                    ? PixelFormat.Format32bppArgb
                    : PixelFormat.Format24bppRgb;

                using (var thumb = new Bitmap(width, height, destFormat))
                {
                    thumb.SetResolution(src.HorizontalResolution, src.VerticalResolution);

                    using (var g = Graphics.FromImage(thumb))
                    {
                        // High quality resizing
                        g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                        using (var wrap = new ImageAttributes())
                        {
                            wrap.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                            g.DrawImage(src, new Rectangle(0, 0, width, height),
                                0, 0, src.Width, src.Height, GraphicsUnit.Pixel, wrap);
                        }
                    }

                    string output = BuildOutputPath(filename, "_th");
                    SaveWithSameFormat(thumb, output, Path.GetExtension(filename));
                }
            }
        });
    }

    // -------------------------------------------------------
    // Below: private helpers (commented normally, no XML tags)
    // -------------------------------------------------------

    // Shared pixel-by-pixel transformation logic using parallel threads.
    private static void ProcessFiles(
        string[] filenames,
        Func<byte, byte, byte, byte, (byte B, byte G, byte R, byte A)> transform,
        string suffix)
    {
        Parallel.ForEach(filenames, filename =>
        {
            using (var original = new Bitmap(filename))
            {
                bool hasAlpha = HasAlpha(original.PixelFormat);
                PixelFormat fmt = hasAlpha ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb;

                using (var bmp = original.Clone(new Rectangle(0, 0, original.Width, original.Height), fmt))
                {
                    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

                    try
                    {
                        int bpp = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
                        int stride = data.Stride;
                        int total = stride * bmp.Height;

                        byte[] buffer = new byte[total];
                        Marshal.Copy(data.Scan0, buffer, 0, total);

                        for (int y = 0; y < bmp.Height; y++)
                        {
                            int row = y * stride;
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                int idx = row + x * bpp;
                                byte B = buffer[idx];
                                byte G = buffer[idx + 1];
                                byte R = buffer[idx + 2];
                                byte A = (bpp == 4) ? buffer[idx + 3] : (byte)255;

                                var t = transform(B, G, R, A);
                                buffer[idx] = t.B;
                                buffer[idx + 1] = t.G;
                                buffer[idx + 2] = t.R;
                                if (bpp == 4) buffer[idx + 3] = t.A;
                            }
                        }

                        Marshal.Copy(buffer, 0, data.Scan0, total);
                    }
                    finally
                    {
                        bmp.UnlockBits(data);
                    }

                    string outPath = BuildOutputPath(filename, suffix);
                    SaveWithSameFormat(bmp, outPath, Path.GetExtension(filename));
                }
            }
        });
    }

    // Builds a proper filename for processed images.
    private static string BuildOutputPath(string originalPath, string suffix)
    {
        string dir = Directory.GetCurrentDirectory();
        string name = Path.GetFileNameWithoutExtension(originalPath);
        string ext = Path.GetExtension(originalPath);
        return Path.Combine(dir, $"{name}{suffix}{ext}");
    }

    // Saves image with the same format as the original.
    private static void SaveWithSameFormat(Image img, string outPath, string ext)
    {
        string e = ext.ToLowerInvariant();
        ImageFormat fmt = ImageFormat.Jpeg;

        if (e == ".png") fmt = ImageFormat.Png;
        else if (e == ".bmp") fmt = ImageFormat.Bmp;
        else if (e == ".gif") fmt = ImageFormat.Gif;
        else if (e == ".tif" || e == ".tiff") fmt = ImageFormat.Tiff;

        img.Save(outPath, fmt);
    }

    // Determines whether an image supports an alpha channel.
    private static bool HasAlpha(PixelFormat pf)
    {
        return (pf & PixelFormat.Alpha) == PixelFormat.Alpha ||
               (pf & PixelFormat.PAlpha) == PixelFormat.PAlpha ||
               pf == PixelFormat.Format32bppArgb ||
               pf == PixelFormat.Format32bppPArgb ||
               pf == PixelFormat.Format64bppArgb ||
               pf == PixelFormat.Format64bppPArgb;
    }
}

