using System.Collections.Generic;

/// <summary>
/// Represents train line.
/// </summary>
public class Line
{
    private readonly Depo depoRed;
    private readonly Depo depoBlue;
    private readonly List<Train> trains = new List<Train>();
    private readonly List<ActionObject> actionObjects = new List<ActionObject>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Line" /> class.
    /// </summary>
    /// <param name="depoRed">The depo red.</param>
    /// <param name="depoBlue">The depo blue.</param>
    public Line(Depo depoRed, Depo depoBlue)
    {
        this.depoRed = depoRed;
        this.depoBlue = depoBlue;
    }

    /// <summary>
    /// Adds the train.
    /// </summary>
    /// <param name="train">The train.</param>
    public void AddTrain(Train train)
    {
        trains.Add(train);
    }

    /// <summary>
    /// Creates the train.
    /// </summary>
    public void CreateTrain(float speed, int live, int attack, Team team)
    {
        var startPosition = team == Team.Red ? depoRed.Position : depoBlue.Position;
        var train = new Train(speed, live, attack, team, startPosition);
        AddTrain(train);
    }

    /// <summary>
    /// Removes the train.
    /// </summary>
    /// <param name="train">The train.</param>
    public void RemoveTrain(Train train)
    {
        trains.Remove(train);
    }

    /// <summary>
    /// Adds the action object.
    /// </summary>
    /// <param name="actionObject">The action object.</param>
    public void AddActionObject(ActionObject actionObject)
    {
        actionObjects.Add(actionObject);
    }
}
