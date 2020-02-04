using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBoss : Boss {
    public UnitData UnitData;
    public GameObject ArenaObj;
    public Animator Animator;
    public Danger RightHandDanger, LeftHandDanger;
    public GameObject ExpansionMap, OldCheckpoint;
    [Header("Laser")]
    public SpriteRenderer LaserSR;
    public Sprite Laser0Sprite, Laser1Sprite;
    public Check LaserAttackCheck;
    [Header("Weary")]
    public SpriteRenderer WearySR;
    public Check WearyCheck;
    public SpriteRenderer HeadSR;
    public Sprite HeadSprite, WearyHeadSprite;
    [Header("Deactivate")]
    public GameObject RightHandObj;
    public GameObject LeftHandObj, TorsoObj;
    [Header("Player")]
    public UnitData PlayerUnitData;
    public CheckpointSaver CheckpointSaver;
    public Checkpoint WearyCheckpoint;

    private void Awake() {
        UnitData.Destroyed += UnitData_Destroyed;
    }

    private void UnitData_Destroyed(UnitData source) {
        Animator.SetBool("Dead", true);
        ArenaObj.SetActive(false);
        // Disable the old checkpoint, so it cannot be retracted.
        // If it were to be retracted, then the player could not get back to weary checkpoint.
        // This is why we disable it.
        OldCheckpoint.SetActive(false);
        // Ensure checkpoint is the weary checkpoint!
        CheckpointSaver.Checkpoint = WearyCheckpoint;
        ExpansionMap.SetActive(true);
        PlayerUnitData.Destroyed -= PlayerUnitData_Destroyed;
        enabled = false;
    }

    public override void Activate() {
        ArenaObj.SetActive(true);
        PlayerUnitData.Destroyed += PlayerUnitData_Destroyed;
    }

    private void PlayerUnitData_Destroyed(UnitData source) {
        if (CheckpointSaver.Checkpoint == WearyCheckpoint) return;
        // Reset Boss
        activated = false;
        ArenaObj.SetActive(false);
        executeTimer = 0f;
        wearyTimer = 0f;
        hitTimer = 0f;
        WearySR.enabled = false;
        HeadSR.sprite = HeadSprite;
        source.Destroyed -= PlayerUnitData_Destroyed;
    }

    private const string
        rightHandAttackTriggerName = "RightHandAttack",
        laserAttackTriggerName = "LaserAttack";

    private float executeTimer = 0f, wearyTimer = 0f, hitTimer = 0f;
    private readonly List<int> randomInts = new List<int>(3);
    public override void Execute() {
        if (executeTimer > 0f)
            executeTimer -= Time.fixedDeltaTime;
        else {
            if (randomInts.Count == 0) {
                randomInts.Add(0);
                randomInts.Add(0);
                randomInts.Add(1);
                randomInts.Add(2);
            }
            int index = Random.Range(0, randomInts.Count);
            switch (randomInts[index]) {
                case 0:
                    Animator.SetTrigger(rightHandAttackTriggerName);
                    executeTimer = 1f;
                    break;
                case 1:
                    Animator.SetTrigger(laserAttackTriggerName);
                    executeTimer = 1f;
                    break;
                default:
                    executeTimer = 1f;
                    break;
            }
            randomInts.RemoveAt(index);
        }

        if (hitTimer > 0f) {
            hitTimer -= Time.fixedDeltaTime;
            if (hitTimer <= 0f)
                HeadSR.sprite = HeadSprite;
        }

        if (laserTimer > 0f) {
            laserTimer -= Time.fixedDeltaTime;

            if (laserTimer < (laserDelay / 2f))
                LaserSR.sprite = Laser1Sprite;

            if (laserTimer <= 0f) {
                LaserSR.enabled = false;
            }
        }

        if (wearyTimer > 0f) {
            if (WearyCheck.EvaluateCheck()) {
                UnitData.Damage(1);
                wearyTimer = 0f;
                hitTimer = 1f;
                WearySR.enabled = false;
                HeadSR.sprite = WearyHeadSprite;
            } else {
                wearyTimer -= Time.fixedDeltaTime;
                if (wearyTimer <= 0f)
                    WearySR.enabled = false;
            }
        }

    }

    private const float laserDelay = 0.25f;
    private float laserTimer = 0f;
    public void LaserAttack() {
        LaserSR.sprite = Laser0Sprite;
        LaserSR.enabled = true;
        laserTimer = laserDelay;

        if (LaserAttackCheck.EvaluateCheck()) {
            PlayerUnitData.Damage(1);
        }
    }

    public void Weary() {
        wearyTimer = 0.4f;
        WearySR.enabled = true;
    }

    public void DeactiveBoss() {
        RightHandObj.SetActive(false);
        LeftHandObj.SetActive(false);
        TorsoObj.SetActive(false);

        HeadSR.sprite = HeadSprite;
        WearySR.enabled = false;
        LaserSR.enabled = false;
    }

    public void EnableRightHandDanger() => RightHandDanger.enabled = true;
    public void DisableRightHandDanger() => RightHandDanger.enabled = false;

    public void EnableLeftHandDanger() => LeftHandDanger.enabled = true;
    public void DisableLeftHandDanger() => LeftHandDanger.enabled = false;
}
