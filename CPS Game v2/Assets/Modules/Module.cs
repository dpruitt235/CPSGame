
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class with implementation that is common to all modules.  Unless the virtual functions are overriden for custom functionality,
/// the modules will pass water through them iff their corresponding pump is on and they have the capacity to take in more water.
/// </summary>
public abstract class Module : MonoBehaviour
{
    public GameObject popupPrefab;
    public GameObject AttackedIndicator;

    public Module PreviousModule;

    public Pump InFlowingPump;

    public bool Attacked = false;

    public WaterObject Water;

    public virtual int Capacity
    {
        get { return 3; }
    }

    private GameController gameController;

    protected List<string> displayFields;
	private GameObject popupInstance;
	private Text displayTextTitle;
	private Text displayTextContent;

    protected Dropdown[] AttackDropdowns;

    private GameObject attackedIndicatorInstance;
    private Canvas rootCanvas;

    public virtual bool IsFilter()
    {
        return false;
    }

    public virtual bool IsPump()
    {
        return false;
    }

	private void Awake() {
        this.displayFields = new List<string>
        {
            "Attacked",
            "Capacity",
            "Water"
        };

        rootCanvas = (Canvas)FindObjectOfType(typeof(Canvas));

        //Instantiate the popup that displays the display fields
        this.popupInstance = Instantiate (this.popupPrefab, this.popupPrefab.transform.position, this.popupPrefab.transform.rotation);
		this.popupInstance.transform.SetParent(this.rootCanvas.transform, false);
		var texts = this.popupInstance.GetComponentsInChildren<Text>();
		if (texts.Length == 2) {
			this.displayTextContent = texts[1];
			this.displayTextTitle = texts[0];
		}
		this.displayTextTitle.text = this.gameObject.name;

		this.CloseInfoPopup();

        this.attackedIndicatorInstance = Instantiate(this.AttackedIndicator, 
            Camera.main.WorldToScreenPoint(this.transform.position),
            this.AttackedIndicator.transform.rotation);
        this.attackedIndicatorInstance.transform.SetParent(GameObject.FindGameObjectWithTag("Attacker").transform);
        this.attackedIndicatorInstance.SetActive(false);

        this.AttackDropdowns = this.attackedIndicatorInstance.GetComponentsInChildren<Dropdown>();
	}

    protected void Start()
    {
        this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public virtual void Tick()
    {
        if (this.InFlowingPump.On)
        {
            this.OnFlow();
        }

        /*if (Water.Amount > this.Capacity)
        {
            this.OnOverflow();
        }*/

        this.UpdatePopupDisplay();
        if (this.PreviousModule)
            this.PreviousModule.Tick();
    }

    /// <summary>
    /// Only called when the pump is on.  Brings as much water as it can from the previous module into this one.
    /// Override for custom functionality in modules!
    /// </summary>
    protected virtual void OnFlow()
    {
        if (this.PreviousModule && this.Water == null)
        {
            this.Water = this.PreviousModule.Water;
            this.PreviousModule.Water = null;
        }
    }

    /// <summary>
    /// Override to specify how the module behaves when fill exceeds capacity.  (Can only occur if OnFlow is overritten)
    /// </summary>
    protected virtual void OnOverflow()
    {

    }

    /// <summary>
    /// What to do when the attacker attacks the module
    /// </summary>
    public virtual void Attack()
    {
        this.Attacked = true;
        this.attackedIndicatorInstance.SetActive(true);
        this.gameController.NumAvailableAttacks--;
    }

    /// <summary>
    /// what to do when the module is no longer being attacked
    /// </summary>
    public void Fix()
    {
        if (this.Attacked)
        {
            this.Attacked = false;
            this.attackedIndicatorInstance.SetActive(false);
        }
    }

    /// <summary>
    /// Defines interaction with mouse
    /// </summary>
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.gameController && this.gameController.GameState == GameState.AttackerTurn)
            {
                if (this.Attacked)
                {
                    this.gameController.NumAvailableAttacks++;
                    this.Fix();
                }
                else
                {
                    if (this.gameController.NumAvailableAttacks > 0)
                    {
                        this.Attack();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (this.popupInstance.activeSelf)
            {
                this.CloseInfoPopup();
            }
            else
            {
                this.OpenInfoPopup(Input.mousePosition);
            }
        }
    }

    /// <summary>
    /// Updates the popup display by getting the values of the fields and changing the popup text to display
    /// the current values of the fields
    /// </summary>
    public void UpdatePopupDisplay() {
		var bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		var fields = new List<FieldInfo>();
        var props = new List<PropertyInfo>();
		foreach (string fieldName in displayFields) {
            var info = this.GetType().GetField(fieldName, bindings);
            if (info != null)
            {
                fields.Add(this.GetType().GetField(fieldName, bindings));
            }
            else
            {
                props.Add(this.GetType().GetProperty(fieldName, bindings));
            }
		}

		var displayStrings = new List<string>();
		foreach(FieldInfo field in fields) {
			displayStrings.Add(field.Name + ": " + field.GetValue(this));
		}
        foreach(PropertyInfo prop in props)
        {
            displayStrings.Add(prop.Name + ": " + prop.GetValue(this, null));
        }

		this.displayTextContent.text = string.Join("\n", displayStrings.ToArray());
	}

	/// <summary>
	/// Opens the info popup at the given location
	/// </summary>
	/// <param name="position">The position to place the popup at.</param>
	protected void OpenInfoPopup(Vector2 position) {
		this.CloseInfoPopup();
		this.UpdatePopupDisplay();
		RectTransform UITransform = this.popupInstance.GetComponent<RectTransform>();
		UITransform.position = position + new Vector2((UITransform.rect.width / 2), (UITransform.rect.height / 2));
		this.popupInstance.SetActive(true);
	}

    /// <summary>
    /// Closes the info popup
    /// </summary>
	protected void CloseInfoPopup() {
		this.popupInstance.SetActive(false);
	}

    /// <summary>
    /// True if the lhs module appears earlier in the system than the rhs
    /// </summary>
    /// <param name="lhs">first module to compare</param>
    /// <param name="rhs">second module to compare</param>
    /// <returns></returns>
    public static bool operator <(Module lhs, Module rhs)
    {
        Module currMod = rhs.PreviousModule;
        while (currMod)
        {
            if (currMod == lhs)
            {
                return true;
            }

            currMod = currMod.PreviousModule;
        }

        return false;
    }

    /// <summary>
    /// True if the lhs module appears later in the system than the rhs
    /// </summary>
    /// <param name="lhs">first module to compare</param>
    /// <param name="rhs">second module to compare</param>
    /// <returns></returns>
    public static bool operator >(Module lhs, Module rhs)
    {
        return (!(lhs < rhs) && lhs != rhs);
    }
}