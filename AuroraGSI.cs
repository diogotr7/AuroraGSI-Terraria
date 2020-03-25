using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AuroraGSI
{
    public class AuroraGSI : Mod
    {
        private const string URI = "http://localhost:9088";
        private readonly GSINode node = new GSINode();
        private HttpClient http;
        private Timer timer;
        private string last;

        public override void Load()
        {
            last = "";
            http = new HttpClient();
            http.Timeout = TimeSpan.FromMilliseconds(100);
            timer = new Timer(100);
            timer.Enabled = true;
            timer.Elapsed += (a, b) => SendGameState();
            timer.Start();
            base.Load();
        }

        public override void Unload()
        {
            timer?.Dispose();
            http?.Dispose();
            base.Unload();
        }

        public async void SendGameState()
        {
            string data = JsonConvert.SerializeObject(node.Update());
            if (data != last)
            {
                last = data;
                try
                {
                    var response = await http.PostAsync(URI, new StringContent(data, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Logger.Info("Request Failed, stopping further requests. Reload the mod to try again");
                        timer.Enabled = false;
                        //if one of these fails, restarting the mod is required.
                        //this is done for users with the mod installed and aurora closed
                    }
                    response.Dispose();
                }
                catch
                {
                    Logger.Info("Request Failed, stopping further requests. Reload the mod to try again");
                    timer.Enabled = false;
                }
            }
        }
    }
}