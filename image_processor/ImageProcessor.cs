using System;
using System.Drawing; // Para trabajar con imágenes
using System.IO;
using System.Threading.Tasks; // Para usar multihilos modernos (Task)

public class ImageProcessor
{
    /// <summary>
    /// Inverts the colors of each image in the filenames array.
    /// </summary>
    public static void Inverse(string[] filenames)
    {
        Parallel.ForEach(filenames, file =>
        {
            try
            {
                using (Bitmap image = new Bitmap(file))
                {
                    // Recorre cada píxel
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            Color original = image.GetPixel(x, y);

                            // Invertir cada canal de color (255 - valor)
                            Color inverted = Color.FromArgb(
                                original.A,
                                255 - original.R,
                                255 - original.G,
                                255 - original.B
                            );

                            image.SetPixel(x, y, inverted);
                        }
                    }

                    // Crear el nuevo nombre del archivo
                    string directory = Path.GetDirectoryName(file);
                    string filename = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);

                    string newPath = Path.Combine(directory ?? "", $"{filename}_inverse{extension}");

                    image.Save(newPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        });
    }
}