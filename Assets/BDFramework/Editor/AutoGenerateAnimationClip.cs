using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AutoGenerateAnimationClip : Editor
{
    [MenuItem("BDFramework Tools/动画片段处理/导出动画片段")]
    static void AutoGenerateAnimClip()
    {
        Debug.Log("AutoGenerateAnimClip");
        var objs = Selection.gameObjects;
        for (var index = 0; index < objs.Length; index++)
        {
            var obj = objs[index];
            var assetPath = AssetDatabase.GetAssetPath(obj);
            var dictionPath = Path.GetDirectoryName(assetPath) + @"/ExportAnimClip/";
            var fileName = obj.name;
            Debug.Log($"assetPath:{assetPath}, fileName:{fileName}, dictionPath:{dictionPath}");
            
            Object[] clipObjs = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            string animationPath = "";
            AnimationClipSettings setting;
            AnimationClip srcClip;//源动画片段
            AnimationClip newClip;//导出动画片段
            foreach (var clipObj in clipObjs)
            {
                if (clipObj is not AnimationClip clip) continue;
                
                if (!Directory.Exists(dictionPath))
                    Directory.CreateDirectory(dictionPath);
                    
                srcClip = clip;
                newClip = new AnimationClip
                {
                    name = srcClip.name
                };
                setting = AnimationUtility.GetAnimationClipSettings(srcClip);
                AnimationUtility.SetAnimationClipSettings(newClip, setting);
                newClip.frameRate = srcClip.frameRate;
                EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(srcClip);//获取clip的curveBinds
                for (var i = 0; i < curveBindings.Length; i++)
                {
                    AnimationUtility.SetEditorCurve(newClip, curveBindings[i],
                        AnimationUtility.GetEditorCurve(srcClip, curveBindings[i]));// 设置新clip的curve
                }
                newClip.legacy = true;
                
                animationPath = dictionPath + fileName + "_" + newClip.name + ".anim";
                if (File.Exists(animationPath))
                {
                    Debug.LogError($"{animationPath} 已存在，请手动删除后再执行导出！");
                    continue;
                }
                AssetDatabase.CreateAsset(newClip, animationPath);
                    
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                Debug.Log($"导出动画片段:{animationPath} 成功");
            }
        }
        
        
        Debug.Log($"导出动画片段全部完成0.0084036");
    }
    
    
    [MenuItem("BDFramework Tools/动画片段处理/压缩动画片段")]
    static void CompressAnimClip()
    {
        var objs = Selection.objects;
        for (int i = 0; i < objs.Length; i++)
        {
            var obj = objs[i];
            if (obj is AnimationClip)
            {
                OptionalFloatCurves(obj as AnimationClip);
            }
        }
        // 重新保存
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log("压缩动画片段全部完成");
    }

    static string floatFormat = "f4";//精度
    public static void OptionalFloatCurves(AnimationClip activeObject)
    {
        
        var animation_go = activeObject;
 
        var clip = animation_go as AnimationClip;
 
        //获取动画片段的曲线信息
        EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
        AnimationClipCurveData[] curves = new AnimationClipCurveData[curveBindings.Length];
        for (int index = 0; index < curves.Length; ++index)
        {
            curves[index] = new AnimationClipCurveData(curveBindings[index]);
            curves[index].curve = AnimationUtility.GetEditorCurve(clip, curveBindings[index]);
        }
 
        clip.ClearCurves();//清除所有曲线，筛选必须的曲线数据再加入
 
        for (int j = 0; j < curves.Length; j++)
        {
            var curveDate = curves[j];
            var keyFrames = curveDate.curve.keys;//初始数据
 
            List<Keyframe> resultKeyFrames = new List<Keyframe>();//结果数据
 
            int sameKeyCount = 0;//值相同的帧数量，若多于两个，剔除中间关键帧，保留首尾两帧
 
            float currentValue = 0;//当前值
            float currentInTangent = 0;//除value外，in/out tangent也需要判断
            float currnetOutTangent = 0;
 
            Keyframe lasetKey = default;//上一帧的数据，若当真帧值与上一帧不同，则把上一帧数据加入保存
 
            //赋初始值
            if (keyFrames.Length > 0)
            {
                currentValue = float.Parse(keyFrames[0].value.ToString(floatFormat));
                currentInTangent = float.Parse(keyFrames[0].inTangent.ToString(floatFormat));
                currnetOutTangent = float.Parse(keyFrames[0].outTangent.ToString(floatFormat));
 
            }
 
            for (int i = 0; i < keyFrames.Length; i++)
            {
                var key = keyFrames[i];
                //优化精度
                key.value = float.Parse(key.value.ToString(floatFormat));
                key.inTangent = float.Parse(key.inTangent.ToString(floatFormat));
                key.outTangent = float.Parse(key.outTangent.ToString(floatFormat));
                key.inWeight = float.Parse(key.inWeight.ToString(floatFormat));
                key.outWeight = float.Parse(key.outWeight.ToString(floatFormat));
                key.time = float.Parse(key.time.ToString(floatFormat));
                keyFrames[i] = key;
 
                if (i == 0 || i == keyFrames.Length - 1)
                {
                    resultKeyFrames.Add(key);//把首帧和尾帧加入结果列表，防止预制体数据异常导致动画异常（预制体初始scale为0，但是首尾关键帧都为1，此时去除首尾帧会异常）
                }
                else
                {
                    if (currentValue == key.value && currentInTangent == key.inTangent && currnetOutTangent == key.outTangent)//当前帧与上一帧相同
                    {
                        sameKeyCount++;
                    }
                    else//当前帧与上一帧不同
                    {
                        if (sameKeyCount == 0)//匹配到的相同帧数量 == 0，表示，上一帧已经通过以下逻辑添加到列表中了，只需要添加当前帧
                        {
 
                        }
                        else//匹配到的帧数量 ！= 0 ，把相同帧的最后一帧加入列表
                        {
                            resultKeyFrames.Add(lasetKey);
                        }
 
                        //把当前帧加入到列表中
                        resultKeyFrames.Add(key);
                        sameKeyCount = 0;
                        currentValue = float.Parse(key.value.ToString(floatFormat));
                        currentInTangent = float.Parse(key.inTangent.ToString(floatFormat));
                        currnetOutTangent = float.Parse(key.outTangent.ToString(floatFormat));
                    }
                }
                lasetKey = key;
            }
 
            if (resultKeyFrames.Count == 1)//只有一个关键帧，说明动画有问题
            {
 
            }
            else
            {
                //设置曲线
                curveDate.curve.keys = resultKeyFrames.ToArray();
                clip.SetCurve(curveDate.path, curveDate.type, curveDate.propertyName, curveDate.curve);
            }
        }
 
        EditorUtility.SetDirty(clip);
        AssetDatabase.SaveAssets();
        
        Debug.Log($"压缩动画片段:{clip.name} 成功");
    }
}
