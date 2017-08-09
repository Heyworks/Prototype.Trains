using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents field view.
/// </summary>
public class FieldView : MonoBehaviour
{
    [SerializeField]
    private TrainView trainPrefab;
    [SerializeField]
    private Transform leftLowerDepo;
    [SerializeField]
    private Transform rightUpperDepo;

    [SerializeField]
    private Text redTeamScore;
    [SerializeField]
    private Text blueTeamScore;
    [SerializeField]
    private ActionObjectView actionObjectViewPrefab;
    [SerializeField]
    private AudioSource ambientSource;

    [SerializeField]
    private Image[] safeZones;

    private GameField field;

    /// <summary>
    /// Gets the position converter.
    /// </summary>
    public PositionConverter PositionConverter { get; private set; }

    /// <summary>
    /// Initializes the field.
    /// </summary>
    /// <param name="field">The field.</param>
    public void InitializeField(GameField field)
    {
        this.field = field;
        PositionConverter = new PositionConverter(leftLowerDepo.position, rightUpperDepo.position,
            field.LeftLowerDepoPosition, field.RightUpperDepoPosition);

        var trains = field.GetTrains();
        foreach (var train in trains)
        {
            var trainView = Instantiate(trainPrefab, transform);
            trainView.Initialize(train, PositionConverter);
        }

        UpdateScore();
        SetupSafeZones();

        ambientSource.Play();
        field.ScoreChanged += Field_ScoreChanged;
        field.ActionObjectAdded += Field_ActionObjectAdded;
    }

    private void SetupSafeZones()
    {
        var sizeDeltaY = PositionConverter.ConvertModelLengthToView(GameSettings.safeZoneSize);
        foreach (var zone in safeZones)
        {
            zone.rectTransform.sizeDelta = new Vector2(0, sizeDeltaY);
        }
    }

    private void CreateActionObjectView(ActionObject actionObject)
    {
        var actionObjectView = Instantiate(actionObjectViewPrefab);
        actionObjectView.transform.SetParent(transform);
        actionObjectView.Initialize(actionObject, PositionConverter);
    }

    private void UpdateScore()
    {
        redTeamScore.text = field.RedTeamScore.ToString();
        blueTeamScore.text = field.BlueTeamScore.ToString();
    }

    private void Field_ActionObjectAdded(ActionObject actionObject)
    {
        CreateActionObjectView(actionObject);
    }

    private void Field_ScoreChanged()
    {
        UpdateScore();
    }
}
