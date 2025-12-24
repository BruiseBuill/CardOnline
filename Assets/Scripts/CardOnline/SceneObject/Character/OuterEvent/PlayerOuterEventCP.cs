using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
    public class PlayerOuterEventCP : OuterEventCP
    {

        protected override void StartAction2()
        {
            CursorManager.Instance().ChangeAct((int)IM_Index.FightActive);
        }
        protected override void EndAction2()
        {
            CursorManager.Instance().ChangeAct((int)IM_Index.FightSilence);
        }
    }
}