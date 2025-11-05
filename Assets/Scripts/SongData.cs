using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "MusicGame/SongData")]
public class SongData : ScriptableObject
{
    public string songName;
    public AudioClip musicClip;
    public TextAsset chartData;
    public Sprite coverImage;  // cover img
}
