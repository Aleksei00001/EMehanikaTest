using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BallTupe
{
    StickySphere,
    SpikedBall,     
    Heart
}
public class Ball : MonoBehaviour
{
    [SerializeField] private BallTupe _ballTupe; public BallTupe ballTupe => _ballTupe;

    [SerializeField] private LevelControl levelControl;
    [SerializeField] private Player player;

    private void OnMouseDrag()
    {
        if (_ballTupe == BallTupe.StickySphere)
        {
            player.AddForsPlayre(this.gameObject);
        }
    }

    private void OnMouseUp()
    {
        if (_ballTupe == BallTupe.StickySphere)
        {
            player.SetTarget(player.gameObject);
        }
    }

    public void SetLevelControl(LevelControl newLevelControl)
    {
        levelControl = newLevelControl;
    }
    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }
}
