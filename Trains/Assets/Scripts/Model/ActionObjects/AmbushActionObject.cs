using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents ambush action object which remove cargo from train.
/// </summary>
public class AmbushActionObject : ActionObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AmbushActionObject"/> class.
    /// </summary>
    /// <param name="ownerTeam">The owner team.</param>
    /// <param name="yPosition">The y position.</param>
    /// <param name="lines">The lines.</param>
    public AmbushActionObject(Team ownerTeam, float yPosition, List<Line> lines)
        : base(ownerTeam, ActionObjectType.Ambush, yPosition, lines)
    {
    }

    /// <summary>
    /// Activates the specified train.
    /// </summary>
    /// <param name="train">The train.</param>
    protected override void Activate(Train train)
    {
        train.RemoveCargo(1);
        ContextBehaviour.StartCoroutine(DeactivateCoroutine());
    }

    private IEnumerator DeactivateCoroutine()
    {
        yield return null;

        Deactivate();        
    }
}
