using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image _fill;

    public void SetHealth(float fill)
    {
        _fill.fillAmount = fill;
    }
}