using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReservoir : ProcessModule {

	public int Capacity = 10;

	private int fill = 5;

	private void Start() {
		//specify which fields to display on the UI
		this.displayFields.Add("Capacity");
		this.displayFields.Add("fill");
	}
}