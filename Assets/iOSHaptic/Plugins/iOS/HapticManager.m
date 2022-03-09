#import "iOSHapticManager.h"

// Converts NSString to C style string by way of copy (Mono will free it)
#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isn't empty
#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

void _setupHapticGenerators()
{
    [[iOSHapticManager HapticManager] SetupHapticGenerators];
}

void _prepareHapticEngine()
{
    [[iOSHapticManager HapticManager] PrepareHapticEngine];
}

void _triggerImpactLight()
{
    [[iOSHapticManager HapticManager] TriggerImpactLight];
}

void _triggerImpactMedium()
{
    [[iOSHapticManager HapticManager] TriggerImpactMedium];
}

void _triggerImpactHeavy()
{
    [[iOSHapticManager HapticManager] TriggerImpactHeavy];
}

void _triggerNotificationSuccess()
{
    [[iOSHapticManager HapticManager] TriggerNotificationSuccess];
}

void _triggerNotificationWarning()
{
    [[iOSHapticManager HapticManager] TriggerNotificationWarning];
}

void _triggerNotificationError()
{
    [[iOSHapticManager HapticManager] TriggerNotificationError];
}

void _triggerSelectionChange()
{
    [[iOSHapticManager HapticManager] TriggerSelectionChange];
}

void _releaseHapticGenerators()
{
    [[iOSHapticManager HapticManager] ReleaseHapticGenerators];
}

void _playContinuousHaptic(float intensity, float sharpness, float time)
{
    [[iOSHapticManager HapticManager] PlayContinuousHaptic:intensity:sharpness:time];
}

void _updateContinuousHaptic(float intensity, float sharpness)
{
    [[iOSHapticManager HapticManager] UpdateContinuousHaptic:intensity:sharpness];
}

void _playTransientHaptic(float intensity, float sharpness)
{
    [[iOSHapticManager HapticManager] PlayTransientHaptic:intensity:sharpness];
}

void _stopHaptic()
{
    [[iOSHapticManager HapticManager] StopHaptic];
}
