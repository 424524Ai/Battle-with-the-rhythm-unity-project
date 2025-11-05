using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaneController : MonoBehaviour
{
    public Transform topLane;    // 上轨道位置
    public Transform bottomLane; // 下轨道位置

    private bool isOnTopLane = false;

    void Start()
    {
        // 初始在下轨道
        SetLanePosition(bottomLane);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isOnTopLane)
        {
            SetLanePosition(topLane);
            isOnTopLane = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && isOnTopLane)
        {
            SetLanePosition(bottomLane);
            isOnTopLane = false;
        }
    }

    void SetLanePosition(Transform lane)
    {
        Vector3 newPos = transform.position;
        newPos.y = lane.position.y;  // 只改y
        transform.position = newPos;
    }
}
