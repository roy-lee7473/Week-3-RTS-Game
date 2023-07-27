using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameLibrary
{
    public GameObject FindClosestTarget(Vector3 position, string tag, float maxDistance)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);  //Fill array with all gameobjects with the tag
        PriorityQueue<GameObject, float> pq = new PriorityQueue<GameObject, float>();
        float distance = maxDistance * maxDistance;         //square the distance

        foreach (GameObject obj in gameObjects)
        {
            Vector3 difference = obj.transform.position - position; //calculate the difference to the object from our current position
            float curDistance = difference.sqrMagnitude;    //distance requires a square root, which is slow, just using the squared magnitued

            if (curDistance < distance)                     //comparing the squared distances
            {
                pq.Enqueue(obj, curDistance);
            }
        }

        GameObject closest = null;                          //result: starts at null just in case we don't find anything in range
        if (pq.Count > 0)                                   //check to make sure something was added to the PQ (prevent errors for empty)
        {
            //Removes the first element which has the smallest distance
            closest = pq.Dequeue();
        }

        //Return the obejct we found, or null if we didn't find anything
        return closest;
    }
}
