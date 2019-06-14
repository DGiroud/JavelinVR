using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRInteraction : MonoBehaviour
{

    #region Events
    public static UnityAction onTouchPadUp = null;
    public static UnityAction onTouchPadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> onControllerSource = null;
    #endregion

    #region Anchors
    public GameObject m_RightAnchor;
    public GameObject m_HeadAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_Sets = null;
    private OVRInput.Controller m_Source = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    private void Awake()
    {
        m_Sets = CreateControllerSets();
    }

    private void Update()
    {
        CheckForController();
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets() {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.RTrackedRemote, m_RightAnchor },
            {OVRInput.Controller.Touchpad, m_HeadAnchor }
        };
        return newSets;
    }

    private OVRInput.Controller UpdateController(OVRInput.Controller check, OVRInput.Controller prev) {
        if (check == prev)
            return prev;

        GameObject controllerObject = null;
        m_Sets.TryGetValue(check, out controllerObject);

        if (controllerObject == null)
            controllerObject = m_HeadAnchor;

        if (onControllerSource != null)
            onControllerSource(check, controllerObject);

        return check;   
    }

    private void CheckForController() {
        OVRInput.Controller checkControl = m_Controller;

        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            checkControl = OVRInput.Controller.RTrackedRemote;
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            checkControl = OVRInput.Controller.Touchpad;

        m_Controller = UpdateController(checkControl, m_Controller);
    }
}
