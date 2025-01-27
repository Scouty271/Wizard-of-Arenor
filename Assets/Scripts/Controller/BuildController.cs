using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static Zone;

public class BuildController : MonoBehaviour
{
    public enum BuildPositions
    {
        MousePosition,
        GeneratedPosition
    }
    public BuildPositions buildPosition;

    public MapArenor mapArenor;

    public InventarRohstoffe inventarRohstoffe;

    public float buildPosX;
    public float buildPosY;

    bool blocked = false;

    private Vector2 buildpos;

    public Zone foundetZone;

    public void build(Wall wall)
    {
        var block = checkBlocked(BuildPositions.MousePosition);

        if (!block)
        {
            switch (wall.wallType)
            {
                case Wall.WallType.stone:
                    buildSpecificWall(wall, inventarRohstoffe.steinAnzahl);
                    break;
                case Wall.WallType.iron:
                    break;
                case Wall.WallType.coal:
                    break;
                case Wall.WallType.wood:
                    buildSpecificWall(wall, inventarRohstoffe.holzAnzahl);
                    break;
                default:
                    break;
            }

            mapArenor.navMesh.BuildNavMesh();
        }
    }
    public void build(Zone zoneToBuild)
    {
        blocked = checkBlocked(BuildPositions.MousePosition);

        if (!blocked)
        {
            var zone = Instantiate(zoneToBuild, new Vector3(buildPosX, buildPosY, -0.1f), Quaternion.identity, FindObjectOfType<ZoneContainer_Arenor>().transform);

            mapArenor.zones.Add(zone);

            foundetZone = checkIfZoneNearOther(zone);

            if (foundetZone != null)
            {
                zone.transform.SetParent(foundetZone.GetComponentInParent<ZoneGroup>().transform);
                foundetZone.GetComponentInParent<ZoneGroup>().childZones.Add(zone);
                doEdges(zone, foundetZone);
            }
            else
            {
                var currZoneGroup = Instantiate(FindObjectOfType<MapArenor>().zoneGroup, FindObjectOfType<ZoneContainer_Arenor>().transform);

                currZoneGroup.profession = zone.profession;
                mapArenor.zoneGroups.Add(currZoneGroup);
                zone.transform.SetParent(currZoneGroup.transform);
                currZoneGroup.childZones.Add(zone);
            }
        }
    }

    public void build(Portal portal)
    {
        for (int i = 0; i < 100; i++)
        {
            blocked = checkBlocked(BuildPositions.GeneratedPosition);

            if (!blocked)
            {
                mapArenor.portals.Add(Instantiate(portal, new Vector3(buildPosX, buildPosY, -0.5f), Quaternion.identity, mapArenor.transform.GetComponentInChildren<Grid>().transform));
                FindObjectOfType<QuestManager>().portalCreated();

                break;
            }
        }
    }

    private void buildSpecificWall(Wall wall, int rohstoff)
    {
        if (!FindObjectOfType<CheatController>().unlimitedBuilding)
        {
            if (rohstoff >= 10)
            {
                mapArenor.walls.Add(Instantiate(wall, new Vector3(buildPosX, buildPosY, -0.5f), Quaternion.identity, FindObjectOfType<WallContainer_Arenor>().transform));
                rohstoff -= 10;
            }
        }
        else
            mapArenor.walls.Add(Instantiate(wall, new Vector3(buildPosX, buildPosY, -0.5f), Quaternion.identity, FindObjectOfType<WallContainer_Arenor>().transform));
    }

    private bool checkBlocked(BuildPositions buildPosition)
    {
        blocked = false;

        switch (buildPosition)
        {
            case BuildPositions.MousePosition:
                buildpos = mousePos();
                buildPosX = buildpos.x;
                buildPosY = buildpos.y;
                break;
            case BuildPositions.GeneratedPosition:
                buildpos = generatedPos();
                buildPosX = buildpos.x;
                buildPosY = buildpos.y;
                break;
            default:
                break;
        }

        foreach (var item in mapArenor.walls)
        {
            isBlocked(new Vector2(item.transform.position.x, item.transform.position.y), buildPosX, buildPosY);
        }

        foreach (var item in mapArenor.trees)
        {
            isBlocked(new Vector2(item.transform.position.x, item.transform.position.y), buildPosX, buildPosY);
        }

        foreach (var item in mapArenor.items)
        {
            isBlocked(new Vector2(item.transform.position.x, item.transform.position.y), buildPosX, buildPosY);
        }

        foreach (var item in mapArenor.zones)
        {
            isBlocked(new Vector2(item.transform.position.x, item.transform.position.y), buildPosX, buildPosY);
        }

        return blocked;
    }

