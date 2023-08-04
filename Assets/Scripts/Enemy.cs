using UnityEngine;
using Spine.Unity;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private ParticleSystem _explosionEnemyPrefab;

    private Player _player;

    [SpineAnimation] public string WinAnim;
    [SpineAnimation] public string AngryAnim;
    [SpineAnimation] public string RunAnim;
    private SkeletonAnimation _skeletonAnimation;

    private void Awake()
    {
        _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }
    private void Start()
    {
        RunningAnimation();
    }
    public void Init(Player player)
    {
        _player = player;
    }

    void Update()
    {
        if (_player.IsDied)
        {
            _skeletonAnimation.AnimationState.ClearTrack(3);
            return;
        }
        else
        {
            EnemyMove();
        }
    }

    private void OnMouseDown()
    {
        _player.StartCoroutine("Shoot");
    }

    private void EnemyMove()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            _skeletonAnimation.state.SetAnimation(0, AngryAnim, false);
            Invoke("EnemyDied", 0.2f);
            Destroy(collision.gameObject);

        }
        else if(collision.tag == "Player")
        {
            Win();  
        }
    }

    private void EnemyDied()
    {
        Vector3 offset = new Vector3(0, 3, 0);
        ParticleSystem explosionEnemy = Instantiate(_explosionEnemyPrefab, transform.position + offset, transform.rotation);
        Destroy(explosionEnemy, 2);
        Destroy(gameObject);
    }

    private void Win()
    {
        _skeletonAnimation.AnimationState.ClearTrack(3);
        _skeletonAnimation.state.SetAnimation(4, WinAnim, false);
    }


    private void RunningAnimation() 
    {
        _skeletonAnimation.state.SetAnimation(3, RunAnim, true);
    }
}

