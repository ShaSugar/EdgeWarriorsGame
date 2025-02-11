using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnA.Base;
using UnityEngine;
using UIModuleBase = UnA.Base.UIModuleBase;
namespace UnA.Manager
{
    public class UIManager : UnitySingleton<UIManager>
    {
        //public static UIManager Instance { get; private set; }
        public GameObject[] ui_list;
        private Dictionary<string, UIModuleBase> uiModules_Dic; //存放ui模块
        private Stack<UIModuleBase> uiModuleBases_Stack;
        public Transform _canvas;

        private Queue<UIModuleBase> modulesQueue = new Queue<UIModuleBase>(); // 保存待初始化的模块队列
        private const int maxModulesPerFrame = 5; // 每帧初始化的模块数

        //private void Awake()
        //{
        //    if (Instance == null)
        //    {
        //        Instance = this;
        //    }
        //}

        public override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            //StartCoroutine(InitializeModules());
        }

        public void AddmodulesQueue(UIModuleBase uIModuleBase)
        {
            modulesQueue.Enqueue(uIModuleBase);
        }

        public void Init()
        {
            uiModules_Dic = new Dictionary<string, UIModuleBase>();
            uiModuleBases_Stack = new Stack<UIModuleBase>();

            foreach (var item in ui_list)
            {
                m_GetUIModule(item.name, item);
            }

            ui_list = null;
        }

        // 控制队列的初始化协程
        public IEnumerator InitializeModules()
        {
            while (modulesQueue.Count > 0)
            {
                int initializedCount = 0;

                // 逐帧初始化指定数量的模块
                while (initializedCount < maxModulesPerFrame && modulesQueue.Count > 0)
                {
                    UIModuleBase module = modulesQueue.Dequeue();
                    yield return module.StartCoroutine(module.AwakeInit());
                    initializedCount++;
                }

                yield return null; 
            }
        }

        public void Clear()
        {
            uiModules_Dic.Clear();
            uiModuleBases_Stack.Clear();
        }


        #region UI Module GameObject


        /// <summary>
        /// 通过名称获取对应UI模块
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UIModuleBase m_GetUIModule(string name,GameObject ui = null)
        {
            UIModuleBase currentModule = null;
            if (!uiModules_Dic.TryGetValue(name, out currentModule))
            {
                currentModule = InstantiateUIModule(ui);
                uiModules_Dic.Add(name, currentModule);

            }
            else if (currentModule == null)
            {
                currentModule = InstantiateUIModule(ui);
                uiModules_Dic[name] = currentModule;
            }
            return currentModule;
        }
        private UIModuleBase InstantiateUIModule(GameObject orefab)
        {
            GameObject currentModule = GameObject.Instantiate(orefab);
            currentModule.transform.SetParent(_canvas, false);
            string name = currentModule.name.Remove(currentModule.name.Length - "(Clone)".Length);
            currentModule.gameObject.name = name;
            var type = Type.GetType($"{name}");
            var moduleBase = (UIModuleBase)currentModule.AddComponent(type);
            return moduleBase;
        }

        #endregion

        #region UI Module Stack

        public UIManager PushUI(string uiModuleName)
        {
            if (uiModuleBases_Stack == null) { return this; }
            UIModuleBase currentModuleBase = m_GetUIModule(uiModuleName);
            if (!uiModuleBases_Stack.Contains(currentModuleBase))
            {
                uiModuleBases_Stack.Push(currentModuleBase);
            }
            currentModuleBase.OnEnter();
            if (uiModuleBases_Stack.Count > 1)
            {
                // 确保上一个模块正确退出
                uiModuleBases_Stack.ElementAt(1).OnExit();
            }
            return this;
        }

        public UIManager PopUI(string uiModuleName)
        {
            UIModuleBase currentModuleBase = m_GetUIModule(uiModuleName);
            if (uiModuleBases_Stack.Count > 1)
            {
                //出栈栈顶元素，执行离开 Pop() 移除并返回顶部对象 即是出栈
                uiModuleBases_Stack.Pop().OnExit();
            }

            //上面出栈后，判断是否还有元素
            if (uiModuleBases_Stack.Count != 0)
            {
                uiModuleBases_Stack.Peek().OnResume();
            }
            currentModuleBase.OnExit();
            return this;
        }

        public void PopUI()
        {
            if (uiModuleBases_Stack == null) { return; }
            if (uiModuleBases_Stack.Count != 0)
            {
                uiModuleBases_Stack.Pop().OnExit();
            }
            else
            {
                Debug.Log("栈中无元素");//栈中无元素
            }

            //上面出栈后，判断是否还有元素
            if (uiModuleBases_Stack.Count != 0)
            {
                //如果有元素返回上一级模块恢复UI点击事件
                //Peek() 返回最顶部对象，但不移除
                uiModuleBases_Stack.Peek().OnResume();
            }
        }

        #endregion

        public UIModuleBase GetModuleBase(string moduleName)
        {
            UIModuleBase currentModule = null;
            if (!uiModules_Dic.TryGetValue(name, out currentModule))
            {
                return null;
            }
            return currentModule;
        }

    }
}

