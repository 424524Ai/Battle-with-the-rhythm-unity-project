using UnityEngine;
using TMPro;
using System.Collections;

public class HitEffectManager : MonoBehaviour
{
    public static HitEffectManager Instance;
    public TextMeshProUGUI hitText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowHitResult(string result)
    {
        StopAllCoroutines(); // 避免多次重叠
        StartCoroutine(ShowHitCoroutine(result));
    }

    private IEnumerator ShowHitCoroutine(string result)
    {
        hitText.text = result;

        switch (result)
        {
            case "Perfect":
                hitText.color = Color.yellow;
                break;
            case "Great":
                hitText.color = Color.cyan;
                break;
            case "Miss":
                hitText.color = Color.red;
                break;
        }

        hitText.alpha = 1f;
        float duration = 0.5f; // 淡出时长
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            hitText.alpha = Mathf.Lerp(1f, 0f, timer / duration);
            yield return null;
        }

        hitText.text = "";
    }
}
