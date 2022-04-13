using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static bool RateLimiter(int _frequency)
    {
        if (Time.frameCount % _frequency == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
