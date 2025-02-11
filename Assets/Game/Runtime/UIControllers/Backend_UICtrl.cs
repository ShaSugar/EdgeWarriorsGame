using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnA.Manager;

public class Backend_UICtrl : UICtrl 
{
	IEnumerator Start()
	{
        UIManager ui = this.AddComponent<UIManager>();
        ui.ui_list = Resources.LoadAll<GameObject>("GUIPrefabs/module");
        yield return null;
        ui._canvas = transform.Find("BackendModuleTest/rightFunctionModule");
        yield return null;
        ui.Init();
        yield return StartCoroutine(ui.InitializeModules());
        transform.Find("BackendModuleTest").GetComponent<BackendPanel>().OpenBackend();
        GetComponent<Canvas>().sortingOrder = 1000;
        transform.parent = null;
        yield return null;
    }

}

