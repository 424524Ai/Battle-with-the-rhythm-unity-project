using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SongSelectManager : MonoBehaviour
{
    public List<SongData> allSongs;        // 拖入所有可选曲目
    public Transform contentParent;        // ScrollView 的 Content
    public GameObject songButtonPrefab;    // 曲目按钮预制体
    public Button playButton;              // 右下角 Play 按钮

    private SongSelectButton currentSelected;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化 ScrollView
        foreach (var song in allSongs)
        {
            GameObject btn = Instantiate(songButtonPrefab, contentParent);
            SongSelectButton buttonScript = btn.GetComponent<SongSelectButton>();
            buttonScript.songData = song;
        }

        if(playButton != null)
        {
            playButton.interactable = false;
            playButton.onClick.AddListener(OnPlay);
        }
    }

    public void SelectSong(SongSelectButton button)
    {
        if (currentSelected != null)
            currentSelected.SetHighlight(false);

        currentSelected = button;
        currentSelected.SetHighlight(true);

        Debug.Log("Track selected: " + currentSelected.songData.songName);

        if(playButton != null)
        {
            playButton.interactable = true;
        }
    }

    void OnPlay()
    {
        if (currentSelected == null)
        {
            Debug.LogWarning("Please select a track first！");
            return;
        }

        // 保存选择的曲目信息
        MusicSelectData.selectedSong = currentSelected.songData;

        // 进入游戏场景
        SceneManager.LoadScene("MusicPlayScene");
    }

}
