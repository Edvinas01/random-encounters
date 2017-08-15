using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Drawing;

namespace RandomEncounters
{
    public class ImageProvider
    {
        private const string ImageSuffix = ".png";

        private readonly List<Image> _images;
        private readonly Random _random;

        public ImageProvider(Random random)
        {
            _random = random;
            _images = LoadImages();
        }

        public Image GetImage()
        {
            return _images[_random.Next(_images.Count)];
        }

        private static List<Image> LoadImages()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var loaded = assembly
                .GetManifestResourceNames()
                .Where(r => r.EndsWith(ImageSuffix))
                .Select(r => assembly.GetManifestResourceStream(r))
                .Where(s => s != null)
                .Select(Image.FromStream)
                .ToList();

            if (!loaded.Any())
            {
                throw new InvalidOperationException("No valid resource images could be found");
            }
            return loaded;
        }
    }
}