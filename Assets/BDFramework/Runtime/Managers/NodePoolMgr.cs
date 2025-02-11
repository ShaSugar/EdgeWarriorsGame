using System.Collections.Generic;
using UnityEngine;


public class NodePoolMgr : UnitySingleton<NodePoolMgr>
{
    Transform nodePoolRoot;
    Dictionary<string, Transform> nodePoolMaps;
    Transform tempItemRoot;

    public void Init()
    {
        this.nodePoolMaps = new Dictionary<string, Transform>();
        this.nodePoolRoot = this.transform.Find("NodePoolRoot");
        if (this.nodePoolRoot == null)
        {
            this.nodePoolRoot = new GameObject().transform;
            this.nodePoolRoot.SetParent(this.transform, false);
            this.nodePoolRoot.localPosition = Vector3.zero;
            this.nodePoolRoot.gameObject.name = "NodePoolRoot";
        }

        this.nodePoolRoot.gameObject.SetActive(false);

        this.tempItemRoot = this.transform.Find("TempItemRoot");
        if (this.tempItemRoot == null)
        {
            this.tempItemRoot = new GameObject().transform;
            this.tempItemRoot.SetParent(this.nodePoolRoot, false);
            this.tempItemRoot.localPosition = Vector3.zero;
            this.tempItemRoot.gameObject.name = "TempItemRoot";
        }
        this.tempItemRoot.gameObject.SetActive(false);
    }

    public void AddNodePool(string assetPrefabPath, int count = 0)
    {
        if (this.nodePoolMaps.ContainsKey(assetPrefabPath))
            return;

        Transform typeNodeRoot = new GameObject().transform;
        typeNodeRoot.SetParent(this.nodePoolRoot, false);
        typeNodeRoot.gameObject.SetActive(false);
        typeNodeRoot.gameObject.name = assetPrefabPath;
        typeNodeRoot.gameObject.SetActive(false);

        this.nodePoolMaps.Add(assetPrefabPath, typeNodeRoot);

        if (count <= 0)
            return;

        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(assetPrefabPath);
        for (var i = 0; i < count; i++)
        {
            GameObject item = GameObject.Instantiate(nodePrefab, typeNodeRoot, false);
            item.transform.localPosition = Vector3.zero;
        }
    }

    public void ClearNodePool(string assetPrefabPath, int residueCount = 0)
    {
        if (!this.nodePoolMaps.TryGetValue(assetPrefabPath, out Transform typeNodeRoot))
            return;

        if (typeNodeRoot == null)
            return;

        residueCount = (residueCount < 0) ? 0 : residueCount;
        if (typeNodeRoot.childCount <= residueCount)
            return;

        int count = typeNodeRoot.childCount - residueCount;
        for (var i = 0; i < count; i++)
        {
            GameObject.DestroyImmediate(typeNodeRoot.GetChild(0));
        }
    }
    public GameObject Get(string assetPrefabPath)
    {
        if (!this.nodePoolMaps.TryGetValue(assetPrefabPath, out Transform typeNodeRoot))
            return null;

        if (typeNodeRoot == null)
            return null;

        GameObject item;
        if (typeNodeRoot.childCount <= 0)
        {
            var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(assetPrefabPath);
            item = GameObject.Instantiate(nodePrefab, this.tempItemRoot, false);
            item.transform.localPosition = Vector3.zero;
        }
        else
        {
            item = typeNodeRoot.GetChild(0).gameObject;
            item.transform.SetParent(this.tempItemRoot, false);
            item.transform.localPosition = Vector3.zero;
        }
        return item;

    }

    public void Recycle(string assetPrefabPath, GameObject obj)
    {
        if (!this.nodePoolMaps.TryGetValue(assetPrefabPath, out Transform typeNodeRoot))
            return;

        obj.transform.SetParent(typeNodeRoot, false);
        obj.transform.localPosition = Vector3.zero;
    }
}
