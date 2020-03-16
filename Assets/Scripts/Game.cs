using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static bool isPaused { get; private set; } = false;

    public static bool Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            return isPaused = false;
        }
        Time.timeScale = 0;
        return isPaused = true;
    }

    public static bool Pause(bool pauseState)
    {
        if (pauseState)
        {
            Time.timeScale = 1;
            return isPaused = false;
        }
        Time.timeScale = 0;
        return isPaused = true;
    }
}
