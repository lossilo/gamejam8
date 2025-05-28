using UnityEngine;


public class SpikeScript : MonoBehaviour
{

    //int spikeDamage = 1; 
    
    void DealDamage()
    {
        //health = health - spikeDamage; 
        Debug.Log("Du dör din gris"); 
    }
    
    private void OnCollisionEnter2D(Collision2D Other)
    {
        if(Other.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

}
