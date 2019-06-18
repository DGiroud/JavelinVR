using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Secondary : MonoBehaviour
{
    public Transform target;

    private Vector3 dir;

    private void Update()
    {
        transform.rotation = target.transform.rotation;
    }
}
