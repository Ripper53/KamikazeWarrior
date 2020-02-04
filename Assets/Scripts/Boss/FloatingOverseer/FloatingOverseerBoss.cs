using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingOverseerBoss : Boss {
    public Transform MovementTransform, RotationTransform;
    public UnitData UnitData;
    public Tentacle[] Tentacles;

    [Header("Movement")]
    public float MinX;
    public float MaxX;

    [Header("Angry")]
    public SpriteRenderer EyeSR;
    public Sprite AngryEyeSprite;
    public Check EyeHitCheck;
    public SmoothDampToPosition SmoothDampCheckpoint;
    public SmoothDampToLocalPosition SmoothDampEye;
    public Checkpoint Checkpoint;

    [Header("Player")]
    public UnitData PlayerUnitData;
    public CheckpointSaver CheckpointSaver;

#if UNITY_EDITOR
    [Header("Editor")]
    public bool die = false;
#endif

    private void Awake() {
        UnitData.Destroyed += Eye_Destroyed;
    }

#if UNITY_EDITOR
    private void Start() {
        if (die) {
            activated = true;
            Activate();
            UnitData.Destroy();
        }
    }
#endif

    private void Eye_Destroyed(UnitData source) {
        source.Destroyed -= Eye_Destroyed;
        source.Destroyed += AngryEye_Destroyed;

        // Make sure player goes to correct checkpoint.
        CheckpointSaver.Checkpoint = Checkpoint;

        SmoothDampCheckpoint.enabled = true;
        SmoothDampEye.enabled = true;
        angry = true;
        EyeSR.sprite = AngryEyeSprite;
        source.Heal(3);

        foreach (Tentacle tentacle in Tentacles) {
            foreach (Tentacle.Node node in tentacle.Nodes)
                node.Target = new Vector2(0f, 0f);
        }
        tentacleOffTimer = 2f;
    }

    private void AngryEye_Destroyed(UnitData source) {
        source.Destroyed -= AngryEye_Destroyed;

    }

    public override void Activate() {
        
    }

    private bool angry = false;
    public override void Execute() {
        // (0, 0, 0.25)
        RotationTransform.rotation *= new Quaternion(0f, 0f, 0.00218166f, 0.9999976f);

        if (angry)
            ExecuteAngryPhase();
        else
            ExecuteFirstPhase();
    }

    private const float tentacleDelay = 0.25f;
    private float tentacleTimer = 0f;
    private int tentacleIndex = 0;
    private int direction = 1;
    private void ExecuteFirstPhase() {
        if (tentacleTimer > 0f)
            tentacleTimer -= Time.fixedDeltaTime;
        else {
            TentacleFlow(Tentacles[tentacleIndex++]);
            if (tentacleIndex == Tentacles.Length)
                tentacleIndex = 0;
        }

        if (MovementTransform.localPosition.x < MinX)
            direction = 1;
        else if (MovementTransform.localPosition.x > MaxX)
            direction = -1;
        MovementTransform.localPosition += new Vector3(0.01f * direction, 0f);

        if (EyeHitCheck.EvaluateCheck()) {
            PlayerUnitData.Damage(1);
            UnitData.Damage(1);
        }
    }

    private float tentacleOffTimer = 0f;
    private void ExecuteAngryPhase() {
        if (tentacleOffTimer > 0f) {
            tentacleOffTimer -= Time.fixedDeltaTime;
            if (tentacleOffTimer <= 0f) {
                foreach (Tentacle tentacle in Tentacles)
                    tentacle.gameObject.SetActive(false);
            }
        }

        if (EyeHitCheck.EvaluateCheck()) {
            PlayerUnitData.Damage(1);
        }
    }

    private void TentacleFlow(Tentacle tentacle) {
        tentacleTimer = tentacleDelay;
        Vector2 previousPos = new Vector2(0f, 0f);
        foreach (Tentacle.Node node in tentacle.Nodes) {
            const float dis = 0.5f;
            node.Target = new Vector2(node.Transform.localPosition.x, previousPos.y + Random.Range(-dis, dis));
            previousPos = node.Transform.localPosition;
        }
    }
}
