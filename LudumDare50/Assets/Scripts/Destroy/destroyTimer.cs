using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTimer : MonoBehaviour
{
    [SerializeField] float defaultDestructionTime;
    [SerializeField] bool startTimerOnAwake = true;

    void Awake(){
        if (startTimerOnAwake) StartTimer(defaultDestructionTime);
    }

    public void StartTimer(float _time){
        StartCoroutine(DestroyAfterTime(_time));
    }

    IEnumerator DestroyAfterTime (float _time){
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);   
    }
}
