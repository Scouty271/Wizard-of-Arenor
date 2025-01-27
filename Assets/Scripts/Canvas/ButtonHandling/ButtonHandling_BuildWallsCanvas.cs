using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandling_BuildWallsCanvas : MonoBehaviour
{
    public GUI_Container guiContainer;

    public void OnClickButtonBack()
    {
        guiContainer.canvasBuild.gameObject.SetActive(true);
        guiContainer.canvasBuildWalls.gameObject.SetActive(false);
    }
}
