using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static MusicPlayManager;
using UnityEngine.SceneManagement;

public class MusicPlayManager : MonoBehaviour
{
    [Header("Music & Chart")]
    public AudioSource audioSource;

    [Header("Enemy Prefabs & Spawn Points")]
    public GameObject enemyTopPrefab;
    public GameObject enemyBottomPrefab;
    public Transform topLane;
    public Transform bottomLane;

    // Internal data structure
    [System.Serializable]
    public class NoteData
    {
        public float time;   // secs
        public string lane;  // "top" / "bottom"
    }

    [System.Serializable]
    public class Chart
    {
        public string songName;
        public string mode;
        public NoteData[] notes;
    }

    private Chart chart;

   
    void Start()
    {
        // 1️ check if any music was selected
        if (MusicSelectData.selectedSong == null)
        {
            Debug.LogError("No song selected!");
            return;
        }

        SongData song = MusicSelectData.selectedSong;

        // 2️ play music
        audioSource.clip = song.musicClip;
        audioSource.Play();

        // 3️ resolve json file
        chart = JsonUtility.FromJson<Chart>(song.chartData.text);
        Debug.Log($"Playing: {chart.songName}, Notes: {chart.notes.Length}");

        // start spawn notes
        StartCoroutine(SpawnNotes());
    }

    void Update()
    {
        if (PauseManager.isPaused) return;
        // check if music has finished playing
        if (!audioSource.isPlaying && audioSource.time > 0.1f)
        {
            Debug.Log("Song finished!");
            CalculateRank();
            SceneManager.LoadScene("GameOverScene");
        }
    }

    IEnumerator SpawnNotes()
    {
        foreach (var note in chart.notes)
        {
            // Waiting for the corresponding time of the note
            //yield return new WaitUntil(() => audioSource.time >= note.time);
            yield return new WaitUntil(() => !PauseManager.isPaused && audioSource.time >= note.time);

            // choose prefab and spawn point
            GameObject prefab = note.lane == "top" ? enemyTopPrefab : enemyBottomPrefab;
            Transform spawnPoint = note.lane == "top" ? topLane : bottomLane;

            Vector3 spawnPos = new Vector3(
                spawnPoint.position.x,
                prefab.transform.position.y, // keep prefab Y，if prefab has offset
                prefab.transform.position.z
            );

            // spawn enemies
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    void CalculateRank()
    {
        int totalHits = GameResultData.perfectCount + GameResultData.greatCount + GameResultData.missCount;
        if (totalHits == 0) totalHits = 1;

        float accuracy = (GameResultData.perfectCount * 1f + GameResultData.greatCount * 0.7f) / totalHits;

        if (accuracy >= 0.95f)
            GameResultData.rank = "S";
        else if (accuracy >= 0.85f)
            GameResultData.rank = "A";
        else if (accuracy >= 0.7f)
            GameResultData.rank = "B";
        else
            GameResultData.rank = "C";
    }
}