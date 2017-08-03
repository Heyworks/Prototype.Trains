using UnityEngine;

/// <summary>
/// Represent converter from model coordinates to view and back.
/// </summary>
public class PositionConverter
{
    private readonly float xDelta;
    private readonly float yDelta;
    private Vector2 lowerLeftDepoViewPosition;
    private Vector2 lowerLeftDepoModelPosition;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PositionConverter"/> class.
    /// </summary>
    /// <param name="lowerLeftDepoViewPosition">The lower left depo view position.</param>
    /// <param name="upperRightDepoViewPosition">The upper right depo view position.</param>
    /// <param name="lowerLeftDepoModelPosition">The lower left depo model position.</param>
    /// <param name="upperRightDepoModelPosition">The upper right depo model position.</param>
    public PositionConverter(Vector2 lowerLeftDepoViewPosition, Vector2 upperRightDepoViewPosition,
        Vector2 lowerLeftDepoModelPosition, Vector2 upperRightDepoModelPosition)
    {
        xDelta = (upperRightDepoViewPosition.x - lowerLeftDepoViewPosition.x) /
                 (upperRightDepoModelPosition.x - lowerLeftDepoModelPosition.x);
        yDelta = (upperRightDepoViewPosition.y - lowerLeftDepoViewPosition.y) /
                 (upperRightDepoModelPosition.y - lowerLeftDepoModelPosition.y);

        this.lowerLeftDepoViewPosition = lowerLeftDepoViewPosition;
        this.lowerLeftDepoModelPosition = lowerLeftDepoModelPosition;
    }

    /// <summary>
    /// Converts the model to view position.
    /// </summary>
    /// <param name="modelPosition">The model position.</param>
    public Vector2 ConvertModelToViewPosition(Vector2 modelPosition)
    {
        var viewX = lowerLeftDepoViewPosition.x + xDelta * (modelPosition.x - lowerLeftDepoModelPosition.x);
        var viewY = lowerLeftDepoViewPosition.y + yDelta * (modelPosition.y - lowerLeftDepoModelPosition.y);
        var viewPosition = new Vector2(viewX, viewY);

        return viewPosition;
    }

    /// <summary>
    /// Converts the view to model position.
    /// </summary>
    /// <param name="viewPosition">The view position.</param>
    public Vector2 ConvertViewToModelPosition(Vector2 viewPosition)
    {
        var viewX = lowerLeftDepoModelPosition.x + (viewPosition.x - lowerLeftDepoViewPosition.x) / xDelta;
        var viewY = lowerLeftDepoModelPosition.y + (viewPosition.y - lowerLeftDepoViewPosition.y) / yDelta;

        return new Vector2(viewX, viewY);
    }
}
