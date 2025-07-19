using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HPBars.src.UI
{
    public class DamageIndicator
    {
        public float PreviousHealth { get; set; }
        public float DamageAmount { get; set; }
        public float AnimationTimer { get; set; }
        public const float ANIMATION_DURATION = 0.75f;

        public DamageIndicator(float currentHealth)
        {
            PreviousHealth = currentHealth;
            DamageAmount = 0;
            AnimationTimer = 0;
        }

        public void Update(float currentHealth, float gameTime)
        {
            if (currentHealth < PreviousHealth)
            {
                DamageAmount = PreviousHealth - currentHealth;
                AnimationTimer = ANIMATION_DURATION;
            }

            if (AnimationTimer > 0)
            {
                AnimationTimer = Math.Max(0, AnimationTimer - gameTime);
            }

            PreviousHealth = currentHealth;
        }

        public Color GetDamageColor()
        {
            if (AnimationTimer <= 0) return Color.Transparent;

            float alpha = (AnimationTimer / ANIMATION_DURATION);
            return new Color(255, 40, 40, 0.3f + (alpha * 0.7f));
        }
    }
}
