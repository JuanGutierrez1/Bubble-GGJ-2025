using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileDisplay : MonoBehaviour
{
    [SerializeField]
    private Launcher launcher;

    [SerializeField]
    private TextMeshProUGUI projectileCounterTMP;

    private void Start()
    {
        projectileCounterTMP.text = $"x{launcher.InitialProjectiles}";
    }

    private void OnEnable()
    {
        launcher.onProjectileUsed += UpdateDisplay;
    }

    private void UpdateDisplay(int remainingProjectiles)
    {
        projectileCounterTMP.text = $"x{launcher.Projectiles}";
    }
}
