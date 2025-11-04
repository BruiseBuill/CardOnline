using BF;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Control
{
	public abstract class GameAction
    {
        public Action onExecuteOver = delegate { };
        public abstract IEnumerator Execute();
    }
    public class ActionSystem : Single<ActionSystem>
	{
		public void Execute(GameAction gameAction)
        {
            StartCoroutine(gameAction.Execute());
        }
	}
}