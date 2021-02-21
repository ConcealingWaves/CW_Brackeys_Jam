using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PopupModule : MonoBehaviour
{
    [SerializeField] private ScorePopup popupPrefab;
    [SerializeField] private Absorber comboerToRead;

    private void OnEnable()
    {
        HealthHaver.OnDie += SpawnPopup;
    }

    private void OnDisable()
    {
        HealthHaver.OnDie -= SpawnPopup;
    }

    private void SpawnPopup(HealthHaver dead)
    {
        Enemy enemy = dead.GetComponent<Enemy>();
        if (enemy == null || !enemy.enabled) return;
        ScorePopup myPopupPrefab = Instantiate(popupPrefab, dead.transform.position, quaternion.identity);
        myPopupPrefab.DisplayScore(enemy.Value * GlobalStats.DifficultyMultiplier(GlobalStats.instance.SelectedDifficulty));
        myPopupPrefab.DisplayCombo(comboerToRead.NumberAbsorbed);
    }
}
