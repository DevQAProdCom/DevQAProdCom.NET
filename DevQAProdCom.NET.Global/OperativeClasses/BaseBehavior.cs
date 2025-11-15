using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.Global.OperativeClasses
{
    public class BaseBehavior : IBehavior
    {
        public IBehaviorParameters Parameters { get; }

        public BaseBehavior(IBehaviorParameters parameters)
        {
            if (!(parameters?.ParamsDictionary.Count > 0))
                throw new ArgumentNullException("parameters");

            Parameters = parameters;
        }
    }
}
