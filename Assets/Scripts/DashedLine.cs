using UnityEngine;

public class DashedLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3 startPoint; // Punto inicial (como la posición del arma)
    public Vector3 targetPoint; // Punto final (como la dirección del disparo)
    public float maxLineLength = 5f; // Longitud de la línea
    public float maxReflectedLength = 1f;
    public float textureScale = 1f; // Ajusta esto para cambiar el tamaño de los puntos
    public LayerMask obstacleLayer;

    void Start()
    {
        // Configurar el LineRenderer
        lineRenderer.positionCount = 3;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        Vector3 direction = (targetPoint - startPoint).normalized;
        Vector3 endPoint = startPoint + direction * maxLineLength;

        // First Raycast
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, maxLineLength, obstacleLayer);

        if (hit.collider != null)
        {
            // Stop at first obstacle
            Vector3 hitPoint = hit.point;

            // Calculate the reflected direction
            Vector2 reflectedDirection = Vector2.Reflect(direction, hit.normal);
            Vector3 reflectedEndPoint = hitPoint + (Vector3)reflectedDirection * maxReflectedLength;

            // Set line points
            lineRenderer.SetPosition(0, startPoint); // Start
            lineRenderer.SetPosition(1, hitPoint); // First hit
            lineRenderer.SetPosition(2, reflectedEndPoint); // Reflected

            // Adjust dashed effect
            float firstSegmentLength = Vector3.Distance(startPoint, hitPoint);
            float secondSegmentLength = Vector3.Distance(hitPoint, reflectedEndPoint);
            float totalLength = firstSegmentLength + secondSegmentLength;
            lineRenderer.material.mainTextureScale = new Vector2(textureScale * (totalLength / maxLineLength), 1);
        }
        else
        {
            // No hit, just extend normally
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
            lineRenderer.SetPosition(2, endPoint); // Keep the third point at the same end

            lineRenderer.material.mainTextureScale = new Vector2(textureScale, 1);
        }
    }
}
