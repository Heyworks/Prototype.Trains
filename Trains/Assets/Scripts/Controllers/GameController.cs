using System.Linq;
using UnityEngine;

/// <summary>
/// Represents controller for all game states.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField]
    private TimeController timeController;
    [SerializeField]
    private TrainsInitController trainsInitController;
    //TODO move to another place. (Will be solved by DI integration)
    [SerializeField]
    private FieldView fieldView;
    [SerializeField]
    private PanelView redPanelView;
    [SerializeField]
    private PanelView bluePanelView;

    private TrainsInitializer[] initializers;

    private void Start()
    {
        ActivateInitState();
    }

    private void ActivateInitState()
    {
        initializers = new[] { new TrainsInitializer(Team.Red), new TrainsInitializer(Team.Blue) };
        trainsInitController.InitFinished += TrainsInitController_InitFinished;
        trainsInitController.Initialize(initializers, GameSettings.teamInitTime);
    }

    private void ActivateGameFieldState()
    {
        var gameField = new GameField(initializers[0].Trains, initializers[1].Trains);
        gameField.StartMovement();
        fieldView.InitializeField(gameField);
        var panel = new ActionsPanel(gameField);
        redPanelView.Initialize(panel.Slots.Where(item => item.Team == Team.Red).ToArray(), fieldView.PositionConverter);
        bluePanelView.Initialize(panel.Slots.Where(item => item.Team == Team.Blue).ToArray(), fieldView.PositionConverter);
        timeController.StartTicks(gameField);
    }

    private void TrainsInitController_InitFinished()
    {
        ActivateGameFieldState();
    }
}
