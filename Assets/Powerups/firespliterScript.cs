using UnityEngine;

public class firesplitterScript : MonoBehaviour
{
    public enum ShootMode
    {
        Left,
        Right,
        Both
    }

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 6f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float projectileGravityScale = 0f;

    [Header("Firing")]
    [SerializeField] private ShootMode shootMode = ShootMode.Both;
    [SerializeField] private float fireInterval = 1f;

    private float fireTimer;

    private void Update()
    {
        if (projectilePrefab == null || fireInterval <= 0f)
        {
            return;
        }

        float tickSpeed = Mathf.Max(0.01f, SettingsScript.TickSpeed);
        float fireRateScale = Mathf.Approximately(tickSpeed, 2f) ? 5f : tickSpeed;
        float adjustedInterval = fireInterval * (1f / fireRateScale);

        fireTimer -= Time.unscaledDeltaTime;
        if (fireTimer <= 0f)
        {
            switch (shootMode)
            {
                case ShootMode.Left:
                    Fire(Vector2.left);
                    break;
                case ShootMode.Right:
                    Fire(Vector2.right);
                    break;
                case ShootMode.Both:
                    Fire(Vector2.left);
                    Fire(Vector2.right);
                    break;
            }

            fireTimer = adjustedInterval;
        }
    }

    private void Fire(Vector2 dir)
    {
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        // Keep the splitter in front without relying on sorting layers.
        if (TryGetComponent<SpriteRenderer>(out var splitterRenderer))
        {
            if (projectile.TryGetComponent<SpriteRenderer>(out var projectileRenderer))
            {
                projectileRenderer.sortingOrder = splitterRenderer.sortingOrder - 1;
            }
            else
            {
                projectile.transform.position = new Vector3(
                    projectile.transform.position.x,
                    projectile.transform.position.y,
                    transform.position.z + 1f
                );
            }
        }

        if (projectileLifetime > 0f)
        {
            Destroy(projectile, projectileLifetime);
        }

        if (!projectile.TryGetComponent<ProjectileKillPlayer>(out _))
        {
            projectile.AddComponent<ProjectileKillPlayer>();
        }

        if (projectile.TryGetComponent<Rigidbody2D>(out var rb2d))
        {
            rb2d.linearVelocity = dir * projectileSpeed;
            rb2d.gravityScale = projectileGravityScale;
        }
        else if (projectile.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = new Vector3(dir.x, dir.y, 0f) * projectileSpeed;
            rb.useGravity = false;
        }
    }
}

