using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class with implementation that is common to all modules.  Right now, it simply displays all display fields when the mouse is hovered over the
/// module.  When inheriting from this class, all you need to do to add another display field is add the name of the field to the displayFields list.  There
/// is a display field called status automatically added for each module.
/// </summary>
public abstract class Module : MonoBehaviour, IModule
{
    public GameObject popupPrefab;

    public Pump InFlowingPump;

    public Module PreviousModule;
    
    public bool Attacked = false;

    public int Fill = 0;
    public int Capacity = 1;

    protected List<string> displayFields;
	private GameObject popupInstance;
	private Text displayTextTitle;
	private Text displayTextContent;

	private void Awake() {
		this.displayFields = new List<string>();
        this.displayFields.Add("Attacked");
        this.displayFields.Add("Fill");
        this.displayFields.Add("Capacity");

        //Instantiate the popup that displays the display fields
        this.popupInstance = Instantiate (popupPrefab, popupPrefab.transform.position, popupPrefab.transform.rotation);
		Canvas c = (Canvas) FindObjectOfType (typeof(Canvas));
		this.popupInstance.transform.SetParent (c.transform, false);
		var texts = this.popupInstance.GetComponentsInChildren<Text>();
		if (texts.Length == 2) {
			this.displayTextContent = texts[1];
			this.displayTextTitle = texts[0];
		}
		this.displayTextTitle.text = this.gameObject.name;

		this.CloseInfoPopup();
	}

    public virtual void Tick()
    {
        if (this.InFlowingPump.On && this.PreviousModule)
        {
            this.Fill += this.PreviousModule.Fill;
            this.PreviousModule.Fill = 0;
        }

        if (this.Fill > this.Capacity)
        {
            Debug.Log("Overflow in module " + this.gameObject.name);
        }

        this.UpdatePopupDisplay();
        if (this.PreviousModule)
            this.PreviousModule.Tick();
    }

	private void OnMouseDown() {
        if (this.popupInstance.activeSelf)
        {
            this.CloseInfoPopup();
        } else
        {
            this.OpenInfoPopup(Input.mousePosition);
        }
	}

	/// <summary>
	/// Updates the popup display by getting the values of the fields and changing the popup text to display
	/// the current values of the fields
	/// </summary>
	public void UpdatePopupDisplay() {
		var bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		var fields = new List<FieldInfo> ();
		foreach (string fieldName in displayFields) {
			fields.Add(this.GetType().GetField(fieldName, bindings));
		}

		var displayStrings = new List<string>();
		foreach(FieldInfo field in fields) {
			displayStrings.Add(field.Name + ": " + field.GetValue(this));
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
		RectTransform UITransform = this.popupInstance.GetComponent<RectTransform> ();
		UITransform.position = position + new Vector2((UITransform.rect.width / 2), (UITransform.rect.height / 2));
		this.popupInstance.SetActive(true);
	}

	protected void CloseInfoPopup() {
		this.popupInstance.SetActive(false);
	}
}