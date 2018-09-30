using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Oracle has two valuations that, each can point at a module.  The Oracle uses these valuations to fix modules.
/// </summary>
public class Oracle : MonoBehaviour
{
    private Valuation firstValuation, secondValuation;

    private GameController gameController;

    private void Awake()
    {
        var vals = this.GetComponentsInChildren<Valuation>();
        this.firstValuation = vals[0];
        this.secondValuation = vals[1];

        this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// Applies a rule between the two valuations, if successfull, it will fix the modules between the valuations.
    /// </summary>
    public void ApplyRule()
    {
        if (!this.firstValuation || !this.secondValuation)
        {
            Debug.Log("must set both valuations");
            return;
        }

        Module firstModule, secondModule;
        if (this.firstValuation.CurrentSelection < this.secondValuation.CurrentSelection)
        {
            firstModule = this.firstValuation.CurrentSelection;
            secondModule = this.secondValuation.CurrentSelection;
        }
        else
        {
            firstModule = this.secondValuation.CurrentSelection;
            secondModule = this.firstValuation.CurrentSelection;
        }

        //Successful attack if all modules between the two modules are attacked
        bool successfulDefense = true;
        var mods = new List<Module>();
        if (!firstModule.Attacked && !secondModule.Attacked)
        {
            var currModule = secondModule.PreviousModule;
            while (currModule != firstModule)
            {
                if (!currModule.Attacked)
                {
                    successfulDefense = false;
                    break;
                }
                else {
                    mods.Add(currModule);
                }

                currModule = currModule.PreviousModule;
            }
        }
        else
        {
            successfulDefense = false;
        }

        if (successfulDefense)
        {
            mods.ForEach(m => m.Fix());
        }
    }
}
