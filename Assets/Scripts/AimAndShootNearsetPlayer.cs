using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class AimAndShootNearsetPlayer : MonoBehaviour
{
    // AIM
    public float gunRotationSpeed = 10f;

    // SHOOT
    public GameObject bulletPrefab;
    [SerializeField] private Transform bulletTransform;
    public float shootingInterval = 2f;
    private float nextShootTime = 0f;

    public void AimAtNearestPlayer(Transform nearestPlayerTransform, Transform gunTransform) {

        Vector3 direction = nearestPlayerTransform.position - gunTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gunTransform.rotation = Quaternion.Lerp(gunTransform.rotation, targetRotation, Time.deltaTime * gunRotationSpeed);

        if (Time.time >= nextShootTime) {
            ShootAtNearestPlayer(direction, targetRotation);
            nextShootTime = Time.time + shootingInterval; // Set the next shooting time
        }

    } // AimAtNearestPlayer

    private void ShootAtNearestPlayer(Vector3 direction, Quaternion targetRotation) {
        GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, targetRotation);
        bullet.GetComponent<CanonBulletScript>().ApplyForceOnBullet(direction);
    } // ShootAtNearestPlayer

} // Class