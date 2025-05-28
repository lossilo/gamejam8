using UnityEngine;


public class SpikeScript : MonoBehaviour
{
    [SerializeField] int spikeDamage = 1;

    PlayerHealthManager healthManager;

    private void Start()
    {
        healthManager = FindFirstObjectByType<PlayerHealthManager>();
    }

    void DealDamage()
    {
        healthManager.TakeDamage(spikeDamage);
    }
    
    private void OnCollisionEnter2D(Collision2D Other)
    {
        if(Other.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

}
