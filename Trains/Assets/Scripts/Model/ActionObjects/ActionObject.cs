﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents action object base class
/// </summary>
public abstract class ActionObject
{
    private readonly float startInstallationTime;
    private readonly float yPosition;
    
    private static MonoBehaviour contextMonoBehaviour;

    /// <summary>
    /// Occurs when object has been deactivated.
    /// </summary>
    public event Action Deactivated;

    /// <summary>
    /// Gets or sets a value indicating whether this object is active.
    /// </summary>
    public bool IsActive { get; protected set; }

    /// <summary>
    /// Gets the position.
    /// </summary>
    public Vector2 Position
    {
        get
        {
            return new Vector2(AssignedLine.XCoordinate, yPosition);
        }
    }

    /// <summary>
    /// Gets the owner team.
    /// </summary>
    public Team OwnerTeam { get; private set; }

    /// <summary>
    /// Gets the type of the action object.
    /// </summary>
    public ActionObjectType ActionObjectType { get; private set; }

    /// <summary>
    /// Gets the installation time.
    /// </summary>
    public float InstallationTime
    {
        get
        {
            return GameSettings.installTime[ActionObjectType];
        }
    }

    /// <summary>
    /// Gets the context behaviour.
    /// </summary>
    protected MonoBehaviour ContextBehaviour
    {
        get
        {
            if (contextMonoBehaviour == null)
            {
                contextMonoBehaviour = (new GameObject()).AddComponent<ContextBehaviour>();
            }

            return contextMonoBehaviour;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this action object is installed.
    /// </summary>
    public bool IsInstalled
    {
        get
        {
            return (Time.time - startInstallationTime) > InstallationTime;
        }
    }

    /// <summary>
    /// Gets the assigned line.
    /// </summary>
    protected Line AssignedLine { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionObject" /> class.
    /// </summary>
    /// <param name="ownerTeam">The owner team.</param>
    /// <param name="actionObjectType">Type of the action object.</param>
    /// <param name="yPosition">The y position.</param>
    /// <param name="lines">The lines.</param>
    protected ActionObject(Team ownerTeam, ActionObjectType actionObjectType, float yPosition, List<Line> lines)
    {
        OwnerTeam = ownerTeam;
        this.yPosition = yPosition;
        ActionObjectType = actionObjectType;
        AssignedLine = lines.First();
        startInstallationTime = Time.time;
    }

    /// <summary>
    /// Adds the object to line.
    /// </summary>
    public void AddObjectToLine()
    {
        AssignedLine.AddActionObject(this);
    }

    /// <summary>
    /// Tries to activate.
    /// </summary>
    public void TryActivate(Train train)
    {
        if (!IsActive && IsInstalled)
        {
            var distance = (train.Position - Position).magnitude;
            if (train.Team == GetRequiredTrainTeam() && distance < Constants.COLLISION_DISTANCE)
            {
                IsActive = true;
                Activate(train);
            }
        }
    }

    protected virtual Team GetRequiredTrainTeam()
    {
        switch (OwnerTeam)
        {
            case Team.Red:
                return Team.Blue;
            case Team.Blue:
                return Team.Red;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected void Deactivate()
    {
        AssignedLine.RemoveActionObject(this);
        OnDeactivated();
    }

    protected abstract void Activate(Train train);
    
    private void OnDeactivated()
    {
        var handler = Deactivated;
        if (handler != null)
        {
            handler();
        }
    }
}