using System;

namespace Tupkach.Bot.NetStandart.Infrastructure
{
    class ConfigurationFieldAttribute : Attribute
    {
        public string AttributeFieldName { get; private set; }
        public string DefaultValue { get; private set; }

        public ConfigurationFieldAttribute(string FieldName = "", string defaultValue = "xd")
        {
            AttributeFieldName = FieldName;
            DefaultValue = defaultValue;
        }
    }
}
