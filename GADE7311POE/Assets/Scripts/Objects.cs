using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    private int x, y;
    void OnMouseDown()
    {
        x = System.Convert.ToInt32(gameObject.transform.position.x);
        y = System.Convert.ToInt32(gameObject.transform.position.z);
        if (gameObject != null)
            GameObject.Find("GameManager").GetComponent<GameManager>().PassOn(x, y, gameObject.tag);
    }
}
