using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Web.DataLayer.Util
{
    public class CreateHelper<T>
    {
        private readonly string _path = @"c:\temp\";

        /// <summary>
        /// Create a directory name temp
        /// see global variable _path
        /// </summary>
        public async Task CreateFolderAsync()
        {
            await Task.Run((() =>
            {
                DirectoryInfo di = Directory.CreateDirectory(_path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }));
        }

        /// <summary>
        /// delete file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task DeleteFileAsync(string fileName)
        {
            await Task.Run(() =>
            {
                if (File.Exists(JsonPath(_path, fileName)))
                {
                    File.Delete(JsonPath(_path, fileName));
                }
            });
        }

        /// <summary>
        /// write json file
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileName"></param>
        public async Task WriteJsonAsync(T model, string fileName)
        {
            await Task.Run(() =>
            {
                using (FileStream fs = File.Open(JsonPath(_path, fileName), FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;

                    JsonSerializer serializer = new JsonSerializer();

                    serializer.Serialize(jw, model);
                }
            });
        }

        /// <summary>
        /// read json file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T ReadJson(string fileName)
        {
            if (File.Exists(JsonPath(_path, fileName)))
            {
                string jsonString = File.ReadAllText(JsonPath(_path, fileName));
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return default(T);
        }

        private static string JsonPath(string path, string fileName)
        {
            return string.Format("{0}{1}.json", path, fileName);
        }
    }
}
