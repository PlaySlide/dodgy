﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class googgodwhy : MonoBehaviour {
	public int target = 60;
	// Use this for initialization
	void Start () {
		QualitySettings.vSyncCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != Application.targetFrameRate){
			Application.targetFrameRate = target;
		}
	}
}

public class FrameControl : MonoBehaviour {

	public int target = 60;

	void Start () {
		QualitySettings.vSyncCount = 0;
	}

	void Update () {
		if (target != Application.targetFrameRate){
			Application.targetFrameRate = target;
		}
	}
}