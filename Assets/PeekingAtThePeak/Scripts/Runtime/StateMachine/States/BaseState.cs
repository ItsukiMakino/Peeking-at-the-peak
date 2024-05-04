using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace MyGame
{
    public  class BaseState
    {
        public PlayerBehaiviour PB { get; protected set; }
        public BaseState(PlayerBehaiviour pb)
        {
            PB = pb;
        }
    }
   
}