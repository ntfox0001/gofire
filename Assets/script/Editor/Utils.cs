using UnityEditor;
using UnityEngine;
using Workflow;
using Workflow.Impl;

namespace GoFire
{
    public class Utils
    {
        [MenuItem("GoFire/查看luban文档")]
        public static void OpenLuBanUrl()
        {
            Application.OpenURL("https://luban.doc.code-philosophy.com/docs/intro");
            // Application.OpenURL("https://github.com/lubanproject/luban");
        }

        [MenuItem("GoFire/查看BulletPro文档")]
        public static void OpenBulletProDoc()
        {
            var path = Application.dataPath + "\\Standard Assets\\BulletPro\\3 - Complete Manual.pdf"; 
            Application.OpenURL(path);
        }
        
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