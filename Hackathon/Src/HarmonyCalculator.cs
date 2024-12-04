namespace Hackathon;

public static class HarmonyCalculator
{
    public static double HarmonicMeanSatisfaction(IEnumerable<int> values)
    {
        var list = values.ToList();
        var sumOfInverses = list.Sum(v => 1.0 / v);
        return list.Count / sumOfInverses;
    }
}