using UnityEngine;
using Zenject;

public class Bomb : MonoBehaviour
{
    private BubbleManager _bubbleManager;

    [Inject]
    public void Constructor(BubbleManager bubbleManager)
    {
        _bubbleManager = bubbleManager;
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
