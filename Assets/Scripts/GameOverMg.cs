using UnityEngine;
using System;

public class GameOverMg : MonoBehaviour
{
    public Vector2 center = new Vector2(7f,0f);  // �~�̒��S�i2D���W�j
    public float radius = 30f;              // �~�̔��a

    public event Action OnOutOfBounds;     // �͈͊O�ɂȂ����Ƃ��̃C�x���g

    private Vector2 checkPosition;

    void Update()
    {
        float distance = Vector2.Distance(checkPosition, center);

        if (distance > radius)
        {
            if (OnOutOfBounds != null)
            {
                OnOutOfBounds.Invoke();
            }
        }
    }
}
