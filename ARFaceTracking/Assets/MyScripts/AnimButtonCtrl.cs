using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimButtonCtrl : MonoBehaviour
{
    public Button learnBtn;
    public Button ARBtn;
    public static bool bAnimButton = false;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (bAnimButton == false)
        {
            learnBtn.GetComponent<ScaleAnim>().bAnim = true;
            ARBtn.GetComponent<ScaleAnim>().bAnim = false;
            bAnimButton = true;
        }
        else
        {
            learnBtn.GetComponent<ScaleAnim>().bAnim = false;
            ARBtn.GetComponent<ScaleAnim>().bAnim = true;
            bAnimButton = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
