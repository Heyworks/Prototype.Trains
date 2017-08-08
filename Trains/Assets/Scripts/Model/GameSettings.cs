using System.Collections.Generic;

/// <summary>
/// Represents game settings.
/// </summary>
public static class GameSettings
{
    public static int fieldLength = 100;
    public static int firstLineX = 1;
    public static int lineDeltaX = 2;
    public static int cargoEffect = 5;
    public static Dictionary<ActionObjectType, int> cooldowns = new Dictionary<ActionObjectType, int> { { ActionObjectType.Ambush, 45 }, { ActionObjectType.Arrow, 10 }, { ActionObjectType.Barrier, 30 } };
    public static Dictionary<ActionObjectType, int> installTime = new Dictionary<ActionObjectType, int> { { ActionObjectType.Ambush, 5 }, { ActionObjectType.Arrow, 0 }, { ActionObjectType.Barrier, 5 } };
    public static int teamInitTime = 20;
}