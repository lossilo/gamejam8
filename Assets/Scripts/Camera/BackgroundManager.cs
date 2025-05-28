using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private BackgroundPart[] backgroundParts;
    [SerializeField] private Transform cameraTransform;

    private void Update()
    {
        foreach (var part in backgroundParts)
        {
            Transform partTransform = part.backgroundPart.transform;
            float newXPosition = (cameraTransform.position.x - part.originalPosition) * part.moveMultiplier;
            partTransform.position = new Vector2(newXPosition, partTransform.position.y);
        }
    }
}

[System.Serializable]
public struct BackgroundPart
{
    public GameObject backgroundPart;
    public float moveMultiplier;
    public float originalPosition;
}
