using UnityEngine;
using UnityEngine.UI;
public class ShootCircleUI : MonoBehaviour
{
    Image m_Image;
    private void Start() {
        m_Image=GetComponent<Image>();
    }
    public void UpdateUI(float m_OpenTime)
    {
        m_Image.fillAmount=m_OpenTime/ConstSetting.SHOOT_SPEED;
    }
}
