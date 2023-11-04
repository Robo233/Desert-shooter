using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
    [SerializeField] int number_of_desert_objects;
    [SerializeField] float min_distance_between_desert_objects;
    [SerializeField] float size_change_value_desert_objects;
    [SerializeField] GameObject[] desert_objects;

    [SerializeField] int number_of_rocks;
    [SerializeField] float min_distance_rocks;
    [SerializeField] float size_change_value_rocks;
    [SerializeField] GameObject[] rocks;

    List<GameObject> created_objects = new List<GameObject>();

    [SerializeField] float min_spawn_value_x;
    [SerializeField] float max_spawn_value_x;
    [SerializeField] float min_spawn_value_z;
    [SerializeField] float max_spawn_value_z;

    [SerializeField] float max_distance_from_spawn_point;


    void Start(){
        generate_random_map(number_of_desert_objects,min_distance_between_desert_objects,desert_objects,created_objects,size_change_value_desert_objects,min_spawn_value_x,max_spawn_value_x,min_spawn_value_z,max_spawn_value_z,max_distance_from_spawn_point);
        generate_random_map(number_of_rocks,min_distance_rocks,rocks,created_objects,size_change_value_rocks,min_spawn_value_x,max_spawn_value_x,min_spawn_value_z,max_spawn_value_z);
    
    }

    void generate_random_map(int number_of_locations, float min_distance, GameObject[] objects, List<GameObject> created_objects, float size_change_value, float min_spawn_value_x, float max_spawn_value_x, float min_spawn_value_z, float max_spawn_value_z, float max_distance_from_spawn_point=0){
        while (number_of_locations >= 0){
        int current_index = Random.Range(0,objects.Length);
        Vector3 position = new Vector3(Random.Range(min_spawn_value_x,max_spawn_value_x),0,Random.Range(min_spawn_value_z,max_spawn_value_z));
        if(is_gameobject_n_distance_apart_from_all_other_gameobjects(position,created_objects,min_distance) && Vector3.Distance(Vector3.zero,position)>max_distance_from_spawn_point  ){
            GameObject spawned_object = GameObject.Instantiate(objects[current_index]);
            spawned_object.transform.localScale = new Vector3(Random.Range(spawned_object.transform.localScale.x-size_change_value,spawned_object.transform.localScale.x+size_change_value),Random.Range(spawned_object.transform.localScale.y-size_change_value,spawned_object.transform.localScale.y+size_change_value),Random.Range(spawned_object.transform.localScale.z-size_change_value,spawned_object.transform.localScale.z+size_change_value));
            spawned_object.transform.rotation = Quaternion.Euler(spawned_object.transform.rotation.x, (float)Random.Range(0f, 360f), spawned_object.transform.rotation.z);
            spawned_object.transform.position = position;
            created_objects.Add(spawned_object);
            number_of_locations--;
        }else{
           // Debug.Log("bad position");
        }
        } 
    }



    bool is_gameobject_n_distance_apart_from_all_other_gameobjects(Vector3 position, List<GameObject> objects, float distance){
        for(int i=0;i<objects.Count;i++){
            if( ((Vector3.Distance(position, objects[i].transform.position))<distance) ){
                return false;
            }   
        }
        return true;
    }


}