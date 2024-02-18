using System.IO;

namespace IconMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                System.Console.WriteLine("Usage: IconMaker <input-png-file> <output-ico-file>");
                return;
            }
            IconCreator iconCreator = new IconCreator();
            iconCreator.CreateIcon(args[1], args[0]);
        }
    }


public class IconCreator
    {
        public void CreateIcon(string filePath, string pngFilePath)
        {
            byte[] pngData = File.ReadAllBytes(pngFilePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Write ICONDIR structure
                    writer.Write((short)0); // Reserved. Must always be 0.
                    writer.Write((short)1); // Image type: 1 for icon (.ICO)
                    writer.Write((short)1); // Number of images in the file.

                    // Write ICONDIRENTRY structure
                    writer.Write((byte)32); // Width, in pixels, of the image
                    writer.Write((byte)32); // Height, in pixels, of the image.
                    writer.Write((byte)0); // Number of colors in the image (0 if >=8bpp)
                    writer.Write((byte)0); // Reserved. Should be 0.
                    writer.Write((short)1); // Color planes.
                    writer.Write((short)32); // Bits per pixel.
                    writer.Write(pngData.Length); // Size of the image data.
                    writer.Write(22); // Offset of image data from the beginning of the file.

                    // Write the image data
                    writer.Write(pngData);
                }
            }
        }
    }

}
