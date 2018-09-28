using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservoir : Module
{
    protected override void OnFlow()
    {
        this.Fill += this.PreviousModule.Fill;
        this.PreviousModule.Fill = 0;
    }
}
