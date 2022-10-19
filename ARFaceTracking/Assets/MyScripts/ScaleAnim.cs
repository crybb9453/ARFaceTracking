using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScaleAnim : MonoBehaviour
{
    

    Vector3 originScale;
    Vector3 originRotZ;
    Vector3 originPos;
    public enum eAnimMode
    {
        eNoneMode = 0,
        eScaleMode =1,
        eZRotMode=2,
        ePosMode=4,
        eScaleRotMode = 3,
        eRottPos = 5,
        eScalePosMode =6,
         eAllMode = 7,
        
    }

    public eAnimMode eAnim;
    RawImage bgImage;
    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        originRotZ = transform.eulerAngles;
        originPos = transform.localPosition;

        //    eAnim = eAnimMode.eScaleMode;
        bgImage =  GetComponent<RawImage>();
    }

    public bool bAnim = false;

    float currentTime = 0.0f;
    public float speed = 2.0f;
    public float addScale = 0.02f;
    public float addRot = 0.02f;
    public float addPos = 0.02f;
    // Update is called once per frame
    void Update()
    {
        if (bAnim == true)
        {
            currentTime += Time.deltaTime;


            if ((eAnim & eAnimMode.eScaleMode) != eAnimMode.eNoneMode)
            {
                float s = addScale * Mathf.Abs(Mathf.Sin(currentTime * speed));
                transform.localScale = originScale + new Vector3(s, s, s);
            }
             if ((eAnim & eAnimMode.eZRotMode) != eAnimMode.eNoneMode)
            {
                float s = addRot * Mathf.Sin(currentTime * speed);
                transform.eulerAngles = originRotZ + new Vector3(0, 0, s);
            }
            if ((eAnim & eAnimMode.ePosMode) != eAnimMode.eNoneMode)
            {
                float s = addPos * Mathf.Sin(currentTime * speed);
                transform.localPosition = originPos + new Vector3(s, 0, 0);
            }
        }
    }

}
