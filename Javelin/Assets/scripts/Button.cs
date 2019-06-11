using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public int m_doorNumber;

    public Image m_matColour;


    private void Start()
    {
        m_matColour = transform.GetComponent<Image>();
    }
}
