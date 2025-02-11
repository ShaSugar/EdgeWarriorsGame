using System;
using System.Collections.Generic;

namespace UnA
{
     class EventManager01
    {
        /**
         // 准备要注册的事件字典
        Dictionary<string, Delegate> eventListeners = new Dictionary<string, Delegate>
        {
            { "OnPlayerDeath", new Action(()=>{Debug.Log("OnPlayerDeath"); }) },
            { "OnPlayerScored", new Action<int>((data)=>{Debug.Log($"OnPlayerScored:{data}"); }) }
        };
         */
        #region Action Event Manager

        // 使用字典存储事件名称和通用的委托类型
        private static Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();
        private static List<string> registeredEvents = new List<string>(); // 用于记录注册的事件名称

        //注册事件，支持任意数量的参数或不带参数
        public static void RegisterEvent(string eventName, Delegate listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = Delegate.Combine(eventDictionary[eventName], listener);
            }
            else
            {
                eventDictionary[eventName] = listener;
                registeredEvents.Add(eventName); // 记录注册的事件
            }
        }

        //注销事件，支持任意数量的参数或不带参数
        public static void UnregisterEvent(string eventName, Delegate listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = Delegate.Remove(eventDictionary[eventName], listener);
                if (eventDictionary[eventName] == null)
                {
                    eventDictionary.Remove(eventName);
                    registeredEvents.Remove(eventName); // 从已注册的事件中移除
                }
            }
        }

        //触发事件，支持不带参数或动态参数
        public static void TriggerEvent(string eventName, params object[] parameters)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                if (parameters == null || parameters.Length == 0)
                {
                    // 如果没有参数，调用不带参数的事件
                    if (eventDictionary[eventName] is Action callback)
                    {
                        callback.Invoke();
                    }
                }
                else
                {
                    // 动态调用委托，传入参数
                    eventDictionary[eventName]?.DynamicInvoke(parameters);
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning($"事件 {eventName} 未注册！");
            }
        }


        //注销所有事件
        public static void UnregisterAllEvents()
        {
            eventDictionary.Clear();
            registeredEvents.Clear();
        }

        //重新注册所有事件
        public static void ReregisterAllEvents(Dictionary<string, Delegate> allEventListeners)
        {
            // 清空现有事件
            UnregisterAllEvents();

            // 重新注册事件
            foreach (var eventEntry in allEventListeners)
            {
                RegisterEvent(eventEntry.Key, eventEntry.Value);
            }
        }

        //返回已注册的事件名称列表
        public static List<string> GetRegisteredEvents()
        {
            return new List<string>(registeredEvents);
        }

        #endregion
    }
}


