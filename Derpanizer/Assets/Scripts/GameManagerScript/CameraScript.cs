using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float Speed = 2.0F;
    public float RotationSpeed = 100.0F;

    public float SpeedH = 2.5f;
    public float SpeedV = 2.5f;

    private float _yaw = 0.0f;
    private float _pitch = 0.0f;

    private bool _gameRunning = false;

    void Update()
    {
        if (_gameRunning)
        {
            // camera translation wasd
            var vertTranslation = Input.GetAxis("Vertical") * Speed;
            var horTranslation = Input.GetAxis("Horizontal") * Speed;
            transform.Translate(horTranslation, 0, vertTranslation);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(transform.up.x, transform.up.y * Speed, transform.up.z);
                //transform.Translate(transform.up);
            }


            //float rotation = Input.GetAxis("Horizontal") * RotationSpeed;
            //translation *= Time.deltaTime;
            //rotation *= Time.deltaTime;
            //transform.Translate(0, 0, translation);
            //transform.Rotate(0, rotation, 0);

            // camera rotation mouse
            _yaw += SpeedH * Input.GetAxis("Mouse X");
            _pitch -= SpeedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
        }

    }

    public void SetGameRunning(bool running)
    {
        _gameRunning = running;
    }
}