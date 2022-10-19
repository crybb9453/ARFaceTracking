using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//영역관련 임포트 
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.Collections;


public class ARCoreFaceRegonManager : MonoBehaviour
{

    public static ARCoreFaceRegonManager instance = null;

    public Material defaultMat;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

 

    // 안드로이드의 경우 3개까지. 
 //    public GameObject[] faceObjects = new GameObject[3];
    
    ARFaceManager arFM;

    //AR상에서 위치를 알아도 AR상에서 제대로 매핑을 하기 위해 필요
    [HideInInspector]
    ARSessionOrigin arSO;



    ARCoreFaceSubsystem subsystem;
    // Start is called before the first frame update
    void Start()
    {
        arFM = GetComponent<ARFaceManager>();
        arSO = GetComponent<ARSessionOrigin>();
        //ARFaceManager안에 있는 arcore의 만의 기능을 직접 얻기 위함
        subsystem = (ARCoreFaceSubsystem)arFM.subsystem;

        //이벤트 연결
        arFM.facesChanged += OnDetectFaceRegion;
        
        koreaFaceObjects = new GameObject[koreaFaceObjectsPrefabs.Length];
        japanFaceObjects = new GameObject[japanFaceObjectsPrefabs.Length];
        InitKoreaMode();
        InitChinseMode();
        InitJapanMode();

    }


    void OnDetectFaceRegion(ARFacesChangedEventArgs args)
    {

        switch (ButtonSeletManager.instance.eSelMode)
        {
            case ButtonSeletManager.eNationMode._eKoreaMode:
                DetectFaceReginKoreaMode(ref args);
                break;

            case ButtonSeletManager.eNationMode._eChinseMode:
                  DetectFaceReginChinseMode(ref args);
                break;

            case ButtonSeletManager.eNationMode._eJapanMode:
                DetectFaceReginJapanMode(ref args);
                break;
        }
                
    }



    public GameObject[] koreaFaceObjectsPrefabs;
    GameObject[]  koreaFaceObjects;
    int koreaTexCnt = 0;
    NativeArray<ARCoreFaceRegionData> faceRegions;
    void SetKoreaFaceObjectActive(bool b)
    {
        for (int i = 0; i < koreaFaceObjectsPrefabs.Length; i++)
        {
            if( koreaFaceObjects[i] == null)
            {
                koreaFaceObjects[i] = Instantiate(koreaFaceObjectsPrefabs[i]);
                koreaFaceObjects[i].transform.position = new Vector3(0.0f, 0.0f, 0.15f);
            }

            koreaFaceObjects[i].SetActive(b);
        }
    }


    public void InitKoreaMode()
    {
        SetKoreaFaceObjectActive(false);
        SetJapanFaceObjectActive(false);
        arFM.facePrefab.GetComponent<MeshRenderer>().material = defaultMat;
    }

    void DetectFaceReginKoreaMode(ref ARFacesChangedEventArgs args)
    {
        // 얼굴 인식 정보가 갱신된 것이 있다면...
        if (args.updated.Count > 0)
        {
            //위치를 받아오는 함수 
            // 인식된 얼굴로부터 세 군데 좌표를 가져온다.
            subsystem.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref faceRegions);

                //코
             ARCoreFaceRegion regionType = faceRegions[0].region;

             SetKoreaFaceRegionObject(koreaFaceObjects[koreaTexCnt], arSO.trackablesParent, faceRegions[0].pose);
        }
        // 얼굴 인식 정보를 잃었다면...
        else if (args.removed.Count > 0)
        {
            koreaTexCnt++;

            if (koreaTexCnt >= koreaFaceObjectsPrefabs.Length) koreaTexCnt = 0;


            SetKoreaFaceObjectActive(false);
        }
    }

    void SetKoreaFaceRegionObject(GameObject g, Transform pTr, Pose p)
    {
        if (g.activeSelf == false)
        {
            // 얼굴전체의 원점을 받아 온다.
            g.SetActive(true);
        }
        g.transform.localPosition = p.position;
        g.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        g.transform.localRotation = p.rotation;
    }


    public Material[] chinseMats;
    int chinseMatCnt = 0;

    public void InitChinseMode()
    {
        SetKoreaFaceObjectActive(false);
        SetJapanFaceObjectActive(false);
        chinseMatCnt = 0;
         arFM.facePrefab.GetComponent<MeshRenderer>().material = chinseMats[chinseMatCnt];
        
    }
    void DetectFaceReginChinseMode(ref ARFacesChangedEventArgs args)
    {
        if (args.removed.Count > 0)
        {
            chinseMatCnt++;

            if (chinseMatCnt >= chinseMats.Length) chinseMatCnt = 0;

            arFM.facePrefab.GetComponent<MeshRenderer>().material = chinseMats[chinseMatCnt];
        }
    }



    public GameObject[] japanFaceObjectsPrefabs;
    GameObject[]   japanFaceObjects;
    int japanTexCnt = 0;


    void SetJapanFaceObjectActive(bool b)
    {
        for (int i = 0; i < japanFaceObjectsPrefabs.Length; i++)
        {
            if (japanFaceObjects[i] == null)
            {
                japanFaceObjects[i] = Instantiate(japanFaceObjectsPrefabs[i]);
                japanFaceObjects[i].transform.position = new Vector3(0.0f, 0.0f, 0.15f);
            }

            japanFaceObjects[i].SetActive(b);
        }
    }


    public void InitJapanMode()
    {
        SetKoreaFaceObjectActive(false);
        SetJapanFaceObjectActive(false);
        arFM.facePrefab.GetComponent<MeshRenderer>().material = defaultMat;
        
    }

    void DetectFaceReginJapanMode(ref ARFacesChangedEventArgs args)
    {
        // 얼굴 인식 정보가 갱신된 것이 있다면...
        if (args.updated.Count > 0)
        {
            //위치를 받아오는 함수 
            // 인식된 얼굴로부터 세 군데 좌표를 가져온다.
            subsystem.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref faceRegions);

            //코
            ARCoreFaceRegion regionType = faceRegions[0].region;

            SetJapanFaceRegionObject(japanFaceObjects[japanTexCnt], arSO.trackablesParent, faceRegions[0].pose);
        }
        // 얼굴 인식 정보를 잃었다면...
        else if (args.removed.Count > 0)
        {
            japanTexCnt++;

            if (japanTexCnt >= japanFaceObjectsPrefabs.Length) japanTexCnt = 0;


            SetJapanFaceObjectActive(false);
        }
    }

    void SetJapanFaceRegionObject(GameObject g, Transform pTr, Pose p)
    {
        if (g.activeSelf == false)
        {
            // 얼굴전체의 원점을 받아 온다.
            g.SetActive(true);
        }
        g.transform.localPosition = p.position;
        g.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        g.transform.localRotation = p.rotation;
    }
}
