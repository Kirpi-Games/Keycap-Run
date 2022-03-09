#import "iOSHapticManager.h"

void UnityPause(int pause);
void UnitySetAudioSessionActive(bool active);
UIViewController *UnityGetGLViewController();

@interface iOSHapticManager()

@property(nonatomic,retain)UIImpactFeedbackGenerator *impactGenerator;
@property(nonatomic,retain)UISelectionFeedbackGenerator *selectionGenerator;
@property(nonatomic,retain)UINotificationFeedbackGenerator *notificationGenerator;

@property(nonatomic, strong) CHHapticEngine* engine;
@property(nonatomic, strong) id<CHHapticAdvancedPatternPlayer> continuousPlayer;
@property(nonatomic) BOOL isEngineStarted;
@property(nonatomic) BOOL isEngineStopping;

@end

@implementation iOSHapticManager

+(iOSHapticManager*)HapticManager
{
    static iOSHapticManager *sharedSingleton;
    
    if(!sharedSingleton)
        sharedSingleton = [[iOSHapticManager alloc] init];
    
    return sharedSingleton;
}

-(id)init
{
    if((self = [super init]))
    {
        [self SetupHapticGenerators];
    }
    
    return self;
}

-(void)SetupHapticGenerators
{
    _impactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleLight];
    _selectionGenerator = [[UISelectionFeedbackGenerator alloc] init];
    _notificationGenerator = [[UINotificationFeedbackGenerator alloc] init];
}

-(void)PrepareHapticEngine
{
    if(_impactGenerator)
        [_impactGenerator prepare];
    
    if(_selectionGenerator)
        [_selectionGenerator prepare];
    
    if(_notificationGenerator)
        [_notificationGenerator prepare];
}

-(void)TriggerImpactLight
{
    _impactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleLight];
    [_impactGenerator impactOccurred];
}

-(void)TriggerImpactMedium
{
    _impactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium];
    [_impactGenerator impactOccurred];
}

-(void)TriggerImpactHeavy
{
    _impactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleHeavy];
    [_impactGenerator impactOccurred];
}

-(void)TriggerNotificationSuccess
{
    [_notificationGenerator notificationOccurred:UINotificationFeedbackTypeSuccess];
}

-(void)TriggerNotificationWarning
{
    [_notificationGenerator notificationOccurred:UINotificationFeedbackTypeWarning];
}

-(void)TriggerNotificationError
{
    [_notificationGenerator notificationOccurred:UINotificationFeedbackTypeError];
}

-(void)TriggerSelectionChange
{
    [_selectionGenerator selectionChanged];
}

-(void)ReleaseHapticGenerators
{
    if(_impactGenerator)
        _impactGenerator = nil;
    
    if(_notificationGenerator)
        _notificationGenerator = nil;
    
    if(_selectionGenerator)
        _selectionGenerator = nil;
}

- (void)CreateEngine {
        NSError* error = nil;
        _engine = [[CHHapticEngine alloc] initAndReturnError:&error];

        if (error == nil) {

            _engine.playsHapticsOnly = true;
            __weak iOSHapticManager *weakSelf = self;

            _engine.stoppedHandler = ^(CHHapticEngineStoppedReason reason) {
                NSLog(@"Stopping because: %ld", (long)reason);
                switch (reason) {
                    case CHHapticEngineStoppedReasonAudioSessionInterrupt:
                        NSLog(@"Haptic Engine audio session interrupted");
                        break;
                    case CHHapticEngineStoppedReasonApplicationSuspended:
                        NSLog(@"Haptic Engine application suspended");
                        break;
                    case CHHapticEngineStoppedReasonIdleTimeout:
                        NSLog(@"Haptic Engine idle timeout");
                        break;
                    case CHHapticEngineStoppedReasonSystemError:
                        NSLog(@"Haptic Engine system error");
                        break;
                    case CHHapticEngineStoppedReasonNotifyWhenFinished:
                        NSLog(@"Haptic Engine playback finished");
                        break;

                    default:
                        NSLog(@"Haptic Engine unknown error");
                        break;
                }

                weakSelf.isEngineStarted = false;
            };

            _engine.resetHandler = ^{
                [weakSelf StartEngine];
            };
        } else {
            NSLog(@"Haptic Engine initialization error: %@", error);
        }
}

- (void)StartEngine {
    if (!_isEngineStarted) {
        NSError* error = nil;
        [_engine startAndReturnError:&error];

        if (error != nil) {
            NSLog(@"Couldn't start Haptic Engine: %@", error);
        } else {
            _isEngineStarted = true;
        }
    }
}

