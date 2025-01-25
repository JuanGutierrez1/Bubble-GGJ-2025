using UnityEngine;
using UnityEngine.UI;

public class ProjectileContainerUI : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    private bool _isActive = true;
    public bool isActive { 
        get { return _isActive; } 
    }

    public void DisableProjectile()
    {
        _image.enabled = false;
        _isActive = false;
    }
}
