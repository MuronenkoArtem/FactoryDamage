using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Raycast : Matrix
{

    // Update is called once per frame
    void Start()
    {
        Vector3 sphere = Vector3.forward + Vector3.right;
        Vector3 position = new Vector3(transform.position.x + 1, transform.position.z);
        Debug.DrawRay(position, sphere, Color.red);

        RaycastHit hit;

        

        if (Physics.SphereCast(position, 0.5f, Vector3.right / 2, out hit))
        {
            if (hit.collider.tag != null)
            {
                go = Instantiate(BuildsPrefabs[0], GetTitleCenter(Convert.ToInt32(hit.transform.position.x - 2.5), Convert.ToInt32(hit.transform.position.z - 2.5)), transform.rotation) as GameObject;
            }

            //if (hit.transform.position.x >= 0 && hit.transform.position.z >= 0)
            //{
            //    Debug.DrawLine(
            //        Vector3.forward * hit.transform.position.z + Vector3.right * hit.transform.position.x,
            //        Vector3.forward * (hit.transform.position.z + 1) + Vector3.right * (hit.transform.position.x + 1));

            //    Debug.DrawLine(
            //        Vector3.forward * (hit.transform.position.z + 1) + Vector3.right * hit.transform.position.x,
            //        Vector3.forward * hit.transform.position.z + Vector3.right * (hit.transform.position.x + 1));
            //}

        }

        //if (Physics.Raycast(upRay, out hit, 1f) && selectionX >= 0 && selectionX <= 48 && selectionY >= 0 && selectionY <= 48)
        //{
        //    go = Instantiate(BuildsPrefabs[0], GetTitleCenter(x + 1, y + 1), transform.rotation) as GameObject;
        //}
    }


    void Update()
    {
        // UpdateSelection();
        //Debug.Log(selectionX + "||" + selectionY);

    }

    private void Rec(int x, int y)
    {

    }
}
