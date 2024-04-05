using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public int FrameRate = 120;
    void Start()
    {
        Application.targetFrameRate = FrameRate;
    }

}
