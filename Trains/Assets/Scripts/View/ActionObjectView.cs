using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents action object view.
/// </summary>
public class ActionObjectView : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Sprite arrowSprite;
    [SerializeField]
    private Sprite barrierSprite;
    [SerializeField]
    private Sprite ambushSprite;

    /// <summary>
    /// Initializes the specified action object.
    /// </summary>
    /// <param name="actionObject">The action object.</param>
    /// <param name="positionConverter">The position converter.</param>
    public virtual void Initialize(ActionObject actionObject, PositionConverter positionConverter)
    {
        SetupImage(actionObject);
        transform.position = positionConverter.ConvertModelToViewPosition(actionObject.Position);
        transform.localScale = Vector3.one;
        actionObject.Deactivated += ActionObject_Deactivated;
    }
    
    private void SetupImage(ActionObject actionObject)
    {
        switch (actionObject.ActionObjectType)
        {
            case ActionObjectType.Arrow:
                icon.sprite = arrowSprite;
                break;
            case ActionObjectType.Barrier:
                icon.sprite = barrierSprite;
                break;
            case ActionObjectType.Ambush:
                icon.sprite = ambushSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException("actionObject");
        }

        icon.color = actionObject.OwnerTeam == Team.Red ? Color.red : Color.blue;
    }

    private void ActionObject_Deactivated()
    {
        gameObject.SetActive(false);
    }
}