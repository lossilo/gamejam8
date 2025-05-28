using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplay : MonoBehaviour
{
    [SerializeField] private Image[] abilityImages;

    public void ChangeAbility(int setAbility)
    {
        for (int i = 0; i < abilityImages.Length; i++)
        {
            if (setAbility == i)
            {
                abilityImages[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                abilityImages[i].color = new Color(1, 1, 1, 0.5f);
            }
        }
    }
}
