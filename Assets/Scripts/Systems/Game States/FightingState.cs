using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Systems.GameStates
{
    public class FightingState : IState
    {
        private WaveSystem waveSystem;
        private GameStatesSystem gameStatesSystem;
        private Light2D globalLight;
        private float transitDuration = 2.5f;
        private float musicFadeTime = 1f;
        private float elapseTime = 0f;
        private AudioClip fightingMusic;
        private AudioSource audioSource;
        public FightingState(GameStatesSystem gameStatesSystem, WaveSystem waveSystem, AudioClip fightingMusic, AudioSource audioSource, Light2D globalLight)
        {
            this.gameStatesSystem = gameStatesSystem;
            this.waveSystem = waveSystem;
            this.fightingMusic = fightingMusic;
            this.audioSource = audioSource;
            this.globalLight = globalLight;
        }
        public void Enter()
        {
            waveSystem.StartWave();
            gameStatesSystem.StartCoroutine(StartMusic());
        }
        public void Exit()
        {
            elapseTime = 0;
            gameStatesSystem.StartCoroutine(StopMusic());
        }

        public void Update()
        {
            if (globalLight.intensity > 0.5f)
            {
                elapseTime += Time.deltaTime;
                globalLight.intensity = Mathf.Lerp(1f, 0.5f, elapseTime / transitDuration);
            }

        }
        private IEnumerator StartMusic()
        {
            yield return new WaitForSeconds(2f);
            audioSource.volume = 0f;
            audioSource.clip = fightingMusic;
            audioSource.Play();
            float currentTime = 0;
            float start = 0f;
            while (currentTime < musicFadeTime)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0.1f, currentTime / musicFadeTime);
                yield return new WaitForFixedUpdate();
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
                audioSource.volume = Mathf.Lerp(start, 0, currentTime/ musicFadeTime);
                yield return new WaitForFixedUpdate();
            }
            audioSource.Stop();
            yield break;
        }
    }
}

