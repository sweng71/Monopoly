using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Default : MonoBehaviour
{

    private Route CurrentRoute; //Moving attrivutes
    private int _routePosition;
    private int _steps;
    private bool _isMoving;
    protected bool HasMoved = false;


    protected Normal Player1; //Turn based attributes
    protected Defender Player2;
    protected Attacker Player3;
    public bool HasTurn; 
    private bool _hasInteracted;

    protected float _money; // Other attributes
    private float _income;
    public int _health;
    private int _workers;
    protected bool _hasHealed = false;
    protected bool _hasAttacked = false;
    protected bool _hasHired = false;

    public virtual void Start()
    {
        CurrentRoute = GameObject.Find("Route").GetComponent<Route>();
        Player1 = GameObject.Find("Normal").GetComponent<Normal>();
        Player2 = GameObject.Find("Defender").GetComponent<Defender>();
        Player3 = GameObject.Find("Attacker").GetComponent<Attacker>();
    }

    public virtual void Update()
    {
        if (PlayerDead() == false)
        {
            if (HasTurn == true)
            {
                if (HasMoved == false)
                {
                    Moving();
                    this._income = 0f;
                    this._hasInteracted = false;
                    this._hasHealed = false;
                    this._hasAttacked = false;
                    this._hasHired = false;
                    Debug.Log($"Health:{this._health}, Workers:{this._workers}, Money:{this._money}");
                    HasMoved = true;
                }
            }
            Interact();
        }

        if (PlayerDead() == true)
        {
            transform.position = new Vector3(100, 100, 100);
            this.HasTurn = false;
        }
    }

    public virtual void Heal()
    {
        if (this._hasHealed == true || this.HasTurn == false || this._money < 1500)
        {
            Debug.Log("Not Healing");
            return;
        }
        this._health += 1;
        this._money -= 1500;
        this._hasHealed = true;
        Debug.Log("Healing");
    }

    public virtual void Hire()
    {
        this._workers += 1;
        this._money -= 500;
        Debug.Log("Hiring");
    }

    public virtual void Attack()
    {
        if (_hasAttacked == true || this.HasTurn == false || this._workers > 0 || this._money < 250)
        {
            return;
        }
        this._workers -= 1;
        this._money -= 250;
        if (Input.GetButtonUp("Green"))
        {
            if (Player1.GetWorkers() < this._workers)
            Player1._health -= 1;
        }

        if (Input.GetButtonUp("Blue"))
        {
            if (Player1.GetWorkers() < this._workers)
                Player2._health -= 1;
        }

        if (Input.GetButtonUp("Red"))
        {
            if (Player1.GetWorkers() < this._workers)
                Player3._health -= 1;
        }
    }

    public int GetWorkers()
    {
        return this._workers;
    }

    public void SetWorkers(int worker)
    {
        this._workers = worker;
    }

    public float GetMoney()
    {
        return this._money;
    }

    public void Interact()
    {
        if (this._hasInteracted == false)
        {
            if (GetIsMoving() == false)
            {
                if (transform.position == CurrentRoute.childNodeList[2].position ||
                    transform.position == CurrentRoute.childNodeList[4].position ||
                    transform.position == CurrentRoute.childNodeList[12].position ||
                    transform.position == CurrentRoute.childNodeList[14].position) //Land on money tiles (Green)
                {
                    this._income += 500f;
                    this._hasInteracted = true;
                }

                else if (transform.position == CurrentRoute.childNodeList[9].position || transform.position == CurrentRoute.childNodeList[19].position) // Land on lose money tiles (Light orange)
                {
                    this._income -= 750f;
                    this._hasInteracted = true;
                }

                else if (transform.position == CurrentRoute.childNodeList[10].position) // Land on lose 1 life tile (Black)
                {
                    this._health -= 1;
                    this._hasInteracted = true;
                }

                else if (transform.position == CurrentRoute.childNodeList[8].position) // Land on lose 1 worker tile (Purple)
                {
                    this._workers -= 1;
                    this._hasInteracted = true;
                }

                else if (transform.position == CurrentRoute.childNodeList[18].position) // Land on gain 1 worker tile (Cyan)
                {
                    this._workers += 1;
                    this._hasInteracted = true;
                }
            }
        }
    }

    public void GetPaid(float multiplier)
    {
        this._money += this._income * multiplier;
    }

    public bool PlayerDead()
    {
        if (this._money < -10)
        {
            return true;
        }

        else if (this._workers < -1)
        {
            return true;
        }

        else if (this._health < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Moving()
    {
        if (!_isMoving)
        {
            _steps = Random.Range(1, 7);

            StartCoroutine(Move());

        }
    }

    private IEnumerator Move()
    {
        if (_isMoving)
        {
            yield break;
        }
        _isMoving = true;

        while (_steps > 0)
        {
            _routePosition += 1;
            _routePosition %= CurrentRoute.childNodeList.Count;

            Vector3 nextPos = CurrentRoute.childNodeList[_routePosition].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.05f);
            _steps -= 1;

        }
        _isMoving = false;
    }

    public bool GetIsMoving()
    {
        return _isMoving;
    }

    private bool MoveToNextNode(Vector3 goal)
    {

        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 6f * Time.deltaTime));

    }
}