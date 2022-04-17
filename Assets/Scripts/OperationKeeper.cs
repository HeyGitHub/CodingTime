using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationKeeper : MonoBehaviour {
    
    private static OperationKeeper _instance = null;
    
    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static OperationKeeper Instance {
        get {
            if (_instance == null) {
                _instance = new GameObject("DoorKeeper").AddComponent<OperationKeeper>();
            }
            return _instance;
        }
    }

    private void Update() {
        OperationHandle();
    }

    private int continuousTimes = 0;
    private float lastOperationTime = 0;
    private const float OPERATION_INTERVAL = .25f;
    private void OperationHandle() {
        if (Input.GetMouseButtonDown(0)) {
            if (Time.realtimeSinceStartup - lastOperationTime < OPERATION_INTERVAL) {
                continuousTimes++;
            } else {
                if (continuousTimes > 1) {
                    OperationTrigger();
                }
                continuousTimes = 1;
            }
            
            lastOperationTime = Time.realtimeSinceStartup;
        }
    }

    private const int STEP_1 = 5;
    private const int STEP_2 = 6;
    private const int STEP_3 = 7;
    private bool triggerStep1 = false;
    private bool triggerStep2 = false;
    private bool triggerStep3 = false;
    private void OperationTrigger() {
        if (triggerStep1 && triggerStep2 && triggerStep3) {
            OpenDoor();
            return;
        }
        
        if (continuousTimes == STEP_1) {
            if (!triggerStep1) {
                triggerStep1 = true;
                Debug.Log("step 1");
                return;
            }
        } else if (triggerStep1 && continuousTimes == STEP_2) {
            if (!triggerStep2) {
                triggerStep2 = true;
                Debug.Log("step 2");
                return;
            }
        } else if (triggerStep1 && triggerStep2 && continuousTimes == STEP_3) {
            if (!triggerStep3) {
                triggerStep3 = true;
                Debug.Log("step 3");
                return;
            }
        }
        
        CloseDoor();
    }

    private void OpenDoor() {
        CloseDoor();
        
        if (continuousTimes == 5) {
            Debug.Log("handled:::" + continuousTimes);
        } else if (continuousTimes == 8) {
            Debug.Log("handled:::" + continuousTimes);
        }
    }

    private void CloseDoor() {
        triggerStep1 = false;
        triggerStep2 = false;
        triggerStep3 = false;
    }
    
}
