using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Sprite normalButtonSprite;
    [SerializeField] private Sprite pressedButtonSprite;

    private bool isHoveringOverButton;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (MouseIsTouching())
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                buttonImage.sprite = pressedButtonSprite;
            }
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
            {
                buttonImage.sprite = normalButtonSprite;
                CallAbility();
            }
        }
        else
        {
            buttonImage.sprite = normalButtonSprite;
        }
    }

    public void CallAbility()
    {
        FindFirstObjectByType<PlayerAbilities>().UseAbility();
    }

    private bool MouseIsTouching()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector2 buttonBottomCorner = new Vector2(1920 - rectTransform.rect.width, 1080 - rectTransform.rect.height);
        Vector2 mouseInput = Input.mousePosition;

        bool isCorrectOnX = mouseInput.x > buttonBottomCorner.x;
        bool isCorrectOnY = mouseInput.y > buttonBottomCorner.y;

        return isCorrectOnX && isCorrectOnY;
    }
}
