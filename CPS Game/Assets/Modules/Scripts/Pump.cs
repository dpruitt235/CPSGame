using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : Module
{
    public bool On {
        get {
            if (!this.Attacked)
            {
                return this.on;
            }
            else
            {
                return false;
            }
        }
        set {
            this.on = value;
        }
    }

    private bool on = true;
}