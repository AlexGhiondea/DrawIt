using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DrawIt
{
    public static class FileHelpers
    {
        public static void SaveObjectToDisk<T>(string fileName, T data)
        {
            var s = JsonSerializer.CreateDefault();
            s.TypeNameHandling = TypeNameHandling.Auto;
            using (StreamWriter sw = new StreamWriter(fileName))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                s.Serialize(jw, data);
            }
        }

        public static T LoadObjectFromDisk<T>(string fileName)
        {
            var s = JsonSerializer.CreateDefault();
            s.TypeNameHandling = TypeNameHandling.Auto;
            using (StreamReader sw = new StreamReader(fileName))
            using (JsonReader jr = new JsonTextReader(sw))
            {
                return s.Deserialize<T>(jr);
            }
        }

        public static void SaveDrawing(Drawing drawing, string fileName)
        {
            // see if we need to trim some of the data in the Images object.

            HashSet<string> reachableObjects = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var item in drawing.Shapes)
            {
                if (item is Image)
                {
                    reachableObjects.Add((item as Image).EncodedImage);
                }
            }

            // remove from the dictionary all non-reachable image blobs
            List<string> toRemove = new List<string>();
            foreach (var item in drawing.Images.Keys)
            {
                if (!reachableObjects.Contains(item))
                {
                    toRemove.Add(item);
                }
            }

            foreach (var key in toRemove)
            {
                drawing.Images.Remove(key);
            }

            SaveObjectToDisk(fileName, drawing);
        }

        public static Drawing LoadDrawing(string fileName)
        {
            Drawing newDrawing = LoadObjectFromDisk<Drawing>(fileName);

            // hook-up the image with the drawing object.
            var imagesInDrawing = newDrawing.Shapes.OfType<Image>();
            foreach (var item in imagesInDrawing)
            {
                item.SetImageResolver(newDrawing.ResolveImage);
            }

            // if we don't have an images object in the serialized object
            // and we have at least on image.
            if (newDrawing.Images == null && newDrawing.Shapes.OfType<Image>().Any())
            {
                foreach (var item in imagesInDrawing)
                {
                    newDrawing.Shapes.Remove(item);
                    newDrawing.AddImage(item);
                }
            }

            return newDrawing;
        }

        public static string ComputeSHA2Hash(string text)
        {
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                return GetHashAsString(hash);
            }
        }

        private static string GetHashAsString(byte[] hash)
        {
            StringBuilder sb = new StringBuilder(hash.Length / 2);
            for (int i = 0; i < hash.Length; i++)
            {
                sb.AppendFormat("{0:x2}", hash[i]);
            }
            return sb.ToString();
        }
    }
}
