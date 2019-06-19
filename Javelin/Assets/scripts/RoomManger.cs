using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Room
{
    [Range(0, 1)]
    public float m_MaxTemperature;
    [Range(0, 1)]
    public float m_heatMultiplier;
    [Range(0, 1)]
    public float m_coolMultiplier;
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
    public bool m_slectedDoor;
    public bool m_slectedRoom;
    public int m_index;

    [Header("UI")]
    public Image m_energyUI;
    public Image m_temperatureUI;
    public Image m_oxygenUI;

    //public SimpleSlider slider;


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
                if (m_Rooms[i].m_isChanging && m_Energy >= 0)
                {
                    m_Rooms[i].m_RoomTemp += Time.deltaTime * m_Rooms[i].m_heatMultiplier;
                    // usign cost of heating a room
                    m_Energy -= m_Rooms[i].m_energyCost;
                    if (m_Rooms[i].m_RoomTemp >= m_Rooms[i].m_MaxTemperature)
                    {
                        m_Rooms[i].m_RoomTemp = m_Rooms[i].m_MaxTemperature;
                        m_Rooms[i].m_isChanging = false;
                    }
                }
                // cooling the room down 
                if (!m_Rooms[i].m_isChanging && m_Rooms[i].m_RoomTemp > 0)
                {
                    m_Rooms[i].m_RoomTemp -= Time.deltaTime * m_Rooms[i].m_coolMultiplier;
                    // checking to see if the room temp gose below 0
                    if (m_Rooms[i].m_RoomTemp < 0)
                    {
                        // setting the room temp to 0
                        m_Rooms[i].m_RoomTemp = 0;
                    }

                }
                // checking if the energy is 0
                if (m_Energy <= 0)
                {
                    m_Rooms[i].m_isChanging = false;
                }                
            }
        }

        m_energyUI.fillAmount = m_Energy;
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
    public void HeatRoom(int roomIndex)
    {
        //Check how many rooms are in the game
        //heat the room
        if (doorsClosed <= 0 )
        {

            if (m_Rooms[roomIndex].m_RoomTemp < m_Rooms[roomIndex].m_MaxTemperature)
            {
               // m_Rooms[roomIndex].m_MaxTemperature = slider.targetTemp;
                m_Rooms[roomIndex].m_isChanging = true;

            }
        }
    }



    // Closes and Opens Door
    void DoorManager(int doorIndex)
    {
        //Checks 
        if (doorsClosed <= 1 || m_Energy != 0)
        {
            if (m_Doors[doorIndex].m_openDoor)
            {
                m_Doors[doorIndex].m_openDoor = false;
                doorsClosed++;
            }
            // cost of using a door
            m_Energy -= m_Doors[doorIndex].m_energyCost;
        }
        else if (!m_Doors[doorIndex].m_openDoor)
        {
            m_Doors[doorIndex].m_openDoor = true;
            doorsClosed--;
            // cost of opening a door
            m_Energy += m_Doors[doorIndex].m_energyCost;

        }

    }
    // Activation on Either heating room or shuting/opening door
    public void DoFunction()
    {
        if (m_slectedDoor)
        {
            DoorManager(m_index);
        }
        if (m_slectedRoom)
        {
            HeatRoom(m_index);
        }
    }
    public void doorSlected(int doorNumber)
    {
        m_slectedRoom = false;
        m_slectedDoor = true;
        m_index = doorNumber;
    }
    public void roomSlected(int roomNumber)
    {
        m_slectedRoom = true;
        m_slectedDoor = false;
        m_index = roomNumber;
    }


}
