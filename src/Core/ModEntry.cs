using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Monsters;
using HPBars.src.UI;
using HPBars.src.Utils;

namespace HPBars.src.Core
{
    public class ModEntry : Mod
    {
        internal static ModEntry Instance;
        internal static Config Config;

        public override void Entry(IModHelper helper)
        {
            Instance = this;
            Config = Helper.ReadConfig<Config>();

            TextureManager.LoadTextures(helper);

            helper.Events.Display.RenderedWorld += OnRenderedWorld;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
            helper.Events.GameLoop.ReturnedToTitle += OnReturnedToTitle;

            helper.ConsoleCommands.Add(
                "hpbars_showfull",
                "Enable or disable showing healthbars when monsters full health.\nUsage: hpbars_showfull <true/false>",
                OnShowFullLifeCommand);

            helper.ConsoleCommands.Add(
                "hpbars_range",
                "Enable or disable the range verification.\nUsage: hpbars_range <true/false>",
                OnRangeVerificationCommand);
        }

        private void OnRenderedWorld(object sender, RenderedWorldEventArgs e)
        {
            if (!Context.IsWorldReady) return;

            foreach (var monster in Game1.currentLocation.characters.OfType<Monster>())
            {
                HealthBar.Render(Game1.spriteBatch, monster, Monitor);
            }
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            HealthBar.Cleanup();
        }

        private void OnReturnedToTitle(object sender, ReturnedToTitleEventArgs e)
        {
            HealthBar.Cleanup();
        }

        private void OnShowFullLifeCommand(string command, string[] args)
        {
            if (args.Length <= 0) return;

            if (bool.TryParse(args[0], out bool showFull))
            {
                Config.Show_Full_Life = showFull;
                Helper.WriteConfig(Config);
                Monitor.Log("Show healthbars when enemy full life changed.", LogLevel.Info);
            }
            else
            {
                Monitor.Log("Invalid value. Use 'true' or 'false'.", LogLevel.Error);
            }
        }

        private void OnRangeVerificationCommand(string command, string[] args)
        {
            if (args.Length <= 0) return;

            if (bool.TryParse(args[0], out bool enableRange))
            {
                Config.Range_Verification = enableRange;
                Helper.WriteConfig(Config);
                Monitor.Log("Range verification changed.", LogLevel.Info);
            }
            else
            {
                Monitor.Log("Invalid value. Use 'true' or 'false'.", LogLevel.Error);
            }
        }
    }
}
