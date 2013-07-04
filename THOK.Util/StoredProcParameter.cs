namespace THOK.Util
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;

    public class StoredProcParameter
    {
        private Dictionary<string, Parameter> parameters = new Dictionary<string, Parameter>();

        public void AddParameter(string parameterName, object parameterValue)
        {
            Parameter parameter = new Parameter {
                ParameterName = parameterName,
                ParameterValue = parameterValue
            };
            this.parameters.Add(parameterName, parameter);
        }

        public void AddParameter(string parameterName, object parameterValue, DbType parameterType)
        {
            Parameter parameter = new Parameter {
                ParameterName = parameterName,
                ParameterValue = parameterValue,
                ParameterType = parameterType
            };
            this.parameters.Add(parameterName, parameter);
        }

        public void AddParameter(string parameterName, object parameterValue, DbType parameterType, ParameterDirection direction)
        {
            Parameter parameter = new Parameter {
                ParameterName = parameterName,
                ParameterValue = parameterValue,
                ParameterType = parameterType,
                ParameterDirectioin = direction
            };
            this.parameters.Add(parameterName, parameter);
        }

        public object this[string parameterName]
        {
            get
            {
                return this.parameters[parameterName].ParameterValue;
            }
        }

        internal Dictionary<string, Parameter> Parameters
        {
            get
            {
                return this.parameters;
            }
        }
    }
}

