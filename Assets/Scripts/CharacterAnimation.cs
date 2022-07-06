using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.001f;

    [SerializeField]
    private float duration = 1f;

    private string walkAnime = "Walk";
    private Vector3 forwards = new(0, 0, -1);
    private Vector3 backwards = new(0, 0, 1);

    private Animator anim;

    void Start()
    {
        TryGetComponent(out anim);
    }

    public void Walk() {
        StartCoroutine(Walking(forwards, duration, speed));
    }


    public void Back() {
        StartCoroutine(Walking(backwards, duration, speed));
    }


    private IEnumerator Walking(Vector3 direction, float duration, float speed) {
        float time = 0.0f;
        anim.SetFloat(walkAnime, 1.0f);

        while (time < duration) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, direction, speed);
            time += Time.deltaTime;
            yield return null;
        }
        anim.SetFloat(walkAnime, 0);
    }
}
