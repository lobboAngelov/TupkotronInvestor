using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupkach.Bot.NetStandart.Infrastructure;
using Ninject;

namespace Tupkach.Bot.NetStandart
{
    public static class StaticData
    {
        public const string ConfigurationFilePath = "Configuration/config.json";
        public static string Mode;
        public static IKernel LifetimeScope;
        [ConfigurationField]
        public const string TokenTypeConfigurationKey = "Token";
        [ConfigurationField]
        public const string CommandKeywordConfigurationKey = "CommandKeyword";
        [ConfigurationField]
        public const string ServerNameConfigurationKey = "ServerName";
        [ConfigurationField]
        public const string LogChannelConfigurationKey = "LogChannelName";
    }
}
