using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Module
{
    //No special functionality in pipes (yet?)
    private bool Flow;
    private bool[] Purity;

    public override void Attack()
    {
        Purity = Water.purity;
        Flow = AttackDropdowns[0];
        Purity[0] = AttackDropdowns[1];
        Purity[1] = AttackDropdowns[2];
        Purity[2] = AttackDropdowns[3];
        base.Attack();
    }
}
