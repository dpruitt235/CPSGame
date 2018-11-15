﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// The Oracle has two valuations that, each can point at a module.  The Oracle uses these valuations to fix modules.
/// </summary>
public class Oracle : MonoBehaviour
{
    public GameObject FloatingTextPreFab;
    public bool InputActive = false;
    public string messageText = "Stopped an attack!";

    public Plane MovementPlane;

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
            if(FloatingTextPreFab !=null)
                ShowFloatingText(messageText);
        }
    }
    void ShowFloatingText(string message)
    {
        var go = Instantiate(FloatingTextPreFab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = message;
    }
}
