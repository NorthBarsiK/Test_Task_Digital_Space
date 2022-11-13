using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChallengeButton : MonoBehaviour, IChallenge, IButton
{
    private FinishChallengeButton FinishButton = null;
    private void Start()
    {
        FinishButton = GameObject.FindGameObjectWithTag("Finish").GetComponentInChildren<FinishChallengeButton>();
    }

    public void OnButtonPressed()
    {
        ChallengeStart();
    }

    public void ChallengeStart()
    {
        Timer.instance.ChallengeStart();
        HUD.instance.ChallengeStart();
        FinishButton.ChallengeStart();
        gameObject.SetActive(false);
    }

    public void ChallengeEnd()
    {
        gameObject.SetActive(true);
    }
}
