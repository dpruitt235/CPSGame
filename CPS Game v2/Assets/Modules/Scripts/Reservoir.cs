using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservoir : Module
{
    public List<WaterObject> WaterList = new List<WaterObject>();

    public int Fill {
        get { return WaterList.Count; }
    }

    public float AveragePurity {
        get {
            int tot = 0;
            foreach(WaterObject w in WaterList)
            {
                tot += (w.purity[0] ? 1 : 0) + (w.purity[1] ? 1 : 0) + (w.purity[2] ? 1 : 0);
            }
            return tot / ((float)Fill * 3);
        }
    }

    private new void Start()
    {
        this.displayFields.Add("Fill");
        this.displayFields.Add("AveragePurity");
        base.Start();
    }

    /// <summary>
    /// Reservoirs ignore capacity, they just keep on filling up
    /// </summary>
    protected override void OnFlow()
    {
        if (this.PreviousModule.Water != null)
        {
            this.WaterList.Add(this.PreviousModule.Water);
            this.PreviousModule.Water = null;
        }
    }

    public override void Attack()
    {
        //Don't let the reservoid be attacked
    }

    public override string ToString()
    {
        string result = "";
        foreach (WaterObject w in this.WaterList)
        {
            result += "; " + w.purity[0] + ", " + w.purity[1] + ", " + w.purity[2];
        }

        return result;
    }
}
