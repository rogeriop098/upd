using UnityEngine;

public class BulletShoter : MonoBehaviour
{
    public GameObject bulletPrefab;//prefab da bala
    public Transform firePoint;//ponto de disparo
    public float bulletSpeed = 100f;//velocidade da bala



    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}
