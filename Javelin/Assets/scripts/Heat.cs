using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
    public bool m_AlreadyHeated;
    public float m_Temp;
    public bool m_visited;
    public bool m_risingTemp;

    private Room m_room;
    

    // Start is called before the first frame update
    void Start()
    {
        m_risingTemp = true;
        m_AlreadyHeated = false;
        m_visited = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void TempChange(bool isheating)
    {
        if (m_risingTemp == true)
        {
            m_room.m_roomTemp += m_Temp;

        }
    }
}
