using GoFire;
using UnityEditor;
using UnityEngine;

namespace GoFire
{
    [InitializeOnLoad]
    public static class WorkflowInit
    {
        static WorkflowInit()
        {
            Workflow.Logger.Info = Debug.Log;
            Workflow.Logger.Error = Debug.LogError;
            Workflow.Logger.Warn = Debug.LogWarning;
        }
    }
}