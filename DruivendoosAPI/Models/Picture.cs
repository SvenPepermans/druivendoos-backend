using System;
using System.IO;

namespace DruivendoosAPI.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Base64 { get; set; }

        public Picture(string name)
        {
            var extension = name.Split(".");
            Url = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images")), name);
            Base64 = "data:image/" + extension[extension.Length - 1] + ";base64, " + Convert.ToBase64String(File.ReadAllBytes(Url));

            Name = name;
        }

        public Picture() { }
    }
}
