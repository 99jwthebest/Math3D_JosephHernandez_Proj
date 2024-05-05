using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public InputField angleInputField;
    public InputField velocityInputField;
    public Button launchButton;

    public Transform target;
    [SerializeField] CameraController camController;
    [SerializeField] TextMeshProUGUI distanceNumText;
    [SerializeField] GameObject gameCanvas;

    private void Awake()
    {

    }

    private void Start()
    {
        RandomizeTargetPosition();
        float distanceToTarget = Vector3.Distance(launchPoint.position, target.position);
        distanceNumText.text = distanceToTarget.ToString("F2");
        //launchButton.onClick.AddListener(LaunchProjectile);
    }

    private void RandomizeTargetPosition()
    {
        float minX = 10f; // Example minimum x-position
        float maxX = 50f;  // Example maximum x-position
        float randomX = Random.Range(minX, maxX);
        target.position = new Vector3(randomX, target.position.y, target.position.z);
    }


    public void LaunchProjectile()
    {
        float angle = float.Parse(angleInputField.text);
        float velocity = float.Parse(velocityInputField.text);

        float angleInRadians = angle * Mathf.Deg2Rad; // Convert angle from degrees to radians

        Vector3 launchDirection = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = launchDirection * velocity;

        camController.SetProjectile(projectile);
        camController.FindProjectileObject();

        gameCanvas.SetActive(false);

    }

    public void CalculateForMissingValue()
    {
        PauseMenu.instance.pressedCalculateButton = true;

        if (string.IsNullOrEmpty(angleInputField.text))
        {
            float velocity = float.Parse(velocityInputField.text);
            float distanceToTarget = Vector3.Distance(launchPoint.position, target.position);

            // Calculate the required angle for a given velocity and distance
            float g = Physics.gravity.magnitude;
            float angleRadians = 0.5f * Mathf.Asin((distanceToTarget * g) / Mathf.Pow(velocity, 2));
            float angleDegrees = angleRadians * Mathf.Rad2Deg;

            angleInputField.text = angleDegrees.ToString("F2"); // Show calculated angle rounded to two decimal places


            PauseMenu.instance.calculatedAngle = true;
        }
        else if (string.IsNullOrEmpty(velocityInputField.text))
        {
            float angle = float.Parse(angleInputField.text);
            float distanceToTarget = Vector3.Distance(launchPoint.position, target.position);

            // Calculate the required velocity for a given angle and distance
            float angleRadians = angle * Mathf.Deg2Rad;
            float g = Physics.gravity.magnitude;
            float velocity = Mathf.Sqrt((distanceToTarget * g) / Mathf.Sin(2 * angleRadians));

            velocityInputField.text = velocity.ToString("F2"); // Show calculated velocity rounded to two decimal places


            PauseMenu.instance.calculatedVelocity = true;

        }
    }


    public void RetryLevel()
    {
        // Reset the projectile position
        if(PauseMenu.instance.ProjectileToDestroy != null)
        {
            Destroy(PauseMenu.instance.ProjectileToDestroy);
        }

        // Reset the angle and velocity input fields
        angleInputField.text = "";
        velocityInputField.text = "";

        // Reset any other relevant game state
        camController.transform.position = camController.GetStartPosition();
        camController.transform.rotation = camController.GetStartRotation();

        PauseMenu.instance.DeactivateEndLevelMenu();
        gameCanvas.SetActive(true);
    }

}
