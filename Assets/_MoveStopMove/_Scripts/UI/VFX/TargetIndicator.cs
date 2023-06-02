using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : UIUnit
{
    protected readonly Vector3 screenQuarter = new Vector2(Screen.width, Screen.height) / 2;

    protected Transform target;
    protected Vector3 viewportPoint;
    protected Camera mainCamera;
    protected bool isVisible;

    [Header("- Target Indicator:")]
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected RectTransform arrow;
    [SerializeField] protected Image arrowImage;
    

    protected bool IsVisible => (
        Util.IsInRange(viewportPoint.x, Constant.Indicator.HORIZONTAL_VISIBLE_AREA_BOUND) &&
        Util.IsInRange(viewportPoint.y, Constant.Indicator.VERTICAL_VISIBLE_AREA_BOUND)
    );

    protected virtual void LateUpdate()
    {
        viewportPoint = mainCamera.WorldToViewportPoint(target.position);
        isVisible = IsVisible;

        arrowImage.gameObject.SetActive(!isVisible);

        viewportPoint.x = Mathf.Clamp(
            viewportPoint.x,
            Constant.Indicator.HORIZONTAL_VISIBLE_AREA_BOUND.x,
            Constant.Indicator.HORIZONTAL_VISIBLE_AREA_BOUND.y);
        viewportPoint.y = Mathf.Clamp(
            viewportPoint.y,
            Constant.Indicator.VERTICAL_VISIBLE_AREA_BOUND.x,
            Constant.Indicator.VERTICAL_VISIBLE_AREA_BOUND.y);

        Vector3 targetScreenPoint = mainCamera.ViewportToScreenPoint(viewportPoint) - screenQuarter;
        Vector3 playerScreenPoint = mainCamera.WorldToScreenPoint(LevelManager.Ins.Player.TF.position) - screenQuarter;
        RectTF.anchoredPosition = targetScreenPoint;

        arrow.up = (targetScreenPoint - playerScreenPoint).normalized;
    }

    public virtual void OnSpawn(Transform target)
    {
        this.target = target;
        mainCamera = CameraFollower.Ins.Camera;
    }
    public virtual void SetColor(Color color)
    {
        arrowImage.color = color;
    }
    public void SetAlpha(float value)
    {
        canvasGroup.alpha = value;
    }
}
