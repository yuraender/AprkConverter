using System.Collections.Generic;

namespace AprkConverter.Entities.Classes {

    public class Step {

        public string Name {
            get; private set;
        }
        public short NumStep {
            get; private set;
        }

        public List<Parameter> Parameters {
            get; private set;
        }

        public Step(string name, short numStep) {
            Name = name;
            NumStep = numStep;
            Parameters = new List<Parameter>();
        }

        public void AddParameter(Parameter parameter) {
            Parameters.Add(parameter);
        }
    }

    public class Parameter {

        public bool Show {
            get; private set;
        }
        public bool ShowName {
            get; private set;
        }
        public string Param {
            get; private set;
        }
        public string DimVal {
            get; private set;
        }
        public short? UpDev {
            get; set;
        }
        public short? LowDev {
            get; set;
        }
        public short? Qual {
            get; set;
        }
        public string IdCad {
            get; private set;
        }

        public Parameter(bool show, bool showName, string param, string dimVal, string idCad) {
            Show = show;
            ShowName = showName;
            Param = param;
            DimVal = dimVal;
            UpDev = null;
            LowDev = null;
            Qual = null;
            IdCad = idCad;
        }

        public Parameter(
            bool show, bool showName,
            string param, string dimVal, short? upDev, short? lowDev,
            string idCad
        ) {
            Show = show;
            ShowName = showName;
            Param = param;
            DimVal = dimVal;
            UpDev = upDev;
            LowDev = lowDev;
            Qual = null;
            IdCad = idCad;
        }

        public Parameter(
            bool show, bool showName,
            string param, string dimVal, short? qual,
            string idCad
        ) {
            Show = show;
            ShowName = showName;
            Param = param;
            DimVal = dimVal;
            UpDev = null;
            LowDev = null;
            Qual = qual;
            IdCad = idCad;
        }
    }
}
