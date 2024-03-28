using System;
using UnityEngine;

namespace App.UI.Events
{
    public class DeathScreenAppearedEvent : MonoBehaviour
    {
        public event Action<DeathScreenAppearedEvent> OnDeathScreenAppeared;

        public void CallDeathScreenAppearedEvent()
        {
            OnDeathScreenAppeared?.Invoke(this);
        }
    }
}
