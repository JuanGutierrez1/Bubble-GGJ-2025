using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Launcher : MonoBehaviour
{
    private BubbleManager _bubbleManager;
    private AudioManager _audioManager;

    [Inject]
    public void Constructor(BubbleManager bubbleManager, AudioManager audioManager)
    {
        _bubbleManager = bubbleManager;
        _audioManager = audioManager;
    }

    private Camera mainCamera;
    private Vector3 mousePos;

    [SerializeField]
    private AudioSource _launchSFX;

    [SerializeField]
    private bool _infinite;

    [SerializeField]
    private int _initialProjectiles;
    public int InitialProjectiles
    {
        get => _initialProjectiles;
    }

    private int _projectiles;
    public int Projectiles
    {
        get => _projectiles;
        set => _projectiles = value;
    }

    public Action<int> onProjectileUsed;

    [SerializeField]
    private Rigidbody2D _projectilePrefab;

    private Rigidbody2D _currentProjectile, _previousProjectile;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private DashedLine _line;


    private void Awake()
    {
        mainCamera = Camera.main;
        _projectiles = InitialProjectiles;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnProjectile();

    }

    private void SpawnProjectile()
    {
        if (!enabled) return;
        _currentProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, transform);
        _currentProjectile.transform.localRotation = Quaternion.Euler(0, 0, 90);
        _currentProjectile.transform.localPosition = new Vector2(0.72f, 0);
    }

    public void ProjectileDestroyed()
    {
        if(_projectiles <= 0 && !_infinite)
        {
            _bubbleManager.OnLevelLose?.Invoke();
            return;
        }
        SpawnProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        transform.right = mousePos - transform.position;

        _line.startPoint = transform.position;
        _line.targetPoint = mousePos;


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            LaunchProjectile();
        }
    }

    public void LaunchProjectile()
    {
        if ((Projectiles <= 0 && !_infinite) || _currentProjectile == null) return;
        _projectiles--;
        onProjectileUsed?.Invoke(_projectiles);
        Projectile projectile = _currentProjectile.GetComponent<Projectile>();

        _launchSFX.Play();
        _currentProjectile.bodyType = RigidbodyType2D.Dynamic;

        Vector2 direction = (mousePos - transform.position).normalized;
        _currentProjectile.AddForceAtPosition(direction * _speed, transform.position);
        projectile.Setup(this);
        _currentProjectile.transform.parent = null;
        _previousProjectile = _currentProjectile;
        _currentProjectile = null;
    }

    private void DisableLauncher()
    {
        enabled = false;
        _line.maxLineLength = 0;
        if(_previousProjectile != null)
        {
            Destroy(_previousProjectile.gameObject);
        }
    }

    private void OnEnable()
    {
        _bubbleManager.OnLevelWin += DisableLauncher;
        _bubbleManager.OnLevelLose += DisableLauncher;
    }

    private void OnDisable()
    {
        _bubbleManager.OnLevelWin -= DisableLauncher;
        _bubbleManager.OnLevelLose -= DisableLauncher;
    }
}
