using UnityEngine;
using UnityEngine.UI;

public class AttackerUI : MonoBehaviour
{
    private GameController gameController;
    private Text panelText;

    private void Start()
    {
        this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        this.panelText = GameObject.FindGameObjectWithTag("Attacker").GetComponent<Text>();
    }

    /// <summary>
    /// Displays how many attacks the attacker has left
    /// </summary>
    private void Update()
    {
        this.panelText.text = "Available Attacks: " + this.gameController.NumAvailableAttacks;
    }
}
