using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents barrier action object which make train slowly.
/// </summary>
public class BarrierActionObject : ActionObject
{
    private readonly float speedMultiplier = 2f;
    private readonly float workTime = 15f;

    /// <summary>
    /// Initializes a new instance of the <see cref="BarrierActionObject"/> class.
    /// </summary>
    /// <param name="ownerTeam">The owner team.</param>
    /// <param name="yPosition">The y position.</param>
    /// <param name="lines">The lines.</param>
    public BarrierActionObject(Team ownerTeam, float yPosition, List<Line> lines)
        : base(ownerTeam, ActionObjectType.Barrier, yPosition, lines)
    {
    }

    /// <summary>
    /// Activates the specified train.
    /// </summary>
    /// <param name="train">The train.</param>
    protected override void Activate(Train train)
    {
        train.DecreaseSpeed(speedMultiplier);
        ContextBehaviour.StartCoroutine(ReturnSpeed(train));
    }

    private IEnumerator ReturnSpeed(Train train)
    {
        yield return new WaitForSeconds(workTime);

        train.IncreaseSpeed(speedMultiplier);
        Deactivate();
    }
}
