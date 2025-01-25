using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDisplay : MonoBehaviour
{
    [SerializeField]
    private Launcher launcher;

    [SerializeField]
    private ProjectileContainerUI projectileContainer;

    private List<ProjectileContainerUI> projectiles = new();

    private void Start()
    {
        for (int i = 0; i < launcher.InitialProjectiles; i++)
        {
            projectiles.Add(Instantiate(projectileContainer, transform.position, Quaternion.Euler(0, 0, 90), transform));
        }
    }

    private void OnEnable()
    {
        launcher.onProjectileUsed += UpdateDisplay;
    }

    private void UpdateDisplay(int remainingProjectiles)
    {
        for (int i = 0; i < launcher.InitialProjectiles; i++)
        {
            if (i >= launcher.Projectiles)
            {
                projectiles[i].DisableProjectile();
            }
        }
    }
}
