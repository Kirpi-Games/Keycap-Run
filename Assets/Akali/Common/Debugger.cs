using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Akali.Common
{
    public static class Debugger
    {
        [Conditional("UNITY_EDITOR")] public static void Log(object message) { Debug.Log(message); }

        [Conditional("UNITY_EDITOR")] public static void LogWarning(object message) { Debug.LogWarning(message); }

        [Conditional("UNITY_EDITOR")] public static void LogError(object message) { Debug.LogError(message); }

        [Conditional("UNITY_EDITOR")] public static void LogException(System.Exception exception) { Debug.LogException(exception); }
    }
}