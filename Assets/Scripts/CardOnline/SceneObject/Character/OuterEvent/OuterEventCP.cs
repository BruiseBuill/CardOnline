using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;

namespace CardOnline.Character
{
    public abstract class OuterEventCP : BaseComponent
    {
        CharacterData characterData;

        [SerializeField]protected GenericEventChannel<CharacterControl> ch_ActionStart;
        [SerializeField]protected GenericEventChannel<CharacterControl> ch_ActionEnd;

        protected override void Awake()
        {
            base.Awake();
            characterData = data as CharacterData;
        }
        void StartAction(CharacterControl character)
        {
            if (character != characterData.characterControl)
            {
                return;
            }

        }
        protected abstract void StartAction2();

        void EndAction()
        {

        }

        public override void Close()
        {
            throw new System.NotImplementedException();
        }

        public override void Open()
        {
            throw new System.NotImplementedException();
        }
    }
}