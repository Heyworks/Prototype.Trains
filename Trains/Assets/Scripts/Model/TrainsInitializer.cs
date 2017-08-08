using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents trains positions and cargo initializer. 
/// </summary>
public class TrainsInitializer
{
    /// <summary>
    /// Gets the team.
    /// </summary>
    public Team Team { get; private set; }

    /// <summary>
    /// Gets the free cargo.
    /// </summary>
    public int FreeCargo { get; private set; }

    /// <summary>
    /// Gets the trains.
    /// </summary>
    public List<Train> Trains { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrainsInitializer"/> class.
    /// </summary>
    /// <param name="team">The team.</param>
    public TrainsInitializer(Team team)
    {
        Team = team;
        FreeCargo = 10;
        CreateTrains();
    }

    /// <summary>
    /// Adds the cargo to train.
    /// </summary>
    /// <param name="train">The train.</param>
    public void AddCargoToTrain(Train train)
    {
        if (FreeCargo > 0)
        {
            train.AddCargo(1);
            FreeCargo--;
        }
    }

    /// <summary>
    /// Removes the cargo from train.
    /// </summary>
    /// <param name="train">The train.</param>
    public void RemoveCargoFromTrain(Train train)
    {
        if (train.Cargo > 0)
        {
            train.RemoveCargo(1);
            FreeCargo++;
        }
    }

    /// <summary>
    /// Swaps the specified trains.
    /// </summary>
    /// <param name="train1">The train1.</param>
    /// <param name="train2">The train2.</param>
    public void Swap(Train train1, Train train2)
    {
        var index1 = Trains.IndexOf(train1);
        var index2 = Trains.IndexOf(train2);
        Trains[index1] = train2;
        Trains[index2] = train1;
    }

    //TODO change Train to TrainData
    private void CreateTrains()
    {
        Trains = new List<Train> {CreateTrain(2, 1, 1, Team), CreateTrain(1.5f, 2, 2, Team), CreateTrain(1, 3, 3, Team)};
    }

    private Train CreateTrain(float speed, int live, int attack, Team team)
    {
        return new Train(speed, live, attack, team, 0, Vector2.zero);
    }
}
