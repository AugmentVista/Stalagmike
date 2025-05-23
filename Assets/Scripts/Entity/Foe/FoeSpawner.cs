using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Entity.Foe
{
    internal class FoeSpawner : MonoBehaviour
    {
        // Place foe prefabs here.
        [SerializeField] GameObject[] spawnableFoes;

        FoeBase[] activeFoes = { };

        // Number of ticks per spawn.
        [SerializeField] int spawnRate = 250;
        [SerializeField] int maxSpawns = 5;

        Random random = new Random(DateTime.Now.Millisecond);

        int currentTicks = 250; // initialize with 250 to make it spawn immediately.

        private void FixedUpdate()
        {
            // Tick counter logic. Spawn every spawnRate ticks.
            currentTicks++;
            if (currentTicks >= spawnRate) { Spawn(); currentTicks = 0; }
        }

        // Tries to spawn a foe.
        void Spawn()
        {
            // Only do things if we have room to spawn more foes.
            if (activeFoes.Length < maxSpawns)
            {
                // Select a random foe, instantiate it at our position, then get its foebase component.
                FoeBase foe = (FoeBase)Instantiate(spawnableFoes[random.Next(0, spawnableFoes.Length)], transform).GetComponent(typeof(FoeBase));
                HealthSystem hs = (HealthSystem)foe.GetComponent(typeof(HealthSystem));

                // Add to list.
                var newList = activeFoes.ToList();
                newList.Add(foe);
                activeFoes = newList.ToArray();

                // Add ondeath behavior.
                hs.OnDeath += OnChildDeath;

                // When the thing we spawned dies, remove it from the list and unlisten.
                void OnChildDeath()
                {
                    hs.OnDeath -= OnChildDeath;

                    // Remove from list.
                    var otherNewList = activeFoes.ToList();
                    otherNewList.Remove(foe);
                    activeFoes = otherNewList.ToArray();
                }
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < activeFoes.Length; i++)
            {
                HealthSystem hs = (HealthSystem)activeFoes[i].GetComponent(typeof(HealthSystem));

                // Deal the max possible damage to the foe so it gets destroyed.
                hs.TakeDamage(int.MaxValue);
            }
        }

        // changes spawn rates from input from difficulty settings in options
        public void ApplyDifficultySettings(DifficultySettings settings)
        {
            spawnRate = settings.spawnRate;
            maxSpawns = settings.maxSpawns;
            Debug.Log($"Difficulty applied: SpawnRate = {spawnRate}, MaxSpawns = {maxSpawns}");
        }
    }
}