using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class materialChange : MonoBehaviour
{
    public XRRayInteractor rayInteractorLeft; // Reference to the left hand ray interactor
    public XRRayInteractor rayInteractorRight; // Reference to the right hand ray interactor
    public InputActionReference leftPointAction; // Input action for pointing with the left hand
    public InputActionReference rightPointAction; // Input action for pointing with the right hand

    public Material[] carouselMaterials; // Array to store materials for the carousel

    public GameObject[] walls;            // Array of wall gameobjects

    public GameObject carouselWallPanel; // Reference to the wall panel in the carousel
    public GameObject nextButton;            // next button
    public GameObject previousButton;        // previous button
    
    private int currentMaterialIndex = 0; // Index to keep track of the current material in carousel

    void Start()
    {
        
        leftPointAction.action.performed += PointAction;
        rightPointAction.action.performed += PointAction;
    }

    private void PointAction(InputAction.CallbackContext context){
        XRRayInteractor rayInteractor;
        if (context.action == leftPointAction.action)
            rayInteractor = rayInteractorLeft;
        else if (context.action == rightPointAction.action)
            rayInteractor = rayInteractorRight;
        else
            return;

        RaycastHit hit;
        if(rayInteractor.TryGetCurrent3DRaycastHit(out hit)){
            if(hit.collider.gameObject == nextButton){
                
                if(currentMaterialIndex < carouselMaterials.Length - 1){
                    currentMaterialIndex++;
                }
                else{
                    currentMaterialIndex = 0;
                }
            }
            else if(hit.collider.gameObject == previousButton){
                if(currentMaterialIndex > 0){
                    currentMaterialIndex--;
                }
                else{
                    currentMaterialIndex = carouselMaterials.Length - 1;
                }
            }
            carouselWallPanel.GetComponent<Renderer>().material = carouselMaterials[currentMaterialIndex];

        }

        RaycastHit hitWall;
        if(rayInteractor.TryGetCurrent3DRaycastHit(out hitWall)){
            for(int i = 0; i < walls.Length; i++){
                if(hitWall.collider.gameObject == walls[i]){
                    walls[i].GetComponent<Renderer>().material = carouselWallPanel.GetComponent<Renderer>().material;
                    walls[i].GetComponent<Renderer>().material.mainTextureScale = new Vector2(7f, 1f);
                }
            }
        }
    }

    void Update()
    {
    
    }
}





