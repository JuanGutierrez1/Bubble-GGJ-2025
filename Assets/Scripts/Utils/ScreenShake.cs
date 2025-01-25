using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField]
    private float _shakeMagnitude = 0.08f;

    private float _shakeElapsedTime = 0f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (_shakeElapsedTime > 0)
        {
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            _shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake(float shakeDuration = 0.2f)
    {
        _shakeElapsedTime = shakeDuration;
    }
}
