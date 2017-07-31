using System;
using UnityEngine;

/// <summary>
/// Represents player's train.
/// </summary>
public class Train
{
    private Vector2 destinationPoint;
    private Vector2 movementVector;

    /// <summary>
    /// Occurs when train has been attacked.
    /// </summary>
    public event Action Attacked;

    /// <summary>
    /// Occurs when train has been crashed.
    /// </summary>
    public event Action Crashed;

    /// <summary>
    /// Gets the speed.
    /// </summary>
    public float Speed { get; private set; }

    /// <summary>
    /// Gets the lives count.
    /// </summary>
    public int Live { get; private set; }

    /// <summary>
    /// Gets the attack.
    /// </summary>
    public int Attack { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is alive.
    /// </summary>
    public bool IsAlive
    {
        get
        {
            return Live > 0;
        }
    }

    /// <summary>
    /// Gets the cargo.
    /// </summary>
    public int Cargo { get; private set; }

    /// <summary>
    /// Gets the team.
    /// </summary>
    public Team Team { get; private set; }

    /// <summary>
    /// Gets the position on the field.
    /// </summary>
    public Vector2 Position { get; private set; }

    /// <summary>
    /// Moves the train to specific destination.
    /// </summary>
    /// <param name="destination">The destination.</param>
    public void Move(Vector2 destination)
    {
        destinationPoint = destination;
        movementVector = destination - Position;
    }

    /// <summary>
    /// Starts the movement.
    /// </summary>
    /// <param name="trainPosition">The train position.</param>
    /// <param name="destination">The destination.</param>
    public void StartMovement(Vector2 trainPosition, Vector2 destination)
    {
        Position = trainPosition;
        Move(destination);
    }

    /// <summary>
    /// Stops this train.
    /// </summary>
    public void Stop()
    {
        movementVector = Vector2.zero;
    }

    /// <summary>
    /// Process movement by time.
    /// </summary>
    /// <param name="deltaTime">The delta time.</param>
    public void Tick(float deltaTime)
    {
        if (IsAlive)
        {
            Position += movementVector*deltaTime;
        }
    }

    /// <summary>
    /// Attacks the by train.
    /// </summary>
    /// <param name="attacker">The attacker.</param>
    public void AttackByTrain(Train attacker)
    {
        var distance = (Position - attacker.Position).magnitude;
        if (distance < Constants.COLLISION_DISTANCE)
        {
            Live -= attacker.Attack;
            Live = Math.Max(Live, 0);
            if (IsAlive)
            {
                OnAttacked();
            }
            else
            {
                OnCrashed();
            }
        }
    }

    private void OnCrashed()
    {
        var handler = Crashed;
        if (handler != null) handler();
    }

    private void OnAttacked()
    {
        var handler = Attacked;
        if (handler != null) handler();
    }
}