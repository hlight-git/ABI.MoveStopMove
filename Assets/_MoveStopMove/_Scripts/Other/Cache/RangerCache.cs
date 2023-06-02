using System.Collections.Generic;

public class RangerCache : Cache<IRanger>
{
    static Dictionary<IRanger, RangerBackUp> backUp;
    public static RangerBackUp GetBackUpStat(IRanger ranger)
    {
        if (!backUp.ContainsKey(ranger))
        {
            backUp.Add(ranger, new RangerBackUp(ranger));
        }
        return backUp[ranger];
    }
}
public class RangerBackUp
{
    public float Size { get; set; }
    public float MoveSpeed { get; set; }
    public RangerBackUp(IRanger ranger)
    {
        Size = ranger.Size;

    }
}