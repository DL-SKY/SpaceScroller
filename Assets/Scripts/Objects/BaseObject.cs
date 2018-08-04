using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    #region Variables
    [Header("Main Parameters")]
    public EnumSpaceObject type = EnumSpaceObject.SpaceObject;
    public bool isImmortal = false;
    public float hitPoints = 1.0f;
    public float damage = 1.0f;

    [Header("Animation")]
    public bool isMoveSidewiseNow = false;
    public float speedSidewise = 10.0f;

    [Header("Move")]
    public bool dontMove = false;
    public float speed = 0.0f;

    [Header("Rotate")]
    public bool dontRotate = false;
    public float speedRotate = 0.0f;

    protected Rigidbody2D rigibody;
    protected new Collider2D collider;
    protected Animator animator;

    protected float spacetrack_count;
    protected float spacetrack_width;
    #endregion

    #region Unity methods
    virtual protected void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        spacetrack_count = ConstantsGameSettings.SPACETRACK_COUNT;
        spacetrack_width = ConstantsGameSettings.SPACETRACK_WIDTH;

        ApplyTag();
        //tag = ConstantsTag.TAG_SPACE_OBJECT;
    }

    virtual protected void OnTriggerEnter2D(Collider2D _other)
    {        
        //Получаем повреждения
        //var _otherObject = _other.GetComponent<BaseObject>();
        //GetSelfDamage(_otherObject.damage);

        string _tag = _other.tag;
        BaseObject _otherObject = _other.GetComponent<BaseObject>();

        //Игрок и его снаряды
        if (tag == ConstantsTag.TAG_PLAYER && (_tag == ConstantsTag.TAG_MISSILE_PLAYER || _tag == ConstantsTag.TAG_SHOT_PLAYER))
        {

        }
        else if (_tag == ConstantsTag.TAG_PLAYER && (tag == ConstantsTag.TAG_MISSILE_PLAYER || tag == ConstantsTag.TAG_SHOT_PLAYER))
        {

        }
        //Противники и их снаряды
        else if (tag == ConstantsTag.TAG_SPACESHIP && (_tag == ConstantsTag.TAG_MISSILE || _tag == ConstantsTag.TAG_SHOT))
        {

        }
        else if (_tag == ConstantsTag.TAG_SPACESHIP && (tag == ConstantsTag.TAG_MISSILE || tag == ConstantsTag.TAG_SHOT))
        {

        }

        //Иначе - получаем повреждения
        else
        {
            GetSelfDamage(_otherObject.damage);
        }
    }

    virtual protected void OnDestroy()
    {

    }
    #endregion

    #region Public methods
    virtual public void Initialize(object _data)
    {

    }

    public void MoveToLeft(bool _withAnimator = false)
    {
        //Проверка уже начатого перемещения
        if (isMoveSidewiseNow)
            return;

        //Проверка выхода за границы Игрового поля
        if (transform.position.x >= -spacetrack_width * (spacetrack_count - 2) / 2)
        {
            //Включаем анимацию и запускаем корутину
            if (_withAnimator)
                animator.SetBool("ToLeft", true);
            StartCoroutine(SmoothMovement(transform.position - transform.right * spacetrack_width, _withAnimator));
        }
    }

    public void MoveToRight(bool _withAnimator = false)
    {
        //Проверка уже начатого перемещения
        if (isMoveSidewiseNow)
            return;

        //Проверка выхода за границы Игрового поля
        if (transform.position.x <= spacetrack_width * (spacetrack_count - 2) / 2)
        {
            //Включаем анимацию и запускаем корутину
            if (_withAnimator)
                animator.SetBool("ToRight", true);
            StartCoroutine(SmoothMovement(transform.position + transform.right * spacetrack_width, _withAnimator));
        }
    }

    public void MoveToFixedUpdate()
    {
        if (dontMove)
            return;

        Vector3 newPostion = rigibody.position - new Vector2(0.0f, speed * Time.fixedDeltaTime);
        rigibody.MovePosition(newPostion);
    }

    public void RotateToFixedUpdate()
    {
        if (dontRotate)
            return;

        transform.Rotate(Vector3.forward, speedRotate * Time.fixedDeltaTime);
    }

    virtual public void GetSelfDamage(float _damage)
    {
        if (isImmortal)
            return;

        hitPoints -= _damage;

        if (hitPoints <= 0)
            SelfDestroy();
    }
    #endregion

    #region Protected methods
    virtual protected void SelfDestroy()
    {
        Destroy(gameObject);
    }

    protected void ApplyTag()
    {
        string newTag = "";

        switch (type)
        {
            case EnumSpaceObject.SpaceObject:
                newTag = ConstantsTag.TAG_SPACE_OBJECT;
                break;
            case EnumSpaceObject.Spaceship:
                newTag = ConstantsTag.TAG_SPACESHIP;
                break;
            case EnumSpaceObject.MissilePlayer:
                newTag = ConstantsTag.TAG_MISSILE_PLAYER;
                break;
            //TODO

            default:
                newTag = ConstantsTag.TAG_SPACE_OBJECT;
                break;
        }

        tag = newTag;
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    protected IEnumerator SmoothMovement(Vector3 _endPos, bool _withAnimator)
    {
        isMoveSidewiseNow = true;

        float sqrRemainingDistance = (transform.position - _endPos).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Vector3 newPostion = Vector3.MoveTowards(rigibody.position, _endPos, speedSidewise * Time.deltaTime);
            Vector3 newPostion = Vector3.MoveTowards(rigibody.position, _endPos, speedSidewise * Time.fixedDeltaTime);

            rigibody.MovePosition(newPostion);
            sqrRemainingDistance = (transform.position - _endPos).sqrMagnitude;

            //yield return null;
            yield return new WaitForFixedUpdate();
        }

        //Снимаем флаг перемещения и отключаем анимацию
        isMoveSidewiseNow = false;
        if (_withAnimator)
        {
            animator.SetBool("ToLeft", false);
            animator.SetBool("ToRight", false);
        }        
    }
    #endregion
}
