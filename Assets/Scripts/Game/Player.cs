using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private LevelControl levelControl;

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float fors;

    [SerializeField] private int maxHP;
    [SerializeField] private int HP;
    [SerializeField] private List<Image> image;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartZero;

    [SerializeField] private GameObject target;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float speedLineRenderer;

    private void Start()
    {
        ChangeHP(0);
    }

    private void Update()
    {
        if (target == null)
        {
            target = this.gameObject;
        }
        Vector2 currentPosition = lineRenderer.GetPosition(1);
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, target.transform.position, speedLineRenderer * Time.deltaTime);

        lineRenderer.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 85));
        lineRenderer.SetPosition(1, new Vector3(newPosition.x, newPosition.y, 85));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>() == true)
        {
            Ball ball = collision.GetComponent<Ball>();
            
            if (ball.ballTupe == BallTupe.StickySphere)
            {
                levelControl.AddScore(5);
            }
            else if (ball.ballTupe == BallTupe.SpikedBall)
            {
                ChangeHP(-1);
            }
            else if (ball.ballTupe == BallTupe.Heart)
            {
                ChangeHP(+1);
            }

            levelControl.RemoveGameObject(collision.gameObject);
        }
    }

    public void ChangeHP(int changeHP)
    {
        HP += changeHP;
        if (HP > maxHP)
        {
            levelControl.AddScore((HP - maxHP) * 50);
            HP = maxHP;
        }

        for (int i = 0; i < image.Count; i++)
        {
            if (HP >= i + 1)
            {
                image[i].sprite = heartFull;
            }
            else
            {
                image[i].sprite = heartZero;
            }
        }

        if (HP <= 0)
        {
            levelControl.LoseGame();
        }

    }

    public void AddForsPlayre(GameObject newTarget)
    {
        SetTarget(newTarget);
        Vector2 diractionFors = new Vector2(transform.position.x - newTarget.transform.position.x, transform.position.y - newTarget.transform.position.y).normalized;
        rigidbody2D.AddForce(-diractionFors * fors);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void SetHPToMaxHP()
    {
        HP = maxHP;
        ChangeHP(0);
    }

    public void SetZeroVelosity()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        rigidbody2D.totalForce = new Vector2(0, 0);
    }

    public void SetZeroLineRender()
    {
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position);
    }
}
