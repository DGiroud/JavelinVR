using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Vector3 m_current;
    public Vector3 m_past;

    private Camera Main;

    public void Pressed()
    {
        if (m_past == null)
        {
            m_past = m_current;
        }
        Main = Camera.main;
        Vector3 previous = Main.WorldToScreenPoint(m_past);
        Vector3 current = Main.WorldToScreenPoint(m_current);
        m_past = m_current;
        MoveInteractable(previous.x - current.x);
    }

    private void MoveInteractable(float amount)
    {
        if (transform.position.x < 0.12f && transform.position.x > -0.5)
        {
            transform.Translate(new Vector3(amount, 0, 0));
        }
    }
}
