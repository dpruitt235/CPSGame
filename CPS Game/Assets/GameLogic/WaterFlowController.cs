using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowController : MonoBehaviour
{
    public Reservoir Reservoir;

    private Module firstModule;

    private void Start()
    {
        Module currMod = Reservoir;
        while (currMod.PreviousModule)
        {
            currMod = currMod.PreviousModule;
        }
        this.firstModule = currMod;
    }

    public void TickModules()
    {
        this.firstModule.Fill = this.firstModule.Capacity;
        this.Reservoir.Tick();
    }

    public void StartWaterFlow(float secondsBetweenTicks)
    {
        this.InvokeRepeating("TickModules", 0.1f, secondsBetweenTicks);
    }
}
