using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    public enum ProjectileType
    {
        Evil,
        Good
    }
    public ProjectileType projectileType;

    public Fire fire;

    public Vector2 startPos;
    public Vector2 endPos;

    public float speed;

    public int damage;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        var diffrenceX = endPos.x - startPos.x;
        var diffrenceY = endPos.y - startPos.y;

        if (diffrenceX > 0)
        {
            if (diffrenceY > 0)
                if (transform.position.x >= endPos.x && transform.position.y >= endPos.y)
                    buildFireDestoryThis();

            if (diffrenceY < 0)
                if (transform.position.x >= endPos.x && transform.position.y <= endPos.y)
                    buildFireDestoryThis();
        }

        if (diffrenceX < 0)
        {
            if (diffrenceY > 0)
                if (transform.position.x <= endPos.x && transform.position.y >= endPos.y)
                    buildFireDestoryThis();

            if (diffrenceY < 0)
                if (transform.position.x <= endPos.x && transform.position.y <= endPos.y)
                    buildFireDestoryThis();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Tree>())
        {
            buildFireDestoryThis();
            collision.GetComponent<Tree>().health -= damage;
        }
        if (collision.gameObject.GetComponent<Wall>())
        {
            buildFireDestoryThis();
            collision.GetComponent<Wall>().health -= damage;
        }
        if (collision.gameObject.GetComponent<Portal>())
        {
            collision.GetComponent<Portal>().health -= damage;

            buildFireDestoryThis();
        }

        if (collision.gameObject.GetComponent<Enemy>() && projectileType == ProjectileType.Good)
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            buildFireDestoryThis();
        }

        if (collision.gameObject.GetComponent<Player>() && projectileType == ProjectileType.Evil)
        {
            buildFireDestoryThis();
            FindObjectOfType<Player>().health -= damage;
            FindObjectOfType<EventCanvas_HealthBar>().updateSlider();

            if (FindObjectOfType<EventCanvas>() != null)            
                FindObjectOfType<EventCanvas>().refreshAttributeText();
        }
    }

    private void buildFireDestoryThis()
    {
        Destroy(this.gameObject);
        Instantiate(fire, new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity, FindObjectOfType<MapArenor>().transform);
    }
}
