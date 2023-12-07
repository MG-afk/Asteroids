using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsGame
{
    public static class ProjectExtensions
    {
        public static Vector2 GetEdgePosition(this Camera camera)
        {
            var orthographicSize = camera.orthographicSize;
            var screenHalfWidth = orthographicSize * Screen.width / Screen.height;
            var screenHalfHeight = orthographicSize;

            var edge = Random.Range(0, 4);

            return edge switch
            {
                0 => new Vector3(Random.Range(-screenHalfWidth, screenHalfWidth), screenHalfHeight),
                1 => new Vector3(Random.Range(-screenHalfWidth, screenHalfWidth), -screenHalfHeight),
                2 => new Vector3(-screenHalfWidth, Random.Range(-screenHalfHeight, screenHalfHeight)),
                3 => new Vector3(screenHalfWidth, Random.Range(-screenHalfHeight, screenHalfHeight)),
                _ => throw new System.Exception("The game went in next dimensions it should have only 4 edges!"),
            };
        }

        public static void KeepObjectOnCamera(this Transform transform, SpriteRenderer spriteRenderer)
        {
            var camera = Camera.main;
            var bottomLeft = camera.ViewportToWorldPoint(new Vector3(0f, 0f));
            var topRight = camera.ViewportToWorldPoint(new Vector3(1f, 1f));

            var halfSpriteWidth = spriteRenderer.bounds.size.x * 0.5f;
            var halfSpriteHeight = spriteRenderer.bounds.size.y * 0.5f;

            var rightBound = topRight.x + halfSpriteWidth;
            var leftBound = bottomLeft.x - halfSpriteWidth;

            var upperBound = topRight.y + halfSpriteHeight;
            var bottomBound = bottomLeft.y - halfSpriteHeight;

            var position = transform.position;

            if (position.x > rightBound)
            {
                transform.position = new(leftBound, position.y);
            }
            else if (position.x < leftBound)
            {
                transform.position = new(rightBound, position.y);
            }
            else if (position.y > upperBound)
            {
                transform.position = new(position.x, bottomBound);
            }
            else if (position.y < bottomBound)
            {
                transform.position = new(position.x, upperBound);
            }
        }

        public static IEnumerator StartBlinking(this SpriteRenderer spriteRenderer)
        {
            var tick = 0;
            var color = spriteRenderer.color;

            while (tick < 6)
            {
                color.a = 0;
                spriteRenderer.color = color;

                yield return new WaitForSeconds(.05f);

                color.a = 1;
                spriteRenderer.color = color;

                yield return new WaitForSeconds(.25f);

                tick++;
            }
        }
    }
}
