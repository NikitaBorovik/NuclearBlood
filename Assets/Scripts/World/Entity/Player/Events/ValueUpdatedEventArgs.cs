using System;

namespace App.World.Entity.Player.Events
{
    public class ValueUpdatedEventArgs : EventArgs
    {
        public float prevValue;
        public float newValue;
        public float maxValue;
    }
}