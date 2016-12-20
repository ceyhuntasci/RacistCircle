using System.Collections.Generic;
using com.adjust.sdk;

public class AdjustAdapter {
    private const string ADJUST_APP_TOKEN = "zhbgqr3x95a8";

    public static void Start() {
        AdjustConfig config = new AdjustConfig( ADJUST_APP_TOKEN, AdjustEnvironment.Production );
        config.setLogLevel( AdjustLogLevel.Info );
        Adjust.start( config );
    }

    public void Destroy() {}

    public static void TrackEvent(string eventName)
    {
        Adjust.trackEvent( new AdjustEvent(eventName) );
    }
}