using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private int updateRate = 10;
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
        panel.Slots[0].InstallObject(new Vector2(1, 3));
        panel.Slots[2].InstallObject(new Vector2(1, 1));
    }

    private IEnumerator TickCoroutine()
    {
        var waitTime = 1f/updateRate;
        var waitYield = new WaitForSeconds(waitTime);
        while (true)
        {
            yield return waitYield;

            gameField.Tick(waitTime);
        }
    }
}