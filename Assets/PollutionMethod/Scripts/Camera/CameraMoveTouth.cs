using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveTouth : MonoBehaviour
{

    // Use this for initialization

    // Use this for initialization
    public float x;
    public float y;
    public float z;
    public float sensity = 1;

    public Slider UpDown;
    public Slider LeftRight;
    public Slider Forvard;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        UpDown.onValueChanged.AddListener(delegate { ValueChangeCheckUpDown(); });
        LeftRight.onValueChanged.AddListener(delegate { ValueChangeCheckLeftRight(); });
        Forvard.onValueChanged.AddListener(delegate { ValueChangeCheckForvard(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheckUpDown()
    {
        x = UpDown.value;
        transform.rotation = Quaternion.Euler(x, y, 0);
    }

    public void ValueChangeCheckLeftRight()
    {
        y = LeftRight.value;
        transform.rotation = Quaternion.Euler(x, y, 0);
    }
    public void ValueChangeCheckForvard()
    {
        z = Forvard.value;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            x -= Input.GetAxis("Mouse Y") * sensity;
            y += Input.GetAxis("Mouse X") * sensity;
            //transform.position = new Vector3(x,y, Time.deltaTime * 50);

            x = Mathf.Clamp(x, 10, 60);
            //transform.position = new Vector3(x,y, Time.deltaTime * 50);
           
            transform.rotation = Quaternion.Euler(x,y,0);
            
        }
        transform.position += transform.forward * +(Input.GetAxis("Mouse ScrollWheel")) * 5;

    }

}
