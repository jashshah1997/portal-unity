using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portalLeft;
    public GameObject portalRight;

    private GameObject m_currentLeft = null;
    private GameObject m_currentRight = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MakePortal(portalLeft, ref m_currentLeft))
            {

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (MakePortal(portalRight, ref m_currentRight))
            {

            }
        }
    }

    bool MakePortal(GameObject portal, ref GameObject current)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool success = Physics.Raycast(ray, out hit);

        // Check for hit
        if (!success)
        {
            return false;
        }

        // Check for portal surface tag
        if (!hit.transform.gameObject.CompareTag("PortalSurface"))
        {
            return false;
        }

        Vector3 position = hit.point;
        Quaternion rotation = Quaternion.LookRotation(hit.normal);

        if (current)
        {
            Destroy(current);
        }

        current = Instantiate(portal, position, rotation * Quaternion.Euler(0, 90, 90));

        return true;
    }
}
