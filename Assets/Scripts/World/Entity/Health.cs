using UnityEngine;
using App.World.Entity.Player.Events;
using System.Collections.Generic;
using System.Collections;

namespace App.World.Entity
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Health : MonoBehaviour
    {
        private float currentHealth;
        private float maxHealth;
        private Color32 blinkColor = new Color32(215, 110, 110, 255);
        private Dictionary<SpriteRenderer, Color> spriteRenderers;
        private List<SpriteRenderer> toDelete;
        private Coroutine blinkRoutine;

        [SerializeField] private float blinkTime;
        //public ValueUpdateEvent healthUpdateEvent;


        public float CurrentHealth
        {
            get => currentHealth;
            private set
            {
                var prev = currentHealth;

                if (value > MaxHealth)
                    currentHealth = MaxHealth;
                else if (value < 0)
                    currentHealth = 0;
                else
                    currentHealth = value;

                //healthUpdateEvent?.CallValueUpdateEvent(prev, currentHealth, MaxHealth);

            }
        }

        public float MaxHealth
        {
            get => maxHealth;
            set
            {
                if (MaxHealth < 0f)
                {
                    maxHealth = 0f;
                    return;
                }

                var prevMaxHealth = maxHealth;
                maxHealth = value;

                if (maxHealth > prevMaxHealth)
                {
                    CurrentHealth += maxHealth - prevMaxHealth;
                }
                else if (maxHealth < CurrentHealth)
                {
                    CurrentHealth = maxHealth;
                }
            }
        }

        public void Awake()
        {
            CurrentHealth = MaxHealth;
            spriteRenderers = new Dictionary<SpriteRenderer, Color>();
            toDelete = new List<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderers.Add(spriteRenderer, spriteRenderer.color);
            }

        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            Blink();
            if (CurrentHealth <= 0)
            {
                IKillable baseScript = GetComponent<IKillable>();
                baseScript.Die();
            }
        }

        public void Heal(float amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        public void HealToMax()
        {
            CurrentHealth = MaxHealth;
        }

        public void ChangeMaxHealth(float amount)
        {
            MaxHealth += amount;
        }

        private IEnumerator BlinkCoroutine()
        {
            if (spriteRenderers == null)
                yield break;
            foreach (SpriteRenderer spriteRenderer in spriteRenderers.Keys)
            {
                if (spriteRenderer != null)
                    spriteRenderer.color = blinkColor;
                else
                    toDelete.Add(spriteRenderer);
            }
            yield return new WaitForSeconds(blinkTime);
            RemoveBlinkingColor();
            blinkRoutine = null;
        }

        private void RemoveBlinkingColor()
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers.Keys)
            {
                if (spriteRenderer != null)
                    spriteRenderer.color = spriteRenderers[spriteRenderer];
            }
        }

        private void Blink()
        {
            if (blinkRoutine != null)
            {
                StopCoroutine(blinkRoutine);
            }
            foreach (SpriteRenderer spriteRenderer in toDelete)
            {
                spriteRenderers.Remove(spriteRenderer);
            }
            toDelete.Clear();

            if (gameObject.activeSelf)
            {
                blinkRoutine = StartCoroutine(BlinkCoroutine());
            }
            else
            {
                RemoveBlinkingColor();
            }
        }
    }
}

