 using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Card;
using Sirenix.OdinInspector;
using CardOnline.View;
using CardOnline.Character;

namespace CardOnline.Player
{
    public abstract class PlayerInputMode
    {
        public PlayerInputMode(PlayerInput playerInput)
        {
            input = playerInput;
        }
        protected PlayerInput input;

        public abstract void EnterInputMode();
        public abstract void ExitInputMode();

        public abstract void OnPointerDown(Vector3 screenPos);
        public abstract void OnPointerUp(Vector3 screenPos);
        public abstract void OnPointerDrag(Vector3 start,Vector3 end);
        public abstract void OnUpdate();
        
    }

	public class PlayerInput : MonoBehaviour
	{
        List<PlayerInputMode> modeList = new List<PlayerInputMode>();
        PlayerInputMode presentMode;

        [ReadOnly]
        [SerializeField] int modeIndex = -1;



        private void Start()
        {
            
        }
        public void ChangeMode()
        {

        }
    }
}