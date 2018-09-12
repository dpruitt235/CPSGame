using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReservoir : ProcessModule {

	public int waterLevel = 5;
	public int capacity = 10;
	private int outFlow = 1;

	private void Start() {
		//specify which fields to display on the UI
		this.displayFields.Add("waterLevel");
		this.displayFields.Add("capacity");
		this.displayFields.Add("outFlow");
	}
}