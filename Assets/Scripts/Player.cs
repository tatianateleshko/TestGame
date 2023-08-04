using System.Collections;
using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay;
    [SerializeField] private ParticleSystem _shootEffectPrefab;

    [SpineAnimation] public string ShootAnim;
    [SpineAnimation] public string DieAnim;
    [SpineAnimation] public string WalkAnim;
    private SkeletonAnimation _skeletonAnimation;

    public bool IsDied;
    private bool _canShoot;

    private void Awake()
    {
        _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }
    private void Start()
    {
        IsDied = false;
        _canShoot = true;
        Invoke("WalkAnimimation", 0.5f);
    }

    private void Update()
    {
        if (IsDied)
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
    public IEnumerator Shoot()
    {
        if (_canShoot)
        {
            _skeletonAnimation.state.SetAnimation(3, ShootAnim, false);
            Instantiate(_bulletPrefab, _shootPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_shootDelay);
        }
        else
        {
            _skeletonAnimation.state.SetAnimation(3, WalkAnim, false);
        }
        
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _canShoot = false;
            Lose(); 
        }
    }

    private void Lose()
    { 
        _skeletonAnimation.AnimationState.ClearTrack(5);
        _skeletonAnimation.AnimationState.ClearTrack(3);
        _skeletonAnimation.state.SetAnimation(1, DieAnim, false);
        IsDied = true;
        Invoke("Pause", 3);
    }

    private void Pause()
    {
        Debug.Log("You Lose!");
        Time.timeScale = 0;
    }

    private void WalkAnimimation()
    {
        _skeletonAnimation.state.SetAnimation(5, WalkAnim, true);
    }
}
