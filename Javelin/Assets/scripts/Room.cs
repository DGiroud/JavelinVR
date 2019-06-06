using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private float RoomTemp = 0;

    public bool m_isChanging;
    public float m_multipler;
    public float m_roomTemp;
    public float m_tempTarget;
    public float m_maxTemp;
    public bool m_alreadyHeated;


    private Console con;



    // Start is called before the first frame update
    void Start()
    {
        m_roomTemp = RoomTemp;
        m_alreadyHeated = false;
        m_maxTemp = con.m_Temp;
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TempChange(bool isheating)
    {
        con.m_risingTemp = isheating;

        if (con.m_risingTemp)
        {
            m_roomTemp += Time.deltaTime * m_multipler;


            if (m_roomTemp == m_maxTemp)
            {
                con.m_risingTemp = false;
            }

        }
        if (!con.m_risingTemp && m_roomTemp > 0)
        {
           m_roomTemp -=Time.deltaTime * m_multipler;
        }
    }

}

}
