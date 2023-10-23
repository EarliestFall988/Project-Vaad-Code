using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreatorCameraController : MonoBehaviour
{
    public Transform FullBody;
    public Transform Head;

    public bool HeadActive = false;
    private bool isHeadActive = false;

    public Transform Camera;

    public TMPro.TMP_Text text;


    void Start()
    {
        SetFullBody();
    }

    private void SetHead()
    {
        // LeanTween.cancelAll();

        LeanTween.value(gameObject, (v) =>
        {
            Camera.transform.position = v;
        }, Camera.transform.position, Head.transform.position, 2f).setEaseInOutCubic();

        LeanTween.value(gameObject, (v) =>
        {
            Camera.transform.rotation = Quaternion.Euler(v);
        }, Camera.transform.rotation.eulerAngles, Head.transform.rotation.eulerAngles, 2f).setEaseInOutCubic();

        // Camera.transform.position = Vector3.Lerp(Camera.transform.position, Head.position, 0.015f);
        // Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, Head.rotation, 0.015f);
    }

    private void SetFullBody()
    {

        // LeanTween.cancelAll();

        LeanTween.value(gameObject, (v) =>
        {
            Camera.transform.position = v;
        }, Camera.transform.position, FullBody.transform.position, 1f).setEaseInOutCubic();

        LeanTween.value(gameObject, (v) =>
        {
            Camera.transform.rotation = Quaternion.Euler(v);
        }, Camera.transform.rotation.eulerAngles, FullBody.transform.rotation.eulerAngles, 1f).setEaseInOutCubic();

        // Camera.transform.position = Vector3.Lerp(Camera.transform.position, FullBody.position, 0.015f);
        // Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, FullBody.rotation, 0.015f);
    }

    private void Update()
    {
        if (HeadActive && !isHeadActive)
        {
            SetHead();

            if (text != null)
                text.text = "Head";

            isHeadActive = true;
        }
        else if (isHeadActive && !HeadActive)
        {
            SetFullBody();

            if (text != null)
                text.text = "Full Body";

            isHeadActive = false;
        }
    }

    public void ToggleCamera()
    {
        HeadActive = !HeadActive;
    }

    public void SetCamera(bool headActive)
    {
        StartCoroutine(SetCameraAfter(0.5f, headActive));
    }

    IEnumerator SetCameraAfter(float time, bool headActive)
    {
        yield return new WaitForSeconds(time);

        HeadActive = !headActive;
        ToggleCamera();
    }
}
