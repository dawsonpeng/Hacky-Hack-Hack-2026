using UnityEngine;

public class cannonscript : MonoBehaviour
{
    public enum ShootDirection
    {
        Up,
        Down
    }

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 6f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float projectileGravityScale = 1f;

    [Header("Firing")]
    [SerializeField] private ShootDirection shootDirection = ShootDirection.Up;
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
            Fire();
            fireTimer = adjustedInterval;
        }
    }

    private void Fire()
    {
        Vector2 dir = GetDirectionVector();
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        // Keep the cannon in front without relying on sorting layers.
        if (TryGetComponent<SpriteRenderer>(out var cannonRenderer))
        {
            if (projectile.TryGetComponent<SpriteRenderer>(out var projectileRenderer))
            {
                projectileRenderer.sortingOrder = cannonRenderer.sortingOrder - 1;
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
            rb.useGravity = true;
        }
    }

    private Vector2 GetDirectionVector()
    {
        switch (shootDirection)
        {
            case ShootDirection.Up:
                return Vector2.up;
            case ShootDirection.Down:
                return Vector2.down;
            default:
                return Vector2.up;
        }
    }
}
