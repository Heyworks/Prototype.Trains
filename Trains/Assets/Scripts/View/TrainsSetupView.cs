using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents setup view for one team trains.
/// </summary>
public class TrainsSetupView : MonoBehaviour
{
    [SerializeField]
    private GameObject back;
    [SerializeField]
    private GameObject slotsRoot;
    [SerializeField]
    private Text timer;
    [SerializeField]
    private Text cargo;
    [SerializeField]
    private TrainsSetupSlotView[] slotViews;

    private TrainsInitializer trainsInitializer;

    private void Awake()
    {
        foreach (var slot in slotViews)
        {
            slot.Swapped += Slot_Swapped;
        }
    }

    public void ShowSetup(TrainsInitializer initializer, float timeForInit)
    {
        if (trainsInitializer != null)
        {
            trainsInitializer.FreeCargoChanged -= TrainsInitializer_FreeCargoChanged; 
        }
        trainsInitializer = initializer;
        StopAllCoroutines();
        StartCoroutine(TimerCoroutine(timeForInit));
        SetupSlots();
        UpdateCargo();
        trainsInitializer.FreeCargoChanged += TrainsInitializer_FreeCargoChanged;
    }

    private void UpdateCargo()
    {
        cargo.text = string.Format("Free cargo : {0}", trainsInitializer.FreeCargo);
    }

    private void SetupSlots()
    {
        for (int i = 0; i < slotViews.Length; i++)
        {
            slotViews[i].Setup(i, trainsInitializer);
        }
    }

    /// <summary>
    /// Activates this view.
    /// </summary>
    public void Activate()
    {
        slotsRoot.SetActive(true);
    }

    /// <summary>
    /// Deactivates this view.
    /// </summary>
    public void Deactivate()
    {
        slotsRoot.SetActive(false);
    }

    /// <summary>
    /// Shows the back.
    /// </summary>
    public void ShowBack()
    {
        back.SetActive(true);
    }

    /// <summary>
    /// Hides the back.
    /// </summary>
    public void HideBack()
    {
        back.SetActive(false);
    }

    private void Slot_Swapped(SwapData data)
    {
        SwapTrains(data);
    }

    private void SwapTrains(SwapData data)
    {
        var nextSlot = data.IsRightSwap ? data.SlotIndex + 1 : data.SlotIndex - 1;
        if (nextSlot < 0)
        {
            nextSlot = slotViews.Length - 1;
        }

        if (nextSlot >= slotViews.Length)
        {
            nextSlot = 0;
        }

        trainsInitializer.Swap(data.SlotIndex, nextSlot);

        foreach (var slot in slotViews)
        {
            slot.UpdateSlot();
        }
    }

    private IEnumerator TimerCoroutine(float timeLeft)
    {
        var delay = 1f;
        while (timeLeft > 0)
        {
            timer.text = string.Format("Time left for {0} team : {1}", trainsInitializer.Team, timeLeft);
            timeLeft -= delay;
            yield return new WaitForSeconds(delay);
        }

        timer.text = string.Empty;
    }

    private void TrainsInitializer_FreeCargoChanged()
    {
        UpdateCargo();
    }
}
