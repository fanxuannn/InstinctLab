using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using System;
using Unity.Collections;
using Unity.Burst;

//@Youngs 2019年5月4日22:30:31

namespace ILab.Youngs
{
    public class GameFXManager : MonoBehaviour
    {
        private EntityQuery query;
        public GameObject fxManager;
        public List<GameObject> fxObject;
        public List<float3> fxPosition;



        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            for (int i = 0; i < fxObject.Count; i++)
            {
                fxObject[i].transform.position = fxPosition[i];
            }
        }
    }

    public class MovementSystem : ComponentSystem
    {
        public EntityQuery playerFXQuery;
        public float3 refPositionData;
        public GameFXManager fXManager;
        public int count;

        protected override void OnCreate()
        {

            playerFXQuery = GetEntityQuery(
          new EntityQueryDesc
          {
              All = new ComponentType[] {
                  typeof(PlayerData)
              },
          });
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.WithAll<PlayerData>().ForEach(
                (Entity id, ref Translation translation) =>
                {
                    refPositionData = translation.Value;
                }
            );

            GameObject.Find("GameFX").gameObject.GetComponent<GameFXManager>().fxPosition[0] = refPositionData;


        }
    }
}

