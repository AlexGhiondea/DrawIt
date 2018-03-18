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

        public static Drawing LoadDrawing(string fileName)
        {
            if (StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(fileName), ".dit"))
            {
                return LoadObjectFromDisk<Drawing>(fileName);
            }
            //else if (StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(fileName), ".ditx"))
            //{
            //    // this is a compressed version of the 
            //}

            return null;
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
