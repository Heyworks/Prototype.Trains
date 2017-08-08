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
    private Text cargo;
    [SerializeField]
    private Sprite triangle;
    [SerializeField]
    private Sprite square;
    [SerializeField]
    private Sprite circle;

    private PositionConverter positionConverter;
    private Train train;

    /// <summary>
    /// Initializes the specified train.
    /// </summary>
    /// <param name="train">The train.</param>
    /// <param name="positionConverter">The position converter.</param>
    public void Initialize(Train train, PositionConverter positionConverter)
    {
        this.positionConverter = positionConverter;
        this.train = train;
        train.Attacked += Train_Attacked;
        train.Crashed += Train_Crashed;
        UpdateStats();
        SetupTrainImage(train);
    }

    /// <summary>
    /// Shows the train data.
    /// </summary>
    /// <param name="train">The train.</param>
    public void ShowTrainData(Train train)
    {
        cargo.text = train.Cargo.ToString();
        SetupTrainImage(train);
    }

    private void Update()
    {
        if (train != null)
        {
            transform.position = positionConverter.ConvertModelToViewPosition(train.Position);
        }
    }

    private void SetupTrainImage(Train train)
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

    private void Train_Crashed()
    {
        PlayAttackedEffect();
        PlayCrashedEffect();
    }

    private void PlayCrashedEffect()
    {
        trainImage.color = Color.black;
    }

    private void Train_Attacked()
    {
        UpdateStats();
        PlayAttackedEffect();
    }

    private void PlayAttackedEffect()
    {
        var scale = transform.localScale;
        var time = 0.25f;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", scale * 1.5f, "easeType", "easeInOutExpo", "time", time));
        iTween.ScaleTo(gameObject, iTween.Hash("scale", scale, "easeType", "easeInOutExpo", "time", time, "delay", time * 1.1f));
    }

    private void UpdateStats()
    {
        cargo.text = train.Cargo.ToString();
    }
}
