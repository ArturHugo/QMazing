using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QLearning : MonoBehaviour
{
    public Agent agent;
    public Environment environment;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            environment.animate = false;
            environment.showGUI = false;
            agent.runEpisodes(1000,1000);
        }
        else if (Input.GetKeyDown("g"))
        {
            environment.animate = true;
            environment.showGUI = false;
            agent.runEpisodes(1, 20);
        }
        else if (Input.GetKeyDown("h"))
        {
            environment.animate = true;
            environment.showGUI = true;
            double tempEpsilon = agent.epsilon;
            agent.epsilon = 0;
            agent.runEpisodes(1, 1);
            agent.epsilon = tempEpsilon;
        }
        else if (Input.GetKeyDown("r"))
        {
            environment.EndEpisode();
        }
    }
}