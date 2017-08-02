using System;
using UnityEngine;

/// <summary>
/// Represents action objects factory.
/// </summary>
public class ActionObjectsFactory
{
    private readonly GameField gameField;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionObjectsFactory"/> class.
    /// </summary>
    /// <param name="gameField">The game field.</param>
    public ActionObjectsFactory(GameField gameField)
    {
        this.gameField = gameField;
    }

    /// <summary>
    /// Creates the action object.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="team">The team.</param>
    /// <param name="position">The position.</param>
    public ActionObject CreateActionObject(ActionObjectType type, Team team, Vector2 position)
    {
        var sortedLines = gameField.GetLinesSortedByDistance(position);
        switch (type)
        {
            case ActionObjectType.Arrow:
                return null;
            case ActionObjectType.Barrier:
                return new BarrierActionObject(team, position.y, sortedLines);
            case ActionObjectType.Ambush:
                return new AmbushActionObject(team, position.y, sortedLines);
            default:
                throw new ArgumentOutOfRangeException("type");
        }
    }
}
