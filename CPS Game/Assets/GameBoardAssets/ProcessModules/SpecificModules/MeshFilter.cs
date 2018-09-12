using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFilter : ProcessModule {

    public int dirt = 0;
    public int dirtLimit = 10;
    public bool waterFlow = false;

	// Use this for initialization
	void Start () {
        this.displayFields.Add("dirt");
        this.displayFields.Add("dirtLimit");
        this.displayFields.Add("waterFlow");
	}
}
