using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour, IInteractable
{
    Player _actualPlayer;

    public float speed;
    public float minDist;

    public LineRenderer myLineRenderer;

    delegate void Movement();
    Movement actualMove = delegate { };

    void Update()
    {
        actualMove();
    }

    void Follow()
    {
        if (Vector3.Distance(transform.position, _actualPlayer.transform.position) > minDist)
        {
            transform.position += (_actualPlayer.transform.position - transform.position) * speed * Time.deltaTime;
        }

        myLineRenderer.SetPosition(0, transform.position);
        myLineRenderer.SetPosition(1, _actualPlayer.transform.position);
    }

    void Pull()
    {

    }

    public void StartInteractable(Player player)
    {
        _actualPlayer = player;
        actualMove = Follow;
    }

    public void PullItem()
    {

    }

    public void StopInteractable()
    {
        _actualPlayer = null;
        actualMove = delegate { };

        myLineRenderer.SetPosition(0, transform.position);
        myLineRenderer.SetPosition(1, transform.position);
    }
}
