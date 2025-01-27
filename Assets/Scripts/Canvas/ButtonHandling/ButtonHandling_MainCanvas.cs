using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandling_MainCanvas : MonoBehaviour
{
    public GUI_Container guiContainer;

    public Raycast raycast;

    public void OnClickButtonBuild()
    {
        guiContainer.canvasMain.gameObject.SetActive(false);
        guiContainer.canvasBuild.gameObject.SetActive(true);
    }
    public void OnClickButtonQuestbook()
    {

    }
    public void OnClickButtonInventory()
    {
        //guiContainer.canvasMain.gameObject.SetActive(false);
        //guiContainer.canvasEvent.gameObject.SetActive(false);
        guiContainer.canvasInventory.gameObject.SetActive(true);

        guiContainer.canvasInventory.GetComponent<InventoryCanvas>().refreshInventory();
    }
}
