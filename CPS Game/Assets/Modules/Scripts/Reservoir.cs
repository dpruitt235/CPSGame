using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservoir : Module
{
    /// <summary>
    /// Reservoirs ignore capacity, they just keep on filling up
    /// </summary>
    protected override void OnFlow()
    {
        this.Fill += this.PreviousModule.Fill;
        this.PreviousModule.Fill = 0;
    }
}
