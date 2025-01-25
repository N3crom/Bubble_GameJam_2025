using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Client : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _timerToCustomerAparition;
    [SerializeField] Vector3 _startScale;
    [SerializeField] Vector3 _endScale;

    [Header("Reference")]
    [SerializeField] Image _imageClient;
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] RSE_OnTimerStart _rseOnTimerStart;
    [SerializeField] RSE_OnClientLeave _rseOnClientLeave;
    [SerializeField] RSE_OnTimerEnd _rseOnTimerEnd;
    [SerializeField] RSE_OnItemGive _rseOnItemGive;
    [SerializeField] RSE_OnClientCreate _rseOnClientCreate;
    [SerializeField] RSE_OnCustomerStateChange _rseOnCustomerStateChange;
    
    private Customer _customer;


    // Start is called before the first frame update
    void Start()
    {
        _rseOnCustomerStateChange.action += ChangeStateSprite;
        _rseOnClientCreate.action += Initialize;

        _imageClient.gameObject.SetActive(false);
        _rectTransform.localScale = _startScale;

    }

    private void OnDestroy()
    {
        _rseOnCustomerStateChange.action -= ChangeStateSprite;
        _rseOnClientCreate.action -= Initialize;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CustomerSpawn()
    {
        _imageClient.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < _timerToCustomerAparition)
        {
            timer += Time.deltaTime;

            if (_imageClient != null)
            {
                _imageClient.color = Color.Lerp(Color.black, Color.white, timer / _timerToCustomerAparition);
            }

            if (_rectTransform != null)
            {
                _rectTransform.localScale = Vector3.Lerp(_startScale, _endScale, timer / _timerToCustomerAparition);
            }


            _rectTransform.localScale = _endScale;
        }
        yield return null;
    }

    IEnumerator CustomerLeave()
    {
        _imageClient.gameObject.SetActive(false) ;
        _rectTransform.localScale = _startScale;

        yield return null;
    }

    void Initialize(Customer customer)
    {
        _customer = customer;
        _imageClient.sprite = customer.SpritesDict[customer.CustomerState];
    }

    void ChangeStateSprite(CustomerState customerState)
    {
        _imageClient.sprite = _customer.SpritesDict[customerState];
    } 
}
