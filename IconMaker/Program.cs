// ***********************************************************************
// Assembly         : IconMaker
// Author           : MattEgen
// Created          : 02-09-2024
//
// Last Modified By : MattEgen
// Last Modified On : 02-11-2024
// ***********************************************************************
// <copyright file="Program.cs" company="Matt Egen">
//     Copyright (c) . All rights reserved.
// </copyright>
//<license>
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
//</license>
// <summary></summary>
// ***********************************************************************
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
            // get the PNG file data
            byte[] pngData = File.ReadAllBytes(pngFilePath);

            // write the ICO file
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
