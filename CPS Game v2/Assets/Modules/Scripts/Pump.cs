using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : Module
{
    /// <summary>
    /// Whether the pump is on or not.  If attacked, it is always off (broken)
    /// </summary>
    public bool On {
        get {
            if (!this.Attacked)
            {
                return this.on;
            }
            else if (this.AttackDropdowns[0].value == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        set {
            this.on = value;
        }
    }

    private bool on = true;

    private new void Start()
    {
        this.displayFields.Add("On");
        base.Start();
    }

    public override bool IsPump()
    {
        return true;
    }
}