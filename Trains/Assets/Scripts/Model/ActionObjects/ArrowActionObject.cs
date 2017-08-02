using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an arrow action object which change line for train.
/// </summary>
public class ArrowActionObject : ActionObject
{
    private Line aimLine;
    private float yDelta;
    private Vector2 destination;
    private float yMin;
    private float yMax;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrowActionObject" /> class.
    /// </summary>
    /// <param name="ownerTeam">The owner team.</param>
    /// <param name="yPosition">The y position.</param>
    /// <param name="lines">The lines.</param>
    /// <param name="yMin">The y minimum.</param>
    /// <param name="yMax">The y maximum.</param>
    public ArrowActionObject(Team ownerTeam, float yPosition, List<Line> lines, float yMin, float yMax) : base(ownerTeam, ActionObjectType.Arrow, yPosition, lines)
    {
        yDelta = (yMax - yMin) / 10 * (ownerTeam == Team.Blue ? -1 : 1);
        
        this.yMin = yMin;
        this.yMax = yMax;
        aimLine = lines[1];
    }

    protected override Team GetRequiredTrainTeam()
    {
        return OwnerTeam;
    }

    protected override void Activate(Train train)
    {
        destination = new Vector2(aimLine.XCoordinate, train.Position.y + yDelta);
        destination.y = Math.Max(yMin, destination.y);
        destination.y = Math.Min(yMax, destination.y);

        train.Move(destination);
        ContextBehaviour.StartCoroutine(CheckTrainPositionCoroutine(train));
    }

    private IEnumerator CheckTrainPositionCoroutine(Train train)
    {
        var distance = float.PositiveInfinity;
        while (distance > Constants.COLLISION_DISTANCE)
        {
            distance = (train.Position - destination).magnitude;
            yield return null;
        }

        AssignedLine.RemoveTrain(train);
        aimLine.AddTrain(train);
        train.Move(train.Position + new Vector2(0, yDelta));
        Deactivate();
    }
}