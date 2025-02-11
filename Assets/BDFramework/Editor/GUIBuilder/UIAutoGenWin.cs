using UnityEditor;
using UnityEngine;


public class UIAutoGenWin : EditorWindow
{
    [MenuItem("BDFramework Tools/生成UICtrl控制代码")]
    static void Run()
    {
        EditorWindow.GetWindow<UIAutoGenWin>();
    }

    void OnGUI()
    {
        GUILayout.Label("选择一个UI 视图根节点");
        if (GUILayout.Button("生成代码"))
        {
            if (Selection.activeGameObject != null)
            {
                Debug.Log("开始生成...");
                CreateUISourceUtil.CreatUISourceFile(Selection.activeGameObject);
                Debug.Log("生成结束");

                AssetDatabase.Refresh();
            }

        }

        GUILayout.Label(Selection.activeGameObject != null ? Selection.activeGameObject.name : "没有选中的UI节点，无法生成");
    }

    void OnSelectionChange()
    {
        this.Repaint();
    }
}
