using System;

namespace ResizeImageForPlatforms
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Enter a path to file");
                string path = Console.ReadLine();
                Console.WriteLine("Enter a platform: Android or iOS or UWP");
                string platform = Console.ReadLine();
                ResizePNGForPlatform.GetImagesProcessAndSave(path, platform);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
