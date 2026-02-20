using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SpeedBoostController : MonoBehaviour
{


    [SerializeField] float boostMultiplier = 2f;
    [SerializeField] float boostDuration = 30f;
    [SerializeField] float collisionPenalty = 5f;
    [SerializeField] TextMeshProUGUI boosterTimer;


    float remainingTime = 0f;
    bool isBoostActive = false;

    Driver driver;

    void Awake()
    {
        driver = GetComponent<Driver>();
        boosterTimer.gameObject.SetActive(true);
       
    }

    private void Update()
    {
        if (!isBoostActive) return;
        remainingTime -= Time.deltaTime;
 
        boosterTimer.text = "" + Mathf.CeilToInt(remainingTime);

        if (remainingTime <= 0)
        {
            EndBoost();
        }
    }
    void ActivateBoost()
    {
        isBoostActive = true;
        remainingTime = boostDuration;
        driver.SprintMultiplier = boostMultiplier;
        boosterTimer.gameObject.SetActive(true);
    }

    void EndBoost()
    {
        isBoostActive = false;
        driver.SprintMultiplier = 1f;
        boosterTimer.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpeedBooster") && !isBoostActive)
        {
            ActivateBoost();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBoostActive)
        {
            remainingTime -= collisionPenalty;
            if (remainingTime <= 0) { 

            EndBoost();
            
            
            }
        }
    }

    void Start()
    {

    }

}
