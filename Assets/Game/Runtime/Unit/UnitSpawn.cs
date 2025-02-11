using UnityEngine;

public class UnitSpawn
{
    public UnitSpawnConfigData.UnitSpawnConfig config { get; private set; }

    public bool flag { get; private set; }

    float nextTime;

    int targetCount;
    float endTime;

    int index;

    public void Start(UnitSpawnConfigData.UnitSpawnConfig config)
    {
        Stop();
        this.config = config;
        this.targetCount = this.config.spawnCount <= 0 ? int.MaxValue : this.config.spawnCount;
        this.endTime = this.config.durationTime > 0 ? Time.time + this.config.durationTime : float.MaxValue;
        this.index = 0;
        OpenTimer(this.config.delay);
    }

    public void Update()
    {
        if (!this.flag)
            return;

        if (this.endTime < Time.time)
        {
            Stop();
            return;
        }

        if (Time.time < this.nextTime)
            return;
        
        int spawnCount = Random.Range(this.config.minSpawnCount, this.config.maxSpawnCount + 1);
        if(spawnCount <= 0)
            spawnCount = 1;
        
        for (var i = 0; i < spawnCount; i++)
        {
            SpawnUnit();
        }
        // int unitId = this.config.unitIds[UnityEngine.Random.Range(0, this.config.unitIds.Length)];
        // int[] data = new[]
        // {
        //     this.config.spawnId,
        //     unitId,
        //     this.config.sequence ? this.index : -1
        // };
        // EventMgr.Instance.Emit(UnitSpawnMgr.UnitSpawnEvent, data);
        // this.index++;
        //
        // this.targetCount--;
        // if (this.targetCount <= 0)
        // {
        //     Stop();
        //     return;
        // }

        this.nextTime = Time.time + UnityEngine.Random.Range(this.config.minInterval, this.config.maxInterval);
    }

    void SpawnUnit()
    {
        if (this.targetCount <= 0)
        {
            Stop();
            return;
        }
        
        int unitId = this.config.unitIds[UnityEngine.Random.Range(0, this.config.unitIds.Length)];
        int[] data = new[]
        {
            this.config.spawnId,
            unitId,
            this.config.sequence ? this.index : -1
        };
        EventMgr.Instance.Emit(UnitSpawnMgr.UnitSpawnEvent, data);
        this.index++;

        this.targetCount--;
    }

    public void OpenTimer(float delayTime)
    {
        this.flag = true;
        if (delayTime < 0f)
        {
            this.nextTime = Time.time + UnityEngine.Random.Range(this.config.minInterval, this.config.maxInterval);
        }
        else
        {
            this.nextTime = Time.time + delayTime;
        }
    }

    public void Stop()
    {
        this.flag = false;
    }
}
