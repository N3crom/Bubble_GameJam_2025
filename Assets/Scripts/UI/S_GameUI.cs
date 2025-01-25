using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_GameUI : MonoBehaviour
{
    [Header("RSO")]
    [SerializeField] private RSO_CurrentCustomers currentCustomer;

    [SerializeField] private RSO_Score score;

    [SerializeField] private RSO_ImpatientTime impatientTime;

    [SerializeField] private RSO_Reputation reputation;

    [Header("RSE")]
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_OnScoreChanged onScoreChanged;
    [SerializeField] private RSE_OnReputationChanged onReputationChanged;
    [SerializeField] private RSE_OnTimerStart onTimerStart;

    [Header("References")]
    [SerializeField] private S_UIManager uiManager;

    [SerializeField] private TextMeshProUGUI textScore;

    [SerializeField] private TextMeshProUGUI textimpatientTime;
    [SerializeField] private Slider sliderimpatientTime;

    [SerializeField] private Slider sliderReputation;
    [SerializeField] private TextMeshProUGUI textReputation;

    [SerializeField] private TextMeshProUGUI textdescription;

    private void OnEnable()
    {
        onScoreChanged.action += ScoreChange;
        onReputationChanged.action += ReputationChanged;

        onTimerStart.action += StartTimer;
    }

    private void OnDisable()
    {
        onScoreChanged.action -= ScoreChange;
        onReputationChanged.action -= ReputationChanged;

        onTimerStart.action -= StartTimer;
    }

    private void Start()
    {
        textScore.text = score.Score.ToString();

        textimpatientTime.text = impatientTime.ImpatientTime.ToString() + "s";
        sliderimpatientTime.value = impatientTime.ImpatientTime;
        sliderimpatientTime.maxValue = impatientTime.ImpatientTime;

        sliderReputation.value = reputation.ReputationCurrency;
        textReputation.text = reputation.ReputationCurrency.ToString() + "%";
    }

    private void StartTimer()
    {
        textdescription.transform.parent.gameObject.SetActive(true);

        int index = Random.Range(0, currentCustomer.CurrentCustomer.ItemWanted.Descriptions.Count);

        textdescription.text = currentCustomer.CurrentCustomer.ItemWanted.Descriptions[index];
    }

    private void ScoreChange()
    {
        textScore.text = score.Score.ToString();
    }

    private void ReputationChanged()
    {
        sliderReputation.value = reputation.ReputationCurrency;
        textReputation.text = reputation.ReputationCurrency.ToString() + "%";
    }
}
