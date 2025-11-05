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
    public TextAsset chartJSON;  // JSON 文件
    public AudioClip musicClip;  // 可选：如果 JSON 不含 clip，可在 Inspector 指定

    [Header("Enemy Prefabs & Spawn Points")]
    public GameObject enemyTopPrefab;
    public GameObject enemyBottomPrefab;
    public Transform topLane;
    public Transform bottomLane;

    //内部数据结构
    [System.Serializable]
    public class NoteData
    {
        public float time;   // 秒
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
        // 解析 JSON
        if (chartJSON == null)
        {
            Debug.LogError("Chart JSON not assigned!");
            return;
        }

        chart = JsonUtility.FromJson<Chart>(chartJSON.text);
        if (chart == null || chart.notes.Length == 0)
        {
            Debug.LogError("Failed to parse chart or chart has no notes!");
            return;
        }
        Debug.Log($"Chart loaded: {chart.songName}, Notes count = {chart.notes.Length}");

        // 播放音乐
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
        }
        else
        {
            Debug.LogWarning("No AudioClip assigned, make sure JSON contains audio or assign manually.");
        }

        audioSource.Play();

        // 开始生成音符
        StartCoroutine(SpawnNotes());
    }

    void Update()
    {
        // 检测音乐结束
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
            // 等待到音符对应时间
            yield return new WaitUntil(() => audioSource.time >= note.time);

            // 选择 prefab 和 spawn point
            GameObject prefab = note.lane == "top" ? enemyTopPrefab : enemyBottomPrefab;
            Transform spawnPoint = note.lane == "top" ? topLane : bottomLane;

            Vector3 spawnPos = new Vector3(
                spawnPoint.position.x,
                prefab.transform.position.y, // 保留 prefab Y，如果 prefab 自带偏移
                prefab.transform.position.z
            );

            // 生成敌人
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