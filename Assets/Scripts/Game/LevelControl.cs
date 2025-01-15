using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private List<GameObject> activeObjectList;

    [SerializeField] private List<Ball> ballPrefab;
    [SerializeField] private List<float> baseSpawnChange;
    private List<float> spawnChange;

    float spawnRange;

    [SerializeField] private float spawnTime;
    private float spawnTimer;


    [SerializeField] private float speed;

    private float score;
    private float maxScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI maxScoreText;

    [SerializeField] private GameObject lostMenu;

    private void Start()
    {
        maxScore = PlayerPrefs.GetFloat("MaxScore", 0);
        spawnRange = ((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize - (ballPrefab[0].transform.localScale.x / 2);
        spawnChange = new List<float>(baseSpawnChange);
        StartGame();
    }

    private void Update()
    {
        if (spawnTimer >= spawnTime)
        {
            float spawnChangeSumm = 0;
            for (int i = 0; i < spawnChange.Count; i++)
            {
                spawnChangeSumm += spawnChange[i];
            }
            float spawnRandom = Random.Range(0, spawnChangeSumm);
            bool isSpawn = false;
            for (int i = 0; i < spawnChange.Count; i++)
            {
                spawnRandom -= spawnChange[i];
                if (isSpawn == false)
                {
                    if (spawnRandom <= 0)
                    {
                        SpawnBall(ballPrefab[i]);
                        spawnChange[i] = baseSpawnChange[i];
                        isSpawn = true;
                    }
                    else
                    {
                        spawnChange[i] += baseSpawnChange[i];
                    }
                }
                else
                {
                    spawnChange[i] += baseSpawnChange[i];
                }
            }
            spawnTimer = 0;
        }
        else
        {
            spawnTimer += Time.deltaTime * speed;
        }

        if (activeObjectList.Count > 0)
        {
            for (int i = activeObjectList.Count - 1; i >= 0; i--)
            {
                if (activeObjectList[i] != null)
                {
                    MoveObject(activeObjectList[i], speed);
                    if (activeObjectList[i].transform.position.y < -6)
                    {
                        RemoveGameObject(activeObjectList[i]);
                    }
                }
                else
                {
                    activeObjectList.Remove(activeObjectList[i]);
                }
            }
        }
        AddScore(speed * Time.deltaTime);
        speed += 0.01f * Time.deltaTime;



    }

    public void SpawnBall(Ball ballPrefab)
    {
        Ball newBall = Instantiate<Ball>(ballPrefab);
        newBall.transform.position = new Vector2(Random.Range(-spawnRange, spawnRange), 6);
        newBall.SetLevelControl(this);
        newBall.SetPlayer(player);
        activeObjectList.Add(newBall.gameObject);
    }

    public void MoveObject(GameObject gameObject, float fullSpeed)
    {
        Vector3 movement = new Vector3(0, -1, 0);
        gameObject.transform.position += movement * fullSpeed * Time.deltaTime;
    }

    public void RemoveGameObject(GameObject removeGameObject)
    {
        activeObjectList.Remove(removeGameObject);
        Destroy(removeGameObject);
    }

    public void AddScore(float addScore)
    {
        score += addScore;
        if (maxScore < score)
        {
            maxScore = score;
            PlayerPrefs.SetFloat("MaxScore", maxScore);
        }

        scoreText.text = ((int)score).ToString();
        maxScoreText.text = ((int)maxScore).ToString();
    }


    public void StartGame()
    {
        Time.timeScale = 1f;
        lostMenu.SetActive(false);
        score = 0;
        player.transform.position = new Vector2(0, -4);
        player.SetZeroLineRender();
        player.SetHPToMaxHP();

        if (activeObjectList.Count > 0)
        {
            for (int i = activeObjectList.Count - 1; i >= 0; i--)
            {
                if (activeObjectList[i] != null)
                {
                    RemoveGameObject(activeObjectList[i]);
                }
                else
                {
                    activeObjectList.Remove(activeObjectList[i]);
                }
            }
        }
        AddScore(0);
        player.SetZeroVelosity();
    }

    public void RestartGame()
    {
        StartGame();
    }

    public void LoseGame()
    {
        lostMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
