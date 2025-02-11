﻿using UnityEngine;
using System.Collections;

public class HoriDoorManager : MonoBehaviour {

	public DoorHori door1;
	public DoorHori door2;

    private void OnTriggerEnter(Collider other)
    {
        if (door1 != null)
        {
            door1.OpenDoor();
        }

        if (door2 != null)
        {
            door2.OpenDoor();
        }
        Debug.Log($"触发进入:{other.gameObject.name}");
    }
}
