using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using FabricWars.Utils.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FabricWars.Utils.Serialization.DataStore
{
    public class JsonDataStore : DataStore
    {
        public JsonDataStore(DirectoryInfo path) : base(path) {}
        public JsonDataStore(string path) : base(path) { }
    
        protected override void Initialize()
        {
            foreach (var file in path.GetFiles())
            {
                switch (file.Name)
                {
                    case MainDbName:
                    {
                        using var reader = new StreamReader(file.OpenRead());
                    
                        foreach (var (key, token) in JObject.Parse(reader.ReadToEnd()))
                        {
                            switch (token)
                            {
                                case JObject obj when obj.ContainsKey("type"):
                                {
                                    try
                                    {
                                        var enumType = Type.GetType(obj.Get<string>("type")!)!;

                                        if(enumType.IsEnum && Enum.TryParse(enumType, obj.GetValue("data")!.ToString(), out var enumData))
                                        {
                                            _dataSet[key] = (obj.Get<bool>("readonly"), enumData);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.WriteLine($"cannot parse property {key}");
                                        Debug.WriteLine($"Reason : {e}");
                                    }
                                    break;
                                }
                            
                                case JObject obj:
                                {
                                    try
                                    {
                                        if (obj.TryGetValue("data", out var tokenData))
                                        {
                                            object value = tokenData.Type switch
                                            {
                                                JTokenType.Array => (JArray)tokenData,
                                                JTokenType.Object => (JObject)tokenData,
                                                JTokenType.Boolean => (bool)tokenData,
                                                JTokenType.Integer => (int)tokenData,
                                                JTokenType.Float => (float)tokenData,
                                                JTokenType.String => tokenData.ToString(),
                                                _ => null
                                            };

                                            if (value != null)
                                            {
                                                _dataSet[key] = (obj.Get<bool>("readonly"), value);
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.WriteLine($"cannot parse property {key}");
                                        Debug.WriteLine($"Reason : {e}");
                                    }
                                    break;
                                }
                            }
                        }
                    
                        reader.Close();
                        break;
                    }
                }
            }
        }

        public override void Save()
        {
            if (!path.Exists) path.Create();
            var saveObj = new JObject();

            foreach (var (key, (@readonly, data)) in _dataSet)
            {
                switch (data)
                {
                    case JObject:
                    case JArray:
                    {
                        saveObj[key] = new JObject
                        {
                            ["readonly"] = @readonly,
                            ["data"] = JToken.FromObject(data)
                        };
                        break;
                    }

                    case Enum value:
                    {
                        saveObj[key] = new JObject
                        {
                            ["readonly"] = @readonly,
                            ["type"] = value.GetType().GetTypeInfo().FullName,
                            ["data"] = new JValue(value.GetHashCode())
                        };
                        break;
                    }

                    case IConvertible:
                    {
                        saveObj[key] = new JObject
                        {
                            ["readonly"] = @readonly,
                            ["data"] = JToken.FromObject(data)
                        };
                        break;
                    }

                    case ISerializable:
                    {
                        break;
                    }
                }
            }

            using var writer = new StreamWriter(Path.Combine(path.ToString(), MainDbName), false);
            writer.WriteAsync(saveObj.ToString(Formatting.Indented));
        }
    }
}