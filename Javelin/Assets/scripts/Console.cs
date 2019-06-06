using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    #region Heat
    public float m_Temp;
    public bool m_risingTemp;
    #endregion

    #region Power
    public float m_powerAmount;
    public int m_doorsclosed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_risingTemp = false;
        m_doorsclosed = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
