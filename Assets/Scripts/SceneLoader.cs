using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manage scene change
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 重新加载当前场景（比如 Restart）
    public void ReloadCurrentScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    // 退出游戏（只在打包后有效）
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
