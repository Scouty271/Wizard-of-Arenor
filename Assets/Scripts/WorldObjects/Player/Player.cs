using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public ButtonHandling_EventCanvas buttonHandling_EventCanvas;

    public Inventory inventory;

    public Collider collision;

    public Text textGameOver;

    public bool doPickUp;

    public bool canPickUp;
    public bool canOpenWorkbench;

    //Atributes
    public int health;
    public int maxHealth;

    public int mana;

    public int skillPoints;

    private void Awake()
    {
        health = maxHealth;
        mana = 0;
        skillPoints = 0;
    }

    private void Update()
    {
        if (FindObjectOfType<CheatController>().fastSpeed)
        {
            GetComponent<NavMeshAgent>().speed = 10;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            textGameOver.gameObject.SetActive(true);
        }

        if (FindObjectOfType<CheatController>().unlimitedHealth)
        {
            health = maxHealth;
        }

        if (doPickUp && collision != null && collision.gameObject.GetComponent<Item>())
        {
            inventory.addItem(collision.gameObject.GetComponent<Item>());



            FindObjectOfType<MapArenor>().items.Remove(collision.gameObject.GetComponent<Item>());

            Destroy(collision.gameObject);

            canPickUp = false;
        }
        doPickUp = false;
    }

    public void healHealthpoints(int healing)
    {
        health += healing;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
