using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject pivot;
    public float rotateSpeed, scrollSpeed;

    private float vertPos, axisY, axisX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertPos += Input.GetAxisRaw("Mouse ScrollWheel") * scrollSpeed;
        vertPos = Mathf.Clamp(vertPos, 0, 6);
        pivot.transform.position = new Vector3(0, vertPos, 0);
        //Debug.Log(vertPos);
    }

    private void FixedUpdate()
    {
        //pivot.transform.Rotate(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0f, Space.World);
        axisY += Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime;
        axisX += Input.GetAxisRaw("Vertical") * rotateSpeed * Time.deltaTime;
        
        axisX = Mathf.Clamp(axisX, 0, 90);
        pivot.transform.rotation = Quaternion.Euler(axisX, axisY, 0);
        

        //pivot.transform.Rotate(axisY, axisX, 0f);
    }
}
