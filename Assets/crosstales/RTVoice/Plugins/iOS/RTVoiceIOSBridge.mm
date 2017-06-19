//
//  RTVoiceIOSBridge.mm
//
//  Copyright 2016 www.crosstales.com
//
#import "RTVoiceIOSBridge.h"
#import <AVFoundation/AVFoundation.h>
#import <Foundation/Foundation.h>

@interface RTVoiceIOSBridge () <AVSpeechSynthesizerDelegate>
@property (readwrite, nonatomic, strong) AVSpeechSynthesizer *synthesizer;
@end

@implementation RTVoiceIOSBridge
@synthesize synthesizer = _synthesizer;

AVSpeechSynthesizer *MySynthesizer;
//char *gameObjectName;

- (AVSpeechSynthesizer *)synthesizer
{
    if (!_synthesizer)
    {
        _synthesizer = [[AVSpeechSynthesizer alloc] init];
        _synthesizer.delegate = self;
    }
    return _synthesizer;
}


/**
 * Speaks the string with a given rate, pitch, volume and culture.
 * @param text Text to speak
 * @param rate Speech rate of the speaker in percent
 * @param pitch Pitch of the speech in percent
 * @param volume Volume of the speaker in percent
 * @param culture Culture of the voice to speak
 */
- (void)speak:(NSString *)text rate:(float)rate pitch:(float)pitch volume:(float)volume culture:(NSString *)culture
{
    if (!_synthesizer)
    {
        _synthesizer = [[AVSpeechSynthesizer alloc] init];
        _synthesizer.delegate = self;
    }
    
    [MySynthesizer stopSpeakingAtBoundary:AVSpeechBoundaryImmediate];
    
    if (text)
    {
        AVSpeechSynthesisVoice *voice = [AVSpeechSynthesisVoice voiceWithLanguage:culture];
        AVSpeechUtterance *utterance = [[AVSpeechUtterance alloc] initWithString:text];
        utterance.voice = voice;
        
        float adjustedRate = AVSpeechUtteranceDefaultSpeechRate * rate;
        
        if (adjustedRate > AVSpeechUtteranceMaximumSpeechRate)
        {
            adjustedRate = AVSpeechUtteranceMaximumSpeechRate;
        }
        
        if (adjustedRate < AVSpeechUtteranceMinimumSpeechRate)
        {
            adjustedRate = AVSpeechUtteranceMinimumSpeechRate;
        }
        
        utterance.rate = adjustedRate;
        utterance.volume = volume;
        
        float pitchMultiplier = pitch;
        if ((pitchMultiplier >= 0.5) && (pitchMultiplier <= 2.0))
        {
            utterance.pitchMultiplier = pitchMultiplier;
        }
        MySynthesizer = _synthesizer;
        [_synthesizer speakUtterance:utterance];
    }
    
}

/**
 * Stops speaking
 */
- (void)stop
{
    [MySynthesizer stopSpeakingAtBoundary:AVSpeechBoundaryImmediate];
}

/** 
 * Collects and sends all voices to RTVoice.
 */
- (void)setVoices
{
    
    NSArray *voices = [AVSpeechSynthesisVoice speechVoices];
    
    NSString *appendstring = @"";
    for (AVSpeechSynthesisVoice *voiceName in voices) {
        if([[[UIDevice currentDevice]systemVersion]floatValue] < 9){
            appendstring = [appendstring stringByAppendingString:voiceName.language];
        } else {
            appendstring = [appendstring stringByAppendingString:voiceName.name];
        }
        appendstring = [appendstring stringByAppendingString:@","];
        appendstring = [appendstring stringByAppendingString:voiceName.language];
        appendstring = [appendstring stringByAppendingString:@","];
        
    }
    
    UnitySendMessage("RTVoice", "SetVoices", [appendstring UTF8String]);
	//UnitySendMessage(gameObjectName, "SetVoices", [appendstring UTF8String]);
	
}

/**
 * Called when the speak is finished and informs RTVoice.
 */
- (void)speechSynthesizer:(AVSpeechSynthesizer *)synthesizer didFinishSpeechUtterance:(AVSpeechUtterance *)utterance
{
    UnitySendMessage("RTVoice", "SetState", "Finish");
	//UnitySendMessage(gameObjectName, "SetState", "Finish");
}

/** 
 * Called when the synthesizer have began to speak a word and informs RTVoice.
 */
- (void)speechSynthesizer:(AVSpeechSynthesizer *)synthesizer willSpeakRangeOfSpeechString:(NSRange)characterRange utterance:(AVSpeechUtterance *)utterance
{
    UnitySendMessage("RTVoice", "WordSpoken", "w");//[substringcutout UTF8String]);
	//UnitySendMessage(gameObjectName, "WordSpoken", "w");//[substringcutout UTF8String]);
}

/**
 * Called when the speak is canceled and informs RTVoice.
 */
- (void)speechSynthesizer:(AVSpeechSynthesizer *)synthesizer didCancelSpeechUtterance:(AVSpeechUtterance *)utterance
{
    UnitySendMessage("RTVoice", "SetState", "Cancel");
	//UnitySendMessage(gameObjectName, "SetState", "Cancel");
}

/**
 * Called when the speak is started and informs RTVoice.
 */
- (void)speechSynthesizer:(AVSpeechSynthesizer *)synthesizer didStartSpeechUtterance:(AVSpeechUtterance *)utterance
{
    UnitySendMessage("RTVoice", "SetState", "Start");
	//UnitySendMessage(gameObjectName, "SetState", "Start");
}

/**
 * Called when the application finished launching
 */
/*
- (void)applicationDidFinishLaunching:(NSNotification *)aNotification
{
    [self setVoices];
}
*/


@end

extern void sendMessage(const char *, const char *, const char *);

extern "C" {
    
    /**
	 * Bridge to speak the string that it receives with a given rate, pitch, volume and culture.
     * @param gameObject Name of the Unity gameObject to receive the messages
     * @param text Text to speak
     * @param rate Speech rate of the speaker in percent
     * @param pitch Pitch of the speech in percent
	 * @param volume Volume of the speaker in percent
     * @param culture Culture of the voice to speak
     */
    void Speak(char *gameObject, char *text, float rate, float pitch, float volume, char *culture)
    {
		//gameObjectName = gameObject;
        NSString *messageFromRTVoice = [NSString stringWithUTF8String:text];
        NSString *cultur = [NSString stringWithUTF8String:culture];

        [[RTVoiceIOSBridge alloc] speak:messageFromRTVoice rate:rate pitch:pitch volume:volume culture:cultur];
        
    }
    
    /**
	 * Bridge to the stop speaking.
     */
    void Stop()
    {
        [[RTVoiceIOSBridge alloc] stop];
    }
    
    /** 
	 * Bridge to order all voices.
     */
    void GetVoices()
    {
        [[RTVoiceIOSBridge alloc] setVoices];
    }
}