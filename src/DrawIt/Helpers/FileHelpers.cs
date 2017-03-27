using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawIt
{
    public static class FileHelpers
    {
        public static void SaveObjectToDisk<T>(string fileName, T data)
        {
            var s = JsonSerializer.CreateDefault();
            s.TypeNameHandling = TypeNameHandling.All;
            using (StreamWriter sw = new StreamWriter(fileName))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                s.Serialize(jw, data);
            }
        }


        public static T LoadObjectFromDisk<T>(string fileName)
        {
            var s = JsonSerializer.CreateDefault();
            s.TypeNameHandling = TypeNameHandling.All;
            using (StreamReader sw = new StreamReader(fileName))
            using (JsonReader jr = new JsonTextReader(sw))
            {
                return s.Deserialize<T>(jr);
            }
        }
    }
}
