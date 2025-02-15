using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public LayerMask wallLayer;  // Duvarlar için LayerMask ekledik.

    private void Start()
    {
        randomReplacement();
    }

    private void randomReplacement()
    {
        Bounds bounds = this.gridArea.bounds;
        Vector3 newPosition;

        int maxAttempts = 100; // Sonsuz döngüden kaçýnmak için maksimum deneme sayýsý
        int attempts = 0;

        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            newPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
            attempts++;

            // Eðer belirli bir denemede uygun bir konum bulunamazsa, döngüyü kýr.
            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Uygun bir konum bulunamadý!");
                return;
            }

        } while (Physics2D.OverlapCircle(newPosition, 0.1f, wallLayer)); // Yeni konum duvarla çakýþýyorsa tekrar dene

        this.transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) => randomReplacement();
}
