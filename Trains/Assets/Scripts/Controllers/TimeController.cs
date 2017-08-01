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
        StartCoroutine(TickCoroutine());
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