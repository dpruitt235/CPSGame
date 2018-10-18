using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Oracle has two valuations that, each can point at a module.  The Oracle uses these valuations to fix modules.
/// </summary>
public class Oracle : MonoBehaviour
{
    public bool InputActive = false;
    string message  = "Prevented an attack!";
    bool displayMessage  = true;
    float displayTime = 3.0f;

    private Valuation firstValuation, secondValuation;

    private Vector3 screenPoint, offset;

    private void Awake()
    {
        var vals = this.GetComponentsInChildren<Valuation>();
        this.firstValuation = vals[0];
        this.secondValuation = vals[1];
    }

    private void OnMouseDown()
    {
        if (InputActive)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseDrag()
    {
        if (InputActive)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = cursorPosition;
            this.firstValuation.UpdateLine();
            this.secondValuation.UpdateLine();
        }
    }

    /// <summary>
    /// Applies a rule between the two valuations, if successfull, it will fix the modules between the valuations.
    /// </summary>
    public void ApplyRule()
    {
        if (this.firstValuation.CurrentSelection == null || this.secondValuation.CurrentSelection == null)
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
            GUI.Label(new Rect(Screen.width * 0.5f - 50f, Screen.height * 0.5f - 10f, 100f, 20f), message);

        }
    }
}
