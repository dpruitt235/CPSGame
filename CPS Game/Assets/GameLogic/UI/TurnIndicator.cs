using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicator : MonoBehaviour
{
    private GameController gameController;
    private Text label;

    private void Start()
    {
        this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        this.label = GetComponentInChildren<Text>();
    }


    private void Update ()
    {
        this.label.text = this.gameController.GameState.ToString();
	}
}
