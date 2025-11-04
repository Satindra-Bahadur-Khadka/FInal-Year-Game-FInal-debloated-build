using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab; // The projectile prefab to be instantiated



    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        Vector3 origScale = projectile.transform.localScale;

        //flip projectile with player
        // Flip projectile if player is facing left
        float flipDirection = transform.localScale.x >= 0 ? 1f : -1f;
        projectile.transform.localScale = new Vector3(origScale.x * flipDirection, origScale.y, origScale.z);
    }
}
