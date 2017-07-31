using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents Game field.
/// </summary>
public class GameField
{
    private readonly List<Depo> depo = new List<Depo>();
    private readonly List<Line> lines = new List<Line>();

    /// <summary>
    /// Initializes a new instance of the <see cref="GameField"/> class.
    /// </summary>
    public GameField()
    {
        CreateFieldItems();
    }

    /// <summary>
    /// Tries the apply action object.
    /// </summary>
    /// <param name="actionObject">The action object.</param>
    public bool TryApplyActionObject(ActionObject actionObject)
    {
        //TODO: Implement
        return true;
    }

    private void CreateFieldItems()
    {
        var blueTeamY = 10;
        var firstLineX = 1;
        var lineDeltaX = 2;

        for (int i = 0; i < 3; i++)
        {
            var lineXPos = firstLineX + lineDeltaX * i;
            var depoRed = new Depo(new Vector2(lineXPos, 0), Team.Red);
            var depoBlue = new Depo(new Vector2(lineXPos, blueTeamY), Team.Blue);
            var line = new Line(depoRed, depoBlue);

            depo.Add(depoRed);
            depo.Add(depoBlue);
            lines.Add(line);
        }

        //Light
        lines[0].CreateTrain(2, 1, 1, Team.Red);
        lines[0].CreateTrain(2, 1, 1, Team.Blue);

        //Medium
        lines[1].CreateTrain(1.5f, 2, 2, Team.Red);
        lines[1].CreateTrain(1.5f, 2, 2, Team.Blue); 

        //Heavy
        lines[2].CreateTrain(1, 3, 3, Team.Red);
        lines[2].CreateTrain(1, 3, 3, Team.Blue); 
    }
}
