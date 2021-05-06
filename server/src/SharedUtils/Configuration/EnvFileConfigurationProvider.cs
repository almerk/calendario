using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
namespace SharedUtils.Configuration
{
    /// <summary>
    /// This configuration provider may be useful for development stage when configuration data that has to be written into environment variables located in .env file 
    /// </summary>
    public class EnvFileConfigurationProvider : ConfigurationProvider
    {
        const char KEY_VALUE_SPLITTER = '=';
        public string EnvFilePath { get; }
        public EnvFileConfigurationProvider(string envFilePath)
        {
            EnvFilePath = envFilePath;
        }
        public override void Load()
        {
            if (string.IsNullOrWhiteSpace(EnvFilePath)) return;
            if (!File.Exists(EnvFilePath)) return;
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (FileStream fs = new FileStream(EnvFilePath, FileMode.Open))
            {
                using (StreamReader textReader = new StreamReader(fs))
                {
                    string line;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        var splitted = line.Split(KEY_VALUE_SPLITTER, 2);
                        if (splitted.Length < 2) continue;
                        var key = splitted[0];
                        var value = splitted[1];
                        data.Add(key, value);
                    }
                }
            }
            Data = data;
        }
    }
}