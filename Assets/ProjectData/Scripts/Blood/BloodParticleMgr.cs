using UnityEngine;
using System.Collections;

public class BloodParticleMgr : MonoBehaviour {

	public GameObject dropPrefab;
	private float disappearIn = 1.0f;
  public float scaleMultiplier = 1.0f;
	public bool popBlobDecal = false;

	void OnParticleCollision(GameObject other) {
    
		if(!popBlobDecal){
			return;
		}

		int safeLength = particleSystem.safeCollisionEventSize; 
		ParticleSystem.CollisionEvent[] collisionEvents = new ParticleSystem.CollisionEvent[safeLength];
		
		int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
		//Debug.Log ("OnParticleColission " + numCollisionEvents + " " + safeLength);
		int i = 0;
		while (i < numCollisionEvents) {
			Vector3 pos = collisionEvents[i].intersection;
			if( collisionEvents[i].collider.gameObject.layer == LayerMask.NameToLayer("Wall")
        || collisionEvents[i].collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
      {
				GameObject splatter = InstanceManager.InstantiateObject(dropPrefab, collisionEvents[i].intersection + (collisionEvents[i].normal * 0.005f)
				                                  , Quaternion.identity);
				//Debug.Log("Prefab instatiated");
        Vector3 scale = splatter.transform.localScale;
        Vector3 velocity = collisionEvents[i].velocity;
        Vector3 normal = collisionEvents[i].normal;

        //Vector3 modifiedVelocity = Math3d.ProjectPointOnPlane(normal, collisionEvents[i].intersection, velocity);
        Vector3 modifiedVelocity = Math3d.ProjectVectorOnPlane(normal, velocity.normalized);
        //Debug.Log("modified vel " + modifiedVelocity + " normal " + normal);
        splatter.transform.LookAt(splatter.transform.position + modifiedVelocity, normal);

        scale *= scaleMultiplier;
        scale.z *= velocity.magnitude * 0.1f;

        splatter.transform.localScale = scale;
				
				/*int rater = Random.Range (0, 3) * 90;
        splatter.transform.RotateAround(collisionEvents[i].intersection, collisionEvents[i].normal, rater);*/
				
				/*int matInd = Random.Range(0, materials.Count);
				splatter.renderer.sharedMaterial = materials[matInd];*/

        BloodSplat blood = splatter.GetComponent<BloodSplat>();
        blood.Invoke("clearBlood", disappearIn + Random.RandomRange(disappearIn * 0.1f, disappearIn * 0.5f));
				//Destroy (splatter, disappearIn + Random.RandomRange(disappearIn * 0.1f, disappearIn * 0.5f));
			}
			i++;
		}
	}
}
