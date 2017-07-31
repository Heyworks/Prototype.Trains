using System.Collections.Generic;

/// <summary>
/// Represents train line.
/// </summary>
public class Line
{
    private readonly Depo depoA;
    private readonly Depo depoB;
    private readonly List<Train> trains = new List<Train>();
    private readonly List<ActionObject> actionObjects = new List<ActionObject>();
 
    /// <summary>
    /// Initializes a new instance of the <see cref="Line"/> class.
    /// </summary>
    /// <param name="depoA">The depo a.</param>
    /// <param name="depoB">The depo b.</param>
    public Line(Depo depoA, Depo depoB)
    {
        this.depoA = depoA;
        this.depoB = depoB;
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
