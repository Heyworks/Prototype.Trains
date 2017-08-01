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
    public Vector2 Position { get; private set; }

    /// <summary>
    /// Gets the owner team.
    /// </summary>
    public Team OwnerTeam { get; private set; }

    /// <summary>
    /// Gets the instalation time.
    /// </summary>
    public float InstalationTime { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionObject" /> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="ownerTeam">The owner team.</param>
    protected ActionObject(Vector2 position, Team ownerTeam)
    {
        Position = position;
        OwnerTeam = ownerTeam;
    }

    public void UpdatePosition(Vector2 position)
    {
        Position = position;
    }

    /// <summary>
    /// Tries to activate.
    /// </summary>
    public void TryActivate(Train train)
    {
        if (!IsActive)
        {
            var distance = (train.Position - Position).magnitude;
            if (train.Team != OwnerTeam && distance < Constants.COLLISION_DISTANCE)
            {
                Activate(train);
            }
        }
    }

    protected abstract void Activate(Train train);
}

