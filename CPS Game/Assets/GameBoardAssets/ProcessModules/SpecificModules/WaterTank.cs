using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : ProcessModule {

    public int capacity = 5;
    public int currentLevel = 0;
    public int overPressure = 0;
    public int overPressureCapacity = 3;

	// Use this for initialization
	void Start () {
        this.displayFields.Add("currentLevel");
        this.displayFields.Add("capacity");
        this.displayFields.Add("overPressure");
	}
}
