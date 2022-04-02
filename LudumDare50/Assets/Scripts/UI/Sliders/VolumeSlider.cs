using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basics.Audio;
using UnityEngine.UI;

namespace Basics.UI
{
    public class VolumeSlider : MonoBehaviour
    {
        private Slider slider = null;

        public enum SubMixer
        {
            MASTER,
            SPEECH,
            MUSIC,
            EFFECTS
        }

        private void Start()
        {
            slider = GetComponent<Slider>();
            UpdateSlider();
        }

        public SubMixer subMixer;

        public void OnSliderInput(float value)
        {
            switch (subMixer)
            {
                case SubMixer.MASTER:
                    AudioSettings.Instance.SetMasterVolume(value);
                    break;

                case SubMixer.SPEECH:
                    AudioSettings.Instance.SetSpeechVolume(value);
                    break;

                case SubMixer.MUSIC:
                    AudioSettings.Instance.SetMusicVolume(value);
                    break;

                case SubMixer.EFFECTS:
                    AudioSettings.Instance.SetEffectsVolume(value);
                    break;
            }
        }

        public void UpdateSlider()
        {
            if(slider == null)
            {
                slider = GetComponent<Slider>();
            }

            switch (subMixer)
            {
                case SubMixer.MASTER:
                    slider.value = AudioSettings.Instance.GetMasterVolume();
                    break;

                case SubMixer.SPEECH:
                    slider.value = AudioSettings.Instance.GetSpeechVolume();
                    break;

                case SubMixer.MUSIC:
                    slider.value = AudioSettings.Instance.GetMusicVolume();
                    break;

                case SubMixer.EFFECTS:
                    slider.value = AudioSettings.Instance.GetEffectsVolume();
                    break;
            }
        }
    }
}

