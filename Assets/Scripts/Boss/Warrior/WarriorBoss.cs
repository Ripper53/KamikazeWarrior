using System.Collections.Generic;
using UnityEngine;

public class WarriorBoss : Boss {
    public UnitData UnitData;
    public Animator Animator;

    public Checkpoint CheckpointLeft, CheckpointTop;
    public SmoothDampToLocalPosition CheckpointTopToLocalPosition;

    [Header("Attacks")]
    public SpriteRenderer SwingAttack1;
    public SpriteRenderer SwingAttack2, SwingAttack3;
    public Check SwingAttackCheck, BladeCheck;

    [Header("Weary")]
    public Check WearyCheck;

    [Header("Player")]
    public UnitData PlayerUnitData;
    public CheckpointSaver CheckpointSaver;

    private void Awake() {
        UnitData.Damaged += UnitData_Damaged;
        UnitData.Destroyed += UnitData_Destroyed;
    }

    private const string hitTriggerName = "Hit";
    private void UnitData_Damaged(UnitData source, int damage) {
        weary = false;
        Animator.SetTrigger(hitTriggerName);
        timeOutTimer = 2f;
    }

    private const string deadBoolName = "Dead";
    private void UnitData_Destroyed(UnitData source) {
        PlayerUnitData.Destroyed -= PlayerUnitData_Destroyed;
        Animator.SetBool(deadBoolName, true);
        // Move checkpoint to finish.
        CheckpointTopToLocalPosition.TargetLocalPosition.x = 18.5f;
        CheckpointTopToLocalPosition.enabled = true;
        // Make sure checkpoint is correct checkpoint.
        CheckpointSaver.Checkpoint = CheckpointTop;
        enabled = false;
    }

    public override void Activate() {
        PlayerUnitData.Destroyed += PlayerUnitData_Destroyed;
    }

    private void PlayerUnitData_Destroyed(UnitData source) {
        PlayerUnitData.Destroyed -= PlayerUnitData_Destroyed;
        if (CheckpointSaver.Checkpoint != CheckpointLeft && CheckpointSaver.Checkpoint != CheckpointTop) {
            activated = false;
        }
    }

    private const string
        swingAttackTriggerName = "SwingAttack",
        stabAttackRightTriggerName = "StabAttackRight",
        stabAttackLeftTriggerName = "StabAttackLeft",
        wearyTriggerName = "Weary";

    private void Update() {
        if (swingTimer > 0f) {
            swingTimer -= Time.deltaTime;

            if (swingTimer < (swingDelay / 2f))
                SwingAttack3.enabled = false;
            if (swingTimer < (swingDelay / 4f))
                SwingAttack2.enabled = false;
            if (swingTimer <= 0f)
                SwingAttack1.enabled = false;
        }
    }

    private readonly List<int> randomInts = new List<int>(5);
    private float timeOutTimer = 0f;
    public override void Execute() {
        if (weary && WearyCheck.EvaluateCheck()) {
            UnitData.Damage(1);
        }

        if (timeOutTimer > 0f) {
            timeOutTimer -= Time.fixedDeltaTime;
        } else {
            if (randomInts.Count == 0) {
                randomInts.Add(0);
                randomInts.Add(0);
                randomInts.Add(1);
                randomInts.Add(2);
                randomInts.Add(3);
            }
            int index = Random.Range(0, randomInts.Count);
            switch (randomInts[index]) {
                case 0:
                    Animator.SetTrigger(swingAttackTriggerName);
                    timeOutTimer = 2f;
                    break;
                case 1:
                    Animator.SetTrigger(stabAttackRightTriggerName);
                    timeOutTimer = 2f;
                    break;
                case 2:
                    Animator.SetTrigger(stabAttackLeftTriggerName);
                    timeOutTimer = 2f;
                    break;
                default:
                    Animator.SetTrigger(wearyTriggerName);
                    timeOutTimer = 9f;
                    break;
            }
            randomInts.RemoveAt(index);
        }
    }

    private const float swingDelay = 0.5f;
    private float swingTimer = 0f;
    public void Swing() {
        SwingAttack1.enabled = true;
        SwingAttack2.enabled = true;
        SwingAttack3.enabled = true;
        swingTimer = swingDelay;

        if (SwingAttackCheck.EvaluateCheck()) {
            PlayerUnitData.Damage(1);
        }
    }

    public void BladeDanger() {
        if (BladeCheck.EvaluateCheck())
            PlayerUnitData.Damage(1);
    }

    private bool weary = false;
    public void BeginWeary() {
        weary = true;
    }

    public void EndWeary() {
        weary = false;
    }
}
