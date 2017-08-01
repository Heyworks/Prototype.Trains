using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents field view.
/// </summary>
public class FieldView : MonoBehaviour
{
    public static FieldView fieldView;

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

    private GameField field;

    private void Awake()
    {
        fieldView = this;
    }

    /// <summary>
    /// Initializes the field.
    /// </summary>
    /// <param name="field">The field.</param>
    public void InitializeField(GameField field)
    {
        this.field = field;
        var positionConverter = new PositionConverter(leftLowerDepo.position, rightUpperDepo.position,
            field.LeftLowerDepoPosition, field.RightUpperDepoPosition);

        var trains = field.GetTrains();
        foreach (var train in trains)
        {
            var trainView = Instantiate(trainPrefab, transform);
            trainView.Initialize(train, positionConverter);
        }

        UpdateScore();
        field.ScoreChanged += Field_ScoreChanged;
    }

    private void Field_ScoreChanged()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        redTeamScore.text = field.RedTeamScore.ToString();
        blueTeamScore.text = field.BlueTeamScore.ToString();
    }
}
