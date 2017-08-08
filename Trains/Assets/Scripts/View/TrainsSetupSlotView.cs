using System;
using UnityEngine;

public class TrainsSetupSlotView : MonoBehaviour
{
    [SerializeField]
    private TrainView trainView;
    [SerializeField]
    private GameInput input;
    
    private Train train;
    private int index;
    private TrainsInitializer initializer;

    /// <summary>
    /// Occurs when slot item was swapped.
    /// </summary>
    public event Action<SwapData> Swapped;

    private void Awake()
    {
        input.Tap += Input_Tap;
        input.DoubleTap += Input_DoubleTap;
    }
    
    /// <summary>
    /// Setups the specified slot index.
    /// </summary>
    /// <param name="slotIndex">Index of the slot.</param>
    /// <param name="initializer">The initializer.</param>
    public void Setup(int slotIndex, TrainsInitializer initializer)
    {
        train = initializer.Trains[slotIndex];
        index = slotIndex;
        this.initializer = initializer;
        UpdateSlot();
    }

    /// <summary>
    /// Updates the slot.
    /// </summary>
    public void UpdateSlot()
    {
        if (train != null)
        {
            trainView.ShowTrainData(train);
        }
    }

    private void Input_DoubleTap()
    {
        initializer.RemoveCargoFromTrain(train);
        UpdateSlot();
    }

    private void Input_Tap()
    {
        initializer.AddCargoToTrain(train);
        UpdateSlot();
    }

    private void OnSwapped(SwapData obj)
    {
        var handler = Swapped;
        if (handler != null)
        {
            handler(obj);
        }
    }
}