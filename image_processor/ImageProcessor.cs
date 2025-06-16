using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Provides static methods for processing images.
/// </summary>
public class ImageProcessor
{
    /// <summary>
    /// Inverts the colors of each image provided in the filenames array.
    /// The new image will be saved in the same format and named using the pattern: originalname_inverse.ext
    /// </summary>
    /// <param name="filenames">Array of file paths to process.</param>
    public static void Inverse(string[] filenames)
    {
        // Process each file in parallel for better performance
        Parallel.ForEach(filenames, file =>
        {
            try
            {
                using (Bitmap image = new Bitmap(file))
                {
                    // Loop through each pixel in the image
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            // Get original pixel color
                            Color original = image.GetPixel(x, y);

                            // Invert color channels
                            Color inverted = Color.FromArgb(
                                original.A,
                                255 - original.R,
                                255 - original.G,
                                255 - original.B
                            );

                            // Set the new color
                            image.SetPixel(x, y, inverted);
                        }
                    }

                    // Build the new file name
                    string filename = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);
                    string outputPath = Path.Combine(Directory.GetCurrentDirectory(), $"{filename}_inverse{extension}");

                    // Save the image to the project root
                    image.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Converts each image provided in the filenames array to grayscale.
    /// The new image will be saved with '_grayscale' in the name.
    /// </summary>
    /// <param name="filenames">Array of file paths to process.</param>
    public static void Grayscale(string[] filenames)
    {
        // Process each file in parallel for performance
        Parallel.ForEach(filenames, file =>
        {
            try
            {
                using (Bitmap image = new Bitmap(file))
                {
                    // Loop through each pixel
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            Color original = image.GetPixel(x, y);

                            // Calculate grayscale value using luminance formula
                            int gray = (int)(0.299 * original.R + 0.587 * original.G + 0.114 * original.B);

                            Color grayscaleColor = Color.FromArgb(original.A, gray, gray, gray);
                            image.SetPixel(x, y, grayscaleColor);
                        }
                    }

                    string filename = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);
                    string outputPath = Path.Combine(Directory.GetCurrentDirectory(), $"{filename}_grayscale{extension}");

                    image.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        });
    }
}
