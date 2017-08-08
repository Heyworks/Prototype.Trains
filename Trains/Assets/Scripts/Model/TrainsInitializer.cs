using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents trains positions and cargo initializer. 
/// </summary>
public class TrainsInitializer
{
    /// <summary>
    /// Occurs when free cargo changed.
    /// </summary>
    public event Action FreeCargoChanged;

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
    /// <param name="index">The index.</param>
    public void AddCargoToTrain(int index)
    {
        if (FreeCargo > 0)
        {
            var train = Trains[index];
            train.AddCargo(1);
            FreeCargo--;
            OnFreeCargoChanged();
        }
    }

    /// <summary>
    /// Removes the cargo from train.
    /// </summary>
    /// <param name="index">The index.</param>
    public void RemoveCargoFromTrain(int index)
    {
        var train = Trains[index];
        if (train.Cargo > 0)
        {
            train.RemoveCargo(1);
            FreeCargo++;
            OnFreeCargoChanged();
        }
    }

    /// <summary>
    /// Swaps the specified trains.
    /// </summary>
    /// <param name="train1Slot">The train1 slot.</param>
    /// <param name="train2Slot">The train2 slot.</param>
    public void Swap(int train1Slot, int train2Slot)
    {
        var train1 = Trains[train1Slot];
        Trains[train1Slot] = Trains[train2Slot];
        Trains[train2Slot] = train1;
    }

    //TODO change Train to TrainData
    private void CreateTrains()
    {
        Trains = new List<Train> { CreateTrain(2, 1, 1, Team), CreateTrain(1.5f, 2, 2, Team), CreateTrain(1, 3, 3, Team) };
    }

    private Train CreateTrain(float speed, int live, int attack, Team team)
    {
        return new Train(speed, live, attack, team, 0, Vector2.zero);
    }

    private void OnFreeCargoChanged()
    {
        Action handler = FreeCargoChanged;
        if (handler != null) handler();
    }
}
