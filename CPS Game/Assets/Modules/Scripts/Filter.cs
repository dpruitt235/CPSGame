public class Filter : Module
{
    /// <summary>
    /// If the filter is attacked, it is clogged and water will not flow through
    /// </summary>
    protected override void OnFlow()
    {
        if (!this.Attacked)
        {
            base.OnFlow();
        }
    }

    public override bool IsFilter()
    {
        return true;
    }
}