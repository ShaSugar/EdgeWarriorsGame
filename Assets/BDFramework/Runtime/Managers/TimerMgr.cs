using System;
using System.Collections.Generic;
using UnityEngine;


public class TimerMgr : UnitySingleton<TimerMgr>
{
    class TimerNode
    {
        public int timerID;
        public Action<object> OnTimer;
        public object param;
        public int repeat;
        public float nextTriggerTime;
        public float curTime;
        public float interval;
        public bool isCancel;
    }

    Dictionary<int, TimerNode> timerNodes = new Dictionary<int, TimerNode>();
    Dictionary<int, TimerNode> preAddTimerNodes = new Dictionary<int, TimerNode>();
    int autoTimerID = 1;
    List<int> removeTimerQueue = new List<int>();

    public void Init()
    {
        // this.timerNodes = new Dictionary<int, TimerNode>();
        // this.preAddTimerNodes = new Dictionary<int, TimerNode>();
        // this.removeTimerQueue = new List<int>();
        // this.autoTimerID = 1;
    }

    void Update()
    {
        if (this.timerNodes == null)
            return;

        if (this.preAddTimerNodes.Count > 0)
        {
            foreach (int key in this.preAddTimerNodes.Keys)
            {
                this.timerNodes.Add(key, this.preAddTimerNodes[key]);
            }
            
            this.preAddTimerNodes.Clear();
        }

        foreach (int key in this.timerNodes.Keys)
        {
            TimerNode timerNode = this.timerNodes[key];
            if (timerNode == null || timerNode.isCancel)
                continue;

            timerNode.curTime += Time.deltaTime;
            if (timerNode.nextTriggerTime > timerNode.curTime)
                continue;

            timerNode.OnTimer?.Invoke(timerNode.param);

            timerNode.nextTriggerTime = timerNode.interval;
            timerNode.curTime = 0;

            if (timerNode.repeat != -1)
                timerNode.repeat--;

            if (timerNode.repeat == 0)
                this.UnSchedule(timerNode.timerID);
        }

        for (var i = 0; i < this.removeTimerQueue.Count; i++)
        {
            if (this.timerNodes.ContainsKey(this.removeTimerQueue[i]))
            {
                this.timerNodes.Remove(this.removeTimerQueue[i]);
            }
        }
        this.removeTimerQueue.Clear();
    }

    public int Schedule(Action<object> onTimer, object param, int repeat, float interval, float delay)
    {
        if (onTimer == null || interval < 0f || delay < 0f)
            return 0;

        int timerID = this.autoTimerID++;
        this.autoTimerID = (this.autoTimerID == 0) ? 1 : this.autoTimerID;

        var timerNode = new TimerNode()
        {
            OnTimer = onTimer,
            param = param,
            repeat = (repeat <= 0) ? -1 : repeat,
            interval = interval,
            curTime = 0,
            nextTriggerTime = delay,
            timerID = timerID,
            isCancel = false
        };
        // this.timerNodes.Add(timerID, timerNode);
        this.preAddTimerNodes.Add(timerID, timerNode);

        return timerID;
    }
    public void UnSchedule(int timerID)
    {
        if (this.preAddTimerNodes.ContainsKey(timerID))
        {
            this.preAddTimerNodes[timerID].OnTimer = null;
            this.preAddTimerNodes.Remove(timerID);
        }
        
        if (!this.timerNodes.TryGetValue(timerID, out TimerNode timerNode))
            return;

        if (timerNode == null)
            return;

        timerNode.isCancel = true;
        timerNode.OnTimer = null;
        this.removeTimerQueue.Add(timerID);
    }

    public int Schedule(Action<object> onTimer, int repeat, float interval, float delay)
    {
        return Schedule(onTimer, null, repeat, interval, delay);
    }
    public int ScheduleOnce(Action<object> onTimer, object param, float delay)
    {
        return Schedule(onTimer, param, 1, 0, delay);
    }
    public int ScheduleOnce(Action<object> onTimer, float delay)
    {
        return Schedule(onTimer, null, 1, 0, delay);
    }
}
