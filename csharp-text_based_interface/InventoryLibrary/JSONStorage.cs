using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace InventoryLibrary
{
    /// <summary>
    /// Storage manager using JSON (Newtonsoft.Json)
    /// </summary>
    public class JSONStorage
    {
        public Dictionary<string, object> objects { get; set; } = new Dictionary<string, object>();
        private readonly string filePath = Path.Combine("storage", "inventory_manager.json");

        public Dictionary<string, object> All()
        {
            return objects;
        }

        public void New(object obj)
        {
            string key = obj.GetType().Name + "." + obj.GetType().GetProperty("id").GetValue(obj).ToString();
            objects[key] = obj;
        }

        public void Save()
        {
            Directory.CreateDirectory("storage");
            string json = JsonConvert.SerializeObject(objects, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                objects = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
        }
    }
}
