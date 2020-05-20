using System.Collections.Generic;
using System.Linq;

namespace AuroraGSI
{
    public class GSINode
    {
        public readonly ProviderNode provider = new ProviderNode();
        public readonly WorldNode world = new WorldNode();
        public readonly PlayerNode player = new PlayerNode();

        public GSINode Update()
        {
            player.Update();
            world.Update();
            return this;
        }

        public class ProviderNode
        {
            public string name = "tmodloader";
            public int appid = 1281930;
        }

        public class PlayerNode
        {
            public bool inGame;

            public int depth;
            public int depthLayer;
            public int maxdepth;
            public int health;
            public int maxHealth;
            public int mana;
            public int maxMana;
            public int defense;
            public int biome;

            public bool zoneTowerSolar;
            public bool zoneTowerVortex;
            public bool zoneTowerNebula;
            public bool zoneTowerStardust;
            public bool zoneOldOneArmy;

            public bool zonePeaceCandle;
            public bool zoneWaterCandle;

            public bool zoneCorrupt;
            public bool zoneCrimson;
            public bool zoneHoly;

            public bool zoneGlowshroom;
            public bool zoneUndergroundDesert;
            public bool zoneMeteor;
            public bool zoneDungeon;
            public bool zoneSandstorm;
    
            public void Update()
            {
                try
                {
                    var player = Terraria.Main.LocalPlayer;
                    inGame = !Terraria.Main.gameMenu;
                    depth = (int)player.position.Y;
                    maxdepth = (int)Terraria.Main.bottomWorld;
                    health = player.statLife;
                    maxHealth = player.statLifeMax;
                    mana = player.statMana;
                    maxMana = player.statManaMax;
                    defense = player.statDefense;

                    if (player.ZoneUnderworldHeight)     depthLayer = 0;
                    else if (player.ZoneRockLayerHeight) depthLayer = 1;
                    else if (player.ZoneDirtLayerHeight) depthLayer = 2;
                    else if (player.ZoneOverworldHeight) depthLayer = 3;
                    else if (player.ZoneSkyHeight)       depthLayer = 4;
                    else depthLayer = -1;

                    zoneTowerSolar = player.ZoneTowerSolar;
                    zoneTowerVortex = player.ZoneTowerVortex;
                    zoneTowerNebula = player.ZoneTowerNebula;
                    zoneTowerStardust = player.ZoneTowerStardust;
                    zoneOldOneArmy = player.ZoneOldOneArmy;

                    zonePeaceCandle = player.ZonePeaceCandle;
                    zoneWaterCandle = player.ZoneWaterCandle;

                    zoneCorrupt = player.ZoneCorrupt;
                    zoneCrimson = player.ZoneCrimson;
                    zoneHoly = player.ZoneHoly;

                    zoneGlowshroom = player.ZoneGlowshroom;
                    zoneUndergroundDesert = player.ZoneUndergroundDesert;
                    zoneMeteor = player.ZoneMeteor;
                    zoneDungeon = player.ZoneDungeon;
                    zoneSandstorm = player.ZoneSandstorm;

                    if (player.ZoneDesert) biome = 4;
                    else if (player.ZoneSnow) biome = 3;
                    else if (player.ZoneJungle) biome = 2;
                    else if (player.ZoneBeach) biome = 1;
                    else biome = 0;//forest
                }
                catch
                { }
            }
        }

        public class WorldNode
        {
            public double time;
            public bool raining;
            public bool hardMode;
            public bool expertMode;
            public bool eclipse;
            public bool bloodMoon;
            public bool pumpkinMoon;
            public bool snowMoon;
            public bool slimeRain;
            public int boss;

            public void Update()
            {
                try
                {
                    eclipse = Terraria.Main.eclipse;
                    hardMode = Terraria.Main.hardMode;
                    expertMode = Terraria.Main.expertMode;
                    time = Terraria.Main.time;
                    bloodMoon = Terraria.Main.bloodMoon;
                    pumpkinMoon = Terraria.Main.pumpkinMoon;
                    snowMoon = Terraria.Main.snowMoon;
                    raining = Terraria.Main.raining;
                    slimeRain = Terraria.Main.slimeRain;
                    boss = GetBoss(System.Array.Find(Terraria.Main.npc, n => n.boss)?.type ?? -1);
                }
                catch
                { }
            }
        }

        private static int GetBoss(int type)
        {
            switch (type)
            {
                case 50://king slime
                    return 0;
                case 4://eye
                    return 1;
                case 13:
                case 14:
                case 15://eater
                    return 2;
                case 266://brain
                    return 3;
                case 222://bee
                    return 4;
                case 35://skeletron
                    return 5;
                case 113://wall
                    return 6;
                case 125:
                case 126://twins
                    return 7;
                case 134://destroyer
                    return 8;
                case 127://prime
                    return 9;
                case 262://plantera
                    return 10;
                case 245://golem
                    return 11;
                case 370://fishron
                    return 12;
                case 439://cultist
                    return 13;
                case 396:
                case 397:
                case 398://moon lord D:
                    return 14;
                default:
                    return -1;
            }
        }
    }
}
