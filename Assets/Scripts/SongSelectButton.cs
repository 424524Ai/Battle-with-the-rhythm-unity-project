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
        // 设置显示内容
        if(coverImage != null) coverImage.sprite = songData.coverImage;
        if(titleText != null) titleText.text = songData.songName;

        // 获取管理器引用
        manager = FindObjectOfType<SongSelectManager>();

        // 初始隐藏选中边框
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
