using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResultData
{
    public static int score = 0;
    public static int perfectCount = 0;
    public static int greatCount = 0;
    public static int missCount = 0;
    public static string rank = "C";

    public static void Reset()
    {
        score = 0;
        perfectCount = 0;
        greatCount = 0;
        missCount = 0;
        rank = "C";
    }
}
