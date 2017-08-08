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
    /// Occurs when action object has been added.
    /// </summary>
    public event Action<ActionObject> ActionObjectAdded;

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
    /// Initializes a new instance of the <see cref="GameField" /> class.
    /// </summary>
    /// <param name="redTrains">The red trains.</param>
    /// <param name="blueTrains">The blue trains.</param>
    public GameField(List<Train> redTrains, List<Train> blueTrains)
    {
        CreateFieldItems(redTrains, blueTrains);
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

    private void CreateFieldItems(List<Train> redTrainsData, List<Train> blueTrainsData)
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

            var redTrainData = redTrainsData[i];
            var blueTrainData = blueTrainsData[i];
            line.CreateTrain(redTrainData.Speed, redTrainData.Live, redTrainData.Attack, redTrainData.Cargo, Team.Red);
            line.CreateTrain(blueTrainData.Speed, blueTrainData.Live, blueTrainData.Attack,  blueTrainData.Cargo, Team.Blue);

            line.ActionObjectAdded += Line_ActionObjectAdded;
        }
    }

    private void Depo_DepoReached()
    {
        OnScoreChanged();
    }
    
    private void Line_ActionObjectAdded(ActionObject actionObject)
    {
        OnActionObjectAdded(actionObject);
    }

    private void OnScoreChanged()
    {
        Action handler = ScoreChanged;
        if (handler != null) handler();
    }

    private void OnActionObjectAdded(ActionObject actionObject)
    {
        var handler = ActionObjectAdded;
        if (handler != null)
        {
            handler(actionObject);
        }
    }
}
