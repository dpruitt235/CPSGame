using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// The Oracle has two valuations that, each can point at a module.  The Oracle uses these valuations to fix modules.
/// </summary>
public class Oracle : MonoBehaviour
{
    public bool InputActive = false;
    public string messageText = "Stopped an attack!";

    public Plane MovementPlane;

    public GameObject OraclePopupPrefab;

    private Valuation firstValuation, secondValuation;
    
    private void Awake()
    {
        var vals = this.GetComponentsInChildren<Valuation>();
        this.firstValuation = vals[0];
        this.secondValuation = vals[1];
    }

    private void Start()
    {
        this.MovementPlane = new Plane(Vector3.up, this.transform.position);
    }
    
    private void OnMouseDrag()
    {
        if (InputActive)
        {
            //Shoot a raycast to the x-z plane that the owl resides to get the location to move to
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;

            if (this.MovementPlane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                Vector3 hitPoint = ray.GetPoint(enter);

                //Move your cube GameObject to the point where you clicked
                this.transform.position = hitPoint;
            }

            //Update the lines that come from the valuations
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
            return;
        }

        //used to decide which to fix on
        bool firstVal = false; //false = first  true = second
        
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
            firstVal = true;
        }

        firstValuation.RuleIndicator.text = "RULE BROKEN";
        secondValuation.RuleIndicator.text = "RULE BROKEN";

        if (!this.ModuleMatchesExpected(firstModule, firstValuation))
        {
            this.firstValuation.RuleIndicator.gameObject.SetActive(true);
            if(firstVal)
                this.FixAttackedModule(firstModule, secondModule, firstValuation);
        }
        else
        {
            this.firstValuation.RuleIndicator.gameObject.SetActive(false);
        }

        if (!this.ModuleMatchesExpected(secondModule, secondValuation))
        {
            this.secondValuation.RuleIndicator.gameObject.SetActive(true);
            if(!firstVal)
                this.FixAttackedModule(firstModule, secondModule, secondValuation);
        }
        else
        {
            this.secondValuation.RuleIndicator.gameObject.SetActive(false);
        }

        ////Successful attack if all modules between the two modules are attacked
        //bool successfulDefense = true;
        //var mods = new List<Module>();
        //if (!firstModule.Attacked && !secondModule.Attacked)
        //{
        //    var currModule = secondModule.PreviousModule;
        //    while (currModule != firstModule)
        //    {
        //        if (!currModule.Attacked)
        //        {
        //            successfulDefense = false;
        //            break;
        //        }
        //        else {
        //            mods.Add(currModule);
        //        }

        //        currModule = currModule.PreviousModule;
        //    }
        //}
        //else
        //{
        //    successfulDefense = false;
        //}

        //if (successfulDefense)
        //{
        //    mods.ForEach(m => m.Fix());
        //    if(FloatingTextPreFab !=null)
        //        ShowFloatingText(messageText);
        //}
    }

    private bool ModuleMatchesExpected(Module m, Valuation v)
    {
        if ((m.Water != null) == (v.dropdowns[0].value == 0))
        {
            if (m.Water == null) return true;

            if ((m.Water.purity[0]) == (v.dropdowns[1].value == 0))
            {
                if ((m.Water.purity[1]) == (v.dropdowns[2].value == 0))
                {
                    if ((m.Water.purity[2]) == (v.dropdowns[3].value == 0))
                    {
                        
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Fixes a module if rules have caught an error. Only fixes if in span of 3 modules.
    /// </summary>
    private void FixAttackedModule(Module first, Module second, Valuation val)
    {
        Module ToFix;

        if (first == second)
        {
            return;
        }
        
        ToFix = second.PreviousModule;
        if(ToFix.PreviousModule != null && ToFix.PreviousModule == first)
        {
            if(ToFix.Attacked)
            {
                val.RuleIndicator.text = "FIXED ATTACK";
            }
            ToFix.Fix();
        }
        else
        {
            return;
        }
    }
    }
