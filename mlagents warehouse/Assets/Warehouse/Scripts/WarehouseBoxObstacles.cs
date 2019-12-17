using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseBoxObstacles : MonoBehaviour
{
    public int score;
    public WarehouseObstacleAgent w_Agent;
    public Text scoreText;

    private void Start()
    {
        score = 0;
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("warehousegoal"))
        {
            score++;
            scoreText.text = score.ToString();

            if (w_Agent.actionsTaken < 1500)
            {
                w_Agent.AddReward(1.0f);
            }
            else if (w_Agent.actionsTaken < 2500)
            {
                w_Agent.AddReward(0.75f);
            }
            else if (w_Agent.actionsTaken < 4500)
            {
                w_Agent.AddReward(0.5f);
            }
            else if (w_Agent.actionsTaken < 7000)
            {
                w_Agent.AddReward(0.25f);
            }

            w_Agent.AddReward(1.0f);

            w_Agent.Done();
        }
    }
}
