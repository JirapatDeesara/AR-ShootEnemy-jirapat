using UnityEngine;

public class ShootControl : MonoBehaviour
{
    // Reference
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform  cameraTransform;

    //settings
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifeTime = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UImanager.OnUIShootButtonPressed += Shoot;
    }

    // Update is called once per frame
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, cameraTransform.position, Quaternion.identity);
        var rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(cameraTransform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, bulletLifeTime);
    }
}// End Shootcontrol
