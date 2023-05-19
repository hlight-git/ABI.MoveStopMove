using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TmpTargetIndicator : UIUnit
{
    protected readonly Vector3 screenQuarter = new Vector2(Screen.width, Screen.height) / 2;

    protected Transform target;
    protected Vector3 viewportPoint;
    protected Camera mainCamera;
    protected bool isVisible;

    [Header("TargetIndicator attributes:")]
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected RectTransform arrow;
    [SerializeField] protected Image arrowImage;
    

    protected bool IsVisible => (
        TmpUtil.IsInRange(viewportPoint.x, GameConstant.Indicator.HORIZONTAL_VIEWPORT_BOUND) &&
        TmpUtil.IsInRange(viewportPoint.y, GameConstant.Indicator.VERTICAL_VIEWPORT_BOUND)
    );

    protected virtual void LateUpdate()
    {
        viewportPoint = mainCamera.WorldToViewportPoint(target.position);
        isVisible = IsVisible;

        arrowImage.gameObject.SetActive(isVisible);

        viewportPoint.x = Mathf.Clamp(
            viewportPoint.x,
            GameConstant.Indicator.HORIZONTAL_VIEWPORT_BOUND.x,
            GameConstant.Indicator.HORIZONTAL_VIEWPORT_BOUND.y);
        viewportPoint.y = Mathf.Clamp(
            viewportPoint.y,
            GameConstant.Indicator.VERTICAL_VIEWPORT_BOUND.x,
            GameConstant.Indicator.VERTICAL_VIEWPORT_BOUND.y);

        Vector3 targetScreenPoint = mainCamera.ViewportToScreenPoint(viewportPoint) - screenQuarter;
        Vector3 playerScreenPoint = mainCamera.WorldToScreenPoint(TmpLevelManager.Ins.Player.TF.position) - screenQuarter;
        UnitTF.anchoredPosition = targetScreenPoint;

        arrow.up = (targetScreenPoint - playerScreenPoint).normalized;
    }

    public virtual void OnInit(Transform target)
    {
        this.target = target;
        mainCamera = CameraFollower.Ins.Camera;
    }
    public virtual void SetArrowColor(Color color)
    {
        arrowImage.color = color;
    }
    public void SetAlpha(float value)
    {
        canvasGroup.alpha = value;
    }
}
