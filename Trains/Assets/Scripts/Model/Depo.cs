using UnityEngine;

/// <summary>
/// Represents player's depo.
/// </summary>
public class Depo
{
    /// <summary>
    /// Gets the original team.
    /// </summary>
    public Team OriginalTeam { get; private set; }

    /// <summary>
    /// Gets the opponent cargo amount.
    /// </summary>
    public int OpponentCargo { get; private set; }

    /// <summary>
    /// Gets the position.
    /// </summary>
    public Vector2 Position { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Depo"/> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="team">The team.</param>
    public Depo(Vector2 position, Team team)
    {
        Position = position;
        OriginalTeam = team;
    }

    /// <summary>
    /// Clears this depo data.
    /// </summary>
    public void Clear()
    {
        OpponentCargo = 0;
    }

    /// <summary>
    /// Processes the reach by train.
    /// </summary>
    public bool TryProcessReachByTrain(Train train)
    {
        var distance = (Position - train.Position).magnitude;
        if (train.Team != OriginalTeam && distance < Constants.COLLISION_DISTANCE)
        {
            OpponentCargo += train.Cargo;
            train.Stop();

            return true;
        }

        return false;
    }
}