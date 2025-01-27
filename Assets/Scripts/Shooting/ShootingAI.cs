using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAI : MonoBehaviour
{
    public enum Side
    {
        Evil,
        Good,
        Neutral
    }
    public Side side;

    public Projectile projectile;

    private MapArenor map;
    private Player player;

    public bool shootProjectile;

    private float rotationProjectile;

    public float shootFrequenzy;

    private float shootTimer;

    public int baseDamage;

    private void Awake()
    {
        map = FindObjectOfType<MapArenor>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (side == Side.Evil)
            Shoot(player, Projectile.ProjectileType.Evil);

        if (side == Side.Good)
        {

        }
        if (side == Side.Neutral)
        {

        }
    }

    private void Shoot(Player target, Projectile.ProjectileType type)
    {
        if (shootTimer >= shootFrequenzy)
        {
            shootProjectile = true;
            shootTimer = 0;
        }
        else
        {
            shootTimer += Time.deltaTime;
        }

        var targetPosX = target.transform.position.x;
        var targetPosY = target.transform.position.y;

        var differenceX = player.transform.position.x - transform.position.x;
        var differenceY = player.transform.position.y - transform.position.y;

        var diffX = 0f;
        var diffY = 0f;

        if (differenceX < 0)
            diffX = -differenceX;
        else
            diffX = differenceX;

        if (differenceY < 0)
            diffY = -differenceY;
        else
            diffY = differenceY;

        if (shootProjectile && diffX < 10 && diffY < 10)
        {
            if ((differenceX) > 0)
                rotationProjectile = Mathf.Atan((differenceY) / (differenceX)) * Mathf.Rad2Deg;
            else if ((differenceX) < 0)
                rotationProjectile = Mathf.Atan((differenceY) / (differenceX)) * Mathf.Rad2Deg + 180f;

            projectile.endPos = new Vector2(targetPosX, targetPosY);

            projectile.projectileType = type;

            projectile.damage = baseDamage;

            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, rotationProjectile), this.gameObject.transform);

            shootProjectile = false;
        }
    }
}
