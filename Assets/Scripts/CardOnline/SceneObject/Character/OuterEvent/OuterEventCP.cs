using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;

namespace CardOnline.Character
{
    public abstract class OuterEventCP : BaseComponent
    {
        CharacterData characterData;

        [SerializeField] protected GenericEventChannel<object> ch_ActionStart;
        [SerializeField] protected GenericEventChannel<object> ch_ActionEnd;

        protected override void Awake()
        {
            base.Awake();
            characterData = data as CharacterData;
        }
        public override void Open()
        {
            ch_ActionStart.AddListener(StartAction);
            ch_ActionEnd.AddListener(EndAction);
        }
        public override void Close()
        {
            ch_ActionStart.RemoveListener(StartAction);
            ch_ActionEnd.RemoveListener(EndAction);
        }
                
        void StartAction(object c)
        {
            CharacterControl character = c as CharacterControl;
            if (character != characterData.characterControl)
            {
                return;
            }
            StartAction2();
        }
        void EndAction(object c) 
        { 
            CharacterControl character = c as CharacterControl;
            if (character != characterData.characterControl)
            {
                return;
            }
            EndAction2();
        }
        protected abstract void StartAction2();
        protected abstract void EndAction2();
        protected abstract void BeHit();
        

        
    }
}