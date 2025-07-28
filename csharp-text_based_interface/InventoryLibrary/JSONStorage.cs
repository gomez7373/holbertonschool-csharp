using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace InventoryLibrary
{
    /// <summary>Handles JSON storage for all objects.</summary>
    public class JSONStorage
    {
        /// <summary>Dictionary of stored objects (key: ClassName.id, value: object).</summary>
        public Dictionary<string, object> objects { get; set; } = new Dictionary<string, object>();
        private readonly string filePath = Path.Combine("storage", "inventory_manager.json");

        /// <summary>Returns all stored objects.</summary>
        public Dictionary<string, object> All() => objects;

        /// <summary>Adds a new object to storage.</summary>
        public void New(object obj)
        {
            string key = $"{obj.GetType().Name}.{obj.GetType().GetProperty("id")?.GetValue(obj)}";
            objects[key] = obj;
        }

        /// <summary>Saves objects to a JSON file.</summary>
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(objects, options));
        }

        /// <summary>Loads objects from the JSON file.</summary>
        public void Load()
        {
            if (!File.Exists(filePath)) return;
            string json = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(json))
                objects = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
        }
    }
}
