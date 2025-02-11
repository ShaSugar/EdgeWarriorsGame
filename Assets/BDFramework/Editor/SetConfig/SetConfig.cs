
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetConfig : EditorWindow
{
    private int dataType, dataIndex;
    private int monsterType, index;
    private int attackIndex;
    [MenuItem("BDFramework Tools/修改路径文件点数据")]
    static void Run()
    {
        EditorWindow.GetWindow<SetConfig>();
    }

    void OnGUI()
    {
        GUILayout.Label("先选择 UnitPathConfigData 配置文件，在选择场景游戏对象 PathEditor 包含脚本");
        monsterType = EditorGUILayout.IntField("怪物配置类型:", monsterType);
        index = EditorGUILayout.IntField("限定类型3 数据索引:", index);
        if (GUILayout.Button("开始复制路径数据"))
        {
            Object[] selectedObjects = Selection.objects;

            if (selectedObjects.Length > 0)
            {
                Debug.Log("Start Copy...");

                // 传递选中的对象和整数参数
                CopyPathData(selectedObjects, monsterType, index);

                Debug.Log("End Copy...");

                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("Please select at least one UnitPathConfigData file!");
            }
        }
        attackIndex = EditorGUILayout.IntField("攻击数据索引:", attackIndex);
        if (GUILayout.Button("开始复制攻击点数据"))
        {
            Object[] selectedObjects = Selection.objects;

            if (selectedObjects.Length > 0)
            {
                Debug.Log("Start Copy...");

                // 传递选中的对象和整数参数
                CopyAttackData(selectedObjects, attackIndex);

                Debug.Log("End Copy...");

                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("Please select at least one UnitPathConfigData file!");
            }
        }
        dataType = EditorGUILayout.IntField("1 为初始位置 2 为关卡数据:", dataType);
        dataIndex = EditorGUILayout.IntField("相机数据索引:", dataIndex);
        if (GUILayout.Button("开始复制相机数据"))
        {
            Object[] selectedObjects = Selection.objects;

            if (selectedObjects.Length > 0)
            {
                Debug.Log("Start Copy...");
                CopyCameraData(selectedObjects, dataType, dataIndex);

                Debug.Log("End Copy...");

                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("Please select at least one UnitPathConfigData file!");
            }
        }
        

        // 显示当前选中的对象数量和名称
        GUILayout.Label("选择中的对象数量: " + Selection.objects.Length);

        if (Selection.objects.Length > 0)
        {
            GUILayout.Label("选择对象名称:");
            foreach (Object obj in Selection.objects)
            {
                GUILayout.Label("- " + obj.name);
            }
        }
        else
        {
            GUILayout.Label("没有选择对象");
        }
    }

    void OnSelectionChange()
    {
        this.Repaint();
    }

    

    //复制相机数据
    public static void CopyCameraData(Object[] selectedObjects, int dataType, int dataIndex)
    {
        GameLevelConfigData configData = selectedObjects[0] as GameLevelConfigData;
        if (dataType == 1) //初始位置
        {
            //configData.initSceneCameraPos[dataIndex] = (selectedObjects[1] as GameObject).transform.position;
            //configData.initSceneCameraAngle[dataIndex] = (selectedObjects[1] as GameObject).transform.eulerAngles;
        }
        else if (dataType == 2) //关卡相机位置
        {
            configData.data[dataIndex].cameraPos = (selectedObjects[1] as GameObject).transform.position;
            configData.data[dataIndex].cameraAngle = (selectedObjects[1] as GameObject).transform.eulerAngles;
        }
        // 标记数据已修改，并保存更改
        EditorUtility.SetDirty(configData);
        // 保存所有更改到硬盘
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    //复制路径数据
    public static void CopyPathData(Object[] selectedObjects, int _monsterType, int index)
    {
        Debug.Log(_monsterType);
        if (_monsterType == 1)
        {
            MonsterType1PathConfig configData = selectedObjects[0] as MonsterType1PathConfig;
            for (int i = 1; i < selectedObjects.Length; i++)
            {
                PathEditor pathEditor = (selectedObjects[i] as GameObject).GetComponent<PathEditor>();
                Debug.Log(pathEditor.GetLocalPoint(0) + "    " + pathEditor.gameObject.name);
                configData.pathPosList[0].inStartPosList[i - 1] = pathEditor.GetLocalPoint(0);
                configData.pathPosList[0].outEndPosList[i - 1] = pathEditor.GetLocalPoint(0);
            }
            // 标记数据已修改，并保存更改
            EditorUtility.SetDirty(configData);
        }
        else if (_monsterType == 2)
        {
            MonsterType2PathConfig configData = selectedObjects[0] as MonsterType2PathConfig;
            for (int i = 1; i < selectedObjects.Length; i++)
            {
                PathEditor pathEditor = (selectedObjects[i] as GameObject).GetComponent<PathEditor>();
                configData.pathPosList[i - 1].inPosList = pathEditor.pathPoints.ToArray();
                configData.pathPosList[i - 1].outPosList = pathEditor.Reverse();
            }
            // 标记数据已修改，并保存更改
            EditorUtility.SetDirty(configData);
        }
        else if (_monsterType == 3)
        {
            MonsterType2PathConfig configData = selectedObjects[0] as MonsterType2PathConfig;
            PathEditor pathEditor = (selectedObjects[1] as GameObject).GetComponent<PathEditor>();
            List<Vector3> pathPosList = new List<Vector3>();
            for (int i = 1; i < pathEditor.pathPoints.Count; i++)
            {
                pathPosList.Add(pathEditor.pathPoints[i]);
            }
            configData.pathPosList[index].outPosList = pathPosList.ToArray();

            // 标记数据已修改，并保存更改
            EditorUtility.SetDirty(configData);
        }
        // 保存所有更改到硬盘
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    //复制攻击数据
    public static void CopyAttackData(Object[] selectedObjects, int attackIndex)
    {
        GameLevelConfigData configData = selectedObjects[0] as GameLevelConfigData;
        for (int i = 1; i < selectedObjects.Length; i++)
        {
            PathEditor pathEditor = (selectedObjects[i] as GameObject).GetComponent<PathEditor>();
            Debug.Log(pathEditor.pathPoints.Count - 1 + "    " + pathEditor.gameObject.name);
            configData.data[attackIndex].attackPointList[i - 1] = pathEditor.GetLocalPoint(pathEditor.pathPoints.Count - 1);
        }
        // 标记数据已修改，并保存更改
        EditorUtility.SetDirty(configData);
        // 保存所有更改到硬盘
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
