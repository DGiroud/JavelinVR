using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public float m_Distance = 10.0f;
    public LineRenderer lineRenderer = null;
    public LayerMask m_AllObjects = 0;
    public LayerMask m_InteractibleObjects = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    private Transform OriginController = null;
    private GameObject m_CurrentObject = null;

    private void Start()
    {
        SetLineColor();
    }

    private void Awake()
    {
        VRInteraction.onControllerSource += UpdateOrigin;
        VRInteraction.onTouchPadDown += ProcessTouchPadDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
            OnPointerUpdate(hitPoint, m_CurrentObject);
    }


    private void OnDestroy()
    {
        VRInteraction.onControllerSource -= UpdateOrigin;
        VRInteraction.onTouchPadDown -= ProcessTouchPadDown;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject gameObject) {
        OriginController = gameObject.transform;

        if (controller == OVRInput.Controller.Touchpad)
        {
            lineRenderer.enabled = false;
        }
        else {
            lineRenderer.enabled = true;
        }
    }

    private void ProcessTouchPadDown() {
        if (!m_CurrentObject)
            return;

        Interactable interat = m_CurrentObject.GetComponent<Interactable>();
        interat.Pressed();
    }

    private GameObject UpdatePointerStatus() {
        RaycastHit hit = CreateRayCast(m_InteractibleObjects);

        if (hit.collider)
        {
            Material mat = hit.transform.GetComponent<Renderer>().material;
            mat.SetFloat("_ASEOutlineWidth", 0.3f);
            return hit.collider.gameObject;
        }
        else {

            Material mat = hit.transform.GetComponent<Renderer>().material;
            mat.SetFloat("_ASEOutlineWidth", 0.0f);
            return hit.collider.gameObject;
        }
        return null;
    }

    private Vector3 UpdateLine() {
        RaycastHit hit = CreateRayCast(m_AllObjects);

        Vector3 endPos = OriginController.position + (OriginController.forward * m_Distance);

        if (hit.collider != null)
            endPos = hit.point;

        lineRenderer.SetPosition(0, OriginController.position);
        lineRenderer.SetPosition(1, endPos);

        return endPos;
    }

    private RaycastHit CreateRayCast(int layer) {
        RaycastHit hit;
        Ray ray = new Ray(OriginController.position, OriginController.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);
        return hit;
    }

    private void SetLineColor() {
        if (lineRenderer != null)
            return;

        Color endCol = Color.white;
        endCol.a = 0;
        lineRenderer.endColor = endCol;
    }

}
