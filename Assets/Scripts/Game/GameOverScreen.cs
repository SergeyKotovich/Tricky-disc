using DG.Tweening;
using Game;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _currentScoreLabel;
    
    [SerializeField]
    private TextMeshProUGUI _bestScoreLabel;
    
    [SerializeField]
    private float _newBestScoreAnimationDuration;
    
    [SerializeField]
    private AudioSource _bestScoreChangedSound;
    
    private void Awake()
    {
        var currentScore = PlayerPrefs.GetInt(GlobalConstants.SCORE_PREFS_KEY);
        var bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PREFS_KEY);
        
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            
            ShowNewBestScoreAnimation();
            
            SaveNewBestSore(bestScore);
            
        }
        _currentScoreLabel.text = currentScore.ToString();
            
        _bestScoreLabel.text = $"BEST {bestScore.ToString()}";
    }
    
    private void ShowNewBestScoreAnimation()
    {
        _bestScoreLabel.transform.DOPunchScale(Vector3.one, _newBestScoreAnimationDuration, 0);
        _bestScoreChangedSound.Play();
    }
    
    private void SaveNewBestSore(int bestScore)
    {
        PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PREFS_KEY, bestScore);
        
        PlayerPrefs.Save();
    }
    
    [UsedImplicitly]
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
    }
    
    [UsedImplicitly]
    public void ExitGame() 
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
        Application.Quit();
    }
}
