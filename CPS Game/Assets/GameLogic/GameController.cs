using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls whose turn it is, the actions available to the players, and other game logic
/// </summary>
public class GameController : MonoBehaviour
{
    public WaterFlowController WaterFlowController;

    public GameObject AttackerUI;

    public int NumberOfAttacksLimit = 1;

    private int NumAttacks = 0;
    public int AvailableAttacks {
        get {
            return this.NumberOfAttacksLimit - NumAttacks;
        }
    } 

    public GameState CurrentState = GameState.AttackerTurn;

    private void Awake()
    {
        this.WaterFlowController.StartWaterFlow(0.1f);
    }

    public void EndTurn()
    {
        if (this.CurrentState == GameState.AttackerTurn)
        {
            this.CurrentState = GameState.DefenderTurn;
            this.AttackerUI.SetActive(false);
        }
        else
        {
            this.CurrentState = GameState.AttackerTurn;
            this.AttackerUI.SetActive(true);
        }
    }

    public void RemoveAttack()
    {
        this.NumAttacks--;
    }

    public void AddAttack()
    {
        this.NumAttacks++;
    }
}
