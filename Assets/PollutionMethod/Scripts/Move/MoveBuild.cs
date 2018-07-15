using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBuild : MonoBehaviour
{

    public GameObject _mCamera;//       Камера из которой получим данные луча (добавить в Инспекторе)
    public bool redact = false;
    private void Start()
    {
        _mCamera = GameObject.Find("Main Camera");
    }
    private bool _moveObject = false;//     При клике на объекте разрешаем перемещение
    void Update()
    {
        if (_moveObject == true && _mCamera.GetComponent<MoveMouse>().hit.point.x > 0 && _mCamera.GetComponent<MoveMouse>().hit.point.x < 96 && _mCamera.GetComponent<MoveMouse>().hit.point.z > 0 && _mCamera.GetComponent<MoveMouse>().hit.point.z < 96)
        {       //      Если разрешено перемещение объекта
            Vector3 point = new Vector3(_mCamera.GetComponent<MoveMouse>().hit.point.x, 0, _mCamera.GetComponent<MoveMouse>().hit.point.z);
            this.transform.position = point;
        }
    }

    void OnMouseDown()
    {
        if (redact == true)
            _moveObject = true;             // Разрешает перемещение
    }

    //      Идентифицирует отпускание зажатого клика
    void OnMouseUp()
    {
        if (redact == true)
        {
            _moveObject = false;            //      Запрещает перемещение
            this.gameObject.GetComponent<BuildPollution>().Start();
        }
        
    }
}
