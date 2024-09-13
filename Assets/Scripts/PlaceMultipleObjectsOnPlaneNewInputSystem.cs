using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class PlaceMultipleObjectsOnPlaneNewInputSystem : MonoBehaviour
{
    // Prefaben som kommer att skapas när användaren rör skärmen.
    [SerializeField]
    GameObject placedPrefab;

    // Referens till det skapade objektet.
    GameObject spawnedObject;

    // Hanterar pekskärmskontroller (input).
    TouchControls controls;

    // Hanterar raycasting i AR för att upptäcka ytor.
    ARRaycastManager aRRaycastManager;

    // Lista över träffar när en raycast används för att hitta AR-objekt eller ytor.
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        // Hämtar komponenten ARRaycastManager som är kopplad till samma objekt.
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // Startar kontrollerna för pekskärmen.
        controls = new TouchControls();

        // Om en tryckning på skärmen utförs, anropa OnPress-funktionen.
        controls.control.touch.performed += ctx =>
        {
            // Kontrollera att den inmatningsenhet som används är en pekare (som en touch-skärm).
            if (ctx.control.device is Pointer device)
            {
                // Använd pekarens position för att utföra OnPress-funktionen.
                OnPress(device.position.ReadValue());
            }
        };
    }

    private void OnEnable()
    {
        // Aktiverar kontrollerna när objektet blir aktivt.
        controls.control.Enable();
    }

    private void OnDisable()
    {
        // Inaktiverar kontrollerna när objektet blir inaktivt.
        controls.control.Disable();
    }

    // Denna funktion körs när användaren trycker på skärmen.
    void OnPress(Vector3 position)
    {
        // Kontrollera om raycasten träffade några spårbara objekt eller ytor.
        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast-träffarna sorteras efter avstånd, så den första träffen är närmast.
            var hitPose = hits[0].pose;

            // Skapa prefab på den träffade platsen och med den rätta rotationen.
            spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            // Gör så att det skapade objektet alltid tittar mot kameran. Ta bort detta om det inte behövs.
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;  // Håll objektet i samma höjd (ingen vertikal rotation).
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }
}

