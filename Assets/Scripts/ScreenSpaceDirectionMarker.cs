
using UnityEngine;

/// <summary>
///     Controls directional UI marker which shows direction to an off-screen object.
/// </summary>
public class ScreenSpaceDirectionMarker : MonoBehaviour
{
    #region Variables

    [Header("References")]
    public RectTransform markerRoot;

    public RectTransform markerArrowPivot;
    public Transform target;

    /// <summary>
    ///     Distance of the marker from the edge of the screen in pixels.
    /// </summary>
    [Header("Settings")]
    public float screenEdgePadding = 50f;

    private Camera targetCamera;

    #endregion

    #region Unity Callbacks

    private void Start() { targetCamera = Camera.main; }

    public void LateUpdate() { UpdateMarker(); }

    #endregion

    #region Methods

    private void UpdateMarker()
    {
        var targetViewportPosition = targetCamera.WorldToViewportPoint(target.position);

        bool targetOnScreen =
            targetViewportPosition.x > 0f
            && targetViewportPosition.x < 1f
            && targetViewportPosition.y > 0f
            && targetViewportPosition.y < 1f;

        markerRoot.gameObject.SetActive(!targetOnScreen);

        UpdateMarkerPosition();
    }

    private void UpdateMarkerPosition()
    {
        // Position
        Vector2 targetScreenPosition = targetCamera.WorldToScreenPoint(target.position);
        var screenCenter = new Vector2
        {
            x = Screen.width * 0.5f,
            y = Screen.height * 0.5f
        };

        var markerPosition = targetScreenPosition;
        markerPosition.x = Mathf.Clamp(markerPosition.x, screenEdgePadding, Screen.width - screenEdgePadding);
        markerPosition.y = Mathf.Clamp(markerPosition.y, screenEdgePadding, Screen.height - screenEdgePadding);

        markerRoot.position = markerPosition;

        // Rotation.
        var centerToTargetVector = targetScreenPosition - screenCenter;
        markerArrowPivot.transform.localRotation = Quaternion.LookRotation(Vector3.forward, centerToTargetVector.normalized);
    }

    public void UpdateMarkerPublic()
    {
        UpdateMarker();
    }

    #endregion
}
