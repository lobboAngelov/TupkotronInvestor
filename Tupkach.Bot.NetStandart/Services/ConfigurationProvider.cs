using System.Collections.Generic;
using Newtonsoft.Json;
using Tupkach.Bot.NetStandart.Services.Interfaces;

namespace Tupkach.Bot.NetStandart.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly Dictionary<string, string> _configurationInternal;

        public ConfigurationProvider()
        {
            var jsonRaw = System.IO.File.ReadAllText(StaticData.ConfigurationFilePath);
            _configurationInternal = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonRaw);
        }

        public void CreateValidFile()
        {
            var dict = new Dictionary<string, string>();
                        
        }

        public string GetConfigurationParameter(string parameter)
        {
            if (!_configurationInternal.ContainsKey(parameter))
            {
                System.Console.WriteLine($"No parameter found in cofig called \"{parameter}\"");
            }
            return _configurationInternal[parameter];
        }
    }
}
