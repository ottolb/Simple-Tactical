
extern "C" {
    extern "C" {
        const char * _GetCFBundleVersion() {
            NSString *version = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleShortVersionString"];
            return strdup([version UTF8String]);
        }
        
        const char * _GetCFBundleVersionCode() {
            NSString *version = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleVersion"];
            return strdup([version UTF8String]);
        }
    }
}

