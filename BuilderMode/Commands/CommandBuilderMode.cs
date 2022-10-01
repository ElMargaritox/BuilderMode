using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderMode.Commands
{
    internal class CommandBuilderMode : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "buildermode";

        public string Help => "Enable/Disable BuilderMode";

        public string Syntax => "/buildermode <on>/<off>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            var instance = BuilderModePlugin.Instance;

            if (command.Length < 1 || command.Length >= 2)
            {
                UnturnedChat.Say(caller, $"Use: {this.Syntax}");
                return;
            }

            switch (command[0].ToLower())
            {
                case "on":
                    if (instance.Builders.Contains(player.CSteamID.m_SteamID))
                    {
                        UnturnedChat.Say(caller, "You are in mode builder");
                    }
                    else
                    {
                        UnturnedChat.Say(caller, "Enabled builder mode");
                        instance.Builders.Add(player.CSteamID.m_SteamID);
                    }
                    break;
                case "off":
                    if (instance.Builders.Contains(player.CSteamID.m_SteamID))
                    {
                        instance.Builders.Remove(player.CSteamID.m_SteamID);
                        UnturnedChat.Say(caller, "Disabled builder mode");
                    }
                    else
                    {
                        UnturnedChat.Say(caller, "You are not in mode builder");

                    }
                    break;
                default:
                    UnturnedChat.Say(caller, $"Use: {this.Syntax}");
                    break;
            }


        }
    }
}
