using UnityEngine;

public class ClampPlayer : MonoBehaviour
{
    public Camera MainCamera; //be sure to assign this in the inspector to your main camera
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    GameObject[] canons;

    public Transform leftWall, rightWall;

    // Start is called before the first frame update
    void Awake()
    {
        if (!GameObject.Find("AudioM").GetComponent<MusicScript>().hajjamiPlay.isPlaying)
        {
            GameObject.Find("AudioM").GetComponent<MusicScript>().hajjamiPlay.Stop();
            GameObject.Find("AudioM").GetComponent<MusicScript>().hajjamiPlay.Play();
        }

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

        canons = GameObject.FindGameObjectsWithTag("Canon");

        if (Mathf.Abs((screenBounds.x - objectWidth - 0.12f) - 4.1f) != 1.543335f) // 4.1 == default Wall x position && 1.543335 == distance between player.x limit and |wall.x|
        {
            leftWall.position = new Vector2(-((screenBounds.x - 0.15f - 0.12f) + 1.543335f), leftWall.position.y);
            rightWall.position = new Vector2((screenBounds.x - 0.15f - 0.12f) + 1.543335f, rightWall.position.y);

            foreach (GameObject canon in canons)
            {
                if (canon.transform.position.x < 0)
                    canon.transform.position = new Vector2(-(rightWall.position.x - (4.1f - Mathf.Abs(canon.transform.position.x))), canon.transform.position.y);
                if (canon.transform.position.x > 0)
                    canon.transform.position = new Vector2(rightWall.position.x - (4.1f - Mathf.Abs(canon.transform.position.x)), canon.transform.position.y);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth + 0.12f, screenBounds.x - objectWidth - 0.12f);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight + 0.12f, screenBounds.y - objectHeight - 0.12f);
        transform.position = viewPos;
    }
}
