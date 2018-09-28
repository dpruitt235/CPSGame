using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowController : MonoBehaviour
{
    public Tank Reservoir;
    public Module FirstModule;

    public void TickModules()
    {
        this.FirstModule.Fill = this.FirstModule.Capacity;
        this.Reservoir.Tick();
    }

    public void StartWaterFlow(float secondsBetweenTicks)
    {
        this.InvokeRepeating("TickModules", 0.1f, secondsBetweenTicks);
    }
}
