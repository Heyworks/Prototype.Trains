using System;
using UnityEngine;

public class TrainsSetupSlotView : MonoBehaviour
{
    [SerializeField]
    private TrainView trainView;
    [SerializeField]
    private GameInput input;
    
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
        input.Drag += Input_Drag;
    }

    /// <summary>
    /// Setups the specified slot index.
    /// </summary>
    /// <param name="slotIndex">Index of the slot.</param>
    /// <param name="initializer">The initializer.</param>
    public void Setup(int slotIndex, TrainsInitializer initializer)
    {
        index = slotIndex;
        this.initializer = initializer;
        UpdateSlot();
    }

    /// <summary>
    /// Updates the slot.
    /// </summary>
    public void UpdateSlot()
    {
        var train = initializer.Trains[index];

        if (train != null)
        {
            trainView.ShowTrainData(train);
        }
    }

    private void Input_DoubleTap()
    {
        initializer.RemoveCargoFromTrain(index);
        UpdateSlot();
    }

    private void Input_Drag(Vector2 delta)
    {
        var isRight = delta.x > 0;
        OnSwapped(new SwapData(index, isRight));
    }
    
    private void Input_Tap()
    {
        initializer.AddCargoToTrain(index);
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