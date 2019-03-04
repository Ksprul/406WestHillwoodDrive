using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_SOCKTEST
{
    public class UserModuleClass_SOCKTEST : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.DigitalInput DISTARTCLIENT;
        Crestron.Logos.SplusObjects.DigitalInput DICLIENTRECONNECTENABLE;
        Crestron.Logos.SplusObjects.DigitalInput DIENABLESERVER;
        Crestron.Logos.SplusObjects.DigitalInput DIENABLEUDP;
        Crestron.Logos.SplusObjects.AnalogInput AICLIENTPORTNUMBER;
        Crestron.Logos.SplusObjects.AnalogInput AISERVERPORTNUMBER;
        Crestron.Logos.SplusObjects.AnalogInput AIUDP_PORTNUMBER;
        Crestron.Logos.SplusObjects.StringInput SICLIENTIPADDRESS;
        Crestron.Logos.SplusObjects.StringInput SISERVERIPADDRESS;
        Crestron.Logos.SplusObjects.StringInput SIUDP_IPADDRESS;
        Crestron.Logos.SplusObjects.StringInput SICLIENTTX;
        Crestron.Logos.SplusObjects.StringInput SISERVERTX;
        Crestron.Logos.SplusObjects.StringInput SIUDP_TX;
        Crestron.Logos.SplusObjects.DigitalOutput DOCLIENTCONNECTED;
        Crestron.Logos.SplusObjects.DigitalOutput DOSERVERCONNECTED;
        Crestron.Logos.SplusObjects.AnalogOutput AOCLIENTSTATUS;
        Crestron.Logos.SplusObjects.AnalogOutput AOSERVERSTATUS;
        Crestron.Logos.SplusObjects.StringOutput SOCLIENTRX;
        Crestron.Logos.SplusObjects.StringOutput SOSERVERRX;
        Crestron.Logos.SplusObjects.StringOutput SOUDP_RX;
        SplusTcpClient MYCLIENT;
        object DISTARTCLIENT_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                short STATUS = 0;
                
                
                __context__.SourceCodeLine = 35;
                STATUS = (short) ( Functions.SocketConnectClient( MYCLIENT , SICLIENTIPADDRESS , (ushort)( AICLIENTPORTNUMBER  .ShortValue ) , (ushort)( DICLIENTRECONNECTENABLE  .Value ) ) ) ; 
                __context__.SourceCodeLine = 37;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( STATUS < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 39;
                    Print( "Error connecting socket to address {0} on port  {1:d}", SICLIENTIPADDRESS , (short)AICLIENTPORTNUMBER  .UshortValue) ; 
                    }
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object DISTARTCLIENT_OnRelease_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            short STATUS = 0;
            
            
            __context__.SourceCodeLine = 51;
            STATUS = (short) ( Functions.SocketDisconnectClient( MYCLIENT ) ) ; 
            __context__.SourceCodeLine = 53;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( STATUS < 0 ))  ) ) 
                {
                __context__.SourceCodeLine = 55;
                Print( "Error disconnecting socket to address {0} on port  {1:d}", SICLIENTIPADDRESS , (short)AICLIENTPORTNUMBER  .UshortValue) ; 
                }
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object MYCLIENT_OnSocketConnect_2 ( Object __Info__ )

    { 
    SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
        int PORTNUMBER = 0;
        
        short LOCALSTATUS = 0;
        
        CrestronString REMOTEIPADDRESS;
        REMOTEIPADDRESS  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 20, this );
        
        CrestronString REQUESTEDADDRESS;
        REQUESTEDADDRESS  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
        
        
        __context__.SourceCodeLine = 75;
        DOCLIENTCONNECTED  .Value = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 77;
        LOCALSTATUS = (short) ( Functions.SocketGetAddressAsRequested( MYCLIENT , ref REQUESTEDADDRESS ) ) ; 
        __context__.SourceCodeLine = 79;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LOCALSTATUS < 0 ))  ) ) 
            {
            __context__.SourceCodeLine = 81;
            Print( "Error getting remote ip address. {0:d}\r\n", (short)LOCALSTATUS) ; 
            }
        
        __context__.SourceCodeLine = 83;
        Print( "OnConnect: Connect call to {0} successful\r\n", REQUESTEDADDRESS ) ; 
        __context__.SourceCodeLine = 87;
        PORTNUMBER = (int) ( Functions.SocketGetPortNumber( MYCLIENT ) ) ; 
        __context__.SourceCodeLine = 89;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( PORTNUMBER < 0 ))  ) ) 
            {
            __context__.SourceCodeLine = 91;
            Print( "Error getting client port number. {0:d}\r\n", (int)PORTNUMBER) ; 
            }
        
        __context__.SourceCodeLine = 95;
        LOCALSTATUS = (short) ( Functions.SocketGetRemoteIPAddress( MYCLIENT , ref REMOTEIPADDRESS ) ) ; 
        __context__.SourceCodeLine = 97;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LOCALSTATUS < 0 ))  ) ) 
            {
            __context__.SourceCodeLine = 99;
            Print( "Error getting remote ip address. {0:d}\r\n", (short)LOCALSTATUS) ; 
            }
        
        __context__.SourceCodeLine = 101;
        Print( "OnConnect: Connected to port {0:d} on address {1}\r\n", (int)PORTNUMBER, REMOTEIPADDRESS ) ; 
        __context__.SourceCodeLine = 105;
        Functions.SocketSend ( MYCLIENT , "This is sent when the socket starts." ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SocketInfo__ ); }
    return this;
    
}

