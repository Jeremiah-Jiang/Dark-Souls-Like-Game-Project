using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class IllusionaryWall : MonoBehaviour
    {
        public bool wallHasBeenHit;
        [SerializeField]
        Material illusionaryWallMaterial;
        float alpha;
        float fadeTimer = 2.5f;
        [SerializeField]
        BoxCollider wallBoxCollider;
        [SerializeField]
        MeshCollider wallMeshCollider;

        [SerializeField]
        MeshRenderer meshRenderer;
        AudioSource audioSource;
        public AudioClip illusionaryWallSound;

        private void Awake()
        {
            illusionaryWallMaterial = GetComponent<Renderer>().material;
            meshRenderer = GetComponent<MeshRenderer>();
            wallBoxCollider = GetComponent<BoxCollider>();
            wallMeshCollider = GetComponent<MeshCollider>();
            audioSource = GetComponent<AudioSource>();
            //illusionaryWallMaterial = Instantiate(illusionaryWallMaterial);
            //meshRenderer.material = illusionaryWallMaterial;
        }

        private void Update()
        {
            if(wallHasBeenHit)
            {
                FadeIllusionaryWall();
            }
        }

        public void FadeIllusionaryWall()
        {
            // = illusionaryWallMaterial.GetColor("_BaseColor").a;
            alpha = illusionaryWallMaterial.color.a;
            alpha -= Time.deltaTime / fadeTimer;
            Color fadedWallColor = new Color(illusionaryWallMaterial.color.r, illusionaryWallMaterial.color.g, illusionaryWallMaterial.color.b, alpha);
            //illusionaryWallMaterial.SetColor("_BaseColor", fadedWallColor); 
            illusionaryWallMaterial.color = fadedWallColor;

            if(wallBoxCollider != null)
            {
                if (wallBoxCollider.enabled)
                {
                    wallBoxCollider.enabled = false;
                    audioSource.PlayOneShot(illusionaryWallSound);
                }
            }
            else if(wallMeshCollider != null)
            {
                if(wallMeshCollider.enabled)
                {
                    wallMeshCollider.enabled = false;
                    audioSource.PlayOneShot(illusionaryWallSound);
                }
            }
            if(alpha < 0)
            {
                Destroy(this);
                Destroy(this.meshRenderer); //remove shadow
                if(wallMeshCollider != null)
                {
                    Destroy(this.wallMeshCollider);

                }
            }
        }
    }
}

