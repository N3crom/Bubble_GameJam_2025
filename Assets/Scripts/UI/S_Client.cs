using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class S_Client : MonoBehaviour, IDropHandler
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
    [SerializeField] RSE_OnCustomerShake _rseOnCustomerShake;

    private Vector3 _originalPosition;
    private Customer _customer;


    // Start is called before the first frame update
    void Start()
    {
        _rseOnCustomerStateChange.action += ChangeStateSprite;
        _rseOnClientCreate.action += Initialize;

        _rectTransform.localScale = _startScale;
        //StartCoroutine(CustomerSpawn());
        _originalPosition = transform.localPosition;

        _rseOnCustomerShake.action += Shake;
    }

    private void OnDestroy()
    {
        _rseOnCustomerStateChange.action -= ChangeStateSprite;
        _rseOnClientCreate.action -= Initialize;

        _rseOnCustomerShake.action -= Shake;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            S_Carte draggable = draggedObject.GetComponent<S_Carte>();
            if (draggable != null)
            {
                int value = draggable.dataValue;

                _rseOnItemGive.RaiseEvent(value);
            }
        }
    }
    IEnumerator CustomerSpawn()
    {
        _imageClient.gameObject.GetComponent<Image>().enabled = false;

        _imageClient.color = Color.black;

        yield return new WaitForSeconds(1);

        _imageClient.gameObject.GetComponent<Image>().enabled = true;

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

            yield return null;
        }

        //_rectTransform.localScale = _endScale;
        _rseOnTimerStart.RaiseEvent();

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
        StartCoroutine(CustomerSpawn());
    }

    void ChangeStateSprite(CustomerState customerState)
    {
        _imageClient.sprite = _customer.SpritesDict[customerState];
    }


    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(_originalPosition.x + x, _originalPosition.y + y, _originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        GoToOriginalPosition();
    }

    public void GoToOriginalPosition()
    {
        transform.localPosition = _originalPosition;
    }
}
