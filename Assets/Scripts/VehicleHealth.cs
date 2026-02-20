using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VehicleHealth : MonoBehaviour
{

    [SerializeField] int MaxHealth = 100;
    int CurrentHealth;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip hitDamage;
    AudioSource audioSource;
    [SerializeField] Image healthFill;
    [SerializeField] Color fullHealthColor = Color.green;
    [SerializeField] Color lowHealthColor = Color.red;





    private void Awake()
    {
        CurrentHealth = MaxHealth;
        audioSource = GetComponent<AudioSource>();
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float healthPercent = (float)CurrentHealth / MaxHealth;
        healthFill.fillAmount = healthPercent;
        healthFill.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercent);
    }

    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        UpdateHealthBar();
        if (CurrentHealth <= 0)
        {
            Die();
        }
        
    }



    void Die()
    {
       
        GetComponent<Driver>().enabled = false;
        audioSource.PlayOneShot(explosionSound);
        SceneManager.LoadSceneAsync(2);
  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        audioSource.PlayOneShot(hitDamage);
        TakeDamage(5);
        
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UpdateHealthBar();
       

    }
    

}
