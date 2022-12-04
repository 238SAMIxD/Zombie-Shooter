using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour {
    public int enemies = 0;
    public int round = 0;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] TextMeshProUGUI Round;
    [SerializeField] TextMeshProUGUI Score;
    [SerializeField] TextMeshProUGUI Health;
    [SerializeField] Player player;
    [SerializeField] GameObject GameOver;
    [SerializeField] FirstPersonController fps;

    void Start() {
        Round.text = $"Round: 0";
        Score.text = $"Score: 0";
        Health.text = $"Health: 100";
        GameOver.SetActive(false);
    }

    void Update() {
        if(enemies == 0) {
            round++;
            NextWave(round);
        }

        Score.text = $"Score: {player.score}";
        Health.text = $"Health: {player.health}";

        if (Time.timeScale == 0) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                PlayAgain();
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void NextWave(int round) {
        Round.text = $"Round: {round}";
        if(player.health < 100) {
            player.health += 10;
        }
        for (int i = 0; i < 2 * round; i++) {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.transform.position, Quaternion.identity);
            enemy.GetComponent<Zombie>().spawn = GetComponent<Spawn>();

            enemies++;
        }
    }

    public void EndGame() {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameOver.SetActive(true);
    }

    public void PlayAgain() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }
}
