using System.Collections.Generic;

/// <summary>
/// Represents panel with action slots.
/// </summary>
public class ActionsPanel
{
    /// <summary>
    /// Gets the action slots.
    /// </summary>
    public List<ActionObjectSlot> Slots { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionsPanel"/> class.
    /// </summary>
    /// <param name="gameField">The game field.</param>
    public ActionsPanel(GameField gameField)
    {
        var factory = new ActionObjectsFactory(gameField);
        CreateSlots(factory);
    }

    private void CreateSlots(ActionObjectsFactory factory)
    {
        Slots = new List<ActionObjectSlot>();
        Slots.Add(new ActionObjectSlot(ActionObjectType.Ambush, Team.Red, factory));
        Slots.Add(new ActionObjectSlot(ActionObjectType.Barrier, Team.Red, factory));
        Slots.Add(new ActionObjectSlot(ActionObjectType.Arrow, Team.Red, factory));
    }
}
