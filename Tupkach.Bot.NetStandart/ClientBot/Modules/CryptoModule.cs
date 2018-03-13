using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json;
using Tupkach.Bot.NetStandart.ClientBot.Context;

namespace Tupkach.Bot.NetStandart.ClientBot.Modules
{
    public class CryptoModule : ModuleBase<TupkachCommandContext>
    {
        [Command(nameof(PriceBtc))]
        public async Task PriceBtc()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.coindesk.com/v1/bpi/currentprice.json");
            request.AutomaticDecompression = DecompressionMethods.GZip;
            
            var rates = new StringBuilder();

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                {
                    return;
                }
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                    var value = dict["time"].ToString();
                    var time = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
                    rates.Append(time["updated"]);
                    rates.AppendLine();

                    dict.Remove("time");
                    dict.Remove("disclaimer");
                    dict.Remove("chartName");

                    var currencies = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(dict["bpi"].ToString());
                    foreach (var currency in currencies)
                    {
                        var code = currency.Value["code"];
                        var rate = currency.Value["rate"];
                        rates.Append($"{code}: {rate}");
                        rates.AppendLine();
                    }
                }
            }
            await Context.Channel.SendMessageAsync(rates.ToString());
        }
    }
}
