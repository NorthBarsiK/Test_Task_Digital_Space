using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour, IChallenge
{
    public static HUD instance = null;
    void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject currentTimeElement = null;
    [SerializeField] private GameObject recordsListElement = null;
    [SerializeField] private GameObject useButtonTip = null;

    [SerializeField] private Text currentTime = null;
    [SerializeField] private Text recordsList = null;
    [SerializeField] private Text objective = null;
    
    [SerializeField] private string objectiveOnStart = "Нужно съесть бургер!";
    [SerializeField] private string objectiveOnChallengeStart = "Ох... нужно срочно найти что-нибудь попить!";
    [SerializeField] private string objectiveOnChallengeEnd = "Прекрасно! Предлагаю съесть ещё бургер!";

    void Start()
    {
        CheckElementsReferences();
        currentTimeElement.SetActive(false);
        recordsListElement.SetActive(false);
        objective.text = objectiveOnStart;
    }

    private void CheckElementsReferences()
    {
        if(currentTime == null ||
           recordsList == null ||
           objective == null ||
           currentTimeElement == null ||
           recordsListElement == null ||
           useButtonTip == null)
        {
            Debug.LogError("References to UI elements are not assigned!");
            Application.Quit();
        }
    }

    private bool isChallengeStart = false;
    public void ChallengeStart()
    {
        isChallengeStart = true;
        currentTimeElement.SetActive(true);
        objective.text = objectiveOnChallengeStart;
    }

    void Update()
    {
        if(isChallengeStart)
        {
            currentTime.text = Timer.instance.GetTimeStringFormatted();
        }
    }

    public void ChallengeEnd()
    {
        currentTimeElement.SetActive(false);
        recordsListElement.SetActive(true);
        recordsList.text = Timer.instance.GetRecordsFormatted();
        objective.text = objectiveOnChallengeEnd;
    }

    public void ShowUseButtonTip()
    {
        useButtonTip.SetActive(true);
    }

    public void HideUseButtonTip()
    {
        useButtonTip.SetActive(false);
    }
}
