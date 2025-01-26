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
        sequence.SetLoops(-1, LoopType.Restart);
        float initialYPosition = transform.localPosition.y;
        sequence.Append(transform.DOLocalMoveY(initialYPosition + 0.15f, 0.6f).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveY(initialYPosition, 0.6f).SetEase(Ease.InQuad));
        sequence.Append(transform.DOLocalMoveY(initialYPosition - 0.15f, 0.6f).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveY(initialYPosition, 0.6f).SetEase(Ease.InQuad));
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //Was Deleted
        {
            _bubbleManager.RemoveBubble();
        }
    }
}
