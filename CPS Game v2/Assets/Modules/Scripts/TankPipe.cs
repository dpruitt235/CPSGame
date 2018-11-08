using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPipe : Module {

    public Tank tank;

    protected override void OnFlow()
    {
        this.Water = tank.WaterList[0];
        tank.WaterList.RemoveAt(0);
    }
}
