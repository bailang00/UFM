
public interface IProgress
{
    float GetLoaded();
    float GetTotal();
}

public static class IProgressHelper
{
    public static float GetRate(this IProgress p)
    {
        var rate = p.GetLoaded() / p.GetTotal();
        if (float.IsNaN(rate)) return 0.0f;
        return (rate < 0 ? 0 : rate > 1 ? 1 : rate);
    }
}