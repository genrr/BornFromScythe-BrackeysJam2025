using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class States
{   
    public static bool deathRollFailed = false;
    public static bool debugMode = true;

    public static bool MenuOpen = false;
    public static bool gameRunning = true;

    public static Dictionary<string, bool> levelInitStatus = new Dictionary<string, bool>();

    public static List<string> initializedLevels = new List<string>();

    public static bool initializedLevelsFileInitialized = false;
}
