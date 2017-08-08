using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Controller for trains init
/// </summary>
public class TrainsInitController : MonoBehaviour
{
    private const int DELAY_BETWEEN_TEAMS = 5;

    [SerializeField]
    private TrainsSetupView trainsSetupView;

    /// <summary>
    /// Occurs when initialize has been finished.
    /// </summary>
    public event Action InitFinished;

    /// <summary>
    /// Initializes the specified red trains initializer.
    /// </summary>
    /// <param name="trainsInitializers">The trains initializers.</param>
    /// <param name="timeForInit">The time for single initialize.</param>
    public void Initialize(TrainsInitializer[] trainsInitializers, float timeForInit)
    {
        StartCoroutine(InitializeCoroutine(trainsInitializers, timeForInit));
    }

    private IEnumerator InitializeCoroutine(TrainsInitializer[] trainsInitializers, float timeForInit)
    {
        trainsSetupView.ShowBack();
        foreach (var initializer in trainsInitializers)
        {
            trainsSetupView.Activate();

            trainsSetupView.ShowSetup(initializer, timeForInit);

            yield return new WaitForSeconds(timeForInit);

            trainsSetupView.Deactivate();

            yield return new WaitForSeconds(DELAY_BETWEEN_TEAMS);
        }

        trainsSetupView.HideBack();
        OnInitFinished();
    }
    
    private void OnInitFinished()
    {
        var handler = InitFinished;
        if (handler != null)
        {
            handler();
        }
    }
}