-(void)PlayContinuousHaptic:(float)intensity :(float)sharpness :(float)time
{
    if (intensity > 1 || intensity <= 0) return;
    if (sharpness > 1 || sharpness < 0) return;
    if (time <= 0 || time > 30) return;

    if (self.engine == NULL) {
        [self CreateEngine];
    }
    [self StartEngine];

    [self CreateContinuousPlayer:sharpness :intensity :time];

    NSError* error = nil;
    [_continuousPlayer startAtTime:0 error:&error];

    if (error != nil) {
        NSLog(@"Haptic Engine couldn't play continuously: %@", error);
    } else {

    }
    
}

-(void)UpdateContinuousHaptic:(float)intensity :(float)sharpness
{
    if (intensity > 1 || intensity <= 0) return;
    if (sharpness > 1 || sharpness < 0) return;

    CHHapticDynamicParameter* intensityParam = [[CHHapticDynamicParameter alloc] initWithParameterID:CHHapticDynamicParameterIDHapticIntensityControl value:intensity relativeTime:0];
    CHHapticDynamicParameter* sharpnessParam = [[CHHapticDynamicParameter alloc] initWithParameterID:CHHapticDynamicParameterIDHapticSharpnessControl value:sharpness relativeTime:0];

    NSError* error = nil;
    [_continuousPlayer sendParameters:@[sharpnessParam, intensityParam] atTime:0 error:&error];

    if (error != nil) {
        NSLog(@"Couldn't update continuous parameters: %@", error);
    }
    
}

-(void)PlayTransientHaptic:(float)intensity :(float)sharpness
{
      if (intensity > 1 || intensity <= 0) return;
      if (sharpness > 1 || sharpness < 0) return;

          if (self.engine == NULL) {
              [self CreateEngine];
          }
          [self StartEngine];

          CHHapticEventParameter* intensityParam = [[CHHapticEventParameter alloc] initWithParameterID:CHHapticEventParameterIDHapticIntensity value:intensity];
          CHHapticEventParameter* sharpnessParam = [[CHHapticEventParameter alloc] initWithParameterID:CHHapticEventParameterIDHapticSharpness value:sharpness];

          CHHapticEvent* event = [[CHHapticEvent alloc] initWithEventType:CHHapticEventTypeHapticTransient parameters:@[sharpnessParam, intensityParam] relativeTime:0];

          NSError* error = nil;
          CHHapticPattern* pattern = [[CHHapticPattern alloc] initWithEvents:@[event] parameters:@[] error:&error];

          if (error == nil) {
              id<CHHapticPatternPlayer> player = [_engine createPlayerWithPattern:pattern error:&error];

              if (error == nil) {
                  [player startAtTime:0 error:&error];
              } else {
                  NSLog(@"Couldn't create transient: %@", error);
              }
          } else {
              NSLog(@"Couldn't create transient pattern: %@", error);
          }
      
}

-(void)StopHaptic
{
    NSError* error = nil;
    if (_continuousPlayer != NULL)
        [_continuousPlayer stopAtTime:0 error:&error];

    if (_engine != NULL && _isEngineStarted && !_isEngineStopping)
    {
        __weak iOSHapticManager *weakSelf = self;

        _isEngineStopping = true;
        [_engine stopWithCompletionHandler:^(NSError *error) {
            if (error != nil) {
            NSLog(@"Haptic engine stopped with error: %@", error);
            }
            weakSelf.isEngineStarted = false;
            weakSelf.isEngineStopping = false;
        }];
    }
}

- (void)CreateContinuousPlayer:(float)intensity :(float)sharpness :(float) time {
        CHHapticEventParameter* myIntensity = [[CHHapticEventParameter alloc] initWithParameterID:CHHapticEventParameterIDHapticIntensity value:intensity];
        CHHapticEventParameter* mySharpness = [[CHHapticEventParameter alloc] initWithParameterID:CHHapticEventParameterIDHapticSharpness value:sharpness];

        CHHapticEvent* event = [[CHHapticEvent alloc] initWithEventType:CHHapticEventTypeHapticContinuous parameters:@[myIntensity, mySharpness] relativeTime:0 duration:time];

        NSError* error = nil;
        CHHapticPattern* pattern = [[CHHapticPattern alloc] initWithEvents:@[event] parameters:@[] error:&error];

        if (error == nil) {
            _continuousPlayer = [_engine createAdvancedPlayerWithPattern:pattern error:&error];
        } else {
            NSLog(@"Couldn't create continuous player: %@", error);
        }
}


@end
