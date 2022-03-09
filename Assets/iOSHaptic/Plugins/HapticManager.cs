using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class HapticManager {

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _setupHapticGenerators();
	#endif

	public static void SetupHapticGenerators()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_setupHapticGenerators();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _prepareHapticEngine();
	#endif

	public static void PrepareHapticEngine()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_prepareHapticEngine();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _triggerImpactLight();
	#endif

	public static void TriggerImpactLight()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_triggerImpactLight();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _triggerImpactMedium();
	#endif

	public static void TriggerImpactMedium()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_triggerImpactMedium();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _triggerImpactHeavy();
	#endif

	public static void TriggerImpactHeavy()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_triggerImpactHeavy();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _triggerNotificationSuccess();
	#endif

	public static void TriggerNotificationSuccess()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_triggerNotificationSuccess();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _triggerNotificationWarning();
	#endif

	public static void TriggerNotificationWarning()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_triggerNotificationWarning();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _triggerNotificationError();
	#endif

	public static void TriggerNotificationError()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_triggerNotificationError();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _triggerSelectionChange();
	#endif

	public static void TriggerSelectionChange()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_triggerSelectionChange();
		#endif
	}

	#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _releaseHapticGenerators();
	#endif

	public static void ReleaseHapticGenerators()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		_releaseHapticGenerators();
		#endif
	}

#if UNITY_IOS && !UNITY_EDITOR
	    [DllImport("__Internal")]
	    private static extern void _playContinuousHaptic(float intensity, float sharpness, float time);
#endif

	public static void PlayContinuousHaptic(float intensity, float sharpness, float time)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        _playContinuousHaptic(intensity, sharpness, time);
        #endif
	}

#if UNITY_IOS && !UNITY_EDITOR
	    [DllImport("__Internal")]
	    private static extern void _updateContinuousHaptic(float intensity, float sharpness);
#endif

	public static void UpdateContinuousHaptic(float intensity, float sharpness)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        _updateContinuousHaptic(intensity, sharpness);
        #endif
	}

#if UNITY_IOS && !UNITY_EDITOR
	    [DllImport("__Internal")]
	    private static extern void _playTransientHaptic(float intensity, float sharpness);
#endif

	public static void PlayTransientHaptic(float intensity, float sharpness)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        _playTransientHaptic(intensity, sharpness);
        #endif
	}

    #if UNITY_IOS && !UNITY_EDITOR
	    [DllImport("__Internal")]
	    private static extern void _stopHaptic();
    #endif

	public static void StopHaptic()
	{
        #if UNITY_IOS && !UNITY_EDITOR
        _stopHaptic();
        #endif
	}
}
