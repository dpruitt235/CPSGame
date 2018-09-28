using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowController : MonoBehaviour
{
    public Tank Reservoir;
    public Module FirstModule;
    
    public void TickModules()
    {
        this.Reservoir.Tick();
    }
}
