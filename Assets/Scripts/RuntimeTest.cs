using UnityEngine;

public class RuntimeTest
{
    [RuntimeInitializeOnLoadMethod]
    public static void DebugLogOnInitialized_str(string value = "")
    {
        Debug.Log($"Log on RuntimeInitialize {value}");
    }

    [RuntimeInitializeOnLoadMethod]
    public static void DebugLogOnInitialized(int value = 0)
    {
        Debug.Log($"Log on RuntimeInitialize {value}");
    }
}