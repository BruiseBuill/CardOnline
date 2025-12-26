using CardOnline.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
    public class AIInput : CharacterInput
    {
        [SerializeField] CharacterData characterData;

        protected override void Awake()
        {
            base.Awake();
            characterData = data as CharacterData;
        }
        public override void Open()
        {
            characterData.onBeHit += OnBeHit;
        }
        public override void Close()
        {
            characterData.onBeHit -= OnBeHit;
        }     
        void OnBeHit(BaseBullet bullet)
        {

        }
    }
}