using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class WarehouseObstacleAgent : Agent
{
    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject sceneRotationObject;
    public GameObject obstaclePrefab;
    public Text actionsText;
    public int actionsTaken;
    public Academy w_Academy;
    public WareHouseSpawnPoints warehouse;
    public GameObject playerStartGraphic;
    public GameObject objectStartGraphic;
    public GameObject targetBox;
    // public GameObject magnetPoint;
    // public GameObject heldBox;
    // public bool hasObject;
    // public bool isRaised;

    // public Transform holdResetPoint;

    public Rigidbody forkliftRB;
    public Rigidbody boxRB;

    private void Start()
    {
        actionsTaken = 0;
        actionsText.text = actionsTaken.ToString();
        w_Academy = FindObjectOfType<Academy>();
        forkliftRB = this.GetComponent<Rigidbody>();
        boxRB = targetBox.GetComponent<Rigidbody>();
    }

    public override void InitializeAgent()
    {
    }

    public override void AgentAction(float[] vectorAction)
    {
        MoveAgent(vectorAction);
        actionsTaken++;
        actionsText.text = actionsTaken.ToString();
    }

    public void MoveAgent(float[] act)
    {
        var moveAction = Mathf.FloorToInt(act[0]);
        var rotateAction = Mathf.FloorToInt(act[1]);

        switch (moveAction)
        {
            case 1:
                // forkliftRB.AddRelativeForce(new Vector3(125.0f, 0.0f, 0.0f), ForceMode.Force);
                forkliftRB.AddRelativeForce(new Vector3(0.0f, 0.0f, 1.0f), ForceMode.VelocityChange);
                break;
            case 2:
                // forkliftRB.AddRelativeForce(new Vector3(250.0f, 0.0f, 0.0f), ForceMode.Force);
                forkliftRB.AddRelativeForce(new Vector3(0.0f, 0.0f, 2.0f), ForceMode.VelocityChange);
                break;
            case 3:
                // forkliftRB.AddRelativeForce(new Vector3(-125.0f, 0.0f, 0.0f), ForceMode.Force);
                forkliftRB.AddRelativeForce(new Vector3(0.0f, 0.0f, -0.75f), ForceMode.VelocityChange);
                break; 
        }

        switch (rotateAction)
        {
            case 1:
                transform.Rotate(0.0f, -5.0f, 0.0f);
                break;
            case 2:
                transform.Rotate(0.0f, 5.0f, 0.0f);
                break;
        }
    }

    public override void AgentReset()
    {
        DestroyObstacles();

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
        this.transform.position = playerStart.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        playerStartGraphic.transform.position = playerStart.transform.position;

        float randomScale = Random.Range(w_Academy.resetParameters["min_sphere_scale"], w_Academy.resetParameters["max_sphere_scale"]);

        targetBox.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        targetBox.transform.position = objectStart.transform.position + new Vector3(0.0f, 1.5f, 0.0f);
        targetBox.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        float randomMass = Random.Range(w_Academy.resetParameters["min_sphere_mass"], w_Academy.resetParameters["max_sphere_mass"]);

        boxRB.mass = randomMass;
        boxRB.velocity = Vector3.zero;
        boxRB.angularVelocity = Vector3.zero;
        forkliftRB.velocity = Vector3.zero;
        forkliftRB.angularVelocity = Vector3.zero;
        objectStartGraphic.transform.position = objectStart.transform.position;

        int obstacleCount = Random.Range((int)w_Academy.resetParameters["obstaclecount"], (int)w_Academy.resetParameters["maxobstaclecount"]);

        if (obstacleCount == 5)
        {
            obstacleCount = 4;
        }

        if (obstacleCount > 0)
        {
            GameObject[] obstacleSpawns = new GameObject[obstacleCount];
            GameObject obstacle;
            obstacleSpawns = warehouse.GetObstacleSpawns(obstacleCount);

            for (int i = 0; i < obstacleCount; i++)
            {
                obstacle = (GameObject)Instantiate(obstaclePrefab);
                obstacle.transform.position = obstacleSpawns[i].transform.position;
                obstacle.transform.localRotation = Quaternion.Euler(0.0f, 90.0f * (float)multiplier, 0.0f);
                obstacles.Add(obstacle);
            }
        }

        actionsTaken = 0;
        actionsText.text = actionsText.ToString();
    }

    public override void CollectObservations()
    {
        AddVectorObs(actionsTaken);
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        if (Input.GetKey(KeyCode.W))
        {
            action[0] = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            action[0] = 3.0f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            action[0] = 2.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            action[1] = 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            action[1] = 2.0f;
        }

        return action;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("warehousebox"))
        {
            var dir = c.contacts[0].point - transform.position;
            dir = dir.normalized;
            c.gameObject.GetComponent<Rigidbody>().AddForce(dir * 1000.0f);
            AddReward(1.0f / 100.0f);
        }

        if (c.gameObject.CompareTag("warehouseobstacle"))
        {
            var dir = c.contacts[0].point - transform.position;
            dir = dir.normalized;
            c.gameObject.GetComponent<Rigidbody>().AddForce(dir * 3500.0f);
            c.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(dir * 300.0f);
            AddReward(1.0f / 500.0f);
        }
    }

    public void DestroyObstacles()
    {
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        obstacles.Clear();
    }
}
