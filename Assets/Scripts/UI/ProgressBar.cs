using UnityEngine;
using App.World.Entity.Player.Events;

namespace App.World.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Transform progressTransform;
        [SerializeField] private ValueUpdatedEvent valueUpdateEvent;
        [Range(0f, 1f)]
        [SerializeField] private float currentPercentage;

        public float CurrentPercentage
        {
            get => currentPercentage;
            set
            {
                if (value < -Mathf.Epsilon || value > 1 + Mathf.Epsilon)
                    throw new System.InvalidOperationException("Percentage ranges between 0 and 1.");
                currentPercentage = value;
                SetGUIPercentage(value);
            }
        }

        private void OnEnable()
        {
            valueUpdateEvent.OnValueUpdate += OnValueUpdate;
        }

        private void OnDisable()
        {
            valueUpdateEvent.OnValueUpdate -= OnValueUpdate;
        }

        private void SetGUIPercentage(float percentage)
        {
            var prev = progressTransform.localScale;
            progressTransform.localScale = new(percentage, prev.y, prev.z);
        }

        private void OnValueUpdate(ValueUpdatedEvent ev, ValueUpdatedEventArgs args)
            => CurrentPercentage = args.newValue >= 0f && args.maxValue > Mathf.Epsilon
            ? args.newValue / args.maxValue
            : 0f;
    }
}
