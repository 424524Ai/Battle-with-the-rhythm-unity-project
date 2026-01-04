using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSelectButton : MonoBehaviour
{
    public SongData songData;

    [SerializeField] private Image coverImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image highlightBorder;

    private SongSelectManager manager;

    // Start is called before the first frame update
    void Start()
    {
        // Set display content
        if (coverImage != null) coverImage.sprite = songData.coverImage;
        if(titleText != null) titleText.text = songData.songName;

        // Get manager reference
        manager = FindObjectOfType<SongSelectManager>();

        // Initially hide the selected border
        if (highlightBorder != null)
            highlightBorder.enabled = false;

        Button btn = GetComponent<Button>();
        if (btn != null) btn.onClick.AddListener(OnSelect);
    }

    public void OnSelect()
    {
        manager.SelectSong(this);
    }

    public void SetHighlight(bool on)
    {
        if (highlightBorder != null)
            highlightBorder.enabled = on;
    }
}
