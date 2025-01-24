using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public new Camera camera;
    public move move;

    public Transform barrelTip;
    public GameObject bullet;

    public float speed = 10f;
    private float lookAngle;

    private Vector3 lastMousePosition;
    public float fireRate = 0.3f;
    private float timeSinceLastShot = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        RotateTowardsMouse();
        MoveOnCircle();
        FireShot();
    }
    private void FireShot(){
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timeSinceLastShot >= fireRate)
        {
            lastMousePosition = Input.mousePosition;
            FireBullet();
            timeSinceLastShot = 0f;
        }
    }

    private void FireBullet()
    {
        // Transforme a posição do último clique do mouse em uma posição no mundo
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(lastMousePosition);
        mouseWorldPosition.z = 0;

        // Calcule a direção da bala
        Vector2 fireDirection = (mouseWorldPosition - barrelTip.position);

        // Defina a velocidade da bala com base na direção e na velocidade
        Vector2 bulletVelocity = fireDirection.normalized * speed;

        // Instancie a bala na posição do "firePoint" com a velocidade calculada
        GameObject firedBullet = Instantiate(bullet, barrelTip.position, Quaternion.identity);
        Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;

        Destroy(firedBullet, 8f);
    }

    private void RotateTowardsMouse()
    {
        float angle = GetAngleTowardsMouse();
        transform.rotation = Quaternion.Euler(0, 0, angle);
        spriteRenderer.flipY = angle >= 90 && angle <= 270;

        Vector3 weaponPosition = transform.position;
        Vector3 playerPosition = move.transform.position;
        Vector3 offset = weaponPosition - playerPosition;
        offset.z = 0;
        offset = offset.normalized * move.sphereRadius;
        weaponPosition = playerPosition + offset;
        transform.position = weaponPosition;
    }

    private float GetAngleTowardsMouse()
    {
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0;
        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;
        return angle;
    }

    private void MoveOnCircle()
    {
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        Vector3 playerToMouse = mouseWorldPosition - move.transform.position;
        playerToMouse = playerToMouse.normalized * move.sphereRadius;

        transform.position = move.transform.position + playerToMouse;

        float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
