using UnityEngine;

/// <summary>
/// Example script showing how to add UI window that follows an object drawn by another camera.
/// </summary>

[AddComponentMenu("NGUI/Examples/Unit HUD")]
public class UnitHUD : MonoBehaviour
{
    /// <summary>
    /// Target object this UI element should follow.
    /// </summary>

    public Transform target;

    Transform mTrans;
    Camera mGameCam;
    Camera mUICam;
    Vector3 mPos;
    bool mVisible = true;

    void Start()
    {
        if (target == null) { Destroy(gameObject); return; }
        mTrans = transform;
        mGameCam = NGUITools.FindCameraForLayer(target.gameObject.layer);
        mUICam = NGUITools.FindCameraForLayer(gameObject.layer);
    }

    void LateUpdate()
    {
        if (target == null) { Destroy(gameObject); return; }

        mPos = mGameCam.WorldToViewportPoint(target.position);

        bool visible = (mPos.z > 0f && mPos.x > 0f && mPos.x < 1f && mPos.y > 0f && mPos.y < 1f);

        if (mVisible != visible)
        {
            mVisible = visible;
            UIWidget[] widgets = gameObject.GetComponentsInChildren<UIWidget>();
            foreach (UIWidget w in widgets) w.enabled = mVisible;
        }

        if (mVisible)
        {
            mPos = mUICam.ViewportToWorldPoint(mPos);
            mPos.z = 0f;
            mTrans.position = mPos;
        }
    }
}