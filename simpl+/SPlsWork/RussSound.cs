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

namespace UserModule_RUSSSOUND
{
    public class UserModuleClass_RUSSSOUND : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        CrestronString CLIENTIPADDRESS;
        ushort CLIENTPORTNUMBER = 0;
        Crestron.Logos.SplusObjects.DigitalInput Z1_VOLUP;
        Crestron.Logos.SplusObjects.DigitalInput Z1_VOLDOWN;
        Crestron.Logos.SplusObjects.DigitalInput Z1_POWEROFF;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE1;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE2;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE3;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE4;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE5;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE6;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE7;
        Crestron.Logos.SplusObjects.DigitalInput Z1_SOURCE8;
        SplusTcpClient RUSSSOUNDSOCKET;
        object Z1_VOLUP_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                short STATUS = 0;
                
                
                __context__.SourceCodeLine = 19;
                STATUS = (short) ( Functions.SocketConnectClient( RUSSSOUNDSOCKET , CLIENTIPADDRESS , (ushort)( CLIENTPORTNUMBER ) , (ushort)( 0 ) ) ) ; 
                __context__.SourceCodeLine = 20;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( STATUS < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 21;
                    Print( "Error connecting socket to address {0} on port  {1:d}", CLIENTIPADDRESS , (short)CLIENTPORTNUMBER) ; 
                    }
                
                __context__.SourceCodeLine = 23;
                Functions.SocketSend ( RUSSSOUNDSOCKET , "EVENT C[1].Z[1]!KeyPress VolumeUp\r" ) ; 
                __context__.SourceCodeLine = 25;
                STATUS = (short) ( Functions.SocketDisconnectClient( RUSSSOUNDSOCKET ) ) ; 
                __context__.SourceCodeLine = 26;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( STATUS < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 27;
                    Print( "Error disconnecting socket to address {0} on port  {1:d}", CLIENTIPADDRESS , (short)CLIENTPORTNUMBER) ; 
                    }
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object Z1_VOLDOWN_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 39;
        CLIENTIPADDRESS  .UpdateValue ( "10.0.0.148"  ) ; 
        __context__.SourceCodeLine = 40;
        CLIENTPORTNUMBER = (ushort) ( 9621 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    CLIENTIPADDRESS  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 64, this );
    RUSSSOUNDSOCKET  = new SplusTcpClient ( 1024, this );
    
    Z1_VOLUP = new Crestron.Logos.SplusObjects.DigitalInput( Z1_VOLUP__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_VOLUP__DigitalInput__, Z1_VOLUP );
    
    Z1_VOLDOWN = new Crestron.Logos.SplusObjects.DigitalInput( Z1_VOLDOWN__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_VOLDOWN__DigitalInput__, Z1_VOLDOWN );
    
    Z1_POWEROFF = new Crestron.Logos.SplusObjects.DigitalInput( Z1_POWEROFF__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_POWEROFF__DigitalInput__, Z1_POWEROFF );
    
    Z1_SOURCE1 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE1__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE1__DigitalInput__, Z1_SOURCE1 );
    
    Z1_SOURCE2 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE2__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE2__DigitalInput__, Z1_SOURCE2 );
    
    Z1_SOURCE3 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE3__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE3__DigitalInput__, Z1_SOURCE3 );
    
    Z1_SOURCE4 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE4__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE4__DigitalInput__, Z1_SOURCE4 );
    
    Z1_SOURCE5 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE5__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE5__DigitalInput__, Z1_SOURCE5 );
    
    Z1_SOURCE6 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE6__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE6__DigitalInput__, Z1_SOURCE6 );
    
    Z1_SOURCE7 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE7__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE7__DigitalInput__, Z1_SOURCE7 );
    
    Z1_SOURCE8 = new Crestron.Logos.SplusObjects.DigitalInput( Z1_SOURCE8__DigitalInput__, this );
    m_DigitalInputList.Add( Z1_SOURCE8__DigitalInput__, Z1_SOURCE8 );
    
    
    Z1_VOLUP.OnDigitalPush.Add( new InputChangeHandlerWrapper( Z1_VOLUP_OnPush_0, false ) );
    Z1_VOLDOWN.OnDigitalPush.Add( new InputChangeHandlerWrapper( Z1_VOLDOWN_OnPush_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_RUSSSOUND ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint Z1_VOLUP__DigitalInput__ = 0;
const uint Z1_VOLDOWN__DigitalInput__ = 1;
const uint Z1_POWEROFF__DigitalInput__ = 2;
const uint Z1_SOURCE1__DigitalInput__ = 3;
const uint Z1_SOURCE2__DigitalInput__ = 4;
const uint Z1_SOURCE3__DigitalInput__ = 5;
const uint Z1_SOURCE4__DigitalInput__ = 6;
const uint Z1_SOURCE5__DigitalInput__ = 7;
const uint Z1_SOURCE6__DigitalInput__ = 8;
const uint Z1_SOURCE7__DigitalInput__ = 9;
const uint Z1_SOURCE8__DigitalInput__ = 10;

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
