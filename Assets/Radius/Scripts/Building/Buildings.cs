using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Buildings : MonoBehaviour {
    public bool Upgrade = false;
    // Use this for initialization
    private void OnMouseDown()
    {
        Upgrade = true;
    }
}
