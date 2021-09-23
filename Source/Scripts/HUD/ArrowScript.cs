using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NumSharp;
using System;

public class ArrowScript : MonoBehaviour
{
    public Agent agent;
    public Environment environment;
    public int index;
    public TextMeshProUGUI textmeshPro;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (environment.showGUI)
        {
            transform.GetChild(0).gameObject.SetActive(true);

            int curState = agent.currentState;
            double max = 0;

            for (int i =0; i< 4; i++)
            {
                double directionReward = Math.Abs(agent.qTable[curState][i]);
                if (directionReward > max)
                    max = directionReward;
            }
            
            double number = (agent.qTable[curState][index]/max);
            Debug.Log(agent.qTable[curState][index]);
            textmeshPro.SetText(number.ToString("0.####"));
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
}
