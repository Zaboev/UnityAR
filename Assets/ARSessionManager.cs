using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARSession))]
public class ARSessionManager : MonoBehaviour
{
    private ARSession session;

    void Awake()
    {
        session = GetComponent<ARSession>();
    }

    void Start()
    {
        // ќтключаем автоматическое обновление ARCore (главное!)
        // ¬ старых верси€х это поле называетс€ attemptUpdate, в новых Ч тоже, но может быть скрыто
        var attemptUpdateField = typeof(ARSession).GetField("m_attemptUpdate",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (attemptUpdateField != null)
            attemptUpdateField.SetValue(session, false);

        StartCoroutine(ForceStart());
    }

    private IEnumerator ForceStart()
    {
        yield return null;
        session.enabled = true;
        Debug.Log("AR Session принудительно запущен на S7 Edge (ARCore 1.38)");
    }
}