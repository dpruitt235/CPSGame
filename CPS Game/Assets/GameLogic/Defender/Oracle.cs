using System.Collections;
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

    private Valuation firstValuation, secondValuation;

    private Vector3 screenPoint, offset;

    private Vector2 minScreen = new Vector2(0, 0);
    private Vector2 maxScreen = new Vector2(Screen.width, Screen.height);

    private int count = 0; // used for testing right mouse clicks

    private void Awake()
    {
        var vals = this.GetComponentsInChildren<Valuation>();
        this.firstValuation = vals[0];
        this.secondValuation = vals[1];
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ShowFloatingText("RIGHT CLICK: " + count);
            this.InputActive = false; // makes the owl unmoveable
        }
        if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            this.InputActive = true; // makes the owl moveable again
        }
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

            Vector2 minPosition = Camera.main.ScreenToWorldPoint(minScreen);
            Vector2 maxPosition = Camera.main.ScreenToWorldPoint(maxScreen);

            //owl screen bounds 
            if ( (cursorPosition.x) < minPosition.x)
            {
                cursorPosition.x = minPosition.x;
            }else if(cursorPosition.x > maxPosition.x)
            {
                cursorPosition.x = maxPosition.x;
            }

            if (cursorPosition.y < minPosition.y)
            {
                cursorPosition.y = minPosition.y;
            }else if(cursorPosition.y > maxPosition.y)
            {
                cursorPosition.y = maxPosition.y;
            }

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
