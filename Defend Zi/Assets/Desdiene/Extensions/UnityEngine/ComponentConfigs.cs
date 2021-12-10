using System;
using UnityEngine;

namespace Desdiene.Extensions.UnityEngine
{
    public static class ComponentConfigs
    {
        public static T As2dTrigger<T>(this T collider) where T : Collider2D
        {
            bool isTrigger = collider.isTrigger;

            if (!isTrigger)
            {
                Debug.LogWarning($"Collider2D on {collider.name} must be trigger! The configs will be changed to the expected one.");
                collider.isTrigger = true;
            }

            return collider;
        }

        public static T As2dCollision<T>(this T collider) where T : Collider2D
        {
            bool isCollision = !collider.isTrigger;

            if (!isCollision)
            {
                Debug.LogWarning($"Collider2D on {collider.name} must be collision! The configs will be changed to the expected one.");
                collider.isTrigger = false;
            }

            return collider;
        }

        public static Rigidbody2D As2dKinematic(this Rigidbody2D rb2d)
        {
            return SetRb2dBodyType(rb2d, RigidbodyType2D.Kinematic);
        }

        public static Rigidbody2D As2dDynamic(this Rigidbody2D rb2d)
        {
            return SetRb2dBodyType(rb2d, RigidbodyType2D.Dynamic);
        }

        public static Rigidbody2D As2dStatic(this Rigidbody2D rb2d)
        {
            return SetRb2dBodyType(rb2d, RigidbodyType2D.Static);
        }

        private static Rigidbody2D SetRb2dBodyType(Rigidbody2D rb2d, RigidbodyType2D expectedBodyType)
        {
            if (rb2d.bodyType != expectedBodyType)
            {
                Debug.LogWarning($"Rigidbody2D on {rb2d.name} must be {expectedBodyType}! The configs will be changed to the expected one.");
                rb2d.bodyType = expectedBodyType;
            }

            return rb2d;
        }
    }
}
