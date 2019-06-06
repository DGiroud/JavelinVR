using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private float RoomTemp = 0;

    public bool m_isChanging;
    public float m_multipler;
    public bool m_currentRoom;
    public float m_roomTemp;
    public float m_tempTarget;

 


    // Start is called before the first frame update
    void Start()
    {
        m_roomTemp = RoomTemp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TempRise(bool heat)
    {

    }
}
