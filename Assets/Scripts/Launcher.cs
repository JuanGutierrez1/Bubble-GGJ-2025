using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launcher : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;

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

    private Rigidbody2D _currentProjectile;

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
        _currentProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, transform);
        _currentProjectile.transform.localRotation = Quaternion.Euler(0, 0, 90);
        _currentProjectile.transform.localPosition = new Vector2(0.72f, 0);
    }

    public void ProjectileDestroyed()
    {
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

        _currentProjectile.bodyType = RigidbodyType2D.Dynamic;

        Vector2 direction = (mousePos - transform.position).normalized;
        _currentProjectile.AddForceAtPosition(direction * _speed, transform.position);
        projectile.Setup(this);
        _currentProjectile.transform.parent = null;
        _currentProjectile = null;
    }
}
