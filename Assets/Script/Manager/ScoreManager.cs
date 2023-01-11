using UnityEngine;
using TMPro;
using Reference.Primitive;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using Entity;
using Unity.VisualScripting;

public class ScoreManager : Manager<ScoreManager>
{
    [SerializeField] private GameObject _wagonPrefab;
    [SerializeField] private GameObject _instantiateContainer;
    [SerializeField] private int _newWagonTreshold = 49;
    //[SerializeField] private int _winThreshold = 777;

    [SerializeField] private IntReference m_score;
    [SerializeField] private int incrementalScore;
    [SerializeField] private Canvas m_canvas;
    [SerializeField] private Image m_background;
    [SerializeField] private TMP_Text m_text;

    [Space(10)]
    [SerializeField] private GameObject m_endScreenCanvas;
    [SerializeField] private TMP_Text _endScreenText;

    protected override void OnAwake()
    {
    }
    
    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        ScoreUpdate();
    }
    public void ScoreUpdate()
    {
        m_text.text = m_score.Value.ToString();
    }

    public void AddScore(int score)
    {
        m_score.Value += score;
        m_text.text = m_score.Value.ToString();

        incrementalScore += score;

/*        if (m_score.Value % _winThreshold == 0)
        {
            GameManager.Instance.eventGamePaused.Invoke();
        }*/
        if (incrementalScore % _newWagonTreshold == 0)
        {
            Entity_Player.Instance.last.OnIntantiateInit();
        }
    }

    public void ShowRestart(string mainMessage = "")
    {
        m_endScreenCanvas.SetActive(true);
        _endScreenText.text = mainMessage;
        GameManager.Instance.SetCursor(true, CursorLockMode.None);
    }
}
