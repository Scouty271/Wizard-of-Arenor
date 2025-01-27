using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandling_ZoneCanvas : MonoBehaviour
{
    public GUI_Container guiContainer;

    public void OnClickButtonBack()
    {
        guiContainer.canvasBuild.gameObject.SetActive(true);
        guiContainer.canvasBuildZones.gameObject.SetActive(false);
    }
}
