using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    private float left_arm_hp = 30;
    private float right_arm_hp = 30;
    private float left_leg_hp = 40;
    private float right_leg_hp = 40;
    private float body_hp = 100;
    private float head_hp = 15;

    private float max_arm_hp = 30;
    private float max_leg_hp = 40;
    private float max_body_hp = 100;
    private float max_head_hp = 15;


    [SerializeField] private Image left_arm_image;
    [SerializeField] private Image right_arm_image;
    [SerializeField] private Image left_leg_image;
    [SerializeField] private Image right_leg_image;
    [SerializeField] private Image body_image;
    [SerializeField] private Image head_image;


    [SerializeField] private UI_Controll UI_Controll;
    private Player_Move Player_Move;
    private Fire Fire;
    void Start()
    {
        left_arm_hp = max_arm_hp;
        right_arm_hp = max_arm_hp;
        left_leg_hp = max_leg_hp;
        right_leg_hp = max_leg_hp;
        body_hp = max_body_hp;
        head_hp = max_head_hp;
        Player_Move = GetComponent<Player_Move>();
        Fire = GetComponent<Fire>();
    }

    int body_part(int accuracy)
    {
        int[] chances = { 15 - accuracy, 30 - accuracy * 2, 45 - accuracy * 3, 60 - accuracy * 4, 90 - accuracy * 2, 100 };

        int chance = Random.Range(0, 100) + 1;

        for (int index = 0; index < chances.Length; index++)
        {
            var ch = chances[index];
            if (chance <= ch)
            {
                return index;
            }
        }
        return 4;
    }

    internal void Damage(int damage, string type_damage, int accuracy)
    {
        switch(type_damage)
        {
            case "bite":

                break;
            case "penetrating":

                break;
            case "chopping":

                break;
            case "bullet":

                break;
        }

        switch (body_part(accuracy))
        {
            case 0:
                LeftArm(-damage);
                break;
            case 1:
                RightArm(-damage);
                break;
            case 2:
                LeftLeg(-damage);
                break;
            case 3:
                RightLeg(-damage);
                break;
            case 4:
                Body(-damage);
                break;
            case 5:
                Head(-damage);
                break;
        }
    }

    Color SetColor(float x, float y)
    {
        Color c = left_arm_image.color;
        c = new Color(c.r, c.g, c.b, (x - y) / x);
        return c;
    }

    void LeftArm(float delta)
    {
        if (left_arm_hp > 0)
        {
            left_arm_hp += delta;
            Fire.health_factor = (left_arm_hp + right_arm_hp) / (max_arm_hp * 2);

            left_arm_image.color = SetColor(max_arm_hp, left_arm_hp);
        }
        else
        {
            Body(delta / 1.5f);
        }
    }
    void RightArm(float delta)
    {
        if (right_arm_hp > 0)
        {
            right_arm_hp += delta;
            Fire.health_factor = (left_arm_hp + right_arm_hp) / (max_arm_hp * 2);

            right_arm_image.color = SetColor(max_arm_hp, right_arm_hp);
        }
        else
        {
            Body(delta / 1.5f);
        }
    }
    void LeftLeg(float delta)
    {
        if (left_leg_hp > 0)
        {
            left_leg_hp += delta;
            Player_Move.health_factor = (left_leg_hp + right_leg_hp) / (max_leg_hp * 2);

            left_leg_image.color = SetColor(max_leg_hp, left_leg_hp);
        }
        else
        {
            Body(delta / 2);
        }

    }
    void RightLeg(float delta)
    {
        if (right_leg_hp > 0)
        {
            right_leg_hp += delta;
            Player_Move.health_factor = (left_leg_hp + right_leg_hp) / (max_leg_hp * 2);

            right_leg_image.color = SetColor(max_leg_hp, right_leg_hp);
        }
        else
        {
            Body(delta / 2);
        }
    }
    void Body(float delta)
    {
        if (body_hp > 0 && head_hp > 0)
        {
            body_hp += delta;

            body_image.color = SetColor(max_body_hp, body_hp);
            if (body_hp <= 0)
            {
                UI_Controll.Death();
            }
        }
    }
    void Head(float delta)
    {
        if (head_hp > 0 && body_hp > 0)
        {
            head_hp += delta;

            head_image.color = SetColor(max_head_hp, head_hp);
            if(head_hp <= 0)
            {
                UI_Controll.Death();
            }
        }
    }
}
