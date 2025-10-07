using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

/// <summary>
/// The ImageProcessor class provides static methods to perform
/// color inversion, grayscale conversion, black & white filtering,
/// and thumbnail generation on images using efficient pixel operations.
/// </summary>
public class ImageProcessor
{
    /// <summary>
    /// Inverts the colors of all images listed in filenames.
    /// </summary>
    /// <param name="filenames">Array of image file paths to process.</param>
    public static void Inverse(string[] filenames)
    {
        ProcessImages(filenames, (b, g, r) => (255 - b, 255 - g, 255 - r), "_inverse");
    }

    /// <summary>
    /// Converts all images listed in filenames to grayscale.
    /// </summary>
    /// <param name="filenames">Array of image file paths to process.</param>
    public static void Grayscale(string[] filenames)
    {
        ProcessImages(filenames, (b, g, r) =>
        {
            byte gray = (byte)(0.114 * b + 0.587 * g + 0.299 * r);
            return (gray, gray, gray);
        }, "_grayscale");
    }

    /// <summary>
    /// Converts all images listed in filenames to pure black or white
    /// according to a luminance threshold.
    /// </summary>
    /// <param name="filenames">Array of image file paths to process.</param>
    /// <param name="threshold">Luminance threshold (0–255).</param>
    public static void BlackWhite(string[] filenames, double threshold)
    {
        ProcessImages(filenames, (b, g, r) =>
        {
            double lum = 0.114 * b + 0.587 * g + 0.299 * r;
            byte bw = (byte)(lum >= threshold ? 255 : 0);
            return (bw, bw, bw);
        }, "_bw");
    }

    /// <summary>
    /// Generates a thumbnail for each image in filenames, preserving aspect ratio.
    /// </summary>
    /// <param name="filenames">Array of image file paths to process.</param>
    /// <param name="height">Desired thumbnail height in pixels.</param>
    public static void Thumbnail(string[] filenames, int height)
    {
        Parallel.ForEach(filenames, filename =>
        {
            using (Bitmap img = new Bitmap(filename))
            {
                double ratio = (double)height / img.Height;
                int width = (int)(img.Width * ratio);

                using (Bitmap thumb = new Bitmap(img, new Size(width, height)))
                {
                    string name = Path.GetFileNameWithoutExtension(filename);
                    string ext = Path.GetExtension(filename);
                    string newPath = Path.Combine(Directory.GetCurrentDirectory(), $"{name}_th{ext}");
                    thumb.Save(newPath);
                }
            }
        });
    }

    // -----------------------------------------------------------------
    // PRIVATE METHOD: Efficient pixel manipulation using LockBits
    // -----------------------------------------------------------------
    private static void ProcessImages(string[] filenames, Func<byte, byte, byte, (byte, byte, byte)> transform, string suffix)
    {
        Parallel.ForEach(filenames, filename =>
        {
            using (Bitmap bmp = new Bitmap(filename))
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

                int bpp = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int byteCount = data.Stride * bmp.Height;
                byte[] pixels = new byte[byteCount];
                Marshal.Copy(data.Scan0, pixels, 0, byteCount);

                for (int y = 0; y < bmp.Height; y++)
                {
                    int row = y * data.Stride;
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        int i = row + x * bpp;
                        var (b, g, r) = transform(pixels[i], pixels[i + 1], pixels[i + 2]);
                        pixels[i] = b;
                        pixels[i + 1] = g;
                        pixels[i + 2] = r;
                    }
                }

                Marshal.Copy(pixels, 0, data.Scan0, byteCount);
                bmp.UnlockBits(data);

                string name = Path.GetFileNameWithoutExtension(filename);
                string ext = Path.GetExtension(filename);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), $"{name}{suffix}{ext}");
                bmp.Save(newPath);
            }
        });
    }
}

