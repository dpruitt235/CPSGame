using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {
    public Text VictorTextbox;
    public Text ReservoirFillTextbox;

    // Use this for initialization
    void Start () {
		if (Results.ReservoirFill >= Results.ReservoirLimit)
        {
            VictorTextbox.color = new Color(0.45f, 0.75f, 1f);
            VictorTextbox.text = "Defender Wins!";
        }
        ReservoirFillTextbox.text = "Reservoir Fill: " + Results.ReservoirFill + "/" + Results.ReservoirLimit;
    }
}
