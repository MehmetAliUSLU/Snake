using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments = new List<Transform>();

    public Transform ssegmentPrefab;

    public int initialSize = 4;

    public int goal=15;

    private int score = 0;  

    void Start()
    {
        ResetGame();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)&&_direction!=Vector2.down)
        {
            _direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
        else if(Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }

        if (_segments.Count==goal)
        {
            WinGame();
        }
        
    }

    private void WinGame()
    {
        if (SceneManager.sceneCount != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count-1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f

            );
    }

    private void Grow()
    {

        Transform segment = Instantiate(ssegmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

    }

    private void ResetGame()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(ssegmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            score++;
            Grow();

        }
        else if (other.tag == "Obstacle")
        {
            ResetGame();
        }
        else if (other.tag == "ObstacleDown")
        {
            this.transform.position = new Vector3(
                this.transform.position.x,this.transform.position.y+25,0.0f
                );
        }
        else if (other.tag == "ObstacleUp")
        {
            this.transform.position = new Vector3(
                this.transform.position.x, this.transform.position.y -25, 0.0f
                );
        }
        else if (other.tag == "ObstacleLeft")
        {
            this.transform.position = new Vector3(
                this.transform.position.x+51, this.transform.position.y, 0.0f
                );
        }
        else if (other.tag == "ObstacleRight")
        {
            this.transform.position = new Vector3(
                 this.transform.position.x - 51, this.transform.position.y, 0.0f
                 );
        }
    }
}