object MYCLIENT_OnSocketDisconnect_3 ( Object __Info__ )

    { 
    SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
        
        __context__.SourceCodeLine = 113;
        DOCLIENTCONNECTED  .Value = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 115;
        if ( Functions.TestForTrue  ( ( DISTARTCLIENT  .Value)  ) ) 
            {
            __context__.SourceCodeLine = 117;
            Print( "Socket disconnected remotely") ; 
            }
        
        else 
            {
            __context__.SourceCodeLine = 121;
            Print( "Local disconnect complete.") ; 
            }
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SocketInfo__ ); }
    return this;
    
}

object MYCLIENT_OnSocketStatus_4 ( Object __Info__ )

    { 
    SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
        short STATUS = 0;
        
        
        __context__.SourceCodeLine = 133;
        STATUS = (short) ( __SocketInfo__.SocketStatus ) ; 
        __context__.SourceCodeLine = 135;
        AOCLIENTSTATUS  .Value = (ushort) ( STATUS ) ; 
        __context__.SourceCodeLine = 137;
        Print( "The SocketGetStatus returns:       {0:d}\r\n", (short)STATUS) ; 
        __context__.SourceCodeLine = 139;
        Print( "The MyClient.SocketStatus returns: {0:d}\r\n", (short)MYCLIENT.SocketStatus) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SocketInfo__ ); }
    return this;
    
}

object MYCLIENT_OnSocketReceive_5 ( Object __Info__ )

    { 
    SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
        
        __context__.SourceCodeLine = 147;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( MYCLIENT.SocketRxBuf ) < 256 ))  ) ) 
            {
            __context__.SourceCodeLine = 149;
            Print( "Rx: {0}", MYCLIENT .  SocketRxBuf ) ; 
            }
        
        __context__.SourceCodeLine = 155;
        SOCLIENTRX  .UpdateValue ( MYCLIENT .  SocketRxBuf  ) ; 
        __context__.SourceCodeLine = 157;
        Functions.ClearBuffer ( MYCLIENT .  SocketRxBuf ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SocketInfo__ ); }
    return this;
    
}

object SICLIENTTX_OnChange_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        short ISTATUS = 0;
        
        
        __context__.SourceCodeLine = 167;
        ISTATUS = (short) ( Functions.SocketSend( MYCLIENT , SICLIENTTX ) ) ; 
        __context__.SourceCodeLine = 169;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( ISTATUS < 0 ))  ) ) 
            {
            __context__.SourceCodeLine = 171;
            Print( "Error Sending to MyClient: {0:d}\r\n", (short)ISTATUS) ; 
            }
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}


