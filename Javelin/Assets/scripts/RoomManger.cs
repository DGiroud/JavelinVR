using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    public float m_MaxTemperature;
    public float m_heatMultiplier;
    public float m_RoomTemp;

    public bool m_isChanging;
    public bool m_hasBeenVisited;
    public GameObject m_heatModule;
}

public class Door
{
    public GameObject m_Door;
    private int doorsClosed;

}

public class RoomManger : MonoBehaviour
{
    public List<Room> m_Rooms;
    public float m_Energy;


    void Awake()
    {
        if (m_Rooms != null)
        {
            for (int i = 0; i < m_Rooms.Count; i++)
            {
                m_Rooms[i].m_RoomTemp = 0;
            }
        }
    }

    void Update()
    {
        if (m_Rooms != null)
        {
            for (int i = 0; i < m_Rooms.Count; i++)
            {
                if (m_Rooms[i].m_RoomTemp > 0 && !m_Rooms[i].m_isChanging)
                {
                    m_Rooms[i].m_RoomTemp -= Time.deltaTime * m_Rooms[i].m_heatMultiplier;
                }
            }
        }
    }

    // Selects a room to be heated 
    void SelectRoom()
    {
        //Check how many rooms are in the game
        //Allows the player to select a room
    }

    // Deselects a room to be heated
    void DeSelectRoom()
    {
        //NOT IMPORTANT NOW
    }

    // Heat the room
    void HeatRoom(float m_playerTemp, int roomIndex)
    {
        //Check how many rooms are in the game
        //heat the room
        if (m_Rooms[roomIndex].m_RoomTemp < m_Rooms[roomIndex].m_MaxTemperature)
        {
             m_Rooms[roomIndex].m_RoomTemp += Time.deltaTime * m_Rooms[roomIndex].m_heatMultiplier;
             m_Rooms[roomIndex].m_isChanging = true;
            if (m_Rooms[roomIndex].m_RoomTemp >= m_Rooms[roomIndex].m_MaxTemperature)
            {
                m_Rooms[roomIndex].m_RoomTemp = m_Rooms[roomIndex].m_MaxTemperature;
                m_Rooms[roomIndex].m_isChanging = false;
            }
        }

    }

    // Checks if the current room has been heated or not
    void CheckRoom()
    {
        //Checks how many rooms are in the game
        // Check if Enemies Collider has been overallped with Heatmodule collider
        // if it has been, mark it as has been visited if not 
        // mark it as it has not been 
    }

    // Closes and Opens Door
    void DoorManager()
    {
        //Checks 
    }


}
