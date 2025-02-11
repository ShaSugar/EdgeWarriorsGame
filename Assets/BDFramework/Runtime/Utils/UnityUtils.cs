using UnityEngine;


public class UnityUtils
{
    public static bool SphereContain(Vector3 center, float sqrR, Vector3 point)
    {
        float dis = Vector3.SqrMagnitude(point - center);

        return dis <= sqrR;
    }

    public static bool BoundContain(Collider c, Vector3 point)
    {
        return c != null && c.bounds.Contains(point);
    }

    public static void DestroyAllChildren(Transform parent)
    {
        int count = parent.childCount;
        for (var i = 0; i < count; i++)
        {
            Transform child = parent.GetChild(0);
            Object.DestroyImmediate(child.gameObject);
            count--;
        }
    }

    public static Vector3 StringToVector3(string vecStr, Vector3 defaultValue)
    {
        Vector3 value = defaultValue;
        string[] values = vecStr.Split(',');
        if (values.Length != 3)
            return value;

        try
        {
            value.x = float.Parse(values[0]);
            value.y = float.Parse(values[1]);
            value.z = float.Parse(values[2]);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

        return value;
    }
}
