using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simply a text ui to show whose turn it is
/// </summary>
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
