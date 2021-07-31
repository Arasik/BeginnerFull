using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField]
	private Color32 topColor = Color.clear;
	[SerializeField]
	private Color32 bottomColor = Color.white;
    public Text m_Text;
    public float m_NowTime=ConstSetting.INIT_TIME;

    private void Start() {
        m_Text=GetComponent<Text>();
        topColor.a=ConstSetting.COLOR_ALPHA;
    }
    private void Update() {
        m_NowTime+=Time.deltaTime;
        m_Text.color=Color32.Lerp(bottomColor, topColor, m_NowTime/ConstSetting.UI_FADE_TIME);
        if(m_NowTime>=ConstSetting.UI_FADE_TIME)
        {
            Destroy(gameObject);
        }
    }
}
