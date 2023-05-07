using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFilter;
    [SerializeField] private TextMeshProUGUI _textOut;
    [SerializeField] private RectTransform _scanZone;

    private bool _isCamAvailable;
    private WebCamTexture _cameraTexture;

    private void Start()
    {
        SetUpCamera();
    }
    private void Update()
    {
        UpdateCameraRender();   
    }
    private void FixedUpdate()
    {
        Scan();
    }
    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length==0)
        {
            _isCamAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing==false)
            _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
        }
        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvailable= true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvailable==false) 
            return;
        float ratio = (float)_cameraTexture.width/(float) _cameraTexture.height;
        _aspectRatioFilter.aspectRatio = ratio;

        int orientation = _cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0,270f);
    }
    
    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);
            if (result != null)
            {
                char delimiter = ',';
                string[] parts = result.Text.Split(delimiter);
                float x = float.Parse(parts[0]);
                float y = float.Parse(parts[1]);
                float z = float.Parse(parts[2]);
                DataManager.SaveCoordinates(x, y, z);
                SceneManager.LoadScene("MainScene");
            }
        }
        catch { }
    }
}
