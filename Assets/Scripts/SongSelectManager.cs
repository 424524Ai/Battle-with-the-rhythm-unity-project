using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SongSelectManager : MonoBehaviour
{
    public List<SongData> allSongs;        // drag in all available songs
    public Transform contentParent;        // Content of ScrollView 
    public GameObject songButtonPrefab;    // song button prefab
    public Button playButton;              

    private SongSelectButton currentSelected;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise ScrollView
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

        // save selected music data
        MusicSelectData.selectedSong = currentSelected.songData;

        // enter music play scene 
        SceneManager.LoadScene("MusicPlayScene");
    }

}
