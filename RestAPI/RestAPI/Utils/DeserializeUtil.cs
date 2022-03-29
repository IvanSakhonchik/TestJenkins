using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RestAPI
{
    public static class DeserializeUtil
    {
        private const string TEST_DATA_PATH = @"Data/testdata.json";

        public static T GetData<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public static string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);

        public static Dictionary<string, string> GetTestData()
        {
            var text = File.ReadAllText(TEST_DATA_PATH);
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
            return data;
        }
    }
}
