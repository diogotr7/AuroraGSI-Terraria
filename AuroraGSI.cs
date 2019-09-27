using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AuroraGSI
{
	public class AuroraGSI : Mod
	{
        private readonly GSINode node = new GSINode();
        private HttpClient http;
        private bool ok;

        public override void Load()
        {
            http = new HttpClient();
            ok = true;
            Task.Run(() => Loop());
            base.Load();
        }

        private void Loop()
        {
            while (ok)
            {
                SendGameState();
                System.Threading.Thread.Sleep(100);
            }
        }

        public override void Unload()
        {
            ok = false;
            http.Dispose();
            base.Unload();
        }

        public void SendGameState()
        {
            try
            {
                http.PostAsync("http://localhost:9088",
                        new StringContent(JsonConvert.SerializeObject(node.Update()),
                                            Encoding.UTF8, "application/json"));
            }
            catch
            { }
        }
    }
}