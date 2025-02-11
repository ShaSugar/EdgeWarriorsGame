using UnityEngine;
using UnityEngine.U2D;


public class ResMgr : UnitySingleton<ResMgr>
{
    public void Init()
    {
    }
        
        
    public T LoadAssetSync<T>(string assetPath, string packageName = null) where T : Object
    {
        return  Resources.Load<T>(assetPath);
    }
    public ResourceRequest LoadAssetASync<T>(string assetPath, string packageName = null) where T : Object
    {
        return  Resources.LoadAsync<T>(assetPath);
    }
}
