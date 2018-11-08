using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservoir : Module
{
    public List<WaterObject> WaterList = new List<WaterObject>();

    /// <summary>
    /// Reservoirs ignore capacity, they just keep on filling up
    /// </summary>
    protected override void OnFlow()
    {
        if (this.PreviousModule.Water != null)
        {
            this.WaterList.Add(this.PreviousModule.Water);
            Debug.Log(WaterList.Count);
        }
        Debug.Log(WaterList.Count);
    }
}
