using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseBox : MonoBehaviour
{
    public int score;
    public WarehouseAgent w_Agent;
    public Text scoreText;

    private void Start()
    {
        score = 0;
    }
    private void OnTriggerEnter(Collider c)
    {
        // if (c.gameObject.CompareTag("forklift"))
        // {
        //     c.gameObject.GetComponent<WarehouseAgent>().GrabBox(this.gameObject);
        // }
        if (c.gameObject.CompareTag("goal"))
        {
            // c.gameObject.GetComponent<WarehouseGoal>().GoalReached();
            score++;
            scoreText.text = score.ToString();
            w_Agent.AddReward(1.0f);
            w_Agent.Done();
        }
    }
}
