using System.Collections;
using System.Linq;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private int updateRate = 10;

    [SerializeField]
    private PanelView redPanelView;
    [SerializeField]
    private PanelView bluePanelView;
    private GameField gameField;

    private void Start()
    {
        gameField = new GameField();
        gameField.StartMovement();
        FieldView.fieldView.InitializeField(gameField);
        TestActions();
        StartCoroutine(TickCoroutine());
    }

    private void TestActions()
    {
        var panel = new ActionsPanel(gameField);
        redPanelView.Initialize(panel.Slots.Where(item => item.Team == Team.Red).ToArray(), FieldView.fieldView.PositionConverter);
        bluePanelView.Initialize(panel.Slots.Where(item => item.Team == Team.Blue).ToArray(), FieldView.fieldView.PositionConverter);
    }

    private IEnumerator TickCoroutine()
    {
        var waitTime = 1f / updateRate;
        var waitYield = new WaitForSeconds(waitTime);
        while (true)
        {
            yield return waitYield;

            gameField.Tick(waitTime);
        }
    }
}