using UnityEngine;
using Zenject;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    private BubbleManager _bubbleManager;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Collider2D collider;

    [SerializeField]
    private ParticleSystem particles;

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

    public void ExplodeBubble()
    {
        animator.SetTrigger("Explode");

        _bubbleManager.RemoveBubble();
        collider.enabled = false;
        particles.Stop();
    }
}
