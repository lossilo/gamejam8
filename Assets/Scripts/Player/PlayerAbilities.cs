using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private AudioClip switchAbilitySound;

    private Abilities currentAbility;

    private PlayerMovement playerMovement;
    private PlayerTools playerTools;
    private AbilityDisplay abilityDisplay;
    private SoundEffectManager soundEffectManager;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerTools = GetComponent<PlayerTools>();
        abilityDisplay = FindFirstObjectByType<AbilityDisplay>();
        soundEffectManager = FindFirstObjectByType<SoundEffectManager>();

        abilityDisplay.ChangeAbility(0);
    }

    public void ChangeAbility()
    {
        soundEffectManager.PlaySound(switchAbilitySound);
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
