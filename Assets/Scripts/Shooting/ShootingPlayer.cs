using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayer : MonoBehaviour
{
    public Raycast raycast;
    public SliderHandling sliderHandling;
    public Projectile projectile;

    public MapArenor map;

    public bool shootProjectile;

    private float rotationProjectile;

    private Vector3 mousePos;

    public int baseDamage;

    private void Update()
    {
        mousePos = raycast.mousePos;

        if (shootProjectile && sliderHandling.readyToFire)
        {
            if ((mousePos.x - transform.position.x) > 0)
            {
                rotationProjectile = Mathf.Atan((mousePos.y - transform.position.y) / (mousePos.x - transform.position.x)) * Mathf.Rad2Deg;
            }
            else if ((mousePos.x - transform.position.x) < 0)
            {
                rotationProjectile = Mathf.Atan((mousePos.y - transform.position.y) / (mousePos.x - transform.position.x)) * Mathf.Rad2Deg + 180f;
            }

            projectile.endPos = new Vector2(mousePos.x, mousePos.y);

            projectile.projectileType = Projectile.ProjectileType.Good;

            if (FindObjectOfType<CheatController>().everthingOneHit)            
                baseDamage = 999999;

            projectile.damage = baseDamage;

            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, rotationProjectile), FindObjectOfType<Player>().transform);

            sliderHandling.readyToFire = false;
        }
    }
}
