using UnityEngine;

namespace Behaviour
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public BehaviourTree Tree;

        private void Awake()
        {
            Tree = Tree.Clone();
            Tree.Bind(GetComponent<EnemyBrain>());
        }

        private void Update()
        {
            if (!GameManager.Instance.IsGameOver)
                Tree.Update();
        }
    }
}
