using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls whose turn it is, the actions available to the players, and other game logic
/// </summary>
public class GameController : MonoBehaviour
{
    public WaterFlowController WaterFlowController;
    public SceneLoader SceneLoader;

    public GameObject OraclePrefab;
    public Vector2 OracleSpawnPoint;

    public GameObject AttackerUI;

    public Reservoir Reservoir;

    public int NumberOfAttacksPerTurn = 1;
    public int NumberOfOracles = 1;
    public int NumAvailableAttacks { get; set; }

    private int Turn = 0;

    public int ReservoirLimit = 10;
    public int TurnLimit = 15;


    public GameState GameState = GameState.AttackerTurn;

    private List<Oracle> oracles;

    private void Awake()
    {
        this.NumAvailableAttacks = this.NumberOfAttacksPerTurn;
        Results.ReservoirLimit = ReservoirLimit;
        this.oracles = new List<Oracle>();
    }

    private void Start()
    {
        for (int i = 0; i < this.NumberOfOracles; i++)
        {
            var newOracle = Instantiate(this.OraclePrefab, new Vector3(this.OracleSpawnPoint.x, this.OracleSpawnPoint.y + (-i * 2), -9), Quaternion.identity);
            oracles.Add(newOracle.GetComponent<Oracle>());
        }
    }

    public void EndTurn()
    {
        if (this.GameState == GameState.AttackerTurn)
        {
            this.oracles.ForEach(o => o.InputActive = true);
            this.GameState = GameState.DefenderTurn;
            this.AttackerUI.SetActive(false);
        }
        else
        {
            this.GameState = GameState.AttackerTurn;
            this.NumAvailableAttacks = this.NumberOfAttacksPerTurn;

            this.AttackerUI.SetActive(true);
            foreach (Oracle o in this.oracles)
            {
                o.InputActive = false;
                o.ApplyRule();
            }

            this.WaterFlowController.TickModules();

            if (Reservoir.Fill >= ReservoirLimit)
            {
                Results.ReservoirFill = Reservoir.Fill;
                this.SceneLoader.LoadNextScene();
            }

            if (++Turn > TurnLimit)
            {
                Results.ReservoirFill = Reservoir.Fill;
                this.SceneLoader.LoadNextScene();
            }
        }
    }
}
