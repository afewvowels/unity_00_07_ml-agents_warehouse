using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class WarehouseAgentVisual : Agent
{
    public GameObject sceneRotationObject;
    public Text actionsText;
    public int actionsTaken;
    public Academy w_Academy;
    public WareHouseSpawnPoints warehouse;
    public GameObject playerStartRoot;
    public GameObject objectStartRoot;
    public GameObject targetBox;

    public Camera cubeguyView;
    // public GameObject magnetPoint;
    // public GameObject heldBox;
    // public bool hasObject;
    // public bool isRaised;

    // public Transform holdResetPoint;

    public Rigidbody forkliftRB;
    public Rigidbody boxRB;

    private void Start()
    {
        // forkliftRB = this.gameObject.GetComponent<Rigidbody>();
        // holdResetPoint.transform.localPosition = magnetPoint.transform.localPosition;
    }

    public override void InitializeAgent()
    {
        actionsTaken = 0;
        actionsText.text = actionsTaken.ToString();
        w_Academy = FindObjectOfType<Academy>();
        forkliftRB = this.GetComponent<Rigidbody>();
        boxRB = targetBox.GetComponent<Rigidbody>();
    }

    public override void AgentAction(float[] vectorAction)
    {
        MoveAgent(vectorAction);
        actionsTaken++;
        actionsText.text = actionsTaken.ToString();
    }

    public void MoveAgent(float[] act)
    {
        var action = Mathf.FloorToInt(act[0]);

        // if (hasObject)
        // {
        //     switch (action)
        //     {
        //         case 1:
        //             forkliftRB.AddForce(transform.forward * 5.0f, ForceMode.Force);
        //             break;
        //         case 2:
        //             forkliftRB.AddForce(transform.forward * -2.5f, ForceMode.Force);
        //             break;
        //         case 3:
        //             forkliftRB.AddRelativeTorque(new Vector3(0.0f, 3.0f, 0.0f), ForceMode.Force);
        //             break;
        //         case 4:
        //             forkliftRB.AddRelativeTorque(new Vector3(0.0f, -3.0f, 0.0f), ForceMode.Force);
        //             break;
        //         case 5:
        //             ApplyForceToHeldObject();
        //             break;
        //     }
        // }
        // else
        // {
        //     switch (action)
        //     {
        //         case 1:
        //             forkliftRB.AddForce(transform.forward * 5.0f, ForceMode.Force);
        //             break;
        //         case 2:
        //             forkliftRB.AddForce(transform.forward * -2.5f, ForceMode.Force);
        //             break;
        //         case 3:
        //             forkliftRB.AddRelativeTorque(new Vector3(0.0f, 3.0f, 0.0f), ForceMode.Force);
        //             break;
        //         case 4:
        //             forkliftRB.AddRelativeTorque(new Vector3(0.0f, -3.0f, 0.0f), ForceMode.Force);
        //             break;
        //     }
        // }

        
        switch (action)
        {
            case 1:
                forkliftRB.AddRelativeForce(new Vector3(125.0f, 0.0f, 0.0f), ForceMode.Force);
                break;
            case 2:
                forkliftRB.AddRelativeForce(new Vector3(250.0f, 0.0f, 0.0f), ForceMode.Force);
                break;
            case 3:
                forkliftRB.AddRelativeForce(new Vector3(-85.0f, 0.0f, 0.0f), ForceMode.Force);
                break;
            case 4:
                transform.Rotate(0.0f, -4.0f, 0.0f);
                break;
            case 5:
                transform.Rotate(0.0f, 4.0f, 0.0f);
                break;
            case 6:
                forkliftRB.velocity = Vector3.zero;
                break;
        }
    }

    public override void AgentReset()
    {
        sceneRotationObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        int multiplier = Mathf.RoundToInt(Random.value * 10.0f);

        sceneRotationObject.transform.Rotate(0.0f, 90.0f * (float)multiplier, 0.0f); 

        int playerStartIndex;
        int objectStartIndex;

        playerStartIndex = Random.Range(0, warehouse.spawnPoints.Length - 1);
        objectStartIndex = Random.Range(0, warehouse.spawnPoints.Length - 1);

        while (playerStartIndex == objectStartIndex)
        {
            objectStartIndex = Random.Range(0, warehouse.spawnPoints.Length - 1);
        }

        GameObject playerStart = warehouse.spawnPoints[playerStartIndex];
        GameObject objectStart = warehouse.spawnPoints[objectStartIndex];
        
        this.transform.localRotation = Quaternion.Euler(0.0f, 360.0f * Random.value, 0.0f);
        this.transform.position = playerStart.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        playerStartRoot.transform.position = playerStart.transform.position;

        targetBox.transform.position = objectStart.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        targetBox.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        boxRB.velocity = Vector3.zero;
        forkliftRB.velocity = Vector3.zero;
        objectStartRoot.transform.position = objectStart.transform.position;

        actionsTaken = 0;
        actionsText.text = actionsTaken.ToString();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("warehousebox"))
        {
            var dir = c.contacts[0].point - transform.position;
            dir = dir.normalized;
            c.gameObject.GetComponent<Rigidbody>().AddForce(dir * 500.0f);
            AddReward(1.0f / 100.0f);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        // if (c.gameObject.CompareTag("warehousebox"))
        // {
        //     AddReward(1.0f / 1000.0f);
        // }
    }

    public override void CollectObservations()
    {
        // AddVectorObs(this.transform.position);
        // AddVectorObs(targetBox.transform.position);
        // AddVectorObs(this.transform.rotation.y);

        // AddVectorObs(forkliftRB.velocity.x);
        // AddVectorObs(forkliftRB.velocity.y);
        // AddVectorObs(forkliftRB.velocity.z);

        // AddVectorObs(boxRB.velocity.x);
        // AddVectorObs(boxRB.velocity.y);
        // AddVectorObs(boxRB.velocity.z);
    }

    // public void GrabBox(GameObject box)
    // {
    //     Debug.Log("grab box fired");
    //     Destroy(box.GetComponent<BoxCollider>());
    //     Destroy(box.GetComponent<Rigidbody>());
    //     box.transform.SetParent(magnetPoint.transform, false);
    //     box.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    //     // boxRB = box.GetComponent<Rigidbody>();
    //     heldBox = box;
    //     hasObject = true;
    //     StopAllCoroutines();
    //     StartCoroutine(RaiseBox());
    // }

    // public IEnumerator RaiseBox()
    // {
    //     Vector3 origin = magnetPoint.transform.localPosition;
    //     Vector3 destination = magnetPoint.transform.localPosition + new Vector3(0.0f, 2.0f, 0.0f);

    //     for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
    //     {
    //         magnetPoint.transform.localPosition = Vector3.Lerp(origin, destination, t);
    //         yield return null;
    //     }

    //     isRaised = true;
    // }

    // public void ApplyForceToHeldObject()
    // {
    //     if (isRaised)
    //     {
    //         heldBox.AddComponent<BoxCollider>();
    //         heldBox.AddComponent<Rigidbody>();
    //         boxRB = heldBox.GetComponent<Rigidbody>();
    //         boxRB.transform.SetParent(null, true);
    //         boxRB.AddRelativeForce(new Vector3(30.0f, 0.0f, 0.0f), ForceMode.Impulse);
    //         magnetPoint.transform.localPosition -= new Vector3(0.0f, 2.0f, 0.0f);
    //         hasObject = false;
    //         AddReward(1.0f / 100.0f);
    //     }
    //     else
    //     {
    //         AddReward(-1.0f / 25.0f);
    //     }
    // }
}
