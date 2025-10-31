using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Control
{
    public class EnemyAction : GameAction
    {
        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("EnemyAction Over");
            onExecuteOver.Invoke();
        }
    }
}