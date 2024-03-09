using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> heads = new List<GameObject>();
    [SerializeField] private List<GameObject> eyes = new List<GameObject>();
    [SerializeField] private List<GameObject> bodies = new List<GameObject>();
    [SerializeField] private List<GameObject> guns = new List<GameObject>();

    private int headIndex = 0;
    private int eyeIndex = 0;
    private int bodyIndex = 0;
    private int gunIndex = 0;

    private void Start()
    {
        headIndex = 0;
        eyeIndex = 0;
        bodyIndex = 0;
        gunIndex = 0;
        LoadCharacter();
    }

    //-----------------------------------------
    public void NextHead()
    {
        heads.ForEach(head => head.SetActive(false));
        headIndex++;
        headIndex = headIndex > heads.Count-1 ? 0 : headIndex;
        heads[headIndex].SetActive(true);
    }
    
    public void PreviousHead()
    {
        heads.ForEach(head => head.SetActive(false));
        headIndex--;
        headIndex = headIndex < 0 ? heads.Count-1 : headIndex;
        heads[headIndex].SetActive(true);
    }

    //-----------------------------------------
    public void NextEye()
    {
        eyes.ForEach(eye => eye.SetActive(false));
        eyeIndex++;
        eyeIndex = eyeIndex > eyes.Count-1 ? 0 : eyeIndex;
        eyes[eyeIndex].SetActive(true);
    }

    public void PreviousEye()
    {
        eyes.ForEach(eye => eye.SetActive(false));
        eyeIndex--;
        eyeIndex = eyeIndex < 0 ? eyes.Count-1 : eyeIndex;
        eyes[eyeIndex].SetActive(true);
    }

    //-----------------------------------------
    public void NextBody()
    {
        bodies.ForEach(body => body.SetActive(false));
        bodyIndex++;
        bodyIndex = bodyIndex > bodies.Count-1 ? 0 : bodyIndex;
        bodies[bodyIndex].SetActive(true);
    }

    public void PreviousBody()
    {
        bodies.ForEach(body => body.SetActive(false));
        bodyIndex--;
        bodyIndex = bodyIndex < 0 ? bodies.Count-1 : bodyIndex;
        bodies[bodyIndex].SetActive(true);
    }

    //-----------------------------------------
    public void NextGun()
    {
        guns.ForEach(gun => gun.SetActive(false));
        gunIndex++;
        gunIndex = gunIndex > guns.Count-1 ? 0 : gunIndex;
        guns[gunIndex].SetActive(true);
    }

    public void PreviousGun()
    {
        guns.ForEach(gun => gun.SetActive(false));
        gunIndex--;
        gunIndex = gunIndex < 0 ? guns.Count-1 : gunIndex;
        guns[gunIndex].SetActive(true);
    }

    //-----------------------------------------
    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("Head",headIndex);
        PlayerPrefs.SetInt("Eye",eyeIndex);
        PlayerPrefs.SetInt("Body",bodyIndex);
        PlayerPrefs.SetInt("Gun",gunIndex);
    }

    private void LoadCharacter()
    {
        headIndex = PlayerPrefs.GetInt("Head");
        eyeIndex = PlayerPrefs.GetInt("Eye");
        bodyIndex = PlayerPrefs.GetInt("Body");
        gunIndex = PlayerPrefs.GetInt("Gun");

        heads.ForEach(head => head.SetActive(false));
        eyes.ForEach(eye => eye.SetActive(false));
        bodies.ForEach(body => body.SetActive(false));
        guns.ForEach(gun => gun.SetActive(false));

        heads[headIndex].SetActive(true);
        eyes[eyeIndex].SetActive(true);
        bodies[bodyIndex].SetActive(true);
        guns[gunIndex].SetActive(true);
    }

}
