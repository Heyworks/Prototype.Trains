using System;
using UnityEngine;

/// <summary>
/// Represents slot for action objects.
/// </summary>
public class ActionObjectSlot
{
    private ActionObjectType objectType;
    private ActionObjectsFactory factory;
    private float lastUseTime;
    private int cooldown;

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
    /// Initializes a new instance of the <see cref="ActionObjectSlot"/> class.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="factory">The factory.</param>
    public ActionObjectSlot(ActionObjectType objectType, ActionObjectsFactory factory)
    {
        this.objectType = objectType;
        this.factory = factory;
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
            return factory.CreateActionObject(objectType, position);
        }

        Debug.LogError(string.Format("Can't install action object of type {0}. Cooldown is not finished.", objectType));
        return null;
    }
}
