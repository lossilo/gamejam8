using UnityEngine;

public class DoorButtonScript : MonoBehaviour
{
    [SerializeField] DoorScript doorScript;
    [SerializeField] DoorScript doorScript2;
    [SerializeField] DoorScript doorScript3; 
    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            ButtonTriggered();

            Debug.Log("Button Triggerd");

        }
    }

    void ButtonTriggered()
    {

        doorScript.ChangeDoorState();
      
        if (doorScript2 != null)
        {
            doorScript2.doorOpen = !doorScript2.doorOpen;
            doorScript2.ChangeDoorState();
        }
        
        if (doorScript3 != null)
        {
            doorScript3.doorOpen = !doorScript3.doorOpen;
            doorScript3.ChangeDoorState();
        }
        
    }

}
