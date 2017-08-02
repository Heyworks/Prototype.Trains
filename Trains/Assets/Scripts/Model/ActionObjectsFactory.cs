using UnityEngine;

/// <summary>
/// Represents action objects factory.
/// </summary>
public class ActionObjectsFactory
{
    private GameField gameField;

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
    /// <param name="position">The position.</param>
    public ActionObject CreateActionObject(ActionObjectType type, Vector2 position)
    {
        //TODO
        return null;
    }
}
