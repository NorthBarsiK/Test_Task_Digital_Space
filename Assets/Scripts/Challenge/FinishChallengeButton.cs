using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishChallengeButton : MonoBehaviour, IChallenge, IButton
{
    private StartChallengeButton StartButton = null;
    private void Start()
    {
        StartButton = GameObject.FindGameObjectWithTag("Start").GetComponentInChildren<StartChallengeButton>();
        gameObject.SetActive(false);
    }

    public void OnButtonPressed()
    {
        ChallengeEnd();
    }

    public void ChallengeStart()
    {
        gameObject.SetActive(true);
    }

    public void ChallengeEnd()
    {
        Timer.instance.ChallengeEnd();
        HUD.instance.ChallengeEnd();
        StartButton.ChallengeEnd();
        gameObject.SetActive(false);
    }
}
