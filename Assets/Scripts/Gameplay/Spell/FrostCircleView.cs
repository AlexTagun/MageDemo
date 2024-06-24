using UnityEngine;

public class FrostCircleView : MonoBehaviour
{
    [SerializeField] private Transform _visual;

    public void SetRadius(float radius)
    {
        _visual.localScale = new Vector3(radius, 0.1f, radius);
    }
}