using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NumSharp;

public class Agent : MonoBehaviour
{
    // Q-Learning variables
    public Environment env;
    public NDArray qTable;
    public int currentState;
    public double learningRate = 0.1f;
    public double discountFactor = 0.99f;

    public double epsilon;
    public double initEpsilon = 0.8f;
    public double epsilonDecay = 0.99f;

    // Management Variables
    public int nextState;
    public int nActions;

    // Reward definitions
    static double goalReward = 100;     // Reward for reaching the goal
    static double wallReward = -10000;  // Reward for hitting a wall
    static double normReward = -1;      // Normal reward for walking to empty cell

    public void initAgent(int nActions, int nStates, int initState)
    {
        this.qTable         = np.zeros(nStates, nActions);
        this.nActions       = nActions;
        this.currentState   = initState;

        epsilon = initEpsilon;
    }

    public void runEpisodes(int nEpisodes, int maxSteps) {
      for(int episode = 0; episode < nEpisodes; episode++) {
        for(int step = 0; step < maxSteps; step++) {
          // break if agent reached goal
          if(this.iterate()) {
            env.EndEpisode();
            break;
          }
        }
        epsilon *= epsilonDecay;

        // for presentation only
        if(maxSteps > 1)
        {
            env.EndEpisode();
        }
      }
    }

    public bool iterate() {
      int action   = chooseAction(currentState);
      nextState    = env.updateState(action);
      double reward = computeReward();
      double oldValue = qTable[currentState][action];

      double optimalFutureValue = np.max(qTable[nextState]);

      qTable[currentState][action] =
          (1-learningRate)*oldValue +
          learningRate*(reward + discountFactor*optimalFutureValue);

      currentState = nextState;

      // If the reward was the goal reward, continue to next episode
      if((reward == goalReward)){
            return true;    // Episode has ended
      }

      return false;         // Continue episode
    }

    double computeReward() {
      if(nextState == currentState)
        return wallReward;
      if(env.checkGoal(nextState)) {
        return goalReward;
      }
      return normReward;
    }

    int chooseAction(int state) {
      int action;
      double rand = Random.Range(0,1f);
      if(rand > epsilon) {
        // Choose optimal action with probability 1-epsilon
        action = (int) np.argmax(qTable[state]);
      } else {
        // Choose random action with probability epsilon
        action = Random.Range(0,nActions);
      }
      return action;
    }
}
