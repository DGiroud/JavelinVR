using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Panel : MonoBehaviour
{
    public List<Button> m_buttons;

    public Button m_selectedButton;

    public Color m_mainColour;
    public Color m_selectedColour;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectDoor(1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectDoor(2);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectDoor(3);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectDoor(4);

        if (Input.GetKeyDown(KeyCode.Alpha0))
            DeselectDoor();
    }

    public void SelectDoor(int doorNumber)
    {
        if (m_selectedButton != null)
            DeselectDoor();  

        

        m_selectedButton = m_buttons[doorNumber];
        m_selectedButton.m_matColour.color = m_selectedColour;
    }

    public void DeselectDoor()
    {
        m_selectedButton.m_matColour.color = m_mainColour;
        m_selectedButton = null;
    }
}
