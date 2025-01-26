using UnityEngine;
using Zenject;

public class Bubble : MonoBehaviour
{
    private BubbleManager _bubbleManager;

    [Inject]
    public void Constructor(BubbleManager bubbleManager)
    {
        _bubbleManager = bubbleManager;
    }

    private void Start()
    {
        _bubbleManager.AddBubble();
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //Was Deleted
        {
            _bubbleManager.RemoveBubble();
        }
    }
}
