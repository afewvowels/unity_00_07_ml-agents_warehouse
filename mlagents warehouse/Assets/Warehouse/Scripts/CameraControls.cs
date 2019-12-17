using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public List<GameObject> warehouses = new List<GameObject>();
    public List<GameObject> uiroots = new List<GameObject>();

    public int active = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject warehouse in GameObject.FindGameObjectsWithTag("warehousescene"))
        {
            warehouses.Add(warehouse);
        }

        foreach (GameObject uiroot in GameObject.FindGameObjectsWithTag("uiroot"))
        {
            uiroots.Add(uiroot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            active++;
            if (active > warehouses.Count - 1)
            {
                active = 0;
            }
            this.transform.position = warehouses[active].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.transform.Rotate(0.0f, 90.0f, 0.0f);

            foreach (GameObject uiroot in uiroots)
            {
                uiroot.transform.Rotate(0.0f, 90.0f, 0.0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            this.transform.Rotate(0.0f, -90.0f, 0.0f);

            foreach (GameObject uiroot in uiroots)
            {
                uiroot.transform.Rotate(0.0f, -90.0f, 0.0f);
            }
        }
    }
}
