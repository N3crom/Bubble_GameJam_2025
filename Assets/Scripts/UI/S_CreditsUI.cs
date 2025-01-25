using System.Collections;
using UnityEngine;

public class S_CreditsUI : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float timeEnd;

    private void OnEnable()
    {
        StartCoroutine(TimeFinish());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator TimeFinish()
    {
        yield return new WaitForSeconds(timeEnd);

        gameObject.SetActive(false);
    }
}
