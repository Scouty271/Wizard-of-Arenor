using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{
    public Enemy enemy;

    public int health;

    private int enemysInstantiated;

    public float timerForNextSpawn = 10;

    private void Update()
    {
        //var value = Random.Range(0, 1000);

        if (timerForNextSpawn > -10)
        {
            timerForNextSpawn -= Time.deltaTime;
        }

        if (FindObjectOfType<CheatController>().noPortals)
            health = 0;

        if (timerForNextSpawn <= 0 && enemysInstantiated < 2)
        {
            FindObjectOfType<MapArenor>().enemys.Add(Instantiate(enemy, transform.position, Quaternion.identity, FindObjectOfType<EnemyContainer_Arenor>().transform));
            enemysInstantiated++;
            timerForNextSpawn = 10;
        }

        if (health <= 0)
        {
            FindObjectOfType<QuestManager>().portaleZerstört++;
            FindObjectOfType<MapArenor>().portals.Remove(this);
            gameObject.GetComponent<NavMeshModifier>().overrideArea = false;
            FindObjectOfType<MapArenor>().navMesh.UpdateNavMesh(FindObjectOfType<MapArenor>().navMesh.navMeshData);
            FindObjectOfType<QuestManager>().portalDestroyed();

            Destroy(gameObject);
        }
    }
}
