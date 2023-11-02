using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace WpfUi.Utils;

internal class Transparancy {
    // Utility to 
    public static void MakeFileTransaprent(string pngFilePath, Color targetColor) { 
        try {


            // Load the PNG image
            BitmapImage originalImage = new(new Uri(pngFilePath, UriKind.Absolute));
            FormatConvertedBitmap formatConvertedBitmap = new(originalImage, PixelFormats.Pbgra32, null, 0);

            // Create a WriteableBitmap for processing
            WriteableBitmap writableBitmap = new(formatConvertedBitmap);

            int width = writableBitmap.PixelWidth;
            int height = writableBitmap.PixelHeight;

            int[] pixels = new int[width * height];
            writableBitmap.CopyPixels(pixels, width* 4, 0);

            for (int i = 0; i<pixels.Length; i++) {
                Color pixelColor = Color.FromArgb((byte)((pixels[i] >> 24) & 0xFF),
                    (byte)((pixels[i] >> 16) & 0xFF),
                    (byte)((pixels[i] >> 8) & 0xFF),
                    (byte)(pixels[i] & 0xFF));

                if (AreColorsEqual(pixelColor, targetColor, 20)) {
                    pixels[i] = 0; // Set the color to transparent
                }
            }

            writableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, 0);

            // Save the modified image
            PngBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create(writableBitmap));

            string outputPath = pngFilePath.Replace(".png", "_modified.png");
            using (var stream = System.IO.File.Create(outputPath)) {
                encoder.Save(stream);
            }

            System.Windows.MessageBox.Show("Image color replacement completed. The modified image is saved.");
        } catch (Exception ex) {
            System.Windows.MessageBox.Show("An error occurred: " + ex.Message);
        }
    }

    private static bool AreColorsEqual(Color a, Color b, int tolerance) {
        return Math.Abs(a.R - b.R) <= tolerance &&
        Math.Abs(a.G - b.G) <= tolerance &&
        Math.Abs(a.B - b.B) <= tolerance &&
        Math.Abs(a.A - b.A) <= tolerance;
    }

}

