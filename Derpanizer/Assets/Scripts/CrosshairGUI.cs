using UnityEngine;

public class CrosshairGUI : MonoBehaviour
{
    public Texture2D m_crosshairTexture;
    public Texture2D m_useTexture;
    public float RayLength = 3f;

    public bool m_DefaultReticle;
    public bool m_UseReticle;
    public bool m_ShowCursor = false;

    private bool m_bIsCrosshairVisible = true;
    private Rect m_crosshairRect;
    private Ray playerAim;
    private Camera playerCam;

    void Update()
    {
        playerCam = Camera.main;
        playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool isHand = false;
        if (Physics.Raycast(playerAim, out hit, RayLength))
        {
           
            if (hit.collider.gameObject.CompareTag(Const.Const.DOOR))
            {
                isHand = true;
            }
            if (hit.collider.gameObject.CompareTag(Const.Const.FILE))
            {
                isHand = true;
            }
            SetCrosshairAsset(isHand);
        }
        else
        {
            SetCrosshairAsset(isHand);
        }
        /*
    if(!m_ShowCursor){
        Cursor.visible = (false);
        Cursor.lockState = CursorLockMode.Locked;			
    }else{
        Cursor.visible = (true);
        Cursor.lockState = CursorLockMode.None;						
    }
    */
        if (Input.GetKeyDown(KeyCode.C))
        {
            m_ShowCursor = !m_ShowCursor;
        }

        if (m_ShowCursor)
        {
            Cursor.visible = (true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = (false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void SetCrosshairAsset(bool isDefault)
    {
        if (!m_DefaultReticle && !m_UseReticle)
        {
            m_DefaultReticle = true;
        }
        if (m_DefaultReticle)
        {
            m_crosshairRect = new Rect((Screen.width - m_crosshairTexture.width) / 2,
                (Screen.height - m_crosshairTexture.height) / 2,
                m_crosshairTexture.width,
                m_crosshairTexture.height);
        }

        if (m_UseReticle)
        {
            m_crosshairRect = new Rect((Screen.width - m_useTexture.width) / 2,
                (Screen.height - m_useTexture.height) / 2,
                m_useTexture.width,
                m_useTexture.height);
        }
    }

    void OnGUI()
    {
        if (m_bIsCrosshairVisible)
            if (m_DefaultReticle)
            {
                GUI.DrawTexture(m_crosshairRect, m_crosshairTexture);
            }
        if (m_UseReticle)
        {
            GUI.DrawTexture(m_crosshairRect, m_useTexture);
        }
    }
}