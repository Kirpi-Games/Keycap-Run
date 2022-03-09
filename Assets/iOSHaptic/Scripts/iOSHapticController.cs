using UnityEngine;
using System.Collections;

public class iOSHapticController : MonoBehaviour {

	public static iOSHapticController instance = null;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void SetupHapticGenerators()
	{
		HapticManager.SetupHapticGenerators();
	}

	public void PrepareHapticEngine()
	{
		HapticManager.PrepareHapticEngine();
	}

	public void TriggerImpactLight()
	{
		HapticManager.TriggerImpactLight();
	}

	public void TriggerImpactMedium()
	{
		HapticManager.TriggerImpactMedium();
	}

	public void TriggerImpactHeavy()
	{
		HapticManager.TriggerImpactHeavy();
	}

	public void TriggerNotificationSuccess()
	{
		HapticManager.TriggerNotificationSuccess();
	}

	public void TriggerNotificationWarning()
	{
		HapticManager.TriggerNotificationWarning();
	}

	public void TriggerNotificationError()
	{
		HapticManager.TriggerNotificationError();
	}

	public void TriggerSelectionChange()
	{
		HapticManager.TriggerSelectionChange();
	}

	public void ReleaseHapticGenerators()
	{
		HapticManager.ReleaseHapticGenerators();
	}

    public void PlayTransientHaptic(float intensity, float sharpness)
    {
		HapticManager.PlayTransientHaptic(intensity, sharpness);
    }

    public void PlayContinuousHaptic(float intensity, float sharpness, float time)
    {
		HapticManager.PlayContinuousHaptic(intensity, sharpness, time);
    }

    public void UpdateContinuousHaptic(float intensity, float sharpness)
    {
		HapticManager.UpdateContinuousHaptic(intensity, sharpness);
    }

    public void StopHaptic()
    {
		HapticManager.StopHaptic();
    }
}
