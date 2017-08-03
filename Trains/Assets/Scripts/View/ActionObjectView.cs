using System;
using System.Collections;
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
    private Sprite barrierSprite;
    [SerializeField]
    private Sprite ambushSprite;

    [SerializeField]
    private float alfaBeforInstall;
    private PositionConverter positionConverter;

    /// <summary>
    /// Initializes the specified action object.
    /// </summary>
    /// <param name="actionObject">The action object.</param>
    /// <param name="positionConverter">The position converter.</param>
    public virtual void Initialize(ActionObject actionObject, PositionConverter positionConverter)
    {
        this.positionConverter = positionConverter;
        transform.position = positionConverter.ConvertModelToViewPosition(actionObject.Position);
        transform.localScale = Vector3.one;
        transform.SetAsLastSibling();
        SetupImage(actionObject);
        actionObject.Deactivated += ActionObject_Deactivated;
        StartCoroutine(WaitForInstallCoroutine(actionObject));
    }
    
    private void SetupImage(ActionObject actionObject)
    {
        switch (actionObject.ActionObjectType)
        {
            case ActionObjectType.Arrow:
                DrawLine((ArrowActionObject)actionObject);
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

        var color = actionObject.OwnerTeam == Team.Red ? Color.red : Color.blue;
        color.a = alfaBeforInstall;
        icon.color = color;
    }

    private void DrawLine(ArrowActionObject arrowActionObject)
    {
        var pointA = positionConverter.ConvertModelToViewPosition(arrowActionObject.Position);
        var pointB = positionConverter.ConvertModelToViewPosition(arrowActionObject.AimPosition);
        Vector3 differenceVector = pointB - pointA;
        icon.sprite = null;
        icon.rectTransform.sizeDelta = new Vector2(differenceVector.magnitude, 20);
        icon.rectTransform.pivot = new Vector2(0, 0.5f);
        icon.rectTransform.position = pointA;
        var angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        icon.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        transform.SetAsFirstSibling();
    }

    private IEnumerator WaitForInstallCoroutine(ActionObject actionObject)
    {
        while (!actionObject.IsInstalled)
        {
            yield return null;
        }

        var color = icon.color;
        color.a = 1;
        icon.color = color;
    }

    private void ActionObject_Deactivated()
    {
        gameObject.SetActive(false);
    }
}