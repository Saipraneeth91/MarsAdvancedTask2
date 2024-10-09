using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.IO;
using MarsAdvancedTask2.Models;

namespace MarsAdvancedTask2.Helpers
{
    public class JSONHelper
    {
        public static T LoadData<T>(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file '{fileName}' was not found in TestData folder.");

            string jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }
    }

}