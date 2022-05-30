using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClick : MonoBehaviour
{
    private string tagMe;

    public string TagMe
    {
        get { return tagMe; }
        set { tagMe = value; }
    }

    public void isClicked()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().ButtonClick(tagMe);
    }
}
