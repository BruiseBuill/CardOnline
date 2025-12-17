using BF;
using CardOnline.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline
{
	public class PhysicsRaycast : Single<PhysicsRaycast>
	{
        [SerializeField] LayerMask cardLayer;
        Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }
        public MagicCard RaycastCard(Vector3 screenPos)
        {
            Ray ray = new Ray(camera.ScreenToWorldPoint(screenPos), Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, 1 << cardLayer))
            {
                Collider collider = hitInfo.collider;
                MagicCard card = collider.GetComponentInParent<MagicCard>();
                return card;
            }
            else
            {
                return null;
            }
        }
    }
}