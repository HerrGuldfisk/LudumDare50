using UnityEditor;
using UnityEngine;
using Basics.Audio;

[CustomEditor(typeof(SoundData))]
public class SoundDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SoundData soundData = target as SoundData;

        if (GUILayout.Button("Rename Asset"))
        {
            soundData.RenameAssetToAudioFile();
        }
    }
}
