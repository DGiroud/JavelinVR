using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour
{
    [Range(0, 10)]
    public float speed;
    private int timeTillReturn = 2;
    public RoomManger m_RoomManager;

    private int[] pastTime;

    private void Awake()
    {
        pastTime = new int[m_RoomManager.m_Rooms.Count];
    }

    void Update()
    {
        Seek();
    }

    void Seek() {

        float maxRoomTemp = 0.0f;
        int maxIndex = -1;

        for (int i = 1; i < m_RoomManager.m_Rooms.Count; i++) {
            if (!m_RoomManager.m_Rooms[i].m_hasBeenVisited && maxRoomTemp < m_RoomManager.m_Rooms[i].m_RoomTemp)
            {
                maxRoomTemp = m_RoomManager.m_Rooms[i].m_RoomTemp;
                maxIndex = i;
            }
        }
        
        if (maxIndex != -1 && !m_RoomManager.m_Rooms[maxIndex].m_hasBeenVisited)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_RoomManager.m_Rooms[maxIndex].m_heatModule.transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, m_RoomManager.m_Rooms[maxIndex].m_heatModule.transform.position) < 1.0f)
            {
                m_RoomManager.m_Rooms[maxIndex].m_hasBeenVisited = true;
                for (int i = 1; i < pastTime.Length; i++)
                {
                    if (i != maxIndex && m_RoomManager.m_Rooms[i].m_hasBeenVisited)
                    {
                        pastTime[i]++;
                        if (pastTime[i] >= timeTillReturn)
                        {
                            pastTime[i] = 0;
                            m_RoomManager.m_Rooms[i].m_hasBeenVisited = false;
                        }
                    }
                }
            }
            
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, m_RoomManager.m_Rooms[0].m_heatModule.transform.position, speed * Time.deltaTime);
        }


    }
}
