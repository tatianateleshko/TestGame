using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Player _player;
    [SerializeField] private Finish _finish;

    void Update()
    {
        if (_player.IsDied || _finish.IsFinished)
        {
            return;
        }
        else
        {
            Move();
        }

    }

    private void Move()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
}
