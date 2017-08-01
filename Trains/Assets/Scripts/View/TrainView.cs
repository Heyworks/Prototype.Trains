using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the train view.
/// </summary>
public class TrainView : MonoBehaviour
{
    [SerializeField]
    private Image trainImage;
    [SerializeField]
    private Sprite triangle;
    [SerializeField]
    private Sprite square;
    [SerializeField]
    private Sprite circle;

    /// <summary>
    /// Initializes the specified train.
    /// </summary>
    /// <param name="train">The train.</param>
    public void Initialize(Train train)
    {
        trainImage.color = train.Team == Team.Red ? Color.red : Color.blue;
        switch (train.Live)
        {
            case 1:
                trainImage.sprite = triangle;
                trainImage.transform.localScale = Vector3.one;
                break;
            case 2:
                trainImage.sprite = circle;
                trainImage.transform.localScale = new Vector3(1.1f, 1.1f);
                break;
            case 3:
                trainImage.sprite = square;
                trainImage.transform.localScale = new Vector3(1.2f, 1.2f);
                break;
            default:
                throw new Exception(string.Format("Can't find figure for train with live : {0}", train.Live));
        }
    }
}
