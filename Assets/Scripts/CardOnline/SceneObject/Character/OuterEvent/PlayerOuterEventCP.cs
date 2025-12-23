using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
    public class PlayerOuterEventCP : OuterEventCP
    {
        [SerializeField] EventChannel ch_PlayerActionStart;

        protected override void StartAction2()
        {
            ch_PlayerActionStart.Invoke();

            CursorManager.Instance().ChangeAct((int)IM_Index.FightActive);
        }
        protected override void EndAction2()
        {
            CursorManager.Instance().ChangeAct((int)IM_Index.FightSilence);
        }
    }
}