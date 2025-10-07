using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

class ImageProcessor
{
    public static void Inverse(string[] filenames)
    {
        AsyncFunction(filenames);
    }
    private static async Task AsyncFunction(string[] filenames)
    {
        await Task.WhenAll(Array.ConvertAll(filenames, async file => await ProcessImageThread(file)));
    }
    private static async Task ProcessImageThread(string file_name)
    {
        Bitmap bitmap = new Bitmap(file_name);

        BitmapData lockedimage = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
        int img_size = lockedimage.Stride * lockedimage.Height;
        byte[] image_copy = new byte[img_size];

        System.Runtime.InteropServices.Marshal.Copy(lockedimage.Scan0, image_copy, 0, img_size);

        for (int i = 0; i < img_size; i++)
            image_copy[i] = (byte)(255 - image_copy[i]);

        System.Runtime.InteropServices.Marshal.Copy(image_copy, 0, lockedimage.Scan0, img_size);
        bitmap.UnlockBits(lockedimage);

        string[] slip = file_name.Split(new char[] { '/', '.' });
        bitmap.Save(slip[slip.Length - 2] + "_inverse." + slip[slip.Length - 1]);

    }
    public static void Grayscale(string[] filenames)
    {
        foreach (var file_name in filenames)
        {
            Bitmap bitmap = new Bitmap(file_name);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    bitmap.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }

            string[] slip = file_name.Split(new char[] { '/', '.' });
            bitmap.Save(slip[slip.Length - 2] + "_grayscale." + slip[slip.Length - 1]);
        }
    }
    public static void BlackWhite(string[] filenames, double threshold)
    {
        Parallel.ForEach(filenames, file_name =>
        {
            Bitmap bitmap = new Bitmap(file_name);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    double grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    if (grey > threshold)
                        bitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    else
                        bitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                }
            }

            string[] slip = file_name.Split(new char[] { '/', '.' });
            bitmap.Save(slip[slip.Length - 2] + "_bw." + slip[slip.Length - 1]);
        });
    }
}
