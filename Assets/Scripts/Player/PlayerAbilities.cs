using TMPro;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tempAbilityDisplay;

    private Abilities currentAbility;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseAbility();
        }
    }

    public void ChangeAbility()
    {
        currentAbility++;

        if (currentAbility == Abilities.Total)
        {
            currentAbility = 0;
        }

        tempAbilityDisplay.text = "Current Ability: " + currentAbility;
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
                break;
            case Abilities.Sword:
                break;
            case Abilities.WallBreak:
                break;
        }
    }

    private enum Abilities
    {
        Jump,
        Dash,
        GroundPound,
        Sword,
        WallBreak,
        Total
    }
}
