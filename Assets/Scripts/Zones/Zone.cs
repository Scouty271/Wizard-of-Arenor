using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public enum Profession
    {
        none,
        woodcutter,
        mason,

        alchemist,
        scientist,
        altarShrine,

        fighter
    }
    public Profession profession;

    public bool isWorkerPlaced;

    public bool openUp;
    public bool openDown;
    public bool openRight;
    public bool openLeft;

    public SpriteRenderer balken;

    private SpriteRenderer balkenOben;
    private SpriteRenderer balkenUnten;
    private SpriteRenderer balkenRechts;
    private SpriteRenderer balkenLinks;


    private void Awake()
    {
        balkenOben = Instantiate(balken, new Vector3(0 + transform.position.x, 0.961f + transform.position.y, -0.1f), Quaternion.identity, transform);
        balkenUnten = Instantiate(balken, new Vector3(0 + transform.position.x, 0 + transform.position.y, -0.1f), Quaternion.identity, transform);
        balkenRechts = Instantiate(balken, new Vector3(1 + transform.position.x, 0 + transform.position.y, -0.1f), Quaternion.Euler(0, 0, 90), transform);
        balkenLinks = Instantiate(balken, new Vector3(0.039f + transform.position.x, 0 + transform.position.y, -0.1f), Quaternion.Euler(0, 0, 90), transform);
    }


    public void refreshEdges()
    {
        if (openUp) Destroy(balkenOben);
        if (openDown) Destroy(balkenUnten);
        if (openLeft) Destroy(balkenLinks);
        if (openRight) Destroy(balkenRechts);
    }
}
