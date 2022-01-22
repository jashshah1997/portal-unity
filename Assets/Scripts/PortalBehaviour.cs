using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    private GameObject m_otherPortal = null;

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
            col.gameObject.GetComponent<CharacterController>().enabled = false;
            col.gameObject.transform.position = m_otherPortal.transform.position - Quaternion.Euler(0, 90, 0) * m_otherPortal.transform.forward * 1.5f;
            col.gameObject.transform.forward = m_otherPortal.transform.up;
            col.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
