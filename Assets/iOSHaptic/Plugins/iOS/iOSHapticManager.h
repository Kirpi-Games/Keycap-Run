#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <CoreHaptics/CoreHaptics.h>

@interface iOSHapticManager : NSObject

+(iOSHapticManager*)HapticManager;

-(void)SetupHapticGenerators;
-(void)PrepareHapticEngine;
-(void)TriggerImpactLight;
-(void)TriggerImpactMedium;
-(void)TriggerImpactHeavy;
-(void)TriggerNotificationSuccess;
-(void)TriggerNotificationWarning;
-(void)TriggerNotificationError;
-(void)TriggerSelectionChange;
-(void)ReleaseHapticGenerators;
-(void)PlayContinuousHaptic:(float)intensity :(float)sharpness :(float)time;
-(void)UpdateContinuousHaptic:(float)intensity :(float)sharpness;
-(void)PlayTransientHaptic:(float)intensity :(float)sharpness;
-(void)StopHaptic;

@end
