using Lab.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Lab.Utils
{
    public static class Util
    {
        static string path = @".\LabTest\plate_test.json";
        public static void ToJsonFile(List<Well> wells)
        {
            string json = JsonConvert.SerializeObject(wells.ToArray());


            if (!File.Exists(path))
            {
                CreateFileAndFolders(json);
            }
            else if (File.Exists(path))
            {
                File.WriteAllText(path, json);
            }
        }

        private static void CreateFileAndFolders(string json)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            File.Create(path).Dispose();
            File.WriteAllText(path, json);
        }
    }
}
