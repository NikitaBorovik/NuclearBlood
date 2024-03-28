using App.World.Entity.Player.Events;
using UnityEngine;

namespace App.Volume
{
    public class BasicVolumeChanger : MonoBehaviour
    {
        [SerializeField] private ValueUpdatedEvent onVolumeUpdated;

        public ValueUpdatedEvent OnVolumeUpdated => onVolumeUpdated;

        public void VolumeUp()
        {
            var prevVolume = AudioListener.volume;
            if (AudioListener.volume < 1f)
            {
                AudioListener.volume += 0.1f;
            }
            if (AudioListener.volume > 1f)
                AudioListener.volume = 1f;
            PlayerPrefs.SetFloat("Volume", AudioListener.volume);
            OnVolumeUpdated.CallValueUpdateEvent(prevVolume, AudioListener.volume, 1f);
        }
        public void VolumeDown()
        {
            var prevVolume = AudioListener.volume;
            if (AudioListener.volume > 0f)
            {
                AudioListener.volume -= 0.1f;
            }
            if (AudioListener.volume < 0f)
                AudioListener.volume = 0f;
            PlayerPrefs.SetFloat("Volume", AudioListener.volume);
            OnVolumeUpdated.CallValueUpdateEvent(prevVolume, AudioListener.volume, 1f);
        }
    }

}
