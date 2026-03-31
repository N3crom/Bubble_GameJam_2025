using Sirenix.OdinInspector;
using UnityEngine;

public class S_MainMenuManager : MonoBehaviour
{
    [TabGroup("Settings")]
    [Title("Audio")]
    [SerializeField] private S_ClassAudio audioMainMenu;

    [TabGroup("Outputs")]
    [SerializeField] private RSE_OnMainMenu rseOnMainMenu;

    [TabGroup("Outputs")]
    [SerializeField] private RSE_OnPlayAudio rsePlayAudio;

    [TabGroup("Outputs")]
    [SerializeField] private RSE_OnShowMouseCursor rseOnShowMouseCursor;

    [TabGroup("Outputs")]
    [SerializeField] private RSE_OnHideMouseCursor rseOnHideMouseCursor;

    [TabGroup("Outputs")]
    [SerializeField] private RSE_OnNeedCursor rseOnNeedCursor;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void OnDisable()
    {
        rsoSettingsSaved.Value = new();
    }

    private void Start()
    {
        rseOnShowMouseCursor.Call();
        rseOnNeedCursor.Call(true);
        rseOnMainMenu.Call();
        rsePlayAudio.Call(audioMainMenu);
    }
}