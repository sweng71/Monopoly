using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Default
{
    private static float MONEYMULTIPLIER = 0.6f;

    public override void Start()
    {
        this._money = 1500f;
        this._health = 5;
        SetWorkers(2);
        this.HasTurn = false;
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonUp("BlueEnd") && this.HasTurn == true)
        {
            GetPaid(MONEYMULTIPLIER);
            this.HasMoved = false;
            this.HasTurn = false;
            if (Player3.PlayerDead() == false)
            {
                Player3.HasTurn = true;
            }

            else if (Player3.PlayerDead() == true && Player1.PlayerDead() == false)
            {
                Player1.HasTurn = true;
            }
        }
    }

    public override void Heal()
    {
        if (this._hasHealed == true || this.HasTurn == false || this._money < 500)
        {
            return;
        }
        else
        {
            this._health += 1;
            this._money -= 500;
            this._hasHealed = true;
        }

    }
}

