using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // enemy speed
    public float speed = 5f;

    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 后续可加入perfect great miss判断
            JudgeHit();
        }
    }

    void JudgeHit()
    {
        float distance = Mathf.Abs(transform.position.x - player.position.x);
        string result;

        if(distance <= 0.5f)
        {
            result = "Perfect";
            Debug.Log("Perfect!");
            GameResultData.perfectCount++;
            GameResultData.score += 300;
        }
        else if(distance <= 0.8f)
        {
            Debug.Log("Great!");
            result = "Great";
            GameResultData.greatCount++;
            GameResultData.score += 100;
        }
        else
        {
            Debug.Log("Miss!");
            result = "Miss";
            GameResultData.missCount++;
        }

        Debug.Log(result);
        HitEffectManager.Instance.ShowHitResult(result);
        Destroy(gameObject);
    }
}
