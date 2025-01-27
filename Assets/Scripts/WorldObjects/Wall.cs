using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wall : MonoBehaviour
{
    public bool inWorkerProcess;

    public enum WallType
    {
        stone,
        iron,
        coal,
        wood
    }
    public WallType wallType;

    public int health;

    private void Update()
    {
        if (health <= 0)
        {
            switch (wallType)
            {
                case WallType.stone:
                    instantiateItem(FindObjectOfType<ItemContainer_Arenor>().itemStone);
                    break;
                case WallType.iron:
                    instantiateItem(FindObjectOfType<ItemContainer_Arenor>().itemIron);
                    break;
                case WallType.coal:
                    instantiateItem(FindObjectOfType<ItemContainer_Arenor>().itemCoal);
                    break;
                case WallType.wood:
                    instantiateItem(FindObjectOfType<ItemContainer_Arenor>().itemWood);
                    break;
                default:
                    break;
            }

            FindObjectOfType<MapArenor>().walls.Remove(this);

            gameObject.GetComponent<NavMeshModifier>().overrideArea = false;

            FindObjectOfType<MapArenor>().navMesh.UpdateNavMesh(FindObjectOfType<MapArenor>().navMesh.navMeshData);


            Destroy(gameObject);
        }
    }

    private void instantiateItem(Item item)
    {
        FindObjectOfType<MapArenor>().items.Add(Instantiate(item, this.transform.position, Quaternion.identity, FindObjectOfType<ItemContainer_Arenor>().transform));
    }
}
