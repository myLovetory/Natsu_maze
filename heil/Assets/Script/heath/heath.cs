using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heath : MonoBehaviour
{
    [Header ("heath")]
    [SerializeField] private float starting_healh;
    public float current_Heath { get; private set; }

    [Header("animation")]
    private Animator anim;
    public bool dead;

    [Header("Iframe")]
    //time dell bị tổn thg
    [SerializeField] private float Iframes_duration;
    [SerializeField] private int num_off_flashes;
    //change colour
    private SpriteRenderer sprite;

    [Header("Component")]

    [SerializeField] private Behaviour[] components;

    [Header("sound")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deadsound;

    //tim bất tử
    public bool invulnable;
    private void Awake()
    {

        anim = GetComponent<Animator>();
        current_Heath = starting_healh;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnable) return;
        current_Heath = Mathf.Clamp(current_Heath - _damage , 0 , starting_healh);
        
        if (current_Heath > 0)
        {
            //playerhurt
            //anime hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerbility());
            Soundmanager.Instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                //Player dead
                anim.SetTrigger("die");
                //if (GetComponent<PlayerMovement>() != null)
                //{
                //    GetComponent<PlayerMovement>().enabled = false;
                //}

                //if (GetComponentInParent<EnemyPatrol>() != null)
                //{
                //    GetComponentInParent<EnemyPatrol>().enabled = false;
                //}
                //if(GetComponent<MeleeEnemy>() != null)
                //{ 
                //    GetComponent<MeleeEnemy>().enabled = false;

                //}

                if (components != null)
                {
                    foreach (Behaviour component in components)
                    {
                        if (component != null)
                        {
                            component.enabled = false;
                        }
                    }
                }

                anim.SetBool("grounded", true);
                dead = true;

                Soundmanager.Instance.PlaySound(deadsound);
            }
        }
    }

    //ăn hp
    public void Add_Heart(float _value)
    {
        current_Heath = Mathf.Clamp(current_Heath + _value, 0, starting_healh);
    }
    //heath respawn
    public void Respawn()
    {
        
        Add_Heart(starting_healh);
        anim.ResetTrigger("die");
        anim.Play("Player_idle");
        StartCoroutine(Invunerbility());
        dead = false;

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    public IEnumerator Invunerbility()  
    {
        invulnable = true;
        //bỏ qua va chạm giữa các lướp
        Physics2D.IgnoreLayerCollision(8, 9, true);

        //quá trình bất tử
        for(int i = 0; i < num_off_flashes; i++)
        {
            //chuyển màu và nhấp nháy :)))
            sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(Iframes_duration / (num_off_flashes * 2));
            sprite.color = Color.white;
            yield return new WaitForSeconds(Iframes_duration / (num_off_flashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnable = false;
    }

    private void deactive()
    {
          gameObject.SetActive(false);
    }

    
    
}
