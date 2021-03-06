using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    private GameObject m_otherPortal = null;
    public float PortalOffset = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOtherPortal(GameObject obj)
    {
        Debug.Log("Setting");
        m_otherPortal = obj;
    }


    void OnTriggerEnter(Collider col)
    {
        
        Debug.Log(col.transform.tag);
        if (col.transform.CompareTag("Player") && m_otherPortal != null)
        {

            var relPoint = transform.InverseTransformPoint(col.transform.position);
            var relVelocity = -transform.InverseTransformDirection(col.GetComponent<Rigidbody>().velocity);

            var newVel = Vector3.Project(m_otherPortal.transform.TransformDirection(relVelocity), m_otherPortal.transform.up);
            if (Vector3.Dot(new Vector3(newVel.x, newVel.y, newVel.z), m_otherPortal.transform.up) < 0)
            {
                newVel *= -1;
            }

            col.GetComponent<Rigidbody>().velocity = newVel;
            col.transform.position = m_otherPortal.transform.TransformPoint(relPoint) + (col.GetComponent<Rigidbody>().velocity.normalized * PortalOffset);
            col.gameObject.transform.forward = m_otherPortal.transform.up;
        }
    }
}
