using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class PlaceMultipleObjectsOnPlaneNewInputSystem : MonoBehaviour
{
    // Prefaben som kommer att skapas n�r anv�ndaren r�r sk�rmen.
    [SerializeField]
    GameObject placedPrefab;

    // Referens till det skapade objektet.
    GameObject spawnedObject;

    // Hanterar peksk�rmskontroller (input).
    TouchControls controls;

    // Hanterar raycasting i AR f�r att uppt�cka ytor.
    ARRaycastManager aRRaycastManager;

    // Lista �ver tr�ffar n�r en raycast anv�nds f�r att hitta AR-objekt eller ytor.
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        // H�mtar komponenten ARRaycastManager som �r kopplad till samma objekt.
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // Startar kontrollerna f�r peksk�rmen.
        controls = new TouchControls();

        // Om en tryckning p� sk�rmen utf�rs, anropa OnPress-funktionen.
        controls.control.touch.performed += ctx =>
        {
            // Kontrollera att den inmatningsenhet som anv�nds �r en pekare (som en touch-sk�rm).
            if (ctx.control.device is Pointer device)
            {
                // Anv�nd pekarens position f�r att utf�ra OnPress-funktionen.
                OnPress(device.position.ReadValue());
            }
        };
    }

    private void OnEnable()
    {
        // Aktiverar kontrollerna n�r objektet blir aktivt.
        controls.control.Enable();
    }

    private void OnDisable()
    {
        // Inaktiverar kontrollerna n�r objektet blir inaktivt.
        controls.control.Disable();
    }

    // Denna funktion k�rs n�r anv�ndaren trycker p� sk�rmen.
    void OnPress(Vector3 position)
    {
        // Kontrollera om raycasten tr�ffade n�gra sp�rbara objekt eller ytor.
        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast-tr�ffarna sorteras efter avst�nd, s� den f�rsta tr�ffen �r n�rmast.
            var hitPose = hits[0].pose;

            // Skapa prefab p� den tr�ffade platsen och med den r�tta rotationen.
            spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            // G�r s� att det skapade objektet alltid tittar mot kameran. Ta bort detta om det inte beh�vs.
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;  // H�ll objektet i samma h�jd (ingen vertikal rotation).
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }
}

