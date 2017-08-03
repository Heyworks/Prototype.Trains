using System;
using UnityEngine;

/// <summary>
/// Represents slot for action objects.
/// </summary>
public class ActionObjectSlot
{
    private readonly ActionObjectType objectType;
    private readonly ActionObjectsFactory factory;
    private readonly int cooldown;
    private readonly Team team;
    private float lastUseTime = -100;

    /// <summary>
    /// Gets a value indicating whether this instance is action object available.
    /// </summary>
    public bool IsActionObjectAvailable
    {
        get
        {
            return (Time.time - lastUseTime) > cooldown;
        }
    }

    /// <summary>
    /// Gets the availability percent.
    /// </summary>
    public float AvailabilityPercent
    {
        get
        {
            return Math.Min((Time.time - lastUseTime)/cooldown, 1);
        }
    }

    /// <summary>
    /// Gets the type of the action object.
    /// </summary>
    public ActionObjectType ActionObjectType
    {
        get
        {
            return objectType;
        }
    }

    /// <summary>
    /// Gets the team.
    /// </summary>
    public Team Team
    {
        get
        {
            return team;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionObjectSlot" /> class.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="team">The team.</param>
    /// <param name="factory">The factory.</param>
    public ActionObjectSlot(ActionObjectType objectType, Team team, ActionObjectsFactory factory)
    {
        this.objectType = objectType;
        this.factory = factory;
        this.team = team;
        cooldown = GameSettings.cooldowns[objectType];
    }

    /// <summary>
    /// Installs the action object.
    /// </summary>
    /// <param name="position">The position.</param>
    public ActionObject InstallObject(Vector2 position)
    {
        if (IsActionObjectAvailable)
        {
            lastUseTime = Time.time;
            return factory.CreateActionObject(objectType, team, position);
        }

        Debug.LogError(string.Format("Can't install action object of type {0}. Cooldown is not finished.", objectType));
        return null;
    }
}
