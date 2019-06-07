using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Room
{
    [Range(0, 1)]
    public float m_MaxTemperature;
    [Range(0, 1)]
    public float m_heatMultiplier;
    [Range(0, 1)]
    public float m_RoomTemp;
    [Range(0, 1)]
    public float m_energyCost;

    public bool m_isChanging;
    public bool m_hasBeenVisited;
    public GameObject m_heatModule;
}

public class Door
{
    public GameObject m_Door;
    public bool m_openDoor;
    public int m_maxclosed;
    [Range(0, 1)]
    public float m_energyCost;

}

public class RoomManger : MonoBehaviour
{
    public List<Room> m_Rooms;
    public List<Door> m_Doors;
    [Range(0, 1)]
    public float m_Energy;
    private int doorsClosed;
    


    void Awake()
    {
        if (m_Rooms != null)
        {
            for (int i = 0; i < m_Rooms.Count; i++)
            {
                m_Rooms[i].m_RoomTemp = 0;
                m_Rooms[i].m_isChanging = false;
            }
        }
        if (m_Doors != null)
        {
            for (int i = 0; i < m_Doors.Count; i++)
            {
                m_Doors[i].m_openDoor = true;
            }
        }
        doorsClosed = 0;
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
        if (doorsClosed <= 0)
        {

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
    }

    // Closes and Opens Door
    void DoorManager(int doorIndex)
    {
        //Checks 
        if (doorsClosed <= 1)
        {
            if (m_Doors[doorIndex].m_openDoor)
            {
                m_Doors[doorIndex].m_openDoor = false;
                doorsClosed++;
            }
        }
        else if (!m_Doors[doorIndex].m_openDoor)
        {
            m_Doors[doorIndex].m_openDoor = true;
            doorsClosed--;
        }

    }


}
