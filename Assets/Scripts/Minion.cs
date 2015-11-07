using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour
{

    public float max_health = 100f;
    public float current_health = 0f;
    public GameObject healthBar;
    public GameObject allBar;

    private GameObject _base;
    public float speed = 10;
    private Animator anim;

    private bool isAttacking = false;
    private BaseHealth bh;

    void Start()
    {
        //initiate curretn health
        current_health = max_health;
        //initiate base to protect
        _base = GameObject.FindGameObjectWithTag("Base");
        //initiate animations
        anim = GetComponent<Animator>();
        anim.SetInteger("anim", 0);


        bh = GameObject.FindGameObjectWithTag("Field").GetComponent<BaseHealth>();
    }
    
    void Update()
    {
        if (current_health > 0 && !isAttacking)
        {
            transform.LookAt(_base.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _base.transform.position, speed * Time.deltaTime);
        } else if (current_health > 0 && isAttacking){
            anim.SetInteger("anim", 2);
        }
    }

    public bool decreaseHealth()
    {
        if (current_health > 0)
        {
            current_health -= 2f;
            float calc_health = current_health / max_health;
            setHealthBar(calc_health);
        }
        if (current_health <= 0)
            return true;
        return false;
    }

    void setHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    public void die()
    {
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        isAttacking = false;
        anim.SetInteger("anim",1);
        Invoke("destroy", 10f);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col){
        if (col.transform.CompareTag("Base")){
            isAttacking = true;
        }

    }

    public void decreaseBaseHealth(){
        bh.decreaseBaseHealth();
    }
}
