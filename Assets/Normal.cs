using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : Default
{
    private static float MONEYMULTIPLIER = 1.5f;

    public override void Start()
    {
        this._money = 500f;
        this._health = 3;
        SetWorkers(1);
        this.HasTurn = true;
        Debug.Log("start");
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonUp("GreenEnd") && this.HasTurn == true)
        {
            GetPaid(MONEYMULTIPLIER);
            this.HasMoved = false;
            this.HasTurn = false;
            if (Player2.PlayerDead() == false)
            {
                Player2.HasTurn = true;
            }

            else if (Player2.PlayerDead() == true && Player3.PlayerDead() == false)
            {
                Player3.HasTurn = true;
            }
        }
    }
}
