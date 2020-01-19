using UnityEngine.Networking;
using UnityEngine;

public class GameManager : NetworkBehaviour {

    bool isFuture;
    bool isPast { get { return !isFuture; } }

    [SerializeField]
    GameObject futurePlayer;
    GameObject futurePlayerInstance = null;

    [SerializeField]
    GameObject pastPlayer;
    GameObject pastPlayerInstance = null;

    // keeps track of the local future and past game managers
    public GameManager futureManager;
    public GameManager pastManager;


    // Use this for initialization
    void Start ()
    {
        
        // is hacky way to determine who gets to be what
        if (netId.Value % 2 == 0)
            isFuture = true;
        else
            isFuture = false;

        // set the local future and past game managers.
        // the manager that is not the local player to this client
        // does nothing and is simply used to perform actions on the server
        if (isFuture)
        {
            gameObject.tag = "FutureManager";
            //futureManager = this;
        }
        else
        {
            gameObject.tag = "PastManager";
            //pastManager = this;
            Debug.Log("past assignmened");
        }
        
        // do nothing if not local player
        if (!isLocalPlayer)
        {
            Debug.Log("Will not do anything.");
            return;
        }

        
        // Following initialization is only done for the local future/past manager

        // build appropriate world
        if (isFuture)
        {
            futurePlayerInstance = Instantiate(futurePlayer, transform.position, transform.rotation);
            futurePlayerInstance.GetComponent<FuturePlayer>().SetGameManager(this);
        }
        else
        {
            pastPlayerInstance = Instantiate(pastPlayer, transform.position, transform.rotation);
            pastPlayerInstance.GetComponent<PastPlayer>().SetGameManager(this);
        }

        Debug.Log(netId);
    }

    // Update is called once per frame
    void Update ()
    {
    }

    /* ======== Spawning an object into the future ======== */

    // general function to spawn obj in the future
    public void SpawnInFuture(int objIndex)
    {
        // tell future man on server to spawn
        CmdPastToSpawnInFuture(objIndex);
    }

    // still the 'past' game manager
    // call the rpc with the future manager
    [Command]
    void CmdPastToSpawnInFuture(int objIndex)
    {
        futureManager.RpcSpawnInFuture(objIndex);
    }


    // tell clients to spawn obj
    // this wont be called on all gamemanagers, but only this game manager on the client
    [ClientRpc]
    void RpcSpawnInFuture(int objIndex)
    {
        if (isFuture && isLocalPlayer)
        {
            // this spawnball fn would be on a script only on the future player
            futurePlayerInstance.GetComponent<FuturePlayer>().Spawn(objIndex);
        }
    }

    /* ======== Spawning an object into the past ======== */

    // general function to spawn obj in the future
    public void SpawnInPast(int objIndex)
    {
        // tell future man on server to spawn
        CmdFutureToSpawnInPast(objIndex);
    }
    // command to tell server what object to spawn in the future
    [Command]
    void CmdFutureToSpawnInPast(int objIndex)
    {
        pastManager.RpcSpawnInPast(objIndex);
    }
    // tell clients to spawn obj
    // this wont be called on all gamemanagers, but only this game manager on the client
    [ClientRpc]
    void RpcSpawnInPast(int objIndex)
    {
        if (isPast && isLocalPlayer)
        {
            // this spawnball fn would be on a script only on the future player
            pastPlayerInstance.GetComponent<PastPlayer>().Spawn(objIndex);
        }
    }
}
