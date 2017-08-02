using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents action object base class
/// </summary>
public abstract class ActionObject
{
    /// <summary>
    /// Gets or sets a value indicating whether this object is active.
    /// </summary>
    public bool IsActive { get; protected set; }

    /// <summary>
    /// Gets the position.
    /// </summary>
    public float YPosition { get; private set; }

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

    protected float StartInstallationTime { get; private set; }

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
        YPosition = yPosition;
        ActionObjectType = actionObjectType;
        lines.First().AddActionObject(this);
        StartInstallationTime = Time.time;
    }

    /// <summary>
    /// Tries to activate.
    /// </summary>
    public void TryActivate(Train train)
    {
        if (!IsActive)
        {
            var distance = Math.Abs(train.Position.y - YPosition);
            if (train.Team != OwnerTeam && distance < Constants.COLLISION_DISTANCE)
            {
                Activate(train);
            }
        }
    }
    
    protected abstract void Activate(Train train);
}

