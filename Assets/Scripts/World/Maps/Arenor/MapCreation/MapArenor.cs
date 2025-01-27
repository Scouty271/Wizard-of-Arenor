using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEditor;
using static UnityEditor.Progress;


public class MapArenor : MonoBehaviour
{
    public NavMeshSurface2d navMesh;

    public GameObject objects;

    public WallContainer_Arenor wallContainer;
    public ItemContainer_Arenor itemContainer;
    public TreeContainer_Arenor treeContainer;
    public EnemyContainer_Arenor enemyContainer;
    public ZoneContainer_Arenor zoneContainer;

    public Tilemap groundTilemap;
    public Tilemap wallTilemap;

    // Walkable
    public TileBase grass1;
    public TileBase grass2;
    public TileBase grass3;

    // Unwalkable
    public TileBase water1;

    public ZoneGroup zoneGroup;

    public List<Item> items;

    public List<Tree> trees;
    public List<Wall> walls;
    //public List<Building> buildings;
    public List<Zone> zones;

    public List<Portal> portals;
    public List<Enemy> enemys;

    public List<ZoneGroup> zoneGroups;

    public int size;

    public int seed;

    float rangeValueWall = 0.65f;
    float rangeValueTree = 0.65f;
    float rangeValueItem = 0.98f;

    public int waterBorderSize = 2;

    private void Awake()
    {
        seed = Random.Range(0, 10000);
    }

    private void Start()
    {
        for (int ix = 0; ix < size; ix++)
        {
            for (int iy = 0; iy < size; iy++)
            {
                if (ix < 2 || iy < 2 || ix >= (size - waterBorderSize) || iy >= (size - waterBorderSize))
                {
                    wallTilemap.SetTile(new Vector3Int(ix, iy, 0), water1);
                }
                else
                {
                    var randomGrassvalue = Random.Range(0, 3);

                    switch (randomGrassvalue)
                    {
                        //Tiles
                        case 0:
                            groundTilemap.SetTile(new Vector3Int(ix, iy, 0), grass1);
                            break;
                        case 1:
                            groundTilemap.SetTile(new Vector3Int(ix, iy, 0), grass2);
                            break;
                        default:
                            groundTilemap.SetTile(new Vector3Int(ix, iy, 0), grass3);
                            break;
                    }

                    // Objektinstanzierung

                    float perlinWall = getPerlin(ix, iy, seed);
                    var perlinTree = getPerlin(ix, iy, seed + 1);

                    if (perlinWall >= rangeValueWall)
                    {
                        var value = Random.Range(0, 10);

                        //Wände
                        if (value <= 7)
                        {
                            instantiateWall(wallContainer.stoneWall, ix, iy);
                        }
                        if (value == 8)
                        {
                            instantiateWall(wallContainer.ironWall, ix, iy);
                        }
                        if (value == 9)
                        {
                            instantiateWall(wallContainer.coalWall, ix, iy);
                        }

                    }
                    else if (perlinTree >= rangeValueTree)
                    {
                        var value = Random.Range(0, 3);

                        //Bäume
                        switch (value)
                        {
                            case 0:
                                instantiateTree(treeContainer.garche, ix, iy);
                                break;
                            case 1:
                                instantiateTree(treeContainer.terne, ix, iy);
                                break;
                            default:
                                instantiateTree(treeContainer.welbe, ix, iy);
                                break;
                        }
                    }
                    //Items

                    else if (getRandom(0, 1) >= rangeValueItem)
                    {
                        instantiateItem(itemContainer.itemStone, ix, iy);
                    }
                    else if (getRandom(0, 1) >= rangeValueItem)
                    {
                        instantiateItem(itemContainer.itemHohlkraut, ix, iy);
                    }
                    else if (getRandom(0, 1) >= rangeValueItem)
                    {
                        instantiateItem(itemContainer.itemEberdorn, ix, iy);
                    }
                    else if (getRandom(0, 1) >= rangeValueItem)
                    {
                        instantiateItem(itemContainer.itemWood, ix, iy);
                    }
                }
            }
        }
        navMesh.BuildNavMesh();
    }

    private void instantiateWall(Wall wall, int ix, int iy)
    {
        if (!FindObjectOfType<CheatController>().noWalls)
        {
            walls.Add(Instantiate(wall, new Vector3(ix, iy, -0.5f), Quaternion.identity, wallContainer.transform));
        }
    }

    private void instantiateTree(Tree tree, int ix, int iy)
    {
        if (!FindObjectOfType<CheatController>().noTrees)
        {
            trees.Add(Instantiate(tree, new Vector3(ix, iy, -0.5f), Quaternion.identity, treeContainer.transform));
        }
    }

    private void instantiateItem(Item item, int ix, int iy)
    {
        if (!FindObjectOfType<CheatController>().noItems)
        {
            items.Add(Instantiate(item, new Vector3(ix, iy, -0.3f), Quaternion.identity, itemContainer.transform));
        }
    }


    private float getRandom(float min, float max)
    {
        var value = Random.Range(min, max);
        return value;
    }
    private float getPerlin(int ix, int iy, int seed)
    {
        var amplitude = 1.0f;
        var frequenzy = 5 * (size / 20);


        var perlinValue = Mathf.PerlinNoise((float)ix / (float)size * frequenzy + seed, (float)iy / (float)size * frequenzy + seed) * amplitude;
        return perlinValue;
    }
}