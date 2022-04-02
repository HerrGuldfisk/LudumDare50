using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Basics.Audio
{
    
    [CreateAssetMenu(menuName = "Audio/Sound Data", order = 220)]
    public class SoundData : ScriptableObject
    {
        public AudioClip audioFile;

        [Tooltip("Should be one of the available Sub Mixers")]
        public AudioMixerGroup audioMixerGroup;

        [Header("Sound Settings")]
        [Range(0, 1)]
        public float volume = 1f;

        [Range(-3, 3)]
        public float pitch = 1;

        [Tooltip("0 for 2D and 1 for full 3D")]
        [Range(0, 1)]
        public float spacialBlend = 0;

        public bool loop;

        public void RenameAssetToAudioFile()
        {
            if(audioFile != null)
            {
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(GetInstanceID());
                UnityEditor.AssetDatabase.RenameAsset(assetPath, audioFile.name);
                UnityEditor.AssetDatabase.SaveAssets();
            }
            else
            {
                Debug.LogWarning("Audiofile is missing from SoundData");
            }
        }
    }
}

