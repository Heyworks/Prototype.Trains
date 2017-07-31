using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private GameField gameField;

    private void Start()
    {
        gameField = new GameField();
        gameField.StartMovement();
        StartCoroutine(TickCoroutine());
    }

    private IEnumerator TickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            gameField.Tick(1f);
        }
    }
}