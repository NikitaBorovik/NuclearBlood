using App.World.Gates;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Systems.GameStates
{
    public class RestingState : IState
    {
        private GameStatesSystem gameStatesSystem;
        private Gates gates;
        private float transitDuration = 2.5f;
        private float musicFadeTime = 1f;
        private float elapseTime;
        private AudioClip restingMusic;
        private AudioSource audioSource;
        public RestingState(GameStatesSystem gameStatesSystem, Gates gates, AudioClip restingMusic,AudioSource audioSource)
        {
            this.gameStatesSystem = gameStatesSystem;
            this.gates = gates;
            this.restingMusic = restingMusic;
            this.audioSource = audioSource;
        }
        public void Enter()
        {
            gates.Open();
            gameStatesSystem.DaysCount++;
            gameStatesSystem.StartCoroutine(StartMusic());
        }

        public void Exit()
        {
            gates.Close();
            elapseTime = 0;
            gameStatesSystem.StartCoroutine(StopMusic());
        }

        public void Update()
        {
        }
        private IEnumerator StartMusic()
        {
            yield return new WaitForSeconds(musicFadeTime);
            audioSource.volume = 0f;
            audioSource.clip = restingMusic;
            audioSource.Play();
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < musicFadeTime)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0.1f, currentTime / musicFadeTime);
                yield return null;
            }
            yield break;
        }
        private IEnumerator StopMusic()
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < musicFadeTime)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0, currentTime / musicFadeTime);
                yield return null;
            }
            audioSource.Stop();
            yield break;
        }
    }

}
