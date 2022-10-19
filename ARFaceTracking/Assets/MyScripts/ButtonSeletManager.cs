using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSeletManager : MonoBehaviour
{

    public GameObject aRSessionOrigin;
    public enum eNationMode
    {
        _eKoreaMode = 0,
        _eChinseMode,
        _eJapanMode,
    }
    public eNationMode eSelMode = eNationMode._eKoreaMode;
    public GameObject[] Nationbtns;


    public GameObject[] eXplaneGroup;

    public static ButtonSeletManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    void Start()
    {
        KoreaSel();
    }
    public void KoreaSel()
    {
        eSelMode = eNationMode._eKoreaMode;
        Nationbtns[(int)eNationMode._eKoreaMode].transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Nationbtns[(int)eNationMode._eChinseMode].transform.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        Nationbtns[(int)eNationMode._eJapanMode].transform.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        
        Nationbtns[(int)eNationMode._eKoreaMode].GetComponent<ScaleAnim>().bAnim = true;
        Nationbtns[(int)eNationMode._eChinseMode].GetComponent<ScaleAnim>().bAnim = false;
        Nationbtns[(int)eNationMode._eJapanMode].GetComponent<ScaleAnim>().bAnim = false;

        if (aRSessionOrigin != null)
        {
            ARCoreFaceRegonManager.instance.InitKoreaMode();
            aRSessionOrigin.SetActive(false);
            StartCoroutine("MyTimeCall");
        }
    }

    public void KoreaExSel()
    {
        eXplaneGroup[(int)eNationMode._eKoreaMode].SetActive(true);
        eXplaneGroup[(int)eNationMode._eChinseMode].SetActive(false);
        eXplaneGroup[(int)eNationMode._eJapanMode].SetActive(false);
        KoreaSel();
    }

    public void ChinseExSel()
    {
        eXplaneGroup[(int)eNationMode._eKoreaMode].SetActive(false);
        eXplaneGroup[(int)eNationMode._eChinseMode].SetActive(true);
        eXplaneGroup[(int)eNationMode._eJapanMode].SetActive(false);
        ChinseSel();
    }
    public void JapanEXSel()
    {
        eXplaneGroup[(int)eNationMode._eKoreaMode].SetActive(false);
        eXplaneGroup[(int)eNationMode._eChinseMode].SetActive(false);
        eXplaneGroup[(int)eNationMode._eJapanMode].SetActive(true);
        JapanSel();
    }

    public void ChinseSel()
    {
        eSelMode = eNationMode._eChinseMode;
        Nationbtns[(int)eNationMode._eKoreaMode].transform.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        Nationbtns[(int)eNationMode._eChinseMode].transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Nationbtns[(int)eNationMode._eJapanMode].transform.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        
        Nationbtns[(int)eNationMode._eKoreaMode].GetComponent<ScaleAnim>().bAnim = false;
        Nationbtns[(int)eNationMode._eChinseMode].GetComponent<ScaleAnim>().bAnim = true;
        Nationbtns[(int)eNationMode._eJapanMode].GetComponent<ScaleAnim>().bAnim = false;
        if (aRSessionOrigin != null)
        {
            ARCoreFaceRegonManager.instance.InitChinseMode();
            aRSessionOrigin.SetActive(false);
            StartCoroutine("MyTimeCall");
        }
    }

    public void JapanSel()
    {
        eSelMode = eNationMode._eJapanMode;
        Nationbtns[(int)eNationMode._eKoreaMode].transform.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        Nationbtns[(int)eNationMode._eChinseMode].transform.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        Nationbtns[(int)eNationMode._eJapanMode].transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        
        Nationbtns[(int)eNationMode._eKoreaMode].GetComponent<ScaleAnim>().bAnim = false;
        Nationbtns[(int)eNationMode._eChinseMode].GetComponent<ScaleAnim>().bAnim = false;
        Nationbtns[(int)eNationMode._eJapanMode].GetComponent<ScaleAnim>().bAnim = true;

        if (aRSessionOrigin != null)
        {

            aRSessionOrigin.SetActive(false);
            ARCoreFaceRegonManager.instance.InitJapanMode();
            StartCoroutine("MyTimeCall");
        }
    }

    IEnumerator MyTimeCall()
    {
        if (aRSessionOrigin != null)
        {
            yield return new WaitForSeconds(0.005f);

            aRSessionOrigin.SetActive(true);
         
        }
        StopCoroutine("MyTimeCall");
        yield return null;        
    }
    

}
