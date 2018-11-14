using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPipe : Module {

    public Tank tank;

    protected override void OnFlow()
    {
        if (tank.WaterList.Count > 0)
        {
            this.Water = tank.WaterList[0];
            tank.WaterList.Remove(this.Water);
        }
        else
        {
            this.Water = null;
        }
    }
}
