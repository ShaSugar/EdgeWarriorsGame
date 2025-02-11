#if UNITY_EDITOR
using DG.Tweening;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PathEditor : MonoBehaviour
{
    public List<Vector3> pathPoints = new List<Vector3>();
    public Color lineColor = Color.green;
    public float handleSize = 0.1f; // 控制点的大小
    public int curveResolution = 10; // 曲线分辨率，用于控制曲线平滑度



    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        // 确保有足够的路径点
        if (pathPoints.Count < 2) return;

        // 绘制曲线
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 p0 = (i > 0) ? transform.TransformPoint(pathPoints[i - 1]) : transform.TransformPoint(pathPoints[i]);
            Vector3 p1 = transform.TransformPoint(pathPoints[i]);
            Vector3 p2 = transform.TransformPoint(pathPoints[i + 1]);
            Vector3 p3 = (i + 2 < pathPoints.Count) ? transform.TransformPoint(pathPoints[i + 2]) : p2; // 处理最后一个点

            DrawCatmullRomCurve(p0, p1, p2, p3);
        }

        // 显示每个路径点的坐标
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Vector3 worldPoint = transform.TransformPoint(pathPoints[i]);
            Gizmos.DrawSphere(worldPoint, handleSize);
            //Handles.Label(worldPoint + Vector3.up * 0.2f, $"    {name} Point {i}: {pathPoints[i]}  "); // 仅显示本地坐标
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 允许选中路径点进行编辑
        for (int i = 0; i < pathPoints.Count; i++)
        {
            // 使用本地坐标处理
            Vector3 localPoint = pathPoints[i];
            Vector3 worldPoint = transform.TransformPoint(localPoint);
            Vector3 newPoint = Handles.PositionHandle(worldPoint, Quaternion.identity);

            // 仅更新本地坐标
            if (newPoint != worldPoint)
            {
                pathPoints[i] = transform.InverseTransformPoint(newPoint); // 更新为本地坐标
            }
        }
    }

    // 绘制 Catmull-Rom 曲线
    private void DrawCatmullRomCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 previousPoint = p1; // 初始化为 p1
        for (int j = 1; j <= curveResolution; j++)
        {
            float t = j / (float)curveResolution; // 从0到1的插值
            Vector3 curvePoint = CatmullRom(p0, p1, p2, p3, t);

            // 绘制线段
            Gizmos.DrawLine(previousPoint, curvePoint);

            // 更新previousPoint
            previousPoint = curvePoint;
        }
    }

    // Catmull-Rom 曲线公式
    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }

    // 添加路径点，确保不重复添加
    public void AddPathPoint(Vector3 localPosition)
    {
        // 检查是否已存在相同的路径点
        if (!pathPoints.Contains(localPosition))
        {
            pathPoints.Add(localPosition); // 仅添加本地坐标路径点
        }
    }

    public Vector3 GetLocalPoint(int index)
    {
        return pathPoints[index];
    }

    public Vector3[] Reverse()
    {
        List<Vector3> reverse = new List<Vector3>();
        reverse.Clear();
        foreach (Vector3 v in pathPoints)
        {
            reverse.Add(v);
        }
        reverse.Reverse();
        return reverse.ToArray();
    }
}
#endif

