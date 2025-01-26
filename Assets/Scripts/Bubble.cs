using UnityEngine;
using Zenject;
using DG.Tweening;

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

        Sequence sequence = DOTween.Sequence();
        //sequence.Append()

        transform.DOLocalMoveY(transform.localPosition.y + 0.3f, 3.5f).SetEase(Ease.InOutQuart).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //Was Deleted
        {
            _bubbleManager.RemoveBubble();
        }
    }
}
