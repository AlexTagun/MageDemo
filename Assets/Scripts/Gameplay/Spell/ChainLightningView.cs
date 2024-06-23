using UnityEngine;

public class ChainLightningView : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    public void Show(Vector3[] points)
    {
        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
        Destroy(gameObject, 0.3f);
    }
}
