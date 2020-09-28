using System;
using SimpleJson;

namespace JsonLogic.Net {
    public interface IManageOperators 
    {
        void AddOperator(string name, Func<IProcessJsonLogic, JsonArray, object, object> operation);

        Func<IProcessJsonLogic, JsonArray, object, object> GetOperator(string name);

        void DeleteOperator(string name);
    }
}