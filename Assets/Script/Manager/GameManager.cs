using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public bool IsPaused = false;

    public UnityEvent eventGamePaused;

    [SerializeField] private GameObject enemy;

    protected override void OnAwake()
    {
        SetCursor(false, CursorLockMode.Locked);
    }

    protected override void OnStart()
    {
        eventGamePaused.AddListener(SetGamePaused);
    }

    public void SetGamePaused()
    {
        IsPaused = true;
    }

    public void LoadCurrentScene()
    {
        Debug.Log("On Click");
        SceneManager.LoadScene(0);
    }

    public void SetCursor(bool visible, CursorLockMode lockMode)
    {
        Cursor.visible = visible;
        Cursor.lockState = lockMode;
    }
}
