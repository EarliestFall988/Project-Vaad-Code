
using System.Collections;
using Avalon;
using RestSharp;
using UnityEngine;
using UnityEngine.AI;

namespace Revelation.Assets.Custom.Scripts.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class ZombieStateMachine : MonoBehaviour
    {

        public GameObject Target;

        private NavMeshAgent _agent;
        private Animator animator;

        public string StateStatus;

        private float _stateMachineTickRate = 0.125f;
        private float _stateMachineTickRateStore = 0f;

        [SerializeField]
        private string currentState = "";

        private string stateMachineInstructionsJSON = "";

        private StateMachine machine;


        private bool isInitialized = false;

        private StateMachineBuilder builder;

        void Start()
        {

            _agent = GetComponent<NavMeshAgent>();
            StartCoroutine(InitializeStateMachine());
        }

        /// <summary>
        /// Initializes the state machine by getting the instructions from the server and building the state machine.
        /// </summary>
        private IEnumerator InitializeStateMachine()
        {

            currentState = "loading instructions";

            builder = new StateMachineBuilder();

            //adding the game object references to the builder
            builder.GameObjectRefDict.Add("self", gameObject);
            builder.GameObjectRefDict.Add("target", Target);
            builder.stringRefDict.Add("status", StateStatus);

            ///grabbing the instructions from the server
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("api/sm/zombie", Method.GET);
            var response = client.Execute(request);

            while (response == null)
            {
                yield return null;
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("Could not get state machine instructions from server");
                yield break;
            }

            stateMachineInstructionsJSON = response.Content;

            currentState = "building instructions";
            machine = builder.ParseInstructionsJSON(stateMachineInstructionsJSON);
            machine.IsRunning = true;

            currentState = "completed building instructions";
            Debug.Log("Completed building instructions for " + gameObject.name);

            Debug.Log("Booting state machine - " + gameObject.name);

        }


        void FixedUpdate()
        {

            _stateMachineTickRateStore += Time.deltaTime;

            if (_stateMachineTickRateStore >= _stateMachineTickRate)
            {
                _stateMachineTickRateStore = 0;
                // Debug.Log("ticking");

                if (machine.IsRunning)
                {
                    machine.Evaluate();
                    currentState = machine.CurrentState.Name;
                    StateStatus = builder.stringRefDict["status"];
                    // Debug.Log("state status: " + builder.stringRefDict["status"]);
                }
            }
        }
    }
}