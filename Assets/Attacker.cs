using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Default
{
    private static float MONEYMULTIPLIER = 1f;

    public override void Start()
    {
        this._money = 50f;
        this._health = 1;
        SetWorkers(5);
        this.HasTurn = false;
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonUp("RedEnd") && this.HasTurn == true) //When Red turn ends, green turn starts, moves and then ends
        {
            GetPaid(MONEYMULTIPLIER);
            this.HasMoved = false;
            this.HasTurn = false;
            if (Player1.PlayerDead() == false)
            {
                Player1.HasTurn = true;
            }

            else if (Player1.PlayerDead() == true && Player2.PlayerDead() == false)
            {
                Player2.HasTurn = true;
            }
        }
    }

    public override void Attack()
    {
        if (_hasAttacked == true || this.HasTurn == false || GetWorkers() > 0)
        {
            return;
        }
        SetWorkers(GetWorkers()-1);
        if (Input.GetButtonUp("Green"))
        {
            if (Player1.GetWorkers() < this.GetWorkers())
                Player1._health -= 1;
        }

        if (Input.GetButtonUp("Blue"))
        {
            if (Player2.GetWorkers() < this.GetWorkers())
                Player2._health -= 1;
        }

        if (Input.GetButtonUp("Red"))
        {
            if (Player3.GetWorkers() < this.GetWorkers())
                Player3._health -= 1;
        }
    }

    public override void Hire()
    {
        if (this._hasHired == true || this.HasTurn == false || this._money < 250)
        {
            return;
        }
        else
        {
            SetWorkers(GetWorkers() + 1);
            this._money -= 250;
            this._hasHired = true;
        }

    }
}

