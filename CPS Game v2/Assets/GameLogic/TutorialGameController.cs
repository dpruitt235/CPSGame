using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGameController : GameController
{
    //public Text TutorialText;
    private int stage;
    private string[] messages = {
            "Welcome to the CPS game Tutorial.\n" +
            "Press Continue when you are ready to begin!",

            "In this game there are two players an attacker and a defender." +
            "The goal of the attacker is stop the flow of water and the goal" +
            " of the defender is to keep this from happening",

            "Right now you are looking at the game from the attackers point of view." +
            "Click on a Water Filter to stop the flow of water, the press continue",

            "Good Job!  Now Click Next Turn to see this from the Defender's Point of View",

            "Normally as the Defender you would have no idea what the Attacker's actions are" +
            " but since your playing both sides you know where the attack is",

            "You can Defend the attack by dragging the oracles tether on the left hand of the screen" +
            " onto the sensors adjacent of the attacked module. Like this!  Now End your Turn",

            "You can now see from the attackers point of view that their attack is gone and has been stopped" +
            " by the defender"
    };

    private bool EndTurnAvailable;

    protected new void Start () {
        stage = 0;
        EndTurnAvailable = false;
        UpdateTutorialText();
        base.Start();
    }


    public void UpdateTutorialText()
    {
        //TutorialText.text = messages[stage];
    }


    /*public void TutorialStep()
    {
        if(stage <= 1)
        {
            stage++;
        } else if(stage == 2)
        {
            Module attackedModule = AttackedModule();
            if(attackedModule != null)
            {
                stage++;
                EndTurnAvailable = true;
            }

        } else if(stage == 4)
        {
            stage++;
            Module attackedModule = AttackedModule();
            oracles[0].SetValuation(attackedModule.PreviousModule, NextModule(attackedModule));
            EndTurnAvailable = true;
        }
        UpdateTutorialText();
    }

    new void EndTurn()
    {
        if (EndTurnAvailable)
        {
            if(stage == 3)
            {
                stage++;
                EndTurnAvailable = false;
            }
            if(stage == 5)
            {
                stage++;
            }
            base.EndTurn();
            UpdateTutorialText();
        }
    }

    //Will not return a module that is a pump
    Module AttackedModule()
    {
        Module CurrentModule = Reservoir.PreviousModule.PreviousModule; //Skip Pumps
        while (CurrentModule != null && !(CurrentModule.Attacked && (CurrentModule.IsPump() || CurrentModule.IsFilter())))
        {
            CurrentModule = CurrentModule.PreviousModule.PreviousModule; //Skip Pumps
        }
        return CurrentModule;
    }

    Module NextModule(Module module)
    {
        Module CurrentModule = Reservoir;
        while (CurrentModule != null)
        {
            if (CurrentModule.PreviousModule == module)
            {
                return CurrentModule;
            }
            CurrentModule = CurrentModule.PreviousModule;
        }
        return null;
    }*/
}
