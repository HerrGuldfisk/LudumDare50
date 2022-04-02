using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Basics.Audio
{
    public class GlobalMusicManager : MonoBehaviour
    {
        public static GlobalMusicManager Instance = null;

        #region Singleton
        private void Awake()
        {
            CreateSingleton();
            InitializeAudioSources();
        }

        private void CreateSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        [SerializeField] private AudioMixer mainMixer;
        public AudioMixer MainMixer { get => mainMixer; }

        [Tooltip("SoundData Scriptable Objects")]
        [SerializeField] private List<SoundData> soundData = new List<SoundData>();

        // All the created audiosources are saved here.
        private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

        // Populate a Dictionary<string, AudioSource> with data from the SoundData List.
        private void InitializeAudioSources()
        {
            foreach (SoundData data in soundData)
            {
                AudioSource newSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

                newSource.clip = data.audioFile;
                newSource.volume = data.volume;
                newSource.pitch = data.pitch;
                newSource.spatialBlend = data.spacialBlend;

                newSource.outputAudioMixerGroup = data.audioMixerGroup;

                audioSources.Add(data.audioFile.name, newSource);
            }
        }

        public void PlayMusic(string name, bool looping = true)
        {
            if(GetAudioSource(name, out AudioSource audioSource))
            {
                audioSource.loop = looping;
                audioSource.volume = 1;
                audioSource.Play();
            }
        }

        public void PlayMusic(string name, float time, float volume = 1, bool looping = true)
        {
            if (GetAudioSource(name, out AudioSource audioSource))
            {
                audioSource.loop = looping;

                if (time == 0)
                {
                    PlayMusic(name, looping);
                }
                else
                {
                    audioSource.volume = 0;
                    audioSource.Play();
                    StartCoroutine(TransitionVolume(audioSource, time, volume));
                }
            }
        }

        public void StopMusic(string name, float time = 0)
        {
            if (audioSources.ContainsKey(name))
            {
                if(time == 0)
                {
                    audioSources[name].volume = 0;
                    audioSources[name].Stop();
                }
                else
                {
                    StartCoroutine(TransitionVolume(audioSources[name], time, 0));
                }
            }
        }

        public void StopMusic(AudioSource source, float time = 0)
        {
            if (time == 0)
            {
                source.volume = 0;
                source.Stop();
            }
            else
            {
                StartCoroutine(TransitionVolume(source, time, 0));
            }
        }

        public void PauseMusic(string name)
        {
            if(GetAudioSource(name, out AudioSource source))
            {
                if (source.isPlaying)
                {
                    source.Pause();
                }
                else
                {
                    source.Play();
                }
            }
        }

        private IEnumerator TransitionVolume(AudioSource source, float time = 0, float targetVolume = 0)
        {
            float startValue = source.volume;
            float currentTime = 0;

            while(source.volume != targetVolume)
            {
                source.volume = Mathf.Lerp(startValue, targetVolume, currentTime);
                currentTime += Time.unscaledDeltaTime / time;

                if(currentTime >= 1)
                {
                    source.volume = targetVolume;

                    // If the transition is to 0 volume the clip is stoped.
                    if(source.volume == 0)
                    {
                        source.Stop();
                    }
                }

                yield return null;
            }
        }

        public void ChangeMusic(string newMusic, string oldMusic, float time, bool looping = true)
        {
            if(GetAudioSource(newMusic, out AudioSource newSource) && GetAudioSource(oldMusic, out AudioSource oldSource))
            {
                StopMusic(oldSource, time);
                PlayMusic(newMusic, time);
            }
            else
            {
                Debug.LogWarning($"Either: {newMusic} or {oldMusic} are missing from the GlobalMusicManager");
            }
        }

        /// <summary>
        /// Return true if there exist an AudioSource with the audioName
        /// if not false is returned and the out AudioSource is null.
        /// </summary>
        /// <param name="audioName"></param>
        /// <param name="audioSource"></param>
        /// <returns></returns>
        private bool GetAudioSource(string audioName, out AudioSource audioSource)
        {
            if (audioSources.ContainsKey(audioName))
            {
                audioSource = audioSources[audioName];
                return true;
            }
            else
            {
                Debug.LogWarning($"No audio source with the clip name {audioName}");
                audioSource = null;
                return false;
            }
        }

        /// <summary>
        /// Search for the currently playing Audio Sources and returns them in a List.
        /// </summary>
        /// <returns>List of AudioSource</returns>
        private List<AudioSource> GetPlayingAudioSources()
        {
            List<AudioSource> sources = new List<AudioSource>();

            foreach(KeyValuePair<string, AudioSource> kvp in audioSources)
            {
                if (kvp.Value.isPlaying)
                {
                    sources.Add(kvp.Value);
                }
            }

            return sources;
        }
    }
}