public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    MYCLIENT  = new SplusTcpClient ( 1024, this );
    
    DISTARTCLIENT = new Crestron.Logos.SplusObjects.DigitalInput( DISTARTCLIENT__DigitalInput__, this );
    m_DigitalInputList.Add( DISTARTCLIENT__DigitalInput__, DISTARTCLIENT );
    
    DICLIENTRECONNECTENABLE = new Crestron.Logos.SplusObjects.DigitalInput( DICLIENTRECONNECTENABLE__DigitalInput__, this );
    m_DigitalInputList.Add( DICLIENTRECONNECTENABLE__DigitalInput__, DICLIENTRECONNECTENABLE );
    
    DIENABLESERVER = new Crestron.Logos.SplusObjects.DigitalInput( DIENABLESERVER__DigitalInput__, this );
    m_DigitalInputList.Add( DIENABLESERVER__DigitalInput__, DIENABLESERVER );
    
    DIENABLEUDP = new Crestron.Logos.SplusObjects.DigitalInput( DIENABLEUDP__DigitalInput__, this );
    m_DigitalInputList.Add( DIENABLEUDP__DigitalInput__, DIENABLEUDP );
    
    DOCLIENTCONNECTED = new Crestron.Logos.SplusObjects.DigitalOutput( DOCLIENTCONNECTED__DigitalOutput__, this );
    m_DigitalOutputList.Add( DOCLIENTCONNECTED__DigitalOutput__, DOCLIENTCONNECTED );
    
    DOSERVERCONNECTED = new Crestron.Logos.SplusObjects.DigitalOutput( DOSERVERCONNECTED__DigitalOutput__, this );
    m_DigitalOutputList.Add( DOSERVERCONNECTED__DigitalOutput__, DOSERVERCONNECTED );
    
    AICLIENTPORTNUMBER = new Crestron.Logos.SplusObjects.AnalogInput( AICLIENTPORTNUMBER__AnalogSerialInput__, this );
    m_AnalogInputList.Add( AICLIENTPORTNUMBER__AnalogSerialInput__, AICLIENTPORTNUMBER );
    
    AISERVERPORTNUMBER = new Crestron.Logos.SplusObjects.AnalogInput( AISERVERPORTNUMBER__AnalogSerialInput__, this );
    m_AnalogInputList.Add( AISERVERPORTNUMBER__AnalogSerialInput__, AISERVERPORTNUMBER );
    
    AIUDP_PORTNUMBER = new Crestron.Logos.SplusObjects.AnalogInput( AIUDP_PORTNUMBER__AnalogSerialInput__, this );
    m_AnalogInputList.Add( AIUDP_PORTNUMBER__AnalogSerialInput__, AIUDP_PORTNUMBER );
    
    AOCLIENTSTATUS = new Crestron.Logos.SplusObjects.AnalogOutput( AOCLIENTSTATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( AOCLIENTSTATUS__AnalogSerialOutput__, AOCLIENTSTATUS );
    
    AOSERVERSTATUS = new Crestron.Logos.SplusObjects.AnalogOutput( AOSERVERSTATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( AOSERVERSTATUS__AnalogSerialOutput__, AOSERVERSTATUS );
    
    SICLIENTIPADDRESS = new Crestron.Logos.SplusObjects.StringInput( SICLIENTIPADDRESS__AnalogSerialInput__, 256, this );
    m_StringInputList.Add( SICLIENTIPADDRESS__AnalogSerialInput__, SICLIENTIPADDRESS );
    
    SISERVERIPADDRESS = new Crestron.Logos.SplusObjects.StringInput( SISERVERIPADDRESS__AnalogSerialInput__, 256, this );
    m_StringInputList.Add( SISERVERIPADDRESS__AnalogSerialInput__, SISERVERIPADDRESS );
    
    SIUDP_IPADDRESS = new Crestron.Logos.SplusObjects.StringInput( SIUDP_IPADDRESS__AnalogSerialInput__, 256, this );
    m_StringInputList.Add( SIUDP_IPADDRESS__AnalogSerialInput__, SIUDP_IPADDRESS );
    
    SICLIENTTX = new Crestron.Logos.SplusObjects.StringInput( SICLIENTTX__AnalogSerialInput__, 256, this );
    m_StringInputList.Add( SICLIENTTX__AnalogSerialInput__, SICLIENTTX );
    
    SISERVERTX = new Crestron.Logos.SplusObjects.StringInput( SISERVERTX__AnalogSerialInput__, 256, this );
    m_StringInputList.Add( SISERVERTX__AnalogSerialInput__, SISERVERTX );
    
    SIUDP_TX = new Crestron.Logos.SplusObjects.StringInput( SIUDP_TX__AnalogSerialInput__, 256, this );
    m_StringInputList.Add( SIUDP_TX__AnalogSerialInput__, SIUDP_TX );
    
    SOCLIENTRX = new Crestron.Logos.SplusObjects.StringOutput( SOCLIENTRX__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SOCLIENTRX__AnalogSerialOutput__, SOCLIENTRX );
    
    SOSERVERRX = new Crestron.Logos.SplusObjects.StringOutput( SOSERVERRX__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SOSERVERRX__AnalogSerialOutput__, SOSERVERRX );
    
    SOUDP_RX = new Crestron.Logos.SplusObjects.StringOutput( SOUDP_RX__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SOUDP_RX__AnalogSerialOutput__, SOUDP_RX );
    
    
    DISTARTCLIENT.OnDigitalPush.Add( new InputChangeHandlerWrapper( DISTARTCLIENT_OnPush_0, false ) );
    DISTARTCLIENT.OnDigitalRelease.Add( new InputChangeHandlerWrapper( DISTARTCLIENT_OnRelease_1, false ) );
    MYCLIENT.OnSocketConnect.Add( new SocketHandlerWrapper( MYCLIENT_OnSocketConnect_2, false ) );
    MYCLIENT.OnSocketDisconnect.Add( new SocketHandlerWrapper( MYCLIENT_OnSocketDisconnect_3, false ) );
    MYCLIENT.OnSocketStatus.Add( new SocketHandlerWrapper( MYCLIENT_OnSocketStatus_4, false ) );
    MYCLIENT.OnSocketReceive.Add( new SocketHandlerWrapper( MYCLIENT_OnSocketReceive_5, false ) );
    SICLIENTTX.OnSerialChange.Add( new InputChangeHandlerWrapper( SICLIENTTX_OnChange_6, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SOCKTEST ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint DISTARTCLIENT__DigitalInput__ = 0;
const uint DICLIENTRECONNECTENABLE__DigitalInput__ = 1;
const uint DIENABLESERVER__DigitalInput__ = 2;
const uint DIENABLEUDP__DigitalInput__ = 3;
const uint AICLIENTPORTNUMBER__AnalogSerialInput__ = 0;
const uint AISERVERPORTNUMBER__AnalogSerialInput__ = 1;
const uint AIUDP_PORTNUMBER__AnalogSerialInput__ = 2;
const uint SICLIENTIPADDRESS__AnalogSerialInput__ = 3;
const uint SISERVERIPADDRESS__AnalogSerialInput__ = 4;
const uint SIUDP_IPADDRESS__AnalogSerialInput__ = 5;
const uint SICLIENTTX__AnalogSerialInput__ = 6;
const uint SISERVERTX__AnalogSerialInput__ = 7;
const uint SIUDP_TX__AnalogSerialInput__ = 8;
const uint DOCLIENTCONNECTED__DigitalOutput__ = 0;
const uint DOSERVERCONNECTED__DigitalOutput__ = 1;
const uint AOCLIENTSTATUS__AnalogSerialOutput__ = 0;
const uint AOSERVERSTATUS__AnalogSerialOutput__ = 1;
const uint SOCLIENTRX__AnalogSerialOutput__ = 2;
const uint SOSERVERRX__AnalogSerialOutput__ = 3;
const uint SOUDP_RX__AnalogSerialOutput__ = 4;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
