using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public InputField angleInputField;
    public InputField velocityInputField;
    public Button launchButton;

    private void Start()
    {
        //launchButton.onClick.AddListener(LaunchProjectile);
    }

    public void LaunchProjectile()
    {
        float angle = float.Parse(angleInputField.text);
        float velocity = float.Parse(velocityInputField.text);

        float angleInRadians = angle * Mathf.Deg2Rad; // Convert angle from degrees to radians

        Vector3 launchDirection = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = launchDirection * velocity;
    }
}
