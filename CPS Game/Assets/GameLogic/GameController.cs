using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameState GameState = GameState.AttackerTurn;

    private void Awake()
    {
        this.WaterFlowController.StartWaterFlow(0.1f);
    }

    public void EndTurn()
    {
        if (this.GameState == GameState.AttackerTurn)
        {
            this.GameState = GameState.DefenderTurn;
            this.AttackerUI.SetActive(false);
        }
        else
        {
            this.GameState = GameState.AttackerTurn;
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
