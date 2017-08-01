using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents Game field.
/// </summary>
public class GameField
{
    private readonly List<Depo> depo = new List<Depo>();
    private readonly List<Line> lines = new List<Line>();

    /// <summary>
    /// Occurs when score has been changed.
    /// </summary>
    public event Action ScoreChanged;
    
    /// <summary>
    /// Gets the left lower depo position.
    /// </summary>
    public Vector2 LeftLowerDepoPosition
    {
        get
        {
            return depo.First().Position;
        }
    }

    /// <summary>
    /// Gets the right upper depo position.
    /// </summary>
    public Vector2 RightUpperDepoPosition
    {
        get
        {
            return depo.Last().Position;
        }
    }

    /// <summary>
    /// Gets the red team score.
    /// </summary>
    public int RedTeamScore
    {
        get
        {
            return depo.Where(item => item.OriginalTeam == Team.Blue).Sum(item => item.OpponentCargo);
        }
    }

    /// <summary>
    /// Gets the blue team score.
    /// </summary>
    public int BlueTeamScore
    {
        get
        {
            return depo.Where(item => item.OriginalTeam == Team.Red).Sum(item => item.OpponentCargo);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GameField"/> class.
    /// </summary>
    public GameField()
    {
        CreateFieldItems();
    }

    /// <summary>
    /// Gets all trains on lines.
    /// </summary>
    public List<Train> GetTrains()
    {
        var list = new List<Train>();

        foreach (var line in lines)
        {
            list.AddRange(line.Trains);
        }

        return list;
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

    /// <summary>
    /// Process movement by time.
    /// </summary>
    /// <param name="deltaTime">The delta time.</param>
    public void Tick(float deltaTime)
    {
        foreach (var line in lines)
        {
            line.Tick(deltaTime);
        }
    }

    /// <summary>
    /// Starts the movement.
    /// </summary>
    public void StartMovement()
    {
        foreach (var line in lines)
        {
            line.StartMovement();
        }
    }

    /// <summary>
    /// Gets the lines sorted by distance.
    /// </summary>
    /// <param name="position">The position.</param>
    public List<Line> GetLinesSortedByDistance(Vector2 position)
    {
        return lines.OrderBy(line => line.GetDistanceFrom(position)).ToList();
    }

    private void CreateFieldItems()
    {
        var blueTeamY = GameSettings.fieldLength;
        var firstLineX = GameSettings.firstLineX;
        var lineDeltaX = GameSettings.lineDeltaX;

        for (int i = 0; i < 3; i++)
        {
            var lineXPos = firstLineX + lineDeltaX * i;
            var depoRed = new Depo(new Vector2(lineXPos, 0), Team.Red);
            var depoBlue = new Depo(new Vector2(lineXPos, blueTeamY), Team.Blue);
            depoRed.DepoReached += Depo_DepoReached;
            depoBlue.DepoReached += Depo_DepoReached;

            var line = new Line(depoRed, depoBlue);

            depo.Add(depoRed);
            depo.Add(depoBlue);
            lines.Add(line);
        }

        //Light
        lines[0].CreateTrain(2, 1, 1, Team.Red);
        lines[1].CreateTrain(2, 1, 1, Team.Blue);

        //Medium
        lines[1].CreateTrain(1.5f, 2, 2, Team.Red);
        lines[2].CreateTrain(1.5f, 2, 2, Team.Blue);

        //Heavy
        lines[2].CreateTrain(1, 3, 3, Team.Red);
        lines[0].CreateTrain(1, 3, 3, Team.Blue);
    }

    private void Depo_DepoReached()
    {
        OnScoreChanged();
    }
    
    private void OnScoreChanged()
    {
        Action handler = ScoreChanged;
        if (handler != null) handler();
    }
}
