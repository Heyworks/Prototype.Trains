using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private int updateRate = 10;
    
    /// <summary>
    /// Starts the ticks.
    /// </summary>
    /// <param name="gameField">The game field.</param>
    public void StartTicks(GameField gameField)
    {
        StartCoroutine(TickCoroutine(gameField));
    }
    
    private IEnumerator TickCoroutine(GameField gameField)
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