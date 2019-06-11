using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float multiplyer;



    private void Update()
    {
        transform.Rotate(0,Time.deltaTime/ multiplyer, 0);
    }
}
