using UnityEngine;
using System.Collections;

public class DoorHori : MonoBehaviour {

	public int levelId; //对应关卡id
    public float delayOpen; //延时打开门
    public float delayClose; //延时关闭门

    public bool isSecondaryOpening; //是否二次开启
    public int SecondaryLevelId; //二次关卡id
    public float SecondarydelayOpen; //二次延时打开门

    public float translateValue;
	public float easeTime;
	public OTween.EaseType ease;

	
	private Vector3 StartlocalPos;
	private Vector3 endlocalPos;

    private void Start(){
		StartlocalPos = transform.localPosition;	
		gameObject.isStatic = false;
        //GameLevel.FinishedEvent 关卡完成，把门关了。传输 level 关卡索引
        EventMgr.Instance.AddListener(GameLevel.FinishedEvent, FinishedEvent);
        // 关卡开始，把门开了
        EventMgr.Instance.AddListener(GameLevel.GameLevelStartEvent, GameLevelStartEvent);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveListener(GameLevel.FinishedEvent, FinishedEvent);
        // 关卡开始，把门开了
        EventMgr.Instance.RemoveListener(GameLevel.GameLevelStartEvent, GameLevelStartEvent);
    }

    public void FinishedEvent(string _,object udata)
    {
        if ((int)udata != levelId) { return; }
        EndOpen();
    }
    public void GameLevelStartEvent(string _, object udata)
    {
        var data = (int[])udata;
        if (data[data.Length - 1] != 2) { return; }
        if (data[0] != levelId)
        {
            if (isSecondaryOpening)
            {
                if (data[0] == SecondaryLevelId)
                {
                    if (SecondarydelayOpen == 0)
                    {
                        OpenDoor();
                    }
                    else
                    {
                        TimerMgr.Instance.ScheduleOnce((obj) =>
                        {
                            OpenDoor();

                        }, SecondarydelayOpen);
                    }
                }
            }
            return;
        }
        else
        {
            if (delayOpen == 0)
            {
                OpenDoor();
            }
            else
            {
                TimerMgr.Instance.ScheduleOnce((obj) =>
                {
                    OpenDoor();

                }, delayOpen);
            }
        }
    }

    public void OpenDoor(){
        //OTween.ValueTo( gameObject,ease,0.0f,-translateValue,easeTime,0.0f,"StartOpen","UpdateOpenDoor","EndOpen");
        OTween.ValueTo(gameObject, ease, 0.0f, -translateValue, easeTime, 0.0f, "StartOpen", "UpdateOpenDoor");
        GetComponent<AudioSource>().Play();
	}
	
	private void UpdateOpenDoor(float f){		
		Vector3 pos = transform.TransformDirection( new Vector3( 1,0,0));
		transform.localPosition = StartlocalPos + pos*f;
		
	}

	private void UpdateCloseDoor(float f){		
		Vector3 pos = transform.TransformDirection( new Vector3( -f,0,0)) ;
		
		transform.localPosition = endlocalPos-pos;
		
	}
	
	private void EndOpen(){
		endlocalPos = transform.localPosition ;
		StartCoroutine( WaitToClose());
	}
	
	private IEnumerator WaitToClose(){
		
		yield return new WaitForSeconds(delayClose);
		//OTween.ValueTo( gameObject,ease,0.0f,translateValue,easeTime,0.0f,"StartClose","UpdateCloseDoor","EndClose");
        OTween.ValueTo(gameObject, ease, 0.0f, translateValue, easeTime, 0.0f, "StartClose", "UpdateCloseDoor");
        GetComponent<AudioSource>().Play();
	}
}
