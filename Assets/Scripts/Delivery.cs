using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delivery : MonoBehaviour
{

    private int iceCreamCount = 0;
    [SerializeField] int iceCreamPackage = 26;
    [SerializeField] GameObject deliveryParticlePrefab;
    [SerializeField] AudioClip deliverySoundEffect;
    [SerializeField] TextMeshProUGUI iceCreamText;
    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package"))
        {
            iceCreamCount += iceCreamPackage;
            UpdateUI();
            Destroy(collision.gameObject, 0.3f);
        }


        if (collision.CompareTag("Customer") && iceCreamCount > 0)
        {
            iceCreamCount--;
            UpdateUI();
            Instantiate(deliveryParticlePrefab, collision.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(deliverySoundEffect);
            GetComponent<VehicleHealth>().Heal(10);
            Destroy(collision.gameObject, 0.5f);
            Invoke(nameof(CheckWinCondition), 0.6f);

            if (iceCreamCount <= 0)
            {
                SceneManager.LoadSceneAsync(2);
            }

        }

    }

    void CheckWinCondition()
    {
        GameObject[] remainingCustomers = GameObject.FindGameObjectsWithTag("Customer");
        if (iceCreamCount <= 0 && remainingCustomers.Length == 0)
        {
           SceneManager.LoadSceneAsync(3);
        }

        else if (iceCreamCount <= 0 && remainingCustomers.Length > 0)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }

    void UpdateUI()
    {
        iceCreamText.text = iceCreamCount.ToString();
    }

}
