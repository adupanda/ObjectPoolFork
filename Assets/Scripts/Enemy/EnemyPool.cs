using CosmicCuration.Bullets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CosmicCuration.Enemy
{

    public class EnemyPool
    {
        private EnemyView enemyView;
        private EnemyScriptableObject enemyScriptableObject;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();

        public EnemyPool(EnemyView enemyView, EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyView = enemyView;
            this.enemyScriptableObject = enemyScriptableObject;
        }

        public EnemyController GetEnemy()
        {
            if(pooledEnemies.Count >0)
            {
                PooledEnemy pooledEnemy = pooledEnemies.Find(item => !item.isUsed);
                if(pooledEnemy != null)
                {
                    pooledEnemy.isUsed = true;
                    return pooledEnemy.enemy;
                }
                
            }

            return CreateNewEnemyInPool();
        }

        private EnemyController CreateNewEnemyInPool()
        {
            PooledEnemy pooledEnemy = new PooledEnemy()
            {
                enemy = new EnemyController(enemyView, enemyScriptableObject.enemyData),
                isUsed = true

            };

            pooledEnemies.Add(pooledEnemy);
            return pooledEnemy.enemy;
        }

        public void ReturnEnemyToPool(EnemyController returnedEnemy)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(item => item.enemy.Equals(returnedEnemy));
            pooledEnemy.isUsed = false;
        }

        public class PooledEnemy
        {
            public EnemyController enemy;
            public bool isUsed = false;
        
        }



    }

}

