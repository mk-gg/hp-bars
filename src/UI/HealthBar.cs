using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using HPBars.src.Utils;
using HPBars.src.Core;


namespace HPBars.src.UI
{
    public class HealthBar
    {
        private static readonly Dictionary<Monster, DamageIndicator> _damageIndicators = new();
        private static readonly int _verificationRange = 100 * Game1.pixelZoom;

        public static void Render(SpriteBatch spriteBatch, Monster monster, IMonitor monitor)
        {
            if (!ShouldRenderHealthBar(monster))
                return;

            float currentHealth = monster.Health;
            float maxHealth = Math.Max(monster.Health, monster.MaxHealth);
            monster.MaxHealth = (int)maxHealth;

            // Update damage indicator
            if (!_damageIndicators.ContainsKey(monster))
            {
                _damageIndicators[monster] = new DamageIndicator(currentHealth);
            }
            _damageIndicators[monster].Update(currentHealth, (float)Game1.currentGameTime.ElapsedGameTime.TotalSeconds);

            // Get bar position and dimensions
            Vector2 monsterPos = monster.getLocalPosition(Game1.viewport);
            Rectangle barRect = GetBarRectangle(monster, monsterPos);

            // Draw background
            spriteBatch.Draw(TextureManager.Pixel, barRect, new Color(0, 0, 0, 135));

            // Draw damage indicator
            if (_damageIndicators[monster].AnimationTimer > 0)
            {
                DrawDamageIndicator(spriteBatch, monster, currentHealth, maxHealth, barRect);
            }

            // Draw health bar
            Rectangle healthRect = barRect;
            healthRect.Width = (int)(barRect.Width * (currentHealth / maxHealth));
            spriteBatch.Draw(TextureManager.Pixel, healthRect, TextureManager.GetBarColor());

            // Draw border
            Rectangle borderRect = new(
                (int)monsterPos.X - (TextureManager.Border.Width * Game1.pixelZoom) / 2 + (monster.Sprite.SpriteWidth * Game1.pixelZoom) / 2,
                (int)monsterPos.Y - monster.Sprite.SpriteHeight * Game1.pixelZoom - Utils.Constants.BarHeight * Game1.pixelZoom,
                TextureManager.Border.Width * Game1.pixelZoom,
                TextureManager.Border.Height * Game1.pixelZoom
            );
            spriteBatch.Draw(TextureManager.Border, borderRect, TextureManager.GetBorderColor());
        }

        private static bool ShouldRenderHealthBar(Monster monster)
        {
            if (!Context.IsWorldReady || Game1.activeClickableMenu != null || Game1.currentMinigame != null)
                return false;

            if (monster.IsInvisible || !Utility.isOnScreen(monster.position.Value, 3 * Game1.tileSize))
                return false;

            // Skip certain monsters in specific animation frames
            if (monster is RockCrab && monster.Sprite.CurrentFrame % 4 == 0)
                return false;
            if (monster is RockGolem && monster.Sprite.CurrentFrame == 16)
                return false;
            if (monster is Spiker)
                return false;

            // Range verification
            if (ModEntry.Config.Range_Verification)
            {
                Vector2 playerPos = Game1.player.Position;
                Vector2 monsterPos = monster.Position;
                if (Math.Abs(monsterPos.X - playerPos.X) > _verificationRange ||
                    Math.Abs(monsterPos.Y - playerPos.Y) > _verificationRange)
                    return false;
            }

            return true;
        }

        private static Rectangle GetBarRectangle(Monster monster, Vector2 monsterPos)
        {
            return new Rectangle(
                (int)monsterPos.X - (TextureManager.Pixel.Width * Game1.pixelZoom) / 2 +
                    (monster.Sprite.SpriteWidth * Game1.pixelZoom) / 2 - Utils.Constants.BarOffsetX * Game1.pixelZoom,
                (int)monsterPos.Y - monster.Sprite.SpriteHeight * Game1.pixelZoom -
                    Utils.Constants.BarHeight * Game1.pixelZoom + 8 * Game1.pixelZoom,
                TextureManager.Pixel.Width * Game1.pixelZoom * Utils.Constants.BarWidth,
                TextureManager.Pixel.Height * Game1.pixelZoom * 4
            );
        }

        private static void DrawDamageIndicator(SpriteBatch spriteBatch, Monster monster, float currentHealth, float maxHealth, Rectangle barRect)
        {
            float previousHealthRatio = _damageIndicators[monster].PreviousHealth / maxHealth;
            float currentHealthRatio = currentHealth / maxHealth;

            Rectangle damageRect = barRect;
            damageRect.X += (int)(barRect.Width * currentHealthRatio);
            damageRect.Width = (int)(barRect.Width * (previousHealthRatio - currentHealthRatio));

            spriteBatch.Draw(TextureManager.Pixel, damageRect, _damageIndicators[monster].GetDamageColor());
        }

        public static void CleanupMonster(Monster monster)
        {
            if (_damageIndicators.ContainsKey(monster))
            {
                _damageIndicators.Remove(monster);
            }
        }

        public static void Cleanup()
        {
            _damageIndicators.Clear();
        }


    }
}
