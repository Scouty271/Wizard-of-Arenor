using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonHandling_BuildCanvas : MonoBehaviour
{
    public GUI_Container guiContainer;

    public void OnClickButtonBack()
    {
        guiContainer.canvasMain.gameObject.SetActive(true);
        guiContainer.canvasBuild.gameObject.SetActive(false);
    }

    public void OnClickButtonWalls()
    {
        guiContainer.canvasBuildWalls.gameObject.SetActive(true);
        guiContainer.canvasBuild.gameObject.SetActive(false);
    }
    public void OnClickButtonZones()
    {
        guiContainer.canvasBuildZones.gameObject.SetActive(true);
        guiContainer.canvasBuild.gameObject.SetActive(false);
    }
}
