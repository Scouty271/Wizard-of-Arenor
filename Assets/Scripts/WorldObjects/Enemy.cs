using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public int skillPointsWhenDead;

    private void Update()
    {
        if (health <= 0)
        {
            FindObjectOfType<MapArenor>().items.Add(Instantiate(FindObjectOfType<ItemContainer_Arenor>().itemSoulstone, this.transform.position, Quaternion.identity));

            FindObjectOfType<MapArenor>().enemys.Remove(this);

            Destroy(gameObject);

            FindObjectOfType<Player>().skillPoints += skillPointsWhenDead;

            FindObjectOfType<EventCanvas>().refreshAttributeText();
        }
    }
}
