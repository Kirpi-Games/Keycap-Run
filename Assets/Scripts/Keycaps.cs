using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Keycaps : MonoBehaviour
{
    public static Keycaps instance;
    public bool isCollect,finalMove;
    public GameObject parentPlayer;
    private Rigidbody _rigidbody;
    [SerializeField] private float swerveClamp;
    [SerializeField] private float forceToPull;
    public bool isRGB;
    private float colorValue;
    private float colorValue2;
    public TextMesh textID;
    public List<KeycapID> keycapID;
    public byte id;
    public bool paintable;

    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        parentPlayer = ParentPlayer.instance.gameObject;
        GameStateManager.Instance.GameStatePlaying.OnExecute += ParentFollow;
        GameStateManager.Instance.GameStatePlaying.OnExecute += FinalMove;
        GameStateManager.Instance.GameStatePlaying.OnExecute += RGB;
        id = (byte) Random.Range(0, 33);
        textID.text = keycapID[id].keycapText;
    }

    private void ParentFollow()
    {
        if (isCollect)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            if (!_rigidbody.isKinematic)
            {
                Vector3 playerPos = parentPlayer.transform.position - transform.position;
                playerPos.y = 0;
                playerPos.x = Mathf.Clamp(playerPos.x, -swerveClamp, swerveClamp);
                _rigidbody.velocity = playerPos.normalized * forceToPull;
                 
                
            }    
        }
    }

    public void LevelFailed()
    {
        GameStateManager.Instance.GameStateFail.OnExecute += ParentFollow;
        GameStateManager.Instance.GameStateFail.OnExecute += FinalMove;
        GameStateManager.Instance.GameStateFail.OnExecute += RGB;
    }
    
    public void DisableParentFollow()
    { 
        GameStateManager.Instance.GameStatePlaying.OnExecute -= ParentFollow;
        GameStateManager.Instance.GameStatePlaying.OnExecute -= FinalMove;
        GameStateManager.Instance.GameStatePlaying.OnExecute -= RGB;
    }

    void FinalMove()
    {
        if (finalMove)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,Vector3.zero, 1 * Time.deltaTime);
            if (transform.localPosition == Vector3.zero)
            {
                GameStateManager.Instance.GameStatePlaying.OnExecute += SetPosZero;
                GameStateManager.Instance.GameStateComplete.OnExecute += SetPosZero;
                GameStateManager.Instance.GameStateComplete.OnExecute += RGB;
                finalMove = false;
            }
        }
    }

    
    
    void RGB()
    {
        if (isRGB)
        {
            //GetComponent<MeshRenderer>().materials[1].SetFloat("_Rgb",1);
            if (colorValue == 0)
            {
                DOTween.To(()=> colorValue, x=> colorValue = x, 1, 2);
                DOTween.To(()=> colorValue2, x=> colorValue2 = x, 0, 1);
            }
            if (colorValue == 1)
            {
                DOTween.To(()=> colorValue2, x=> colorValue2 = x, 1, 2);
                DOTween.To(()=> colorValue, x=> colorValue = x, 0, 1);
            }

            GetComponent<MeshRenderer>().materials[1].SetFloat("_Value1",colorValue);
            GetComponent<MeshRenderer>().materials[1].SetFloat("_Value2",colorValue2);
        }
    }

    void SetPosZero()
    {
        transform.localPosition = Vector3.zero;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (isCollect)
            {
                other.GetComponent<CapsuleCollider>().isTrigger = false;
                other.gameObject.layer = 7;
                other.GetComponent<Keycaps>().isCollect = true;
                other.transform.SetParent(transform.parent);
                ParentPlayer.instance.DummyAdd(other.gameObject);
                other.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (other.gameObject.layer == 10)
        {
            if (paintable)
            {
                Color door = new Color(other.GetComponent<ColorDoors>().doorColor.r, other.GetComponent<ColorDoors>().doorColor.g, other.GetComponent<ColorDoors>().doorColor.b, 255);
                GetComponent<MeshRenderer>().materials[0].DOColor(door, 0.2f);    
                GetComponent<MeshRenderer>().materials[0].SetColor("_Emission", other.GetComponent<ColorDoors>().emColor);
            }
        }
        if (other.gameObject.layer == 11)
        {
            isRGB = true;
            GetComponent<MeshRenderer>().materials[1].SetFloat("_Rgb",1);
        }
    }
}
