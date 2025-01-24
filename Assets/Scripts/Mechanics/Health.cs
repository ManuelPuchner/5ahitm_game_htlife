using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.UI;


namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {

        public Image energyBar;
        public Image sleepBar;

        public Image gradesBar;


        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        /// <summary>
        ///  Energy for the Entity, Can be refueld by redbull
        /// </summary>
        public float maxEnergy = 100;
        float currentEnergy;
        public const float energyCoef = 1f;

        /// <summary>
        /// Sleep for the Entity, Can be refueld by sleeping
        /// </summary>
        public float maxSleep = 100;
        float currentSleep;
        public const float sleepCoef = 1f;

        /// <summary>
        /// Grades for the Entity, Can be refueld by studying
        /// </summary>
        public float maxGrades = 5;
        float currentGrades;
        public const float gradesCoef = 0.05f;

        private int updateCounter = 0;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            currentHP = maxHP;
            currentEnergy = maxEnergy / 2;
            currentSleep = maxSleep / 2;
            currentGrades = maxGrades;
        }

        void Update()
        {

            if(updateCounter % 200 == 0)
            {
                // decrement energy over time
                currentEnergy = Mathf.Clamp(currentEnergy - energyCoef, 0, maxEnergy);
                energyBar.fillAmount = currentEnergy / maxEnergy;

                // decrement sleep over time
                currentSleep = Mathf.Clamp(currentSleep - sleepCoef, 0, maxSleep);
                sleepBar.fillAmount = currentSleep / maxSleep;

                // decrement grades over time
                currentGrades = Mathf.Clamp(currentGrades - gradesCoef, 0, maxGrades);
                gradesBar.fillAmount = currentGrades / maxGrades;


                //log energy, sleep and grades
                Debug.Log("Energy: " + currentEnergy);
                Debug.Log("Sleep: " + currentSleep);
                Debug.Log("Grades: " + currentGrades);
            }
            updateCounter++;
        }

        public void IncrementEnergy(int value)
        {
            currentEnergy = Mathf.Clamp(currentEnergy + value, 0, maxEnergy);
            energyBar.fillAmount = currentEnergy / maxEnergy;
        }

        public void IncrementSleep(int value) 
        {
            currentSleep = Mathf.Clamp(currentSleep + value, 0, maxSleep);
            sleepBar.fillAmount = currentSleep / maxSleep;
        }

        public void IncrementGrades(int value)
        {
            currentGrades = Mathf.Clamp(currentGrades + value, 0, maxGrades);
            gradesBar.fillAmount = currentGrades / maxGrades;
        }
    }
}
