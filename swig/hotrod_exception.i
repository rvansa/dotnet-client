%insert(runtime) %{
    typedef enum {
        HotRod_TransportException,
        HotRod_InvalidResponseException,
        HotRod_RemoteNodeSuspectException,
        HotRod_InternalException,
        HotRod_RemoteCacheManagerNotStartedException,
        HotRod_UnsupportedOperationException,
        HotRod_HotRodClientException,
        HotRod_Exception,

    } HotRodExceptionCodes;

    typedef void (SWIGSTDCALL* CSharpExceptionCallback_t)(HotRodExceptionCodes errcode, const char *);

    CSharpExceptionCallback_t customExceptionCallback;

    extern "C" SWIGEXPORT
    void SWIGSTDCALL CustomExceptionRegisterCallback(CSharpExceptionCallback_t callback) {
        customExceptionCallback = callback;
    }

    static void SWIG_CSharpSetPendingExceptionCustom(HotRodExceptionCodes errcode, const char *msg) {
        customExceptionCallback(errcode, msg);
    }
    %}

%pragma(csharp) imclasscode=%{
     class CustomExceptionHelper {
         public delegate void CustomExceptionDelegate(int errcode, string message);

         static CustomExceptionDelegate customDelegate = new CustomExceptionDelegate(SetPendingCustomException);

         [DllImport("$dllimport", EntryPoint="CustomExceptionRegisterCallback")]
         public static extern void CustomExceptionRegisterCallback(CustomExceptionDelegate customCallback);

         static void SetPendingCustomException(int errcode, string message) {
             switch(errcode) {
             case 0:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.TransportException(message));
                 break;
             case 1:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.InvalidResponseException(message));
                 break;
             case 2:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.RemoteNodeSuspectException(message));
                 break;
             case 3:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.InternalException(message));
                 break;
             case 4:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.RemoteCacheManagerNotStartedException(message));
                 break;
             case 5:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.UnsupportedOperationException(message));
                 break;
             case 6:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.HotRodClientException(message));
                 break;
             case 7:
             default:
                 SWIGPendingException.Set(new Infinispan.HotRod.Exceptions.Exception(message));
                 break;
             }
         }

         static CustomExceptionHelper() {
             CustomExceptionRegisterCallback(customDelegate);
         }
     }
     static CustomExceptionHelper exceptionHelper = new CustomExceptionHelper();
     %}

%exception {
    try {
        $action
    } catch (const infinispan::hotrod::TransportException& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_TransportException, e.what());

    } catch (const infinispan::hotrod::InvalidResponseException& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_InvalidResponseException, e.what());

    } catch (const infinispan::hotrod::RemoteNodeSuspectException& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_RemoteNodeSuspectException, e.what());

    } catch (const infinispan::hotrod::InternalException& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_InternalException, e.what());

    } catch (const infinispan::hotrod::RemoteCacheManagerNotStartedException& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_RemoteCacheManagerNotStartedException, e.what());

    } catch (const infinispan::hotrod::UnsupportedOperationException& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_UnsupportedOperationException, e.what());

    } catch (const infinispan::hotrod::HotRodClientException& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_HotRodClientException, e.what());

    } catch (const infinispan::hotrod::Exception& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_Exception, e.what());

    } catch (const std::exception& e) {
        SWIG_CSharpSetPendingExceptionCustom(HotRod_Exception, e.what());
    }
 };
