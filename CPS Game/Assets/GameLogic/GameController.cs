using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text TurnCounter;
    public Text ReservoirCounter;

    public GameObject ScreenCover;
    public GameObject GameUI;
    public Text TurnText;

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
        TurnText.gameObject.SetActive(false);
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
            TurnText.text = "Defender's Turn";
            TurnText.color = new Color(0, .5F, 1F);
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

            if (++Turn >= TurnLimit)
            {
                Results.ReservoirFill = Reservoir.Fill;
                this.SceneLoader.LoadNextScene();
            }
            ReservoirCounter.text = Reservoir.Fill.ToString();
            TurnCounter.text = "Turn: " + (Turn+1);
            TurnText.text = "Attacker's Turn";
            TurnText.color = new Color(1F, 0, 0);
        }

        StartCoroutine(WaitForClick());
    }

    IEnumerator WaitForClick()
    {
        ScreenCover.transform.localPosition -= Vector3.up * 15;
        GameUI.SetActive(false);
        TurnText.gameObject.SetActive(true);

        yield return new WaitWhile(() => !Input.GetMouseButtonDown(0));

        TurnText.gameObject.SetActive(false);
        GameUI.SetActive(true);
        ScreenCover.transform.localPosition += Vector3.up * 15;
    }
}

