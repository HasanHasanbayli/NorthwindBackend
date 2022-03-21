using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Exception;

public class ExceptionLogAspect : MethodInterception
{
    private LoggerServiceBase _loggerServiceBase;

    public ExceptionLogAspect(Type loggerServiceBase)
    {
        if (loggerServiceBase.BaseType != typeof(LoggerServiceBase))
        {
            throw new System.Exception(AspectMessages.WrongLoggerType);
        }

        _loggerServiceBase = (LoggerServiceBase) Activator.CreateInstance(loggerServiceBase);
    }

    protected override void OnException(IInvocation invocation)
    {
    }
}