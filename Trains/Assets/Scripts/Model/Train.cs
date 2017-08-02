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
    /// Gets or sets the position on the field.
    /// </summary>
    public Vector2 Position { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Train" /> class.
    /// </summary>
    /// <param name="speed">The speed.</param>
    /// <param name="live">The live.</param>
    /// <param name="attack">The attack.</param>
    /// <param name="team">The team.</param>
    /// <param name="startPosition">The start position.</param>
    public Train(float speed, int live, int attack, Team team, Vector2 startPosition)
    {
        Speed = speed;
        Live = live;
        Attack = attack;
        Team = team;
        Position = startPosition;
        Cargo = 10;
    }

    /// <summary>
    /// Moves the train to specific destination.
    /// </summary>
    /// <param name="destination">The destination.</param>
    public void Move(Vector2 destination)
    {
        destinationPoint = destination;
        movementVector = (destination - Position).normalized;
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
            var cargoEffect = Cargo * (GameSettings.cargoEffect / 100f);
            cargoEffect = Math.Min(cargoEffect, 1);
            Position += (Speed * movementVector * deltaTime) * (1 - cargoEffect);
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
    
    /// <summary>
    /// Removes the cargo.
    /// </summary>
    /// <param name="cargoAmount">The cargo amount.</param>
    public void RemoveCargo(int cargoAmount)
    {
        Cargo = Math.Max(Cargo - cargoAmount, 0);
        OnAttacked();
    }

    /// <summary>
    /// Decreases the speed.
    /// </summary>
    /// <param name="value">The value.</param>
    public void DecreaseSpeed(float value)
    {
        Speed /= 2;
    }
    
    /// <summary>
    /// Increases the speed.
    /// </summary>
    /// <param name="value">The value.</param>
    public void IncreaseSpeed(float value)
    {
        Speed *= value;
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