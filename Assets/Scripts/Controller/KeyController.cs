using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class KeyController : MonoBehaviour
{
    private ShootingPlayer shootingPlayer;
    public Player player;
    public GUI_Container gui;

    public BuildController buildController;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        shootingPlayer = player.GetComponent<ShootingPlayer>();
    }

    private void Update()
    {
        //PickUp
        if (Input.GetKeyDown(KeyCode.E))
            player.doPickUp = true;

        //Shooting
        if (Input.GetKeyDown(KeyCode.Alpha1) && isCanvasActive(gui.canvasMain))
        {
            if (!shootingPlayer.sliderHandling.runSlider)
            {
                shootingPlayer.shootProjectile = true;
            }
        }

        // ShortKeys MainCanvas

        if(isCanvasActive(gui.canvasMain))
        {
            if (Input.GetKey(KeyCode.I))
                FindObjectOfType<ButtonHandling_MainCanvas>().OnClickButtonInventory();
            if (Input.GetKey(KeyCode.B))
                FindObjectOfType<ButtonHandling_MainCanvas>().OnClickButtonBuild();
        }

        //Build Canvas
        if (isCanvasActive(gui.canvasBuild))
        {
            if (Input.GetKey(KeyCode.Alpha1)) 
                FindObjectOfType<ButtonHandling_BuildCanvas>().OnClickButtonWalls();
            if (Input.GetKey(KeyCode.Alpha2))
                FindObjectOfType<ButtonHandling_BuildCanvas>().OnClickButtonZones();
            if (Input.GetKey(KeyCode.Escape))
                FindObjectOfType<ButtonHandling_BuildCanvas>().OnClickButtonBack();
        }

        //Build Walls Canvas
        if (isCanvasActive(gui.canvasBuildWalls))
        {
            var wallContainer = FindObjectOfType<WallContainer_Arenor>();

            keyHandlingWallBuilding(KeyCode.Alpha1, wallContainer.stoneWall);
            keyHandlingWallBuilding(KeyCode.Alpha2, wallContainer.woodWall);
        }

        //Build Zones Canvas
        if (isCanvasActive(gui.canvasBuildZones))
        {
            var zoneContainer = FindObjectOfType<ZoneContainer_Arenor>();

            keyHandlingZoneBuilding(KeyCode.Alpha1, zoneContainer.woodcutterZone);
            keyHandlingZoneBuilding(KeyCode.Alpha2, zoneContainer.masonZone);
            keyHandlingZoneBuilding(KeyCode.Alpha3, zoneContainer.alchimistZone);
            keyHandlingZoneBuilding(KeyCode.Alpha4, zoneContainer.scientistZone);
            keyHandlingZoneBuilding(KeyCode.Alpha5, zoneContainer.altarShrine);
        }

        //Inventory ShortKeys
        if (isCanvasActive(gui.canvasInventory))
        {
            if (Input.GetKey(KeyCode.Escape))
                FindObjectOfType<ButtonHandling_InventoryCanvas>().OnClickButtonBack();
        }
    }

    private void keyHandlingWallBuilding(KeyCode keyCode, Wall wall)
    {
        if (Input.GetKeyDown(keyCode))
            buildController.build(wall);
    }

    private void keyHandlingZoneBuilding(KeyCode keyCode, Zone zone)
    {
        if (Input.GetKeyDown(keyCode))
            buildController.build(zone);
    }

    private bool isCanvasActive(Canvas canvas)
    {
        return canvas.gameObject.activeSelf;
    }
}
