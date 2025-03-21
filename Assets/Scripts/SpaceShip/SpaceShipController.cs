using System;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] private GameObject boosterL;
    [SerializeField] private GameObject boosterR;
    public Transform Effect_Middle = null;
    public Transform Effect_Left = null;
    public Transform Effect_Right = null;
    public Transform Muzzle_Middle = null;
    public Transform Muzzle_Left = null;
    public Transform Muzzle_Right = null;
    private int Upgradelevel = 0;
    public GameObject[] bullet;
    public float SpaceShipHp = 100f;
    public GameObject[] Effect;

    private void OnEnable()
    {
        EventManager.Instance.OnAttack += Attack;
    }
    
    private void OnDisable()
    {
        EventManager.Instance.OnAttack -= Attack;
    }

    private void Update()
    {
        if (InputManager.Instance.horizontal > 0f)
        {
            boosterL?.SetActive(true);
            boosterR?.SetActive(false);
        }
        else if (InputManager.Instance.horizontal < 0f)
        {
            boosterL?.SetActive(false);
            boosterR?.SetActive(true);
        }
        else
        {
            boosterL?.SetActive(false);
            boosterR?.SetActive(false);
        }
    }

    private void Attack()
    {
        if (Upgradelevel == 0)
        {
            Instantiate(bullet[0], Muzzle_Middle.position, Quaternion.identity);
            Instantiate(Effect[0], Effect_Middle.position, Quaternion.identity);
        }
        else if (Upgradelevel == 1)
        {
            Instantiate(bullet[0], Muzzle_Left.position, Quaternion.identity);
            Instantiate(bullet[0], Muzzle_Middle.position, Quaternion.identity);
            Instantiate(bullet[0], Muzzle_Right.position, Quaternion.identity);
            Instantiate(Effect[0], Effect_Middle.position, Quaternion.identity);
            Instantiate(Effect[1], Effect_Left.position, Quaternion.identity);
            Instantiate(Effect[1], Effect_Right.position, Quaternion.identity);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Upgradelevel++;
            Upgradelevel = Mathf.Clamp(Upgradelevel, 0, 1);
            Destroy(collision.gameObject);
        }
    }
}
