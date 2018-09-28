public class Filter : Module
{
    protected override void OnFlow()
    {
        if (!this.Attacked)
        {
            base.OnFlow();
        }
    }
}