using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTankSensor : ProcessModule {

    public int currentState = 0;

	// Use this for initialization
	void Start () {
        this.displayFields.Add("currentState");
	}
	
}
