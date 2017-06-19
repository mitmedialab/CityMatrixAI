//
//  RTVoiceIOSBridge.h
//
//  Copyright 2016 www.crosstales.com
//
#ifndef RTVoiceIOSBridge_h
#define RTVoiceIOSBridge_h

@interface RTVoiceIOSBridge:NSObject
- (void)setVoices;
- (void)speak:(NSString *)text rate:(float)rate pitch:(float)pitch volume:(float)volume culture:(NSString *)culture;
- (void)stop;
@end


#ifdef __cplusplus
extern "C" {
    
    void UnitySendMessage(const char *, const char *, const char *);
    
}
#endif

#endif