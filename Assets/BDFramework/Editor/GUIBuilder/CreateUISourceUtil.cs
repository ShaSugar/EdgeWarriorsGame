using System.IO;
using UnityEngine;


public static class CreateUISourceUtil
{
    public const string OutPutPath = "/Game/Runtime/UIControllers/";

    //创建UISource文件的函数
    public static void CreatUISourceFile(GameObject selectGameObject)
    {
        string gameObjectName = selectGameObject.name;
        string className = gameObjectName + "_UICtrl";

        var filePath = $"{Application.dataPath}{OutPutPath}{className}.cs";
        if (File.Exists(filePath))
        {
            return;
        }

        var sw = new StreamWriter(filePath);
        sw.WriteLine(
            "using UnityEngine;\r\nusing System.Collections;\r\nusing UnityEngine.UI;\r\nusing System.Collections.Generic;\r\n");

        sw.WriteLine($"public class " + className + " : UICtrl \r\n{\r\n");
        sw.WriteLine("\t" + "void Start()\r\n\t{");
        sw.WriteLine("\t" + "}" + "\r\n");
        sw.WriteLine("}\r\n");
        sw.Flush();
        sw.Close();

        Debug.Log($"Gen: {filePath}");
    }
}
