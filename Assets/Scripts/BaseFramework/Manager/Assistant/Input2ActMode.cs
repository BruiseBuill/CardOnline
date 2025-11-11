using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
	public abstract class Input2ActMode : ScriptableObject
	{
		public abstract void SetActMode();
		public abstract void UnSetActMode();
		public abstract void Update();
    }
}