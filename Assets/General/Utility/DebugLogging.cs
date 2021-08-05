using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugLogging
{
    public static void DebugLog(object[] input)
    {
        foreach(object @object in input)
            Debug.Log(@object.ToString());
    }
}
