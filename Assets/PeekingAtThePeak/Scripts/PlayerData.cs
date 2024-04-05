using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPack;
using System;
[MemoryPackable]
public partial class PlayerData
{
    public bool SkipInitAnimation { get; set; } = false;
    public bool Poseing { get; set; } = false;
    public bool InitializeGame { get; set; } = false;
    public bool ContinueGame { get; set; } = false;

    public int ClearCount { get; set; }
    public TimeSpan BestTime { get; set; }
    public TimeSpan CurrentTime { get; set; }
    public Vector3 CurrentPlayerPositon { get; set; }
    public Vector3 InitalPlayerPositon { get; set; } = new Vector3(1.47f, 0.58f, 0);
    public bool IsTimeStop { get; set; } = false;


    public int TargetFrame { get; set; } = 120;
    public int CurrentFrameIndex { get; set; } = 3;
    public int CurentLangIndex { get; set; } = 0;
    public int ResolutionIndex { get; set; } = 4;

    public float Senstivity { get; set; } = 0.5f;
    public float SeVolume { get; set; } = 1f;
    public float BgmVolume { get; set; } = 0.8f;
    public float MasterVolume { get; set; } = 1f;
    public bool IsFullscrean { get; set; } = true;
    public bool EnableShowTime { get; set; } = false;

}
