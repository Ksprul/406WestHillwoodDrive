namespace RIO;
        // class declarations
         class Driver;
     class Driver 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Configure ( STRING RussIPAddress , SIGNED_LONG_INTEGER DebugOn );
        FUNCTION SelectSonos ( SIGNED_LONG_INTEGER Zone );
        FUNCTION SelectAppleTV ( SIGNED_LONG_INTEGER Zone );
        FUNCTION ZoneOff ( SIGNED_LONG_INTEGER Zone );
        FUNCTION SetVolume ( SIGNED_LONG_INTEGER Zone , SIGNED_LONG_INTEGER Volume );
        FUNCTION SendMsg ( STRING TCPMessage );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

