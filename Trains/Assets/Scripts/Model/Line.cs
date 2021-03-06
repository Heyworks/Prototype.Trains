﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    /// Occurs when action object has been added.
    /// </summary>
    public event Action<ActionObject> ActionObjectAdded;

    /// <summary>
    /// Gets the line x coordinate.
    /// </summary>
    public float XCoordinate
    {
        get
        {
            return depoRed.Position.x;
        }
    }

    /// <summary>
    /// Gets the trains.
    /// </summary>
    public List<Train> Trains
    {
        get
        {
            return trains;
        }
    }

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
    public void CreateTrain(float speed, int live, int attack, int cargo, Team team)
    {
        var startPosition = team == Team.Red ? depoRed.Position : depoBlue.Position;
        var train = new Train(speed, live, attack, team, cargo, startPosition);
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
        OnActionObjectAdded(actionObject);
    }

    /// <summary>
    /// Removes the action object.
    /// </summary>
    /// <param name="actionObject">The action object.</param>
    public void RemoveActionObject(ActionObject actionObject)
    {
        actionObjects.Remove(actionObject);
    }

    /// <summary>
    /// Process movement by time.
    /// </summary>
    /// <param name="deltaTime">The delta time.</param>
    public void Tick(float deltaTime)
    {
        MoveTrains(deltaTime);

        ProcessTrainsCollisions();

        ActivateObjects();

        ProcessDepoReaches();
    }

    /// <summary>
    /// Starts the movement.
    /// </summary>
    public void StartMovement()
    {
        foreach (var train in trains)
        {
            var destination = train.Team == Team.Blue ? depoRed.Position : depoBlue.Position;
            train.Move(destination);
        }
    }
    
    /// <summary>
    /// Gets the distance from point.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns></returns>
    public float GetDistanceFrom(Vector2 position)
    {
        return Math.Abs(depoRed.Position.x - position.x);
    }

    private void ActivateObjects()
    {
        foreach (var train in trains)
        {
            foreach (var actionObject in actionObjects)
            {
                actionObject.TryActivate(train);
            }
        }
    }

    private void ProcessTrainsCollisions()
    {
        var crashedTrains = new List<Train>();
        for (int i = 0; i < trains.Count; i++)
        {
            for (int j = i + 1; j < trains.Count; j++)
            {
                trains[i].AttackByTrain(trains[j]);
                trains[j].AttackByTrain(trains[i]);
                if (!trains[i].IsAlive)
                {
                    crashedTrains.Add(trains[i]);
                }

                if (!trains[j].IsAlive)
                {
                    crashedTrains.Add(trains[j]);
                }
            }
        }

        foreach (var crashedTrain in crashedTrains)
        {
            RemoveTrain(crashedTrain);
        }
    }

    private void MoveTrains(float deltaTime)
    {
        foreach (var train in trains)
        {
            train.Tick(deltaTime);
        }
    }

    private void ProcessDepoReaches()
    {
        var reachedDepoTrains = trains.Where(train => depoRed.TryProcessReachByTrain(train) || depoBlue.TryProcessReachByTrain(train)).ToList();

        foreach (var reachedDepoTrain in reachedDepoTrains)
        {
            RemoveTrain(reachedDepoTrain);
        }
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