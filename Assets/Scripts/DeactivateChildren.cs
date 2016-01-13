using UnityEngine;
using System.Collections;

public class DeactivateChildrens : MonoBehaviour
{

    public static void DeactivateChildren(GameObject g, bool visibility)
    {
        if (g.GetComponent<Renderer>())
        {
            Renderer r = g.GetComponent<Renderer>();
            r.enabled = visibility;
        }
        if (g.GetComponent<Canvas>())
        {
            Canvas c = g.GetComponent<Canvas>();
            c.enabled = visibility;
        }
        g.GetComponentInChildren<Renderer>();

        foreach (Transform child in g.transform)
        {
            DeactivateChildren(child.gameObject, visibility);
        }
    }
}
