using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;
using DG.Tweening;

public class ParentPlayer : MonoBehaviour
{
    public static ParentPlayer instance;
    public List<GameObject> keycapsGO;
    public bool final;


    private void Awake()
    {
        instance = this;
        GameStateManager.Instance.GameStateMainMenu.OnExecute += StartGame;
        GameStateManager.Instance.GameStatePlaying.OnExecute += Movement;
        GameStateManager.Instance.GameStatePlaying.OnExecute += FailCondition;
        foreach (var dummy in transform.GetComponentsInChildren<CapsuleCollider>())
        {
            keycapsGO.Add(dummy.gameObject);
        }
        
    }


    void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AkaliLevelManager.Instance.LevelIsPlaying();
        }
    }
    
    public void RemoveDummy(GameObject dummy)
    {
        Taptic.Heavy();
        keycapsGO.Remove(dummy);
        dummy.GetComponent<Keycaps>().isRGB = false;
        dummy.GetComponent<Keycaps>().DisableParentFollow();
        var brokenKeycap = AkaliPoolManager.Instance.Dequeue<MeshCollider>();
        brokenKeycap.transform.position = dummy.transform.position;
        brokenKeycap.transform.SetParent(PlatformZMove.instance.transform);
        Destroy(dummy);
        Destroy(brokenKeycap,2);
    }
    
    public void DummyAdd(GameObject dummy)
    {
        Taptic.Heavy();
        keycapsGO.Add(dummy);
    }

    void FailCondition()
    {
        if (keycapsGO.Count <= 0 && !final)
        {
            AkaliLevelManager.Instance.LevelIsFail();    
        }
    }

    #region Movement

    [SerializeField] private float speed;
    [SerializeField] private float xClamp;
    public bool pressed;
    private float transformX, sensitivity = 5;
    private Vector3 firstPos, secondPos;
    
    void Movement()
    {
        if (Input.GetMouseButtonDown(0)) 
        { 
            pressed = true; 
            firstPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && pressed) 
        { 
            secondPos = Camera.main.ScreenToViewportPoint(Input.mousePosition); 
            transformX += (secondPos.x - firstPos.x) * sensitivity; 
            transformX = Mathf.Clamp(transformX, -xClamp, xClamp); 
            transform.position = new Vector3(transformX, transform.position.y, transform.position.z); 
            //Vector3 movement = new Vector3((secondPos - firstPos).x * sensitivity * turnAmount, 0, speed * Time.deltaTime);
            ////transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * turnSpeed);
            firstPos = secondPos;
        }
        else if (Input.GetMouseButtonUp(0)) 
        { 
            pressed = false; 
            transform.DORotate(new Vector3(0,0,0), .2f);
        }
    }
    #endregion
    
    IEnumerator FinalMove()
    {
        GetComponent<BoxCollider>().enabled = false;
        CameraController.Instance.isFinal = true;
        PlatformZMove.instance.speed = 0;
        if (keycapsGO.Count <= 32)
        {
            
            for (int j = 0; j < keycapsGO.Count; j++)
            {
                yield return new WaitForSeconds(0.1f);
                final = true;
                keycapsGO[j].transform.SetParent(Keyboard.instance.keyCapHolders[j].transform);
                keycapsGO[j].GetComponent<Keycaps>().finalMove = true;
                keycapsGO[j].GetComponent<Keycaps>().isCollect = false;
                keycapsGO[j].GetComponent<Keycaps>().id = (byte) j;
                keycapsGO[j].GetComponent<Keycaps>().textID.text = keycapsGO[j].GetComponent<Keycaps>().keycapID[j].keycapText;
                if (transform.childCount == 0)
                {
                    print("Finish");
                    StartCoroutine(LevelCompleted(true));
                }
            }
            
        }
        else
        {
            for (int i = 0; i < 33; i++)
            { 
                print(i);
                yield return new WaitForSeconds(0.1f);
                final = true;
                keycapsGO[i].transform.SetParent(Keyboard.instance.keyCapHolders[i].transform);
                keycapsGO[i].GetComponent<Keycaps>().finalMove = true;
                keycapsGO[i].GetComponent<Keycaps>().isCollect = false;
                keycapsGO[i].GetComponent<Keycaps>().id = (byte) i;
                keycapsGO[i].GetComponent<Keycaps>().textID.text = keycapsGO[i].GetComponent<Keycaps>().keycapID[i].keycapText;
                if (i == 32)
                {
                    print("Finish");
                    StartCoroutine(LevelCompleted(false));
                }
            }
            
        }
        
    }

    IEnumerator LevelCompleted(bool x)
    {
        if (x)
        {
            yield return new WaitForSeconds(2);
            AkaliLevelManager.Instance.LevelIsCompleted();    
        }
        else
        {
            for (int i = 33; i < keycapsGO.Count; i++)
            {
                yield return new WaitForSeconds(0.02f);
                keycapsGO[i].GetComponent<Keycaps>().DisableParentFollow();
                Destroy(keycapsGO[i]);
                var brokenKeycap = AkaliPoolManager.Instance.Dequeue<MeshCollider>();
                brokenKeycap.transform.position = keycapsGO[i].transform.position;
                brokenKeycap.transform.SetParent(PlatformZMove.instance.transform);
                Destroy(brokenKeycap,1.5f);
            }
            yield return new WaitForSeconds(2.2f);
            AkaliLevelManager.Instance.LevelIsCompleted();
        }
        
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GameStateManager.Instance.GameStatePlaying.OnExecute -= Movement;
            transform.DOMoveZ(transform.position.z + 7,0.5f);
            if (keycapsGO.Count <= 16)
            {
                GetComponent<BoxCollider>().enabled = false;
                CameraController.Instance.isFinal = true;
                PlatformZMove.instance.speed = 0;
                for (int i = 0; i < keycapsGO.Count; i++)
                {
                    keycapsGO[i].GetComponent<Keycaps>().DisableParentFollow();
                    keycapsGO[i].GetComponent<Keycaps>().isRGB = false;
                    Destroy(keycapsGO[i]);
                    var brokenKeycap = AkaliPoolManager.Instance.Dequeue<MeshCollider>();
                    brokenKeycap.transform.position = keycapsGO[i].transform.position;
                    brokenKeycap.transform.SetParent(PlatformZMove.instance.transform);
                    Destroy(brokenKeycap,1.5f);
                }
                AkaliLevelManager.Instance.LevelIsFail();
            }
            else
            {
                StartCoroutine(FinalMove());
            }
        }
    }
}
