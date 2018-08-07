using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;

public class ResizePNGForPlatform
{
    private const double HDPI = 2;
    private const double XHDPI = 1.5;

    private const double RETINA = 1.5;

    private const double WVGA = 0.72;
    private const double WXGA = 0.78;

    private const string Android = "Android";
    private const string iOS = "iOS";
    private const string UWP = "UWP";
    private enum Resolutions { HDPI, XHDPI, XXHDPI, RETINA, RETINA_FHD, WVGA, WXGA, HDTV_720p }

    private static MagickImage ReadImageFromFile(string fileName)
    {
        MagickImage image = new MagickImage();
        image.Read(fileName);
        return image;
    }
    private static MagickImage Resize(double coefficient, MagickImage image)
    {
        MagickImage imageCopy = image;
        int heigth = (int)Math.Floor(imageCopy.Height * coefficient);
        int width = (int)Math.Floor(imageCopy.Width * coefficient);
        imageCopy.VirtualPixelMethod = VirtualPixelMethod.Transparent;
        imageCopy.Resize(heigth, width);
        return imageCopy;
    }
    private static void SaveImages(Dictionary<MagickImage, string> dictionary)
    {
        foreach (var element in dictionary)
        {
            element.Key.Write(element.Value);
        }
    }

    private static string GetNameForSave(string filename, Resolutions resolution)
    {
        string filepath = "";
        string pathString = "";
        string folderForSave = filename.Remove(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        string nameOfFile = filename.Remove(0, filename.LastIndexOf(Path.DirectorySeparatorChar));
        switch (resolution)
        {
            case Resolutions.HDPI:
                pathString = Path.Combine(folderForSave, "HDPI");
                filepath = folderForSave + "HDPI" + nameOfFile;
                break;
            case Resolutions.XHDPI:
                pathString = Path.Combine(folderForSave, "XHDPI");
                filepath = folderForSave + "XHDPI" + nameOfFile;
                break;
            case Resolutions.XXHDPI:
                pathString = Path.Combine(folderForSave, "XXHDPI");
                filepath = folderForSave + "XXHDPI" + nameOfFile;
                break;
            case Resolutions.RETINA:
                pathString = Path.Combine(folderForSave, "RETINA");
                filepath = folderForSave + "RETINA" + nameOfFile;
                break;
            case Resolutions.RETINA_FHD:
                pathString = Path.Combine(folderForSave, "RETINA_FHD");
                filepath = folderForSave + "RETINA_FHD" + nameOfFile;
                break;
            case Resolutions.WVGA:
                pathString = Path.Combine(folderForSave, "WVGA");
                filepath = folderForSave + "WVGA" + nameOfFile;
                break;
            case Resolutions.WXGA:
                pathString = Path.Combine(folderForSave, "WXGA");
                filepath = folderForSave + "WXGA" + nameOfFile;
                break;
            case Resolutions.HDTV_720p:
                pathString = Path.Combine(folderForSave, "HDTV_720p");
                filepath = folderForSave + "HDTV_720p" + nameOfFile;
                break;
        }
        Directory.CreateDirectory(pathString);
        return filepath;
    }

    public static void GetImagesProcessAndSave(string filename, string platform)
    {
        MagickImage image = ReadImageFromFile(filename);
        Dictionary<MagickImage, string> resizedImages = new Dictionary<MagickImage, string>();
        switch (platform)
        {
            case Android:
                resizedImages.Add(image, GetNameForSave(filename, Resolutions.XXHDPI));
                resizedImages.Add(Resize(HDPI, image), GetNameForSave(filename, Resolutions.HDPI));
                resizedImages.Add(Resize(XHDPI, image), GetNameForSave(filename, Resolutions.XHDPI));
                break;
            case iOS:
                resizedImages.Add(image, GetNameForSave(filename, Resolutions.RETINA_FHD));
                resizedImages.Add(Resize(RETINA, image), GetNameForSave(filename, Resolutions.RETINA));
                break;
            case UWP:
                resizedImages.Add(image, GetNameForSave(filename, Resolutions.HDTV_720p));
                resizedImages.Add(Resize(WVGA, image), GetNameForSave(filename, Resolutions.WVGA));
                resizedImages.Add(Resize(WXGA, image), GetNameForSave(filename, Resolutions.WXGA));
                break;
            default:
                Console.WriteLine("Invalid platform name");
                Console.ReadLine();
                break;
        }
        SaveImages(resizedImages);
    }
}

