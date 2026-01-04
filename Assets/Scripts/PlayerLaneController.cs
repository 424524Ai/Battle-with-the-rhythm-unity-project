using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaneController : MonoBehaviour
{
    public Transform topLane;    // top lane position
    public Transform bottomLane; // bottom lane position

    private bool isOnTopLane = false;

    void Start()
    {
        // starts at bottom lane
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
        newPos.y = lane.position.y;  // only change y
        transform.position = newPos;
    }
}
