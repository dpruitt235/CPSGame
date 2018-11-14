using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Module
{
    public Pump OutFlowingPump;

    public GameObject overFlowSprite;

    public List<WaterObject> WaterList = new List<WaterObject>();

    public int Fill {
        get {
            return WaterList.Count;
        }
     }

    [Range(1, 10)]
    public int TankCapacity = 5;

    private new void Start()
    {
        this.displayFields.Add("Fill");
        base.Start();
    }

    /// <summary>
    /// Check if inpump is on. Bring in unit of water from previous
    /// Adds water if incoming pump is on
    /// </summary>
    protected override void OnFlow()
    {
        if(this.PreviousModule && PreviousModule.Water != null)
        {
            WaterList.Add(this.PreviousModule.Water);
            this.PreviousModule.Water = null;
            if(WaterList.Count > TankCapacity)
            {
                OnOverflow();
            }
        }
    }

    //create object behind tank to look like spilled water
    protected override void OnOverflow()
    {
        
    }
}