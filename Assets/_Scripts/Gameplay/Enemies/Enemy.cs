using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class  Enemy : MonoBehaviour
    {
        public static int countEnemy = 0;

     
        public static void AddEnemy()
        {
            countEnemy++;
            Debug.Log("se agrego un enemigo" + countEnemy);
        }
        public static void RemoveEnemy()
        {
            Debug.Log("se resto un enemigo" + countEnemy);
            countEnemy--;
        }
       
    }
}
