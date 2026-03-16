using UnityEngine;

public class ExampleSubscribeToGameState : MonoBehaviour
{
    private PlayingState _myUpdateState;
    void Start()
    {
        //myUpdateState = (PlayingState)GameManager.Instance.GetGameState<PlayingState>();
        _myUpdateState = GameManager.Instance.GetState<PlayingState>();
        if (_myUpdateState != null)
        {
            _myUpdateState.StateUpdate.AddListener(ManagedUpdate);
        } else
        {
            Debug.Log("tried to add listener but myUpdateState == null");
        }
    }

    void OnDisable()
    {
        if (_myUpdateState != null)
        {
            _myUpdateState.StateUpdate.RemoveListener(ManagedUpdate);
        }
    }

    // Managed update is called when the play state event it is Listening to is Invoked
    void ManagedUpdate()
    {
        Debug.Log(this.ToString() +", is printing this on the play states's update event");
    }

    void ManagedFixedUpdate()
    {
        Debug.Log(this.ToString() + ", is printing this on the play state's fixed update event");
    }
}
