using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// The ImageProcessor class provides static methods to perform
/// color inversion, grayscale conversion, black & white filtering,
/// and thumbnail creation on images.
/// </summary>
public class ImageProcessor
{
    /// <summary>
    /// Inverts the colors of all images listed in filenames.
    /// </summary>
    /// <param name="filenames">Array of image file paths to process.</param>
    public static void Inverse(string[] filenames)
    {
        Parallel.ForEach(filenames, filename =>
        {
            using (Bitmap img = new Bitmap(filename))
            {
                int width = img.Width;
                int height = img.Height;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = img.GetPixel(x, y);
                        Color inverted = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                        img.SetPixel(x, y, inverted);
                    }
                }

                string name = Path.GetFileNameWithoutExtension(filename);
                string ext = Path.GetExtension(filename);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), $"{name}_inverse{ext}");
                img.Save(newPath);
            }
        });
    }

    /// <summary>
    /// Converts all images listed in filenames to grayscale.
    /// </summary>
    /// <param name="filenames">Array of image file paths to process.</param>
    public static void Grayscale(string[] filenames)
    {
        Parallel.ForEach(filenames, filename =>
        {
            using (Bitmap img = new Bitmap(filename))
            {
                int width = img.Width;
                int height = img.Height;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = img.GetPixel(x, y);
                        int gray = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                        Color newColor = Color.FromArgb(gray, gray, gray);
                        img.SetPixel(x, y, newColor);
                    }
                }

                string name = Path.GetFileNameWithoutExtension(filename);
                string ext = Path.GetExtension(filename);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), $"{name}_grayscale{ext}");
                img.Save(newPath);
            }
        });
    }

    /// <summary>
    /// Converts all images listed in filenames to pure black or white
    /// according to a luminance threshold.
    /// </summary>
    /// <param name="filenames">Array of image file paths to process.</param>
    /// <param name="threshold">Luminance threshold (0–255).</param>
    public static void BlackWhite(string[] filenames, double threshold)
    {
        Parallel.ForEach(filenames, filename =>
        {
            using (Bitmap img = new Bitmap(filename))
            {
                int width = img.Width;
                int height = img.Height;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = img.GetPixel(x, y);
                        double lum = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
                        Color newColor = lum >= threshold ? Color.White : Color.Black;
                        img.SetPixel(x, y, newColor);
                    }
                }

                string name = Path.GetFileNameWithoutExtension(filename);
                string ext = Path.GetExtension(filename);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), $"{name}_bw{ext}");
                img.Save(newPath);
            }
        });
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
}

