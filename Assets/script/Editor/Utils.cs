using UnityEditor;
using UnityEngine;
using Workflow;
using Workflow.Impl;

namespace GoFire
{
    public class Utils
    {
        [MenuItem("GoFire/GenConfig")]
        public static void GenConfig()
        {
            string genPath = "../GoFireDesign/gen_config.bat";
            var code = new NodeContainer()
                .AddNode(new CallProgram(genPath, ""))
                .Execute(null);
        }
    }
}