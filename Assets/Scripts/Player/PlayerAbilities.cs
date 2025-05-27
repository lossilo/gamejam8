using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private Abilities currentAbility;

    private PlayerMovement playerMovement;
    private PlayerTools playerTools;
    private AbilityDisplay abilityDisplay;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerTools = GetComponent<PlayerTools>();
        abilityDisplay = FindFirstObjectByType<AbilityDisplay>();

        abilityDisplay.ChangeAbility(0);
    }

    public void ChangeAbility()
    {
        currentAbility++;

        if (currentAbility == Abilities.Total)
        {
            currentAbility = 0;
        }
        abilityDisplay.ChangeAbility((int)currentAbility);
    }

    public void UseAbility()
    {
        switch (currentAbility)
        {
            case Abilities.Jump:
                playerMovement.Jump();
                break;
            case Abilities.Dash:
                playerMovement.Dash();
                break;
            case Abilities.GroundPound:
                playerMovement.GroundPound();
                break;
            case Abilities.Sword:
                playerTools.SwordAttack();
                break;
        }
    }

    private enum Abilities
    {
        Jump,
        Dash,
        GroundPound,
        Sword,
        Total
    }
}
