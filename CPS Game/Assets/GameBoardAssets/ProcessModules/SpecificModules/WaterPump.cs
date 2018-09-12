using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : ProcessModule {

    public bool pumpingWater = false;
    public bool waterFlowing = false;

	// Use this for initialization
	void Start () {
        this.displayFields.Add("pumpingWater");
	}
	
}
