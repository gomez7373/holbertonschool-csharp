using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace image_processor
{
    class ImageProcessor
    {
        private static string[] _filenames;

        /// <summary>
        /// Runs a Color invert process on a list of images.
        /// </summary>
        /// <param name="filenames">List of files</param>
        public static void Inverse(string[] filenames)
        {
            Console.WriteLine(filenames[0]);
            _filenames = filenames;
            Thread.CurrentThread.Name = "Main";
            List<Task> tasks = new List<Task>();
            // for (int i = 0; i < tasks.Length; i++)
            // {
            //     var i1 = i;
            //     tasks[i] = Task.Factory.StartNew(() =>
            //         InvertTask(filenames[i1])
            //     );
            // }

            foreach (var filename in filenames)
            {
                // tasks.Add(Task.Run(() => InvertTask(filename)));
                tasks.Add(Task.Run(() => MatrixInvertTask(filename)));
            }

            Console.WriteLine("Starting Tasks");
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Finished Tasks");
        }


        private static void Invert()
        {
            foreach (var filename in _filenames)
            {
                Console.WriteLine($"Processing: {filename}");
                Image img = Image.FromFile(filename);

                Bitmap bitmap = new Bitmap(img);
                Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

                for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.ToArgb() ^ 0xffffff);
                    newBitmap.SetPixel(x, y, pixel);
                }

                var fn = Path.GetFileNameWithoutExtension(filename);
                var ext = Path.GetExtension(filename);
                var file = $"images/{fn}_inverse.{ext}";
                newBitmap.Save(file);
                // var e = img.GetEncoderParameterList(Guid.Empty);
                // Console.WriteLine($"{e.Param}");
            }
        }

        private static void InvertTask(string filename)
        {
            Console.WriteLine($"Processing: {filename}");
            Image img = Image.FromFile(filename);
            Bitmap bitmap = new Bitmap(img);

            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int x = 0; x < bitmap.Width; x++)
            for (int y = 0; y < bitmap.Height; y++)
            {
                var pixel = bitmap.GetPixel(x, y);
                pixel = Color.FromArgb(pixel.ToArgb() ^ 0xffffff);
                newBitmap.SetPixel(x, y, pixel);
            }

            var fn = Path.GetFileNameWithoutExtension(filename);
            var ext = Path.GetExtension(filename);
            var file = $"images/{fn}_inverse{ext}";
            newBitmap.Save(file, img.RawFormat);
        }

        private static void MatrixInvertTask(string filename)
        {
            Console.WriteLine($"Processing: {filename}");
            Image img = Image.FromFile(filename);
            Bitmap bitmap = new Bitmap(img);

            Graphics g = Graphics.FromImage(bitmap);
            
            ColorMatrix clrMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {-1, 0, 0, 0, 0},
                    new float[] {0, -1, 0, 0, 0},
                    new float[] {0, 0, -1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {1, 1, 1, 0, 1}
                });

            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(clrMatrix);

            g.DrawImage(img,
                new Rectangle(0, 0, img.Width, img.Height),
                0, 0,
                img.Width, img.Height,
                GraphicsUnit.Pixel,
                attributes
            );
            g.Dispose();

            var fn = Path.GetFileNameWithoutExtension(filename);
            var ext = Path.GetExtension(filename);
            bitmap.Save($"./{fn}_inverse{ext}", img.RawFormat);
        }
    }
}
