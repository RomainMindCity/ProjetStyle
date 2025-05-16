using UnityEngine;

public interface ITriggerable
{
    void OnTriggerEnterObject(GameObject triggeringObject);
    void OnTriggerExitObject(GameObject triggeringObject);
}