    private void isBlocked(Vector2 itemPos, float buildPosX, float buildPosY)
    {
        if (Mathf.RoundToInt(itemPos.x) == buildPosX)
        {
            if (Mathf.RoundToInt(itemPos.y) == buildPosY)
            {
                blocked = true;
            }
        }
    }

    private Zone checkIfZoneNearOther(Zone zoneToBuild)
    {
        Zone foundetZone = null;

        var zoneToBuildPosX = Mathf.RoundToInt(zoneToBuild.transform.position.x);
        var zoneToBuildPosY = Mathf.RoundToInt(zoneToBuild.transform.position.y);

        foreach (Zone zone in mapArenor.zones)
        {
            var listZonePosX = Mathf.RoundToInt(zone.transform.position.x);
            var listZonePosY = Mathf.RoundToInt(zone.transform.position.y);

            if (zoneToBuildPosY == listZonePosY)
            {
                if (zoneToBuildPosX + 1 == listZonePosX)
                {
                    if (zoneToBuild.profession == zone.profession)
                        foundetZone = zone;
                }
                if (zoneToBuildPosX - 1 == listZonePosX)
                {
                    if (zoneToBuild.profession == zone.profession)
                        foundetZone = zone;
                }
            }

            if (zoneToBuildPosX == listZonePosX)
            {
                if (zoneToBuildPosY + 1 == listZonePosY)
                {
                    if (zoneToBuild.profession == zone.profession)
                        foundetZone = zone;
                }
                if (zoneToBuildPosY - 1 == listZonePosY)
                {
                    if (zoneToBuild.profession == zone.profession)
                        foundetZone = zone;
                }
            }
        }
        return foundetZone;
    }

    private void doEdges(Zone zoneToBuild, Zone foundetZone)
    {
        var zoneToBuildPosX = Mathf.RoundToInt(zoneToBuild.transform.position.x);
        var zoneToBuildPosY = Mathf.RoundToInt(zoneToBuild.transform.position.y);

        foreach (Zone zone in mapArenor.zones)
        {
            var listZonePosX = Mathf.RoundToInt(zone.transform.position.x);
            var listZonePosY = Mathf.RoundToInt(zone.transform.position.y);

            if (zoneToBuildPosY == listZonePosY)
            {
                if (zoneToBuildPosX + 1 == listZonePosX && zone.transform.parent == foundetZone.transform.parent)
                {
                    foundetZone = zone;

                    zoneToBuild.openRight = true;
                    foundetZone.openLeft = true;

                    foundetZone.refreshEdges();
                    zoneToBuild.refreshEdges();
                }
                if (zoneToBuildPosX - 1 == listZonePosX && zone.transform.parent == foundetZone.transform.parent)
                {
                    foundetZone = zone;

                    zoneToBuild.openLeft = true;
                    foundetZone.openRight = true;

                    foundetZone.refreshEdges();
                    zoneToBuild.refreshEdges();
                }
            }

            if (zoneToBuildPosX == listZonePosX)
            {
                if (zoneToBuildPosY + 1 == listZonePosY && zone.transform.parent == foundetZone.transform.parent)
                {
                    foundetZone = zone;

                    zoneToBuild.openUp = true;
                    foundetZone.openDown = true;

                    foundetZone.refreshEdges();
                    zoneToBuild.refreshEdges();
                }
                if (zoneToBuildPosY - 1 == listZonePosY && zone.transform.parent == foundetZone.transform.parent)
                {
                    foundetZone = zone;

                    zoneToBuild.openDown = true;
                    foundetZone.openUp = true;

                    foundetZone.refreshEdges();
                    zoneToBuild.refreshEdges();
                }
            }
        }


    }

    private Vector2 mousePos()
    {
        Vector2 mousePos = new Vector2();

        mousePos.x = Mathf.RoundToInt(FindObjectOfType<Raycast>().mousePos.x - 0.5f);
        mousePos.y = Mathf.RoundToInt(FindObjectOfType<Raycast>().mousePos.y - 0.5f);

        return mousePos;
    }

    private Vector2 generatedPos()
    {
        Vector2 generatedPos = new Vector2();

        generatedPos.x = Random.Range(FindObjectOfType<MapArenor>().waterBorderSize, mapArenor.size - FindObjectOfType<MapArenor>().waterBorderSize);
        generatedPos.y = Random.Range(FindObjectOfType<MapArenor>().waterBorderSize, mapArenor.size - FindObjectOfType<MapArenor>().waterBorderSize);

        return generatedPos;
    }
}
