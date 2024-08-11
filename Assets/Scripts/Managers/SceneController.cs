using System;
using System.Collections.Generic;
using System.Linq;
using DataClasses;
using Enums;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class SceneController : PersistentSingleton<SceneController>
    {
        private LevelData _levelData;
        private readonly HashSet<int> _boostersUsedThisLevel = new();
        
        public Dictionary<int, int> CollectedItems { get; } = new();
        public bool IsLevelCompleted { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
        
        private void OnEnable()
        {
            SceneManager.sceneUnloaded += OnSceneLoaded;

            EventManager.Instance.AddHandler(GameEvents.OnLevelButtonPressed, SwitchToGameScene);
            EventManager.Instance.AddHandler<int>(GameEvents.OnBoosterUsed, AddBoostersUsed);
            EventManager.Instance.AddHandler<int>(GameEvents.OnBoosterRemoved, RemoveBoostersUsed);
            EventManager.Instance.AddHandler<int,int>(GameEvents.OnMainEventGoalRemoval, HandleMainEventGoalRemoved);
            EventManager.Instance.AddHandler(GameEvents.OnLevelRestart,HandleRestart);
            EventManager.Instance.AddHandler(GameEvents.OnLevelFailed, () =>
            {
                CollectedItems.Clear();
                IsLevelCompleted = false;
                SceneManager.LoadScene(sceneBuildIndex: 1);
            });
            EventManager.Instance.AddHandler(GameEvents.OnReturnToMainMenu, () =>
            {
                IsLevelCompleted = true;
                SceneManager.LoadScene(sceneBuildIndex: 1);
            });
            EventManager.Instance.AddHandler(GameEvents.OnLevelCompleted, () =>
            {
                IsLevelCompleted = true;
            });
        }
        
        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= OnSceneLoaded;

            if (EventManager.Instance == null) 
                return;
            
            EventManager.Instance.RemoveHandler(GameEvents.OnLevelFailed, () =>
            {
                SceneManager.LoadScene(sceneBuildIndex: 1);
            });
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnBoosterUsed, AddBoostersUsed);
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnBoosterRemoved, RemoveBoostersUsed);
            EventManager.Instance.RemoveHandler(GameEvents.OnLevelButtonPressed, SwitchToGameScene);
            EventManager.Instance.RemoveHandler<int,int>(GameEvents.OnMainEventGoalRemoval, HandleMainEventGoalRemoved);
            EventManager.Instance.RemoveHandler(GameEvents.OnReturnToMainMenu, () =>
            {
                SceneManager.LoadScene(0);
            });
        }
        
        private void OnSceneLoaded(Scene arg0)
        {
            GC.Collect();
        }
    
        private void HandleRestart()
        {
            CollectedItems.Clear();
            IsLevelCompleted = false;
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
        
        private void SwitchToGameScene()
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
        
        public List<int> GetBoostersUsedThisLevel()
        {
            if( _boostersUsedThisLevel.Count==0)
                return null; 
            
            var boostersUsedCopy = new List<int>(_boostersUsedThisLevel.ToList());
            _boostersUsedThisLevel.Clear();
            
            return boostersUsedCopy;
        }
        
        private void AddBoostersUsed(int boosterId)
        {
            _boostersUsedThisLevel.Add(boosterId);
        }
        
        private void RemoveBoostersUsed(int boosterId)
        {
            _boostersUsedThisLevel.Remove(boosterId);
        }
        
        private void HandleMainEventGoalRemoved(int itemID, int amount)
        {
            if (!CollectedItems.TryAdd(itemID, amount))
                CollectedItems[itemID] += amount;
        }
    }
}