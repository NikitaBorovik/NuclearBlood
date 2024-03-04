using System;
using UnityEngine;

namespace App.World.Entity.Player.Events
{
    public class ValueUpdatedEvent : MonoBehaviour
    {
        public event Action<ValueUpdatedEvent, ValueUpdatedEventArgs> OnValueUpdate;

        public void CallValueUpdateEvent(float prevValue, float newHP, float maxHP)
        {
            ValueUpdatedEventArgs args = new() { prevValue = prevValue, newValue = newHP, maxValue = maxHP };
            OnValueUpdate?.Invoke(this, args);
        }
    }
}