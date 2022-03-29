using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RestAPI
{
    public static class FileUtil
    {

        public static void WriteFile(string path, string file)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.WriteLine(file);
            }
        }

        public static void DeleteFile(string path) => new FileInfo(path).Delete();
    }
}
