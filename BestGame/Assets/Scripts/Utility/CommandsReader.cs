using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandsReader
{
    public static string Read(TextAsset text)
    {
        string s = text.ToString();
        var replace = s.Replace("\n", " ");
        return replace;
    }
}
