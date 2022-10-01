using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderMode
{
    public class BuilderModePlugin : RocketPlugin
    {
        public static BuilderModePlugin Instance { get; private set; }
        protected override void Load()
        {
            Instance = this;
            Logger.Log($"Plugin By Margarita#8172", ConsoleColor.Yellow);

            StructureManager.onStructureSpawned += OnStructureSpawned;
            BarricadeManager.onBarricadeSpawned += OnBarricadeSpawned;

            StructureDrop.OnSalvageRequested_Global += StructureDrop_OnSalvageRequested_Global;
            BarricadeDrop.OnSalvageRequested_Global += BarricadeDrop_OnSalvageRequested_Global;
        }

        private void BarricadeDrop_OnSalvageRequested_Global(BarricadeDrop barricade, SteamPlayer instigatorClient, ref bool shouldAllow)
        {

            if (this.Builders.Contains(barricade.GetServersideData().owner))
            {

                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(instigatorClient);

                List<InventorySearch> list = player.Inventory.search(barricade.asset.id, true, true);

                if (list.Count > 1) player.Inventory.removeItem(list[0].page, player.Inventory.getIndex(list[0].page, list[0].jar.x, list[0].jar.y));
            }

        }

        private void StructureDrop_OnSalvageRequested_Global(StructureDrop structure, SteamPlayer instigatorClient, ref bool shouldAllow)
        {

            if (this.Builders.Contains(structure.GetServersideData().owner))
            {
                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(instigatorClient);

                List<InventorySearch> list = player.Inventory.search(structure.asset.id, true, true);

                if (list.Count > 1) player.Inventory.removeItem(list[0].page, player.Inventory.getIndex(list[0].page, list[0].jar.x, list[0].jar.y));
            }

        }

        private void OnBarricadeSpawned(BarricadeRegion region, BarricadeDrop drop)
        {
            if (this.Builders.Contains(drop.GetServersideData().owner))
            {
                UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromCSteamID(new CSteamID(drop.GetServersideData().owner));
                if (unturnedPlayer != null)
                {
                    unturnedPlayer.GiveItem(drop.asset.id, 1);
                }
            }
        }

        private void OnStructureSpawned(StructureRegion region, StructureDrop drop)
        {
            if (this.Builders.Contains(drop.GetServersideData().owner))
            {
                UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromCSteamID(new CSteamID(drop.GetServersideData().owner));
                if (unturnedPlayer != null)
                {
                    unturnedPlayer.GiveItem(drop.asset.id, 1);
                }
            }
        }

        protected override void Unload()
        {
            StructureManager.onStructureSpawned -= OnStructureSpawned;
            BarricadeManager.onBarricadeSpawned -= OnBarricadeSpawned;
            StructureDrop.OnSalvageRequested_Global -= StructureDrop_OnSalvageRequested_Global;
            BarricadeDrop.OnSalvageRequested_Global -= BarricadeDrop_OnSalvageRequested_Global;
        }

        public List<ulong> Builders = new List<ulong>();
    }
}
