using UnityEngine;
using UnityEngine.UI;

namespace Leaderboards
{
    public class LeaderboardMgr : MonoBehaviour
    {
        public Text scoreText;

        private void Start()
        {
            scoreText.text = GameState.Instance.takeHomeTips.ToString("c2");
        }
    }
}
