public class Filter : Module
{
    public int PurityIndex;
    /// <summary>
    /// This onflow override cleans water when passed through based on a specified purity index set on the filter object.
    /// </summary>
    protected override void OnFlow()
    {
        if (!this.Attacked)
        {
            base.OnFlow();
            if (this.Water != null) this.Water.purity[this.PurityIndex] = true;
        }
        else if (this.AttackDropdowns[0].value == 0) //CLOG
        {

        }
        else //Disable
        {
            base.OnFlow();
        }
    }

    public override bool IsFilter()
    {
        return true;
    }
}