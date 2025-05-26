using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private Abilities currentAbility;

    private void Start()
    {

    }

    public void ChangeAbility()
    {
        currentAbility++;
        Debug.Log(currentAbility);
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
