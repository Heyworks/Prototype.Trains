using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents action object base class
/// </summary>
public abstract class ActionObject
{
    private readonly float startInstallationTime;
   

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

    /// <summary>
    /// Gets the context behaviour.
    /// </summary>
    protected MonoBehaviour ContextBehaviour
    {
        get
        {
            //TODO: create own context beh.
            return FieldView.fieldView;
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
        YPosition = yPosition;
        ActionObjectType = actionObjectType;
        AssignedLine = lines.First();
        AssignedLine.AddActionObject(this);
        startInstallationTime = Time.time;
    }

    /// <summary>
    /// Tries to activate.
    /// </summary>
    public void TryActivate(Train train)
    {
        var isInstalled = (Time.time - startInstallationTime) > InstallationTime;

        if (!IsActive && isInstalled)
        {
            var distance = Math.Abs(train.Position.y - YPosition);
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
    }

    protected abstract void Activate(Train train);
}