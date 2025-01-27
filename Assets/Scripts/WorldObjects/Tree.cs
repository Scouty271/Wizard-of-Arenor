using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tree : MonoBehaviour
{
    public bool inWorkerProcess;

    public int health;

    private void Update()
    {
        if (health <= 0)
        {
            FindObjectOfType<MapArenor>().items.Add(Instantiate(FindObjectOfType<ItemContainer_Arenor>().itemWood, this.transform.position, Quaternion.identity, FindObjectOfType<ItemContainer_Arenor>().transform));

            FindObjectOfType<MapArenor>().trees.Remove(this);
            gameObject.GetComponent<NavMeshModifier>().overrideArea = false;

            FindObjectOfType<MapArenor>().navMesh.UpdateNavMesh(FindObjectOfType<MapArenor>().navMesh.navMeshData);
            Destroy(gameObject);
        }
    }
}
