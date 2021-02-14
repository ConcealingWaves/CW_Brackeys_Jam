using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MusicUtility
{
    public static float BeatsToSeconds(float beats, float bpm)
    {
        return 60 * beats / bpm;
    }

    public static float SecondsToBeats(float seconds, float bpm)
    {
        return seconds / 60 * bpm;
    }
}
