using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChairTrigger : MonoBehaviour
{
    public string nextSceneName; // Set the scene name in Inspector
    public float fadeDuration = 2f; // Duration of fade effect
    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading) // Detect player sitting
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    IEnumerator FadeAndLoadScene()
    {
        isFading = true;

        // Start Fade Out
        FadeScreen(1);
        yield return new WaitForSeconds(fadeDuration);

        // Load Next Scene
        SceneManager.LoadScene(nextSceneName);

        // Start Fade In (after new scene loads)
        yield return new WaitForSeconds(0.5f); // Short delay for scene load
        FadeScreen(0);

        isFading = false;
    }

    void FadeScreen(float targetAlpha)
    {
        GameObject fadeObject = new GameObject("FadeScreen");
        MeshRenderer renderer = fadeObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Unlit/Color"));
        renderer.material.color = new Color(0, 0, 0, targetAlpha); // Black screen

        // Position a plane in front of the camera
        fadeObject.transform.localScale = new Vector3(5, 5, 1);
        fadeObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        fadeObject.transform.LookAt(Camera.main.transform.position);
        fadeObject.transform.Rotate(0, 180, 0); // Face the camera

        StartCoroutine(FadeEffect(renderer.material, targetAlpha));
    }

    IEnumerator FadeEffect(Material material, float targetAlpha)
    {
        float elapsed = 0;
        float startAlpha = material.color.a;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            material.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }
        material.color = new Color(0, 0, 0, targetAlpha);
    }
}
