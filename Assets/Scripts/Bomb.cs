using UnityEngine;
using Zenject;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    private BubbleManager _bubbleManager;

    [Inject]
    public void Constructor(BubbleManager bubbleManager)
    {
        _bubbleManager = bubbleManager;
    }

    private void Start()
    {
        float randomTime = Random.Range(0.6f, 1.2f);
        float randomPosition = Random.Range(0.1f, 0.2f);

        Sequence sequence = DOTween.Sequence();
        sequence.SetLoops(-1, LoopType.Restart);
        float initialYPosition = transform.localPosition.y;
        sequence.Append(transform.DOLocalMoveY(initialYPosition + randomPosition, randomTime).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveY(initialYPosition, randomTime).SetEase(Ease.InQuad));
        sequence.Append(transform.DOLocalMoveY(initialYPosition - randomPosition, randomTime).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveY(initialYPosition, randomTime).SetEase(Ease.InQuad));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            _bubbleManager.OnLevelLose?.Invoke();
            Destroy(gameObject);
        }
    }
}
