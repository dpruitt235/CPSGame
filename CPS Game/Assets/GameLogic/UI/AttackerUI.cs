using UnityEngine;
using UnityEngine.UI;

public class AttackerUI : MonoBehaviour
{
    private GameController gameController;
    private Text panelText;

    private void Start()
    {
        this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        this.panelText = this.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        this.panelText.text = "Available Attacks: " + this.gameController.AvailableAttacks;
    }
}
